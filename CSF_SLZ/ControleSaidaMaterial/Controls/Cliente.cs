using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class Cliente
    {
        #region Campos

        private string _idCliente;
        private string _idClienteIlux;
        private string _razaoSocial;
        private string _dtCadastro;
        private string _dtAtualizacao;
        private string _status;
        private string _operador;

        #region GET/SET
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

        public string IdClienteIlux
        {
            get
            {
                return _idClienteIlux;
            }

            set
            {
                _idClienteIlux = value;
            }
        }

        public string RazaoSocial
        {
            get
            {
                return _razaoSocial;
            }

            set
            {
                _razaoSocial = value;
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

        #endregion

        #endregion

        public Cliente()
        {

        }

        public Cliente(int idCliente)
        {
            string tsql = string.Format("SELECT idCliente, idClienteIlux, razaoSocial, dtCadastro, dtAtualizacao, status, operador FROM Clientes WHERE idCliente = {0}"
                , IdCliente.ToString());
            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow c in dt.Rows)
                {
                    this.IdCliente = c["idCliente"].ToString();
                    this.IdClienteIlux = c["idClienteIlux"].ToString();
                    this.RazaoSocial = c["razaoSocial"].ToString();
                    this.DtCadastro = c["dtCadastro"].ToString();
                    this.DtAtualizacao = c["dtAtualizacao"].ToString();
                    this.Status = c["status"].ToString();
                    this.Operador = c["operador"].ToString();
                }
            }
        }

        public static List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            string tsql = string.Format("SELECT idCliente, idClienteIlux, razaoSocial, dtCadastro, dtAtualizacao, status, operador FROM Clientes;");
            DataTable dt = DAO.retornadt(tsql);

            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow c in dt.Rows)
                {
                    Cliente cCliente = new Cliente();

                    cCliente.IdCliente = c["idCliente"].ToString();
                    cCliente.IdClienteIlux = c["idClienteIlux"].ToString();
                    cCliente.RazaoSocial = c["razaoSocial"].ToString();
                    cCliente.DtCadastro = c["dtCadastro"].ToString();
                    cCliente.DtAtualizacao = c["dtAtualizacao"].ToString();
                    cCliente.Status = c["status"].ToString();
                    cCliente.Operador = c["operador"].ToString();

                    lista.Add(cCliente);
                }
            }

            return lista;
        }

        public bool Adicionar()
        {
            bool result = false;

            if (this.IdClienteIlux != "" && this.RazaoSocial != "" && this.Operador != null)
            {
                string tsqlInsert = string.Format("INSERT INTO Clientes(IidClienteIlux, razaoSocial, operador) VALUES('{0}','{1}','{2}');",
                    this.IdClienteIlux, this.RazaoSocial, this.Operador);
                if (DAO.ExecuteNonQuery(tsqlInsert) > 0)
                    result = true;
            }
            return result;
        }

        public bool Atualizar()
        {
            bool result = false;
            string tsqlUpdate = string.Format("UPDATE Clientes SET idClienteIlux = '{0}', razaoSocial = '{1}', status = '{2}', operador = '{3}', dtAtualizacao = GETDATE() WHERE idCliente = '{4}';",
                this.IdClienteIlux, this.RazaoSocial, this.Status, this.Operador, this.IdCliente);
            if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                result = true;

            return result;
        }

        public bool Desativar()
        {
            bool result = false;
            string tsqlUpdate = string.Format("UPDATE Clientes SET status = 0, operador = '{0}', dtAtualizacao = GETDATE() WHERE idCliente = '{1}';",
                this.Operador, this.IdCliente);
            if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                result = true;

            return result;
        }
    }
}
