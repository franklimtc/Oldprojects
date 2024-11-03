using System;

public partial class Unidades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void tbInserir_Click(object sender, EventArgs e)
    {
        string tsqlInsert = string.Format("insert into markers(descricao, nomeSimples, lat, lng) VALUES('{0}','{1}','{2}','{3}')"
            , tbdescricao.Text, tbNome.Text, tbLat.Text, tbLng.Text);
        if (DAO.Execute(tsqlInsert))
        {
            tbdescricao.Text = "";
            tbNome.Text = "";
            tbLat.Text = "";
            tbLng.Text = "";
        }
        dbMaps.DataBind();
        gvUnidades.DataBind();
        
    }
    protected void tbMapa_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}