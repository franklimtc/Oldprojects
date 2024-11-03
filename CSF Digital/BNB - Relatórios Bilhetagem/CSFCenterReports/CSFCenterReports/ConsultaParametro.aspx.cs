using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections.Generic;
using CSFCenterReports.Controls;
using Telerik.Web.UI;

namespace CSFCenterReports
{
    public partial class ConsultaParametro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
                Response.Redirect("~/Login.aspx");
        }

        protected void rbtnCadastrar_Click(object sender, EventArgs e)
        {
            rtxtNome.Text = "";

            LimparCampos();

            rWindow.VisibleOnPageLoad = true;
        }

        protected void rbtnConsultar_Click(object sender, EventArgs e)
        {
            string strNome = rtxtNome.Text.Trim();

            if (strNome != "")
            {
                ObjectDataSourceParametros.SelectParameters[1].DefaultValue = strNome;
            }
            else
            {
                ObjectDataSourceParametros.SelectParameters[0].DefaultValue = null;
            }

            ObjectDataSourceParametros.DataBind();
            rgrdParametros.DataBind();
        }

        protected void rbtnFechar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            rWindow.VisibleOnPageLoad = false;
        }

        protected void rbtnConfirmar_Click(object sender, EventArgs e)
        {
            string strNome = "";
            string strValor = "";

            if (rtbNome.Text.Trim() != "")
                strNome = rtbNome.Text.Trim();
            else
            {
                lbMensagem.Text = "Preencha o campo 'Nome'!";
                rtbNome.Text = "";
                rtbNome.Focus();
                return;
            }

            if (rtbValor.Text.Trim() != "")
                strValor = rtbValor.Text.Trim();
            else
            {
                lbMensagem.Text = "Preencha o campo 'Valor'!";
                rtbValor.Text = "";
                rtbValor.Focus();
                return;
            }

            Parametro parametro = new Parametro();
            parametro.Nome = strNome.Trim();
            parametro.Valor = strValor.Trim();

            if (!Parametro.Alterar(parametro))
                lbMensagem.Text = "Não foi possível atualizar cadastro!";
            else
                lbMensagem.Text = "Cadastro alterado com sucesso!";

            Response.Redirect("~/ConsultaParametro.aspx");
            rWindow.VisibleOnPageLoad = false;
        }

        protected void rgrdParametros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Alterar" && e.Item is GridDataItem)
            {
                e.Item.Selected = true;

                LinkButton lb = (LinkButton)e.CommandSource;
                Parametro parametro = Parametro.RetornaParametro(lb.Text);

                if (parametro != null)
                {
                    rtbNome.Text = parametro.Nome;
                    rtbValor.Text = parametro.Valor;

                    rWindow.VisibleOnPageLoad = true;
                }
                else
                {
                    LimparCampos();
                }
            }
        }

        private void LimparCampos()
        {
            rtbNome.Text = "";
            rtbValor.Text = "";
        }
    }
}