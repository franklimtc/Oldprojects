using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Etiquetas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void tbSerial_TextChanged(object sender, EventArgs e)
    {
        if (tbSerial.Text != "")
        {
            btInserir.Enabled = true;
        }

    }
    protected void btInserir_Click(object sender, EventArgs e)
    {
        if(Etiquetas.Adicionar(dpSuprimento.SelectedValue, tbSerial.Text, tbEtiqueta.Text, User.Identity.Name))
        Limpar();
    }

    private void Limpar()
    {
        tbSerial.Text = "";
        tbEtiqueta.Text = "";
        btInserir.Enabled = false;
        gvEtiquetas.DataBind();
        ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Registro inserido!');", true);
    }

    protected void btSerial_Click(object sender, EventArgs e)
    {
        string oid = null;
        switch (dpSuprimento.SelectedValue)
        {
            case "Cilindro":
                oid = ".1.3.6.1.2.1.43.11.1.1.6.1.2";
                break;
            default:
                oid = ".1.3.6.1.2.1.43.11.1.1.6.1.1";
                break;
        }

        tbSerial.Text = DAO.ConsultaSNMP(oid, tbIP.Text).Replace("Black Toner Cartridge S/N:","").Replace("Black Drum Cartridge S/N:","") ;


        if (tbSerial.Text != "")
            btInserir.Enabled = true;
    }

    protected void dsEtiquetas_Deleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        gvEtiquetas.DataBind();
    }
}
