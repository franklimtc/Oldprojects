using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Correios
/// </summary>
public class Correios
{
    #region Campos
    private string _idChamadoCorreio;
    private string _postagem;
    private string _operador;
    private string _protocolo;
    private string _dtAbertura;
    private string _dtFechamento;
    private string _status;
    private string _obs;
    #endregion
   
    #region Get/set

    public string IdChamadoCorreio
    {
        get
        {
            return _idChamadoCorreio;
        }

        set
        {
            _idChamadoCorreio = value;
        }
    }

    public string Postagem
    {
        get
        {
            return _postagem;
        }

        set
        {
            _postagem = value;
        }
    }

    public string Operador
    {
        get
        {
            return _operador;
        }

        set
        {
            _operador = value;
        }
    }

    public string Protocolo
    {
        get
        {
            return _protocolo;
        }

        set
        {
            _protocolo = value;
        }
    }

    public string DtAbertura
    {
        get
        {
            return _dtAbertura;
        }

        set
        {
            _dtAbertura = value;
        }
    }

    public string DtFechamento
    {
        get
        {
            return _dtFechamento;
        }

        set
        {
            _dtFechamento = value;
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

    #endregion

    public Correios()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Correios(string postagem)
    {
        string tsql = string.Format("select * from chamadosCorreios where postagem = '{0}';", postagem);
        DataTable dt = DAO.RetornaDt(DAO.connString(), tsql);

        foreach (DataRow log in dt.Rows)
        {
            this.IdChamadoCorreio = log["idChamadoCorreio"].ToString();
            this.Postagem = log["postagem"].ToString();
            this.Operador = log["operador"].ToString();
            this.Protocolo = log["protocolo"].ToString();
            this.DtAbertura = log["dtAbertura"].ToString();
            this.DtFechamento = log["dtFechamento"].ToString();
            this.Obs = log["obs"].ToString();
        }

    }
    public static bool AbrirReclamacao(string postagem, string operador)
    {
        string tsql = string.Format("insert into chamadosCorreios(postagem, operador) values('{0}','{1}');", postagem, operador);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static bool AbrirReclamacao(string postagem, string operador, string protocolo)
    {
        string tsql = string.Format("insert into chamadosCorreios(postagem, operador, protocolo) values('{0}','{1}','{2}');", postagem, operador, protocolo);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public bool Atualizar()
    {
        if (this.DtAbertura == "")
            this.DtAbertura = "null";
        else
            this.DtAbertura = "'" + this.DtAbertura + "'";
        if (this.DtFechamento == "")
            this.DtFechamento = "null";
        else
            this.DtFechamento = "'" + this.DtFechamento + "'";

        string tsql = string.Format("update chamadosCorreios set protocolo = '{1}', dtAbertura ={2}, dtFechamento = {3}, obs = '{4}' where idChamadoCorreio =  '{0}';", this.IdChamadoCorreio, this.Protocolo, this.DtAbertura, this.DtFechamento, this.Obs);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static void Cancelar(string postagem, string operador)
    {
        string tsqlUpdate = string.Format("update enviosSuprimentos set verificado = 1 where postagem = '{0}'", postagem);
        string tsqlInsert = string.Format("insert into chamadosCorreios(postagem, operador, dtFechamento, status, obs) values('{0}','{1}',GETDATE(),'Fechada','Verificado por {1}');", postagem, operador);
        DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
        DAO.ExecuteNonQuery(DAO.connString(), tsqlInsert);
    }

    public static bool ReclamacaoAberta(string postagem)
    {
        string tsql = string.Format("select count(*) from chamadosCorreios where postagem = '{0}';", postagem);
        int qtd = 0;
        int.TryParse(DAO.ExecuteScalar(DAO.connString(), tsql), out qtd);
        bool result = false;
        if (qtd > 0)
            result = true;
        return result;
    }

    public static int CalculaPrazo(string servico, string cepOrigem, string cepDestino)
    {
        int qtdDias = 0;
        string codicoServico = null;
        switch (servico.ToUpper())
        {
            case "SEDEX":
                codicoServico = "40010";
                break;
            case "PAC":
                codicoServico = "41106";
                break;
            default:
                codicoServico = "41106";
                break;
        }

        CorreiosPrazo.CalcPrecoPrazoWS calc = new CorreiosPrazo.CalcPrecoPrazoWS();
        CorreiosPrazo.cResultado result = null;
        try
        {
            result = calc.CalcPrazo(codicoServico, cepOrigem, cepDestino);
        }
        catch { }

        qtdDias = int.Parse(result.Servicos[0].PrazoEntrega);
        return qtdDias;
    }

    public static void Concluir(string idOcorrencia, bool deferido)
    {
        string defe = null;
        if (deferido)
        {
            defe = "1";
        }
        else
        {
            defe = "0";
        }
        string tsql = string.Format("update ChamadosCorreios set deferido = {0}, dtFechamento = GETDATE() where idChamadoCorreio = {1};", defe, idOcorrencia);
        DAO.ExecuteNonQuery(DAO.connString(), tsql);

    }
}