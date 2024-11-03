using hightcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string user = null;

                try
                {
                    user = Session["User"].ToString();
                }
                catch
                {
                    Response.Redirect(@"~\Logon\Default.aspx");
                }
                if (!string.IsNullOrEmpty(user))
                    ClientScript.RegisterStartupScript(GetType(), "chart", GerarChart(), true);
                    
            }
        }
        private string GerarChart()
        {
            string xAxis = hightcharts.XAxis.returnXAxis(new string[] { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" });
            hightcharts.Serie serie1 = new hightcharts.Serie("Franklim", new string[] { "10", "15", "2", "15", "2", "15", "30", "10", "15", "2", "15", "2" });
            hightcharts.Serie serie2 = new hightcharts.Serie("Cassio", new string[] { "15", "9", "4", "9", "4", "22", "14", "15", "9", "4", "9", "4" });
            hightcharts.Serie serie4 = new hightcharts.Serie("Val", new string[] { "13", "8", "16", "8", "16", "19", "15", "13", "8", "16", "8", "16" });

            List<Serie> listaSeries = new List<Serie>();

            listaSeries.Add(serie1);
            listaSeries.Add(serie2);
            listaSeries.Add(serie4);

            string series = serie1.Text + ", " + serie2.Text + ", " + serie4.Text;



            return hightcharts.Charts.returnChart("Chart1", hightcharts.Charts.types.line.ToString(), "Volume Mensal", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("yyyy"))
                + " " + hightcharts.Charts.returnChart("Chart2", hightcharts.Charts.types.bar.ToString(), "TOP 5 - Equipamentos", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("MMM"))
                + " " + hightcharts.Charts.returnChart("Chart3", hightcharts.Charts.types.bar.ToString(), "TOP 5 - Usuários", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("MMM")
                );
        }
    }
}