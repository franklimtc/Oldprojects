using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DAO
/// </summary>
public class DAO
{
    private static string ConnString = @"Server=csfocomon.mysql.dbaas.com.br;Port=3306;Database=csfocomon;Uid=csfocomon;Pwd=Senh@123;";
    private static string ConnStringSQL = @"Server=192.168.2.222;Database=pecas;User Id=sa;Password=Senh@123;";
    public DAO()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public static DataTable RetornaDT(string query)
    {
        MySqlConnection conn = new MySqlConnection(ConnString);
        MySqlCommand comand = new MySqlCommand(query, conn);
        MySqlDataAdapter da = new MySqlDataAdapter(comand);
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

    public static int ExecuteNonQuery(MySqlCommand comand)
    {
        MySqlConnection conn = new MySqlConnection(ConnString);
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

    public static void Log(string Mensagem)
    {
        string filename = string.Format(@"{0}/Log_{1}.txt", Directory.GetCurrentDirectory(), DateTime.Now.ToString("yyyyMMdd"));
        if(!File.Exists(filename))
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
            RegistrarErro(ex.Message);
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
            RegistrarErro(ex.Message);
        }
        return qtdLinhas;
    }

    public static object SQLExecute(string query)
    {
        SqlConnection conn = new SqlConnection(ConnStringSQL);
        SqlCommand comand = new SqlCommand(query, conn);
        object result = null;
        try
        {
            conn.Open();
            result = comand.ExecuteScalar();
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            RegistrarErro(ex.Message);
        }
        return result;
    }

    public static void RegistrarErro(string erro)
    {
        SqlConnection conn = new SqlConnection(ConnStringSQL);
        SqlCommand comand = new SqlCommand();
        comand.Connection = conn;
        comand.CommandText = string.Format("insert into LogsErros(sistema, erro) values('{0}','{1}');", "Alertas por Email", erro.Replace("'",";"));

        comand.Connection = conn;
        try
        {
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            conn.Close();
            //Log(ex.Message);
        }
    }
}