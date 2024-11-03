using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for reqSuprimentos
/// </summary>
public class reqSuprimentos
{
    #region Campos

    private string _idreqSuprimento;
    private string _serie;
    private string _valorAtual;
    private string _durabilidadeEstimada;
    private string _falhaAnterior;
    private string _solicitante;
    private string _usuario;
    private string _emailUsuario;
    private string _telefoneUsuario;
    private string _endereco;
    private string _bairro;
    private string _cep;
    private string _uf;
    private string _cidade;
    private string _status;
    private string _atendidoPor;
    private string _dataSolicitacao;
    private string _dataAtendimento;
    private string _reqUSD;
    private string _obs;
    private string _suprimento;
    private string _contador;

    public string IdreqSuprimento
    {
        get
        {
            return _idreqSuprimento;
        }

        set
        {
            _idreqSuprimento = value;
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

    public string ValorAtual
    {
        get
        {
            return _valorAtual;
        }

        set
        {
            _valorAtual = value;
        }
    }

    public string DurabilidadeEstimada
    {
        get
        {
            return _durabilidadeEstimada;
        }

        set
        {
            _durabilidadeEstimada = value;
        }
    }

    public string FalhaAnterior
    {
        get
        {
            return _falhaAnterior;
        }

        set
        {
            _falhaAnterior = value;
        }
    }

    public string Solicitante
    {
        get
        {
            return _solicitante;
        }

        set
        {
            _solicitante = value;
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

    


    public string EmailUsuario
    {
        get
        {
            return _emailUsuario;
        }

        set
        {
            _emailUsuario = value;
        }
    }

   

    public string TelefoneUsuario
    {
        get
        {
            return _telefoneUsuario;
        }

        set
        {
            _telefoneUsuario = value;
        }
    }

    public string Endereco
    {
        get
        {
            return _endereco;
        }

        set
        {
            _endereco = value;
        }
    }



    public string Bairro
    {
        get
        {
            return _bairro;
        }

        set
        {
            _bairro = value;
        }
    }

    public string Cep
    {
        get
        {
            return _cep;
        }

        set
        {
            _cep = value;
        }
    }

    public string Uf
    {
        get
        {
            return _uf;
        }

        set
        {
            _uf = value;
        }
    }

    public string Cidade
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

    public string AtendidoPor
    {
        get
        {
            return _atendidoPor;
        }

        set
        {
            _atendidoPor = value;
        }
    }

    public string DataSolicitacao
    {
        get
        {
            return _dataSolicitacao;
        }

        set
        {
            _dataSolicitacao = value;
        }
    }

    public string DataAtendimento
    {
        get
        {
            return _dataAtendimento;
        }

        set
        {
            _dataAtendimento = value;
        }
    }

    public string ReqUSD
    {
        get
        {
            return _reqUSD;
        }

        set
        {
            _reqUSD = value;
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
    #endregion

    public reqSuprimentos()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Adicionar()
    {
        bool result = false;

        string tsqlInserir = @"EXECUTE SolicitarSuprimento2
                        @serie  ,@suprimento  ,@contador  ,@valorAtual  ,@durabilidadeEstimada  ,@solicitante  ,@contato  ,@emailContato  ,@codReq  ,@falha  ,@obs";

        List<object[]> parametros = new List<object[]>();
        parametros.Add(new object[] { "@serie", this.Serie });
        parametros.Add(new object[] { "@suprimento", this.Suprimento });
        parametros.Add(new object[] { "@contador", this.Contador });
        parametros.Add(new object[] { "@valorAtual", this.ValorAtual});
        parametros.Add(new object[] { "@durabilidadeEstimada", this.DurabilidadeEstimada });
        parametros.Add(new object[] { "@solicitante", this.Solicitante});
        parametros.Add(new object[] { "@contato", this.Usuario });
        parametros.Add(new object[] { "@emailContato", this.EmailUsuario});
        parametros.Add(new object[] { "@codReq", this.ReqUSD});
        parametros.Add(new object[] { "@falha", this.FalhaAnterior});
        parametros.Add(new object[] { "@obs", this.Obs});

        if (DAO.ExecuteNonQuery(DAO.connString(), tsqlInserir,parametros))
        {
            result = true;
            informarOperadorSolicitacao();
        }

        return result;
    }

    private void informarOperadorSolicitacao()
    {
        Usuarios user = new Usuarios(this.Solicitante);
        EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();
        string mensagem = string.Format("Suprimento ({0}) solicitado para a série {1}!", this.Suprimento, this.Serie);
        msg.Enviar(user.Email, string.Format("{0} - {1} - Suprimento Solicitado", this.ReqUSD, this.Serie), mensagem, "Painel de Peças");
    }

    public void informarOperador(string mensagem)
    {
        Usuarios user = new Usuarios(this.Solicitante);

        string emailUser = "sac@csfdigital.com.br";
        if (user.Email != null)
        {
            emailUser = user.Email;
        }

        //EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();
        //msg.Enviar(user.Email, string.Format("{0} - {1} - Suprimento Enviado", this.ReqUSD, this.Serie), mensagem, "Painel de Peças");
        EmailBD email = new EmailBD(this.EmailUsuario, emailUser, null, string.Format("{0} - {1} - Suprimento Enviado", this.ReqUSD, this.Serie), mensagem, "1");
        email.Adicionar();


    }
    public static void AtualizarPrazo(string postagem)
    {
        DataTable dtDados = DAO.RetornaDt(DAO.connString(), string.Format("select top 1 origem, destino, tpEnvio from vw_chamadoscorreios where postagem = '{0}'", postagem));
        if (dtDados.Rows.Count > 0)
        {
            int prazo = 0;
            try
            {
                prazo = Correios.CalculaPrazo(dtDados.Rows[0]["tpEnvio"].ToString(), dtDados.Rows[0]["origem"].ToString(), dtDados.Rows[0]["destino"].ToString());
            }
            catch { }
            if (prazo > 0)
            {
                string tsqlupdate = string.Format("update enviosSuprimentos set prazoEntrega = {0} where postagem = '{1}';", prazo.ToString(), postagem);
                DAO.ExecuteNonQuery(DAO.connString(), tsqlupdate);
            }
        }
        
    }


    /// <summary>
    /// Informa cliente e operadores sobre o envio dos suprimentos
    /// </summary>
    /// <param name="mensagem"></param>
    /// 

    public void informarCliente(string mensagem)
    {
        Usuarios user = new Usuarios(this.Solicitante);
        EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();


        if (this.ReqUSD != "" && this.ReqUSD != null)
        {
            if (this.EmailUsuario != "" && this.EmailUsuario != null)
            {
                msg.EnviarHtmlMessageCopia(this.EmailUsuario, user.Email, string.Format("{0} - {1} - Suprimento Enviado", this.ReqUSD, this.Serie), mensagem, "Envio de Suprimentos", true);
            }
            else
            {
                msg.EnviarHtmlMessage(user.Email, string.Format("{0} - {1} - Suprimento Enviado", this.ReqUSD, this.Serie), mensagem, "Envio de Suprimentos", true);
            }
        }
        else
        {
            if (this.EmailUsuario != "" && this.EmailUsuario != null)
            {
                msg.EnviarHtmlMessageCopia(this.EmailUsuario, user.Email, string.Format("{0} - Suprimento Enviado", this.Serie), mensagem, "Envio de Suprimentos", true);
            }
            else
            {
                msg.EnviarHtmlMessage(user.Email, string.Format("{0} - Suprimento Enviado", this.Serie), mensagem, "Envio de Suprimentos", true);
            }
        }

    }

    public bool Fechar()
    {
        bool result = false;

        string tsqlUpdate = string.Format("update reqSuprimentos set status = 'Enviado', dataAtendimento = GETDATE() where idreqSuprimento = {0}", this.IdreqSuprimento);
        if (DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate))
        {
            result = true;
        }
        return result;

    }

    public bool Fechar(string atendidoPor)
    {
        bool result = false;

        string tsqlUpdate = string.Format("update reqSuprimentos set status = 'Enviado', atendidoPor = '{1}', dataAtendimento = GETDATE() where idreqSuprimento = {0}", this.IdreqSuprimento, atendidoPor);
        if (DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate))
        {
            result = true;
        }
        return result;
    }

    public static reqSuprimentos Get(int id)
    {
        reqSuprimentos req = new reqSuprimentos();
        string tsql = string.Format(@"select serie,valorAtual,durabilidadeEstimada,solicitante,usuario,emailUsuario,telefoneUsuario,endereco,
bairro,cep,uf,cidade,dataSolicitacao,codUsd,obs,suprimento,contador
from reqSuprimentos where idreqSuprimento = {0}", id.ToString());
        DataTable dtReq = DAO.RetornaDt(DAO.connString(), tsql);
        if (dtReq.Rows.Count > 0)
        {
            foreach (DataRow rReq in dtReq.Rows)
            {
                req.IdreqSuprimento = id.ToString();
                req.Serie = rReq["serie"].ToString();
                req.ValorAtual = rReq["valorAtual"].ToString();
                req.DurabilidadeEstimada = rReq["durabilidadeEstimada"].ToString();
                req.Solicitante = rReq["solicitante"].ToString();
                req.Usuario = rReq["usuario"].ToString();
                req.EmailUsuario = rReq["emailUsuario"].ToString();
                req.TelefoneUsuario = rReq["telefoneUsuario"].ToString();
                req.Endereco = rReq["endereco"].ToString();
                req.Bairro = rReq["bairro"].ToString();
                req.Cep = rReq["cep"].ToString();
                req.Cidade = rReq["cidade"].ToString();
                req.DataSolicitacao = rReq["dataSolicitacao"].ToString();
                req.ReqUSD = rReq["codUsd"].ToString();
                req.Obs = rReq["obs"].ToString();
                req.Suprimento = rReq["suprimento"].ToString();
                req.Contador = rReq["contador"].ToString();
                req.Uf = rReq["uf"].ToString();
            }



        }

        return req;
    }

    public static void ConfirmarEntrega(int idReqSupr, string name)
    {
        string tsqlUpdate = string.Format("update enviosSuprimentos set statusEntrega = 'Objeto entregue - confirmado por {1}' where idEnvio = {0};", idReqSupr.ToString(), name);
        DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
    }

    public static void Priorizar(string text)
    {
        string tsqlUpdate = string.Format("update reqSuprimentos set priorizar = 1 where idreqSuprimento = {0};", text);
        DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
    }

    public static void Cancelar(string idRequisicao, string usuario, string assunto, string suprimento)
    {
        string tsqlUpdate = string.Format("update reqSuprimentos set status = 'Cancelado por {1}' where idreqSuprimento = {0};", idRequisicao, usuario.ToUpper());
        if (DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate))
        {
            Usuarios user = new Usuarios(usuario);
            EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();
            string mensagem = string.Format("A solicitação de {2} de número {0} foi cancelada pelo usuário {1}", idRequisicao, usuario.ToUpper(), suprimento);
            msg.EnviarHtmlMessageCopia("logistica.for@csfdigital.com.br", user.Email, assunto, mensagem, "Suprimentos", true);
        }
    }

    public static void Priorizar(string idRequisicao, string usuario, string assunto, string suprimento)
    {
        string tsqlUpdate = string.Format("update reqSuprimentos set priorizar = 1 where idreqSuprimento = {0};", idRequisicao);
        if (DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate))
        {
            Usuarios user = new Usuarios(usuario);
            EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();
            string mensagem = string.Format("A solicitação de {2} de número {0} foi priorizada pelo usuário {1}", idRequisicao, usuario.ToUpper(), suprimento);
            msg.EnviarHtmlMessageCopia("logistica.for@csfdigital.com.br", user.Email, assunto, mensagem, "Suprimentos", true);

        }
    }

    public bool Atualizar()
    {
        bool result = false;
        string tsqlUpdate = string.Format("UPDATE dbo.reqSuprimentos SET valorAtual = {0},durabilidadeEstimada = {1},falhaAnterior = {2},solicitante = '{3}',usuario = '{4}',emailUsuario = '{5}',telefoneUsuario = '{6}',endereco = '{7}',bairro = '{8}',cep = '{9}',uf = '{10}',cidade = '{11}',codUsd = '{12}',obs = '{13}',suprimento = '{14}',contador = '{15}' WHERE idreqSuprimento = {16}",
            this.ValorAtual, this.DurabilidadeEstimada, this.FalhaAnterior, this.Solicitante, this.Usuario, this.EmailUsuario, this.TelefoneUsuario, this.Endereco, this.Bairro, this.Cep, this.Uf, this.Cidade, this.ReqUSD, this.Obs, this.Suprimento, this.Contador, this.IdreqSuprimento);
        result = DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
        return result;
    }

}