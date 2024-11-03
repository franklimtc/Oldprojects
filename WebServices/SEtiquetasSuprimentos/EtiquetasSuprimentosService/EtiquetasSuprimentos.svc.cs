using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EtiquetasSuprimentosService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EtiquetasSuprimentos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EtiquetasSuprimentos.svc or EtiquetasSuprimentos.svc.cs at the Solution Explorer and start debugging.
    public class EtiquetasSuprimentos : IEtiquetasSuprimentos
    {
        private static string chave = @"c$fDigit@l2017";

        public void AtualizarTermino(string key, string CRUM, DateTime data, int Producao, int ValorAtual)
        {
            if (key == chave)
            {
                using (mEtiquetas ctx = new mEtiquetas())
                {
                    etiquetasSuprimentos etiqueta = ctx.etiquetasSuprimentos.Where(x => x.serialSuprimento == CRUM).First();
                    etiqueta.dtTermino = data;
                    etiqueta.status = "Utilizado";
                    etiqueta.producao = Producao;
                    etiqueta.valorAtual = ValorAtual;
                    ctx.SaveChanges();
                }
            }

        }

        public void AtualizarProducao(string key, string CRUM, DateTime data, int Producao, int ValorAtual)
        {
            if (key == chave)
            {
                using (mEtiquetas ctx = new mEtiquetas())
                {
                    var etiqueta = ctx.etiquetasSuprimentos.Where(x => x.serialSuprimento == CRUM).First();
                    etiqueta.dtTroca = data;
                    etiqueta.producao = Producao;
                    etiqueta.valorAtual = ValorAtual;
                    etiqueta.dtAtualizacao = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
        }

        public void AtualizarTroca(string key, string CRUM, DateTime data)
        {
            if (key == chave)
            {
                using (mEtiquetas ctx = new mEtiquetas())
                {
                    etiquetasSuprimentos etiqueta = ctx.etiquetasSuprimentos.Where(x => x.serialSuprimento == CRUM).First();
                    etiqueta.dtTroca = data;
                    etiqueta.status = "Em uso";
                    etiqueta.dtAtualizacao = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
        }

        public List<vw_EtiquetasSuprimentos> Listar(string key, string Status, int idLista)
        {
            if (key == chave)
            {
                DBEtiquetas ctx = new DBEtiquetas();
                return ctx.vw_EtiquetasSuprimentos.Where(x => x.status == Status & x.Grupo == idLista).ToList();
            }
            else
            {
                return new List<vw_EtiquetasSuprimentos>();
            }
        }

        public long QuantidadeListas()
        {
            DBEtiquetas ctx = new DBEtiquetas();
            var result = ctx.vw_EtiquetasSuprimentos.OrderByDescending(x => x.Grupo).First();
            return (long)result.Grupo;
        }
    }
}
