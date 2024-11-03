using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_ConfirmarEntrega : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvConfirmar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvConfirmar.Rows[Convert.ToInt32(e.CommandArgument)];
        int id = int.Parse(row.Cells[0].Text);
        switch (e.CommandName)
        {
            case "Confirmar":
                #region Comando Confirmar
                string user = User.Identity.Name;
                try
                {
                    ConfirmarEntregasCorreios.ConfirmarEntrega(id, user);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "erro", string.Format("alert('ERRO: {0}')", ex.Message), true);
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "INFO", string.Format("alert('INFO: Registro confirmado com sucesso!')"), true);
                #endregion
                break;
            default:
                break;
        }
        gvConfirmar.DataBind();

    }
}