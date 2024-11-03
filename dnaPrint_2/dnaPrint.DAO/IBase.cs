using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace dnaPrint.DAO
{
    public interface IBase
    {
        bool Conectar(string connectionString);

        int ExecuteNonQuery(string connectionString, string query);
        int ExecuteNonQuery(string connectionString, string query, List<string[]> parametros);
        int ExecuteNonQuery(string connectionString, string query, List<object[]> parametros);

        Task<int> ExecuteNonQueryAsync(string connectionString, string query);
        Task<int> ExecuteNonQueryAsync(string connectionString, string query, List<string[]> parametros);
        Task<int> ExecuteNonQueryAsync(string connectionString, string query, List<object[]> parametros);

        object ExecuteScalar(string connectionString, string query);
        object ExecuteScalar(string connectionString, string query, List<string[]> parametros);
        object ExecuteScalar(string connectionString, string query, List<object[]> parametros);

        Task<object> ExecuteScalarAsync(string connectionString, string query);
        Task<object> ExecuteScalarAsync(string connectionString, string query, List<string[]> parametros);
        Task<object> ExecuteScalarAsync(string connectionString, string query, List<object[]> parametros);

        DataTable ReturnDt(string connectionString, string query);
        DataTable ReturnDt(string connectionString, string query, List<string[]> parametros);
        DataTable ReturnDt(string connectionString, string query, List<object[]> parametros);
    }
}
