using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for Email
/// </summary>
namespace WebEmailMonitor
{
    public class Email
    {
        #region Campos
        private string _idEmail;
        private string _para;
        private string _copia;
        private string _copiaOculta;
        private string _titulo;
        private string _mensagem;
        private bool _html;

        public string Para
        {
            get
            {
                return _para;
            }

            set
            {
                _para = value;
            }
        }

        public string Copia
        {
            get
            {
                return _copia;
            }

            set
            {
                _copia = value;
            }
        }

        public string CopiaOculta
        {
            get
            {
                return _copiaOculta;
            }

            set
            {
                _copiaOculta = value;
            }
        }

        public string Titulo
        {
            get
            {
                return _titulo;
            }

            set
            {
                _titulo = value;
            }
        }

        public string Mensagem
        {
            get
            {
                return _mensagem;
            }

            set
            {
                _mensagem = value;
            }
        }

        public bool Html
        {
            get
            {
                return _html;
            }

            set
            {
                _html = value;
            }
        }

        public string IdEmail
        {
            get
            {
                return _idEmail;
            }

            set
            {
                _idEmail = value;
            }
        }
        #endregion

        public Email()
        {

        }

        public Email(string para, string copia, string copiaOculta, string titulo, string mensagem, bool html)
        {
            this.Para = para;
            this.Copia = copia;
            this.CopiaOculta = copiaOculta;
            this.Titulo = titulo;
            this.Mensagem = mensagem;
            this.Html = html;
        }

        public void EnviaMensagemEmail()
        {
            if (ValidaEnderecoEmail(this.Para))
            {
                string ServidorSMTP = "smtp.gmail.com";
                int PortaSMTP = 587;
                string Email_De = "atendimentocsfdigital@gmail.com";
                string Senha = "Senh@123";
                bool ssl = true;

                //string ServidorSMTP = "email-ssl.com.br";
                //int PortaSMTP = 465;
                //string Email_De = "atendimento@csfdigital.com.br";
                //string Senha = "Senh@123";

                SmtpClient smtp = new SmtpClient(ServidorSMTP, PortaSMTP);
                smtp.EnableSsl = ssl;
                NetworkCredential cred = new NetworkCredential(Email_De, Senha);
                smtp.Credentials = cred;

                //cria uma mensagem
                MailMessage mail = new MailMessage();

                //define os endereços
                mail.From = new MailAddress(Email_De, "CSF Digital");
                mail.ReplyToList.Add("sac@csfdigital.com.br");
                mail.To.Add(this.Para);

                //define o conteúdo
                mail.Subject = this.Titulo;
                mail.Body = Mensagem;
                mail.IsBodyHtml = this.Html;

                try
                {
                    //envia a mensagem
                    smtp.Send(mail);
                    this.Enviado();
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                }
            }
            else
            {
                this.Enviado();
            }
           
        }

        public static void EnviaMensagemEmail(Email email)
        {
            if (ValidaEnderecoEmail(email.Para))
            {
                string ServidorSMTP = "smtp.gmail.com";
                int PortaSMTP = 587;
                string Email_De = "atendimentocsfdigital@gmail.com";
                string Senha = "Senh@123";
                bool ssl = true;

                //string ServidorSMTP = "email-ssl.com.br";
                //int PortaSMTP = 465;
                //string Email_De = "atendimento@csfdigital.com.br";
                //string Senha = "Senh@123";

                SmtpClient smtp = new SmtpClient(ServidorSMTP, PortaSMTP);
                smtp.EnableSsl = ssl;
                NetworkCredential cred = new NetworkCredential(Email_De, Senha);
                smtp.Credentials = cred;

                //cria uma mensagem
                MailMessage mail = new MailMessage();

                //define os endereços
                mail.From = new MailAddress(Email_De, "CSF Digital");
                mail.ReplyToList.Add("sac@csfdigital.com.br");
                mail.To.Add(email.Para);

                //define o conteúdo
                mail.Subject = email.Titulo;
                mail.Body = email.Mensagem;
                mail.IsBodyHtml = email.Html;

                try
                {
                    //envia a mensagem
                    smtp.Send(mail);
                    email.Enviado();
                }
                catch (Exception ex)
                {
                    string erro = ex.InnerException.ToString();
                }
            }
            else
            {
                email.Enviado();
            }
            
        }

        private void Enviado()
        {
            System.Data.SqlClient.SqlCommand comand = new System.Data.SqlClient.SqlCommand();
            comand.CommandText = string.Format("Update Emails set emailEnviado = 1 where idEmail = {0};", this.IdEmail);
            Dao.SQLExecuteNonQuery(comand);
        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Email> ListarEmailsPendentes()
        {
            List<Email> lista = new List<Email>();

            var dtEmails = Dao.SQLRetornaDT("select * from Emails where emailEnviado = 0 or emailEnviado is null;");

            foreach (DataRow email in dtEmails.Rows)
            {
                bool html = false;
                if ((bool)email["html"])
                    html = true;
                Email e = new Email(email["emailPara"].ToString(), email["emailCopia"].ToString(), email["emailCopiaOculta"].ToString(), email["titulo"].ToString(), email["mensagem"].ToString(), html);
                e.IdEmail = email["idEmail"].ToString();
                lista.Add(e);
            }

            return lista;
        }
    }
}