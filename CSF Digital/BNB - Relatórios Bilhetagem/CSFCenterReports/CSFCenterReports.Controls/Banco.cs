using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CSFCenterReports.Controls
{
    public class Banco
    {
        #region Conexão
        public enum TipoConexao
        {
            Sistema,
            Dados,
            DadosReplicado,
            Transacional
        }

        private SqlConnection connTransacional, connSistema, connDados, connDadosReplicado = null;

        public Banco()
        {
            connSistema = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoCSF_Sistema"].ToString());
            connDados = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoCSF_Dados"].ToString());
            connDadosReplicado = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoCSF_DadosReplicado"].ToString());
            connTransacional = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoCSF_Transacional"].ToString());
        }

        public SqlConnection Conexao_Sistema
        {
            get { return connSistema; }
        }

        public SqlConnection Conexao_Dados
        {
            get { return connDados; }
        }

        public SqlConnection Conexao_DadosReplicado
        {
            get { return connDadosReplicado; }
        }
        public SqlConnection Conexao_DadosTransacionais
        {
            get { return connTransacional; }
        }

        public DataTable RetornarTabela(string sql, TipoConexao tipoConexao)
        {
            SqlCommand cmd = new SqlCommand();

            if(tipoConexao == TipoConexao.Sistema)
                cmd = new SqlCommand(sql, connSistema);
            else if(tipoConexao == TipoConexao.Dados)
                cmd = new SqlCommand(sql, connDados);
            else if (tipoConexao == TipoConexao.Transacional)
                cmd = new SqlCommand(sql, connTransacional);
            else
                cmd = new SqlCommand(sql, connDadosReplicado);

            return RetornarTabela(cmd);
        }

        public DataTable RetornarTabela(SqlCommand cmd)
        {
            DataTable dtResult = new DataTable();
            cmd.CommandTimeout = 300;
            
            SqlDataAdapter oDa = new SqlDataAdapter(cmd);

            try
            {
                try
                {
                    cmd.Connection.Open();
                    oDa.Fill(dtResult);
                }
                catch (Exception ex)
                {
                    if (cmd.Connection.State != ConnectionState.Closed)
                        cmd.Connection.Close();
                }
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dtResult;
        }

        public bool ExecutarComando(string sql, TipoConexao tipoConexao)
        {
            SqlCommand cmd = new SqlCommand();

            if (tipoConexao == TipoConexao.Sistema)
                cmd = new SqlCommand(sql, connSistema);
            else if (tipoConexao == TipoConexao.Dados)
                cmd = new SqlCommand(sql, connDados);
            else
                cmd = new SqlCommand(sql, connDadosReplicado);

            return ExecutarComando(cmd);
        }

        public bool ExecutarComando(SqlCommand cmd)
        {
            bool sucesso = false;

            try
            {
                try
                {
                    cmd.Connection.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        sucesso = true;
                    else
                        sucesso = false;
                }
                catch (Exception ex)
                {
                    sucesso = false;

                    if (cmd.Connection.State != ConnectionState.Closed)
                        cmd.Connection.Close();
                }
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return sucesso;
        }

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