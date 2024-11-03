using dnaPrint.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Objetos
{
    public class EnvioSuprimento
    {
        #region campos
        public int ID { get; set; }
        public string Postagem { get; set; }
        public string Status { get; set; }
        public DateTime dataEntrega { get; set; }

        private static string connString = ConfigurationManager.ConnectionStrings["pecas"].ToString();
        #endregion

        public void Atualizar()
        {
            string tsqlUpdate = $"update enviosSuprimentos set statusEntrega = @status, dtEntrega = @data where idEnvio = @id;";
            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@id", this.ID });
            parametros.Add(new object[] { "@status", this.Status });
            if (this.Status.Contains("entregue"))
            {
                parametros.Add(new object[] { "@data", this.dataEntrega });
            }
            else
            {
                tsqlUpdate = $"update enviosSuprimentos set statusEntrega = @status where idEnvio = @id;";
            }

            new Operacoes(connString, Operacoes.tipo.SQLServer).ExecuteNonQuery(tsqlUpdate, parametros);
        }

        public static IQueryable<EnvioSuprimento> Listar(Func<EnvioSuprimento, bool> predicate)
        {
            return ListarTodos().Where(predicate).AsQueryable();
        }

        public static IQueryable<EnvioSuprimento> ListarTodos()
        {
            string tsql = @"select idEnvio, postagem, statusEntrega, dtEntrega  from enviosSuprimentos  where statusEntrega not like '%entregue%'
and statusEntrega <> 'Entrega Efetuada' and tpenvio in ('pac','sedex') and postagem is not null";
            DataTable dt = new Operacoes(connString, Operacoes.tipo.SQLServer).ReturnDt(tsql);

            List<EnvioSuprimento> Lista = new List<EnvioSuprimento>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    EnvioSuprimento envio = new EnvioSuprimento();
                    envio.ID = int.Parse(orow["idEnvio"].ToString());
                    envio.Postagem = orow["postagem"].ToString();
                    envio.Status = orow["statusEntrega"].ToString();
                    DateTime data;
                    if (DateTime.TryParse(orow["dtEntrega"].ToString(), out data))
                    {
                        envio.dataEntrega = data;
                    }
                    Lista.Add(envio);
                }
            }
            return Lista.AsQueryable();
        }
    }
}
