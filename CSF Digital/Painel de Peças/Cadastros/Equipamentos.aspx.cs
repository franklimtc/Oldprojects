using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastros_Equipamentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btInserir_Click(object sender, EventArgs e)
    {

        Equipamentos eqpto = new Equipamentos();
        eqpto.Uf = tbUf.Text;
        eqpto.Cidade = tbCidade.Text;
        eqpto.Serie = tbSerie.Text;
        eqpto.IdModelo = dpModelos.SelectedValue;
        eqpto.IdCliente = dpClientes.SelectedValue;

        if (eqpto.adicionar())
        {
            //lbMensagem.Text = "Adicionado com sucesso!";
            limparCampos();
        }
        else
        {
            //lbMensagem.Text = "Falha ao tentar adicionar equipamento!";
        }
        //divMensagem.Controls.Add(lbMensagem);

    }

    private void limparCampos()
    {
        tbCidade.Text = "";
        tbIP.Text = "";
        tbSerie.Text = "";
        tbUf.Text = "";
    }

    protected void gvEquipamentos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        tbUf.Text = "---";
        tbCidade.Text = "---";
        tbIP.Text = "---";
        tbIP.Text = "---";
        tbSerie.Text = "---";
        btInserir.Enabled = false;

    }

    protected void gvEquipamentos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        btInserir.Enabled = true;
        limparCampos();
    }
}