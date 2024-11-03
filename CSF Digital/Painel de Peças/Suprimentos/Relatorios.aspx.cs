using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Relatorios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbDtInicial.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            tbDtFinal.Text = DateTime.Now.ToShortDateString();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RangeValidator1.MinimumValue = DateTime.Now.AddDays(-90).Date.ToString("dd-MM-yy");
            RangeValidator1.MaximumValue = DateTime.Now.Date.ToString("dd-MM-yy");

            RangeValidator2.MinimumValue = DateTime.Now.AddDays(-90).Date.ToString("dd-MM-yy");
            RangeValidator2.MaximumValue = DateTime.Now.Date.ToString("dd-MM-yy");
        }
        

    }

    protected void btPesquisar_Click(object sender, EventArgs e)
    {
        pnGv.Visible = true;
        pnReport.Visible = false;
        gvEnvios.DataBind();
    }

    protected void gvEnvios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvEnvios.Rows[Convert.ToInt32(e.CommandArgument)];

        switch (e.CommandName)
        {
            case "Excluir":
                #region Comando Excluir
                string user = User.Identity.AuthenticationType;
                if (Usuarios.Administrador(User.Identity.Name))
                {
                    Envios.Excluir(row.Cells[0].Text);
                }
                gvEnvios.DataBind();
                #endregion
                break;
            default:
                break;
        }
    }

    protected void btReport_Click(object sender, EventArgs e)
    {
        pnGv.Visible = false;
        pnReport.Visible = true;

        ReportViewer1.LocalReport.ReportPath = @"Suprimentos\ReportEnvioSuprimento.rdlc";
        string tSql = string.Format("select * from enviosSuprimentos WHERE dtenvio between @dtInicial and DATEADD(DAY,1,@dtFinal)");
        List<string[]> parametros = new List<string[]>();

        parametros.Add(new string[] { "@dtInicial", tbDtInicial.Text });
        parametros.Add(new string[] { "@dtFinal", tbDtFinal.Text });

        DataTable listaEnvios = DAO.RetornaDt(DAO.connString(), tSql, parametros);
        //List<EnvioSuprimento> listaEnvios = EnvioSuprimento.ListarPorData(DateTime.Parse(tbDtInicial.Text), DateTime.Parse(tbDtFinal.Text));

        if (listaEnvios.Rows.Count > 0)
        {
            if (ReportViewer1.LocalReport.DataSources.Count > 0)
            {
                ReportViewer1.LocalReport.DataSources.RemoveAt(0);
            }
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsEnvios", listaEnvios));
            ReportViewer1.LocalReport.Refresh();
        }
    }
}