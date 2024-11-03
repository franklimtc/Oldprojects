using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_editar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int idreq = 0;
            if (int.TryParse(Request.QueryString["id"], out idreq))
            {
                reqSuprimentos req = reqSuprimentos.Get(idreq);
                Preenchercampos(req);
            }

        }
    }

    private void Preenchercampos(reqSuprimentos req)
    {
        tbBairro.Text = req.Bairro;
        tbCep.Text = req.Cep;
        tbCidade.Text = req.Cidade;
        tbContador.Text = req.Contador;
        tbDurEstimada.Text = req.DurabilidadeEstimada;
        tbEmail.Text = req.EmailUsuario;
        tbEndereco.Text = req.Endereco;
        tbID.Text = req.IdreqSuprimento;
        tbOBS.Text = req.Obs;
        tbSerie.Text = req.Serie;
        tbSolicitante.Text = User.Identity.Name;
        tbSuprAtual.Text = req.ValorAtual;
        tbSuprimento.Text = req.Suprimento;
        tbTelefone.Text = req.TelefoneUsuario;
        tbUF.Text = req.Uf;
        tbUSD.Text = req.ReqUSD;
        tbUsuario.Text = req.Usuario;
        if (req.FalhaAnterior == "1")
        {
            chFalha.Checked = true;
        }
        else
        {
            chFalha.Checked = false;
        }
    }

    protected void btAtualizar_Click(object sender, EventArgs e)
    {
        reqSuprimentos req = new reqSuprimentos();

        req.Bairro = tbBairro.Text;
        req.Cep = tbCep.Text;
        req.Cidade = tbCidade.Text;
        req.Contador = tbContador.Text;
        req.DurabilidadeEstimada = tbDurEstimada.Text;
        req.EmailUsuario = tbEmail.Text;
        req.Endereco = tbEndereco.Text;
        req.IdreqSuprimento = tbID.Text;
        req.Obs = tbOBS.Text;
        req.Serie = tbSerie.Text;
        req.Solicitante = tbSolicitante.Text;
        req.ValorAtual = tbSuprAtual.Text;
        req.Suprimento = tbSuprimento.Text;
        req.TelefoneUsuario = tbTelefone.Text;
        req.Uf = tbUF.Text;
        req.ReqUSD = tbUSD.Text;
        req.Usuario = tbUsuario.Text;

        if (chFalha.Checked)
        {
            req.FalhaAnterior = "1";
        }
        else
        {
            req.FalhaAnterior = "0";
        }
        if (req.Atualizar())
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("alert('Solicitação atualizada com sucesso!');"), true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("alert('Falha ao atualizar a solicitação!');"), true);
        }
    }
}