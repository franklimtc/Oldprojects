using System;
using System.Collections.Generic;
using System.Data;
using dnaPrint.DAO;
using System.Linq;

namespace dnaPrint.Base
{
    public class Unidade : dnaPrint.Base.IBase<Unidade>
    {
        #region Campos
        public enum statusUnidade { Inativo = 0, Ativo = 1 }

        public int idLocalidade { get; set; }
        public int idCidade { get; set; }
        public string descricao { get; set; }
        public string endereco { get; set; }
        public string telefone { get; set; }
        public string contato { get; set; }
        public string uf { get; set; }
        public string cidade { get; set; }
        public string email { get; set; }

        public statusUnidade status { get; set; }

        #endregion

        public Unidade()
        {

        }

        public Unidade(int _idLocal, int _idCidade, string _unidade, string _endereco, string _telefone, string _contato, statusUnidade _status)
        {
            this.idLocalidade = _idLocal;
            this.idCidade = _idCidade;
            this.descricao = _unidade;
            this.endereco = _endereco;
            this.telefone = _telefone;
            this.contato = _contato;
            this.status = _status;
        }

        public static List<Unidade> Listar(string connString, dnaPrint.DAO.Operacoes.tipo TipoDB, int _idCidade)
        {
            List<Unidade> Lista = new List<Unidade>();

            DataTable dt = new DAO.Operacoes(connString, TipoDB).ReturnDt($"select a.idLocalidade,c.uf, b.cidade, a.idCidade, a.descricao, a.endereco, a.telefone, a.contato, a.status, a.email from CadastroUnidade as a left join CadastroCidade as b on a.idCidade = b.idCidade left join cadastroEstado as c on b.idEstado = c.idEstado where a.idCidade = {_idCidade} and status = '1'");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _und in dt.Rows)
                {
                    Unidade u = new Unidade(
                        int.Parse(_und["idLocalidade"].ToString())
                        , int.Parse(_und["idCidade"].ToString())
                        , _und["descricao"].ToString()
                        , _und["endereco"].ToString()
                        , _und["telefone"].ToString()
                        , _und["contato"].ToString()
                        , DefinirStatus(_und["status"].ToString())
                        );
                    u.uf = _und["uf"].ToString();
                    u.cidade = _und["cidade"].ToString();
                    u.email = _und["email"].ToString();
                    Lista.Add(u);
                }
            }

            return Lista.OrderBy(x => x.descricao).ToList();
        }

        private static statusUnidade DefinirStatus(string status)
        {
            if (status == "0")
                return statusUnidade.Inativo;
            else
                return statusUnidade.Ativo;
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;
            bool statusInt = true;

            if (this.status == statusUnidade.Inativo)
                statusInt = false;

            string tsqlUpdate = $"update cadastroUnidade set descricao = @descricao, endereco = @endereco, telefone = @telefone, contato = @contato, email = @email where idLocalidade = @idLocalidade;";

            List<object[]> listaParametros = new List<object[]>();
            listaParametros.Add(new object[] { "@descricao", this.descricao});
            listaParametros.Add(new object[] { "@endereco", this.endereco});
            listaParametros.Add(new object[] { "@telefone", this.telefone});
            listaParametros.Add(new object[] { "@contato", this.contato});
            listaParametros.Add(new object[] { "@email", this.email});
            listaParametros.Add(new object[] { "@idLocalidade", this.idLocalidade});

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsqlUpdate, listaParametros);

            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;
            string tsqlInsert = @"insert into CadastroUnidade(idCidade, descricao, endereco, telefone, contato, status, email) values(@idCidade, @descricao, @endereco, @telefone, @contato, '1', @email);";
            List<string[]> parametros = new List<string[]>();
            parametros.Add(new string[] { "@idCidade", this.idCidade.ToString() });
            parametros.Add(new string[] { "@descricao", this.descricao });
            parametros.Add(new string[] { "@endereco", this.endereco});
            parametros.Add(new string[] { "@telefone", this.telefone});
            parametros.Add(new string[] { "@contato", this.contato});
            parametros.Add(new string[] { "@email", this.email});

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsqlInsert, parametros);

            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;
            string tsqlUpdate = $"update CadastroUnidade set status = '0' where idLocalidade = {this.idLocalidade};";

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsqlUpdate);

            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public Unidade ListarByID(string connString, Operacoes.tipo TipoDB, int idUnidade)
        {
            //List<Unidade> Lista = new List<Unidade>();
            
            DataTable dt = new DAO.Operacoes(connString, TipoDB).ReturnDt($"select a.idLocalidade,c.uf, b.cidade, a.idCidade, a.descricao, a.endereco, a.telefone, a.contato, a.status, a.email from CadastroUnidade as a left join CadastroCidade as b on a.idCidade = b.idCidade left join cadastroEstado as c on b.idEstado = c.idEstado where a.idLocalidade = {idUnidade} and status = '1'");
            Unidade u = new Unidade();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _und in dt.Rows)
                {
                    u = new Unidade(
                        int.Parse(_und["idLocalidade"].ToString())
                        , int.Parse(_und["idCidade"].ToString())
                        , _und["descricao"].ToString()
                        , _und["endereco"].ToString()
                        , _und["telefone"].ToString()
                        , _und["contato"].ToString()
                        , DefinirStatus(_und["status"].ToString())
                        );
                    u.uf = _und["uf"].ToString();
                    u.cidade = _und["cidade"].ToString();
                    u.email = _und["email"].ToString();
                }
            }

            return u;
        }
    }
}
