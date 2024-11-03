using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class Setor : IBase<Setor>
    {
        #region Campos

        public int idSetor { get; set; }
        public int idLocalidade { get; set; }
        public string Descricao { get; set; }
        public string CentroCusto { get; set; }
        public bool Status { get; set; }
        public int CotaMensal { get; set; }

        #endregion

        public Setor()
        {

        }

        public Setor(int idLocal, string _descricao, string _centroCusto, int _cota)
        {
            this.idLocalidade = idLocal;
            this.Descricao = _descricao;
            this.CentroCusto = _centroCusto;
            this.CotaMensal = _cota;
        }

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"insert into CadastroSetor(idlocalidade, descricao, centroCusto, cotaMensal, status) values(@idlocalidade, @descricao, @centroCusto, @cotaMensal, '1');";
            List<string[]> Parametros = new List<string[]>();
            Parametros.Add(new string[] { "@idLocalidade", this.idLocalidade.ToString() });
            Parametros.Add(new string[] { "@descricao", this.Descricao.ToString() });
            Parametros.Add(new string[] { "@centroCusto", this.CentroCusto.ToString() });
            Parametros.Add(new string[] { "@cotaMensal", this.CotaMensal.ToString() });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"update CadastroSetor set descricao = @descricao, centroCusto = @centroCusto, cotaMensal = @cotaMensal where idSetor = @idSetor;";
            List<string[]> Parametros = new List<string[]>();
            Parametros.Add(new string[] { "@descricao", this.Descricao.ToString() });
            Parametros.Add(new string[] { "@centroCusto", this.CentroCusto.ToString() });
            Parametros.Add(new string[] { "@cotaMensal", this.CotaMensal.ToString() });
            Parametros.Add(new string[] { "@idSetor", this.idSetor.ToString() });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"update CadastroSetor set status = '0' where idSetor = @idSetor;";
            List<string[]> Parametros = new List<string[]>();
            Parametros.Add(new string[] { "@idSetor", this.idSetor.ToString() });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public Setor ListarByID(string connString, Operacoes.tipo Tipo, int id)
        {
            Setor set = new Setor();
            string tsql = $"select idSetor, idLocalidade, descricao, centroCusto, status, cotaMensal from CadastroSetor where idSetor = {id};";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    set.idSetor = int.Parse(orow["idSetor"].ToString());
                    set.idLocalidade = int.Parse(orow["idLocalidade"].ToString());
                    set.Descricao = orow["descricao"].ToString();
                    set.CentroCusto = orow["centroCusto"].ToString();
                    set.Status = bool.Parse(orow["status"].ToString());
                    set.CotaMensal = int.Parse(orow["cotaMensal"].ToString());
                }
            }
            return set;
        }

        public static List<Setor> ListarByUnidade(string connString, Operacoes.tipo Tipo, int idUnidade)
        {
            List<Setor> Lista = new List<Setor>();
            string tsql = $"select idSetor, idLocalidade, descricao, centroCusto, status, cotaMensal from CadastroSetor where status = '1' and idlocalidade = {idUnidade};";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    Setor set = new Setor();
                    set.idSetor = int.Parse(orow["idSetor"].ToString());
                    set.idLocalidade = int.Parse(orow["idLocalidade"].ToString());
                    set.Descricao = orow["descricao"].ToString();
                    set.CentroCusto = orow["centroCusto"].ToString();
                    set.Status = bool.Parse(orow["status"].ToString());
                    set.CotaMensal = int.Parse(orow["cotaMensal"].ToString());
                    Lista.Add(set);
                }
            }
            return Lista.OrderBy(x => x.Descricao).ToList(); ;
        }
    }
}