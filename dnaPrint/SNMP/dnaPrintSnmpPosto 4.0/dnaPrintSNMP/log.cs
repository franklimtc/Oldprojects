using System;
using System.IO;
using System.Reflection;

namespace dnaPrintSNMP
{
    public class log
    {
        public static void escrever(string tipo, string mensagem)
        {
            //string filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string _log = string.Format("{0} : {1} : {2}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), tipo, mensagem);
            //filename += "\\logs\\" + string.Format("log_{0}.txt", DateTime.Now.ToString("dd-MM-yyyy"));
            //DAO.GerarTXT(filename, _log);
            string connstring = string.Format("Data Source={0}\\config\\oids.sdf;Password=Senh@123", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString());
            DAO.ExecuteCompact(connstring, string.Format("insert into logs(componente,data, mensagem) values('{0}',getdate(),'{1}')", tipo, mensagem)); ;
            
        }
    }
}
