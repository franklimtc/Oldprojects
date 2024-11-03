using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Ocomon
{
    /// <summary>
    /// Summary description for Email
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Email : System.Web.Services.WebService
    {

        [WebMethod]
        public void Enviar(string para, string assunto, string mensagem, string sistema)
        {
            SMTP.Enviar(para, assunto, mensagem, sistema);
        }

        [WebMethod]
        public void EnviarHtmlMessageCopia(string para, string copia, string assunto, string mensagem, string sistema, bool html)
        {
            SMTP.Enviar(para, copia, assunto, mensagem, sistema, html);
        }

        [WebMethod]
        public void EnviarHtmlMessage(string para, string assunto, string mensagem, string sistema, bool html)
        {
            SMTP.Enviar(para, assunto, mensagem, sistema, html);
        }
        [WebMethod]
        public void EnviarHtmlMessageTeste(string para, string assunto, string mensagem, string sistema, bool html)
        {
            SMTP.EnviarTeste(para, assunto, mensagem, sistema, html);
        }

        [WebMethod]
        public void EnviarAlertaTrocaSuprimento(string Email, string[] Dados, bool TrocaOK)
        {
            string Mensagem = null;
            if (TrocaOK)
            {
                Mensagem = GerarMensagemOk(Dados);
            }
            else
            {
                Mensagem = GerarMensagemTrocaAntecipdada(Dados);
            }
            SMTP.EnviarAlertaTroca(Email, "Alerta de troca de suprimento", Mensagem, "Gestão de Suprimentos", TrocaOK);
        }

        private string GerarMensagemTrocaAntecipdada(string[] dados)
        {
            string htmlhead = @"<head><img src='Comunicado_Negativo.jpg'>
            <style>
            table {
                font-family: arial, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }

            td, th {
                border: 1px solid #dddddd;
                text-align: left;
                padding: 8px;
            }

            tr:nth-child(even) {
                background-color: #dddddd;
            }
            </style>
            </head>";
            string inicioBody = @"<p>Prezado Cliente</p>
            <p>Através do monitoramento, identificamos a(s) troca(s) de toner do(s) equipamento(s) abaixo de forma antecipada (toner retirado antes do término). 
Pedimos que volte o toner ao equipamento e utilize até o fim.</p>";
            string htmlTable = GerarTabela(dados);
            string fimBody = @"<p>Pedimos que caso haja alguma falha na impressão, agite o cartucho e reinstale no equipamento para utilização de 100% dos insumos fornecidos. 
            Em caso de dúvidas, favor enviar e-mail para sac@csfdigital.com.br. </p>
            <br>
            <p>
            Atenciosamente,
            Gestão de consumíveis.
            CSF Digital
            </p>
            <img src='Comunicado_Rodape.jpg'> ";

            return string.Format(@"<html>
            {0} {1} {2} {3}
            </html>", htmlhead, inicioBody, htmlTable, fimBody);
            
        }

        private string GerarMensagemOk(string[] dados)
        {
            string htmlhead = @"<head><img src='Comunicado_Positivo.jpg'>
            <style>
            table {
                font-family: arial, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }

            td, th {
                border: 1px solid #dddddd;
                text-align: left;
                padding: 8px;
            }

            tr:nth-child(even) {
                background-color: #dddddd;
            }
            </style>
            </head>";
            string inicioBody = @"<p>Prezado Cliente</p>
            <p>Através do monitoramento, identificamos a(s) troca(s) de toner do(s) equipamento(s) abaixo. Para esse(s) equipamento(s) enviaremos um novo toner preventivamente.</p>";
            string htmlTable = GerarTabela(dados);
            string fimBody = @"<p>Em caso de dúvidas, favor enviar e-mail para sac@csfdigital.com.br.</p> 
            <br>
            <p>
            Atenciosamente,
            Gestão de consumíveis.
            CSF Digital
            </p>
            <img src='Comunicado_Rodape.jpg'> ";

            return string.Format(@"<html>
            {0} {1} {2} {3}
            </html>", htmlhead, inicioBody, htmlTable, fimBody);
        }

        private string GerarTabela(string[] dados)
        {
            #region Gerar Tabela
            string html = "<table>";
            for (int i = 0; i < dados.Length; i++)
            {
                html += "<tr>";
                if (i == 0)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        html += string.Format("<th>{0}</th>", dados[i][y]);
                    }
                }
                else
                {
                    for (int y = 0; y < 6; y++)
                    {
                        html += string.Format("<td>{0}</td>", dados[i][y]);
                    }
                }
                html += "</tr>";
            }
            html += "</table>";

            #endregion

            return html;
        }
    }
}
