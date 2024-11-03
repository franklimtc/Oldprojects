using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Suprimento
/// </summary>
public class Suprimento
{
    public int idSuprimento { get; set; }
    public string NomeSuprimento { get; set; }

    public Suprimento(int _id, string _nome)
    {
        this.idSuprimento = _id;
        this.NomeSuprimento = _nome;
    }
}