using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Validacoes
/// </summary>
public class Validacoes
{
	public Validacoes()
	{
		//
		// TODO: Add constructor logic here
		//

	}
    public static bool validaPartNumber(string partnumber)
    {
        bool result = false;

        string tsql = string.Format("select count(*) from tipoSuprimentos where codigo = '{0}';", partnumber);
        if (int.Parse(DAO.ExecuteScalar(DAO.connString(), tsql)) > 0)
        {
            result = true;
        }

        return result;
    }

    public static string newid()
    {
        string result = null;

        string tsql = string.Format("select newid();");
        result = DAO.ExecuteScalar(DAO.connString(), tsql);
        return result;
    }

    public static bool validaChamado(string Chamado, string Serie)
    {

        bool result = false;

        string tsql = string.Format("select COUNT(*) from reqsuprimentos where status = 'Aberto' and idreqSuprimento = '{0}' and serie = '{1}';", Chamado, Serie);
        if (int.Parse(DAO.ExecuteScalar(DAO.connString(), tsql)) > 0)
        {
            result = true;
        }

        return result;
    }
}