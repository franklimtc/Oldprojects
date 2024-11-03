using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Movimentacoes_Relatorio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportViewer1.LocalReport.ReportPath = @"Movimentacoes\RMovimentacoes.rdlc";
            string tSql = string.Format("select * from vw_Movimentacoes");
            DataTable resumoEnvios = DAO.RetornaDt(DAO.connString(), tSql);
            if (ReportViewer1.LocalReport.DataSources.Count > 0)
            {
                ReportViewer1.LocalReport.DataSources.RemoveAt(0);
            }
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Movimentacoes", resumoEnvios));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}