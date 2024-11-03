using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Parametros.WebService;

namespace dnaPrintSNMP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new dnaPrintService() 
            };
            ServiceBase.Run(ServicesToRun);
        }

        //Modo debug
        #region Modo debug

        //public static void Main(string[] args)
        //{
        //    disparo.iniciar();
        //}
        #endregion
    }
}

