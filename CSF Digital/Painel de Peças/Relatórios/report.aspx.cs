using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class Relatórios_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region antigo
        //bool calendar = false;
        //if (!IsPostBack)
        //{
        //    bool.TryParse(Request.QueryString["Calendar"], out calendar);
        //    if (!calendar)
        //    {
        //        emitir();
        //    }
        //    else
        //    {
        //        dtInicial.Visible = true;
        //        dtFinal.Visible = true;
        //        tbEmitir.Visible = true;
        //        label1.Visible = true;
        //        label2.Visible = true;
        //    }
        //}
        #endregion
        if (!IsPostBack)
        {
            string report = Request.QueryString["Report"];

            switch (report)
            {
                case "pecasGeral":
                    //dtInicial.Visible = true;
                    tbdtInicial.Visible = true;
                    //dtFinal.Visible = true;
                    tbdtFinal.Visible = true;
                    tbEmitir.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    break;
                case "PecasPendentes":
                    emitirPecasPendentes();
                    break;
                case "ras":
                    emitirRAS();
                    break;
                case "resumoEnvios":
                    emitirResumoEnvios();
                    break;
            }
        }
    }

    private void emitirResumoEnvios()
    {
        ReportViewer1.LocalReport.ReportPath = @"Relatórios\resumoEnvios.rdlc";
        string tSql = string.Format("select * from vw_resumoEnvios order by 4,1");
        DataTable resumoEnvios = DAO.RetornaDt(DAO.connString(), tSql);
        if (ReportViewer1.LocalReport.DataSources.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.RemoveAt(0);
        }
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsResumoEnvios", resumoEnvios));
        ReportViewer1.LocalReport.Refresh();
    }

    private void emitirRAS()
    {
        ReportViewer1.LocalReport.ReportPath = @"Relatórios\ras.rdlc";
        string tSql = "select * from vw_ras";
        DataTable dtras = DAO.RetornaDt(DAO.connString(), tSql);
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ras", dtras));
        ReportViewer1.LocalReport.Refresh();
    }

    protected void emitirPecasPendentes()
    {
        ReportViewer1.LocalReport.ReportPath = @"Relatórios\PecasPendentes2.rdlc";
        string tSql = "select idreqPeca,reqUSD,uf,cidade,unidade,serieEqpto,partNumber,descricao,solicitante,tecnico,dtSolicitacao,qtdDias,Status from pecasPendentes2";
        DataTable PecasPendentes = DAO.RetornaDt(DAO.connString(), tSql);
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("pecas2", PecasPendentes));
        ReportViewer1.LocalReport.Refresh();
    }
    protected void tbEmitir_Click(object sender, EventArgs e)
    {
        ReportViewer1.LocalReport.ReportPath = @"Relatórios\pecasGeral.rdlc";
        string tSql = string.Format("select * from PecasAll where  dtEnvio between '{0}' and '{1}'", DateTime.Parse(tbdtInicial.Text).ToString("dd-MM-yyyy"), DateTime.Parse(tbdtFinal.Text).ToString("dd-MM-yyyy"));
        DataTable PecasPendentes = DAO.RetornaDt(DAO.connString(), tSql);
        if (ReportViewer1.LocalReport.DataSources.Count > 0)
        {
            ReportViewer1.LocalReport.DataSources.RemoveAt(0);
        }
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("pecas", PecasPendentes));
        ReportViewer1.LocalReport.Refresh();
    }
}