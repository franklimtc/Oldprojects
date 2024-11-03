using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Solicitacoes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }

    protected void btHistorico_Click(object sender, EventArgs e)
    {
        gvHistorico.DataBind();
        MultiView1.ActiveViewIndex = 0;
    }

    protected void btSolicitacao_Click(object sender, EventArgs e)
    {
        gvChamadosAbertos.DataBind();
        MultiView1.ActiveViewIndex = 1;
        CarregarDadosEquipamento();
    }

    private void CarregarDadosEquipamento()
    {
        Equipamentos eqp = Equipamentos.Localizar(dpSeries.SelectedValue);
        tbEndereco.Text = eqp.Endereco;
        tbBairro.Text = eqp.Bairro;
        tbCEP.Text = eqp.Cep;
        tbContato.Text = eqp.Contato;
        tbTelefone.Text = eqp.Fone;
        tbEmailContato.Text = eqp.Email;
    }

    protected void btSolicitar_Click(object sender, EventArgs e)
    {
        #region SolicitarSuprimento
       

        reqSuprimentos req = new reqSuprimentos();
        req.Serie = dpSeries.SelectedValue;
        req.Uf = dpUf.SelectedValue;
        req.Cidade = dpCidades.SelectedValue;

        req.Endereco = tbEndereco.Text;
        req.Bairro = tbBairro.Text;
        req.Cep = tbCEP.Text;

        req.Usuario = tbContato.Text;
        req.EmailUsuario = tbEmailContato.Text;
        req.TelefoneUsuario = tbTelefone.Text;

        req.ReqUSD = tbUSD.Text;
        req.Obs = tbObs.Text;
        req.ValorAtual = tbValorAtual.Text;
        req.DurabilidadeEstimada = tbDurabilidadeEstimada.Text;
        req.Solicitante = User.Identity.Name;

        req.Suprimento = dpSuprimento.SelectedValue;
        req.Contador = tbContador.Text;

        if (rbFalhaNao.Checked)
        {
            req.FalhaAnterior = "0";
        }
        else
        {
            req.FalhaAnterior = "1";
        }

        if (req.Adicionar())
        {
            AtualizarEquipamento();
            LimparCampos();
            gvChamadosAbertos.DataBind();
        }
        #endregion

    }

    private void AtualizarEquipamento()
    {

        Equipamentos eqp = new Equipamentos();
        eqp.Serie = dpSeries.SelectedValue;
        eqp.Uf = dpUf.SelectedValue;
        eqp.Cidade = dpCidades.SelectedValue;
        eqp.Endereco = tbEndereco.Text;
        eqp.Bairro = tbBairro.Text;
        eqp.Cep = tbCEP.Text;
        eqp.Contato = tbContato.Text;
        eqp.Fone = tbTelefone.Text;
        eqp.Email = tbEmailContato.Text;
        eqp.Atualizar(User.Identity.Name);

    }

    protected void btLimpar_Click(object sender, EventArgs e)
    {
        LimparCampos();
    }

    private void LimparCampos()
    {
        tbEndereco.Text = "";
        tbBairro.Text = "";
        tbCEP.Text = "";
        tbContato.Text = "";
        tbTelefone.Text = "";
        tbEmailContato.Text = "";
        tbValorAtual.Text = "";
        tbDurabilidadeEstimada.Text = "";
        rbFalhaNao.Checked = true;
        tbObs.Text = "";
        if (!btSolicitar.Enabled)
        {
            btSolicitar.Enabled = true;
        }
    }

    
    public void Messagebox(string mensagem)
    {
        Response.Write("<script>alert('" + mensagem + "');</script>");
    }

    protected void dpSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void gvChamadosAbertos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PreencherCampos();
    }

    private void PreencherCampos()
    {
        tbEndereco.Text = "------";
        tbBairro.Text = "------";
        tbContato.Text = "------";
        tbEmailContato.Text = "------";
        tbContador.Text = "0";
        tbValorAtual.Text = "0";
        tbDurabilidadeEstimada.Text = "0";
        tbUSD.Text = "0";
        tbCEP.Text = "00000000";
        tbTelefone.Text = "00000000";
        btSolicitar.Enabled = false;

    }

    protected void gvChamadosAbertosHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvChamadosAbertosHistorico.Rows[Convert.ToInt32(e.CommandArgument)];
        string assunto = null;
        switch (e.CommandName)
        {
            case "Cancelar":
                assunto = string.Format("{0} - {1} - {2} - {3} - Solicitação de Suprimento Cancelada", row.Cells[3].Text, row.Cells[4].Text, row.Cells[5].Text, row.Cells[6].Text);
                reqSuprimentos.Cancelar(row.Cells[3].Text,User.Identity.Name, assunto, row.Cells[8].Text);
                break;
            case "Priorizar":
                assunto = string.Format("{0} - {1} - {2} - {3} - Solicitação de Suprimento Priorizada", row.Cells[3].Text, row.Cells[4].Text, row.Cells[5].Text, row.Cells[6].Text);
                reqSuprimentos.Priorizar(row.Cells[3].Text, User.Identity.Name, assunto, row.Cells[8].Text);
                break;
            default:
                break;
        }
        gvChamadosAbertosHistorico.DataBind();
    }

    protected void gvEquipamento_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //GridViewRow row = gvChamadosAbertosHistorico.Rows[Convert.ToInt32(e.)];
    }
}