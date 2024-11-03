using System;
using System.ServiceProcess;
using System.Timers;


namespace Service
{
    public partial class dnaPrintService : ServiceBase
    {
        Timer timerMoveSprite;
        public dnaPrintService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timerMoveSprite = new Timer();
            timerMoveSprite.Interval = new TimeSpan(0, 1, 0).TotalMilliseconds;
            timerMoveSprite.Elapsed += new ElapsedEventHandler(ExecutaAgente);
            timerMoveSprite.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        public void ExecutaAgente(object source, ElapsedEventArgs e)
        {
            timerMoveSprite.Enabled = false;
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 20)
            {
                timerMoveSprite.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;
                //dnaPrint.AgenteB.Execucao.Iniciar();
                dnaPrint.Execucao.Iniciar();
            }
            timerMoveSprite.Enabled = true;
        }
    }
}
