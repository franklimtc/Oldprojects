using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hightcharts;
using CSFCenterReports.Controls;
using System.Data;

namespace CSFCenterReports
{
    public partial class Resumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                lbHora.Text = "Emitido em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                if ((bool)Session["vip"] || (bool)Session["admin"])
                {
                    ClientScript.RegisterStartupScript(GetType(), "chart", GraficoGeral(), true);
                }
                else
                {
                    Grupo grupo = Grupo.RetornaGrupo(Session["grupo"].ToString());
                    ClientScript.RegisterStartupScript(GetType(), "chart", GraficoAmbiente(grupo), true);
                }
            }
        }

        private string GraficoAmbiente(Grupo grupo)
        {
            DataTable dtVolumeTotal = CSFCenterReports.Controls.Graficos.VolumeTotalContrato(grupo);
            string xAxisGeral = hightcharts.XAxis.returnXAxis(dtVolumeTotal);
            string SerisGeral = hightcharts.Serie.RetornaString(dtVolumeTotal);
            string ChartGeral = hightcharts.Charts.returnChart("graficoGeral", hightcharts.Charts.types.column.ToString(), "VOLUME MENSAL DE IMPRESSÃO, COPIA E FAX DA UNIDADE", xAxisGeral, "", SerisGeral, DateTime.Now.ToString("yyyy"),"true");

            DataTable dtVolumeUsuarios = CSFCenterReports.Controls.Graficos.VolumeUsuarios(grupo);
            string xAxisUsuarios = hightcharts.XAxis.returnXAxis(dtVolumeUsuarios);
            string SerisUsuarios = hightcharts.Serie.RetornaString(dtVolumeUsuarios);
            string ChartUsuarios = hightcharts.Charts.returnChart("graficoUsuarios", hightcharts.Charts.types.bar.ToString(), "TOP 10 - VOLUME MENSAL POR USUÁRIO", "'Pags'", "", SerisUsuarios, DateTime.Now.ToString("MMM"));

            DataTable dtVolumeImpressora = CSFCenterReports.Controls.Graficos.VolumeImpressoras(grupo);
            string xAxisImpressoras = hightcharts.XAxis.returnXAxis(dtVolumeImpressora);
            string SerisImpressoras = hightcharts.Serie.RetornaString(dtVolumeImpressora);
            string ChartImpressoras = hightcharts.Charts.returnChart("graficoVar", hightcharts.Charts.types.bar.ToString(), "TOP 10 - VOLUME MENSAL POR EQUIPAMENTO", "'Pags'", "", SerisImpressoras, DateTime.Now.ToString("MMM"));

            return ChartGeral + " " + ChartUsuarios + " " + ChartImpressoras;
        }

        private string GraficoGeral()
        {
            DataTable dtVolumeTotal = CSFCenterReports.Controls.Graficos.VolumeTotalContrato();
            string xAxisGeral = hightcharts.XAxis.returnXAxis(dtVolumeTotal);
            string SerisGeral = hightcharts.Serie.RetornaString(dtVolumeTotal);
            string ChartGeral = hightcharts.Charts.returnChart("graficoGeral", hightcharts.Charts.types.line.ToString(), "VOLUME MENSAL DE IMPRESSÃO, COPIA E FAX POR ESTADO", xAxisGeral, "", SerisGeral, DateTime.Now.ToString("yyyy"));

            DataTable dtVolumeUsuarios = CSFCenterReports.Controls.Graficos.VolumeUsuarios();
            string xAxisUsuarios = hightcharts.XAxis.returnXAxis(dtVolumeUsuarios);
            string SerisUsuarios = hightcharts.Serie.RetornaString(dtVolumeUsuarios);
            string ChartUsuarios = hightcharts.Charts.returnChart("graficoUsuarios", hightcharts.Charts.types.bar.ToString(), "TOP 10 - VOLUME MENSAL POR USUÁRIO", "'Pags'", "", SerisUsuarios, DateTime.Now.ToString("MMM"),"true");

            DataTable dtVolumeImpressora = CSFCenterReports.Controls.Graficos.VolumeImpressoras();
            string xAxisImpressoras = hightcharts.XAxis.returnXAxis(dtVolumeImpressora);
            string SerisImpressoras = hightcharts.Serie.RetornaString(dtVolumeImpressora);
            string ChartImpressoras = hightcharts.Charts.returnChart("graficoVar", hightcharts.Charts.types.bar.ToString(), "TOP 10 - VOLUME MENSAL POR EQUIPAMENTO", "'Pags'", "", SerisImpressoras, DateTime.Now.ToString("MMM"), "true");

            return ChartGeral + " " + ChartUsuarios + " " + ChartImpressoras;
        }

        private string GerarChart()
        {
            string xAxis = hightcharts.XAxis.returnXAxis(new string[] { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" });
            hightcharts.Serie serie1 = new hightcharts.Serie("Franklim", new string[] { "10", "15", "2", "15", "2", "15", "30", "10", "15", "2", "15", "2" });
            hightcharts.Serie serie2 = new hightcharts.Serie("Cassio", new string[] { "15", "9", "4", "9", "4", "22", "14", "15", "9", "4", "9", "4" });
            hightcharts.Serie serie3 = new hightcharts.Serie("Jo", new string[] { "12", "8", "16", "8", "16", "17", "26", "12", "8", "16", "8", "16" });
            hightcharts.Serie serie4 = new hightcharts.Serie("Val", new string[] { "13", "8", "16", "8", "16", "19", "15", "13", "8", "16", "8", "16" });

            List<Serie> listaSeries = new List<Serie>();

            listaSeries.Add(serie1);
            listaSeries.Add(serie2);
            listaSeries.Add(serie3);
            listaSeries.Add(serie4);

            string series = serie1.Text + ", " + serie2.Text + ", " + serie3.Text + ", " + serie4.Text;

            return hightcharts.Charts.returnChart("graficoGeral", hightcharts.Charts.types.line.ToString(), "Volume Mensal por Equipamento", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("yyyy"))
                + " " + hightcharts.Charts.returnChart("graficoUsuarios", hightcharts.Charts.types.bar.ToString(), "TOP 5 - Usuários", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("MMM"))
                + " " + hightcharts.Charts.returnChart("graficoArquivos", hightcharts.Charts.types.bar.ToString(), "TOP 5 - Arquivos", xAxis, "", Serie.RetornaString(listaSeries), DateTime.Now.ToString("MMM")
                );

        }
    }
}