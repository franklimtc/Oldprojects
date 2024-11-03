using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace dnaPrint.DAO
{
    public class Operacoes
    {
        public enum tipo { SQLServer, Postgre, SQLite, MySQL, Oracle}

        public tipo TipoDataBase { get; set; }
        public string ConnectionString { get; set; }

        public static tipo DefinirTipo(string banco)
        {
            tipo tp = tipo.SQLite;
            switch (banco)
            {
                case "SQLServer":
                    tp = tipo.SQLServer;
                    break;
                case "Postgre":
                    tp = tipo.Postgre;
                    break;
                case "MySQL":
                    tp = tipo.Postgre;
                    break;
                case "Oracle":
                    tp = tipo.Oracle;
                    break;
                default:
                    break;

            }
            return tp;
        }

        public Operacoes()
        {

        }

        public Operacoes(string connString, tipo tipoBase)
        {
            this.ConnectionString = connString;
            this.TipoDataBase = tipoBase;
        }

        public bool Conectar()
        {
            bool result = false;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().Conectar(this.ConnectionString);
                    break;
                case tipo.Postgre:
                    result = new Postgre().Conectar(this.ConnectionString);
                    break;
                case tipo.SQLite:
                    result = new SQLite().Conectar(this.ConnectionString);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().Conectar(this.ConnectionString);
                    break;
                case tipo.Oracle:
                    result = new Oracle().Conectar(this.ConnectionString);
                    break;
                default:
                    break;
            }
            return result;
        }

        public int ExecuteNonQuery(string query)
        {
            int result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ExecuteNonQuery(this.ConnectionString, query);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ExecuteNonQuery(this.ConnectionString, query);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ExecuteNonQuery(this.ConnectionString, query);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ExecuteNonQuery(this.ConnectionString, query);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ExecuteNonQuery(this.ConnectionString, query);
                    break;
                default:
                    break;
            }
            return result;
        }

        public int ExecuteNonQuery(string query, List<string[]> parametros)
        {
            int result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public int ExecuteNonQuery(string query, List<object[]> parametros)
        {
            int result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ExecuteNonQuery(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            int result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = await new MySQL().ExecuteNonQueryAsync(this.ConnectionString, query);
                    break;
                case tipo.Postgre:
                    result = await new Postgre().ExecuteNonQueryAsync(this.ConnectionString, query);
                    break;
                case tipo.SQLite:
                    result = await new SQLite().ExecuteNonQueryAsync(this.ConnectionString, query);
                    break;
                case tipo.SQLServer:
                    result = await new SQLServer().ExecuteNonQueryAsync(this.ConnectionString, query);
                    break;
                case tipo.Oracle:
                    result = await new Oracle().ExecuteNonQueryAsync(this.ConnectionString, query);
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string query, List<string[]> parametros)
        {
            int result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = await new MySQL().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = await new Postgre().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = await new SQLite().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = await new SQLServer().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = await new Oracle().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string query, List<object[]> parametros)
        {
            int result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = await new MySQL().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = await new Postgre().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = await new SQLite().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = await new SQLServer().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = await new Oracle().ExecuteNonQueryAsync(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public object ExecuteScalar(string query)
        {
            object result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ExecuteScalar(this.ConnectionString, query);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ExecuteScalar(this.ConnectionString, query);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ExecuteScalar(this.ConnectionString, query);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ExecuteScalar(this.ConnectionString, query);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ExecuteScalar(this.ConnectionString, query);
                    break;
                default:
                    break;
            }
            return result;
        }

        public object ExecuteScalar(string query, List<string[]> parametros)
        {
            object result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public object ExecuteScalar(string query, List<object[]> parametros)
        {
            object result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ExecuteScalar(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<object> ExecuteScalarAsync(string query)
        {
            object result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = await new MySQL().ExecuteScalarAsync(this.ConnectionString, query);
                    break;
                case tipo.Postgre:
                    result = await new Postgre().ExecuteScalarAsync(this.ConnectionString, query);
                    break;
                case tipo.SQLite:
                    result = await new SQLite().ExecuteScalarAsync(this.ConnectionString, query);
                    break;
                case tipo.SQLServer:
                    result = await new SQLServer().ExecuteScalarAsync(this.ConnectionString, query);
                    break;
                case tipo.Oracle:
                    result = await new Oracle().ExecuteScalarAsync(this.ConnectionString, query);
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<object> ExecuteScalarAsync(string query, List<string[]> parametros)
        {
            object result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = await new MySQL().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = await new Postgre().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = await new SQLite().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = await new SQLServer().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = await new Oracle().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public async Task<object> ExecuteScalarAsync(string query, List<object[]> parametros)
        {
            object result = 0;
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = await new MySQL().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = await new Postgre().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = await new SQLite().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = await new SQLServer().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = await new Oracle().ExecuteScalarAsync(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public DataTable ReturnDt( string query)
        {
            DataTable result = new DataTable();
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ReturnDt(this.ConnectionString, query);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ReturnDt(this.ConnectionString, query);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ReturnDt(this.ConnectionString, query);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ReturnDt(this.ConnectionString, query);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ReturnDt(this.ConnectionString, query);
                    break;
                default:
                    break;
            }
            return result;
        }

        public DataTable ReturnDt(string query, List<string[]> parametros)
        {
            DataTable result = new DataTable();
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }

        public DataTable ReturnDt( string query, List<object[]> parametros)
        {
            DataTable result = new DataTable();
            switch (TipoDataBase)
            {
                case tipo.MySQL:
                    result = new MySQL().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.Postgre:
                    result = new Postgre().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLite:
                    result = new SQLite().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.SQLServer:
                    result = new SQLServer().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                case tipo.Oracle:
                    result = new Oracle().ReturnDt(this.ConnectionString, query, parametros);
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
