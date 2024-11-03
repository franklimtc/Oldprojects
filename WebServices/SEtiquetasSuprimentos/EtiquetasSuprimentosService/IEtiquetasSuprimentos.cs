using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EtiquetasSuprimentosService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEtiquetasSuprimentos" in both code and config file together.
    [ServiceContract]
    public interface IEtiquetasSuprimentos
    {
        [OperationContract]
        List<vw_EtiquetasSuprimentos> Listar(string key, string Status, int idLista);

        [OperationContract]
        void AtualizarProducao(string key, string CRUM, DateTime data, int Producao, int ValorAtual);

        [OperationContract]
        void AtualizarTroca(string key, string CRUM, DateTime data);

        [OperationContract]
        void AtualizarTermino(string key, string CRUM, DateTime data, int Producao, int ValorAtual);

        [OperationContract]
        long QuantidadeListas();
    }
}
