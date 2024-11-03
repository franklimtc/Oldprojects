using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class postagens : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvPecas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvPecas.Rows[Convert.ToInt32(e.CommandArgument)];
        int idReqPeca = Convert.ToInt32(row.Cells[0].Text);

        switch (e.CommandName)
        {
            case "Confirmar":
                reqPecas.ConfirmarEntrega(idReqPeca, User.Identity.Name);
                break;
            default:
                break;
        }
        gvPecas.DataBind();
    }

    protected void gvSuprimentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvSuprimentos.Rows[Convert.ToInt32(e.CommandArgument)];
        int idReqSupr = Convert.ToInt32(row.Cells[0].Text);

        switch (e.CommandName)
        {
            case "Confirmar":
                reqSuprimentos.ConfirmarEntrega(idReqSupr, User.Identity.Name);
                break;
            default:
                break;
        }
        gvSuprimentos.DataBind();
    }
}