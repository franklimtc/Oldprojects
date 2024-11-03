using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MonitorEtqSuprimentos
{
    class Program
    {
        static string key = "c$fDigit@l2017";
        static void Main(string[] args)
        {

            AtualizarProducao();
            AtualizarSuprimentosEmUso();
        }

        static void AtualizarSuprimentosEmUso()
        {
            wsEtqSuprimentos.EtiquetasSuprimentosClient client = new wsEtqSuprimentos.EtiquetasSuprimentosClient();
            DBTrocas dbx = new DBTrocas();
            IList<wsEtqSuprimentos.vw_EtiquetasSuprimentos> lista;

            //Verificar itens em estoque
            int qtdListas = int.Parse(client.QuantidadeListas().ToString());
            for (int i = 0; i < qtdListas; i++)
            {
                lista = client.Listar(key, "Em estoque", i).ToList();

                foreach (var etq in lista)
                {
                    try
                    {
                        var Achou = dbx.ControleTrocaCilindro.Where(x => x.serial.Contains(etq.serialSuprimento)).First();
                        client.AtualizarTroca(key, etq.serialSuprimento, DateTime.Parse(Achou.data.ToString()));
                    }
                    catch
                    {

                    }
                }
            }

            client.Close();
        }

        static void AtualizarProducao()
        {
            wsEtqSuprimentos.EtiquetasSuprimentosClient client = new wsEtqSuprimentos.EtiquetasSuprimentosClient();
            DBTrocas dbx = new DBTrocas();
            IList<wsEtqSuprimentos.vw_EtiquetasSuprimentos> lista;
            int qtdListas = int.Parse(client.QuantidadeListas().ToString());

            dnaPrintEntities dbxProducao = new dnaPrintEntities();

            for (int i = 0; i < qtdListas; i++)
            {
                lista = client.Listar(key, "Em uso", i).ToList();
                foreach (var etq in lista)
                {
                    try
                    {
                        if (dbx.vw_relatorio_suprimentos.Where(x => x.serialFoto == etq.serialSuprimento).Count() == 0)
                        {
                            var Achou = dbxProducao.Func_ProducaoCilindro(etq.serialSuprimento).First();
                            client.AtualizarTermino(key, etq.serialSuprimento, (DateTime)Achou.dataFinal, (int)Achou.Produção, (int)Achou.Cilindro);
                        }
                        else
                        {
                            var Achou = dbxProducao.Func_ProducaoCilindro(etq.serialSuprimento).First();
                            client.AtualizarProducao(key, etq.serialSuprimento, DateTime.Parse(Achou.data.ToString()), (int)Achou.Produção, (int)Achou.Cilindro);
                        }

                    }
                    catch
                    {
                    }
                }

            }


            client.Close();
        }
    }
}
