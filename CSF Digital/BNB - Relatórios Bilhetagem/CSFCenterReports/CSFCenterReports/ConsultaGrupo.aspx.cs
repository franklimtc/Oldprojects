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
    public partial class ConsultaGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LimparCamposConsulta();
            }

            if (Session["login"] == null)
                Response.Redirect("~/Login.aspx");
        }

        protected void rbtnConsultar_Click(object sender, EventArgs e)
        {
            string strGrupo = null;
            string strUf = null;
            string strCidade = null;
            string strUnidade = null;
            string strSetor = null;
            bool blnAtivo = true;

            if (rcbConsultaCodigo.SelectedItem != null)
            {
                if (rcbConsultaCodigo.SelectedValue == "Todos")
                    strGrupo = "";
                else
                    strGrupo = rcbConsultaCodigo.SelectedValue;
            }

            if (rcbConsultaUf.SelectedItem != null)
            {
                if (rcbConsultaUf.SelectedValue == "Todos")
                    strUf = "";
                else
                    strUf = rcbConsultaUf.SelectedValue;
            }

            if (rcbConsultaCidade.SelectedItem != null)
            {
                if (rcbConsultaCidade.SelectedValue == "Todas")
                    strCidade = "";
                else
                    strCidade = rcbConsultaCidade.SelectedValue;
            }

            if (rcbConsultaUnidade.SelectedItem != null)
            {
                if (rcbConsultaUnidade.SelectedValue == "Todas")
                    strUnidade = "";
                else
                    strUnidade = rcbConsultaUnidade.SelectedValue;
            }

            if (rcbConsultaSetor.SelectedItem != null)
            {
                if (rcbConsultaSetor.SelectedValue == "Todos")
                    strSetor = "";
                else
                    strSetor = rcbConsultaSetor.SelectedValue;
            }

            if (rcbConsultaStatus.SelectedItem != null)
                blnAtivo = Util.Status(rcbConsultaStatus.SelectedIndex);

            ObjectDataSourceGrupos.SelectParameters[0].DefaultValue = strGrupo;
            ObjectDataSourceGrupos.SelectParameters[1].DefaultValue = strUf;
            ObjectDataSourceGrupos.SelectParameters[2].DefaultValue = strCidade;
            ObjectDataSourceGrupos.SelectParameters[3].DefaultValue = strUnidade;
            ObjectDataSourceGrupos.SelectParameters[4].DefaultValue = strSetor;
            ObjectDataSourceGrupos.SelectParameters[5].DefaultValue = blnAtivo.ToString();
        }

        protected void rbtnFechar_Click(object sender, EventArgs e)
        {
            LimparCamposConsulta();

            rWindow.VisibleOnPageLoad = false;
        }

        protected void rbtnConfirmar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ConsultaGrupo.aspx");
            rWindow.VisibleOnPageLoad = false;
        }

        protected void rgrdGrupos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Listar" && e.Item is GridDataItem)
            {
                e.Item.Selected = true;

                LinkButton lb = (LinkButton)e.CommandSource;
                Grupo grupo = Grupo.RetornaGrupo(lb.Text);

                if (grupo != null)
                {
                    ObjectDataSourceUsuarios.SelectParameters[0].DefaultValue = grupo.Codigo;

                    ObjectDataSourceUsuarios.DataBind();
                    rgrdUsuarios.DataBind();

                    rWindow.VisibleOnPageLoad = true;
                }
                else
                {
                    //LimparCampos();
                }
            }
        }

        private void LimparCamposConsulta()
        {
            rcbConsultaCodigo.ClearSelection();
            rcbConsultaCodigo.Items.Clear();
            rcbConsultaUf.ClearSelection();
            rcbConsultaUf.Items.Clear();
            rcbConsultaCidade.ClearSelection();
            rcbConsultaCidade.Items.Clear();
            rcbConsultaUnidade.ClearSelection();
            rcbConsultaUnidade.Items.Clear();
            rcbConsultaSetor.ClearSelection();
            rcbConsultaSetor.Items.Clear();
            rcbConsultaStatus.SelectedIndex = 0;

            List<Grupo> ListaGrupos = Grupo.RetornaGrupos();

            Parametro GrupoAdmin = Parametro.RetornaParametro("CodGrupoAdmin");

            if (GrupoAdmin != null)
            {
                Grupo grupo = new Grupo();
                grupo.Codigo = GrupoAdmin.Valor;
                grupo.Uf = "CE";
                grupo.Unidade = "CAPGV";
                grupo.Cidade = "FORTALEZA";
                grupo.Setor = "INFRAESTRUTURA";
                ListaGrupos.Add(grupo);
            }
            else
                Response.Redirect("~/Erro.aspx?cod=1");

            Parametro GrupoGerentes = Parametro.RetornaParametro("CodGrupoGer");

            if (GrupoGerentes != null)
            {
                Grupo grupo = new Grupo();
                grupo.Codigo = GrupoGerentes.Valor;
                grupo.Uf = "CE";
                grupo.Unidade = "CAPGV";
                grupo.Cidade = "FORTALEZA";
                grupo.Setor = "INFRAESTRUTURA";
                ListaGrupos.Add(grupo);
            }
            else
                Response.Redirect("~/Erro.aspx?cod=1");

            rcbConsultaCodigo.Items.Add(new RadComboBoxItem("", ""));
            foreach (string codigo in Grupo.RetornaListaCodigos(ListaGrupos))
                rcbConsultaCodigo.Items.Add(new RadComboBoxItem(codigo, codigo));

            rcbConsultaUf.Items.Add(new RadComboBoxItem("", ""));
            foreach (string uf in Grupo.RetornaListaUf(null))
                rcbConsultaUf.Items.Add(new RadComboBoxItem(uf, uf));

            rcbConsultaCidade.Items.Add(new RadComboBoxItem("", ""));
            foreach (string cidade in Grupo.RetornaListaCidades(null))
                rcbConsultaCidade.Items.Add(new RadComboBoxItem(cidade, cidade));

            rcbConsultaUnidade.Items.Add(new RadComboBoxItem("", ""));
            foreach (string unidade in Grupo.RetornaListaUnidades(null, null))
                rcbConsultaUnidade.Items.Add(new RadComboBoxItem(unidade, unidade));

            rcbConsultaSetor.Items.Add(new RadComboBoxItem("", ""));
            foreach (string setor in Grupo.RetornaListaSetores(null, null, null))
                rcbConsultaSetor.Items.Add(new RadComboBoxItem(setor, setor));
        }

        protected void rgrdUsuarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Fechar" && e.Item is GridDataItem)
            {
                e.Item.Selected = true;

                LinkButton lb = (LinkButton)e.CommandSource;
                Usuario usuario = Usuario.RetornaUsuario(lb.Text);

                rWindow.VisibleOnPageLoad = false;
            }
        }
        protected void rcbConsultaCodigo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        protected void rcbConsultaUf_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbConsultaCidade.Enabled = true;
            rcbConsultaCidade.Items.Clear();

            rcbConsultaUnidade.Enabled = true;
            rcbConsultaUnidade.Items.Clear();
            rcbConsultaUnidade.Enabled = false;

            rcbConsultaSetor.Enabled = true;
            rcbConsultaSetor.Items.Clear();
            rcbConsultaSetor.Enabled = false;

            string strUf = rcbConsultaUf.SelectedValue.ToString().Trim();

            if (strUf != "")
            {
                ObjectDataSourceCidade.SelectParameters[0].DefaultValue = strUf;
            }
            else
            {
                ObjectDataSourceCidade.SelectParameters[0].DefaultValue = null;
            }

            ObjectDataSourceCidade.DataBind();
            rcbConsultaCidade.DataBind();
        }
        protected void rcbConsultaCidade_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbConsultaUnidade.Enabled = true;
            rcbConsultaUnidade.Items.Clear();

            rcbConsultaSetor.Enabled = true;
            rcbConsultaSetor.Items.Clear();
            rcbConsultaSetor.Enabled = false;

            string strUf = rcbConsultaUf.SelectedValue.ToString().Trim();
            string strCidade = rcbConsultaCidade.SelectedValue.ToString().Trim();

            if (strUf != "" && strCidade != "")
            {
                ObjectDataSourceUnidade.SelectParameters[0].DefaultValue = strUf;
                ObjectDataSourceUnidade.SelectParameters[1].DefaultValue = strCidade;
            }
            else
            {
                ObjectDataSourceUnidade.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceUnidade.SelectParameters[1].DefaultValue = null;
            }

            ObjectDataSourceUnidade.DataBind();
            rcbConsultaUnidade.DataBind();
        }
        protected void rcbConsultaUnidade_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbConsultaSetor.Enabled = true;
            rcbConsultaSetor.Items.Clear();

            string strUf = rcbConsultaUf.SelectedValue.ToString().Trim();
            string strCidade = rcbConsultaCidade.SelectedValue.ToString().Trim();
            string strUnidade = rcbConsultaUnidade.SelectedValue.ToString().Trim();

            if (strUf != "" && strCidade != "")
            {
                ObjectDataSourceSetor.SelectParameters[0].DefaultValue = strUf;
                ObjectDataSourceSetor.SelectParameters[1].DefaultValue = strCidade;
                ObjectDataSourceSetor.SelectParameters[2].DefaultValue = strUnidade;
            }
            else
            {
                ObjectDataSourceSetor.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceSetor.SelectParameters[1].DefaultValue = null;
                ObjectDataSourceSetor.SelectParameters[2].DefaultValue = null;
            }

            ObjectDataSourceSetor.DataBind();
            rcbConsultaSetor.DataBind();
        }

        protected void rcbConsultaSetor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        protected void rcbConsultaStatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
    }
}