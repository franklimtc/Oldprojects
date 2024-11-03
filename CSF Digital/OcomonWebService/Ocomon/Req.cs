using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ocomon
{
    public class Req
    {
        #region Campos
        private string _numero;

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        private string _local;

        public string Local
        {
            get { return _local; }
            set { _local = value; }
        }
        private string _serie;

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        private string _descricao;

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        private string _toner;

        public string Toner
        {
            get { return _toner; }
            set { _toner = value; }
        }
        private string _fotoreceptor;

        public string Fotoreceptor
        {
            get { return _fotoreceptor; }
            set { _fotoreceptor = value; }
        }
        private string _dataAbertura;

        public string DataAbertura
        {
            get { return _dataAbertura; }
            set { _dataAbertura = value; }
        }
        private string _qtdDias;

        public string QtdDias
        {
            get { return _qtdDias; }
            set { _qtdDias = value; }
        }

        private string _tpEnvio;

        public string TpEnvio
        {
            get { return _tpEnvio; }
            set { _tpEnvio = value; }
        }

        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        #endregion

        public static Req retornaReq(string numero, string local, string serie, string descricao, string dataAbertura, string qtdDias, string nome)
        {
            Req r = new Req();
            r.Numero = numero;
            r.Local = local;
            r.Serie = serie;
            r.Descricao = descricao;
            r.DataAbertura = dataAbertura;
            r.QtdDias = qtdDias;
            r.Nome = nome;
           
            if (r.Descricao.ToUpper().Contains("TONER") || r.Descricao.ToUpper().Contains("TONNER"))
            {
                r.Toner = "Sim";
            }

            if (r.Descricao.ToUpper().Contains("CILINDRO") || r.Descricao.ToUpper().Contains("FOTO"))
            {
                r.Fotoreceptor = "Sim";
            }


            if (descricao.ToUpper().Contains("SEDEX"))
            {
                r.TpEnvio = "SEDEX";
            }
            else
            {
                if (descricao.ToUpper().Contains("PAC"))
                {
                    r.TpEnvio = "PAC";
                }
                else
                {
                    if (descricao.ToUpper().Contains("MOTOBOY"))
                    {
                        r.TpEnvio = "MOTOBOY";
                    }
                    else
                    {
                        r.TpEnvio = "INDEFINIDO";
                    }
                }
            }
            return r;
        }

        internal static bool Fechar(string numero, string etiqueta, string postagem, string usuario, string serie)
        {
            bool result = false;
            string descricao = string.Format("Suprimento enviado com postagem {0} e etiqueta {1} por {2}", postagem, etiqueta, usuario);
            string sqlInsert = string.Format("insert into assentamentos(ocorrencia, assentamento, data, responsavel, asset_privated, tipo_assentamento) values({0}, '{1}', now(),1,0,0);", numero, descricao);
            string sqlUpdate = string.Format("update ocorrencias set status = 4, data_fechamento = now(), data_atendimento = Now() where numero = {0};", numero);

            if (dao.Execute("ocomonWeb", sqlInsert) && dao.Execute("ocomon", sqlUpdate))
            {
                result = true;
            }

            return result;
        }

        internal static void Informar(string numero, string usuario, string postagem, string serie)
        {
            string ReqUSD = dao.RetornaValor("ocomonWeb", string.Format("select contato from ocorrencias where  numero = {0};", numero));
            string email = dao.RetornaValor("ocomonWeb", string.Format("select email from usuarios as a where user_id in (select operador from ocorrencias where numero = {0});", numero));

            string assunto = string.Format("{0} - {1} - Suprimento enviado!", ReqUSD, serie);
            string mensagem = string.Format("Suprimento enviado com postagem {0} para atendimento da demanda {1} do equipamento de série {2}.", postagem, ReqUSD, serie);
            SMTP.Enviar(email, assunto, mensagem);
        }
    }
}