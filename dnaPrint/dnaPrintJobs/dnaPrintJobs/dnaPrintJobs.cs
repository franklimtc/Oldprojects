using System;
using System.ServiceProcess;
using System.Timers;
using System.Collections.Generic;

namespace dnaPrintJobs
{
    partial class dnaPrintJobs : ServiceBase
    {
        static string diretorio1 = Util.RetornaDiretorio() + @"\logs";
        //private static Log filelog = new Log("dnaPrintJobs", diretorio1);
        Timer timer;
        Timer timerEnvio;

        public dnaPrintJobs()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio1);
            // TODO: Add code here to start your service.
            timer = new Timer();
            timerEnvio = new Timer();

            timer.Interval = new TimeSpan(0, 1, 0).TotalMilliseconds;
            timerEnvio.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;

            try
            {
                //timer.Start();
                
                timer.Elapsed += new ElapsedEventHandler(LerJobs);
                timer.Enabled = true;
                
            }
            catch(Exception ex)
            { 
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
            }
            

            try
            {
                //timerEnvio.Start();

                timerEnvio.Elapsed += new ElapsedEventHandler(EnviarJobs);
                timerEnvio.Enabled = true;
            }
            catch(Exception ex)
            {
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
            }
            

            filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
            filelog.Escrever(Log.TipoLogs.info, "Servico iniciado.");
            filelog.Escrever(Log.TipoLogs.info, "***************************************************************");

        }

        protected override void OnStop()
        {
            Log filelog = new Log("dnaPrintJobs", diretorio1);
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
            filelog.Escrever(Log.TipoLogs.info, "Servico parado.");
            filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
        }

        protected void EnviarJobs(object sender, ElapsedEventArgs e)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio1);
            timerEnvio.Enabled = false;

            filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
            filelog.Escrever(Log.TipoLogs.info, "Iniciando envio dos arquivos impressos para o servidor.");
            filelog.Escrever(Log.TipoLogs.info, "***************************************************************");

            string origem = Util.RetornaDiretorio();
            int arquivos_enviados = 0;
            List<string> lista = new List<string>();

            if (true)
            {
                lista = Util.ListarArquivos(@origem + @"\jobs");

                foreach (string arq in lista)
                {
                    string query = Util.LerTxt(arq);

                    if (PrinterJob.Inserir(query))
                    {
                        if (Util.DeletarArquivo(arq))
                        {
                            arquivos_enviados++;
                        }
                        else
                        {
                            filelog.Escrever(Log.TipoLogs.erro, "Falha ao tentar excluir o arquivo: " + arq);
                        }
                    }
                    else
                    {
                        filelog.Escrever(Log.TipoLogs.erro, "Falha ao tentar inserir o arquivo: " + arq);
                    }
                }
                if (!PrinterJob.ConfigFilas())
                {
                    filelog.Escrever(Log.TipoLogs.erro, "Falha ao tentar configurar as filas de impressão");
                }
            }
            
           

            if (arquivos_enviados > 0)
            {
                filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
                filelog.Escrever(Log.TipoLogs.info, "Arquivos listados: " + lista.Count.ToString());
                filelog.Escrever(Log.TipoLogs.info, "Arquivos enviados: " + arquivos_enviados.ToString());

                //***********************************************************************************************************

                //filelog.Escrever(Log.TipoLogs.info, "Envio concluido. Proximo em " + dtUltimoEnvio.Value.AddHours(1).ToString());
                //filelog.Escrever(Log.TipoLogs.info, "***************************************************************");
            }

            

            timerEnvio.Enabled = true;
        }

        protected void LerJobs(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            string origem = Util.RetornaDiretorio();
            PrinterJob.EnviaArquivo(origem, origem);
            PrinterJob.ColetarJobs(System.Environment.MachineName, Util.RetornaDiretorio(), DateTime.Now);
            timer.Enabled = true;
        }
    }
}
