using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace dnaPrint.DAO
{
    public class SQLite : IBase
    {
        public bool Conectar(string connectionString)
        {

            bool result = false;
            SQLiteConnection Conexao = new SQLiteConnection(connectionString);
            try
            {
                Conexao.Open();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
            return result;
        }

        public int ExecuteNonQuery(string connectionString, string query)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);

            int result = 0;

            try
            {
                conn.Open();
                result = comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int ExecuteNonQuery(string connectionString, string query, List<string[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);

            int result = 0;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    int valor = 0;
                    if (int.TryParse(par[1], out valor))
                    {
                        comand.Parameters.AddWithValue(par[0], valor);
                    }
                    else
                    {
                        comand.Parameters.AddWithValue(par[0], par[1]);
                    }
                }
            }

            try
            {
                conn.Open();
                result = comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int ExecuteNonQuery(string connectionString, string query, List<object[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);

            int result = 0;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    comand.Parameters.AddWithValue(par[0].ToString(), par[1]);
                }
            }

            try
            {
                conn.Open();
                result = comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string connectionString, string query)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);

            int result;

            try
            {
                conn.Open();
                result = await comand.ExecuteNonQueryAsync();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string connectionString, string query, List<string[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);

           int result;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    int valor = 0;
                    if (int.TryParse(par[1], out valor))
                    {
                        comand.Parameters.AddWithValue(par[0], valor);
                    }
                    else
                    {
                        comand.Parameters.AddWithValue(par[0], par[1]);
                    }
                }
            }

            try
            {
                conn.Open();
                result = await comand.ExecuteNonQueryAsync();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string connectionString, string query, List<object[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);

            int result;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    comand.Parameters.AddWithValue(par[0].ToString(), par[1]);
                }
            }

            try
            {
                conn.Open();
                result = await comand.ExecuteNonQueryAsync();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public object ExecuteScalar(string connectionString, string query)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            object result;

            try
            {
                conn.Open();
                result = comand.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public object ExecuteScalar(string connectionString, string query, List<string[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            object result;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    int valor = 0;
                    if (int.TryParse(par[1], out valor))
                    {
                        comand.Parameters.AddWithValue(par[0], valor);
                    }
                    else
                    {
                        comand.Parameters.AddWithValue(par[0], par[1]);
                    }
                }
            }

            try
            {
                conn.Open();
                result = comand.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public object ExecuteScalar(string connectionString, string query, List<object[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            object result;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    comand.Parameters.AddWithValue(par[0].ToString(), par[1]);
                }
            }

            try
            {
                conn.Open();
                result = comand.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<object> ExecuteScalarAsync(string connectionString, string query)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            object result;

            try
            {
                conn.Open();
                result = await comand.ExecuteScalarAsync();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<object> ExecuteScalarAsync(string connectionString, string query, List<string[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            object result;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    int valor = 0;
                    if (int.TryParse(par[1], out valor))
                    {
                        comand.Parameters.AddWithValue(par[0], valor);
                    }
                    else
                    {
                        comand.Parameters.AddWithValue(par[0], par[1]);
                    }
                }
            }

            try
            {
                conn.Open();
                result = await comand.ExecuteScalarAsync();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public async Task<object> ExecuteScalarAsync(string connectionString, string query, List<object[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            object result;

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    comand.Parameters.AddWithValue(par[0].ToString(), par[1]);
                }
            }

            try
            {
                conn.Open();
                result = await comand.ExecuteScalarAsync();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public DataTable ReturnDt(string connectionString, string query)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            SQLiteDataAdapter da = new SQLiteDataAdapter(comand);

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public DataTable ReturnDt(string connectionString, string query, List<string[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            SQLiteDataAdapter da = new SQLiteDataAdapter(comand);

            DataTable dt = new DataTable();

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    int valor = 0;
                    if (int.TryParse(par[1], out valor))
                    {
                        comand.Parameters.AddWithValue(par[0], valor);
                    }
                    else
                    {
                        comand.Parameters.AddWithValue(par[0], par[1]);
                    }
                }
            }

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public DataTable ReturnDt(string connectionString, string query, List<object[]> parametros)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand comand = new SQLiteCommand(query, conn);
            SQLiteDataAdapter da = new SQLiteDataAdapter(comand);

            DataTable dt = new DataTable();

            if (parametros != null)
            {
                foreach (var par in parametros)
                {
                    comand.Parameters.AddWithValue(par[0].ToString(), par[1]);
                }
            }

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}
