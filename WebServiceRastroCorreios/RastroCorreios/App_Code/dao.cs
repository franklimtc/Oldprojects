using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using MySql.Web;


/// <summary>
/// Summary description for dao
/// </summary>
public class dao
{
    private static string connString = @"Server=csfocomon.mysql.dbaas.com.br;Database=csfocomon;Uid=csfocomon;Pwd=Senh@123;";

    public dao()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable retornadt(string query)
    {
        MySqlConnection conn = new MySqlConnection(connString);
        MySqlCommand comand = new MySqlCommand(query, conn);
        MySqlDataAdapter da = new MySqlDataAdapter(comand);
        DataTable dt = new DataTable();

        try
        {
            conn.Open();
            da.Fill(dt);
        }
        catch(Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }

        return dt;
    }

    internal static void Execute(string query)
    {
        MySqlConnection conn = new MySqlConnection(connString);
        MySqlCommand comand = new MySqlCommand(query, conn);

        try
        {
            conn.Open();
            comand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
    }

    internal static object RetornaValor(string query)
    {
        MySqlConnection conn = new MySqlConnection(connString);
        MySqlCommand comand = new MySqlCommand(query, conn);
        object valor = new object();
        try
        {
            conn.Open();
            valor = comand.ExecuteScalar();
        }
        catch (Exception ex)
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
        return valor;
    }
}