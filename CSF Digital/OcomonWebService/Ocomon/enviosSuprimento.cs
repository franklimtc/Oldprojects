using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Ocomon
{
    public class enviosSuprimento
    {

        #region Campos
        private string _serie;
        private string _dtEnvio;
        private string _qtd;
        private string _tpSuprimento;
        private string _tpEnvio;
        private string _origem;
        private string _postagem;
        private string _etiqueta;
        private string _partNumber;
        private string _Interno;
        private string _cliente;

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

        public string Qtd
        {
            get
            {
                return _qtd;
            }

            set
            {
                _qtd = value;
            }
        }

        public string TpSuprimento
        {
            get
            {
                return _tpSuprimento;
            }

            set
            {
                _tpSuprimento = value;
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

        public string Origem
        {
            get
            {
                return _origem;
            }

            set
            {
                _origem = value;
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

        public string Etiqueta
        {
            get
            {
                return _etiqueta;
            }

            set
            {
                _etiqueta = value;
            }
        }

        public string PartNumber
        {
            get
            {
                return _partNumber;
            }

            set
            {
                _partNumber = value;
            }
        }

        public string Interno
        {
            get
            {
                return _Interno;
            }

            set
            {
                _Interno = value;
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
        #endregion

        public static List<enviosSuprimento> ListarPendentes()
        {
            List<enviosSuprimento> lista = new List<enviosSuprimento>();

            DataTable dtEnviosSuprimentos = dao.retornaDt("select serie,dtEnvio,qtd,tpSuprimento,tpEnvio,origem,postagem,etiqueta,partNumber,Interno,cliente,inserida from vw_listaEnvios where inserida = 0");

            foreach (DataRow envio in dtEnviosSuprimentos.Rows)
            {
                enviosSuprimento e = new enviosSuprimento();

                e.Serie = envio["serie"].ToString();
                e.DtEnvio = envio["dtEnvio"].ToString();
                e.Qtd = envio["qtd"].ToString();
                e.TpSuprimento = envio["tpSuprimento"].ToString();
                e.TpEnvio = envio["tpEnvio"].ToString();
                e.Origem = envio["origem"].ToString();
                e.Postagem = envio["postagem"].ToString();
                e.Etiqueta = envio["etiqueta"].ToString();
                e.PartNumber = envio["partNumber"].ToString();
                e.Cliente = envio["cliente"].ToString();

                lista.Add(e);
            }

            return lista;
        }

        public static bool Confirmar(string postagem)
        {
            bool result = false;
            string tsqlUpdate = string.Format("update enviosSuprimentos set inserida = 1 where postagem = '{0}';", postagem);
            result = dao.ExecuteNonQuery(tsqlUpdate);
            return result;
        }
    }
}