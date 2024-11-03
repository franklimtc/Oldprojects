using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

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
