using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Rastrear(string postagem) {

        Postagens.logPostgem log =  Postagens.html.Rastrear(postagem);
        return log.Status;
    }

    [WebMethod]
    public Postagens.logPostgem RastrearWS(string postagem)
    {
        Postagens.logPostgem log = new Postagens.logPostgem(postagem);
        return log;
    }

}