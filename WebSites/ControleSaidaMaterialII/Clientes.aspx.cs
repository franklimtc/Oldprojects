using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controls;


public partial class Clientes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (tbidIlux.Text != "" && tbRazaoSocial.Text != "")
        {
            Cliente c = new Cliente();

            c.IdClienteIlux = tbidIlux.Text;
            c.RazaoSocial = tbRazaoSocial.Text;
            c.Operador = User.Identity.Name;
            if (c.Adicionar())
            {
                Limpar();
                dsClientes.DataBind();
                gvClientes.DataBind();
            }
        }
    }

    private void Limpar()
    {
        tbidIlux.Text = "";
        tbRazaoSocial.Text = "";
    }
}
