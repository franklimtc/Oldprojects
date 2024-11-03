using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label lbCliente = new Label();
            string grupo = Account.RetornaGrupo(User.Identity.Name);
            if (grupo != "")
            {
                if (grupo != "Operadores")
                {
                    lbCliente.Text = grupo;
                    cliente.Controls.Add(lbCliente);
                }
                else
                {
                    Response.Redirect("~/Monitor.aspx");
                }
            }
        }
    }
}