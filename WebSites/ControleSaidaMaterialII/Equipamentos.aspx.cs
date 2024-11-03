using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controls;

public partial class Equipamentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (tbSerie.Text != "")
        {
            Equipamento eqp = new Equipamento();
            eqp.IdCliente = dpClientes.SelectedValue;
            eqp.Serie = tbSerie.Text;
            eqp.Operador = User.Identity.Name;
            if (eqp.Adicionar())
            {
                Limpar();
                dsEquipamentos.DataBind();
                gvEquipamentos.DataBind();
            }
        }
        

    }

    private void Limpar()
    {
        tbSerie.Text = "";
    }
}