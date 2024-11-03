using System.Collections.Generic;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class ModeloEquipamento : IBase<ModeloEquipamento>
    {
        #region Campos
        public int idModeloEquipamento { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int Franquia { get; set; }
        public float Valor { get; set; }
        public bool Status { get; set; }
        public string ItemModelo { get; set; }
        #endregion

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"insert into CadastroEquipamentoModelo(Fabricante, Modelo, franquia, valor) values(@Fabricante, @Modelo, @franquia, @valor);";
            List<object[]> Parametros = new List<object[]>();
            Parametros.Add(new object[] { "@Fabricante", this.Fabricante });
            Parametros.Add(new object[] { "@Modelo", this.Modelo });
            Parametros.Add(new object[] { "@franquia", this.Franquia });
            Parametros.Add(new object[] { "@valor", this.Valor });


            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"update CadastroEquipamentoModelo set Fabricante = @Fabricante, Modelo = @Modelo, franquia = @franquia, valor = @valor where idModeloEquipamento = @idModeloEquipamento;";
            List<object[]> Parametros = new List<object[]>();
            Parametros.Add(new object[] { "@Fabricante", this.Fabricante });
            Parametros.Add(new object[] { "@Modelo", this.Modelo });
            Parametros.Add(new object[] { "@franquia", this.Franquia });
            Parametros.Add(new object[] { "@valor", this.Valor });
            Parametros.Add(new object[] { "@idModeloEquipamento", this.idModeloEquipamento });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"update CadastroEquipamentoModelo set satus = '0' where idModeloEquipamento = @idModeloEquipamento;";
            List<object[]> Parametros = new List<object[]>();
            Parametros.Add(new object[] { "@idModeloEquipamento", this.idModeloEquipamento});

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public ModeloEquipamento ListarByID(string connString, Operacoes.tipo Tipo, int idModelo)
        {
            ModeloEquipamento mod = new ModeloEquipamento();
            string tsql = $"Select idModeloEquipamento, Fabricante, Modelo, status from CadastroEquipamentoModelo where idModeloEquipamento = {idModelo};";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    mod.idModeloEquipamento = int.Parse(orow["idModeloEquipamento"].ToString());
                    mod.Fabricante = orow["Fabricante"].ToString();
                    mod.Modelo = orow["Modelo"].ToString();
                    mod.Status = bool.Parse(orow["status"].ToString());
                }
            }
            return mod;
        }

        public static List<ModeloEquipamento> ListarTodos(string connString, Operacoes.tipo Tipo)
        {
            List<ModeloEquipamento> Lista = new List<ModeloEquipamento>();
            string tsql = $"Select idModeloEquipamento, Fabricante, Modelo, status, franquia, valor from CadastroEquipamentoModelo;";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    ModeloEquipamento mod = new ModeloEquipamento();
                    mod.idModeloEquipamento = int.Parse(orow["idModeloEquipamento"].ToString());
                    mod.Fabricante = orow["Fabricante"].ToString();
                    mod.Modelo = orow["Modelo"].ToString();
                    mod.Franquia = int.Parse(orow["franquia"].ToString());
                    mod.Valor = float.Parse(orow["valor"].ToString());
                    mod.Status = bool.Parse(orow["status"].ToString());
                    mod.ItemModelo = $"{mod.idModeloEquipamento} - {mod.Modelo}";
                    Lista.Add(mod);
                }
            }
            return Lista;
        }

        
    }
}
