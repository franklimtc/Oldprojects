using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Gerenciar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Account.Administrador(User.Identity.Name.ToLower()))
        {
            Response.Redirect("~/Default.aspx");
        }
       
    }

    protected void btAssociar_Click(object sender, EventArgs e)
    {
        if (Account.associarUsuarioGrupo(dpUser.SelectedValue, dpRole.SelectedValue))
        {
            dsUsers.DataBind();
            dpUser.DataBind();
            dsUsersinRoles.DataBind();
            gvUsuarios.DataBind();
        }

    }
}