using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Requisicoes_Novarequisicao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbUserName.Text = Account.RetornaUserId(User.Identity.Name);
            string codigo = Guid.NewGuid().ToString();
            tbCodReq.Text = codigo;
            CarregarCodReq(codigo);
            IniciarMensagem();
        }
    }

    private void IniciarMensagem()
    {
        tbDescricao.Text = @"


--------Dados para contato---------
Telefone: (XX) 0000-0000
Endereço: -- - - - - - - - - -
Email: cliente@dominio.com.br";
    }

    private void CarregarCodReq(string Identificador)
    {
        Requisicao.Abrir(Identificador, User.Identity.Name);
    }

    protected void btCancelar_Click(object sender, EventArgs e)
    {
        Requisicao.Cancelar(tbCodReq.Text);
        Response.Redirect("~/Requisicoes/Default.aspx");
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {

        string codReq = null;
        bool continuar = false;
        codReq = string.Format("{0}", DateTime.Now.ToString("yyyy-MM"));
        switch(dpCategoria.SelectedItem.Value)
        {
            case "Atendimento Técnico":
                continuar = true;
                codReq += "0";
                break;
            case "Solicitação de suprimentos":
                codReq += "1";
                break;
            case "Outras solicitações":
                codReq += "2";
                break;
            default:
                break;
        }
       
        codReq += Requisicao.RetornaIDReq(tbCodReq.Text);

        int contador = 0;
        int suprimento = 0;

        if (!int.TryParse(tbContador.Text, out contador))
        {
            contador = -1;
        }
        if (!int.TryParse(tbSuprimento.Text, out suprimento))
        {
            suprimento = -1;
        }


        bool result = Requisicao.Salvar(tbCodReq.Text, codReq, User.Identity.Name, dpEqpto.SelectedItem.Text, dpCategoria.SelectedItem.Text, tbResumo.Text, tbDescricao.Text, Account.RetornaGrupo(User.Identity.Name), contador, suprimento);
        if (result)
        {
            tbcodReqFinal.Text = codReq;
            dpCategoria.Enabled = false;
            dpEqpto.Enabled = false;
            tbResumo.Enabled = false;
            tbDescricao.Enabled = false;
            btSalvar.Enabled = false;
           
        }
    }

    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Requisicoes/Default.aspx");
    }

    protected void dpCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (dpCategoria.SelectedItem.Value)
        {
            case "Atendimento Técnico":
                rftbContador.Enabled = true;
                rftbSuprimento.Enabled = false;

                rvtbContador.Enabled = true;
                rvtbSuprimento.Enabled = false;

                break;
            case "Solicitação de suprimentos":
                rftbContador.Enabled = true;
                rftbSuprimento.Enabled = true;

                rvtbContador.Enabled = true;
                rvtbSuprimento.Enabled = true;
                break;
            default:
                rftbContador.Enabled = false;
                rftbSuprimento.Enabled = false;

                rvtbContador.Enabled = false;
                rvtbSuprimento.Enabled = false;
                break;
        }
    }
}