using System;
using System.Collections.Generic;
using System.Data;
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
    public void Adicionar(string postagem, string dtEnvio, string cepOrigem, string cepDestino, string responsavel, string email, string tpEnvio)
    {
        Postagen p = new Postagen(postagem, dtEnvio, cepOrigem, cepDestino,responsavel, email, tpEnvio);
        p.Rastrear();
    }

    [WebMethod]
    public void AdicionarDescricao(string descricao, string postagem, string dtEnvio, string cepOrigem, string cepDestino, string responsavel, string email, string tpEnvio)
    {
        Postagen p = new Postagen(descricao, postagem, dtEnvio, cepOrigem, cepDestino, responsavel, email, tpEnvio);
        p.Rastrear();
    }

    [WebMethod]
    public void AtualizarStatus()
    {
        List<Postagen> lista = Postagen.Listar(false);
        foreach (Postagen post in lista)
        {
            post.Rastrear();
        }
    }
    
}