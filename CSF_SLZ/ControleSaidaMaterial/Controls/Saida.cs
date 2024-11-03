using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class Saida
    {
        #region Campos

        private string _idSaida;
        private string _idCliente;
        private string _idMaterial;
        private string _idSolicitante;
        private string _idEquipamento;
        private string _qtd;
        private string _tipoOperacao;
        private string _dtCadastro;
        private string _dtAtualizacao;
        private string _operador;
        private string _notaFiscal;

        public string IdSaida
        {
            get
            {
                return _idSaida;
            }

            set
            {
                _idSaida = value;
            }
        }

        public string IdCliente
        {
            get
            {
                return _idCliente;
            }

            set
            {
                _idCliente = value;
            }
        }

        public string IdMaterial
        {
            get
            {
                return _idMaterial;
            }

            set
            {
                _idMaterial = value;
            }
        }

        public string IdSolicitante
        {
            get
            {
                return _idSolicitante;
            }

            set
            {
                _idSolicitante = value;
            }
        }

        public string IdEquipamento
        {
            get
            {
                return _idEquipamento;
            }

            set
            {
                _idEquipamento = value;
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

        public string TipoOperacao
        {
            get
            {
                return _tipoOperacao;
            }

            set
            {
                _tipoOperacao = value;
            }
        }

      

        public string DtCadastro
        {
            get
            {
                return _dtCadastro;
            }

            set
            {
                _dtCadastro = value;
            }
        }

        public string DtAtualizacao
        {
            get
            {
                return _dtAtualizacao;
            }

            set
            {
                _dtAtualizacao = value;
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

        public string NotaFiscal
        {
            get
            {
                return _notaFiscal;
            }

            set
            {
                _notaFiscal = value;
            }
        }

        #endregion

        public Saida()
        {

        }

        public static List<Saida> Listar(bool PendenciasIlux)
        {
            List<Saida> lista = new List<Saida>();


            string tsql = null;
            if(PendenciasIlux)
                tsql = string.Format(@"select idSaida,idCliente,idMaterial,idSolicitante,idEquipamento,notaFiscal,qtd,tipoOperacao,dtCadastro,dtAtualizacao,operador from saidas where pubIlux = 0");
            else
                tsql = string.Format(@"select idSaida,idCliente,idMaterial,idSolicitante,idEquipamento,notaFiscal,qtd,tipoOperacao,dtCadastro,dtAtualizacao,operador from saidas");


            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow s in dt.Rows)
                {
                    Saida sad = new Saida();

                    sad.IdSaida = s["idSaida"].ToString();
                    sad.IdCliente = s["idCliente"].ToString();
                    sad.IdMaterial = s["idMaterial"].ToString();
                    sad.IdSolicitante = s["idSolicitante"].ToString();
                    sad.IdEquipamento = s["idEquipamento"].ToString();
                    sad.NotaFiscal = s["notaFiscal"].ToString();
                    sad.Qtd = s["qtd"].ToString();
                    sad.TipoOperacao = s["tipoOperacao"].ToString();
                    sad.DtCadastro = s["dtCadastro"].ToString();
                    sad.DtAtualizacao = s["dtAtualizacao"].ToString();
                    sad.Operador = s["operador"].ToString();

                    lista.Add(sad);
                }
            }

            return lista;
        }

        public bool Adicionar()
        {
            bool result = false;

            if (this.IdCliente != "" && this.IdMaterial != "" && this.IdSolicitante != "" && this.IdEquipamento != "" && this.NotaFiscal != "" && this.Qtd != "" && this.Operador != "")
            {
                string tsqlInsert = string.Format("INSERT INTO Saidas(idCliente, idMaterial, idSolicitante, idEquipamento, notaFiscal, qtd, tipoOperacao, operador) VALUES({0},{1},{2},{3},{4},{5},'{6}','{7}');",
                    this.IdCliente, this.IdMaterial, this.IdSolicitante, this.IdEquipamento, this.NotaFiscal, this.Qtd, this.TipoOperacao, this.Operador);
                if (DAO.ExecuteNonQuery(tsqlInsert) > 0)
                    result = true;
            }

            return result;
        }

        public bool Atualizar()
        {
            bool result = false;

            if (this.IdCliente != "" && this.IdMaterial != "" && this.IdSolicitante != "" && this.IdEquipamento != "" && this.NotaFiscal != "" && this.Qtd != "" && this.Operador != "" && this.IdSaida != "")
            {
                string tsqlInsert = string.Format("UPDATE controleSaidaMaterial SET idCliente = {0}, idMaterial = {1}, idSolicitante = {2}, idEquipamento = {3}, notaFiscal = {4}, qtd = {5}, tipoOperacao = '{6}', operador = '{7}' WHERE idSaida = {8};",
                    this.IdCliente, this.IdMaterial, this.IdSolicitante, this.IdEquipamento, this.NotaFiscal, this.Qtd, this.TipoOperacao, this.Operador, this.IdSaida);
                if (DAO.ExecuteNonQuery(tsqlInsert) > 0)
                    result = true;
            }

            return result;
        }

        public static bool ConcluirIlux(int idSaida, string operador)
        {
            bool result = false;
            string tsqlUpdate = string.Format("UPDATE SAIDAS set pubIlux = 1,operador = '{0}', dtAtualizacao = GETDATE() WHERE idSaida = {1};", operador, idSaida);
            if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                result = true;
            return result;

        }
        //public bool Desativar()
        //{
        //    bool result = false;

        //    if (this.IdCliente != "" && this.IdMaterial != "" && this.IdSolicitante != "" && this.IdEquipamento != "" && this.NotaFiscal != "" && this.Qtd != "" && this.Operador != "" && this.IdSaida != "")
        //    {
        //        string tsqlInsert = string.Format("UPDATE controleSaidaMaterial SET idCliente = {0}, idMaterial = {1}, idSolicitante = {2}, idEquipamento = {3}, notaFiscal = {4}, qtd = {5}, tipoOperacao = '{6}', operador = '{7}' WHERE idSaida = {8};",
        //            this.IdCliente, this.IdMaterial, this.IdSolicitante, this.IdEquipamento, this.NotaFiscal, this.Qtd, this.TipoOperacao, this.Operador, this.IdSaida);
        //        if (DAO.ExecuteNonQuery(tsqlInsert) > 0)
        //            result = true;
        //    }

        //    return result;
        //}
    }
}
