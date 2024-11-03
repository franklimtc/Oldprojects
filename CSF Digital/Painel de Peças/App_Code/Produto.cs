using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de Produto
/// </summary>
public class Produto: IBase
{
    #region Campos
    private int _id;
    private string _descricao;
    private string _partnumber;

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

    public string Partnumber
    {
        get
        {
            return _partnumber;
        }

        set
        {
            _partnumber = value;
        }
    }

    public static Produto BuscarPorID(int idProduto)
    {
        List<Produto> Lista = new List<Produto>();
        DataTable dtProdutos = DAO.RetornaDt(DAO.connString(), string.Format("select id, descricao, partnumber from produtos where id = {0}", idProduto));
        foreach (DataRow p in dtProdutos.Rows)
        {
            Lista.Add(new Produto(int.Parse(p["id"].ToString()), p["descricao"].ToString(), p["partnumber"].ToString()));
        }
        return Lista.First();
    }

    #endregion
    public Produto()
    { }

    public Produto(int id, string descricao, string partnumber)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //

        this.Id = id;
        this.Descricao = descricao;
        this.Partnumber = partnumber;
    }

    public static bool Existe(string PartNumber)
    {
        bool result = false;
        string query = string.Format("select count(*) from produtos where partnumber = '{0}'", PartNumber);
        string valor = DAO.ExecuteScalar(DAO.connString(), query);
        if (int.Parse(valor) > 0)
            result = true;
        return result;

    }

    public static Produto Buscar(object partnumber)
    {
        List<Produto> Lista = new List<Produto>();
        DataTable dtProdutos = DAO.RetornaDt(DAO.connString(), string.Format("select id, descricao, partnumber from produtos where partnumber = '{0}'", partnumber));
        foreach (DataRow p in dtProdutos.Rows)
        {
            Lista.Add(new Produto(int.Parse(p["id"].ToString()), p["descricao"].ToString(), p["partnumber"].ToString()));
        }
        if (Lista.Count > 0)
            return Lista.First();
        else
            return null;
    }

    public static int Saldo(string PartNumber)
    {
        int i = 0;
        int.TryParse(DAO.ExecuteScalar(DAO.connString(), string.Format("select Saldo from vw_EstoqueAtual where partnumber = '{0}' and estoque = '{1}';", PartNumber)), out i);
        return i;        
    }

    public static int Saldo(string PartNumber, string Estoque)
    {
        int i = 0;
        int.TryParse(DAO.ExecuteScalar(DAO.connString(), string.Format("select Saldo from vw_EstoqueAtual where partnumber = '{0}' and estoque = '{1}';", PartNumber, Estoque)), out i);
        return i;
    }

    public bool Adicionar()
    {
        bool result = false;
        string querySelect = string.Format("SELECT count(*) from produtos where partNumber = '{0}'", this.Partnumber);
        int qtdLinhas = int.Parse(DAO.ExecuteScalar(DAO.connString(), querySelect));
        if (qtdLinhas == 0)
        {
            string queryInsert = string.Format("INSERT INTO produtos(descricao, partnumber) VALUES('{0}','{1}')", this.Descricao, this.Partnumber);
            DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
            result = true;
        }
        return result;
    }

    public bool Deletar()
    {
        string queryInsert = string.Format("update produtos set status = 0 where id = {0}", this.Id);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }

    public static List<Produto> Listar()
    {
        List<Produto> Lista = new List<Produto>();
        DataTable dtProdutos = DAO.RetornaDt(DAO.connString(), "select id, descricao, partnumber from produtos where status = 1");
        foreach (DataRow p in dtProdutos.Rows)
        {
            Lista.Add(new Produto(int.Parse(p["id"].ToString()), p["descricao"].ToString(), p["partnumber"].ToString()));
        }
        return Lista;
    }

    public bool Salvar()
    {
        string queryInsert = string.Format("UPDATE produtos SET descricao = '{0}', partnumber = '{1}' WHERE id = {2}", this.Descricao, this.Partnumber, this.Id);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }
}