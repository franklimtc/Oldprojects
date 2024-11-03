using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for estacao
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class estacao : System.Web.Services.WebService {

    public estacao () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool Atualizar(string nome, string versao, string dtInventarioInicial, string dtInventarioFinal, string usuario, string senha) 
    {
        bool result = false;
        string query = null;

        try
        {
            query = string.Format("exec lerEstacao '{0}', '{1}','{2}', '{3}', '{4}', '[5]', 1;", Descripto(nome), Descripto(versao), Descripto(dtInventarioInicial), Descripto(dtInventarioFinal), Descripto(usuario), senha);

        }
        catch (Exception ex)
        {
            DAO.ExecutaSQL(string.Format("insert into logs(componente, mensagem) values('{0}','{1}');", "estacao_ws", ex.ToString()));
        }
        result = DAO.ExecutaSQL(query);
        if (!result)
        {
            DAO.ExecutaSQL(string.Format("insert into logs(componente, mensagem) values('{0}','{1}');", "estacao_ws", string.Format("Falha ao inserir a query: {0}", query)));
        }
        return result;
    }

    [WebMethod]
    public bool RegistroLog(string componente, string mensagem)
    {
        bool result = false;

        string query = string.Format("insert into logs(componente, mensagem) values('{0}','{1}');", componente, mensagem);
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
