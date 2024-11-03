using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace dnaPrint.Web.Relatorios
{
    public partial class Bilhetagem : System.Web.UI.Page
    {
        private static List<Base.Bilhetagem> listaBilhetagem = null;

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

        protected void tbExibir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbdtInicial.Text) && !string.IsNullOrEmpty(tbdtFinal.Text))
            {
                
                DateTime dtInicial = DateTime.Parse(tbdtInicial.Text);
                DateTime dtFinal = DateTime.Parse(tbdtFinal.Text);

                listaBilhetagem = Base.Bilhetagem.Listar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), dtInicial, dtFinal);

                if (listaBilhetagem.Count > 0)
                {
                    gvBilhetagem.DataSource = listaBilhetagem;
                    gvBilhetagem.DataBind();
                    Report.Visible = false;
                    gvBilhetagem.Visible = true;
                    tbExportar.Enabled = true;
                }
                else
                {
                    tbExportar.Enabled = false;
                }
            }
        }

        protected void tbExportar_Click(object sender, EventArgs e)
        {
            gvBilhetagem.Visible = false;

            Report.LocalReport.ReportPath = "Relatorios/Bilhetagem.rdlc";
            ReportDataSource ds = new ReportDataSource("dsBilhetagem2", listaBilhetagem);
            Report.LocalReport.DataSources.Add(ds);
            Report.DataBind();

            Report.Visible = true;
        }
    }
}