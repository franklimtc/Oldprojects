using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ControlePostagenReversa
/// </summary>
public class ControlePostagenReversa :IBase
{
    #region Campos

    public int ID { get; set; }
    public int CodColeta { get; set; }
    public string Postagem { get; set; }
    public int TipoObj { get; set; }
    public string Tipo { get; set; }
    public int Quantidade { get; set; }
    public string Usuario { get; set; }
    public DateTime data { get; set; }
    public DateTime dataAtualizacao { get; set; }
    public int Status { get; set; }
    public string StatusSolicitacao { get; set; }
    public DateTime dataEntrega { get; set; }

    #endregion

    public ControlePostagenReversa()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public ControlePostagenReversa(int ColetaNumero, string PostagemNumero, int TipoObjeto, int QuantidadeSolicitada)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
        this.CodColeta = ColetaNumero;
        this.Postagem = PostagemNumero;
        this.TipoObj = TipoObjeto;
        this.Quantidade = QuantidadeSolicitada;
    }
    public ControlePostagenReversa(string ColetaNumero, string PostagemNumero, int TipoObjeto, int QuantidadeSolicitada)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
        this.CodColeta = int.Parse(ColetaNumero);
        this.Postagem = PostagemNumero;
        this.TipoObj = TipoObjeto;
        this.Quantidade = QuantidadeSolicitada;
    }

    public ControlePostagenReversa(int _ID, int _CodColeta, string _Postagem, int _TipoObj, string _Tipo, int _Quantidade, string _Usuario, DateTime _data, DateTime _dataAtualizacao, int _Status, string _StatusSolicitacao, DateTime _dataEntrega)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //

        this.ID = _ID;
        this.CodColeta = _CodColeta;
        this.Postagem = _Postagem;
        this.TipoObj = _TipoObj;
        this.Tipo = _Tipo;
        this.Quantidade = _Quantidade;
        this.Usuario = _Usuario;
        this.data = _data;
        this.dataAtualizacao = _dataAtualizacao;
        this.Status = _Status;
        this.StatusSolicitacao = _StatusSolicitacao;
        this.dataEntrega = _dataEntrega;
    }

    public static List<ControlePostagenReversa> ListarPorUsurio(string Usuario)
    {
        List<ControlePostagenReversa> Lista = new List<ControlePostagenReversa>();

        List<string[]> parametros = new List<string[]>();
        parametros.Add(new string[] { "@usuario", Usuario });

        DataTable dt = DAO.RetornaDt(DAO.connString(), "select ID, CodColeta, Postagem, TipoObj, Tipo, Quantidade, Usuario, data, dataAtualizacao, Status, StatusSolicitacao, dataEntrega from vw_ControlePostagensReversas where usuario = @usuario;", parametros);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                DateTime dtAtualizacaoTemp;
                DateTime dtEntregaTemp;
                DateTime.TryParse(row["dataAtualizacao"].ToString(), out dtAtualizacaoTemp);
                DateTime.TryParse(row["dataEntrega"].ToString(), out dtEntregaTemp);

                ControlePostagenReversa c = new ControlePostagenReversa(
                    int.Parse(row["ID"].ToString()),
                    int.Parse(row["CodColeta"].ToString()),
                    (row["Postagem"].ToString()),
                    int.Parse(row["TipoObj"].ToString()),
                    (row["Tipo"].ToString()),
                    int.Parse(row["Quantidade"].ToString()),
                    (row["Usuario"].ToString()),
                    DateTime.Parse(row["data"].ToString()),
                    dtAtualizacaoTemp,
                    int.Parse(row["Status"].ToString()),
                    (row["StatusSolicitacao"].ToString()),
                    dtEntregaTemp);
                Lista.Add(c);
            }
        }

        return Lista;
    }


    public static List<ControlePostagenReversa> ListarPorStatus(string status)
    {
        List<ControlePostagenReversa> Lista = new List<ControlePostagenReversa>();

        List<string[]> parametros = new List<string[]>();
        parametros.Add(new string[] { "@StatusSolicitacao", status });

        DataTable dt = DAO.RetornaDt(DAO.connString(), "select ID, CodColeta, Postagem, TipoObj, Tipo, Quantidade, Usuario, data, dataAtualizacao, Status, StatusSolicitacao, dataEntrega from vw_ControlePostagensReversas where StatusSolicitacao = @StatusSolicitacao;", parametros);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                ControlePostagenReversa c = new ControlePostagenReversa(
                    int.Parse(row["ID"].ToString()),
                    int.Parse(row["CodColeta"].ToString()),
                    (row["Postagem"].ToString()),
                    int.Parse(row["TipoObj"].ToString()),
                    (row["Tipo"].ToString()),
                    int.Parse(row["Quantidade"].ToString()),
                    (row["Usuario"].ToString()),
                    DateTime.Parse(row["data"].ToString()),
                    DateTime.Parse(row["dataAtualizacao"].ToString()),
                    int.Parse(row["Status"].ToString()),
                    (row["StatusSolicitacao"].ToString()),
                    DateTime.Parse(row["dataEntrega"].ToString()));
                Lista.Add(c);
            }
        }

        return Lista;
    }

    public static ControlePostagenReversa ListarPorId(int id)
    {
        List<ControlePostagenReversa> Lista = new List<ControlePostagenReversa>();

        List<string[]> parametros = new List<string[]>();
        parametros.Add(new string[] { "@ID", id.ToString() });

        DataTable dt = DAO.RetornaDt(DAO.connString(), "select ID, CodColeta, Postagem, TipoObj, Tipo, Quantidade, Usuario, data, dataAtualizacao, Status, StatusSolicitacao, dataEntrega from vw_ControlePostagensReversas where ID = @ID;", parametros);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                DateTime dtAtualizacaoTemp;
                DateTime dtEntregaTemp;
                DateTime.TryParse(row["dataAtualizacao"].ToString(), out dtAtualizacaoTemp);
                DateTime.TryParse(row["dataEntrega"].ToString(), out dtEntregaTemp);

                ControlePostagenReversa c = new ControlePostagenReversa(
                    int.Parse(row["ID"].ToString()),
                    int.Parse(row["CodColeta"].ToString()),
                    (row["Postagem"].ToString()),
                    int.Parse(row["TipoObj"].ToString()),
                    (row["Tipo"].ToString()),
                    int.Parse(row["Quantidade"].ToString()),
                    (row["Usuario"].ToString()),
                    DateTime.Parse(row["data"].ToString()),
                    dtAtualizacaoTemp,
                    int.Parse(row["Status"].ToString()),
                    (row["StatusSolicitacao"].ToString()),
                    dtEntregaTemp);
                Lista.Add(c);
            }
        }

        return Lista.First();
    }

    public bool Salvar()
    {
        string tsql = "update ControlePostagensReversas set postagem = @postagem, status = @status, dataAtualizacao = GETDATE() where id = @id";

        List<string[]> parametros = new List<string[]>();

        parametros.Add(new string[] { "@Postagem", this.Postagem });
        parametros.Add(new string[] { "@status", this.Status.ToString() });
        parametros.Add(new string[] { "@id", this.ID.ToString() });

        return DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);
    }

    public bool Adicionar()
    {
        string tsql = "INSERT INTO ControlePostagensReversas(CodColeta, Postagem, tipoObj, Quantidade, Usuario) VALUES(@CodColeta, @Postagem, @tipoObj,@Quantidade, @Usuario)";
        List<string[]> parametros = new List<string[]>();

        parametros.Add(new string[] { "@CodColeta", this.CodColeta.ToString() });
        parametros.Add(new string[] { "@Postagem", this.Postagem });
        parametros.Add(new string[] { "@tipoObj", this.TipoObj.ToString() });
        parametros.Add(new string[] { "@Quantidade", this.Quantidade.ToString() });
        parametros.Add(new string[] { "@Usuario", this.Usuario });

        return DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);
    }

    public bool Deletar()
    {
        string tsql = "delete ControlePostagensReversas where id = @id";

        List<string[]> parametros = new List<string[]>();
        parametros.Add(new string[] { "@id", this.ID.ToString() });
        return DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);
    }
}