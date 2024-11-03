using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Controls
{
    class DAO
    {
        private static string conString = ConfigurationManager.ConnectionStrings["controleSaidaMaterial"].ToString();

        public static DataTable retornadt(string tsql)
        {
            SqlConnection conection = new SqlConnection(conString);
            SqlCommand comand = new SqlCommand(tsql, conection);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comand);

            try
            {
                conection.Open();
                da.Fill(dt);
                conection.Close();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return dt;
        }

        public static int ExecuteNonQuery(string tsql)
        {
            SqlConnection conection = new SqlConnection(conString);
            SqlCommand comand = new SqlCommand(tsql, conection);
            int result = 0;
            try
            {
                conection.Open();
                result = comand.ExecuteNonQuery();
                conection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        public static object ExecuteScalar(string tsql)
        {
            SqlConnection conection = new SqlConnection(conString);
            SqlCommand comand = new SqlCommand(tsql, conection);
            object result = null;
            try
            {
                conection.Open();
                result = comand.ExecuteScalar();
                conection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

    }
}
