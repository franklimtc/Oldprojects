using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using System.Data;
using System.Configuration;

namespace CSF.Objetos
{
    public class Retornos
    {
        #region campos
        public int ID { get; set; }
        public string Postagem { get; set; }
        public int Status { get; set; }

        private static string connString = ConfigurationManager.ConnectionStrings["pecas"].ToString();
        #endregion

        public void Atualizar()
        {
            string tsqlUpdate = $"update ControlePostagensReversas set status = {this.Status} where id = {this.ID}";
            new Operacoes(connString, Operacoes.tipo.SQLServer).ExecuteNonQuery(tsqlUpdate);
        }

        public static IQueryable<Retornos> Listar(Func<Retornos, bool> predicate)
        {
            return ListarTodos().Where(predicate).AsQueryable();
        }

        public static IQueryable<Retornos> ListarTodos()
        {
            string tsql = "select  id, postagem, status from ControlePostagensReversas  where postagem <> '' and status  <> 3";
            DataTable dt = new Operacoes(connString, Operacoes.tipo.SQLServer).ReturnDt(tsql);

            List<Retornos> Lista = new List<Retornos>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    Retornos r = new Retornos();
                    r.ID = int.Parse(orow["id"].ToString());
                    r.Postagem = orow["postagem"].ToString();
                    r.Status = int.Parse(orow["status"].ToString());
                    Lista.Add(r);
                }
            }
            return Lista.AsQueryable();
        }
    }
}
