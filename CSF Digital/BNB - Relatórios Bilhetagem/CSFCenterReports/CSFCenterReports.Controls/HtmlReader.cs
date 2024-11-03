using System.Text;
using System.Net;

namespace CSFCenterReports.Controls
{
    class HtmlReader
    {
        public static string htmlToString(string url)
        {
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            client.Encoding = Encoding.Default;

            return client.DownloadString(url);
        }

        public static string LimparHtml(string html)
        {
            while (html.Contains("TD class"))
            {
                int inicio = 0;
                int fim = 0;
                inicio = html.IndexOf("TD class");
                fim = html.IndexOf('>', inicio) - inicio;
                string temp = html.Substring(inicio, fim);
                html = html.Replace(temp, "TD");
            }
            html = html.Replace("&nbsp;", " ");
            html = html.Replace("\r\n", "");
            html = html.Substring(html.IndexOf("<TABLE"));

            return html;
        }

        public static string retornarCampoHtml(string Campo, string htmlCode)
        {
            int iniUnd = htmlCode.IndexOf(Campo);
            iniUnd = htmlCode.IndexOf("<TD>", iniUnd);
            int finUnd = htmlCode.IndexOf("</TD>", iniUnd) - iniUnd;

            return htmlCode.Substring(iniUnd, finUnd).Replace("<TD>", "").Trim();
        }
    }
}
