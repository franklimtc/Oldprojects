using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Ocomon
{
    /// <summary>
    /// Summary description for Suprimentos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Suprimentos : System.Web.Services.WebService
    {
        //req.Solicitar(reg.Serie, "Cilindro", reg.Cilindro, reg.CilindroEstimativaDias, reg.ContadorAtual);
        [WebMethod]
        public bool Solicitar(string serie, string suprimento, string statusSuprimento, string estimativaDias, string Contador)
        {
            bool result = false;

            string tsqlInsert = string.Format("EXEC SolicitarSuprimento '{0}', '{1}', {2}, {3}, {4};",
                serie, suprimento, Contador, statusSuprimento, estimativaDias);
            result = dao.ExecuteNonQuery(tsqlInsert);
            return result;
        }

        [WebMethod]
        public List<enviosSuprimento> ListarEnviosPendentes()
        {
            List<enviosSuprimento> lista =  enviosSuprimento.ListarPendentes();
            return lista;
        }

        [WebMethod]
        public bool ConfirmarInsercaoEnvio(string Postagem)
        {
            bool result = false;

            result = enviosSuprimento.Confirmar(Postagem);

            return result;
        }
    }
}
