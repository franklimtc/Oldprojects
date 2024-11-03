using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Ocomon
{
    /// <summary>
    /// Summary description for Correios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Correios : System.Web.Services.WebService
    {

        [WebMethod]
        public Postagens.logPostgem Rastrear(string postagem)
        {
            return Postagens.html.Rastrear(postagem);
        }
    }
}
