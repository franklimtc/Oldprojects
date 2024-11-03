using System;
using System.IO;
using System.Reflection;

namespace dnaPrint
{
    class Logs
    {
        public enum TipoLogs { geral, snmp, email };

        private static string dirAtual = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string dirLogs = dirAtual + @"\Logs";
        private static string dataAtual = DateTime.Now.ToString("dd-MM-yy");

        private static string LogGeral = dirLogs + @"\GERAL_" + dataAtual + ".txt";
        private static string LogSNMP = dirLogs + @"\SNMP_" + dataAtual + ".txt";
        private static string LogEmail = dirLogs + @"\Email_" + dataAtual + ".txt";


        public static void GerarLogs(TipoLogs tipo, string Mensagem)
        {
            switch (tipo)
            {
                case TipoLogs.email:
                    DAO.GerarTXT(LogEmail, DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + " : " + Mensagem);
                    break;
                case TipoLogs.geral:
                    DAO.GerarTXT(LogGeral, DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + " : " + Mensagem);
                    break;
                case TipoLogs.snmp:
                    DAO.GerarTXT(LogSNMP, DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + " : " + Mensagem);
                    break;
            }
        }
    }
}
