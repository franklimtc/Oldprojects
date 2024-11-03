using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI.WebControls;

public partial class Suprimentos_Solicitar2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void dpSeries3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('série mudou')", true);
        LimparCampos();
        if (dpSeries.SelectedItem.Text != "Selecionar")
        {
            dpSeries.Items.FindByText("Selecionar").Enabled = false;

            if (Equipamentos.CadastroAtualizado(dpSeries.SelectedItem.Text))
            {
                gvSuprimentosSolicitados.DataSourceID = "dsRequisicoes";
                gvSuprimentosSolicitados.DataBind();

                SolicitarSuprimento();
            }
            else
            {
                AtualizarCadastro();
            }
        }
    }

    private void LimparCampos()
    {
        tbReq.Text = "";
        tbContato.Text = "";
        tbEmail.Text = "";

        tbFone.Text = "";
        tbContador.Text = "";
        tbSupriAtual.Text = "";
        tbDurEstimada.Text = "";
        dpFalha.ClearSelection();
        dpFalha.Items.FindByText("Não").Selected = true;
        gvSuprimentosSolicitados.DataBind();
    }

    protected void btAtualizar_Click(object sender, EventArgs e)
    {
        string serie = "";
        if (tbSerie.Text == "")
            serie = dpSeries.SelectedItem.Text;
        else
            serie = tbSerie.Text;

        if (Equipamentos.CadastroAtualizado(serie))
        {
            SolicitarSuprimento();
        }
        else
        {
            AtualizarCadastro();
        }
    }

    private void AtualizarCadastro()
    {
        if (tbSerie.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "AtualizarCadastro('" + dpSeries.SelectedItem.Text + "')", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "AtualizarCadastro('" + tbSerie.Text + "')", true);
        }
        btSolicitar.Enabled = false;
        btAtualizar.Enabled = true;
    }

    private void SolicitarSuprimento()
    {
        Equipamentos eqp = new Equipamentos();
        if (tbSerie.Text != "")
        {
            eqp = Equipamentos.Localizar(tbSerie.Text);
            if (eqp.Serie != null && eqp.Serie != "")
            {
                tbCliente.Text = dpClientes.Items.FindByValue(eqp.IdCliente.ToString()).Text;
                tbUF.Text = eqp.Uf;
                tbCidade.Text = eqp.Cidade;
                tbUnidade.Text = eqp.Unidade;
                tbSerie2.Text = eqp.Serie;
            }
        }
        else
        {
            eqp = Equipamentos.Localizar(dpSeries.SelectedItem.Text.Trim());
        }

        dpSuprimento.DataSource = eqp.CarregarSuprimentos();
        dpSuprimento.DataTextField = "NomeSuprimento";
        dpSuprimento.DataValueField = "idSuprimento";
        dpSuprimento.DataBind();

        tbContato.Text = eqp.Contato;
        tbFone.Text = eqp.Fone;
        tbEmail.Text = eqp.Email;
        //tbEndereco.Text = string.Format("{0}, {1}", eqp.Endereco, eqp.enderecoNumero);
        btSolicitar.Enabled = true;
        btAtualizar.Enabled = false;
        gvSuprimentosSolicitados.DataBind();
        tbReq.Focus();
    }

    protected void btSolicitar_Click(object sender, EventArgs e)
    {
        if (dpSuprimento.SelectedItem != null)
        {
            string abertos = null;
            bool result = false;
            foreach (ListItem supri in dpSuprimento.Items)
            {
                if (supri.Selected)
                {
                    if (AbrirSolicitação(supri.Text))
                    {
                        if (abertos == null)
                            abertos = supri.Text;
                        else
                            abertos += " + " + supri;

                        result = true;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Falha ao tentar solicitar " + supri + " para o equipamento.')", true);
                        result = false;
                    }
                }
            }
            if (result)
            {
                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('" + "Solicitado para o equipamento " + abertos + " ." + "')", true);
                LimparCampos();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Houveram falhas nas solicitações. Verifique quais suprimentos foram solicitados!')", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Selecione o suprimento desejado!')", true);
        }
    }

    private bool AbrirSolicitação(string suprimento)
    {
        Equipamentos eqp = new Equipamentos();

        if (tbSerie.Text == "")
        {
            eqp = Equipamentos.Localizar(dpSeries.SelectedItem.Text);
        }
        else
        {
            eqp = Equipamentos.Localizar(tbSerie.Text.Trim());

        }

        reqSuprimentos req = new reqSuprimentos();


        req.Serie = eqp.Serie;
        req.Uf = eqp.Uf;
        req.Cidade = eqp.Cidade;

        req.Endereco = eqp.Endereco;
        req.Bairro = eqp.Bairro;
        req.Cep = eqp.Cep;

        req.Usuario = tbContato.Text;
        req.EmailUsuario = tbEmail.Text;
        req.TelefoneUsuario = tbFone.Text;

        req.ReqUSD = tbReq.Text;
        req.Obs = tbObs.Text;

        req.ValorAtual = tbSupriAtual.Text;
        req.DurabilidadeEstimada = tbDurEstimada.Text;
        req.Solicitante = User.Identity.Name;

        req.Suprimento = suprimento;
        req.Contador = tbContador.Text;

        req.FalhaAnterior = dpFalha.SelectedItem.Value;

        bool result = false;
        result = req.Adicionar();
        return result;

    }

    protected void tbSerie_TextChanged(object sender, EventArgs e)
    {
        if (tbSerie.Text != "")
        {
            if (Equipamentos.Existe(tbSerie.Text.Trim()))
            {
                if (Equipamentos.CadastroAtualizado(tbSerie.Text.Trim()))
                {
                    dpUF.Visible = false;
                    tbUF.Visible = true;

                    dpCidade.Visible = false;
                    tbCidade.Visible = true;

                    dpUnidade.Visible = false;
                    tbUnidade.Visible = true;

                    dpSeries.Visible = false;
                    tbSerie2.Visible = true;

                    dpClientes.Visible = false;
                    tbCliente.Visible = true;
                    gvSuprimentosSolicitados.DataSourceID = "dsRequisicoes2";
                    gvSuprimentosSolicitados.DataBind();

                    SolicitarSuprimento();
                }
                else
                {
                    AtualizarCadastro();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Equipamento não localizado!')", true);
            }
            
        }
        else
        {
            dpUF.Visible = !false;
            tbUF.Visible = !true;

            dpCidade.Visible = !false;
            tbCidade.Visible = !true;

            dpUnidade.Visible = !false;
            tbUnidade.Visible = !true;

            dpSeries.Visible = !false;
            tbSerie2.Visible = !true;

            dpClientes.Visible = !false;
            tbCliente.Visible = !true;

            LimparCampos();
        }
    }

    protected void btHistórico_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Historico.aspx");
        if(tbSerie.Text !="")
        ClientScript.RegisterStartupScript(GetType(), "historico", "window.open('Historico.aspx?serie="+ tbSerie.Text.Trim() +"')", true);
        else
            ClientScript.RegisterStartupScript(GetType(), "historico", "window.open('Historico.aspx?serie=" + dpSeries.SelectedItem.Text + "')", true);

    }

    protected void gvSuprimentosSolicitados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvSuprimentosSolicitados.Rows[Convert.ToInt32(e.CommandArgument)];
        string assunto = null;
        Equipamentos eqp = Equipamentos.Localizar(row.Cells[3].Text);

        switch (e.CommandName)
        {
            case "Priorizar":
                assunto = string.Format("{0} - {1} - {2} - {3} - Solicitação de Suprimento Priorizada", row.Cells[0].Text, eqp.Uf, eqp.Cidade, eqp.Serie);
                try
                {
                    reqSuprimentos.Priorizar(row.Cells[0].Text, User.Identity.Name, assunto, row.Cells[4].Text);
                    Page.ClientScript.RegisterStartupScript(GetType(), "OK", "alert('Solicitação priorizada com sucesso!')", true);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OK", string.Format("alert('Falha ao priorizar a solicitação: Erro: {0}!')", ex.Message), true);
                }
                break;
            default:
                break;
        }
    }
}