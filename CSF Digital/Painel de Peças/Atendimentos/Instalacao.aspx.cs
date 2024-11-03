using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_Instalacao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void limparCampos()
    {
        tbContato.Text = "";
        tbEmailTecnico.Text = "";
        tbEndereco.Text = "";
        tbFalha.Text = "";
        tbFone.Text = "";
        tbReq.Text = "";
        tbTecnico.Text = "";
    }
    protected void btSolicitar_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (!checkRegistro.Checked)
        {
            result = reqAtendimentos.Abrir(dpClientes.SelectedValue, tbReq.Text, tbEndereco.Text, tbSerie.Text, tbContato.Text, tbFone.Text, tbFalha.Text, User.Identity.Name, "0", tbTecnico.Text, tbEmailTecnico.Text);
        }
        else
        {
            //result = result = reqAtendimentos.Abrir(dpClientes.SelectedValue, tbReq.Text, tbEndereco.Text, tbSerie.Text, tbContato.Text, tbFone.Text, tbFalha.Text, User.Identity.Name, "0", tbTecnico.Text, tbEmailTecnico.Text, false);
        }

        if(result)
        {
        lbMensagem.Text = "Requisição aberta com sucesso!";
                dsSolicitacoes.DataBind();
                gvSolicitacoes.DataBind();
                limparCampos();
        }
        else{
        lbMensagem.Text = "falha na abertura da solicitação!";
        }
        
    }
}