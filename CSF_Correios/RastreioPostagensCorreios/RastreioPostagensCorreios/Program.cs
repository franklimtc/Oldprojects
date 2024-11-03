using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RastreioPostagensCorreios
{
    static class Program
    {
        static string connString = ConfigurationManager.ConnectionStrings["dbPecas"].ToString();
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Rastrear()
            };
            ServiceBase.Run(ServicesToRun);
        }


        // Testes
        //static void Main()
        //{
        //    List<Suprimentos> Lista = Suprimentos.Listar(connString);
        //}
    }
}
