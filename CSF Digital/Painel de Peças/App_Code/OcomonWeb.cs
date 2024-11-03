using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OcomonWeb
/// </summary>
public class OcomonWeb
{
    #region Campos

    private string _numero;

    public string Numero
    {
        get { return _numero; }
        set { _numero = value; }
    }
    private string _local;

    public string Local
    {
        get { return _local; }
        set { _local = value; }
    }
    private string _serie;

    public string Serie
    {
        get { return _serie; }
        set { _serie = value; }
    }
    private string _descricao;

    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    private string _toner;

    public string Toner
    {
        get { return _toner; }
        set { _toner = value; }
    }
    private string _foto;

    public string Foto
    {
        get { return _foto; }
        set { _foto = value; }
    }
    private string _dataAbertura;

    public string DataAbertura
    {
        get { return _dataAbertura; }
        set { _dataAbertura = value; }
    }
    private string _qtdDias;

    public string QtdDias
    {
        get { return _qtdDias; }
        set { _qtdDias = value; }
    }
    private string _tpEnvio;

    public string TpEnvio
    {
        get { return _tpEnvio; }
        set { _tpEnvio = value; }
    }
    private string _Nome;

    public string Nome
    {
        get { return _Nome; }
        set { _Nome = value; }
    }
    #endregion
    public OcomonWeb()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<OcomonWeb> retornaWebOcomon()
    {
        List<OcomonWeb> lista = new List<OcomonWeb>();

        Chamados.ChamadosSoapClient reqs = new Chamados.ChamadosSoapClient();
        Chamados.Req[] vetor = reqs.retornaChamados();
        

        for (int i = 0; i < vetor.Length; i++)
        {
            OcomonWeb reqTemp = new OcomonWeb();
            reqTemp.Numero = vetor[i].Numero;
            reqTemp.Serie = vetor[i].Serie;
            reqTemp.Local = vetor[i].Local;
            reqTemp.DataAbertura = vetor[i].DataAbertura;
            reqTemp.Descricao = vetor[i].Descricao;
            reqTemp.Nome = vetor[i].Nome;
            reqTemp.QtdDias = vetor[i].QtdDias;

            if (reqTemp.Descricao.ToUpper().Contains("SEDEX"))
                reqTemp.TpEnvio = "SEDEX";
            else if (reqTemp.Descricao.ToUpper().Contains("PAC"))
                reqTemp.TpEnvio = "PAC";
            else if (reqTemp.Descricao.ToUpper().Contains("MOTOBOY"))
                reqTemp.TpEnvio = "MOTOBOY";
            else reqTemp.TpEnvio = "INDEFINIDO";

            if (reqTemp.Descricao.ToString().ToUpper().Contains("TONER") || reqTemp.Descricao.ToString().ToUpper().Contains("TONNER"))
                reqTemp.Toner = "SIM";
            if (reqTemp.Descricao.ToString().ToUpper().Contains("CILINDRO") || reqTemp.Descricao.ToString().ToUpper().Contains("FOTO"))
                reqTemp.Foto = "SIM";
            lista.Add(reqTemp);
        }
        return lista;
    }
}