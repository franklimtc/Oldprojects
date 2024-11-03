using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.ObjectModel;


namespace dnaprint.Testes
{
    class Program
    {
        //Executando Scripts PowerShell
        static void Main()
        {

        }
        //static void Main(string[] args)
        //{
        //    List<dadosEqpto> listaDadosEqptos = dadosEqpto.Listar();
        //    int cont = 0;
        //    foreach (dadosEqpto eqpto in listaDadosEqptos)
        //    {
        //        cont++;
        //        Console.WriteLine($"{DateTime.Now} - {cont} de {listaDadosEqptos.Count}!");
        //        eqpto.Suprimento = Suprimentos.BuscarSuprimento(eqpto.SerialToner, eqpto.Serie.Trim());

        //        int tonerPr, tonerAm, tonerMa, tonerCi;
        //        int producaoTotal, duracaoEstimada;

        //        if (eqpto.Suprimento != null)
        //            if (DateTime.Now.Subtract(eqpto.Data).Days < 20) //1ª Condição - Possuir dados atualizados
        //            {
        //                {
        //                    switch (eqpto.Modelo)
        //                    {
        //                        case "SL-M4020D":
        //                            producaoTotal = eqpto.Toner_pr * 100;
        //                            if (eqpto.Media > 0)
        //                            {
        //                                duracaoEstimada = producaoTotal / eqpto.Media;
        //                            }
        //                            else
        //                            {
        //                                duracaoEstimada = producaoTotal / 100;
        //                            }
        //                            if (duracaoEstimada <= 35)
        //                            {
        //                                //bool solicitar = true;
        //                                Console.WriteLine($"{DateTime.Now} - Solicitar suprimento para equipamento de serie {eqpto.Serie}, suprimento com {eqpto.Toner_pr}% e duração estimada de {duracaoEstimada} dias.");
        //                                Log.Adicionar("Solitacoes.txt", Log.tpLog.Info, $"Solicitar suprimento para equipamento de serie {eqpto.Serie}, suprimento com {eqpto.Toner_pr}% e duração estimada de {duracaoEstimada} dias.");
        //                                Log.Adicionar("ListaSolicitacoes.txt", Log.tpLog.Info, $"execute SolicitarSuprimento '{eqpto.Serie}','Toner',{eqpto.ContFinal}, {eqpto.Toner_pr}, {duracaoEstimada} ;");
        //                            }
        //                            break;
        //                        case "SL-M4070":
        //                            producaoTotal = eqpto.Toner_pr * 100;
        //                            if (eqpto.Media > 0)
        //                            {
        //                                duracaoEstimada = producaoTotal / eqpto.Media;
        //                            }
        //                            else
        //                            {
        //                                duracaoEstimada = producaoTotal / 100;
        //                            }
        //                            if (duracaoEstimada <= 35)
        //                            {
        //                                //bool solicitar = true;
        //                                Console.WriteLine($"{DateTime.Now} - Solicitar suprimento para equipamento de serie {eqpto.Serie}, suprimento com {eqpto.Toner_pr}% e duração estimada de {duracaoEstimada} dias.");
        //                                Log.Adicionar("Solitacoes.txt", Log.tpLog.Info, $"Solicitar suprimento para equipamento de serie {eqpto.Serie}, suprimento com {eqpto.Toner_pr}% e duração estimada de {duracaoEstimada} dias.");
        //                                Log.Adicionar("ListaSolicitacoes.txt", Log.tpLog.Info, $"execute SolicitarSuprimento '{eqpto.Serie}','Toner',{eqpto.ContFinal}, {eqpto.Toner_pr}, {duracaoEstimada} ;");

        //                            }
        //                            break;
        //                        case "C4062FX":
        //                            break;
        //                        case "CLP680":
        //                            break;
        //                        default:
        //                            break;
        //                    }

        //                }
        //            }
        //    }
        //} 
    }
}
