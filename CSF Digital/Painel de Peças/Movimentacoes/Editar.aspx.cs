using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections;

public partial class Movimentacoes_Editar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OcultarMensagens();

            int id;

            if (int.TryParse(Request.QueryString["id"], out id))
            {
                movimentacao m = new movimentacao();
                m = m.BuscarPorId(id);

                tbId.Text = m.Id.ToString();
                tbDescricao.Text = m.Descricao;
                tbQtd.Text = m.Qtd.ToString();
                tbData.Text = m.Data.ToString();
                tbPartNumber.Text = Produto.BuscarPorID(m.IdProduto).Partnumber;
                tbEstoque.Text = Estoque.BuscarPorId(m.IdEstoque).Descricao;
                tbSerie.Text = m.Serie;
                tbEtiqueta.Text = m.Etiqueta;
            }
        }
    }

    private void OcultarMensagens()
    {
        lbSucessoSalvar.Visible = false;
        lbErroSalvar.Visible = false;
        lbErroPartNumber.Visible = false;
        lbErroQuantidade.Visible = false;
        lbErroSaldo.Visible = false;
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        OcultarMensagens();

        if (Produto.Existe(tbPartNumber.Text))
        {
            lbErroPartNumber.Visible = false;
            if (Produto.Saldo(tbPartNumber.Text) >= int.Parse(tbQtd.Text))
            {
                lbErroSaldo.Visible = false;
                movimentacao m = new movimentacao();
                m.Id = int.Parse(tbId.Text);
                m.IdProduto = Produto.Buscar(tbPartNumber.Text).Id;
                m.Serie = tbSerie.Text;
                m.Etiqueta = tbEtiqueta.Text;
                m.Qtd = int.Parse(tbQtd.Text);
                m.Usuario = User.Identity.Name;

                if (m.Salvar())
                {
                    lbSucessoSalvar.Visible = true;
                    lbErroSalvar.Visible = false;
                    FecharJanela();

                }
                else
                {
                    lbSucessoSalvar.Visible = false;
                    lbErroSalvar.Visible = true;
                }
            }
            else
            {
                lbErroSaldo.Visible = true;
            }
        }
        else
        {
            lbErroPartNumber.Visible = true;

        }
    }

    private void FecharJanela()
    {
        ScriptManager.RegisterClientScriptBlock(
    Page,
    Page.GetType(),
    "mensagem",
    "close()",
    true);
    }

    protected void btCancelar_Click(object sender, EventArgs e)
    {
        FecharJanela();
    }
}