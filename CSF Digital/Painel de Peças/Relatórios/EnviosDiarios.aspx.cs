using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Relatórios_EnviosDiarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void btEmitir_Click(object sender, EventArgs e)
    {
        string tSql = null;
        string report = Request.QueryString["Report"];
        if (ReportViewer1.LocalReport.DataSources.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.RemoveAt(0);
        }


        switch (report)
        {
            case "ras":
                ReportViewer1.LocalReport.ReportPath = @"Relatórios\ras.rdlc";
                tSql = string.Format("select * from func_rasD('{0}')", DateTime.Parse(tbdata.Text).ToString("yyyyMMdd"));
                DataTable dtras = DAO.RetornaDt(DAO.connString(), tSql);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ras", dtras));
                ReportParameter rDate = new ReportParameter("data");
                rDate.Values.Add(DateTime.Parse(tbdata.Text).ToString("yyyyMMdd"));
                ReportViewer1.LocalReport.SetParameters(rDate);
                ReportViewer1.LocalReport.Refresh();
                break;
            default:
                ReportViewer1.LocalReport.ReportPath = @"Relatórios\listaEnvios.rdlc";
                tSql = string.Format("select * from vw_listaEnvios where dtEnvio >= '{0}'", DateTime.Parse(tbdata.Text).ToString("yyyyMMdd"));
                DataTable dtEnviosDiarios = DAO.RetornaDt(DAO.connString(), tSql);
                if (ReportViewer1.LocalReport.DataSources.Count > 0)
                {
                    ReportViewer1.LocalReport.DataSources.RemoveAt(0);
                }
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("listaEnvios", dtEnviosDiarios));
                ReportViewer1.LocalReport.Refresh();
                break;
        }


    }
}