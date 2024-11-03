using System.Collections.Generic;
using System.ServiceModel;
using System.Data;

namespace dnaPrint.WebServices
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IExecute" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IExecute
    {
        [OperationContract]
        bool Exec(string key, string query, List<string[]> parametros);
        [OperationContract]
        string RetornaValor(string key, string query);
        [OperationContract]
        List<string[]> ListarOids(string key);
    }
}
