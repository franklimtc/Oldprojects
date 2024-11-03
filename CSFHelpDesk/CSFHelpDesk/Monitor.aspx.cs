using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Monitor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string grupo = Account.RetornaGrupo(User.Identity.Name);
            if (grupo != "")
            {
                if (grupo == "Administradores" || grupo == "Operadores")
                {
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        dsRequisicoes.DataBind();
        gvRequisicoes.DataBind();
    }

    protected void gvRequisicoes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string reqCod = gvRequisicoes.Rows[index].Cells[1].Text;
            string url = string.Format("Requisicoes/Chamado.aspx?req={0}", reqCod);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("window.open('{0}', '_blank');", url), true);
        }
    }
}