using System;
using System.ServiceProcess;
using System.Timers;


namespace dnaPrintSNMP
{
    public partial class dnaPrintService : ServiceBase
    {
        System.Timers.Timer timer;
        //System.Timers.Timer timerDiscovery;
        
        public dnaPrintService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.escrever("Serviço", "Serviço iniciado");
            timer = new System.Timers.Timer();
            timer.Interval = new TimeSpan(0, 1, 0).TotalMilliseconds;
            timer.Elapsed += new ElapsedEventHandler(DisparoSNMP);
            timer.Enabled = true;

            //timerDiscovery = new System.Timers.Timer();
            //timerDiscovery.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;
            //timerDiscovery.Elapsed += new ElapsedEventHandler(descobrir);
            //timerDiscovery.Enabled = true;
        }

        protected override void OnStop()
        {
            log.escrever("Serviço", "Serviço parado.");
        }
        public void DisparoSNMP(object source, ElapsedEventArgs e)
        {
            parametros par = new parametros();
            timer.Enabled = false;
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 20)
            {
                timer.Interval = new TimeSpan(0, int.Parse(parametros.retornaParametro("intervalo")), 0).TotalMilliseconds;
                disparo.iniciar();
            }
            timer.Enabled = true;
        }

        //public void descobrir(object source, ElapsedEventArgs e)
        //{
        //    timerDiscovery.Enabled = false;
        //    timer.Interval = new TimeSpan(6, 0, 0).TotalMilliseconds;
        //    discovery.Program.DescobrirEqptos();
        //    timerDiscovery.Enabled = true;
        //}
    }
}
