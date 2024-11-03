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
    public partial class Suprimentos : System.Web.UI.Page
    {
        private static List<Base.Suprimentos> lista = null;
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
                {
                    lista = Base.Suprimentos.Listar(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString()));
                    gvSuprimentos.DataSource = lista;
                    gvSuprimentos.DataBind();
                }

            }
        }

        protected void tbAtualizar_Click(object sender, EventArgs e)
        {
            Report.Visible = false;
            gvSuprimentos.Visible = true;

            lista = Base.Suprimentos.Listar(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString()));
            gvSuprimentos.DataSource = lista;
            gvSuprimentos.DataBind();
        }

        protected void tbExportar_Click(object sender, EventArgs e)
        {
            gvSuprimentos.Visible = false;

            Report.LocalReport.ReportPath = "Relatorios/Suprimentos.rdlc";
            ReportDataSource ds = new ReportDataSource("dsSuprimentos", lista);
            Report.LocalReport.DataSources.Add(ds);
            Report.DataBind();

            Report.Visible = true;
        }
    }
}