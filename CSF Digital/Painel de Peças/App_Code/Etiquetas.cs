using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Etiquetas
/// </summary>
public class Etiquetas
{
    #region Campos
    private string _id;
    private string _suprimento;
    private string _serialSuprimento;
    private string _etiqueta;
    private string _operador;
    private string _data;

    public string Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public string Suprimento
    {
        get
        {
            return _suprimento;
        }

        set
        {
            _suprimento = value;
        }
    }

    public string SerialSuprimento
    {
        get
        {
            return _serialSuprimento;
        }

        set
        {
            _serialSuprimento = value;
        }
    }

    public string Etiqueta
    {
        get
        {
            return _etiqueta;
        }

        set
        {
            _etiqueta = value;
        }
    }

    public string Operador
    {
        get
        {
            return _operador;
        }

        set
        {
            _operador = value;
        }
    }

    public string Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
        }
    }
    #endregion

    public Etiquetas()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public static bool Adicionar(string suprimento, string serialSuprimento, string etiqueta, string operador)
    {
        bool result = false;

        string tsql = string.Format(@"INSERT INTO etiquetasSuprimentos(suprimento, serialSuprimento, etiqueta, operador) 
VALUES('{0}','{1}','{2}','{3}')", suprimento, serialSuprimento, etiqueta, operador);

        result = DAO.ExecuteNonQuery( DAO.connString(),tsql);

        return result;
    }
}