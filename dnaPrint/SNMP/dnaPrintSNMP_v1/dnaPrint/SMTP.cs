using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

namespace dnaPrint
{
    class SMTP
    {
        static string dirAtual = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string config = dirAtual + @"\Config\Config.xml";

        public static void PreparaEmail()
        {
            string dirDados = dirAtual + @"\Dados";

            Cripto c = new Cripto();
            string de = DAO.RetornaAtributoXml(config, "Usuario");
            string senha = c.Decriptar(DAO.RetornaAtributoXml(config, "Senha"));
            bool ssl = bool.Parse(DAO.RetornaAtributoXml(config, "SSL"));
            string para = DAO.RetornaAtributoXml(config, "Email");
            string servidor = DAO.RetornaAtributoXml(config, "Servidor");
            int porta = int.Parse(DAO.RetornaAtributoXml(config, "Porta"));
            string endereco = DAO.RetornaAtributoXml(config, "Endereco");
            string cliente = DAO.RetornaAtributoXml(config, "Cliente");

            string Assunto = "Contadores Diários" + cliente + " localizado no endereço: " + endereco;
            string Mensagem = "Contadores do cliente " + cliente + " localizado no endereço: " + endereco + " Computador: " + System.Environment.MachineName
                + ".\ndnaPrint SNMP, "
                + DateTime.Now + "." + "\nVersão: " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();

            bool status = false;


            DirectoryInfo di = new DirectoryInfo(dirDados);
            FileInfo[] lista = di.GetFiles();
            List<string> anexos = new List<string>();
            List<string> arquivos = new List<string>();

            foreach (FileInfo f in lista)
            {
                if (!f.Name.Contains("Enviado"))
                {
                    anexos.Add(f.FullName);
                    arquivos.Add(f.Name);
                }
            }

            //lista = null;

            if (anexos.Count > 0)
            {
                try
                {
                    status = SMTP.Enviar(servidor, porta, de, senha, para, Assunto, Mensagem, ssl, anexos);
                }
                catch
                {
                    throw new SmtpException();
                }
            }
            if (status)
            {
                //foreach (string arquivo in arquivos)
                //{
                //    // Testar se o arquivo ainda estará em uso.
                //    //File.Move(arquivo, arquivo.Replace(".csv", "_Enviado.csv"));
                //    DAO.GerarTXT(dirDados + @"\Arquivos Enviados.txt", arquivo);
                //}

                foreach (FileInfo f in lista)
                {
                    if (!f.Name.Contains("Enviado"))
                    {
                        f.MoveTo(f.FullName.Replace(".csv", "_Enviado.csv"));
                    }
                }

            }
            else
            {
                Logs.GerarLogs(Logs.TipoLogs.email, "Falha ao enviar email.");
            }

        }

        public static void Enviar(string ServidorSMTP, int PortaSMTP, string Email_De, string Senha, string Email_Para, string Assunto, string Mensagem, bool ssl)
        {

            //define as configurações do servidor para envio de mensagens
            SmtpClient smtp = new SmtpClient(ServidorSMTP, PortaSMTP);
            smtp.EnableSsl = ssl;
            NetworkCredential cred = new NetworkCredential(Email_De, Senha);
            smtp.Credentials = cred;

            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress(Email_De);
            mail.To.Add(Email_Para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;

            //envia a mensagem
            smtp.Send(mail);
        }

        public static bool Enviar(string ServidorSMTP, int PortaSMTP, string Email_De, string Senha, string Email_Para, string Assunto, string Mensagem, bool ssl, List<string> Anexos)
        {
            bool status;

            //define as configurações do servidor para envio de mensagens
            SmtpClient smtp = new SmtpClient(ServidorSMTP, PortaSMTP);
            smtp.EnableSsl = ssl;
            NetworkCredential cred = new NetworkCredential(Email_De, Senha);
            smtp.Credentials = cred;

            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress(Email_De);
            mail.To.Add(Email_Para);

            //adiciona anexos ao email
            foreach (string file in Anexos)
            {
                if (System.IO.File.Exists(file))
                {
                    Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(data);
                }
            }

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem;

            //envia a mensagem
            try
            {
                smtp.Send(mail);
                //mail.Attachments.Dispose();
                status = true;
            }
            catch
            {
                status = false;
                //mail.Attachments.Dispose();
            }
            finally
            {
                mail.Attachments.Dispose();
            }
            return status;
        }

        public static void Enviar(string Assunto, string Mensagem)
        {

            Cripto c = new Cripto();
            string de = DAO.RetornaAtributoXml(config, "Usuario");
            string senha = c.Decriptar(DAO.RetornaAtributoXml(config, "Senha"));
            bool ssl = bool.Parse(DAO.RetornaAtributoXml(config, "SSL"));
            string para = DAO.RetornaAtributoXml(config, "Email");
            string servidor = DAO.RetornaAtributoXml(config, "Servidor");
            int porta = int.Parse(DAO.RetornaAtributoXml(config, "Porta"));
            string endereco = DAO.RetornaAtributoXml(config, "Endereco");
            string cliente = DAO.RetornaAtributoXml(config, "Cliente");

            //string Assunto = "Contadores Diários";
            //string Mensagem = "Contadores do cliente " + cliente + " localizado no endereço: " + endereco + ".\nEasy Account, " + DateTime.Now + ".";
            //bool status = false;

            //define as configurações do servidor para envio de mensagens
            SmtpClient smtp = new SmtpClient(servidor, porta);
            smtp.EnableSsl = ssl;
            NetworkCredential cred = new NetworkCredential(de, senha);
            smtp.Credentials = cred;

            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress(de);
            mail.To.Add(para);

            //define o conteúdo
            mail.Subject = Assunto;
            mail.Body = Mensagem + "\n" + endereco;

            //envia a mensagem
            try
            {
                smtp.Send(mail);
            }
            catch
            {

            }
        }
    }
}
