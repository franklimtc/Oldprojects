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
    public partial class ConsultaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LimparCampos();
            }

            if (Session["login"] == null)
                Response.Redirect("~/Login.aspx");
        }

        protected void rbtnCadastrar_Click(object sender, EventArgs e)
        {
            rtxtLogin.Text = "";
            rtxtNome.Text = "";

            LimparCampos();

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

            foreach (Grupo grupo in ListaGrupos)
                rcbGrupo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(grupo.Descricao, grupo.Codigo));

            rbtnResetarSenha.Visible = false;

            rWindow.VisibleOnPageLoad = true;
        }

        protected void rbtnConsultar_Click(object sender, EventArgs e)
        {
            string strLogin = rtxtLogin.Text.Trim();
            string strNome = rtxtNome.Text.Trim();

            if (strLogin != "" && strNome != "")
            {
                ObjectDataSourceUsuarios.SelectParameters[0].DefaultValue = strLogin;
                ObjectDataSourceUsuarios.SelectParameters[1].DefaultValue = strNome;
            }
            else if (strLogin != "" && strNome == "")
            {
                ObjectDataSourceUsuarios.SelectParameters[0].DefaultValue = strLogin;
                ObjectDataSourceUsuarios.SelectParameters[1].DefaultValue = null;
            }
            else if (strLogin == "" && strNome != "")
            {
                ObjectDataSourceUsuarios.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceUsuarios.SelectParameters[1].DefaultValue = strNome;
            }
            else
            {
                ObjectDataSourceUsuarios.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceUsuarios.SelectParameters[1].DefaultValue = null;
            }

            ObjectDataSourceUsuarios.DataBind();
            rgrdUsuarios.DataBind();
        }

        protected void rbtnResetarSenha_Click(object sender, EventArgs e)
        {
            string strLogin = rtbLogin.Text.Trim();

            Usuario usuario = Usuario.RetornaUsuario(strLogin);

            if (usuario != null)
            {
                if (Usuario.ResetarSenha(usuario, ""))
                {
                    LimparCampos();

                    rWindow.VisibleOnPageLoad = false;
                }
                else
                {
                    lbMensagem.Text = "Erro ao tentar resetar a senha do usuário.\n\nAtualize a página!";
                }
            }
            else
            {
                lbMensagem.Text = "Usuário inexistente!";
            }
        }

        protected void rbtnFechar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            rWindow.VisibleOnPageLoad = false;
        }

        protected void rbtnConfirmar_Click(object sender, EventArgs e)
        {
            string strLogin = "";
            string strNome = "";
            DateTime? dtCadastro = (DateTime?)null;
            DateTime dtValidade = new DateTime();
            DateTime? dtAlteracao = (DateTime?)null;
            string strGrupo = "";
            bool blnAtivo = false;
            bool blnUtilizaAD = false;

            if (rtbLogin.Text.Trim() != "")
                strLogin = rtbLogin.Text.Trim();
            else
            {
                lbMensagem.Text = "Preencha o campo 'Login'!";
                rtbLogin.Text = "";
                rtbLogin.Focus();
                return;
            }

            if (rtbNome.Text.Trim() != "")
                strNome = rtbNome.Text.Trim();
            else
            {
                lbMensagem.Text = "Preencha o campo 'Nome'!";
                rtbNome.Text = "";
                rtbNome.Focus();
                return;
            }

            if (rdpDtCadastro.SelectedDate.HasValue)
                dtCadastro = rdpDtCadastro.SelectedDate.Value;
            else
                rdpDtCadastro.Clear();

            if (rdpDtValidade.SelectedDate.HasValue)
                dtValidade = rdpDtValidade.SelectedDate.Value;
            else
            {
                lbMensagem.Text = "Preencha o campo 'Válido até'!";
                rdpDtValidade.Clear();
                rdpDtValidade.Focus();
                return;
            }

            if (rdpDtAlteracao.SelectedDate.HasValue)
                dtAlteracao = rdpDtAlteracao.SelectedDate.Value;
            else
                rdpDtAlteracao.Clear();

            if (rcbGrupo.SelectedItem != null)
                strGrupo = rcbGrupo.SelectedValue;
            else
            {
                lbMensagem.Text = "Preencha o campo 'Grupo'!";
                rcbGrupo.ClearSelection();
                rcbGrupo.Focus();
                return;
            }

            blnAtivo = Util.Status(rcbStatus.SelectedIndex);
            blnUtilizaAD = Util.Status(rcbUtilizaAD.SelectedIndex);

            if (dtCadastro.HasValue)
            {
                Usuario usuario = Usuario.RetornaUsuario(strLogin);
                usuario.Codigo = strLogin;
                usuario.Nome = strNome;
                usuario.Codigo = strLogin;
                usuario.DtCadastro = DateTime.Now;
                usuario.DtValidade = dtValidade;
                usuario.DtAlteracao = dtAlteracao;
                usuario.CdGrupo = strGrupo;
                usuario.Ativo = blnAtivo;
                usuario.UtilizaAD = blnUtilizaAD;

                if (!Usuario.Alterar(usuario))
                    lbMensagem.Text = "Não foi possível atualizar cadastro!";
                else
                    lbMensagem.Text = "Cadastro alterado com sucesso!";
            }
            else
            {
                Usuario usuario = new Usuario();
                usuario.Codigo = strLogin;
                usuario.Nome = strNome;
                usuario.Codigo = strLogin;
                usuario.DtCadastro = DateTime.Now;
                usuario.DtValidade = dtValidade;
                usuario.DtAlteracao = dtAlteracao;
                usuario.CdGrupo = strGrupo;
                usuario.Ativo = blnAtivo;
                usuario.UtilizaAD = blnUtilizaAD;

                if (!Usuario.Inserir(usuario))
                    lbMensagem.Text = "Não foi possível efetuar o cadastro!";
                else
                    lbMensagem.Text = "Cadastro efetuado com sucesso!";
            }

            Response.Redirect("~/ConsultaUsuario.aspx");
            rWindow.VisibleOnPageLoad = false;
        }

        protected void rgrdUsuarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Alterar" && e.Item is GridDataItem)
            {
                e.Item.Selected = true;

                LinkButton lb = (LinkButton)e.CommandSource;
                Usuario usuario = Usuario.RetornaUsuario(lb.Text);

                if (usuario != null)
                {
                    if (usuario.UtilizaAD)
                        rbtnResetarSenha.Visible = true;

                    rtbLogin.Text = usuario.Codigo;
                    rtbNome.Text = usuario.Nome;

                    rdpDtCadastro.Enabled = true;
                    rdpDtCadastro.SelectedDate = usuario.DtCadastro.Date;
                    rdpDtCadastro.Enabled = false;

                    rdpDtValidade.SelectedDate = usuario.DtValidade.Date;

                    rdpDtAlteracao.Enabled = true;
                    if (usuario.DtAlteracao.HasValue)
                        rdpDtAlteracao.SelectedDate = usuario.DtAlteracao.Value.Date;
                    else
                        rdpDtAlteracao.Clear();
                    rdpDtAlteracao.Enabled = false;

                    rcbGrupo.Items.Clear();
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

                    foreach (Grupo grupo in ListaGrupos)
                        rcbGrupo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(grupo.Descricao, grupo.Codigo));

                    foreach (RadComboBoxItem item in rcbGrupo.Items)
                    {
                        if (item.Value == usuario.CdGrupo)
                            item.Selected = true;
                    }

                    rcbStatus.SelectedIndex = Util.Status(usuario.Ativo);
                    rcbUtilizaAD.SelectedIndex = Util.Status(usuario.UtilizaAD);

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
            rtbLogin.Text = "";
            rtbNome.Text = "";
            rdpDtCadastro.Enabled = true;
            rdpDtCadastro.Clear();
            rdpDtCadastro.Enabled = false;
            rdpDtValidade.Enabled = true;
            rdpDtValidade.Clear();
            rdpDtAlteracao.Enabled = true;
            rdpDtAlteracao.Clear();
            rdpDtAlteracao.Enabled = false;
            rcbGrupo.ClearSelection();
            rcbGrupo.Items.Clear();
            rcbStatus.SelectedIndex = 0;
            rcbUtilizaAD.SelectedIndex = 0;
        }
    }
}