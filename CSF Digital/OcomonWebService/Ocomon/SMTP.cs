using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

namespace Ocomon
{
    class SMTP
    {

        public static void Enviar( string Email_Para, string Assunto, string Mensagem)
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
            mail.From = new MailAddress(Email_De,"OCOMON");
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;

            //envia a mensagem
            smtp.Send(mail);
        }
        public static void Enviar(string Email_Para, string Assunto, string Mensagem, string Sistema)
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
            mail.From = new MailAddress(Email_De, Sistema);
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;

            //envia a mensagem
            smtp.Send(mail);
        }

        public static void Enviar(string Email_Para, string copia, string Assunto, string Mensagem, string Sistema, bool html)
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
            mail.From = new MailAddress(Email_De, Sistema);
            mail.CC.Add(copia);
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;
            mail.IsBodyHtml = html;

            //envia a mensagem
            smtp.Send(mail);
        }

        public static void Enviar(string Email_Para, string Assunto, string Mensagem, string Sistema, bool html)
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
            mail.From = new MailAddress(Email_De, Sistema);
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;
            mail.IsBodyHtml = html;

            //envia a mensagem
            smtp.Send(mail);
        }

        public static void EnviarAlertaTroca(string Email_Para, string Assunto, string Mensagem, string Sistema, bool trocaCorreta)
        {

            //define as configurações do servidor para envio de mensagens
            string cabecalho = @"C:\Users\Franklim\Source\Workspaces\Projetos\CSF Digital\OcomonWebService\Ocomon\Imagens\Comunicado_Negativo.jpg";
            string corpo = @"<html><head><img src='Comunicado_Negativo.jpg'><br><br></head>";
            if (trocaCorreta)
            {
                cabecalho = @"C:\Users\Franklim\Source\Workspaces\Projetos\CSF Digital\OcomonWebService\Ocomon\Imagens\Comunicado_Positivo.jpg";
                corpo = @"<html><head><img src='Comunicado_Positivo.jpg'><br><br></head>";
            }
            string rodape = @"C:\Users\Franklim\Source\Workspaces\Projetos\CSF Digital\OcomonWebService\Ocomon\Imagens\Comunicado_Rodape.jpg";

            corpo +=@"<body> <p>" + Mensagem + @"</p>
                    <img src='Comunicado_Rodape.jpg'> 
                    </body></html>";

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
            mail.From = new MailAddress(Email_De, Sistema);
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = corpo;
            mail.IsBodyHtml = true;

            Attachment attCabecalho = new Attachment(cabecalho);
            Attachment attRodape = new Attachment(rodape);
            mail.Attachments.Add(attCabecalho);
            mail.Attachments.Add(attRodape);

            //envia a mensagem
            smtp.Send(mail);
        }

        public static void EnviarTeste(string Email_Para, string Assunto, string Mensagem, string Sistema, bool html)
        {

            //define as configurações do servidor para envio de mensagens

            string cabecalho = @"C:\Users\Franklim\Source\Workspaces\Projetos\CSF Digital\OcomonWebService\Ocomon\Imagens\Comunicado_Negativo.jpg";
            string rodape = @"C:\Users\Franklim\Source\Workspaces\Projetos\CSF Digital\OcomonWebService\Ocomon\Imagens\Comunicado_Rodape.jpg";

            string corpo = @"<html>
                    <head><img src='Comunicado_Negativo.jpg'><br><br></head>
                    <body>
                    <b>Algum texto do email...</b><br><br>
                    Mais alguma coisa.....<br>
                    <img src='Comunicado_Rodape.jpg'> 
                    </body></html>";

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
            mail.From = new MailAddress(Email_De, Sistema);
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = corpo;
            mail.IsBodyHtml = html;

            Attachment attCabecalho = new Attachment(cabecalho);
            Attachment attRodape = new Attachment(rodape);
            mail.Attachments.Add(attCabecalho);
            mail.Attachments.Add(attRodape);

            //envia a mensagem
            smtp.Send(mail);
        }
    }
}
