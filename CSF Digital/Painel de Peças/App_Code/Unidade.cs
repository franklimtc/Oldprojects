using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Unidade
/// </summary>
public class Unidade
{
    private string _unidade;

    public Unidade(string Nome)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
        this.UnidadeDescricao = Nome;
    }

    public string UnidadeDescricao
    {
        get
        {
            return _unidade;
        }

        set
        {
            _unidade = value;
        }
    }

    public static List<Unidade> Listar(int idCliente, string uf, string cidade)
    {
        List<Unidade> lista = new List<Unidade>();

        DataTable dt = DAO.RetornaDt(DAO.connString(), string.Format("select distinct Unidade from equipamentos WHERE idCliente = {2} and UF = '{0}' and Cidade = '{1}' order by 1", uf, cidade, idCliente));

        foreach (DataRow row in dt.Rows)
        {
            lista.Add(new Unidade(row["Unidade"].ToString()));
        }

        return lista;
    }

}