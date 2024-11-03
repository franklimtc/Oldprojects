using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECT.Rastro;
using CSF.Objetos;
using System.Threading;

namespace Testes
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread Reverso = new Thread(VerificarReversos);
            Reverso.Start();

            Thread Envios = new Thread(VerificarEnviosSuprimentos);
            Envios.Start();
        }
        
        static void VerificarReversos()
        {
            #region Postagens Reversas

            IQueryable<Retornos> lista = Retornos.ListarTodos();

            if (lista.Count() > 0)
            {
                Console.WriteLine($"{DateTime.Now.ToString()} - Serão rastreadas {lista.Count()} postagens reversas!");

                returnObjeto obj = new returnObjeto();
                List<returnObjetoEvento> listaEventos = null;

                foreach (Retornos r in lista)
                {
                    obj = Rastreio.RastrearObjeto(r.Postagem);

                    if (obj.evento != null)
                    {
                        listaEventos = obj.evento.ToList();
                    }
                    else
                    {
                        Console.WriteLine($"{DateTime.Now.ToString()} - Postagem reversa {r.Postagem} não coletada!");
                        listaEventos = null;
                    }
                    if (listaEventos != null)
                    {
                        if (listaEventos[0].descricao == "Objeto entregue ao destinatário")
                        {
                            r.Status = 3;
                            Console.WriteLine($"{DateTime.Now.ToString()} - Postagem reversa {r.Postagem} entregue!");
                        }
                        else
                        {
                            r.Status = 2;
                            Console.WriteLine($"{DateTime.Now.ToString()} - Postagem reversa {r.Postagem} em trânsito!");
                        }
                        r.Atualizar();
                    }
                }
            }
            Console.WriteLine("Fim do rastreamento de postagens reversas!");
            #endregion
        }

        static void VerificarEnviosSuprimentos()
        {
            #region Envios de Suprimentos

            IQueryable<EnvioSuprimento> listaEnvios = EnvioSuprimento.ListarTodos();

            if (listaEnvios.Count() > 0)
            {
                Console.WriteLine($"{DateTime.Now.ToString()} - Serão rastreadas {listaEnvios.Count()} postagens de suprimentos!");

                returnObjeto obj = new returnObjeto();
                List<returnObjetoEvento> listaEventos = null;

                foreach (EnvioSuprimento r in listaEnvios)
                {
                    bool continuar = false;
                    try
                    {
                        obj = Rastreio.RastrearObjeto(r.Postagem);
                        continuar = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (continuar)
                    {
                        if (obj.evento != null)
                        {
                            listaEventos = obj.evento.ToList();
                        }
                        else
                        {
                            Console.WriteLine($"{DateTime.Now.ToString()} - Postagem {r.Postagem} não coletada!");
                            listaEventos = null;
                        }
                        if (listaEventos != null)
                        {
                            if (listaEventos[0].descricao == "Objeto entregue ao destinatário")
                            {
                                Console.WriteLine($"{DateTime.Now.ToString()} - Postagem {r.Postagem} entregue!");
                            }
                            else
                            {
                                Console.WriteLine($"{DateTime.Now.ToString()} - Postagem {r.Postagem} em trânsito!");
                            }
                            r.dataEntrega = DateTime.Parse(listaEventos[0].data);
                            r.Status = listaEventos[0].descricao;
                            r.Atualizar();
                        }
                    }

                }
            }

            #endregion

            Console.WriteLine("Fim ddo rastreamento de suprimentos");

        }
    }
}
