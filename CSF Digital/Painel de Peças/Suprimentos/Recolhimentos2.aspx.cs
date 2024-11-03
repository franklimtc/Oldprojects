using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Recolhimentos2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbUserName.Text = User.Identity.Name;
            lbUser.Text = "Resumo " + User.Identity.Name.ToUpper();
        }
    }

    static ControlePostagenReversa postagem = new ControlePostagenReversa();

    protected void btAdicionar_Click(object sender, EventArgs e)
    {
        ControlePostagenReversa novaColeta = new ControlePostagenReversa(tbColeta.Text, tbPostagem.Text, int.Parse(dpTipo.SelectedValue), int.Parse(tbQuantidade.Text));
        novaColeta.Usuario = User.Identity.Name;
        if (novaColeta.Adicionar())
        {
            LimparCampos();
            gvListaReversos.DataBind();
        }
    }

    private void LimparCampos()
    {
        tbColeta.Text = "";
        tbPostagem.Text = "";
        tbQuantidade.Text = "1";
        tbColeta.Enabled = true;
        dpTipo.Enabled = true;
        tbQuantidade.Enabled = true;
        tbColeta.Focus();
        btAtualizar.Visible = false;
        btExcluir.Visible = false;
        postagem = null;

        dsResumoRecolhimentos.DataBind();
    }

    protected void gvListaReversos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvListaReversos.Rows[Convert.ToInt32(e.CommandArgument)];
        string requisicao = Server.UrlEncode(row.Cells[1].Text);
        //string url = string.Format("EditarRecolhimento.aspx?id={0}", requisicao);
        //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("window.open('{0}', '_blank');", url), true);
        LimparCampos();
        postagem = ControlePostagenReversa.ListarPorId(int.Parse(row.Cells[1].Text));

        tbColeta.Text = postagem.CodColeta.ToString();
        tbIdReq.Text = postagem.ID.ToString();
        tbColeta.Enabled = false;
        dpTipo.SelectedIndex = dpTipo.Items.IndexOf(dpTipo.Items.FindByValue(postagem.TipoObj.ToString()));
        dpTipo.Enabled = false;
        tbPostagem.Text = postagem.Postagem;

        tbQuantidade.Enabled = false;
        btAtualizar.Visible = true;
        btExcluir.Visible = true;
    }

    protected void btAtualizar_Click(object sender, EventArgs e)
    {
        btAtualizar.Visible = false;
        postagem.Postagem = tbPostagem.Text;
        postagem.Salvar();

        LimparCampos();
        gvListaReversos.DataBind();
    }

    protected void btExcluir_Click(object sender, EventArgs e)
    {
        if (postagem.Deletar())
        {
            LimparCampos();
            gvListaReversos.DataBind();
        }
    }

    protected void dsResumoRecolhimentos_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (gvResumo.Rows.Count == 0)
        {
            lbUser.Visible = false;
        }
    }
}