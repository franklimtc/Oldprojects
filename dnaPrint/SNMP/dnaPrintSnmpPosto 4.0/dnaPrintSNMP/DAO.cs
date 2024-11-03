using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Data.SqlServerCe;

namespace dnaPrintSNMP
{
    public class DAO
    {
        private static string msg = "";

        public static DataTable RetornaDt(string ConnString, string Query)
        {
            DataTable dt = new DataTable();

            SqlConnection conexao = new SqlConnection(ConnString);
            SqlCommand comando = new SqlCommand(Query, conexao);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            try
            {
                conexao.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                msg = "Falha na conexão como BD.\n" + ex.ToString();
                //Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                conexao.Close();
            }
            finally
            {
                conexao.Close();
            }

            return dt;
        }

        public static bool ExecutaSQL(string ConnString, string Query)
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
                msg = "Falha na execução do comando.\n" + ex.ToString();
                //Logs.GerarLogs(Logs.TipoLogs.geral, msg);
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

        public static void GerarTXT(string NomeArquivo, string Texto)
        {
            if (!File.Exists(NomeArquivo))
            {
                File.Create(NomeArquivo).Close();
            }
            TextWriter arquivo = File.AppendText(NomeArquivo);
            arquivo.WriteLine(Texto);
            arquivo.Flush();
            arquivo.Close();
        }

        public static List<string> LerTxt(string Arquivo)
        {
            List<string> sb = new List<string>();
            string line;

            try
            {
                StreamReader sr = new StreamReader(Arquivo);
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Add(line.ToString());
                }
                sr.Close();
            }
            catch
            {
                sb.Add("");
            }

            return sb;
        }

        public static string RetornaAtributoXml(string arquivo, string atributo)
        {
            XmlTextReader reader = new XmlTextReader(arquivo);
            string temp = "";
            while (reader.Read())
            {
                if (reader.AttributeCount > 0)
                {
                    reader.MoveToAttribute(atributo);
                    temp = reader.Value;
                }
            }
            return temp;
        }

        public static DataTable RetornaDtCompact(string connString, string Query)
        {
            SqlCeConnection conn = new SqlCeConnection(connString);
            SqlCeCommand comand = new SqlCeCommand(Query, conn);
            SqlCeDataAdapter da = new SqlCeDataAdapter(comand);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                DAO.sqlcompactUpgrade(connString);
            }
            catch
            {
                conn.Close();
            }
            return dt;

        }

        public static string RetornaValorCompact(string connString, string Query)
        {
            SqlCeConnection conn = new SqlCeConnection(connString);
            SqlCeCommand comand = new SqlCeCommand(Query, conn);
            string valor = null;
            try
            {
                conn.Open();
                valor = comand.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                DAO.sqlcompactUpgrade(connString);
            }
            catch
            {
                conn.Close();
            }
            return valor;

        }

        public static void ExecuteCompact(string connString, string Query)
        {
            SqlCeConnection conn = new SqlCeConnection(connString);
            SqlCeCommand comand = new SqlCeCommand(Query, conn);
            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                DAO.sqlcompactUpgrade(connString);
            }
            catch
            {
                conn.Close();
            }

        }

        public static void sqlcompactUpgrade(string connString)
        {
            SqlCeEngine ce = new SqlCeEngine(connString);
            try
            {
                ce.Upgrade();
            }
            catch { }
        }
    }
}
