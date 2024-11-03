using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Controls
{
    class dao
    {

        public static string connString()
        {
            //return "Data Source=localhost;Initial Catalog=EasyAccount;Integrated Security=True"; 
            return ConfigurationManager.ConnectionStrings["pecasSigep01"].ToString();
        }

        public static bool ExecuteNonQuery(string ConnectionString, string query)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand comand = new SqlCommand(query, connection);
            bool resposta;
            try
            {
                connection.Open();
                comand.ExecuteNonQuery();
                resposta = true;
            }
            catch
            {
                connection.Close();
                resposta = false;
            }
            finally
            {
                connection.Close();
            }
            return resposta;
        }

        public static DataTable RetornaDt(string ConnectionString, string query)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand comand = new SqlCommand(query, connection);
            SqlDataAdapter da = new SqlDataAdapter(comand);

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                da.Fill(dt);
            }
            catch
            {
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static string ExecuteScalar(string ConnectionString, string query)
        {
            string result = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand comand = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                result = comand.ExecuteScalar().ToString();
            }
            catch
            {
                connection.Close();
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        public static void Execute(string sql, string Connectionname)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Connectionname].ToString());
            SqlCommand comand = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
            }
            catch
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
