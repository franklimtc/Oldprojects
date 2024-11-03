using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class Equipamento
    {
        #region Campos

        private string _idEquipamento;
        private string _idCliente;
        private string _cliente;
        private string _serie;
        private string _dtCadastro;
        private string _dtAtualizacao;
        private string _status;
        private string _operador;

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

        public Equipamento()
        {

        }

        public Equipamento(int idEquipamento)
        {
            string tsql = string.Format(@"select idEquipamento 'ID', b.razaoSocial 'Cliente', Serie, a.Operador,a.Status, a.dtCadastro, a.dtAtualizacao 
            from Equipamentos as a left join Clientes as b on a.idCliente = b.idCliente where idEquipamento = {0}"
              , idEquipamento.ToString());
            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow c in dt.Rows)
                {
                    this.IdEquipamento = c["ID"].ToString();
                    this.Cliente = c["Cliente"].ToString();
                    this.Serie = c["Serie"].ToString();
                    this.Operador = c["Operador"].ToString();
                    this.Status = c["Status"].ToString();
                    this.DtCadastro = c["dtCadastro"].ToString();
                    this.DtAtualizacao = c["dtAtualizacao"].ToString();
                }
            }
        }

        public static List<Equipamento> Listar()
        {
            List<Equipamento> lista = new List<Equipamento>();

            string tsql = string.Format(@"select idEquipamento 'ID', b.razaoSocial 'Cliente', Serie, a.Operador,a.Status, a.dtCadastro, a.dtAtualizacao 
            from Equipamentos as a left join Clientes as b on a.idCliente = b.idCliente");
            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow c in dt.Rows)
                {
                    Equipamento eqp = new Equipamento();

                    eqp.IdEquipamento = c["ID"].ToString();
                    eqp.Cliente = c["Cliente"].ToString();
                    eqp.Serie = c["Serie"].ToString();
                    eqp.Operador = c["Operador"].ToString();
                    eqp.Status = c["Status"].ToString();
                    eqp.DtCadastro = c["dtCadastro"].ToString();
                    eqp.DtAtualizacao = c["dtAtualizacao"].ToString();
                    lista.Add(eqp);
                }
            }

            return lista;
        }

        public bool Adicionar()
        {
            bool result = false;

            if (this.IdCliente != "" && this.Serie != "" && this.Operador != "")
            {
                string tsqlInsert = string.Format("INSERT INTO Equipamentos(idCliente, serie, operador) VALUES({0}, '{1}','{2}') ",
                    this.IdCliente, this.Serie, this.Operador);
                if (DAO.ExecuteNonQuery(tsqlInsert) > 0)
                    result = true;
            }

            return result;
        }

        public bool Atualizar()
        {
            bool result = false;
            string tsqlUpdate = string.Format("UPDATE Equipamentos SET serie = '{0}', STATUS = '{1}', operador = '{2}', dtAtualizacao = GETDATE() WHERE idEquipamento = '{3}';",
               this.Serie, this.Status, this.Operador, this.IdEquipamento);
            if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                result = true;
            return result;
        }

        public bool Desativar()
        {
            bool result = false;
            string tsqlUpdate = string.Format("UPDATE Equipamentos SET STATUS = 0, operador = '{0}', dtAtualizacao = GETDATE() WHERE idEquipamento = '{1}';",
              this.Operador, this.IdEquipamento);
            if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                result = true;
            return result;
        }
    }
}
