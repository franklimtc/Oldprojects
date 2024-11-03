using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace Ocomon
{
    /// <summary>
    /// Summary description for Chamados
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Chamados : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Req> retornaChamados()
        {
            List<Req> listaRequisicoes = new List<Req>();

            DataTable dtReqs = dao.retornaDt("ocomonWeb", "select numero, local, serie, descricao, data_abertura, qtdDias, nome from resumoOcomonOperador;");
            
            foreach (DataRow req in dtReqs.Rows)
            {
                Req r = Req.retornaReq(req["numero"].ToString(), req["local"].ToString(), req["serie"].ToString(), req["descricao"].ToString(), req["data_abertura"].ToString(), req["qtdDias"].ToString(), req["nome"].ToString());
                listaRequisicoes.Add(r);
            }

            return listaRequisicoes;
        }

        [WebMethod]
        public bool Fechar(string numero, string etiqueta, string postagem, string usuario, string serie)
        {
            bool result = false;
            if (Req.Fechar(numero, etiqueta, postagem, usuario, serie))
            {
                result = true;
            }
            EnviarEmail(numero, usuario, postagem, serie);
            return result;
        }

        
        public void EnviarEmail(string numero, string usuario, string postagem, string serie)
        {
            Req.Informar(numero, usuario, postagem, serie);
        }
    }
}
