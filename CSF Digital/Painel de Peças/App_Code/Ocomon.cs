using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for Ocomon
/// </summary>
public class Ocomon
{
    private string _chamado;

    public string Chamado
    {
        get { return _chamado; }
        set { _chamado = value; }
    }
    private string _tpEnvio;

    public string TpEnvio
    {
        get { return _tpEnvio; }
        set { _tpEnvio = value; }
    }
    private string _postagem;

    public string Postagem
    {
        get { return _postagem; }
        set { _postagem = value; }
    }
    private string _etiqueta;

    public string Etiqueta
    {
        get { return _etiqueta; }
        set { _etiqueta = value; }
    }

    private string _serie;

    public string Serie
    {
        get { return _serie; }
        set { _serie = value; }
    }

	public Ocomon(string serie, string postagem, string etiqueta, string tpEnvio)
	{
        this.Serie = serie;
        this.Postagem = postagem;
        this.Etiqueta = etiqueta;
        this.TpEnvio = tpEnvio;
	}

    public bool localizar()
    {
        bool localizado = false;
        if (this.Serie != "")
        {
            string sql = string.Format("select numero from resumoOcomon where serie = '{0}'", this.Serie);
            this.Chamado = DAO.ExecuteScalar(DAO.connString(), sql);
            int i = 0;
            localizado = int.TryParse(this.Chamado, out i);
        }
        return localizado;
    }
    

    public void fechar()
    {
        string sqlInsert = string.Format("insert into assentamentos(ocorrencia, assentamento,data,responsavel ,asset_privated,tipo_assentamento) values({0},'Suprimento sera enviado {1} com postagem {2} e etiqueta {3}.',now(),18,0,0);", 
            this.Chamado, this.TpEnvio, this.Postagem, this.Etiqueta);
        string sqlUpdate = string.Format("update ocorrencias set status = 4, data_fechamento = now() where numero = {0};", this.Chamado);
        
        //DAO.MysqlExecute(ConfigurationManager.ConnectionStrings["ocomon"].ToString(), sqlInsert);
        //DAO.MysqlExecute(ConfigurationManager.ConnectionStrings["ocomon"].ToString(), sqlUpdate);
    }

   
}