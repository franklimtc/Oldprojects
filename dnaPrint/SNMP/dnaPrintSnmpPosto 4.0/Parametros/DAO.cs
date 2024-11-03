using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Parametros
{
    class DAO
    {
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
        public static DataTable RetornaDT(string connString, string Query)
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

        public static void sqlcompactUpgrade(string connString)
        {
            SqlCeEngine ce = new SqlCeEngine(connString);
            try
            {
                ce.Upgrade();
            }
            catch { }
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

    }
}
