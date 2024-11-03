using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Monitor_Service_Correios
{
    public class reqSuprimento
    {
        #region Campos
        private string _postagem;
        private string _id;
        private string _solicitante;
        private string _status;
        private string _serie;
        private string _emailSolicitante;
        private string _prazoEntrega;
        private string _cepDestino;
        private string _tpEnvio;
        private string _uf;
        private string _cidade;
        private string _usd;

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



        public string Id
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

        public string EmailSolicitante
        {
            get
            {
                return _emailSolicitante;
            }

            set
            {
                _emailSolicitante = value;
            }
        }

        internal bool Entregue(string data)
        {
            string tsqlUpdate = string.Format("update enviosSuprimentos set dtEntrega = '{0}', statusEntrega = '{2}' where postagem = '{1}';", data, this.Postagem, this.Status);
            return DAO.Execute(tsqlUpdate);
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

        public string Usd
        {
            get
            {
                return _usd;
            }

            set
            {
                _usd = value;
            }
        }
        #endregion
        internal bool InformarUsuario(string descricao)
        {
            Email.Email msg = new Email.Email();
            string reqUsd = this.Usd;
            string assunto = null;
            if (reqUsd != null)
            {
                assunto = string.Format("{0} - {1} - {2} - {3} - {4}!", this.Uf, this.Cidade, reqUsd.ToString(), this.Serie, descricao);
            }
            else
            {
                assunto = string.Format("{0} - {1} - {2} - {3}!", this.Uf, this.Cidade, this.Serie, descricao);
            }

            string emailUser = null;

            if (this.EmailSolicitante != "" && this.EmailSolicitante != null)
            {
                emailUser = this.EmailSolicitante;
            }
            else
            {
                emailUser = "sac@csfdigital.com.br";
            }

            string mensagem = string.Format("Confimado a entrega do objeto de postagem {0} pelos correios. Favor alimentar as demandas!", this.Postagem);
            bool result = false;
            result = msg.EnviarHtmlMessage(emailUser, assunto, mensagem, "Monitor de Postagens", false);
            return result;
        }

        internal void AtualizarPrazos()
        {
            string tsqlUpdate = null;
            if (this.PrazoEntrega == "" || this.PrazoEntrega == null)
            {
                if (this.CepDestino != "" && this.CepDestino != null)
                {
                    if (this.TpEnvio != "" && this.TpEnvio != null)
                    {
                        CSF_Correios.Eventos.servicoCorreios tpservice = CSF_Correios.Eventos.servicoCorreios.sedex;
                        if (this.TpEnvio != "SEDEX")
                        {
                            tpservice = CSF_Correios.Eventos.servicoCorreios.pac;
                        }
                        this.PrazoEntrega = CSF_Correios.Eventos.CalculaPrazo(tpservice, "60175175", this.CepDestino).ToString();
                    }
                }
                tsqlUpdate = string.Format("update enviossuprimentos set  prazoEntrega = {1} where idEnvio = {0};", this.Id, this.PrazoEntrega);
                DAO.Execute(tsqlUpdate);
            }
        }

        private string EmailOperador(string solicitante)
        {
            string tsql = string.Format("select email from ASPNETDB.dbo.vw_aspnet_MembershipUsers where username = '{0}'", solicitante);
            string email = null;
            try
            {
                email = DAO.ExecuteScalar(tsql).ToString();
            }
            catch
            {
                email = "sac@csfdigital.com.br";
            }
            return email;
        }

        //private string reqUSD()
        //{
        //    string tsql = string.Format("select codUsd from reqSuprimentos where idreqSuprimento = {0}", this.Id);
        //    string req = null;
        //    try
        //    {
        //        req = DAO.ExecuteScalar(tsql).ToString();
        //    }
        //    catch
        //    {
        //        Ocomon.Chamados reqOcomon = new Ocomon.Chamados();
        //        Ocomon.Req requisicao = new Ocomon.Req();
        //        try
        //        {
        //            requisicao = reqOcomon.retornaChamadoId(int.Parse(this.Id));

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        req = requisicao.CodUSD;
        //        this.EmailSolicitante = requisicao.EmailUsuario;
        //    }

        //    return req;
        //}

        internal static List<reqSuprimento> Listar()
        {
            List<reqSuprimento> Lista = new List<reqSuprimento>();
            //            string tsql = @"select UPPER(a.postagem) 'Postagem', a.idRequisicao 'id', b.serie, b.solicitante, a.statusEntrega
            //from enviosSuprimentos as a
            //left join reqSuprimentos as b on a.idRequisicao = b.idreqSuprimento
            //where a.idRequisicao is not null and (a.statusEntrega not in ( 'Entrega Efetuada', 'Objeto entregue ao destinatário') or a.statusEntrega is null) and tpenvio in ('PAC','SEDEX' )";
            string tsql = @"select id, série 'serie', postagem 'Postagem', status 'statusEntrega', data, usd, operador 'solicitante', uf, cidade, prazoEntrega, cep, tpEnvio, uf, cidade from vw_postagensSuprimentos";

            DataTable dtReqs = DAO.retornaDt(tsql);
            if (dtReqs.Rows.Count > 0)
            {
                foreach (DataRow oReq in dtReqs.Rows)
                {
                    reqSuprimento req = new reqSuprimento();

                    req.Id = oReq["id"].ToString();
                    req.Serie = oReq["serie"].ToString();
                    req.Solicitante = oReq["solicitante"].ToString();
                    req.Status = oReq["statusEntrega"].ToString();
                    req.Postagem = oReq["Postagem"].ToString();
                    req.PrazoEntrega = oReq["prazoEntrega"].ToString();
                    req.CepDestino = oReq["cep"].ToString();
                    req.TpEnvio = oReq["tpEnvio"].ToString();
                    req.Uf = oReq["uf"].ToString();
                    req.Cidade = oReq["cidade"].ToString();
                    req.Usd = oReq["usd"].ToString();

                    Lista.Add(req);
                }
            }

            foreach (reqSuprimento req in Lista)
            {

            }

            return Lista;
        }

        internal bool AtualizarStatus(string descricao)
        {
            bool result = false;
            string tsqlUpdate = null;

            tsqlUpdate = string.Format("update enviossuprimentos set statusEntrega = '{0}' where idEnvio = {1};", descricao, this.Id);
            result = DAO.Execute(tsqlUpdate);
            return result;
        }
    }
}