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

namespace dnaPrint.Jobs
{
    public partial class dnaPrintJobs : ServiceBase
    {
        System.Timers.Timer timerJobs;

        public dnaPrintJobs()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
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
        }
    }
}
