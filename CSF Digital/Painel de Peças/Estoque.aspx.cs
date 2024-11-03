using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Estoque : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvEstoqueAtual.DataSource = Estoque.EstoqueAtual();
            gvEstoqueAtual.DataBind();
        }
    }

    protected void dpEstoque_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvEstoqueAtual.DataSource = Estoque.EstoqueAtual(dpEstoque.SelectedItem.Text);
        gvEstoqueAtual.DataBind();
    }
}