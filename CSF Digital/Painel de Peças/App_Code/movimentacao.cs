using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de movimentacao
/// </summary>
public class movimentacao : IBase
{

    #region Campos

    public enum descricao { Entrada, Saida};

    private int _id;
    private string _descricao;
    private int _qtd;
    private DateTime _data;
    private int _idProduto;
    private int _idEstoque;
    private string _serie;
    private string _etiqueta;
    private string _usuario;

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

    public int Qtd
    {
        get
        {
            return _qtd;
        }

        set
        {
            _qtd = value;
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

    public int IdProduto
    {
        get
        {
            return _idProduto;
        }

        set
        {
            _idProduto = value;
        }
    }

    public int IdEstoque
    {
        get
        {
            return _idEstoque;
        }

        set
        {
            _idEstoque = value;
        }
    }

    public string Serie
    {
        get
        {
            if (_serie != null)
                return _serie;
            else
                return "";
        }

        set
        {
            _serie = value;
        }
    }

    public string Etiqueta
    {
        get
        {
            if (_etiqueta != null)
                return _etiqueta;
            else
                return "";
        }

        set
        {
            _etiqueta = value;
        }
    }

    public string Usuario
    {
        get
        {
            return _usuario;
        }

        set
        {
            _usuario = value;
        }
    }
    #endregion

    public movimentacao()
    {

    }

    public movimentacao(int _idEstoque, int _idProduto, int _qtd, descricao _desc, string _user)
    {
        this.IdEstoque = _idEstoque;
        this.IdProduto = _idProduto;
        this.Qtd = _qtd;
        this.Usuario = _user;
        this.Descricao = _desc.ToString();
    }

    public static List<movimentacao> Listar()
    {
        List<movimentacao> Lista = new List<movimentacao>();
        DataTable dtMovimentacoes = DAO.RetornaDt(DAO.connString(), "select id, descricao, qtd, data, idProduto, idEstoque, usuario from movimentacoes");
        foreach (DataRow mov in dtMovimentacoes.Rows)
        {
            Lista.Add(new movimentacao(int.Parse(mov["idEstoque"].ToString()), int.Parse(mov["idProduto"].ToString()), int.Parse(mov["qtd"].ToString()), RetornaTipo(mov["descricao"].ToString()), mov["usuario"].ToString()));
        }
        return Lista;
    }

    private static descricao RetornaTipo(string descricao)
    {
        descricao desc = movimentacao.descricao.Entrada;
        switch (descricao)
        {
            case "Entrada":
                desc = movimentacao.descricao.Entrada;
                break;
            case "Saida":
                desc = movimentacao.descricao.Saida;
                break;
            default:
                break;
        }
        return desc;
    }

    public static DataTable ListarMovimentacoesDia()
    {
        return DAO.RetornaDt(DAO.connString(), "select * from vw_MovimentacoesDoDia order by data desc");
    }

    public static DataTable ListarMovimentacoesDia(string estoque)
    {
        return DAO.RetornaDt(DAO.connString(), string.Format("select * from vw_MovimentacoesDoDia where estoque = '{0}'  order by data desc", estoque));
    }

    public bool Salvar()
    {
        string queryInsert = string.Format("update movimentacoes set  qtd = {0}, idProduto = {1}, serie = '{2}', etiqueta = '{3}', usuario = '{5}', data = GETDATE() where id = {4};",
            this.Qtd, this.IdProduto, this.Serie, this.Etiqueta, this.Id, this.Usuario);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }

    public bool Adicionar()
    {
        //string queryInsert = string.Format("INSERT INTO movimentacoes(descricao, qtd, idProduto, idEstoque) VALUES('{0}', {1}, {2}, {3})", this.Descricao, this.Qtd, this.IdProduto, this.IdEstoque);
        string queryInsert = string.Format("INSERT INTO movimentacoes(descricao, qtd, idProduto, idEstoque, serie, etiqueta, usuario) VALUES(@descricao, @qtd, @idProduto, @idEstoque, @serie, @etiqueta, @usuario)");
        List<string[]> paramentros = new List<string[]>();

        paramentros.Add(new string[] { "@descricao", this.Descricao });
        paramentros.Add(new string[] { "@qtd", this.Qtd.ToString()});
        paramentros.Add(new string[] { "@idProduto", this.IdProduto.ToString()});
        paramentros.Add(new string[] { "@idEstoque", this.IdEstoque.ToString()});
        paramentros.Add(new string[] { "@serie", this.Serie.ToString()});
        paramentros.Add(new string[] { "@etiqueta", this.Etiqueta.ToString()});
        paramentros.Add(new string[] { "@usuario", this.Usuario });

        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert, paramentros);
    }

    public bool Deletar()
    {
        string queryInsert = string.Format("delete movimentacoes where id = {0}", this.Id);
        return DAO.ExecuteNonQuery(DAO.connString(), queryInsert);
    }

    public movimentacao BuscarPorId(int idMovimentacao)
    {
        List<movimentacao> Lista = new List<movimentacao>();
        DataTable dtMovimentacoes = DAO.RetornaDt(DAO.connString(), string.Format("select * from movimentacoes where id = {0}", idMovimentacao));
        
        //id
        //descricao
        //qtd
        //data
        //idProduto
        //idEstoque
        //serie
        //etiqueta

        foreach (DataRow mov in dtMovimentacoes.Rows)
        {
            movimentacao mm = new movimentacao(int.Parse(mov["idEstoque"].ToString()), int.Parse(mov["idProduto"].ToString()), int.Parse(mov["qtd"].ToString()), RetornaTipo(mov["descricao"].ToString()), mov["usuario"].ToString());
            mm.Id = int.Parse(mov["id"].ToString());
            mm.Data = (DateTime)mov["data"];
            mm.Serie = mov["serie"].ToString();
            mm.Etiqueta = mov["etiqueta"].ToString();
            Lista.Add(mm);
        }
        return Lista.First();
    }
}