using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for Oids
/// </summary>
public class Oids
{
    #region campos
    private string _idPerfil;

    public string IdPerfil
    {
        get { return _idPerfil; }
        set { _idPerfil = value; }
    }
    private string _Fabricante;

    public string Fabricante
    {
        get { return _Fabricante; }
        set { _Fabricante = value; }
    }
    private string _modelo;

    public string Modelo
    {
        get { return _modelo; }
        set { _modelo = value; }
    }
    private string _Firmware;

    public string Firmware
    {
        get { return _Firmware; }
        set { _Firmware = value; }
    }
    private string _oid;

    public string Oid
    {
        get { return _oid; }
        set { _oid = value; }
    }
    private string _propriedade;

    public string Propriedade
    {
        get { return _propriedade; }
        set { _propriedade = value; }
    }
    #endregion
    public static List<Oids> Listar()
    {
        List<Oids> listaOids = new List<Oids>();

        DataTable dtOids = DAO.retornadt(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), "select * from ListaOids");

        if (dtOids.Rows.Count > 0)
        {
            foreach (DataRow oid in dtOids.Rows)
            {
                Oids o = new Oids();
                o.IdPerfil = oid["idPerfil"].ToString();
                o.Fabricante = oid["Fabricante"].ToString();
                o.Modelo = oid["modelo"].ToString();
                o.Firmware = oid["Firmware"].ToString();
                o.Oid = oid["oid"].ToString();
                o.Propriedade = oid["propriedade"].ToString();
                listaOids.Add(o);
            }
        }
        return listaOids;
    }
    public static List<Oids> ListarParcial(string fabricante, string modelo, string firmware)
    {
        List<Oids> listaOids = new List<Oids>();

        DataTable dtOids = DAO.retornadt(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), string.Format("select * from listaoids where fabricante = '{0}' and modelo = '{1}' and firmware = '{2}'", fabricante, modelo, firmware));

        if (dtOids.Rows.Count > 0)
        {
            foreach (DataRow oid in dtOids.Rows)
            {
                Oids o = new Oids();
                o.IdPerfil = oid["idPerfil"].ToString();
                o.Fabricante = oid["Fabricante"].ToString();
                o.Modelo = oid["modelo"].ToString();
                o.Firmware = oid["Firmware"].ToString();
                o.Oid = oid["oid"].ToString();
                o.Propriedade = oid["propriedade"].ToString();
                listaOids.Add(o);
            }
        }
        return listaOids;
    }
}
