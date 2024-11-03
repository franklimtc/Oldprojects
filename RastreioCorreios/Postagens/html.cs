using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Postagens
{
    public class html
    {
        public static logPostgem Rastrear(string postagem)
        {
            string url = string.Format(@"http://websro.correios.com.br/sro_bin/txect01$.QueryList?P_LINGUA=001&P_TIPO=001&P_COD_UNI={0}", postagem);

            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            Stream dataStream = res.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream,Encoding.Default);
            string resposta = reader.ReadToEnd();
            reader.Close();
            res.Close();

            logPostgem log  = UltimoLog(resposta);
            log.Postagem = postagem;

            return log;
        }

        private static logPostgem UltimoLog(string resposta)
        {

            int i1 = resposta.IndexOf("<table");
            int f1 = resposta.IndexOf("</TABLE>");

            try
            {
                resposta = resposta.Substring(i1, (f1 - i1) + 8).Replace("<font color=\"000000\">", "");
            }
            catch
            {

            }

            logPostgem log = new logPostgem();
            string rTemp = "";

            string inicio = "<tr>";
            string fim = "</tr>";
            bool ultimoStatus = false;
            while (!ultimoStatus)
            {
                try
                {
                    rTemp = resposta.Substring(resposta.IndexOf(inicio), resposta.IndexOf(fim) - resposta.IndexOf(inicio) + 5);

                    resposta = resposta.Replace(rTemp, "");

                    if (!rTemp.Contains("<b>Data</b>") && !rTemp.Contains("<b>Local</b>"))
                    {
                        ultimoStatus = true;
                        log = ExtrairLog(rTemp);
                    }
                }
                catch
                {
                    ultimoStatus = true;
                }
               
            }

            return log;
        }

        private static logPostgem ExtrairLog(string rTemp)
        {
             logPostgem log = new logPostgem();
            string logTemp2 = "";
            string logFinal = "";
            while (rTemp.Length > 0)
            {
                int inicio = rTemp.IndexOf('>') + 1;
                int fim = rTemp.IndexOf('<', inicio);
                if (fim > inicio || fim == inicio)
                {
                    logTemp2 = rTemp.Substring(inicio, fim - inicio);
                    rTemp = rTemp.Replace(rTemp.Substring(0, fim), "");
                    if (logTemp2 != "")
                        if (logFinal != "")
                            logFinal += @"|" + logTemp2;
                        else
                            logFinal += logTemp2;
                }
                else
                {
                    rTemp = "";
                }
            }

            string[] temp = logFinal.Split('|');
            if (temp.Length == 3)
            {
                log.Data = temp[0];
                log.Local = temp[1];
                log.Status = temp[2];
            }
            return log;
        }

    }
}
