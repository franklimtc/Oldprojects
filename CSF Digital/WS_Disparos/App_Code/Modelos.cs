using System.Collections.Generic;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Modelos
/// </summary>
public class Modelos
{
    private string _modelo;

    public string Modelo
    {
        get { return _modelo; }
        set { _modelo = value; }
    }

    public static List<Modelos> Listar()
    {
        List<Modelos> listaModelos = new List<Modelos>();

        DataTable dtModelos = DAO.retornadt(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), "select distinct modelo from cadastroperfiloid");
        if (dtModelos.Rows.Count > 0)
        {
            foreach (DataRow p in dtModelos.Rows)
            {
                Modelos mod = new Modelos();
                mod.Modelo = p["modelo"].ToString();
                listaModelos.Add(mod);
            }
        }
        return listaModelos;
    }
}