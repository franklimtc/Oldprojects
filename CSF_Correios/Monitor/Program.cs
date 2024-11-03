using CSF_Correios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Monitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    //Application.Run(new Preventivos());
        //    Application.Run(new Form1());
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MonitordePostagens()
            };
            ServiceBase.Run(ServicesToRun);
        }
        //static void Main()
        //{
        //    List<reqSuprimento> listaSuprimentos = reqSuprimento.Listar();
        //    foreach (reqSuprimento req in listaSuprimentos)
        //    {
        //        Postagens.logPostgem log = new Postagens.logPostgem(req.Postagem);

        //        if (log.Ok)
        //        {
        //            int prazo = 0;
        //            if (!int.TryParse(req.PrazoEntrega, out prazo))
        //            {
        //                CSF_Correios.Eventos.servicoCorreios tpServico;
        //                if (log.TpEnvio != null)
        //                {
        //                    if (log.TpEnvio.Contains("PAC") && !log.TpEnvio.Contains("REVERSO"))
        //                    {
        //                        tpServico = Eventos.servicoCorreios.pac;
        //                    }
        //                    else if (log.TpEnvio.Contains("SEDEX") && !log.TpEnvio.Contains("REVERSO"))
        //                    {
        //                        tpServico = Eventos.servicoCorreios.sedex;
        //                    }
        //                    else
        //                    {
        //                        tpServico = Eventos.servicoCorreios.pac;
        //                    }


        //                    if (log.CepDestino != null)
        //                    {
        //                        int prazo1 = CSF_Correios.Eventos.CalculaPrazo(tpServico, "65175175", log.CepDestino);
        //                        EntregaSuprimentos entrega = new EntregaSuprimentos(req.Serie, req.Postagem, log.CepDestino, tpServico.ToString());
        //                        entrega.AtualizarPrazo(prazo1);
        //                    }
        //                }
        //            }

        //            if (log.Status != req.Status)
        //            {
        //                if (log.Status.Contains("Objeto entregue ao destinatário") || log.Status.Contains("Entrega Efetuada"))
        //                {
        //                    req.AtualizarStatus(log.Status, log.Data);
        //                }
        //                req.AtualizarStatus(log.Status);

        //            }
        //        }
        //    }

        //    List<reqPeca> listaPecas = reqPeca.listar();

        //    foreach (reqPeca req in listaPecas)
        //    {
        //        Postagens.logPostgem log = new Postagens.logPostgem(req.Postagem);

        //        if (log.Ok)
        //        {
        //            {
        //                if (log.Status != req.StatusEntrega)
        //                {
        //                    if (log.Status.Contains("Objeto entregue ao destinatário") || log.Status.Contains("Entrega Efetuada"))
        //                    {
        //                        req.Entregue(log.Data);
        //                    }
        //                    req.AtualizarStatus(log.Status);
        //                }
        //            }
        //        }

        //    }
        //}
    }
}
