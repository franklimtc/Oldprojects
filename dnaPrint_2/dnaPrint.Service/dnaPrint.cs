using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;


namespace dnaPrint.Service
{
    partial class dnaPrint : ServiceBase
    {
        System.Timers.Timer timerSnmp;
        System.Timers.Timer timerJobs;

        public dnaPrint()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timerSnmp = new System.Timers.Timer();
            timerSnmp.Interval = new TimeSpan(0, 0, 30).TotalMilliseconds;
            timerSnmp.Elapsed += new ElapsedEventHandler(DisparoSNMP);
            timerSnmp.Enabled = true;

            timerJobs = new System.Timers.Timer();
            timerSnmp.Interval = new TimeSpan(0, 0, 30).TotalMilliseconds;
            timerSnmp.Elapsed += new ElapsedEventHandler(ColetarJobs);
            timerSnmp.Enabled = true;
        }

        private void ColetarJobs(object sender, ElapsedEventArgs e)
        {
            timerSnmp.Interval = new TimeSpan(0, 1, 0).TotalMilliseconds;
            PrinterJob.ColetarJobs(Directory.GetCurrentDirectory(), DateTime.Now);
        }

        protected override void OnStop()
        {
            // TODO: Adicione aqui o código para realizar qualquer desmontagem necessária para interromper seu serviço.
        }

        public void DisparoSNMP(object source, ElapsedEventArgs e)
        {
            Operacoes.EfetuarLeitura();
            timerSnmp.Interval = new TimeSpan(0, 30, 0).TotalMilliseconds;
        }
    }
}
