using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastros_Clientes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void tbInserir_Click(object sender, EventArgs e)
    {
        Clientes c = new Clientes(tbCliente.Text, tbEndereco.Text, tbResponsavel.Text, tbTelefone.Text, tbEmail.Text);
        if (c.Inserir())
        {
            limpar();
            //dbClientes.DataBind();
            dsClientes.DataBind();
            gvClientes.DataBind();
        }
    }
    void limpar()
    {
        tbCliente.Text = "";
        tbEndereco.Text = "";
        tbResponsavel.Text = "";
        tbTelefone.Text = "";
        tbEmail.Text = "";
    }
}