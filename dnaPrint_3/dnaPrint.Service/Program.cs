using dnaPrint.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;

namespace dnaPrint.Service
{
    class Program
    {
        //static void Main()
        //{
        //    ServiceBase[] ServicesToRun;
        //    ServicesToRun = new ServiceBase[]
        //    {
        //        new dnaPrint()
        //    };
        //    ServiceBase.Run(ServicesToRun);
        //}

        //debug

        static void Main()
        {
            if (ConfigurationManager.AppSettings["SNMP"].ToString() == "1")
            {
                DAO.Operacoes.tipo tpDB = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());
                string connString = ConfigurationManager.ConnectionStrings["dnaPrint"].ToString();

                while (true)
                {
                    Console.WriteLine("Informe o endereço ip desejado");
                    string ip = Console.ReadLine();
                    if (ip.ToString() != null && ip != "")
                    {
                        List<Equipamento> lista = Equipamento.ListarEquipamentosPorIp(tpDB, connString, ip);
                        Operacoes.EfetuarLeitura(lista);
                    }
                    else
                    {
                        Operacoes.EfetuarLeituraRapida();
                    }
                }

            }
            //    if (ConfigurationManager.AppSettings["Jobs"].ToString() == "1")
            //    {
            //        PrinterJob.ColetarJobs(Directory.GetCurrentDirectory(), DateTime.Now);
            //    }
            //}

        }
    }
}
