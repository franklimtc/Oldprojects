
using System;
using System.Collections.Generic;
using System.Data;
/// <summary>
/// Summary description for reqPecas
/// </summary>
public class reqPecas
{
    #region Campos

    private string _idCliente;

    public string IdCliente
    {
        get { return _idCliente; }
        set { _idCliente = value; }
    }

    private string _idreqPeca;

    public string IdreqPeca
    {
        get { return _idreqPeca; }
        set { _idreqPeca = value; }
    }
    private string _reqUSD;

    public string ReqUSD
    {
        get { return _reqUSD; }
        set
        {
            if (value != "")
                _reqUSD = value;
            else
                _reqUSD = "NÃO INFORMADO";
        }
    }

    private string _peca;

    public string Peca
    {
        get { return _peca; }
        set { _peca = value; }
    }
    private string _partNumber;

    public string PartNumber
    {
        get { return _partNumber; }
        set { _partNumber = value; }
    }
    private string _qtd;

    public string Qtd
    {
        get { return _qtd; }
        set { _qtd = value; }
    }
    private string _solicitante;

    public string Solicitante
    {
        get { return _solicitante; }
        set
        {
            if (value != "")
                _solicitante = value;
            else
                _solicitante = "NÃO INFORMADO";
        }
    }

    private string _tecnico;

    public string Tecnico
    {
        get { return _tecnico; }
        set
        {
            if (value != "")
                _tecnico = value;
            else
                _tecnico = "NÃO INFORMADO";
        }
    }
    private string _serieEqpto;

    public string SerieEqpto
    {
        get { return _serieEqpto; }
        set { _serieEqpto = value; }
    }
    private string _dtSolicitacao;

    public string DtSolicitacao
    {
        get { return _dtSolicitacao; }
        set { _dtSolicitacao = value; }
    }
    private string _status;

    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }
    private string _dtCriacao;

    public string DtCriacao
    {
        get { return _dtCriacao; }
        set { _dtCriacao = value; }
    }

    private string _critico;

    public string Critico
    {
        get { return _critico; }
        set { _critico = value; }
    }

    public string Obs
    {
        get
        {
            return _obs;
        }

        set
        {
            _obs = value;
        }
    }

    private string _obs;

    #endregion

    public reqPecas()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public static reqPecas ListarPorId(int idreqPeca)
    {
        reqPecas r = new reqPecas();
        List<object[]> parametros = new List<object[]>();
        parametros.Add(new object[] { "@idReqPeca", idreqPeca});

        string query = @"select idreqPeca, reqUSD, peca, partNumber, qtd, solicitante, tecnico, serieEqpto
, dtSolicitacao, dtCriacao, dtAberturaUSD, status, obs, eqpParado, idCliente
, statusEntrega, idReqUniq, dtAtualizacao, atualizadoPor from reqPecas  where idreqPeca = @idReqPeca and status not in ('Atendido', 'Cancelado')";

        DataTable dt = DAO.RetornaDt(DAO.connString(), query, parametros);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                r.IdreqPeca = idreqPeca.ToString();
                r.ReqUSD = row["reqUSD"].ToString();
                r.Peca = row["peca"].ToString();
                r.PartNumber = row["partNumber"].ToString();
                r.Qtd = row["qtd"].ToString();
                r.Solicitante = row["solicitante"].ToString();
                r.Tecnico = row["tecnico"].ToString();

                r.SerieEqpto = row["serieEqpto"].ToString();
                r.Status = row["status"].ToString();
                r.Obs = row["obs"].ToString();
                r.IdCliente = row["idCliente"].ToString();
            }
        }

        return r;
    }

    public bool Cadastrar()
    {
        string tSql = "INSERT INTO reqPecas(reqUSD,peca,partNumber,qtd,solicitante,tecnico,serieEqpto,dtSolicitacao, dtAberturaUSD, status, eqpParado, idCliente)"
                        + " VALUES('" + this.ReqUSD + "','" + this.Peca + "','" + this.PartNumber + "'," + this.Qtd + ",'" + this.Solicitante + "','" + this.Tecnico + "','" + this.SerieEqpto + "',GETDATE(),'" + this.DtSolicitacao + "','Aberto','" + this.Critico + "',"+this.IdCliente+")";
        return DAO.ExecuteNonQuery(DAO.connString(), tSql);
    }

    public bool Cadastrar(string idReqUniq)
    {
        string tSql = "INSERT INTO reqPecas(reqUSD,peca,partNumber,qtd,solicitante,tecnico,serieEqpto,dtSolicitacao, dtAberturaUSD, status, eqpParado, idCliente, idReqUniq)"
                         + " VALUES('" + this.ReqUSD + "','" + this.Peca + "','" + this.PartNumber + "'," + this.Qtd + ",'" + this.Solicitante + "','" + this.Tecnico + "','" + this.SerieEqpto + "',GETDATE(),'" + this.DtSolicitacao + "','Aberto','" + this.Critico + "'," + this.IdCliente + ",'" + idReqUniq + "')";
        return DAO.ExecuteNonQuery(DAO.connString(), tSql);
    }

    public static bool Atualizar(string idReqPeca, string Postagem, string Data, string obs, string status, string usuario,string dtPrevisaoEntrega)
    {
        string tsql = null;
        if (dtPrevisaoEntrega != "")
        {
            tsql = "INSERT INTO atulPecas(idreqPeca, postagem, obs, dtEnvio, status, usuario,dtPrevisaoEntrega)"
        + " VALUES(" + idReqPeca + ",'" + Postagem + "','" + obs + "','" + Data + "','" + status + "','" + usuario + "','" + dtPrevisaoEntrega + "')";
        }
        else
        {
            tsql = "INSERT INTO atulPecas(idreqPeca, postagem, obs, dtEnvio, status, usuario,dtPrevisaoEntrega)"
            + " VALUES(" + idReqPeca + ",'" + Postagem + "','" + obs + "','" + Data + "','" + status + "','" + usuario + "',null)";
        }
        

        string updateSql = "UPDATE reqPecas SET status = '" + status + "' WHERE idreqPeca = " + idReqPeca;

        DAO.ExecuteNonQuery(DAO.connString(), updateSql);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static bool Atualizar(string idReqPeca, string Postagem, string Data, string obs, string status, string usuario, string dtPrevisaoEntrega, string ar)
    {
        string tsql = null;
        if (dtPrevisaoEntrega != "")
        {
            tsql = "INSERT INTO atulPecas(idreqPeca, postagem, obs, dtEnvio, status, usuario,dtPrevisaoEntrega, ar)"
        + " VALUES(" + idReqPeca + ",'" + Postagem + "','" + obs + "','" + Data + "','" + status + "','" + usuario + "','" + dtPrevisaoEntrega + "','" + ar + "')";
        }
        else
        {
            tsql = "INSERT INTO atulPecas(idreqPeca, postagem, obs, dtEnvio, status, usuario,dtPrevisaoEntrega, ar)"
            + " VALUES(" + idReqPeca + ",'" + Postagem + "','" + obs + "','" + Data + "','" + status + "','" + usuario + "',null, '" + ar + "')";
        }


        string updateSql = "UPDATE reqPecas SET status = '" + status + "' WHERE idreqPeca = " + idReqPeca;

        DAO.ExecuteNonQuery(DAO.connString(), updateSql);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static void ConfirmarEntrega(int idReqPeca, string name)
    {
        string tsqlUpdate = string.Format("update reqpecas set statusEntrega = 'Objeto entregue - Confirmado por {1}' where idreqpeca = {0}", idReqPeca.ToString(), name);
        DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
    }

}