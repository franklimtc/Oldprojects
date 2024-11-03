using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Relatorios
{
    public partial class Impressao : System.Web.UI.Page
    {
        static List<Base.PrintJob> listaJobs = null;
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
            }
        }

        protected void btPesquisarEquipamento_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbEquipamento.Text) && !string.IsNullOrEmpty(tbEqptoDtIni.Text) && !string.IsNullOrEmpty(tbEqptoDtFin.Text))
            {
                listaJobs = null;
                listaJobs = Base.PrintJob.ListByPrinter(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), tbEquipamento.Text.Trim(),
                    DateTime.Parse(tbEqptoDtIni.Text),
                    DateTime.Parse(tbEqptoDtFin.Text));
                gvImpressoes.DataSource = listaJobs;
                gvImpressoes.DataBind();
                btExportarEquipamento.Enabled = true;

                gvImpressoes.Visible = true;
                Report.Visible = false;
            }
        }

        protected void btPesquisarUsuario_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsuario.Text) && !string.IsNullOrEmpty(tbUserDtIni.Text) && !string.IsNullOrEmpty(tbUserDtFin.Text))
            {
                listaJobs = null;
                listaJobs = Base.PrintJob.ListByUser(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), tbUsuario.Text.Trim(),
                    DateTime.Parse(tbUserDtIni.Text),
                    DateTime.Parse(tbUserDtFin.Text));
                gvImpressoes.DataSource = listaJobs;
                gvImpressoes.DataBind();
                btExportarUsuario.Enabled = true;

                gvImpressoes.Visible = true;
                Report.Visible = false;
            }
        }

        protected void btExportarEquipamento_Click(object sender, EventArgs e)
        {
            ExcluirDataSources();

            if (listaJobs != null)
            {
                Report.LocalReport.ReportPath = "Relatorios/Impressoes.rdlc";
                ReportDataSource ds = new ReportDataSource("dsImpressoes", listaJobs);
                Report.LocalReport.DataSources.Add(ds);
                Report.DataBind();

                gvImpressoes.Visible = false;
                Report.Visible = true;
            }
        }

        private void ExcluirDataSources()
        {
            try
            {
                if (Report.LocalReport.DataSources.Count > 0)
                {
                    foreach (var item in Report.LocalReport.DataSources)
                    {
                        Report.LocalReport.DataSources.Remove(item);
                    }
                }
            }
            catch (Exception)
            {

            }
           
        }

        protected void btExportarUsuario_Click(object sender, EventArgs e)
        {
            ExcluirDataSources();

            if (listaJobs != null)
            {
                Report.LocalReport.ReportPath = "Relatorios/Impressoes.rdlc";
                ReportDataSource ds = new ReportDataSource("dsImpressoes", listaJobs);
                Report.LocalReport.DataSources.Add(ds);
                Report.DataBind();

                gvImpressoes.Visible = false;
                Report.Visible = true;
            }
        }
    }
}