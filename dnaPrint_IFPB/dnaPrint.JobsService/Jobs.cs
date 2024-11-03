using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using dnaPrint.Jobs;


namespace dnaPrint.JobsService
{
    public partial class Jobs : ServiceBase
    {
        System.Timers.Timer timerJobs;
        System.Timers.Timer timerKeepFiles;

        public Jobs()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timerJobs = new System.Timers.Timer();
            timerJobs.Interval = new TimeSpan(0, 0, 30).TotalMilliseconds;
            timerJobs.Elapsed += new ElapsedEventHandler(ColetarJobs);
            timerJobs.Enabled = true;

            timerKeepFiles = new System.Timers.Timer();
            timerKeepFiles.Interval = new TimeSpan(0, 0, 30).TotalMilliseconds;
            timerKeepFiles.Elapsed += new ElapsedEventHandler(KeepFiles);
            timerKeepFiles.Enabled = true;

        }

        private void KeepFiles(object sender, ElapsedEventArgs e)
        {
            timerKeepFiles.Enabled = false;

            dnaPrint.Jobs.Program.ManterArquivosImpressos();

            timerKeepFiles.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;
            timerKeepFiles.Enabled = true;

        }

        private void ColetarJobs(object sender, ElapsedEventArgs e)
        {
            timerJobs.Enabled = false;

            dnaPrint.Jobs.Program.GetJobs2();

            timerJobs.Interval = new TimeSpan(0, 1, 0).TotalMilliseconds;
            timerJobs.Enabled = true;

        }

        protected override void OnStop()
        {
        }
    }
}
