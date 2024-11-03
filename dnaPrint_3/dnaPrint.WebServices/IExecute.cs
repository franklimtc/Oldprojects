using System.Collections.Generic;
using System.ServiceModel;
using System.Data;
using dnaPrint.Base;

namespace dnaPrint.WebServices
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IExecute" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IExecute
    {
        [OperationContract]
        bool Exec(DAO.Operacoes.tipo Tipo, string key, string query, List<string[]> parametros);
        [OperationContract]
        object RetornaValor(DAO.Operacoes.tipo Tipo, string key, string query);
        [OperationContract]
        OID[] ListarOids(DAO.Operacoes.tipo Tipo, string key);
    }
}
