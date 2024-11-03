using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CSFDigital.Controls
{
    public class Banco
    {
        #region Conexão SQL SERVER
        private SqlConnection connUsd, connCsf = null, connDna;

        public Banco()
        {
            connUsd = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoMDB"].ToString());
            connCsf = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoCSFDB"].ToString());
            connDna = new SqlConnection(ConfigurationManager.ConnectionStrings["dnaPrint"].ToString());
        }

        #region MDB - USD
        public SqlConnection Conexao
        {
            get { return connUsd; }
        }

        public DataTable ExecuteDataTable(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connUsd);

            return ExecuteDataTable(cmd);
        }

        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            DataTable dtResult = new DataTable();
            cmd.CommandTimeout = 1000;

            SqlDataAdapter oDa = new SqlDataAdapter(cmd);

            try
            {
                connUsd.Open();
                oDa.Fill(dtResult);
            }
            finally
            {
                connUsd.Close();
            }

            return dtResult;
        }

        //public DataTable ExecuteDataTable(string sql)
        //{
        //    if (sql.Contains("CATEGORIA"))
        //        return Excel.RetornarDados();
        //    else
        //        return Excel.RetornarLog();
        //}
        //public DataTable ExecuteDataTable(SqlCommand cmd)
        //{
        //    if (cmd.CommandText.Contains("CATEGORIA"))
        //        return Excel.RetornarDados();
        //    else
        //        return Excel.RetornarLog();
        //}

        public int ExecuteCommand(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connUsd);

            cmd.CommandTimeout = 480;

            try
            {
                connUsd.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                connUsd.Close();
            }
        }

        public int ExecuteCommand(SqlCommand cmd)
        {
            try
            {
                connUsd.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                connUsd.Close();
            }
        }
        #endregion

        #region MDB - CSF
        public SqlConnection ConexaoCsf
        {
            get { return connCsf; }
        }

        public SqlConnection ConexaoDna
        {
            get { return connDna; }
        }

        public DataTable ExecuteDataTableCsf(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connCsf);

            return ExecuteDataTableCsf(cmd);
        }

        public DataTable ExecuteDataTableCsf(SqlCommand cmd)
        {
            DataTable dtResult = new DataTable();
            cmd.CommandTimeout = 1000;

            SqlDataAdapter oDa = new SqlDataAdapter(cmd);

            try
            {
                connCsf.Open();
                oDa.Fill(dtResult);
            }
            finally
            {
                connCsf.Close();
            }

            return dtResult;
        }

        public int ExecuteCommandCsf(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connCsf);

            cmd.CommandTimeout = 480;

            try
            {
                connCsf.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                connCsf.Close();
            }
        }

        public int ExecuteCommandCsf(SqlCommand cmd)
        {
            try
            {
                connCsf.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                connCsf.Close();
            }
        }
        #endregion

        public void AddParameter(SqlCommand cmd, string name, object value, SqlDbType sqlDbType, ParameterDirection parameterDirection)
        {
            SqlParameter param = new SqlParameter(name, sqlDbType);

            param.Direction = parameterDirection;

            if (value == null)
                param.Value = DBNull.Value;
            else
                param.Value = value;

            cmd.Parameters.Add(param);
        }
        #endregion
    }
}