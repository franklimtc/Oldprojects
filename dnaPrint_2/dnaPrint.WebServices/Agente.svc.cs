using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace dnaPrint.WebServices
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Agente" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Agente.svc ou Agente.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Agente : IAgente
    {
        private static string chave = "CsfDigit@l2016";
        private static string connString = ConfigurationManager.ConnectionStrings["dnaPrintWS"].ToString();

        public bool Adicionar(string key, string computador, string serie)
        {
            bool result = false;

            if (key == chave)
            {
                string query = $"exec sp_AtualizarAgentesAtivos '{computador.Trim()}', '{serie.Trim()}'";
                DAO.SQLServer sql = new DAO.SQLServer();

                if (sql.ExecuteNonQuery(connString, query) > 0)
                    result = true;
            }

            return result;
        }

        public bool Ativo(string key, string serie)
        {
            bool result = false;

            if (key == chave)
            {
                string query = $"select count(*) from agentesAtivos where serie = '{serie.Trim()}'";
                DAO.SQLServer sql = new DAO.SQLServer();

                if (int.Parse(sql.ExecuteScalar(connString, query).ToString()) > 0)
                    result = true;
            }
            
            return result;
        }

        public async Task<int> AdicionarAsync(string key, string computador, string serie)
        {
            string query = $"exec sp_AtualizarAgentesAtivos '{computador.Trim()}', '{serie.Trim()}'";
            DAO.SQLServer sql = new DAO.SQLServer();

            return await sql.ExecuteNonQueryAsync(connString, query);
        }
    }
}
