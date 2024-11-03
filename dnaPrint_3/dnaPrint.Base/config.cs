namespace dnaPrint.Base
{
    using System.Data;
    using System.IO;

    public partial class config
    {
        static string connString = $"Data Source={Directory.GetCurrentDirectory()}\\App.s3db;Version=3;";
        public int Id { get; set; }

        public string configuracao { get; set; }

        public string valor { get; set; }

        public static string Verificar(string config)
        {
            string result = null;

            #region Antigo
            //using (Context ctx = new Context())
            //{
            //    List<config> lista = ctx.config.ToList();
            //    try
            //    {
            //        config c = lista.Where(x => x.configuracao == config).First();
            //        result = c.valor;
            //    }
            //    catch (Exception)
            //    {

            //    }

            //}
            #endregion

            DataTable dt = new DAO.SQLite().ReturnDt(connString, "select * from config");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if(row["configuracao"].ToString().Equals(config))
                    {
                        result = row["valor"].ToString();
                        break;
                    }
                }
            }
            return result;
        }
    }
}
