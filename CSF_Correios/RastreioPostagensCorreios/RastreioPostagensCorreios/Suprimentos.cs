using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using Rastreamento;

namespace RastreioPostagensCorreios
{
    public class Suprimentos
    {
        #region Campos
        private int _idEnvio;
        private string _serie;
        private string _postagem;
        private string _statusEntrega;
        private Rastreio rastro;
        public string TipoEnvio { get; set; }
        public DateTime DTEnvio { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }

        public int IdEnvio { get => _idEnvio; set => _idEnvio = value; }
        public string Serie { get => _serie; set => _serie = value; }
        public string Postagem { get => _postagem; set => _postagem = value; }
        public string StatusEntrega { get => _statusEntrega; set => _statusEntrega = value; }
        public Rastreio Rastro { get => rastro; set => rastro = value; }
        #endregion 

        public static List<Suprimentos> Listar(string connString)
        {
            List<Suprimentos> Lista = new List<Suprimentos>();
            string tsql = @"select a.idEnvio, a.serie, a.postagem, a.statusEntrega, a.dtEnvio, a.tpEnvio, b.contato, b.email
from enviosSuprimentos as a
left join equipamentos as b on a.serie = b.serie and b.status = 1
where a.tpenvio in ('pac','sedex')
and a.postagem is not null
and a.statusEntrega not like '%Entregue%'
and a.statusEntrega not like '%incêndio%'
and a.statusEntrega not like '%Objeto devolvido%'
and a.statusEntrega not in ('Entrega Efetuada')
and a.dtEnvio < cast(getdate()-2 as date) 
and b.email like '%@%.%'
";

            DataTable dt = new dnaPrint.DAO.SQLServer().ReturnDt(connString, tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow envio in dt.Rows)
                {
                    Suprimentos suprimento = new Suprimentos();
                    suprimento.IdEnvio = int.Parse(envio["idEnvio"].ToString());
                    suprimento.Serie = envio["serie"].ToString();
                    suprimento.Postagem = envio["postagem"].ToString();
                    suprimento.StatusEntrega = envio["statusEntrega"].ToString();
                    suprimento.DTEnvio = DateTime.Parse(envio["dtEnvio"].ToString());
                    suprimento.TipoEnvio = envio["tpEnvio"].ToString();
                    suprimento.Contato = envio["contato"].ToString();
                    suprimento.Email = envio["email"].ToString();

                    Lista.Add(suprimento);
                }
            }

            return Lista;
        }

        public void Rastrear()
        {
            using (WSCorreios.AtendeClienteClient clientws = new WSCorreios.AtendeClienteClient())
            {
                string[] listaObj = { this.Postagem };

                try
                {
                    string r = clientws.consultaSRO(listaObj, "L", "U", "ECT", "SRO");
                    this.Rastro = new Rastreio(r);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ConfirmarEntrega(string connString)
        {
            string tsql = $"update enviosSuprimentos set statusEntrega = @status, dtEntrega = @data where idEnvio = @idEnvio;";

            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@status", this.Rastro.Status });
            parametros.Add(new object[] { "@idEnvio", this.IdEnvio });
            parametros.Add(new object[] { "@data", this.Rastro.Data });

            try
            {
                new SQLServer().ExecuteNonQuery(connString, tsql, parametros);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void AtualizarStatus(string connString)
        {
            string tsql = $"update enviosSuprimentos set statusEntrega = @status where idEnvio = @idEnvio;";

            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@status", $"{this.Rastro.Status} - {this.Rastro.Local}" });
            parametros.Add(new object[] { "@idEnvio", this.IdEnvio });

            try
            {
                new SQLServer().ExecuteNonQuery(connString, tsql, parametros);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
