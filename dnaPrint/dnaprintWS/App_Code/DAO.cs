using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for DAO
/// </summary>
public class DAO
{
    static string ConnString = ConfigurationManager.ConnectionStrings["dnaprintws"].ToString();

    public static bool ExecutaSQL( string Query)
    {
        bool result = false;

        SqlConnection conexao = new SqlConnection(ConnString);
        SqlCommand comando = new SqlCommand(Query, conexao);
        SqlDataAdapter da = new SqlDataAdapter(comando);
        try
        {
            conexao.Open();
            comando.ExecuteNonQuery();
            result = true;
        }
        catch (Exception ex)
        {
            conexao.Close();
        }
        finally
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
        return result;
    }
}