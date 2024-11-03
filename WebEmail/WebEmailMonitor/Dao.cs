using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for Dao
/// </summary>
/// 

namespace WebEmailMonitor
{
    public class Dao
    {
        private static string ConnString = @"Server=csfocomon.mysql.dbaas.com.br;Port=3306;Database=csfocomon;Uid=csfocomon;Pwd=Senh@123;";
        private static string ConnStringSQL = @"Server=192.168.2.222;Database=pecas;User Id=sa;
Password=Senh@123;";
      

        public static void Log(string Mensagem)
        {
            string filename = string.Format(@"{0}/Log_{1}.txt", Directory.GetCurrentDirectory(), DateTime.Now.ToString("yyyyMMdd"));
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            using (var arquivo = File.OpenWrite(filename))
            {
                using (var escritor = new StreamWriter(arquivo))
                {
                    escritor.Write(string.Format("{0} - INFO - {1}", DateTime.Now.ToString("yyyyMMddHHmmss"), Mensagem));
                }
            }
        }

        public static DataTable SQLRetornaDT(string query)
        {
            SqlConnection conn = new SqlConnection(ConnStringSQL);
            SqlCommand comand = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                //Log(ex.Message);
            }
            return dt;
        }

        public static int SQLExecuteNonQuery(SqlCommand comand)
        {
            SqlConnection conn = new SqlConnection(ConnStringSQL);
            comand.Connection = conn;
            int qtdLinhas = 0;
            try
            {
                conn.Open();
                qtdLinhas = comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                //Log(ex.Message);
            }
            return qtdLinhas;
        }

    }
}