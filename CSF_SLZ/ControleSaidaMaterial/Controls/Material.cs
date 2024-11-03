using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class Material
    {
        #region Campos

        private string _idMaterial;
        private string _descricao;
        private string _modelo;
        private string _partNumber;
        private string _dtCadastro;
        private string _dtAtualizacao;
        private string _status;
        private string _operador;

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

        public string Descricao
        {
            get
            {
                return _descricao;
            }

            set
            {
                _descricao = value;
            }
        }

        public string Modelo
        {
            get
            {
                return _modelo;
            }

            set
            {
                _modelo = value;
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

        public Material()
        { }
        public Material(int idMaterial)
        {
            string tsql = string.Format(@"SELECT idMaterial,descricao,modelo,partNumber,dtCadastro,dtAtualizacao,status,operador FROM Materiais where idMaterial = {0}", idMaterial.ToString());

            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow m in dt.Rows)
                {
                    this.IdMaterial = m["idMaterial"].ToString();
                    this.Descricao = m["descricao"].ToString();
                    this.Modelo = m["modelo"].ToString();
                    this.PartNumber = m["partNumber"].ToString();
                    this.DtCadastro = m["dtCadastro"].ToString();
                    this.DtAtualizacao = m["dtAtualizacao"].ToString();
                    this.Status = m["status"].ToString();
                    this.Operador = m["operador"].ToString();
                }
            }
        }

        public Material(string partNumber)
        {
            string tsql = string.Format(@"SELECT idMaterial,descricao,modelo,partNumber,dtCadastro,dtAtualizacao,status,operador FROM Materiais where LOWER(partNumber) = '{0}'",partNumber.ToLower());

            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow m in dt.Rows)
                {
                    this.IdMaterial = m["idMaterial"].ToString();
                    this.Descricao = m["descricao"].ToString();
                    this.Modelo = m["modelo"].ToString();
                    this.PartNumber = m["partNumber"].ToString();
                    this.DtCadastro = m["dtCadastro"].ToString();
                    this.DtAtualizacao = m["dtAtualizacao"].ToString();
                    this.Status = m["status"].ToString();
                    this.Operador = m["operador"].ToString();
                }
            }
        }

        public static List<Material> Listar()
        {
            List<Material> lista = new List<Material>();

            string tsql = string.Format(@"SELECT idMaterial,descricao,modelo,partNumber,dtCadastro,dtAtualizacao,status,operador FROM Materiais");

            DataTable dt = DAO.retornadt(tsql);
            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow m in dt.Rows)
                {
                    Material mat = new Material();
                    mat.IdMaterial = m["idMaterial"].ToString();
                    mat.Descricao = m["descricao"].ToString();
                    mat.Modelo = m["modelo"].ToString();
                    mat.PartNumber = m["partNumber"].ToString();
                    mat.DtCadastro = m["dtCadastro"].ToString();
                    mat.DtAtualizacao = m["dtAtualizacao"].ToString();
                    mat.Status = m["status"].ToString();
                    mat.Operador = m["operador"].ToString();

                    lista.Add(mat);
                }
            }

            return lista;
        }
        public bool Adicionar()
        {
            bool result = false;

            if (this.Descricao != "" && this.Modelo != "" && this.PartNumber != "" && this.Operador != "")
            {
                string tsqlInsert = string.Format("insert into Materiais(descricao, modelo, partNumber, operador) VALUES('{0}','{1}','{2}','{3}');",
                    this.Descricao, this.Modelo, this.PartNumber, this.Operador);
                if (DAO.ExecuteNonQuery(tsqlInsert) > 0)
                    result = true;
            }

            return result;
        }

        public bool Atualizar()
        {
            bool result = false;
            if (this.Descricao != "" && this.Modelo != "" && this.PartNumber != "" && this.Operador != "")
            {
                string tsqlUpdate = string.Format("UPDATE Materiais set descricao = '{0}', modelo = '{1}', partNumber = '{2}', operador = '{3}', dtAtualizacao = GETDATE() WHERE idMaterial = {4}",
               this.Descricao, this.Modelo, this.PartNumber, this.Operador, this.IdMaterial);
                if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                    result = true;
            }
            return result;
        }

        public bool Desativar()
        {
            bool result = false;
            string tsqlUpdate = string.Format("UPDATE Materiais SET STATUS = 0, operador = '{0}', dtAtualizacao = GETDATE() WHERE idMaterial = '{1}';",
              this.Operador, this.IdMaterial);
            if (DAO.ExecuteNonQuery(tsqlUpdate) > 0)
                result = true;
            return result;
        }
       
    }
}