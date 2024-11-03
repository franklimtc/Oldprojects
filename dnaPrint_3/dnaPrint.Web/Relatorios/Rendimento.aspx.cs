using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Relatorios
{
    public partial class Rendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string user = null;
                bool continuar = false;
                try
                {
                    user = Session["User"].ToString();
                    continuar = true;
                }
                catch
                {
                    Response.Redirect(@"~\Logon\Default.aspx");
                }
                if (continuar)
                {
                    List<dnaPrint.Base.RendimentoSuprimento> lista = dnaPrint.Base.RendimentoSuprimento.Listar(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()));
                    Report.LocalReport.ReportPath = "Relatorios/RendimentoSuprimentos.rdlc";
                    ReportDataSource ds = new ReportDataSource("dsRendimento", lista);
                    Report.LocalReport.DataSources.Add(ds);
                    Report.DataBind();
                    Report.Visible = true;
                }
            }
        }
    }
}