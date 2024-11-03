using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Monitor_Service_Correios
{
    public partial class Monitor : ServiceBase
    {
        Timer timerMoveSprite;

        public Monitor()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timerMoveSprite = new Timer();
            timerMoveSprite.Interval = new TimeSpan(0, 1, 0).TotalMilliseconds;
            timerMoveSprite.Elapsed += new ElapsedEventHandler(AtualizarPostagens);
            timerMoveSprite.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        protected void AtualizarPostagens(object source, ElapsedEventArgs e)
        {
            List<reqSuprimento> lista = reqSuprimento.Listar();
          
            foreach (reqSuprimento req in lista)
            {
                Postagens.logPostgem log = new Postagens.logPostgem(req.Postagem);

                if (log.Status != null && log.Status != "")
                {
                    req.AtualizarStatus(log.Status);

                    if (log.Status.Contains("Entrega Efetuada") || log.Status.Contains("Objeto entregue ao destinatário"))
                    {
                        req.Status = log.Status;
                        req.Entregue(log.Data);
                    }
                }
            }

            List<reqPeca> listaPecas = reqPeca.listar();
            foreach (reqPeca req in listaPecas)
            {
                //Postagens.logPostgem log = Postagens.html.Rastrear(req.Postagem);
                Postagens.logPostgem log = new Postagens.logPostgem(req.Postagem);

                if (log.Status != null && log.Status != "")
                {
                    req.AtualizarStatus(log.Status);

                    if (log.Status.Contains("Entrega Efetuada") || log.Status.Contains("Objeto entregue ao destinatário"))
                    {
                        //req.InformarUsuario(evCorreios.Descricao);
                        //MessageBox.Show(string.Format("Peça Entregue! ({0})", req.Postagem));
                        req.Entregue(log.Data);
                    }
                }

            }
            timerMoveSprite.Enabled = false;
            timerMoveSprite.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;
            timerMoveSprite.Enabled = true;
        }
    }
}
