using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Cadastros_Roles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbUsuarios.SelectedIndex = 0;
            dtUsuarios.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
            dtUsuarios.SelectCommand = "select UserId, UserName from aspnet_users where username <> '" + User.Identity.Name + "'";
            dtUsuarios.DataBind();

        }
    }
    protected void btAdicionar_Click(object sender, EventArgs e)
    {
        string tsql = string.Format("insert into aspnet_Roles(ApplicationId, RoleId, RoleName,LoweredRoleName,Description) (select ApplicationId, NEWID(),'{0}', LOWER('{0}'),'{1}' from aspnet_Applications where ApplicationName='/')", tbName.Text, tbDescricao.Text);
        DAO.Execute(tsql, "ApplicationServices");

        dtRoles.DataBind();
        gvRoles.DataBind();
    }

    protected void cklRoles_DataBound(object sender, EventArgs e)
    {
        CheckBoxList cbl = (CheckBoxList)sender;
        foreach (ListItem l in cbl.Items)
        {
            if (l.Value.Contains("True")) //or whatever check you need to do
            {
                l.Selected = true;
            }
        }
    }
    protected void lbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtUsersinRoles.DataBind();
        cklRoles.DataBind();
    }

    protected void btAtualizar_Click(object sender, EventArgs e)
    {
        string tsqlRemoveRoleUsers = string.Format("delete aspnet_UsersInRoles where UserId = '{0}'", lbUsuarios.SelectedValue.ToString());
        string tsqlInsert = null;

        DAO.Execute(tsqlRemoveRoleUsers, "ApplicationServices");
        foreach (ListItem l in cklRoles.Items)
        {
            if (l.Selected)
            {
                tsqlInsert = string.Format("insert into aspnet_UsersInRoles(UserId, RoleId) VALUES('{0}','{1}')",
                    lbUsuarios.SelectedValue.ToString(), l.Value.Replace(" True", "").Replace(" False", ""));
                DAO.Execute(tsqlInsert, "ApplicationServices");
                tsqlInsert = null;
            }
        }

        cklRoles.DataBind();
    }
}