using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for PerfilOID
/// </summary>
public class PerfilOID
{
    #region Campos

    private string _idPerfil;

    public string IdPerfil
    {
        get { return _idPerfil; }
        set { _idPerfil = value; }
    }
    private string _modelo;

    public string Modelo
    {
        get { return _modelo; }
        set { _modelo = value; }
    }
    private string _firmware;

    public string Firmware
    {
        get { return _firmware; }
        set { _firmware = value; }
    }
    private string _data;

    public string Data
    {
        get { return _data; }
        set { _data = value; }
    }
    private string _fabricante;

    public string Fabricante
    {
        get { return _fabricante; }
        set { _fabricante = value; }
    }
    private string _CadastroOidPadrao;

    public string CadastroOidPadrao
    {
        get { return _CadastroOidPadrao; }
        set { _CadastroOidPadrao = value; }
    }
    private string _idClasse;

    public string IdClasse
    {
        get { return _idClasse; }
        set { _idClasse = value; }
    }
    private string _oidpadrao;

    public string Oidpadrao
    {
        get { return _oidpadrao; }
        set { _oidpadrao = value; }
    }
    #endregion

    public static List<PerfilOID> Listar()
    {
        List<PerfilOID> listaPerfis = new List<PerfilOID>();

        DataTable dtOids = DAO.retornadt(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), "select * from CadastroPerfilOid");
        if (dtOids.Rows.Count > 0)
        {
            foreach (DataRow p in dtOids.Rows)
            {
                PerfilOID perfil = new PerfilOID();
                perfil.IdPerfil = p["idPerfil"].ToString();
                perfil.Modelo = p["modelo"].ToString();
                perfil.Firmware = p["firmware"].ToString();
                perfil.Data = p["data"].ToString();
                perfil.Fabricante = p["fabricante"].ToString();
                perfil.CadastroOidPadrao = p["CadastroOidPadrao"].ToString();
                perfil.IdClasse = p["idClasse"].ToString();
                perfil.Oidpadrao = p["oidpadrao"].ToString();
                listaPerfis.Add(perfil);
            }
        }
        return listaPerfis;
    }
}