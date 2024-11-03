using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using System.Data;
using System.Data.Sql;

namespace dnaPrint.Base
{
    public class ValorPagina
    {

        #region Campos

        public float valorpba4 { get; set; }
        public float valorpba3 { get; set; }
        public float valorcolora4 { get; set; }
        public float valorcolora3 { get; set; }
        public float valorscana4 { get; set; }
        public float valorscana3 { get; set; }

        #endregion

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;
            string tsql = "insert into cadastrovalorpag(valorpba4,valorpba3 ,valorcolora4 ,valorcolora3 ,valorscana4 ,valorscana3) values(@valorpba4, @valorpba3, @valorcolora4, @valorcolora3, @valorscana4, @valorscana3)";

            List<object[]> parametros = new List<object[]>();

            parametros.Add(new object[] { "@valorpba4", this.valorpba4 });
            parametros.Add(new object[] { "@valorpba3", this.valorpba3 });
            parametros.Add(new object[] { "@valorcolora4", this.valorcolora4 });
            parametros.Add(new object[] { "@valorcolora3", this.valorcolora3 });
            parametros.Add(new object[] { "@valorscana4", this.valorscana4 });
            parametros.Add(new object[] { "@valorscana3", this.valorscana3 });

            int qtd = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, parametros);

            if (qtd > 0)
                result = true;

            return result;
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;
            string tsql = $"update cadastrovalorpag set valorpba4 = @valorpba4, valorpba3 = @valorpba3, valorcolora4  = @valorcolora4, valorcolora3  = @valorcolora3, valorscana4  = @valorscana4 ,valorscana3 = @valorscana3";

            List<object[]> parametros = new List<object[]>();

            parametros.Add(new object[] { "@valorpba4", this.valorpba4 });
            parametros.Add(new object[] { "@valorpba3", this.valorpba3 });
            parametros.Add(new object[] { "@valorcolora4", this.valorcolora4 });
            parametros.Add(new object[] { "@valorcolora3", this.valorcolora3 });
            parametros.Add(new object[] { "@valorscana4", this.valorscana4 });
            parametros.Add(new object[] { "@valorscana3", this.valorscana3 });

            int qtd = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, parametros);

            if (qtd > 0)
                result = true;

            return result;
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            throw new NotImplementedException();
        }

        public static List<ValorPagina> Listar(string connString, Operacoes.tipo Tipo)
        {
            string tsql = "select valorpba4,valorpba3 ,valorcolora4 ,valorcolora3 ,valorscana4 ,valorscana3 from cadastrovalorpag";
            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);
            List<ValorPagina> Lista = new List<ValorPagina>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    ValorPagina vp = new ValorPagina();
                    vp.valorpba4 = float.Parse(orow["valorpba4"].ToString());
                    vp.valorpba3 = float.Parse(orow["valorpba3"].ToString());
                    vp.valorcolora4 = float.Parse(orow["valorcolora4"].ToString());
                    vp.valorcolora3 = float.Parse(orow["valorcolora3"].ToString());
                    vp.valorscana4 = float.Parse(orow["valorscana4"].ToString());
                    vp.valorscana3 = float.Parse(orow["valorscana3"].ToString());
                    Lista.Add(vp);
                }
            }

            return Lista;
        }
    }
}
