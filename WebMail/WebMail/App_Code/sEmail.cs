using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "sEmail" in code, svc and config file together.
public class sEmail : IsEmail
{
    #region Campos

    private string _idEmail;
    private string _emailPara;
    private string _emailCopia;
    private string _emailCopiaOculta;
    private string _titulo;
    private string _mensagem;
    private bool _html;
    private string _emailEnviado;

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

    public string EmailPara
    {
        get
        {
            return _emailPara;
        }

        set
        {
            _emailPara = value;
        }
    }

    public string EmailCopia
    {
        get
        {
            return _emailCopia;
        }

        set
        {
            _emailCopia = value;
        }
    }

    public string EmailCopiaOculta
    {
        get
        {
            return _emailCopiaOculta;
        }

        set
        {
            _emailCopiaOculta = value;
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

    public string EmailEnviado
    {
        get
        {
            return _emailEnviado;
        }

        set
        {
            _emailEnviado = value;
        }
    }

    #endregion

    public bool Enviar(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html)
    {
        bool result = false;
        #region Antigo

        //sEmail email = new sEmail();
        //email.EmailPara = emailPara;
        //if(emailCopia != null)
        //    email.EmailCopia = emailCopia;
        //if(emailCopiaOculta != null)
        //    email.EmailCopiaOculta = emailCopiaOculta;
        //email.Titulo = titulo;
        //email.Mensagem = mensagem;
        //email.Html = html;

        //System.Data.SqlClient.SqlCommand comand = new System.Data.SqlClient.SqlCommand();
        //comand.CommandText = @"INSERT INTO Emails(emailPara, emailCopia, emailCopiaOculta, titulo, mensagem, html) 
        //                        VALUES(@emailPara, @emailCopia, @emailCopiaOculta, @titulo, @mensagem, @html);";

        //comand.Parameters.AddWithValue("@emailPara", emailPara);
        //comand.Parameters.AddWithValue("@emailCopia", emailCopia);
        //comand.Parameters.AddWithValue("@emailCopiaOculta", emailCopiaOculta);
        //comand.Parameters.AddWithValue("@titulo", titulo);
        //comand.Parameters.AddWithValue("@mensagem", mensagem);
        //comand.Parameters.AddWithValue("@html", html);

        //if (DAO.SQLExecuteNonQuery(comand) > 0)
        //    result = true;
        #endregion

        Model cbx = new Model();
        try
        {
            cbx.Emails.Add(new Emails(emailPara, emailCopia, emailCopiaOculta, titulo, mensagem, html));
            cbx.SaveChanges();
            result = true;
        }
        catch(Exception ex)
        {
            cbx.LogsErros.Add(new LogsErros("Alertas de Emails", ex.Message));
        }

        return result;
    }

    public IList<sEmail> Listar()
    {
        //List<sEmail> listaEmails = new List<sEmail>();

        #region Antigo

        List<sEmail> listaEmails = new List<sEmail>();
        DataTable dtEmails = DAO.SQLRetornaDT("Select * from Emails");
        foreach (DataRow email in dtEmails.Rows)
        {
            sEmail e = new sEmail();
            e.EmailPara = email["emailPara"].ToString();
            e.EmailCopia = email["emailCopia"].ToString();
            e.EmailCopiaOculta = email["emailCopiaOculta"].ToString();
            e.Titulo = email["titulo"].ToString();
            e.Mensagem = email["mensagem"].ToString();
            e.Html = bool.Parse(email["html"].ToString());
            e.EmailEnviado = email["emailEnviado"].ToString();
            listaEmails.Add(e);
        }
        return listaEmails;

        #endregion
    }

    public string EmailUsuario(string SerieEquipamento)
    {
        string email = null;
        string query = string.Format("select email from equipamentos where serie = '{0}';", SerieEquipamento);
        try
        {
            email = DAO.SQLExecute(query).ToString();
            if(email == null || email =="")
                email = "franklim@csfdigital.com.br";
        }
        catch
        {
            email = "franklim@csfdigital.com.br";
        }

        return email;
    }
}
