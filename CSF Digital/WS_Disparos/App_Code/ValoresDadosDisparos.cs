using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for ValoresDadosDisparos
/// </summary>
public class ValoresDadosDisparos
{
    public static string[] RetornaidsDisparos()
    {
        string consulta = @"declare @idInicial as int
declare @idFinal as int

set @idInicial= (select min(idDisparo) from dadosdisparos where inserida = 0)
set @idFinal = (select max(idDisparo) from dadosdisparos where inserida = 0)

if((@idfinal - 100) > @idInicial)
begin
	set @idFinal = @idInicial + 100
end

select @idInicial 'idInicial', @idFinal 'idFinal' ";

        DataTable dtIds = DAO.retornadt(ConfigurationManager.ConnectionStrings["Disparos"].ToString(), consulta);
        string[] ids = new string[2];
        foreach (DataRow id in dtIds.Rows)
        {
            ids[0] = id["idInicial"].ToString();
            ids[1] = id["idFinal"].ToString();
        }
        return ids;
    }
    public static string RetornaDisparo(int idDisparo)
    {
        string consulta = string.Format("select * from dadosdisparos where idDisparo = {0} for xml auto", idDisparo.ToString());
        return DAO.retornaValor(ConfigurationManager.ConnectionStrings["Disparos"].ToString(), consulta);
    }

    internal static bool ConfirmarCadastroDisparo(int idDisparo)
    {
        string consulta = string.Format("update dadosdisparos set inserida = 1 where iddisparo = {0}", idDisparo.ToString());
        return DAO.execute(ConfigurationManager.ConnectionStrings["Disparos"].ToString(), consulta);
    }
}