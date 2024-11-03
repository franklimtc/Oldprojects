using System;
using System.Web.UI;


public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        ScriptManager.RegisterClientScriptBlock(
            this,
            typeof(Page),
            "Mapa",
            Maps.ConstruirMapa(),
            true);
        }
    }
   
    protected void btCadastro_Click(object sender, EventArgs e)
    {
        Response.Redirect("Unidades.aspx");
    }

    protected void btTecnicos_Click(object sender, EventArgs e)
    {

    }
    protected void btPrinter_of_Click(object sender, EventArgs e)
    {

    }
}