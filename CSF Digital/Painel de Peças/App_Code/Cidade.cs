using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Cidade
/// </summary>
public class Cidade
{
    private string _cidade;
    public Cidade(string cidade)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
        this.CidadeDescricao = cidade;
    }

    public string CidadeDescricao
    {
        get
        {
            return _cidade;
        }

        set
        {
            _cidade = value;
        }
    }

    public static List<Cidade> Listar(int idCliente, string uf)
    {
        List<Cidade> lista = new List<Cidade>();

        DataTable dt = DAO.RetornaDt(DAO.connString(), string.Format("select distinct Cidade from equipamentos WHERE UF = '{0}' and idCliente = {1} order by 1", uf, idCliente));

        foreach (DataRow row in dt.Rows)
        {
            lista.Add(new Cidade(row["Cidade"].ToString()));
        }

        return lista;
    }

}