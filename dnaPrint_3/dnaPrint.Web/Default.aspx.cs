using hightcharts;
using System;
using System.Collections.Generic;
using System.Data;
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
                    try
                    {
                        ClientScript.RegisterStartupScript(GetType(), "chart", GerarChart(), true);
                    }
                    catch
                    {
                        Response.Redirect(@"~\Cadastros\Equipamentos.aspx");
                    }
                    
            }
        }
        private string GerarChart()
        {

            var dtInicial = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays((DateTime.Now.Day * -1) + 1).ToString("yyyyMMdd");
            var dtFinal = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays((DateTime.Now.Day * -1)).AddMonths(1).ToString("yyyyMMdd");

            #region Chart1

            string xAxis = hightcharts.XAxis.returnXAxis(new string[] { "Tipo 1", "Tipo 2", "Tipo 3", "Tipo 4", "Tipo 5", "Tipo 6" });

            DataTable dtDados = new DAO.Operacoes(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()))
                .ReturnDt("select tipo, sum(franquia) franquia, sum(contFinal - contInicial) volume  from bilhetagem('" + dtInicial + "','" + dtFinal + "') group by tipo order by 1");

            string[] listaVolume = new string[6];
            string[] listaFranquia = new string[6];
            //if (dtDados.Rows.Count == 6)
            {
                for (int i = 0; i < dtDados.Rows.Count; i++)
                {
                    listaFranquia[i] = dtDados.Rows[i][1].ToString();
                    listaVolume[i] = dtDados.Rows[i][2].ToString();

                }
            }

            hightcharts.Serie Franquia = new hightcharts.Serie("Franquia", listaFranquia);
            hightcharts.Serie Volume = new hightcharts.Serie("Volume", listaVolume);

            List<Serie> listaSeries = new List<Serie>();

            listaSeries.Add(Franquia);
            listaSeries.Add(Volume);

            #endregion

            #region Chart2
            string tsqlChart2 = $"select fila, (contfinal - continicial) volume from bilhetagem('{dtInicial}','{dtFinal}') where volume is not null order by 2 desc limit 5";
            DataTable dtChart2 =  new DAO.Operacoes(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()))
                .ReturnDt(tsqlChart2);



            string[] listaVolEqptos = new string[5];
            string[] listaNomeEqptos = new string[5];

            if (dtChart2.Rows.Count == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    listaNomeEqptos[i] = dtChart2.Rows[i][0].ToString();
                    listaVolEqptos[i] = dtChart2.Rows[i][1].ToString();
                }
            }

            string xChart2 = hightcharts.XAxis.returnXAxis(listaNomeEqptos);

            hightcharts.Serie VolEqptos = new hightcharts.Serie("Volume", listaVolEqptos);
            List<Serie> listaSeriesChart2 = new List<Serie>();
            listaSeriesChart2.Add(VolEqptos);

            #endregion

            #region Chart3
            string tsqlChart3 = $"select username, sum(totalpages * copies) volume from arquivoimpresso where submitted between '{dtInicial}' and '{dtFinal}' group by username order by 2 desc limit 5";
            DataTable dtChart3 = new DAO.Operacoes(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()))
                .ReturnDt(tsqlChart3);

            string[] listaNomeUsuarios = new string[5];
            string[] listaVolUsuarios = new string[5];

            if (dtChart3.Rows.Count == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    listaNomeUsuarios[i] = dtChart3.Rows[i][0].ToString();
                    listaVolUsuarios[i] = dtChart3.Rows[i][1].ToString();
                }
            }

            string xChart3 = hightcharts.XAxis.returnXAxis(listaNomeUsuarios);

            hightcharts.Serie VolUsuarios = new hightcharts.Serie("Volume", listaVolUsuarios);
            List<Serie> listaSeriesChart3 = new List<Serie>();
            listaSeriesChart3.Add(VolUsuarios);


            #endregion

            string series = Franquia.Text + ", " + Volume.Text;

            return hightcharts.Charts.returnChart("Chart1", hightcharts.Charts.types.column.ToString(), "Volume do Mês", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("MMM/yyyy"))
                + " " + hightcharts.Charts.returnChart("Chart2", hightcharts.Charts.types.bar.ToString(), "TOP 5 - Equipamentos", xChart2, "", Serie.RetornaString(listaSeriesChart2), DateTime.Now.ToString("MMM/yyyy"))
                + " " + hightcharts.Charts.returnChart("Chart3", hightcharts.Charts.types.bar.ToString(), "TOP 5 - Usuários", xChart3, "", Serie.RetornaString(listaSeriesChart3), DateTime.Now.ToString("MMM/yyyy")
                );
        }
    }
}