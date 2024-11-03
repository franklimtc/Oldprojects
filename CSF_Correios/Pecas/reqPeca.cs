using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Pecas
{
    public class reqPeca
    {
        #region Campos

        private string _idreqPeca;
        private string _reqUsd;
        private string _solicitante;
        private string _postagem;
        private string _serie;
        private string _uf;
        private string _cidade;

        public string IdreqPeca
        {
            get
            {
                return _idreqPeca;
            }

            set
            {
                _idreqPeca = value;
            }
        }

        public string ReqUsd
        {
            get
            {
                return _reqUsd;
            }

            set
            {
                _reqUsd = value;
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
        #endregion

        public static List<reqPeca> listar()
        {
            List<reqPeca> Lista = new List<reqPeca>();

            string tsql = @"select a.idreqPeca, a.reqUsd, a.solicitante, b.postagem, c.serie, c.uf, c.cidade, a.statusEntrega
from reqPecas as a
left join atulPecas as b on a.idreqPeca = b.idreqPeca and (b.postagem is not null and b.postagem <> '')
left join equipamentos as c on a.serieEqpto = c.serie
where statusentrega not like  '%Objeto entregue%' and postagem is not null";

            DataTable dtReqs = DAO.retornaDt(tsql);
            if (dtReqs.Rows.Count > 0)
            {
                foreach (DataRow oReq in dtReqs.Rows)
                {
                    reqPeca req = new reqPeca();
                    req.Uf = oReq["uf"].ToString();
                    req.Cidade = oReq["cidade"].ToString();
                    req.IdreqPeca = oReq["idreqPeca"].ToString();
                    req.ReqUsd = oReq["reqUsd"].ToString();
                    req.Solicitante = oReq["solicitante"].ToString();
                    req.Postagem = oReq["postagem"].ToString();
                    req.Serie = oReq["serie"].ToString();
                    Lista.Add(req);
                }
            }

            return Lista;
        }

        internal bool InformarUsuario(string descricao)
        {
            Email.Email msg = new Email.Email();
            string assunto = null;
            if (this.ReqUsd != null)
            {
                assunto = string.Format("{0} - {1} - {2} - {3} - Peça Entregue!", this.ReqUsd, this.Serie, this.Uf, this.Cidade);
            }
            else
            {
                assunto = string.Format("{0} - Peça Entregue!", this.Serie);
            }

            string emailUser = EmailOperador(this.Solicitante);

            string mensagem = string.Format("Confimado a entrega do objeto de postagem {0} pelos correios. Favor alimentar as demandas!", this.Postagem);
            bool result = false;
            if (emailUser != "" && emailUser != null)
            {
                result = msg.EnviarHtmlMessage(emailUser, assunto, mensagem, "Monitor de Postagens", false);
            }
            return result;
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

        internal bool Entregue(string data)
        {
            bool result = false;
            string tsqlUpdate = string.Format("update atulPecas set dtEntrega = '{0}' where idreqPeca = {1};", data, this.IdreqPeca);
            result = DAO.Execute(tsqlUpdate);
            return result;
        }

        public bool AtualizarStatus(string descricao)
        {
            bool result = false;
            string tsqlUpdate = string.Format("update reqPecas set statusentrega = '{0}' where idreqPeca = {1};", descricao, this.IdreqPeca);
            result = DAO.Execute(tsqlUpdate);
            return result;
        }
    }
}
