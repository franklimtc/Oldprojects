﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Monitor_Service_Correios
{
    class DAO
    {
        private static string connString = @"Data Source=192.168.2.222;Initial Catalog=pecas;Persist Security Info=True;User Id=sa;Password=Senh@123";

        public static DataTable retornaDt(string query)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comand = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(comand);

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch
            {
                conn.Close();
            }
            return dt;
        }

        public static bool Execute(string query)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comand = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
                conn.Close();
                result = true;
            }
            catch
            {
                conn.Close();
            }

            return result;
        }

        public static object ExecuteScalar(string query)
        {
            object result = null;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comand = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                result = comand.ExecuteScalar();
                conn.Close();
            }
            catch
            {
                conn.Close();
            }

            return result;
        }
    }
}