using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace RastreioPostagensCorreios
{
    public partial class Rastrear : ServiceBase
    {
        Timer tRastreio;
        public Rastrear()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            tRastreio = new Timer();
            tRastreio.Interval = new TimeSpan(0, 0, 30).TotalMilliseconds;
            tRastreio.Elapsed += new ElapsedEventHandler(RastrearPostagens);
            tRastreio.Enabled = true;          
        }

        private void RastrearPostagens(object sender, ElapsedEventArgs e)
        {
            tRastreio.Enabled = false;
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 18)
            {
                #region Método
                string connString = ConfigurationManager.ConnectionStrings["dbPecas"].ToString();

                List<Suprimentos> Lista = Suprimentos.Listar(connString);

                foreach (Suprimentos supri in Lista)
                {
                    try
                    {
                        supri.Rastrear();
                    }
                    catch (Exception ex)
                    {
                        switch (ex.Message)
                        {
                            case "As etiquetas informadas estão fora do padrão esperado.":
                                Email.EnviarErro($"ERRO EM ENVIO - {supri.TipoEnvio} - {supri.Postagem} - {supri.Serie}", ex.Message, "logistica.for@csfdigital.com.br");
                                break;
                            case "Objeto não encontrado":
                                Email.EnviarErro($"ERRO EM ENVIO - {supri.TipoEnvio} - {supri.Postagem} - {supri.Serie}", ex.Message, "logistica.for@csfdigital.com.br");
                                break;
                            default:
                                break;

                        }
                    }
                    if (supri.Rastro != null)
                    {
                        if (supri.Rastro.Status.Contains("entregue"))
                        {
                            supri.ConfirmarEntrega(connString);
                            ConfirmarEntregasCorreios confirmarEntrega = new ConfirmarEntregasCorreios(supri.Postagem, supri.Serie, supri.DTEnvio, supri.Rastro.Data, supri.TipoEnvio);
                            confirmarEntrega.Registrar();
                            string assunto = $"{DateTime.Now.ToShortDateString()} - {supri.Postagem} - {supri.Serie} - Confirmar entrega!";

                            try
                            {
                                Email.Enviar(assunto, ConfirmarEntregasCorreios.GerarEmail(supri.Postagem, supri.Rastro.Data.ToShortDateString(), supri.Serie), supri.Email, true);

                            }
                            catch (Exception ex)
                            {

                                Email.EnviarErro($"Erro Monitor Postagens {supri.Postagem}", ex.Message, "franklim@csfdigital.com.br");
                            }

                            Console.WriteLine($"###ENTREGUE### - {DateTime.Now.ToString()} - Objeto de postagem {supri.Postagem} Entregue!");
                        }
                        else
                        {
                            if (supri.StatusEntrega != supri.Rastro.Status)
                            {
                                supri.AtualizarStatus(connString);
                                Console.WriteLine($"???ATUALIZADO??? - {DateTime.Now.ToString()} - Objeto de postagem {supri.Postagem} com status {supri.Rastro.Status}!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Não foi possível rastrear a  postagem {supri.Postagem}!");
                    }

                }

                var ListaCobrancas = ConfirmarEntregasCorreios.ListarEmailsPendentes();

                foreach (var supri in ListaCobrancas)
                {
                    string assunto = $"{DateTime.Now.ToShortDateString()} - {supri.Postagem} - {supri.Serie} - Confirmar entrega!";

                    try
                    {
                        Email.Enviar(assunto, ConfirmarEntregasCorreios.GerarEmail(supri.Postagem, supri.DtEntrega.ToShortDateString(), supri.Serie), supri.Email, true);
                        supri.AtualizarDataEmail();
                    }
                    catch (Exception ex)
                    {

                        Email.EnviarErro($"Erro Monitor Postagens {supri.Postagem}", ex.Message, "franklim@csfdigital.com.br");
                    }
                }
                #endregion
            }

            tRastreio.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;
            tRastreio.Enabled = true;

        }

        protected override void OnStop()
        {
        }
    }
}
