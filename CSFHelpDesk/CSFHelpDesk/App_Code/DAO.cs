using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Management;
using System.Configuration;

/// <summary>
/// Summary description for DAO
/// </summary>
public class DAO
{
    public enum connection { DefaultConnection };
    public static bool ExecuteNonQuery(string ConnectionName, string query)
    {
        bool result = false;
        string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ToString();
        SqlConnection conexao = new SqlConnection(connectionString);
        SqlCommand comando = new SqlCommand(query, conexao);
        try
        {
            conexao.Open();
            int value = comando.ExecuteNonQuery();
            if (value > 0)
                result = true;
        }
        catch
        {
            conexao.Close();
        }
        finally
        {
            conexao.Close();
        }
        return result;
    }

    public static object ExecuteScalar(string ConnectionName, string query)
    {
        object result = new object();
        string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ToString();
        SqlConnection conexao = new SqlConnection(connectionString);
        SqlCommand comando = new SqlCommand(query, conexao);
        try
        {
            conexao.Open();
            result = comando.ExecuteScalar();
        }
        catch
        {
            conexao.Close();
        }
        finally
        {
            conexao.Close();
        }
        return result;
    }

    internal static DataTable retornadt(string ConnectionName, string query)
    {
        DataTable result = new DataTable();
        string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ToString();
        SqlConnection conexao = new SqlConnection(connectionString);
        SqlCommand comando = new SqlCommand(query, conexao);
        SqlDataAdapter da = new SqlDataAdapter(comando);
        try
        {
            conexao.Open();
            da.Fill(result);
        }
        catch
        {
            conexao.Close();
        }
        finally
        {
            conexao.Close();
        }
        return result;
    }
}