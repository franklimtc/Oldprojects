using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controls;

public partial class Materiais : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dsMateriais.DataBind();
            gvMateriais.DataBind();
        }
    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (tbModelo.Text != "" && tbDescricao.Text != "" && tbPartnumber.Text != "")
        {
            Material mat = new Material();
            mat.Modelo = tbModelo.Text;
            mat.Descricao = tbDescricao.Text;
            mat.PartNumber = tbPartnumber.Text;
            if (mat.Adicionar())
            {
                Limpar();
                dsMateriais.DataBind();
                gvMateriais.DataBind();
            }
        }
    }

    private void Limpar()
    {
        tbModelo.Text = "";
        tbDescricao.Text = "";
        tbPartnumber.Text = "";
    }
}