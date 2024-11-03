using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Controls
{
    public class DAO    
    {
        public static DataTable Retornadt(string connString, string query)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comand = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(comand);

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }

            return dt;
        }
    }
}
