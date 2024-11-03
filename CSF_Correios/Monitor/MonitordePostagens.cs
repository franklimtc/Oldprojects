using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using CSF_Correios;

namespace Monitor
{
    partial class MonitordePostagens : ServiceBase
    {
        Timer timerMoveSprite;

        public MonitordePostagens()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timerMoveSprite = new Timer();
            timerMoveSprite.Interval = new TimeSpan(0, 5, 0).TotalMilliseconds;
            timerMoveSprite.Elapsed += new ElapsedEventHandler(AtualizarPostagens);
            timerMoveSprite.Enabled = true;
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        protected void AtualizarPostagens(object source, ElapsedEventArgs e)
        {
            List<reqSuprimento> listaSuprimentos = reqSuprimento.Listar();
            foreach (reqSuprimento req in listaSuprimentos)
            {
                Postagens.logPostgem log = new Postagens.logPostgem(req.Postagem);

                if (log.Ok)
                {
                    int prazo = 0;
                    if (!int.TryParse(req.PrazoEntrega, out prazo))
                    {
                        CSF_Correios.Eventos.servicoCorreios tpServico;
                        if (log.TpEnvio != null)
                        {
                            if (log.TpEnvio.Contains("PAC") && !log.TpEnvio.Contains("REVERSO"))
                            {
                                tpServico = Eventos.servicoCorreios.pac;
                            }
                            else if (log.TpEnvio.Contains("SEDEX") && !log.TpEnvio.Contains("REVERSO"))
                            {
                                tpServico = Eventos.servicoCorreios.sedex;
                            }
                            else
                            {
                                tpServico = Eventos.servicoCorreios.pac;
                            }


                            if (log.CepDestino != null)
                            {
                                int prazo1 = CSF_Correios.Eventos.CalculaPrazo(tpServico, "65175175", log.CepDestino);
                                EntregaSuprimentos entrega = new EntregaSuprimentos(req.Serie, req.Postagem, log.CepDestino, tpServico.ToString());
                                entrega.AtualizarPrazo(prazo1);
                            }
                        }
                    }

                    if (log.Status != req.Status)
                    {
                        if (log.Status.Contains("Objeto entregue ao destinatário") || log.Status.Contains("Entrega Efetuada"))
                        {
                            req.AtualizarStatus(log.Status, log.Data);
                        }
                        req.AtualizarStatus(log.Status);

                    }
                }
            }

            List<reqPeca> listaPecas = reqPeca.listar();

            foreach (reqPeca req in listaPecas)
            {
                Postagens.logPostgem log = new Postagens.logPostgem(req.Postagem);

                if (log.Ok)
                {
                    {
                        if (log.Status != req.StatusEntrega)
                        {
                            if (log.Status.Contains("Objeto entregue ao destinatário") || log.Status.Contains("Entrega Efetuada"))
                            {
                                req.Entregue(log.Data);
                            }
                            req.AtualizarStatus(log.Status);
                        }
                    }
                }

            }
        }
    }
}
