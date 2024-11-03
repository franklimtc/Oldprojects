using System;
using System.Diagnostics;
using PrinterQueueWatch;
using System.Configuration;
using System.IO;

namespace dnaPrint.Service
{
    public class PrinterJob
    {
        //static string connString = "Server=172.25.131.114;Port=5432;Database=dnaprint;User Id=dnaprint; Password=dnaprint;";
        static string connString = ConfigurationManager.ConnectionStrings["dnaprint"].ToString();
        static DAO.Operacoes.tipo tpDB = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());
        static string key = "317498F1EB83DFA4C3D999F5C1D4A5B591AAAAD0BFDFE6939E6B6E5F26036E3B";

        public static int ColetarJobs(string diretorio, DateTime dt)
        {
            int resultado = 0;

            try
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = @diretorio + @"\keepprintedjobs.vbs";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                }
                catch (Exception ex)
                {
                    //filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            { }

            try
            {
                try
                {
                    PrinterQueueWatch.PrintServer pServer = new PrinterQueueWatch.PrintServer();

                    foreach (PrinterQueueWatch.PrinterInformation p in pServer.Printers)
                    {
                        
                        PrintJobCollection pjc = new PrintJobCollection(p.PrinterName, 100);

                        foreach (PrintJob pj in pjc)
                        {
                            if (pj.Printed)
                            {
                                int p01 = BoolToInt(pj.Color);
                                int p02 = pj.Copies;
                                string p03 = pj.DataType;
                                int p04 = BoolToInt(pj.Deleted);
                                int p05 = BoolToInt(pj.Deleting);
                                string p06 = pj.Document.Trim().Replace("'", "");
                                string p07 = pj.DriverName;
                                int p08 = BoolToInt(pj.InError);
                                int p09 = pj.JobId;
                                int p10 = pj.JobSize;
                                int p11 = BoolToInt(pj.Landscape);
                                string p12 = pj.MachineName;
                                string p13 = pj.NotifyUserName;
                                int p14 = BoolToInt(pj.Offline);
                                int p15 = pj.PagesPrinted;
                                string p16 = pj.PaperKind.ToString();
                                int p17 = pj.PaperLength;
                                int p18 = BoolToInt(pj.PaperOut);
                                string p19 = pj.PaperSource.ToString();
                                int p20 = pj.PaperWidth;
                                string p21 = pj.Parameters;
                                int p22 = BoolToInt(pj.Paused);
                                int p23 = pj.Position;
                                int p24 = BoolToInt(pj.Printed);
                                string p25 = pj.PrinterName;
                                string p26 = pj.PrinterResolutionKind.ToString();
                                int p27 = pj.PrinterResolutionX;
                                int p28 = pj.PrinterResolutionY;
                                int p29 = BoolToInt(pj.Printing);
                                string p30 = pj.PrintProcessorName;
                                int p31 = pj.Priority;
                                int p32 = pj.QueuedTime;
                                int p33 = BoolToInt(pj.Spooling);
                                string p34 = pj.StatusDescription;
                                string p35 = pj.Submitted.ToString("yyyMMdd HH:mm:ss");
                                string p36 = pj.TimeWindow.ToString();
                                int p37 = pj.TotalPages;
                                int p38 = BoolToInt(pj.UserInterventionRequired);
                                string p39 = pj.UserName;
                                string p40 = Environment.MachineName;
                                int duplex = VerificarDuplex(pj.JobId);
                                string cmd = string.Format("insert into ArquivoImpresso(Color,Copies,DataType,Deleted,Deleting,Document,DriverName,InError,JobId,JobSize,Landscape,MachineName,NotifyUserName,Offline,PagesPrinted,PaperKind,PaperLength,PaperOut,PaperSource,PaperWidth,Parameters,Paused,Position,Printed,PrinterName,PrinterResolutionKind,PrinterResolutionX,PrinterResolutionY,Printing,PrintProcessorName,Priority,QueuedTime,Spooling,StatusDescription,Submitted,TimeWindow,TotalPages,UserInterventionRequired,UserName, server, duplex) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',{40})",
                                    p01, p02, p03, p04, p05, p06, p07, p08, p09, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, duplex.ToString());

                                int insertResult = new DAO.Operacoes(connString, tpDB).ExecuteNonQuery(cmd);
                                if (insertResult > 0)
                                {
                                    resultado++;
                                    pj.Delete();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            { }

            return resultado;
        }

        private static int VerificarDuplex(int jobId)
        {
            string file = $"0000{jobId}";

            if(jobId < 9)
                file = $"0000{jobId}";
            else if(jobId < 100)
                file = $"000{jobId}";
            else if (jobId < 1000)
                file = $"00{jobId}";
            else if (jobId < 10000)
                file = $"0{jobId}";
            else 
                file = $"{jobId}";

            string path = $@"C:\Windows\System32\spool\PRINTERS\{file.PadLeft(5)}.spl";
            int result = 0;
            string line;
            try
            {
                StreamReader sr = new StreamReader(path);
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("DUPLEX=ON"))
                    {
                        result = 1;
                        break;
                    }
                    else if (line.Contains("language"))
                    {
                        break;
                    }

                }
                sr.Close();
            }
            catch 
            {

            }
            return result;
            
        }

        public static void ColetarJobsDistr(string diretorio, DateTime dt)
        {

            try
            {
                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = @diretorio + @"\keepprintedjobs.vbs";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                }
                catch (Exception ex)
                {
                    //filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            { }

            try
            {
                try
                {
                    PrinterQueueWatch.PrintServer pServer = new PrinterQueueWatch.PrintServer();

                    foreach (PrinterQueueWatch.PrinterInformation p in pServer.Printers)
                    {
                        PrintJobCollection pjc = new PrintJobCollection(p.PrinterName, 100);

                        foreach (PrintJob pj in pjc)
                        {
                            if (pj.Printed)
                            {
                                int p01 = BoolToInt(pj.Color);
                                int p02 = pj.Copies;
                                string p03 = pj.DataType;
                                int p04 = BoolToInt(pj.Deleted);
                                int p05 = BoolToInt(pj.Deleting);
                                string p06 = pj.Document.Trim().Replace("'", "");
                                string p07 = pj.DriverName;
                                int p08 = BoolToInt(pj.InError);
                                int p09 = pj.JobId;
                                int p10 = pj.JobSize;
                                int p11 = BoolToInt(pj.Landscape);
                                string p12 = pj.MachineName;
                                string p13 = pj.NotifyUserName;
                                int p14 = BoolToInt(pj.Offline);
                                int p15 = pj.PagesPrinted;
                                string p16 = pj.PaperKind.ToString();
                                int p17 = pj.PaperLength;
                                int p18 = BoolToInt(pj.PaperOut);
                                string p19 = pj.PaperSource.ToString();
                                int p20 = pj.PaperWidth;
                                string p21 = pj.Parameters;
                                int p22 = BoolToInt(pj.Paused);
                                int p23 = pj.Position;
                                int p24 = BoolToInt(pj.Printed);
                                string p25 = pj.PrinterName;
                                string p26 = pj.PrinterResolutionKind.ToString();
                                int p27 = pj.PrinterResolutionX;
                                int p28 = pj.PrinterResolutionY;
                                int p29 = BoolToInt(pj.Printing);
                                string p30 = pj.PrintProcessorName;
                                int p31 = pj.Priority;
                                int p32 = pj.QueuedTime;
                                int p33 = BoolToInt(pj.Spooling);
                                string p34 = pj.StatusDescription;
                                string p35 = pj.Submitted.ToString("yyyMMdd HH:mm:ss");
                                string p36 = pj.TimeWindow.ToString();
                                int p37 = pj.TotalPages;
                                int p38 = BoolToInt(pj.UserInterventionRequired);
                                string p39 = pj.UserName;
                                string p40 = Environment.MachineName;


                                int duplex = VerificarDuplex(pj.JobId);
                                string insertJob = string.Format("insert into ArquivoImpresso(Color,Copies,DataType,Deleted,Deleting,Document,DriverName,InError,JobId,JobSize,Landscape,MachineName,NotifyUserName,Offline,PagesPrinted,PaperKind,PaperLength,PaperOut,PaperSource,PaperWidth,Parameters,Paused,Position,Printed,PrinterName,PrinterResolutionKind,PrinterResolutionX,PrinterResolutionY,Printing,PrintProcessorName,Priority,QueuedTime,Spooling,StatusDescription,Submitted,TimeWindow,TotalPages,UserInterventionRequired,UserName, server, duplex) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',{40})",
                                    p01, p02, p03, p04, p05, p06, p07, p08, p09, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, duplex.ToString());

                                //string insertJob = string.Format("insert into ArquivoImpresso(Color,Copies,DataType,Deleted,Deleting,Document,DriverName,InError,JobId,JobSize,Landscape,MachineName,NotifyUserName,Offline,PagesPrinted,PaperKind,PaperLength,PaperOut,PaperSource,PaperWidth,Parameters,Paused,Position,Printed,PrinterName,PrinterResolutionKind,PrinterResolutionX,PrinterResolutionY,Printing,PrintProcessorName,Priority,QueuedTime,Spooling,StatusDescription,Submitted,TimeWindow,TotalPages,UserInterventionRequired,UserName, server) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}')",
                                //    p01, p02, p03, p04, p05, p06, p07, p08, p09, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40);

                                
                                ws.OperacoesClient op = new ws.OperacoesClient();
                                if (op.Exec(key, insertJob, null))
                                {
                                    pj.Delete();
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            { }

            //return resultado;
        }

        public static int BoolToInt(bool valor)
        {
            if (valor)
                return 1;
            else
                return 0;
        }
    }
}
