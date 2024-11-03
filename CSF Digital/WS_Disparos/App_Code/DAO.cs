using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for DAO
/// </summary>
public class DAO
{
    public static DataTable retornadt(string connStrin, string query)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(connStrin);
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

        }
        return dt;
    }

    public static bool execute(string connStrin, string query)
    {
        SqlConnection conn = new SqlConnection(connStrin);
        SqlCommand comand = new SqlCommand(query, conn);
        bool result = false;
        try
        {
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            result = true;
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    internal static string retornaValor(string connStrin, string query)
    {
        SqlConnection conn = new SqlConnection(connStrin);
        SqlCommand comand = new SqlCommand(query, conn);
        string value = null;
        try
        {
            conn.Open();
            value = comand.ExecuteScalar().ToString();
            conn.Close();
        }
        catch (Exception ex)
        {

        }
        return value;
    }
}