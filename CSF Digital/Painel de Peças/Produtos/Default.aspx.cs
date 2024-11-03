using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Produtos_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btadicionar_Click(object sender, EventArgs e)
    {
        if (Usuarios.Administrador(User.Identity.Name))
        {
            Produto p = new Produto(0, tbDescricao.Text, tbParNumber.Text);
            if (p.Adicionar())
            {
                LimparTextBox();
                gvProdutos.DataBind();
            }
            else
            {
                lbFalha.Visible = true;
            }
        }
        else
        {
            lbUsuario.Visible = true;
        }
        
    }

    private void LimparTextBox()
    {
        tbDescricao.Text = "";
        lbFalha.Visible = false;
        tbParNumber.Text = "";
        lbUsuario.Visible = false;
    }
}