using System;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using System.Reflection;
using System.Printing;


namespace dnaPrintJobsMonitor
{
    public partial class Service1 : ServiceBase
    {
        Timer timer;
        static string diretorio1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\logs";
        private static Log filelog = new Log("dnaPrintJobsMonitor", diretorio1);

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = new TimeSpan(0, 30, 0).TotalMilliseconds;
            try
            {
                timer.Elapsed += new ElapsedEventHandler(VerificarServico);
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
            }
        }

        protected override void OnStop()
        {
        }

        protected void VerificarServico(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;

            if (StatusServico("dnaPrintJobs"))
            {
                string machine = Util.Criptografar(Util.Chave(), Util.Vetor(), Environment.MachineName);
                string versao = Util.Criptografar(Util.Chave(), Util.Vetor(), "2.0.0");
                string dtInicial = Util.Criptografar(Util.Chave(), Util.Vetor(), DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss.000"));
                string dtFinal = Util.Criptografar(Util.Chave(), Util.Vetor(), DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss.000"));
                string usuario = Util.Criptografar(Util.Chave(), Util.Vetor(), "old");

                if (new bnb.intra.capgv.s1sbdp01.estacao().Atualizar(machine, versao, dtInicial, dtFinal, usuario, "senha"))
                {
                    //string text = null;
                }
            }
            else
            {
                ReiniciaServico("dnaPrintJobs");
                new bnb.intra.capgv.s1sbdp01.estacao().RegistroLog("SERVIDOR - FALHA", string.Format("Falha no serviço dnaPrintJobs do servidor {0}.", Environment.MachineName));
            }

            timer.Enabled = true;
        }

        public int TotalJobs()
        {
            PrintServer pServer = new LocalPrintServer();
            PrintQueueCollection listaImpressoras = pServer.GetPrintQueues();
            int qtdJobs = 0;
            foreach (PrintQueue print in listaImpressoras)
            {
                qtdJobs += print.NumberOfJobs;
            }
            return qtdJobs;
        }

        public bool StatusServico(string ServiceName)
        {
            bool result = false;

            ServiceController sc;
            ServiceControllerStatus status;
            try
            {
                sc = new ServiceController(ServiceName);
                status = sc.Status;
            }
            catch
            {
                status = ServiceControllerStatus.Stopped;
            }

            if (status == ServiceControllerStatus.Running)
            {
                result = true;
            }
            else
            {
                filelog.Escrever(Log.TipoLogs.erro, "Serviço dnaprintJobs encontra-se parado.");
            }

            return result;
        }

        public void ReiniciaServico(string ServiceName)
        {
            ServiceController sc = new ServiceController();
            ServiceControllerStatus status;
            try
            {
                sc = new ServiceController(ServiceName);
                status = sc.Status;
            }
            catch
            {
                status = ServiceControllerStatus.Stopped;
            }

            if (status != ServiceControllerStatus.Running)
            {
                filelog.Escrever(Log.TipoLogs.info, "Serviço dnaprintJobs está sendo reiniciado.");
                try
                {
                    sc.Stop();
                    sc.Start();
                }
                catch
                {

                }
                
            }
        }
    }
}
