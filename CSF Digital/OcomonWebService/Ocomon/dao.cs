using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Ocomon
{
    public class dao
    {
        private static string conexao = string.Format("Server=192.168.2.222;Database=pecas;User Id=sa;Password = Senh@123;");

        public static DataTable retornaDt(string connexao, string query)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connexao].ToString());
            MySqlCommand comand = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(comand);

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Clone();
            }
            catch
            {
                conn.Clone();
            }
            return dt;
        }
        internal static bool Execute(string connexao, string query)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connexao].ToString());
            MySqlCommand comand = new MySqlCommand(query, conn);

            DataTable dt = new DataTable();
            bool result = false;
            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
                result = true;
                conn.Clone();
            }
            catch
            {
                conn.Clone();
            }
            return result;
        }
        internal static string RetornaValor(string connexao, string query)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connexao].ToString());
            MySqlCommand comand = new MySqlCommand(query, conn);

            DataTable dt = new DataTable();
            string result = null;
            try
            {
                conn.Open();

                result = comand.ExecuteScalar().ToString();
                conn.Clone();
            }
            catch
            {
                conn.Clone();
            }
            return result;
        }

        public static bool ExecuteNonQuery(string query)
        {
            SqlConnection conn = new SqlConnection(conexao);
            SqlCommand comand = new SqlCommand(query, conn);
            int qtdLinhas = 0;
            try
            {
                conn.Open();
                qtdLinhas = comand.ExecuteNonQuery();
            }
            catch
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            bool result = false;
            if (qtdLinhas > 0)
                result = true;
            return result;
        }

        public static DataTable retornaDt(string query)
        {
            SqlConnection conn = new SqlConnection(conexao);
            SqlCommand comand = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comand);

            try
            {
                conn.Open();
                da.Fill(dt);
            }
            catch
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static object Execute(string query)
        {
            SqlConnection conn = new SqlConnection(conexao);
            SqlCommand comand = new SqlCommand(query, conn);
            object result = null;
            try
            {
                conn.Open();
                result = comand.ExecuteScalar();
            }
            catch
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }


    }
}