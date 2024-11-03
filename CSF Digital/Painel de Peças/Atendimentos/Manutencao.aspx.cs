using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_Manutencao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }
    }
    protected void btSolicitar_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (checkRegistro.Checked)
        {
            result = reqAtendimentos.Abrir(dpClientes.SelectedValue, tbReq.Text, tbEndereco.Text, dpSeries.SelectedValue, tbContato.Text, tbFone.Text, tbFalha.Text, User.Identity.Name, "1", tbTecnico.Text, tbEmailTecnico.Text);
        }
        else
        {
            result = reqAtendimentos.Abrir(dpUF.SelectedValue, dpCidade.SelectedValue, dpSeries.SelectedValue, dpClientes.SelectedValue, tbReq.Text, tbEndereco.Text, dpSeries.SelectedItem.Text, tbContato.Text, tbFone.Text, tbFalha.Text, User.Identity.Name.ToUpper(), "1", tbTecnico.Text, tbEmailTecnico.Text, tbemailCopia.Text, true);
        }

        if (result)
        {
            lbMensagem.Text = "Requisição aberta com sucesso!";
            dsSolicitacoes.DataBind();
            gvSolicitacoes.DataBind();
            limparCampos();
        }
        else
        {
            lbMensagem.Text = "falha na abertura da solicitação!";
        }
    }

    private void limparCampos()
    {
        tbContato.Text = "";
        tbEmailTecnico.Text = "";
        tbEndereco.Text="";
        tbFalha.Text = "";
        tbFone.Text = "";
        tbReq.Text = "";
        tbTecnico.Text = "";
    }
}