using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace EtiquetasSuprimentosService
{
    [ServiceContract]
    public interface IEtiquetasService
    {
        [OperationContract]
        List<etiquetasSuprimentos> Listar(string key);

        [OperationContract]
        void AtualizarTroca(string key, string CRUM, DateTime data);
        [OperationContract]
        void AtualizarTermino(string key, string CRUM, DateTime data, int Producao);
    }
}