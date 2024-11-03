using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dnaPrint.DAO;
using System.Configuration;
using System.Data;

/// <summary>
/// Descrição resumida de enviosSuprimentos
/// </summary>
public class enviosSuprimentos
{
    public string serie { get; set; }
    public string postagem { get; set; }
    public DateTime dtEnvio { get; set; }
    public string statusEntrega { get; set; }
    public int prazoEntrega { get; set; }
    public DateTime dtEntrega { get; set; }
    public string tpSuprimento { get; set; }


    static string ConnString = ConfigurationManager.ConnectionStrings["pecas"].ToString();
    public enviosSuprimentos()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public enviosSuprimentos(string _serie, string _postagem, DateTime _dtEnvio, string _statusEntrega, int _prazoEntrega, DateTime _dtEntrega)
    {
        this.serie = _serie;
        this.postagem = _postagem;
        this.dtEntrega = _dtEnvio;
        this.statusEntrega = _statusEntrega;
        this.prazoEntrega = _prazoEntrega;
        this.dtEntrega = _dtEntrega;
    }

    public static List<enviosSuprimentos> ListarPorSerie(string serie)
    {
        List<enviosSuprimentos> Lista = new List<enviosSuprimentos>();
        SQLServer sql = new SQLServer();
        string tsql = string.Format("select serie, tpSuprimento, postagem, dtEnvio, statusEntrega, prazoEntrega, dtEntrega from vw_listaEnvios where serie = '{0}';", serie.Trim());
        DataTable dt = sql.ReturnDt(ConnString, tsql);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                enviosSuprimentos envio = new enviosSuprimentos();

                envio.serie = row["serie"].ToString().ToUpper();
                envio.postagem = row["postagem"].ToString().ToUpper();
                envio.dtEnvio = DateTime.Parse(row["dtEnvio"].ToString());
                envio.statusEntrega = row["statusEntrega"].ToString().ToUpper();
                int intTemp = 25;
                int.TryParse(row["prazoEntrega"].ToString(), out intTemp);
                envio.prazoEntrega = intTemp;
                DateTime dtTemp = DateTime.Now;
                if (DateTime.TryParse(row["dtEntrega"].ToString(), out dtTemp))
                {
                    envio.dtEntrega = dtTemp;
                }
                envio.tpSuprimento = row["tpSuprimento"].ToString().ToUpper();
                Lista.Add(envio);
            }
        }

        return Lista;
    }

}