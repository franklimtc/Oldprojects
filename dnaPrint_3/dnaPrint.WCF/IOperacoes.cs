using dnaPrint.Base;
using System.Collections.Generic;
using System.ServiceModel;

namespace dnaPrint.WCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IOperacoes" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IOperacoes
    {
        [OperationContract]
        bool Exec(string key, string query, List<string[]> parametros);

        [OperationContract]
        string RetornaValor(string key, string query);

        [OperationContract]
        List<OID> ListarOids(string key);

        [OperationContract]
        List<OID> ListarOidsParcial(string key, string descri);
    }
}
