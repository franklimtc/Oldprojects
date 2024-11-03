using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Postagens
{
    public class SMTP
    {
        public static void Enviar(string Assunto, string Mensagem, string Email_Para)
        {
            //define as configurações do servidor para envio de mensagens
            string ServidorSMTP = "smtp.gmail.com";
            int PortaSMTP = 587;
            string Email_De = "impressora.csf@gmail.com";
            string Senha = "Senh@123";
            bool ssl = true;

            SmtpClient smtp = new SmtpClient(ServidorSMTP, PortaSMTP);
            smtp.EnableSsl = ssl;
            NetworkCredential cred = new NetworkCredential(Email_De, Senha);
            smtp.Credentials = cred;



            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress(Email_De, "Correios");
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;

            //envia a mensagem
            smtp.Send(mail);
        }
    }
}
