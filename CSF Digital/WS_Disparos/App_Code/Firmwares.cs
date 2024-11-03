using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for Firmwares
/// </summary>
public class Firmwares
{
    private string _idFirmware;

    public string IdFirmware
    {
        get { return _idFirmware; }
        set { _idFirmware = value; }
    }
    private string _firmwares;

    public string Firmwares1
    {
        get { return _firmwares; }
        set { _firmwares = value; }
    }

    public static List<Firmwares> Listar(string fabricante, string modelo)
    {
        List<Firmwares> listaFirmwares = new List<Firmwares>();

        DataTable dtFirmwares = DAO.retornadt(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), string.Format("select distinct idPerfil, firmware  from cadastroperfiloid where fabricante = '{0}' and modelo = '{1}'", fabricante, modelo));
        if (dtFirmwares.Rows.Count > 0)
        {
            foreach (DataRow p in dtFirmwares.Rows)
            {
                Firmwares firm = new Firmwares();
                firm.IdFirmware = p["idPerfil"].ToString();
                firm.Firmwares1 = p["firmware"].ToString();
                listaFirmwares.Add(firm);
            }
        }
        return listaFirmwares;
    }
}