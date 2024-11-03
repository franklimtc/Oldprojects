using System.Collections.Generic;
using System.Configuration;
using System.Data;
using dnaPrint.Base;

namespace dnaPrint.WCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Operacoes" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Operacoes.svc ou Operacoes.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Operacoes : IOperacoes
    {
        private static string chave = "C@g&cI092017%@16";
        private static string vetor = "CsfDigit@l201607";
        //"317498F1EB83DFA4C3D999F5C1D4A5B591AAAAD0BFDFE6939E6B6E5F26036E3B"

        public bool Exec(string key, string query, List<string[]> parametros)
        {
            bool result = false;
            string keyDecripto = null;
            var dbTipo = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());

            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }

            if (keyDecripto == chave)
            {
                DAO.Operacoes op = new DAO.Operacoes(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), dbTipo);
                int qtdLinhas = op.ExecuteNonQuery(query, parametros);
                if (qtdLinhas > 0)
                    result = true;
            }
            return result;
        }

        public List<OID> ListarOids(string key)
        {
            string keyDecripto = null;
            var dbTipo = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());

            List<OID> Lista = new List<OID>();

            DataTable dt = new DataTable();
            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }
            if (keyDecripto == chave)
            {
                
                Lista = OID.Listar(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), dbTipo);
            }
            return Lista;
        }

        public List<OID> ListarOidsParcial(string key, string descri)
        {
            string keyDecripto = null;
            List<OID> Lista = new List<OID>();
            var dbTipo = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());

            DataTable dt = new DataTable();
            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }
            if (keyDecripto == chave)
            {
                var ListaTemp = OID.Listar(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), dbTipo);
                foreach (var oid in ListaTemp)
                {
                    if (descri.Contains(oid.Fabricante) && descri.Contains(oid.Modelo) && descri.Contains(oid.Firmware))
                    {
                        Lista.Add(oid);
                    }
                }
            }
            return Lista;
        }

        public string RetornaValor(string key, string query)
        {
            object result = new object();
            string keyDecripto = null;
            var dbTipo = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());

            try
            {
                keyDecripto = dnaPrint.Base.Cripto.Descriptografar(chave, vetor, key);
            }
            catch
            {
            }
            if (keyDecripto == chave)
            {
                DAO.Operacoes op = new DAO.Operacoes(ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString(), dbTipo);
                result = op.ExecuteScalar(query);
            }
            return result.ToString();
        }

    }
}
