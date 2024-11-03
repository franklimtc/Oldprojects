using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Requisicao
/// </summary>
public class Requisicao
{
    public enum categoria : int { Suprimento = 0, Tecnico = 1, Outros = 2 };
    public enum status { Aberto, Em_atendimento, Pendente, Pendente_com_Usuario, Fechado };

    #region Campos
    private string _idrequisicao;
    private string _codReq;
    private string _serie;
    private string _categoria;
    private string _resumo;
    private string _descricao;
    private string _status;
    private string _dtAbertura;
    private string _dtFechamento;
    private string _dtModificacao;
    private string _abertorPor;
    private string _modificadoPor;
    private string _cliente;
    private string _responsavel;
    private string _contador;
    private string _suprimento;
    private SLA _sla;
    private string _tempoSlq;


    public string Idrequisicao
    {
        get
        {
            return _idrequisicao;
        }

        set
        {
            _idrequisicao = value;
        }
    }

    public string CodReq
    {
        get
        {
            return _codReq;
        }

        set
        {
            _codReq = value;
        }
    }

    public string Serie
    {
        get
        {
            return _serie;
        }

        set
        {
            _serie = value;
        }
    }

    public string Categoria
    {
        get
        {
            return _categoria;
        }

        set
        {
            _categoria = value;
        }
    }

    public string Resumo
    {
        get
        {
            return _resumo;
        }

        set
        {
            _resumo = value;
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

    public string DtModificacao
    {
        get
        {
            return _dtModificacao;
        }

        set
        {
            _dtModificacao = value;
        }
    }

    public string AbertorPor
    {
        get
        {
            return _abertorPor;
        }

        set
        {
            _abertorPor = value;
        }
    }

    public string ModificadoPor
    {
        get
        {
            return _modificadoPor;
        }

        set
        {
            _modificadoPor = value;
        }
    }

    public string Cliente
    {
        get
        {
            return _cliente;
        }

        set
        {
            _cliente = value;
        }
    }

    public string Responsavel
    {
        get
        {
            return _responsavel;
        }

        set
        {
            _responsavel = value;
        }
    }

    public string Contador
    {
        get
        {
            return _contador;
        }

        set
        {
            _contador = value;
        }
    }

    public string Suprimento
    {
        get
        {
            return _suprimento;
        }

        set
        {
            _suprimento = value;
        }
    }

    public SLA Sla
    {
        get
        {
            return _sla;
        }

        set
        {
            _sla = value;
        }
    }

    public string TempoSlq
    {
        get
        {
            return string.Format("{0}:{1}", (int)this.Sla.TempoTotal.TotalHours, (int)this.Sla.TempoTotal.Minutes);
            //this.Sla.TempoTotal.ToString(@"hh\:mm\:ss");
        }
       
    }


    #endregion

    public Requisicao()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Requisicao Buscar(string codReq)
    {
        string tsql = string.Format("select * from requisicoes where codReq = '{0}';", codReq);
        DataTable dtReq = DAO.retornadt(DAO.connection.DefaultConnection.ToString(), tsql);
        Requisicao r = new Requisicao();

        if (dtReq.Rows.Count == 1)
        {
            r.Idrequisicao = dtReq.Rows[0]["idrequisicao"].ToString();
            r.CodReq = dtReq.Rows[0]["codReq"].ToString();
            r.Serie = dtReq.Rows[0]["serie"].ToString();
            r.Categoria = dtReq.Rows[0]["categoria"].ToString();
            r.Resumo = dtReq.Rows[0]["resumo"].ToString();
            r.Descricao = dtReq.Rows[0]["descricao"].ToString();
            r.Status = dtReq.Rows[0]["status"].ToString();
            r.DtAbertura = dtReq.Rows[0]["dtAbertura"].ToString();
            r.DtModificacao = dtReq.Rows[0]["dtModificacao"].ToString();
            r.AbertorPor = dtReq.Rows[0]["abertorPor"].ToString();
            r.ModificadoPor = dtReq.Rows[0]["modificadoPor"].ToString();
            r.Cliente = dtReq.Rows[0]["cliente"].ToString();
            r.Responsavel = dtReq.Rows[0]["responsavel"].ToString();
            r.Contador = dtReq.Rows[0]["contador"].ToString();
            r.Suprimento = dtReq.Rows[0]["suprimento"].ToString();
        }

        return r;
    }

    public static List<Requisicao> Listar()
    {
        List<Requisicao> Lista = new List<Requisicao>();
        string tsql = string.Format(@"SELECT idrequisicao, codReq, serie, categoria, resumo, descricao
, status, dtAbertura, dtModificacao, abertorPor, modificadoPor, cliente, contador, suprimento
FROM vwRequisicoes WHERE status <> 'Fechado' ");

        DataTable dtReqs = DAO.retornadt(DAO.connection.DefaultConnection.ToString(), tsql);
        if (dtReqs.Rows.Count > 0)
        {
            foreach (DataRow req in dtReqs.Rows)
            {
                Requisicao r = new Requisicao();

                r.Idrequisicao = req["idrequisicao"].ToString();
                r.CodReq = req["codReq"].ToString();
                r.Serie = req["serie"].ToString();
                r.Categoria = req["categoria"].ToString();
                r.Resumo = req["resumo"].ToString();
                r.Descricao = req["descricao"].ToString();
                r.Status = req["status"].ToString();
                r.DtAbertura = req["dtAbertura"].ToString();
                r.DtModificacao = req["dtModificacao"].ToString();
                r.AbertorPor = req["abertorPor"].ToString();
                r.ModificadoPor = req["modificadoPor"].ToString();
                r.Cliente = req["cliente"].ToString();
                r.Suprimento = req["suprimento"].ToString();
                r.Contador = req["contador"].ToString();

                Lista.Add(r);
            }
        }

        foreach (Requisicao req in Lista)
        {
            req.CalcularSL();
        }
        return Lista;
    }

    private void CalcularSL()
    {
        this.Sla = new SLA();
        this.Sla.Abertura = Convert.ToDateTime(this.DtAbertura, CultureInfo.CurrentCulture);

        if (this.Status != "Fechado")
            this.Sla.Fechamento = DateTime.Now;
        else
            this.Sla.Fechamento = Convert.ToDateTime(this.DtFechamento, CultureInfo.CurrentCulture);

        this.Sla.CalculoTempo();
    }

    public static List<Requisicao> Listar(string UserName)
    {
        List<Requisicao> Lista = new List<Requisicao>();
        string tsql = string.Format(@"SELECT idrequisicao,codReq,serie,categoria,resumo
        ,descricao,status,dtAbertura
        ,dtFechamento,dtModificacao,abertorPor,modificadoPor,cliente, suprimento, contador 
        FROM requisicoes where abertorPor = '{0}' and codReq is not null ORDER BY dtModificacao DESC;", UserName);

        DataTable dtReqs = DAO.retornadt(DAO.connection.DefaultConnection.ToString(), tsql);
        if (dtReqs.Rows.Count > 0)
        {
            foreach (DataRow req in dtReqs.Rows)
            {
                Requisicao r = new Requisicao();

                r.Idrequisicao = req["idrequisicao"].ToString();
                r.CodReq = req["codReq"].ToString();
                r.Serie = req["serie"].ToString();
                r.Categoria = req["categoria"].ToString();
                r.Resumo = req["resumo"].ToString();
                r.Descricao = req["descricao"].ToString();
                r.Status = req["status"].ToString();
                r.DtAbertura = req["dtAbertura"].ToString();
                r.DtFechamento = req["dtFechamento"].ToString();
                r.DtModificacao = req["dtModificacao"].ToString();
                r.AbertorPor = req["abertorPor"].ToString();
                r.ModificadoPor = req["modificadoPor"].ToString();
                r.Cliente = req["cliente"].ToString();
                r.Suprimento = req["suprimento"].ToString();
                r.Contador = req["contador"].ToString();

                Lista.Add(r);
            }
        }
        return Lista;
    }

    public static bool Abrir(string Identificador, string Usuario)
    {
        bool result = false;

        string tsql = string.Format("INSERT INTO requisicoes(chave, abertorPor) VALUES('{0}','{1}');", Identificador, Usuario);

        result = DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);

        return result;
    }

    public static bool Cancelar(string Identificador)
    {
        bool result = false;
        string tsql = string.Format("delete requisicoes where chave = '{0}' and codReq is null;", Identificador);
        result = DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);
        return result;
    }

    public static string RetornaIDReq(string chave)
    {
        string value = "000";
        string tsql = string.Format("select idrequisicao from requisicoes where chave = '{0}';", chave);
        value += DAO.ExecuteScalar(DAO.connection.DefaultConnection.ToString(), tsql).ToString();
        value = value.Substring(value.Length - 3);
        return value;
    }

    public static void AtualizarModificacao(string reqCod, string userName)
    {
        string tsql = string.Format("update requisicoes set modificadoPor= '{0}', dtModificacao = GETDATE() where codReq = '{1}';"
            , userName, reqCod);
        DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);
    }

    public static void AtualizarStatus(string reqCod, string UserName, status tpStatus)
    {
        string StatusAnterior = DAO.ExecuteScalar(DAO.connection.DefaultConnection.ToString(),
            string.Format("select status from requisicoes where codReq= '{0}';", reqCod)).ToString();

        string StatusAtual = tpStatus.ToString().Replace('_', ' ');

        if (StatusAnterior != null)
        {
            string descricao = string.Format("Status mudou de {0} para {1}.", StatusAnterior, StatusAtual);

            string tsqlInsert = string.Format("INSERT INTO logsRequisicoes(codReq, tipo, usuario, descricao) values('{0}', '{1}', '{2}', '{3}')"
            , reqCod, "Atualização de Status", UserName, descricao);

            string tsqlUpdate = null;
            switch (tpStatus)
            {
                case status.Fechado:
                    tsqlUpdate = string.Format("update requisicoes set status = '{0}', modificadoPor= '{1}', dtModificacao = GETDATE(), dtFechamento = GETDATE() where codReq = '{2}';",
                StatusAtual, UserName, reqCod);
                    break;
                default:
                    tsqlUpdate = string.Format("update requisicoes set status = '{0}', modificadoPor= '{1}', dtModificacao = GETDATE() where codReq = '{2}';",
                StatusAtual, UserName, reqCod);
                    break;
            }

            string.Format("update requisicoes set status = '{0}', modificadoPor= '{1}', dtModificacao = GETDATE() where codReq = '{2}';",
            StatusAtual, UserName, reqCod);

            if (DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsqlUpdate))
            {
                DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsqlInsert);
            }
        }

    }

    public static bool Salvar(string chave, string codReq, string name, string serie, string categoria, string resumo, string descricao, string cliente)
    {
        bool result = false;
        string tsql = string.Format("update requisicoes set codReq = '{0}', serie = '{1}', categoria = '{2}', resumo = '{3}', descricao = '{4}', cliente = '{6}' where chave = '{5}';",
            codReq, serie, categoria, resumo, descricao, chave, cliente);
        result = DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);
        return result;
    }

    public static bool Salvar(string chave, string codReq, string name, string serie, string categoria, string resumo, string descricao, string cliente, int contador, int suprimento)
    {
        bool result = false;
        string sup = "NULL";
        string cont = "NULL";

        if (suprimento > 0)
            sup = suprimento.ToString();
        if (contador > 0)
            cont = contador.ToString();

        string tsql = string.Format("update requisicoes set codReq = '{0}', serie = '{1}', categoria = '{2}', resumo = '{3}', descricao = '{4}', cliente = '{6}', contador = {7}, suprimento = {8} where chave = '{5}';",
            codReq, serie, categoria, resumo, descricao, chave, cliente, cont, sup);
        result = DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);
        if (result)
            EnviarAlerta(codReq);
        return result;
    }

    public static void Atender(string reqCod, string UserName)
    {
        string descricao = string.Format("Solicitação está em atendimento.");

        string tsqlInsert = string.Format("INSERT INTO logsRequisicoes(codReq, tipo, usuario, descricao) values('{0}', '{1}', '{2}', '{3}')"
        , reqCod, "Atualização de Status", UserName, descricao);

        string tsqlUpdate = string.Format("update requisicoes set status = '{0}', modificadoPor= '{1}', dtModificacao = GETDATE(), responsavel = '{1}' where codReq = '{2}';", "Em atendimento", UserName, reqCod);

        if (DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsqlUpdate))
        {
            DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsqlInsert);
        }
    }

    public static List<string> ListaStatus()
    {
        Array aStatus = Enum.GetValues(typeof(Requisicao.status));
        List<string> lStatus = new List<string>();
        for (int i = 0; i < aStatus.Length; i++)
        {
            lStatus.Add(aStatus.GetValue(i).ToString().Replace('_', ' '));
        }
        return lStatus;
    }

    public static void EnviarAlerta(string codReq)
    {
        Requisicao req = Requisicao.Buscar(codReq);

        string htmBody = string.Format(@"<h2>Requisição {0} aberta para o equipamento {1}</h2>
		<br>
		<p>Categoria: {2}</p>
		<p>Unidade: {3}</p>
		<p>Resumo: {4}</p>
		<p>Descrição: {5}</p>", req.CodReq, req.Serie, req.Categoria, req.AbertorPor.ToUpper(), req.Resumo, req.Descricao);

        string Assunto = string.Format("Nova Requisição: {0} - {1} - {2}", req.CodReq, req.Categoria, req.Serie);
        try
        {
            SMTP.Enviar("jane@csfdigital.com.br, jefferson@csfdigital.com.br", Assunto, htmBody);
        }
        catch
        { }
    }

}