using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Ocomon
{
    /// <summary>
    /// Summary description for Recolhimento
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Recolhimento : System.Web.Services.WebService
    {

        [WebMethod]
        public bool Solicitar(string uf, string cidade, string unidade, string ambiente, string serie, string suprimento, string serialSuprimento)
        {
            bool result = false;
            string tsqlSelect = string.Format("select count(*) from controleRecolhimento where serialSuprimento = '{0}';", serialSuprimento);
            int qtdSerial = (int)dao.Execute(tsqlSelect);
            if (qtdSerial == 0)
            {
                string tsqlInsert = string.Format("insert into controleRecolhimento(uf, cidade, unidade, ambiente, serie, suprimento, serialSuprimento) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');",
                    uf, cidade, unidade, ambiente, serie, suprimento, serialSuprimento);
                result = dao.ExecuteNonQuery(tsqlInsert);
            }
            return result;
        }

        [WebMethod]
        public void AtualizarRastreio()
        {
            string tsqlSelect = string.Format("select idRecolhimento, postagem from controleRecolhimento where status <> 'Entregue' and postagem is not null");
            DataTable dtRecolhimentos = dao.retornaDt(tsqlSelect);

            foreach (DataRow rec in dtRecolhimentos.Rows)
            {
                Postagens.logPostgem log = new Postagens.logPostgem(rec["postagem"].ToString());
                if (log.Status != null)
                {
                    if (log.Status == "Objeto entregue ao destinatário")
                    {
                        string tsqlUpdate = string.Format("update controleRecolhimento set status = 'Entregue' where idRecolhimento = {0};", rec["idRecolhimento"].ToString());
                        dao.ExecuteNonQuery(tsqlUpdate);
                    }
                    else
                    {
                        string tsqlUpdate = string.Format("update controleRecolhimento set status = 'Recolhido' where idRecolhimento = {0};", rec["idRecolhimento"].ToString());
                        dao.ExecuteNonQuery(tsqlUpdate);
                    }
                }
               
            }
        }
    }
}
