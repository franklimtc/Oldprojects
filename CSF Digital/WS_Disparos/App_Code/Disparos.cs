using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Disparos
/// </summary>
[WebService(Namespace = "http://www.csfdigital.com.br/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Disparos : System.Web.Services.WebService {

    public Disparos () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<Oids> RetornaOids()
    {
        List<Oids> listaoids = Oids.Listar();
        return listaoids;
    }

    [WebMethod]
    public List<Oids> RetornaOidsParcial(string fabricante, string modelo, string firmware)
    {
        List<Oids> listaoids = Oids.ListarParcial(fabricante, modelo, firmware);
        return listaoids;
    }

    [WebMethod]
    public List<PerfilOID> RetornaPerfis()
    {
        List<PerfilOID> listaPerfis = PerfilOID.Listar();
        return listaPerfis;
    }

    [WebMethod]
    public void CadastrarDisparo(string disparo)
    {
        if (disparo.Contains("insert into dadosDisparosErros") || disparo.Contains("insert into dadosDisparos"))
        {
            DAO.execute(ConfigurationManager.ConnectionStrings["Disparos"].ToString(), disparo);
        }
    }

    [WebMethod]
    public int RetornaIdEquipamento(string serie)
    {
        string consulta = string.Format("select idEquipamento from CadastroEquipamentos where serie = '{0}'", serie);
        string idEquipamento = DAO.retornaValor(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), consulta);
        if (idEquipamento == null || idEquipamento == "")
        {
            idEquipamento = "0";
        }
        return int.Parse(idEquipamento);
    }

    [WebMethod]
    public List<Modelos> RetornaModelos()
    {
        List<Modelos> listaModelos = Modelos.Listar();
        return listaModelos;
    }

    [WebMethod]
    public List<Firmwares> RetornaFirmwares(string fabricante, string modelo)
    {
        List<Firmwares> listaFirmwares = Firmwares.Listar(fabricante, modelo);
        return listaFirmwares;
    }

    [WebMethod]
    public DateTime retornaData()
    {
        return DateTime.Parse(DAO.retornaValor(ConfigurationManager.ConnectionStrings["dnaprint"].ToString(), "select getdate()"));
    }

    [WebMethod]
    public string[] RetornaIdsDisparos()
    {
        return ValoresDadosDisparos.RetornaidsDisparos();
    }

    [WebMethod]
    public string RetornaDisparo(int idDisparo)
    {
        return ValoresDadosDisparos.RetornaDisparo(idDisparo);
    }

    [WebMethod]
    public static bool ConfirmarCadastroDisparo(int idDisparo)
    {
        return ValoresDadosDisparos.ConfirmarCadastroDisparo(idDisparo);
    }
    
}
