using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

public partial class LogsErros
{
    [Key]
    public int id { get; set; }

    public string sistema { get; set; }

    public string erro { get; set; }

    public LogsErros() { }

    public LogsErros(string Sistema, string erro)
    {
        this.sistema = Sistema;
        this.erro = erro;
    }
}

