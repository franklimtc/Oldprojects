using System.ServiceProcess;
using System.Configuration;
using System;
using System.Collections.Generic;

namespace dnaPrintJobs
{
    class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new dnaPrintJobs() 
            };
            ServiceBase.Run(ServicesToRun);
        }

        #region Teste
        //static string diretorio1 = Util.RetornaDiretorio() + @"\logs";
        //static void Main()
        //{
        //    LerJobs();
        //    EnviarJobs();
        //}

        //static void LerJobs()
        //{
        //string origem = Util.RetornaDiretorio();
        //    PrinterJob.EnviaArquivo(origem, origem);
        //    PrinterJob.ColetarJobs(System.Environment.MachineName, Util.RetornaDiretorio(), DateTime.Now);
        //}

        //static void EnviarJobs()
        //{
        //    Log filelog = new Log("dnaPrintJobs", diretorio1);

        //    filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
        //    filelog.Escrever(Log.TipoLogs.info, "Iniciando envio dos arquivos impressos para o servidor.");
        //    filelog.Escrever(Log.TipoLogs.info, "***************************************************************");

        //    string origem = Util.RetornaDiretorio();
        //    int arquivos_enviados = 0;
        //    List<string> lista = new List<string>();

        //    if (true)
        //    {
        //        lista = Util.ListarArquivos(@origem + @"\jobs");

        //        foreach (string arq in lista)
        //        {
        //            string query = Util.LerTxt(arq);

        //            if (PrinterJob.Inserir(query))
        //            {
        //                if (Util.DeletarArquivo(arq))
        //                {
        //                    arquivos_enviados++;
        //                }
        //                else
        //                {
        //                    filelog.Escrever(Log.TipoLogs.erro, "Falha ao tentar excluir o arquivo: " + arq);
        //                }
        //            }
        //            else
        //            {
        //                filelog.Escrever(Log.TipoLogs.erro, "Falha ao tentar inserir o arquivo: " + arq);
        //            }
        //        }
        //    }



        //    if (arquivos_enviados > 0)
        //    {
        //        filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
        //        filelog.Escrever(Log.TipoLogs.info, "Arquivos listados: " + lista.Count.ToString());
        //        filelog.Escrever(Log.TipoLogs.info, "Arquivos enviados: " + arquivos_enviados.ToString());

        //        //***********************************************************************************************************

        //        //filelog.Escrever(Log.TipoLogs.info, "Envio concluido. Proximo em " + dtUltimoEnvio.Value.AddHours(1).ToString());
        //        //filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
        //    }

        //}
        #endregion
    }
}