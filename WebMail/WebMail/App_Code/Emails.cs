using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

public partial class Emails
{
    [Key]
    public int idEmail { get; set; }

    public string emailPara { get; set; }

    public string emailCopia { get; set; }

    public string emailCopiaOculta { get; set; }

    public string titulo { get; set; }

    public string mensagem { get; set; }

    public bool? html { get; set; }

    public Emails()
    {

    }

    public Emails(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html)
    {
        this.emailPara = emailPara;
        this.emailCopia = emailCopia;
        this.emailCopiaOculta = emailCopiaOculta;
        this.titulo = titulo;
        this.mensagem = mensagem;
        this.html = html;
    }
}