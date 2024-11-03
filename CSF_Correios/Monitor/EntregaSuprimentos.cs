using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Monitor
{
    class EntregaSuprimentos
    {
        #region Campos

        private string _serie;
        private string _postagem;
        private string cep;
        private string _tipoEnvio;

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

        public string Cep
        {
            get
            {
                return cep;
            }

            set
            {
                cep = value;
            }
        }

        public string TipoEnvio
        {
            get
            {
                return _tipoEnvio;
            }

            set
            {
                _tipoEnvio = value;
            }
        }

        #endregion

        public EntregaSuprimentos(string serie, string postagem, string cep, string tipoEnvio)
        {
            this.Serie = serie;
            this.Postagem = postagem;
            this.TipoEnvio = TipoEnvio;
        }

        public static List<EntregaSuprimentos> listar()
        {
            List<EntregaSuprimentos> lista = new List<EntregaSuprimentos>();

            //string tsql = "select a.serie, a.postagem, b.cep, a.tpenvio from enviossuprimentos as a left join equipamentos as b on a.serie = b.serie where a.dtEntrega is not null and a.prazoEntrega is null and b.cep is not null";
            string tsql = @"select a.serie, a.postagem, a.tpenvio 
from enviosSuprimentos as a
where a.tpEnvio in ('SEDEX', 'PAC')
AND a.statusEntrega not in ('Entrega Efetuada','Objeto entregue ao destinatário')
AND a.statusEntrega not like '%Objeto entregue%'";
            DataTable dtLista = DAO.retornaDt(tsql);

            foreach (DataRow entrega in dtLista.Rows)
            {
                lista.Add(new EntregaSuprimentos(entrega["serie"].ToString(), entrega["postagem"].ToString(), null, entrega["tpenvio"].ToString()));
            }

            return lista;
        }

        internal void AtualizarPrazo(int prazo1)
        {
            string tsql = string.Format("update enviosSuprimentos set prazoEntrega = {0} where postagem = '{1}'", prazo1.ToString(), this.Postagem);
            DAO.Execute(tsql);
        }
    }
}
