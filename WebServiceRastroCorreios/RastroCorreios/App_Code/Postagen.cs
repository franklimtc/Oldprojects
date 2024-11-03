using System;
using Postagens;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Postagen
/// </summary>
public class Postagen
{
    #region Campos

    private string _idPostagem;
    private string _postagem;
    private string _dtEnvio;
    private string _dtEntrega;
    private string _status;
    private string _responsavel;
    private string _email;
    private string _cepOrigem;
    private string _cepDestino;
    private string _tpEnvio;
    private string _prazoEntrega;
    private string _descricao;

    public string IdPostagem
    {
        get
        {
            return _idPostagem;
        }

        set
        {
            _idPostagem = value;
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

    public string DtEnvio
    {
        get
        {
            return _dtEnvio;
        }

        set
        {
            _dtEnvio = value;
        }
    }

    

    public string DtEntrega
    {
        get
        {
            return _dtEntrega;
        }

        set
        {
            _dtEntrega = value;
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

    public string Email
    {
        get
        {
            return _email;
        }

        set
        {
            _email = value;
        }
    }

    public string CepOrigem
    {
        get
        {
            return _cepOrigem;
        }

        set
        {
            _cepOrigem = value;
        }
    }

    public string CepDestino
    {
        get
        {
            return _cepDestino;
        }

        set
        {
            _cepDestino = value;
        }
    }

    public string TpEnvio
    {
        get
        {
            return _tpEnvio;
        }

        set
        {
            _tpEnvio = value;
        }
    }

    public string PrazoEntrega
    {
        get
        {
            return _prazoEntrega;
        }

        set
        {
            _prazoEntrega = value;
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

    #endregion

    public Postagen()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Postagen(string postagem, string dtEnvio, string cepOrigem, string cepDestino, string responsavel, string email, string tpEnvio)
    {
        this.Postagem = postagem;
        this.DtEnvio = dtEnvio;
        this.Responsavel = responsavel;
        this.Email = email;
        this.CepDestino = cepDestino;
        this.CepOrigem = cepOrigem;
        this.TpEnvio = tpEnvio;
        if (!this.Existe())
        {
            this.Adicionar();
        }
        else
        {
            this.IdPostagem = CarregarID(this.Postagem);
        }
    }

    public Postagen(string descricao, string postagem, string dtEnvio, string cepOrigem, string cepDestino, string responsavel, string email, string tpEnvio)
    {
        this.Postagem = postagem;
        this.DtEnvio = dtEnvio;
        this.Responsavel = responsavel;
        this.Email = email;
        this.CepDestino = cepDestino;
        this.CepOrigem = cepOrigem;
        this.TpEnvio = tpEnvio;
        this.Descricao = descricao;

        if (!this.Existe())
        {
            this.Adicionar();
        }
        else
        {
            this.IdPostagem = CarregarID(this.Postagem);
        }
    }

    private bool Existe()
    {
        bool result = false;

        object obj = dao.RetornaValor(string.Format("SELECT COUNT(*) from postagens where postagem = '{0}';", this.Postagem));
        if (int.Parse(obj.ToString()) > 0)
            result = true;
        return result;
    }

    private void Adicionar()
    {
        if (this.CepDestino != "" && this.CepOrigem != "")
        {
            Prazo.servicoCorreios p = Prazo.servicoCorreios.pac;
            if (this.TpEnvio.ToLower() == "sedex")
                p = Prazo.servicoCorreios.sedex;
            this.PrazoEntrega = Postagens.Prazo.CalculaPrazo(p, this.CepOrigem, this.CepDestino).ToString();
        }
        else
        {
            this.PrazoEntrega = "-1";
        }
        if (this.Postagem != null && this.DtEnvio != null && this.Responsavel != "" && this.Email != "")
        {
            string tsqlInsert = string.Format("INSERT INTO postagens(postagem, dtEnvio, responsavel, email, cepOrigem, cepDestino, tpEnvio, prazoEntrega, descricao) VALUES('{0}','{1}','{2}','{3}','{4}','{5}', '{6}',{7},'{8}');",
                this.Postagem, DateTime.Parse(this.DtEnvio).ToString("yyyy-MM-dd"),this.Responsavel, this.Email, this.CepOrigem, this.CepDestino, this.TpEnvio, this.PrazoEntrega, this.Descricao);
            dao.Execute(tsqlInsert);
        }
    }

    private string CarregarID(string postagem)
    {
        return dao.RetornaValor(string.Format("SELECT idPostagem FROM postagens WHERE postagem = '{0}';", this.Postagem)).ToString();
    }

    internal void Rastrear()
    {
        logPostgem log = Postagens.html.Rastrear(this.Postagem);
        if (log.Status != "" && log.Status != null)
        {
            if (log.Status != this.Status)
                this.AtualizarStatus(log.Status);
        }
        if (this.Status == "Entrega Efetuada")
        {
            EnviarConfirmacaoEntrega();
        }
    }

    private void EnviarConfirmacaoEntrega()
    {
        string assunto = null;
        if (this.Descricao !="")
            assunto = string.Format("{0} - {1} - Entrega do objeto efetuada", this.Descricao, this.Postagem);
        else
            assunto = string.Format("{0} - Entrega do objeto efetuada", this.Postagem);

        string mensagem = "O Objeto enviado foi entregue conforme solicitação.";

        SMTP.Enviar(assunto, mensagem, this.Email);
        ConfirmarEnvioEmail();
    }

    private void ConfirmarEnvioEmail()
    {
        string tsqlUpdate = string.Format("UPDATE postagens set usuarioInformado = 1 WHERE postagem = '{0}'", this.Postagem);
        dao.Execute(tsqlUpdate);
    }

    private void AtualizarStatus(string status)
    {
        string tsqlupdate = string.Format("UPDATE postagens SET status = '{0}' WHERE postagem = '{1}';",status, this.Postagem);
        dao.Execute(tsqlupdate);
        this.Status = status;
    }

    public static List<Postagen> Listar(bool Entregues)
    {
        List<Postagen> lista = new List<Postagen>();
        string tsql = null;

        if (Entregues)
            tsql = "SELECT * FROM postagens where usuarioInformado is null AND status = 'Entrega Efetuada'";
        else
            tsql = "SELECT idPostagem, postagem, dtEnvio, status, responsavel, email FROM postagens where status <> 'Entrega Efetuada' or status is null;";

        DataTable dtPostagens = dao.retornadt(tsql);

        foreach (DataRow post in dtPostagens.Rows)
        {
            Postagen p = new Postagen();
            p.IdPostagem = post["idPostagem"].ToString();
            p.Postagem = post["postagem"].ToString();
            p.DtEntrega = post["dtEnvio"].ToString();
            p.Status = post["status"].ToString();
            p.Responsavel = post["responsavel"].ToString();
            p.Email = post["email"].ToString();
            lista.Add(p);
        }

        return lista;
    }
}