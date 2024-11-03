using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Descrição resumida de EmailBD
/// </summary>
public class EmailBD
{
    #region Campos
    private string _emailPara;
    private string _emailCopia;
    private string _emailCopiaOculta;
    private string _titulo;
    private string _mensagem;
    private string _html;

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
            if (!string.IsNullOrEmpty(_emailCopia))
                return _emailCopia;
            else
                return "";
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
            if (!string.IsNullOrEmpty(_emailCopiaOculta))
                return _emailCopiaOculta;
            else
                return "";
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

    public string Html
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

    #endregion

    public EmailBD(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, string html)
    {
        this.EmailPara = emailPara;
        this.EmailCopia = emailCopia;
        this.EmailCopiaOculta = emailCopiaOculta;
        this.Titulo = titulo;
        this.Mensagem = mensagem;
        this.Html = html;
    }

    public void Adicionar()
    {
        SqlCommand comand = new SqlCommand(@"INSERT INTO Emails (emailPara, emailCopia, emailCopiaOculta, titulo,mensagem ,html) VALUES(@emailPara, @emailCopia, @emailCopiaOculta, @titulo,@mensagem ,@html);");
        comand.Parameters.AddWithValue("@emailPara", this.EmailPara);
        comand.Parameters.AddWithValue("@emailCopia", this.EmailCopia);
        comand.Parameters.AddWithValue("@emailCopiaOculta", this.EmailCopiaOculta);
        comand.Parameters.AddWithValue("@titulo", this.Titulo);
        comand.Parameters.AddWithValue("@mensagem", this.Mensagem);
        comand.Parameters.AddWithValue("@html", this.Html);

        DAO.ExecuteNonQuery(DAO.connString(), comand);
    }
}