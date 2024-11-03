using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_Monitor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //dsMonitor.DataBind();
            //gvMonitor.DataBind();
        }
    }
    protected void gvMonitor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvMonitor.SelectedRow;
        string url = string.Format(@"Atualizar.aspx?idreqAtendimento=" + Convert.ToString(row.Cells[1].Text));
        Response.Redirect(url);
    }
}