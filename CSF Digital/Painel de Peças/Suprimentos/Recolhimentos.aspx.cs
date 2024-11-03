using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Recolhimentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            mvRecolhimentos.ActiveViewIndex = 0;
            lbUsername.Text = User.Identity.Name;
        }
    }

    protected void gvRecolhimentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvRecolhimentos.Rows[Convert.ToInt32(e.CommandArgument)];

        switch (e.CommandName)
        {
            case "Atender":
                string tsqlUpdate = string.Format("update controleRecolhimento set responsavel = '{0}' where idRecolhimento = {1};",
                    User.Identity.Name.ToUpper(), row.Cells[1].Text);
                DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
                gvRecolhimentos.DataBind();
                break;
            default:
                break;
        }
    }

    protected void btChamadosUsuario_Click(object sender, EventArgs e)
    {
        switch (btChamadosUsuario.Text)
        {
            case "Meus Recolhimentos":
                //dsRecolhimentosUsuario.SelectParameters.Add("responsavel", User.Identity.Name);
                //dsRecolhimentosUsuario.SelectCommand = string.Format("select * from controleRecolhimento where responsavel = @responsavel");
                mvRecolhimentos.ActiveViewIndex = 1;
                btChamadosUsuario.Text = "Todos os Recolhimentos";
                dsRecolhimentosUsuario.DataBind();
                gvRecolhimentosUsuario.DataBind();
                break;
            case "Todos os Recolhimentos":
                mvRecolhimentos.ActiveViewIndex = 0;
                btChamadosUsuario.Text = "Meus Recolhimentos";
                break;
            default:
                break;
        }
    }





   
}