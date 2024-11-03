using System.Net.Mail;

namespace RastreioPostagensCorreios
{
    public class Email
    {
        public static async void Enviar(string assunto, string mensagem, string destinatario, bool html = false, string file = null)
        {
            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress("suporte.csfdigital@gmail.com", "CSF Digital");
            mail.To.Add(destinatario);
            mail.CC.Add("sac@csfdigital.com.br");
            if (file != null)
            {
                mail.Attachments.Add(new Attachment(file));
            }

            //define o conteúdo
            mail.Subject = assunto;
            mail.Body = mensagem;
            mail.IsBodyHtml = html;
            mail.ReplyToList.Add("sac@csfdigital.com.br");
            //envia a mensagem
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("suporte.csfdigital@gmail.com", "Senh@123");
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (System.Exception ex)
            {
                EnviarErro($"ERRO AO ENVIAR - {assunto}", $"Não foi possível enviar email para o endereço {destinatario}! Verifique o cadastro do equipamento! Mensagem: {ex.Message}");
            }
        }

        public static async void EnviarErro(string assunto, string mensagem, string destinatario = "sac@csfdigital.com.br")
        {
            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress("suporte.csfdigital@gmail.com", "CSF Digital - Erro");
            mail.To.Add(destinatario);

            if (destinatario != "sac@csfdigital.com.br")
                mail.CC.Add("sac@csfdigital.com.br");

            //define o conteúdo
            mail.Subject = assunto;
            mail.Body = mensagem;
            mail.ReplyToList.Add("sac@csfdigital.com.br");
            //envia a mensagem
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("suporte.csfdigital@gmail.com", "Senh@123");
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch
            {

            }
        }
    }
}
