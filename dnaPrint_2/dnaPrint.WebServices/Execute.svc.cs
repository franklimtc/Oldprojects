using dnaPrint.DAO;
using System.Collections.Generic;
using System.Configuration;
using dnaPrint.Base;
using System;
using System.Data;
using System.Threading.Tasks;

namespace dnaPrint.WebServices
{
    public class Execute : IExecute
    {
        private static string chave = "CsfDigit@l2016";

        public bool Exec(string key, string query, List<string[]> parametros)
        {
            bool result = false;
            if(key == chave)
            {
                SQLServer comando = new SQLServer();
                int qtdLinhas = comando.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), query, parametros);
                if (qtdLinhas > 0)
                    result = true;
            }
            return result;
        }

        public async Task<int> ExecAsync(string key, string query, List<string[]> parametros)
        {
            if (key == chave)
            {
                SQLServer comando = new SQLServer();
                return await comando.ExecuteNonQueryAsync(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), query, parametros);
            }
            else
                return -1;
        }

        public List<string[]> ListarOids(string key)
        {
            List<string[]> lista = new List<string[]>();
            if(key == chave)
            {
                List<OID> listaOids = OID.Listar(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), Operacoes.tipo.SQLServer);
                foreach (var oid in listaOids)
                {
                    lista.Add(new string[] { oid.idPerfil.ToString(), oid.Fabricante, oid.Modelo, oid.Firmware, oid.Oid, oid.Propriedade });
                }
            }
            
            return lista;
        }

        public string RetornaValor(string key, string query)
        {
            SQLServer comando = new SQLServer();
            try
            {
                if (chave == key)
                {
                    return comando.ExecuteScalar(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), query).ToString();
                }
                else
                {
                    return null;
                }
            }
            catch 
            {
                return null;
            }
        }

        public async Task<object> RetornaValorAsync(string key, string query)
        {
            SQLServer comando = new SQLServer();
            try
            {
                if (chave == key)
                {
                    return await comando.ExecuteScalarAsync(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), query);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
