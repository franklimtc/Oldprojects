using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Monitor
{
    class PostagensReversas
    {
        #region Campos
        private string _idPostagemReversa;
        private string _cepOrigem;
        private string _cepDestino;
        private string _postagem;
        private string _operador;
        private string _data;
        private string _dtEnvio;
        private string _dtEntrega;
        private string _prazoEntrega;
        private string _entregueEm;
        private string _statusentrega;
        private string _verificado;

        public string IdPostagemReversa
        {
            get
            {
                return _idPostagemReversa;
            }

            set
            {
                _idPostagemReversa = value;
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

        public string Data
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

        public string EntregueEm
        {
            get
            {
                return _entregueEm;
            }

            set
            {
                _entregueEm = value;
            }
        }

        public string Statusentrega
        {
            get
            {
                return _statusentrega;
            }

            set
            {
                _statusentrega = value;
            }
        }

        public string Verificado
        {
            get
            {
                return _verificado;
            }

            set
            {
                _verificado = value;
            }
        }
        #endregion

        public static List<PostagensReversas> Listar()
        {
            string tsql = "select idPostagemReversa,cepOrigem,cepDestino,postagem,operador,data,dtEnvio,dtEntrega,prazoEntrega,entregueEm,statusentrega,verificado from postagensReversas where verificado = 0";
            List<PostagensReversas> Lista = new List<PostagensReversas>();

            DataTable dtReqs = DAO.retornaDt(tsql);
            if (dtReqs.Rows.Count > 0)
            {
                foreach (DataRow oReq in dtReqs.Rows)
                {
                    PostagensReversas postagem = new PostagensReversas();
                    postagem.IdPostagemReversa = oReq["idPostagemReversa"].ToString();
                    postagem.CepOrigem = oReq["cepOrigem"].ToString();
                    postagem.CepDestino = oReq["cepDestino"].ToString();
                    postagem.Postagem = oReq["postagem"].ToString();
                    postagem.Operador = oReq["operador"].ToString();
                    postagem.Data = oReq["data"].ToString();
                    postagem.DtEnvio = oReq["dtEnvio"].ToString();
                    postagem.DtEntrega = oReq["dtEntrega"].ToString();
                    postagem.PrazoEntrega = oReq["prazoEntrega"].ToString();
                    postagem.EntregueEm = oReq["entregueEm"].ToString();
                    postagem.Statusentrega = oReq["statusentrega"].ToString();
                    Lista.Add(postagem);

                }
            }

            return Lista;
        }

        internal bool Entregue(string data)
        {
            bool result = false;
            string tsqlUpdate = string.Format("update postagensReversas set dtEntrega = '{0}'  where idPostagemReversa = {1}", data, this.IdPostagemReversa);
            result = DAO.Execute(tsqlUpdate);
            return result;
        }

        public bool AtualizarStatus(string descricao)
        {
            bool result = false;
            string tsqlUpdate = string.Format("update postagensReversas set statusentrega = '{0}' where idPostagemReversa = {1};", descricao, this.IdPostagemReversa);
            result = DAO.Execute(tsqlUpdate);
            return result;
        }

        internal static bool Entregue(string data, string postagem, string status)
        {
            bool result = false;
            string tsqlUpdate = string.Format("update postagensReversas set dtEntrega = '{0}',statusentrega='{1}'   where postagem = '{2}'", data, status, postagem);
            result = DAO.Execute(tsqlUpdate);
            return result;
        }

        internal static List<string> ListarPostagensEntregues()
        {
            throw new NotImplementedException();
        }

        internal void CalcularPrazo()
        {
            CSF_Correios.CorreiosPrazo.CalcPrecoPrazoWS prazo = new CSF_Correios.CorreiosPrazo.CalcPrecoPrazoWS();
            CSF_Correios.CorreiosPrazo.cResultado result = prazo.CalcPrazo("41076", this.CepOrigem, this.CepDestino);
            int qtdDias = 0;

            try {
                qtdDias = int.Parse(result.Servicos[0].PrazoEntrega);
            }
            catch { }
            if (qtdDias > 0)
            {
                string tsqlupdate = string.Format("update postagensReversas set prazoEntrega = {0}  where idPostagemReversa = {1}", qtdDias, this.IdPostagemReversa);
                DAO.Execute(tsqlupdate);
                this.PrazoEntrega = qtdDias.ToString();
            }
        }

        public void Checado()
        {
            string tsqlupdate = string.Format("update postagensReversas set verificado = 1  where idPostagemReversa = {0}", this.IdPostagemReversa);
            DAO.Execute(tsqlupdate);
        }
    }
}
