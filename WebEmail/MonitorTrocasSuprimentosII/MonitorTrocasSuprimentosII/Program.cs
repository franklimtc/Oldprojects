using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MonitorTrocasSuprimentosII.wsEmail;

namespace MonitorTrocasSuprimentosII
{
    class Program
    {
        static void Main(string[] args)
        {
            dbModelTrocas dbx = new dbModelTrocas();
            DbSet<vw_UltimasTrocasSuprimentos> dbTrocas = dbx.vw_UltimasTrocasSuprimentos;
            IsEmailClient client = new IsEmailClient();


            foreach (var troca in dbTrocas)
            {
                string emailUsuario = client.EmailUsuario(troca.serie.Trim());
                //string emailUsuario = "franklim@csfdigital.com.br";
                string emailMessage = null;

                if (troca.suprimentoAnterior > 10)
                {
                    emailMessage = GerarEmailTrocaIndevida(troca);
                    string titulo = string.Format("CSF Digital: Alerta de troca INDEVIDA de {0} do equipamento de série {1}", troca.Suprimento, troca.serie);
                    bool result = client.Enviar(emailUsuario, "sac@csfdigital.com.br", null, titulo, emailMessage, true);
                }
                else
                {
                    emailMessage = GerarEmailTrocaNormal(troca);
                    string titulo = string.Format("CSF Digital: Alerta de troca de {0} do equipamento de série {1}", troca.Suprimento, troca.serie);
                    bool result = client.Enviar(emailUsuario, null, null, titulo, emailMessage, true);

                }
            }

            client.Close();

        }

        private static string GerarEmailTrocaIndevida(vw_UltimasTrocasSuprimentos troca)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title></title>");
            sb.AppendLine("<meta charset=\"UTF-8\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            sb.AppendLine("<link href=\"css/style.css\" rel=\"stylesheet\">");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("Prezado cliente,");
            sb.AppendLine("<br> <br>");
            sb.AppendLine($"Através do monitoramento, identificamos a troca de {troca.Suprimento} do equipamento descrito abaixo de forma antecipada ({troca.Suprimento} retirado antes do término).");
            sb.AppendLine($"<br>Pedimos que retorne o {troca.Suprimento} ao equipamento e utilize até o fim.");
            sb.AppendLine($"<br><br> Série: <b>{troca.serie}</b>");
            sb.AppendLine("<br><br>");
            sb.AppendLine("Caso haja alguma falha na impressão, agite o toner e reinstale no equipamento.");
            sb.AppendLine("<br><br>");
            sb.AppendLine("Em caso de dúvidas, favor enviar e-mail para sac@csfdigital.com.br.");
            sb.AppendLine("<br><br>");
            sb.AppendLine("<br><br>");
            sb.AppendLine("");
            sb.AppendLine("Atenciosamente,");
            sb.AppendLine("<br><b>CSF Digital</b>");
            sb.AppendLine("<br><i>Gestão de Suprimentos</i>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();

        }

        private static string GerarEmailTrocaNormal(vw_UltimasTrocasSuprimentos troca)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"pt-br\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title></title>");
            sb.AppendLine("<meta charset=\"UTF-8\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("Prezado cliente,");
            sb.AppendLine("<br> <br>");
            sb.AppendLine($"Através do monitoramento, identificamos a troca de {troca.Suprimento} do equipamento descrito abaixo.");
            sb.AppendLine($"<br>Para esse equipamento enviaremos um novo {troca.Suprimento} preventivamente.");
            sb.AppendLine($"<br><br> Série: <b>{troca.serie}</b>");
            sb.AppendLine("<br><br>");
            sb.AppendLine("Em caso de dúvidas, favor enviar e-mail para sac@csfdigital.com.br.");
            sb.AppendLine("<br><br>");
            sb.AppendLine("<br><br>");
            sb.AppendLine("");
            sb.AppendLine("Atenciosamente,");
            sb.AppendLine("<br><b>CSF Digital</b>");
            sb.AppendLine("<br><i>Gestão de Suprimentos</i>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();

        }
    }
}
