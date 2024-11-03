using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Estoque
/// </summary>
public class Estoque : IBase
{
    #region campos

    private int _id;
    private string _descricao;
    private DateTime _data;
    private string _status;

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

   

    public string Descricao
    {
        get
        {
            return _descricao;
        }

        set
        {
            _descricao = value;
        }
    }

    public DateTime Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
        }
    }

   

    public string Status
    {
        get
        {
            return _status;
        }

        set
        {
            _status = value;
        }
    }

    #endregion
    public Estoque(int id, string descricao)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
        this.Id = id;
        this.Descricao = descricao;
    }

    public static List<Estoque> Listar()
    {
        List<Estoque> Lista = new List<Estoque>();
        DataTable dtEstoques = DAO.RetornaDt(DAO.connString(), "select id, descricao from estoques where status = 1  union all select 0, 'Todos' order by 1");
        foreach (DataRow p in dtEstoques.Rows)
        {
            Lista.Add(new Estoque(int.Parse(p["id"].ToString()), p["descricao"].ToString()));
        }
        return Lista;
    }

    public static Estoque BuscarPorId(int idEstoque)
    {
        List<Estoque> Lista = new List<Estoque>();
        DataTable dtEstoques = DAO.RetornaDt(DAO.connString(), string.Format("select id, descricao from estoques where id = {0}", idEstoque));
        foreach (DataRow p in dtEstoques.Rows)
        {
            Lista.Add(new Estoque(int.Parse(p["id"].ToString()), p["descricao"].ToString()));
        }
        return Lista.First();
    }

    public static DataTable EstoqueAtual()
    {
        return DAO.RetornaDt(DAO.connString(), "SELECT * FROM vw_EstoqueAtual");
    }

    public static object EstoqueAtual(string Estoque)
    {
        if (Estoque == "Todos")
            return EstoqueAtual();
        else
            return DAO.RetornaDt(DAO.connString(), String.Format("SELECT Produto, PartNumber, QtdEntrada 'Entrada', QtdSaida 'Saida', Saldo FROM vw_EstoqueAtual where estoque = '{0}'", Estoque));
    }

    public bool Adicionar()
    {
        string queryInsert = string.Format("INSERT INTO estoques(descricao) VALUES('{0}')", this.Descricao);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }

    public bool Deletar()
    {
        string queryInsert = string.Format("update estoques set status = 0 where id = {0}", this.Id);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }

    public bool Salvar()
    {
        string queryInsert = string.Format("UPDATE estoques SET descricao = '{0}' WHERE id = {1}", this.Descricao, this.Id);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }
}