using dnaPrint.DAO;
using System.Collections.Generic;
using System.Configuration;
using dnaPrint.Base;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace dnaPrint.WebServices
{
    public class Execute : IExecute
    {
        private static string chave = "C@g&cI092017%@16";
        private static string vetor = "CsfDigit@l2016";

        public bool Exec(Operacoes.tipo Tipo, string key, string query, List<string[]> parametros)
        {
            bool result = false;
            string keyDecripto = null;
            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }

            if (key == chave)
            {
                Operacoes op = new DAO.Operacoes(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), Tipo);
                int qtdLinhas = op.ExecuteNonQuery(query, parametros);
                if (qtdLinhas > 0)
                    result = true;
            }
            return result;
        }

        public OID[] ListarOids(Operacoes.tipo Tipo, string key)
        {
            string keyDecripto = null;
            List<OID> Lista = new List<OID>();

            DataTable dt = new DataTable();
            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }
            if (key == chave)
            {
                Lista = OID.Listar(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), Tipo);
            }
            return Lista.ToArray();            
        }

        public object RetornaValor(Operacoes.tipo Tipo, string key, string query)
        {
            object result = new object();
            string keyDecripto = null;
            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }
            if (key == chave)
            {
                Operacoes op = new DAO.Operacoes(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), Tipo);
                result = op.ExecuteScalar(query);
            }
            return result;
        }
    }
}
