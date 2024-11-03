using System;
using System.ServiceProcess;
using System.Printing;

namespace dnaPrintJobsMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }

        #region Teste
        //static string diretorio1 = Util.RetornaDiretorio() + @"\logs";
        ////static string diretorio1 = Util.RetornaDiretorio();


        //static void Main()
        //{
        //    Log filelog = new Log("dnaPrintJobsMonitor", diretorio1);

        //    if (TotalJobs() < 5000 && StatusServico("dnaPrintJobs"))
        //    {
        //        string machine = Util.Criptografar(Util.Chave(), Util.Vetor(), Environment.MachineName);
        //        string versao = Util.Criptografar(Util.Chave(), Util.Vetor(), "2.0.0");
        //        string dtInicial = Util.Criptografar(Util.Chave(), Util.Vetor(), "null");
        //        string dtFinal = Util.Criptografar(Util.Chave(), Util.Vetor(), "null");
        //        string usuario = Util.Criptografar(Util.Chave(), Util.Vetor(), "old");
        //        try
        //        {
        //            if (new bnb.intra.capgv.s1sbdp01.estacao().Atualizar(machine, versao, dtInicial, dtFinal, usuario, "senha"))
        //            {
        //                filelog.Escrever(Log.TipoLogs.info, machine + " - " + versao + " - " + dtInicial + " - " + dtFinal + " - " + usuario);
        //            }
        //            else
        //            {
        //                filelog.Escrever(Log.TipoLogs.info, machine + " - " + versao + " - " + dtInicial + " - " + dtFinal + " - " + usuario);
        //                filelog.Escrever(Log.TipoLogs.erro, "Não foi possível inserir a atualização da estação.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
        //        }

        //    }
        //    else
        //    {
        //        ReiniciaServico("dnaPrintJobs");
        //        new bnb.intra.capgv.s1sbdp01.estacao().RegistroLog("SERVIDOR - FALHA", string.Format("Falha no serviço dnaPrintJobs do servidor {0}.", Environment.MachineName));
        //    }
        //}

        //public static int TotalJobs()
        //{
        //    PrintServer pServer = new LocalPrintServer();
        //    PrintQueueCollection listaImpressoras = pServer.GetPrintQueues();
        //    int qtdJobs = 0;
        //    foreach (PrintQueue print in listaImpressoras)
        //    {
        //        qtdJobs += print.NumberOfJobs;
        //    }
        //    return qtdJobs;
        //}

        //public static bool StatusServico(string ServiceName)
        //{
        //    bool result = false;

        //    ServiceController sc;
        //    ServiceControllerStatus status;
        //    try
        //    {
        //        sc = new ServiceController(ServiceName);
        //        status = sc.Status;
        //    }
        //    catch
        //    {
        //        status = ServiceControllerStatus.Stopped;
        //    }

        //    if (status == ServiceControllerStatus.Running)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        //filelog.Escrever(Log.TipoLogs.erro, "Serviço dnaprintJobs encontra-se parado.");
        //    }

        //    return result;
        //}

        //public static void ReiniciaServico(string ServiceName)
        //{
        //    ServiceController sc = new ServiceController();
        //    ServiceControllerStatus status;
        //    try
        //    {
        //        sc = new ServiceController(ServiceName);
        //        status = sc.Status;
        //    }
        //    catch
        //    {
        //        status = ServiceControllerStatus.Stopped;
        //    }

        //    if (status != ServiceControllerStatus.Running)
        //    {
        //        //filelog.Escrever(Log.TipoLogs.info, "Serviço dnaprintJobs está sendo reiniciado.");
        //        try
        //        {
        //            sc.Start();

        //        }
        //        catch(Exception ex)
        //        {
        //            ex.ToString();
        //        }
        //    }
        //}

        #endregion

    }
}
