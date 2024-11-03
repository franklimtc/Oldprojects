using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for printjob
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class printjob : System.Web.Services.WebService {

    public printjob () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool Inserir(string value) {
        bool result = false;

        string query = Descripto(value);
        result = DAO.ExecutaSQL(query);

        return result;
    }

    public string Descripto(string value)
    {
        string result = null;
        result = Util.Descriptografar(Util.Chave(), Util.Vetor(), value);
        return result;
    }
    
}
