using System;
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
            //printerjob.coletarjobs(directory.getcurrentdirectory(), datetime.now);
            Operacoes.EfetuarLeitura();
        }

    }
}
