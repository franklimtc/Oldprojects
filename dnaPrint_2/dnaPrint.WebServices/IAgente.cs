using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace dnaPrint.WebServices
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IAgente" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IAgente
    {
        [OperationContract]
        bool Ativo(string key, string serie);

        [OperationContract]
        bool Adicionar(string key, string computador, string serie);
    }
}
