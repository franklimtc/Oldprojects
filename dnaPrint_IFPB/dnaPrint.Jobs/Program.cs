using System;
using System.Printing;
using System.Threading;
using PrinterQueueWatch;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace dnaPrint.Jobs
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Lendo jobs.");

                ManterArquivosImpressos();

                Console.WriteLine($"{DateTime.Now.ToString()} - ######## 10s #########");
                Thread.Sleep(new TimeSpan(0, 0, 10));
            }
        }

        public static void GetJobs()
        {
            try
            {
                LocalPrintServer localPrintServer = new LocalPrintServer();
                var printers = localPrintServer.GetPrintQueues();
                foreach (var printer in printers)
                {
                    var jobs = printer.GetPrintJobInfoCollection();
                    foreach (var job in jobs)
                    {
                        Console.WriteLine(job.Name);
                        Console.WriteLine(job.JobStatus);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        public static void GetJobs2()
        {
            try
            {
                PrinterQueueWatch.PrintServer localPrintServer = new PrinterQueueWatch.PrintServer();
                PrinterQueueWatch.PrinterInformationCollection printers = localPrintServer.Printers;

                foreach (PrinterQueueWatch.PrinterInformation printer in printers)
                {
                    string tsqlInsert = @"INSERT INTO ArquivoImpresso(Submitted, Document, UserName, DriverName, MachineName, PaperKind, Copies, TotalPages, PagesPrinted, PrinterName, Color, JobId, NotifyUserName, PrintProcessorName, server)
VALUES(@Submitted, @Document, @UserName, @DriverName, @MachineName, @PaperKind, @Copies, @TotalPages, @PagesPrinted, @PrinterName, @Color, @JobId, @NotifyUserName, @PrintProcessorName, @server);";

                    PrintJobCollection pjc = new PrintJobCollection(printer.PrinterName, 10);
                    foreach (PrinterQueueWatch.PrintJob job in pjc)
                    {
                        if (job.Printed == true)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString()} - Inserindo o job de id {job.JobId}");
                            List<object[]> parametros = new List<object[]>();
                            parametros.Add(new object[] { "@Submitted", job.Submitted });
                            parametros.Add(new object[] { "@Document", job.Document });
                            parametros.Add(new object[] { "@UserName", job.UserName });
                            parametros.Add(new object[] { "@DriverName", job.DriverName });
                            parametros.Add(new object[] { "@MachineName", job.MachineName });
                            parametros.Add(new object[] { "@PaperKind", job.PaperKind.ToString() });
                            parametros.Add(new object[] { "@Copies", job.Copies });
                            parametros.Add(new object[] { "@TotalPages", job.TotalPages });
                            parametros.Add(new object[] { "@PagesPrinted", job.PagesPrinted });
                            parametros.Add(new object[] { "@PrinterName", job.PrinterName });
                            parametros.Add(new object[] { "@Color", job.Color });
                            parametros.Add(new object[] { "@JobId", job.JobId });
                            parametros.Add(new object[] { "@NotifyUserName", job.NotifyUserName });
                            parametros.Add(new object[] { "@PrintProcessorName", job.PrintProcessorName });
                            parametros.Add(new object[] { "@server", Environment.MachineName });
                            if (Inserir(tsqlInsert, parametros))
                            {
                                try
                                {
                                    job.Delete();
                                }
                                catch
                                {
                                    job.Cancel();
                                }
                            }

                        }

                    }
                    #region Teste
                    //Console.WriteLine(
                    //job.Submitted
                    //+ ", " + job.Document
                    //+ ", " + job.UserName
                    //+ ", " + job.DriverName
                    //+ ", " + job.MachineName
                    //+ ", " + job.PaperKind
                    //+ ", " + job.Copies
                    //+ ", " + job.TotalPages
                    //+ ", " + job.PagesPrinted
                    //+ ", " + job.PrinterName
                    //+ ", " + job.Color
                    //+ ", " + job.JobId
                    //+ ", " + job.NotifyUserName
                    //+ ", " + job.PrintProcessorName
                    //);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                string tsql = "INSERT INTO logs(componente, mensagem, data) values(@componente, @mensagem, GETDATE());";
                Inserir(tsql);

                Console.WriteLine(ex.Message);
            }
        }

        public static void ManterArquivosImpressos()
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\keepprintedjobs.vbs";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
            }
            catch (Exception ex)
            {
                string tsql = "INSERT INTO logs(componente, mensagem, data) values(@componente, @mensagem, GETDATE());";
                Inserir(tsql);

                Console.WriteLine(ex.Message);
            }
        }

        public static bool Inserir(string query, List<object[]> parametros = null)
        {
            string connString = "Server=10.0.67.1;Database=dnaPrint;User Id=dnaprint;Password=dnaprint;";
            int linhas = 0;
            bool result = false;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comand = new SqlCommand(query, conn);

            foreach (var par in parametros)
            {
                comand.Parameters.AddWithValue(par[0].ToString(), par[1]);
            }

            try
            {
                conn.Open();
                linhas = comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }

            if (linhas > 0)
                result = true;

            return result;
        }

    }
}
