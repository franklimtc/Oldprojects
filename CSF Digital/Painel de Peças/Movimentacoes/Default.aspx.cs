using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Movimentacoes_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dpEstoque.SelectedIndex = 1;
            dpDescricao.Focus();
        }
    }

    protected void dpDescricao_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dpDescricao.SelectedItem.Text == "Entrada")
        {
            tbSerie.Enabled = false;
            tbEtiqueta.Enabled = false;
        }
        else
        {
            tbSerie.Enabled = true;
            tbEtiqueta.Enabled = true;
        }
        dpEstoque.Focus();
    }

    protected void btAdicionar_Click(object sender, EventArgs e)
    {
        int Qtd;
        if (int.TryParse(tbQtd.Text, out Qtd))
        {
            lbErroQuantidade.Visible = false;
            if (dpDescricao.SelectedItem.Text == "Entrada")
            {
                //Passou em todos os testes
                //Produto p = Produto.Buscar(tbPartNumber.Text);

                movimentacao mov = new movimentacao(int.Parse(dpEstoque.SelectedValue), int.Parse(tbPartNumber.SelectedItem.Value), int.Parse(tbQtd.Text), movimentacao.descricao.Entrada, User.Identity.Name);
                if (mov.Adicionar())
                    LimparTextBox();
                gvMovimentacoes.DataBind();
            }
            else
            {
                if (Produto.Saldo(tbPartNumber.SelectedItem.Text, dpEstoque.SelectedItem.Text) >= int.Parse(tbQtd.Text))
                {
                    lbErroSaldo.Visible = false;
                    if (tbSerie.Text != "" && tbEtiqueta.Text != "")
                    {
                        lbErroSerieEtiqueta.Visible = false;
                        //Passou em todos os testes
                        //Produto p = Produto.Buscar(tbPartNumber.Text);

                        movimentacao mov = new movimentacao(int.Parse(dpEstoque.SelectedItem.Value), int.Parse(tbPartNumber.SelectedItem.Value), int.Parse(tbQtd.Text), movimentacao.descricao.Saida, User.Identity.Name);
                        mov.Serie = tbSerie.Text;
                        mov.Etiqueta = tbEtiqueta.Text;
                        if (mov.Adicionar())
                        {
                            if (dpEstoque.SelectedItem.Text == "CAPGV")
                            {
                                // Registra a saída do estoque do CAPGV na tabela EnviosSuprimentos para emissão do RAS-D!

                                EnvioSuprimento novoEnvio = new EnvioSuprimento(tbSerie.Text, tbPartNumber.SelectedItem.Text, tbEtiqueta.Text, "CAPGV", User.Identity.Name);
                                if (novoEnvio.Adicionar())
                                {
                                    LimparTextBox();

                                }
                            }
                        }
                        gvMovimentacoes.DataBind();
                    }
                    else
                    {
                        lbErroSerieEtiqueta.Visible = true;
                    }
                }
                else
                {
                    lbErroSaldo.Visible = true;
                }
                
            }
        }
        else
        {
            lbErroQuantidade.Visible = true;
        }

    }

        private void LimparTextBox()
        {
            tbQtd.Text = "";
            tbSerie.Text = "";
            tbEtiqueta.Text = "";
            tbPartNumber.Focus();
        }

    protected void btEditar_Click(object sender, GridViewCommandEventArgs e)
    {
      
    }

    protected void gvMovimentacoes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvMovimentacoes.Rows[Convert.ToInt32(e.CommandArgument)];
        string requisicao = Server.UrlEncode(row.Cells[1].Text);
        string url = string.Format("Editar.aspx?id={0}", requisicao);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("window.open('{0}', '_blank');", url), true);
    }

    protected void dpEstoque_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvMovimentacoes.DataSourceID = null;

        if (dpEstoque.SelectedItem.Text == "Todos")
        {
            gvMovimentacoes.DataSource = movimentacao.ListarMovimentacoesDia();
        }
        else
        {
            gvMovimentacoes.DataSource = movimentacao.ListarMovimentacoesDia(dpEstoque.SelectedItem.Text);

        }
    }
}