using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatórios_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvDados.Visible = false;
        }
    }

    protected void btEmitir_Click(object sender, EventArgs e)
    {
        dsDadosEnvios.DataBind();
        gvDados.DataBind();
        gvDados.Visible = true;
    }
}