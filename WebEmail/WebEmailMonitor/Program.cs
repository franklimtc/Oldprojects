using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEmailMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Email.ListarEmailsPendentes().ForEach(x => x.EnviaMensagemEmail());
        }
    }
}
