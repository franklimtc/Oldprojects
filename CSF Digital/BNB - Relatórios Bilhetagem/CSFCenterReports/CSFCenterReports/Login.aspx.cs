using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using CSFCenterReports.Controls;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace CSFCenterReports
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //DateTime dtInicial = new DateTime(2012, 7, 20, 0, 0, 0);
                //DateTime dtFinal = new DateTime(2012, 7, 25, 23, 59, 59);

                //List<Relatorio3Dados> Dados = Relatorio3Dados.RetornaDados(dtInicial, dtFinal, "CE", "FORTALEZA", "CAPGV", "", "", "", "", "", "");

                if (Session["login"] == null)
                {
                    rWindow.VisibleOnPageLoad = true;

                    Parametro DominiosAD = Parametro.RetornaParametro("DominiosAD");

                    if (DominiosAD != null)
                    {
                        if (DominiosAD.Valor.Contains(";"))
                        {
                            string[] dominios = DominiosAD.Valor.Split(';');

                            for (int i = 0; i < dominios.Length; i++)
                                rcbDominio.Items.Add(new Telerik.Web.UI.RadComboBoxItem(dominios[i], dominios[i]));
                        }
                        else
                        {
                            string dominio = DominiosAD.Valor;
                        }
                    }
                    else
                        Response.Redirect("~/Erro.aspx?cod=0");

                    Parametro DominioLocal = Parametro.RetornaParametro("DominioLocal");

                    if (DominioLocal != null)
                    {
                        rcbDominio.Items.Add(new Telerik.Web.UI.RadComboBoxItem(DominioLocal.Valor, DominioLocal.Valor));
                    }
                    else
                        Response.Redirect("~/Erro.aspx?cod=1");

                    rcbDominio.SelectedIndex = 0;

                    rtxtLogin.Focus();
                }
                else
                    Response.Redirect("~/Principal.aspx");
            }
        }

        protected void rbtnEntrar_Click(object sender, EventArgs e)
        {
            Session["nome"] = null;
            Session["login"] = null;
            Session["senha"] = null;
            Session["dominio"] = null;
            Session["admin"] = null;
            Session["grupo"] = null;
            Session["vip"] = null;

            if (rtxtLogin.Text != "" && rtxtSenha.Text != "")
            {
                string login = rtxtLogin.Text.ToString().Trim();
                string senha = rtxtSenha.Text.ToString().Trim();
                string dominio = rcbDominio.SelectedValue.Trim();

                Usuario usuario = Usuario.RetornaUsuario(login);

                if (usuario != null)
                {
                    if (usuario.UtilizaAD)
                    {
                        try
                        {
                            //Diretório LDAP
                            //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, dominio))
                            //{
                            //    // validate the credentials
                            //    bool isValid = pc.ValidateCredentials(login, senha);

                            //    if (isValid)
                            //    {
                            //        Session["nome"] = usuario.Nome;
                            //        Session["login"] = usuario.Codigo;
                            //        Session["senha"] = usuario.Senha;
                            //        Session["dominio"] = dominio;

                            //        if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoAdmin").Valor)
                            //        {
                            //            Session["admin"] = true;
                            //            Session["vip"] = true;
                            //        }
                            //        else if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoGer").Valor)
                            //        {
                            //            Session["admin"] = false;
                            //            Session["vip"] = true;
                            //        }
                            //        else
                            //        {
                            //            Session["admin"] = false;
                            //            Session["vip"] = false;
                            //        }

                            //        Session["grupo"] = usuario.CdGrupo;

                            //        rWindow.VisibleOnPageLoad = false;
                            //        Usuario.AtualizarDataAcesso(usuario);
                            //        Response.Redirect("~/Principal.aspx");
                            //    }
                            //    else
                            //    {
                            //        lbMensagem.Text = "Usuário e Senha inválidos.";
                            //    }
                            //}

                            DirectoryEntry lobjDirectory;
                            //DirectoryEntry lobjDirectory = new DirectoryEntry("LDAP://" + dominio, login, senha);
                            //DirectoryEntry lobjDirectory = new DirectoryEntry("LDAP://" + dominio + ":389");

                            if (dominio == "CAPGV")
                            {
                                lobjDirectory = new DirectoryEntry("LDAP://" + dominio, login, senha);
                            }
                            else
                            {
                                lobjDirectory = new DirectoryEntry("LDAP://" + dominio + ":389");
                            }

                            DirectorySearcher lobjSrch = new DirectorySearcher(lobjDirectory, "(&(objectClass=user)(objectCategory=person))");

                            if (lobjSrch.FindAll().Count > 0)
                            {
                                lobjSrch.Filter = "samaccountname=" + login; ;
                                SearchResult sr = lobjSrch.FindOne();

                                if (sr != null)
                                {
                                    ResultPropertyCollection rsProper = sr.Properties;

                                    foreach (string strKey in rsProper.PropertyNames)
                                    {
                                        foreach (object obProp in rsProper[strKey])
                                        {
                                            if (strKey.Equals("name"))
                                            {
                                                Session["nome"] = usuario.Nome;
                                                Session["login"] = usuario.Codigo;
                                                Session["senha"] = usuario.Senha;
                                                Session["dominio"] = dominio;

                                                if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoAdmin").Valor)
                                                {
                                                    Session["admin"] = true;
                                                    Session["vip"] = true;
                                                }
                                                else if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoGer").Valor)
                                                {
                                                    Session["admin"] = false;
                                                    Session["vip"] = true;
                                                }
                                                else
                                                {
                                                    Session["admin"] = false;
                                                    Session["vip"] = false;
                                                }

                                                Session["grupo"] = usuario.CdGrupo;

                                                rWindow.VisibleOnPageLoad = false;
                                                Usuario.AtualizarDataAcesso(usuario);
                                                Response.Redirect("~/Principal.aspx");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lbMensagem.Text = "Usuário e Senha inválidos.";
                        }
                    }
                    else
                    {
                        Parametro DominioLocal = Parametro.RetornaParametro("DominioLocal");

                        if (DominioLocal != null)
                        {
                            if (dominio == DominioLocal.Valor)
                            {
                                if (usuario.Senha == "")
                                {
                                    if (lbMensagem.Text == "Primeiro acesso.")
                                    {
                                        if (rtxtSenha.Text.Trim() != "")
                                        {
                                            if (Usuario.ResetarSenha(usuario, rtxtSenha.Text.Trim()))
                                            {
                                                usuario.Senha = rtxtSenha.Text.Trim();

                                                lbMensagem.Text = "Senha alterada.";

                                                Session["nome"] = usuario.Nome;
                                                Session["login"] = usuario.Codigo;
                                                Session["senha"] = usuario.Senha;
                                                Session["dominio"] = dominio;

                                                if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoAdmin").Valor)
                                                {
                                                    Session["admin"] = true;
                                                    Session["vip"] = true;
                                                }
                                                else if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoGer").Valor)
                                                {
                                                    Session["admin"] = false;
                                                    Session["vip"] = true;
                                                }
                                                else
                                                {
                                                    Session["admin"] = false;
                                                    Session["vip"] = false;
                                                }

                                                Session["grupo"] = usuario.CdGrupo;
                                                rWindow.VisibleOnPageLoad = false;
                                                Usuario.AtualizarDataAcesso(usuario);
                                                Response.Redirect("~/Principal.aspx");
                                            }
                                            else
                                                lbMensagem.Text = "Erro ao tentar salvar nova senha.\n\nAtualize a página!";
                                        }
                                        else
                                        {
                                            rtxtSenha.Focus();
                                        }
                                    }
                                    else
                                    {
                                        lbMensagem.Text = "Primeiro acesso.";

                                        rtxtSenha.Text = "";
                                        rtxtSenha.Focus();

                                        return;
                                    }
                                }
                                else if (senha == usuario.Senha)
                                {
                                    Session["nome"] = usuario.Nome;
                                    Session["login"] = usuario.Codigo;
                                    Session["senha"] = usuario.Senha;
                                    Session["dominio"] = dominio;

                                    if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoAdmin").Valor)
                                    {
                                        Session["admin"] = true;
                                        Session["vip"] = true;
                                    }
                                    else if (usuario.CdGrupo == Parametro.RetornaParametro("CodGrupoGer").Valor)
                                    {
                                        Session["admin"] = false;
                                        Session["vip"] = true;
                                    }
                                    else
                                    {
                                        Session["admin"] = false;
                                        Session["vip"] = false;
                                    }

                                    Session["grupo"] = usuario.CdGrupo;
                                    rWindow.VisibleOnPageLoad = false;
                                    Usuario.AtualizarDataAcesso(usuario);
                                    Response.Redirect("~/Principal.aspx");
                                }
                                else
                                {
                                    lbMensagem.Text = "Senha inválida.";
                                }
                            }
                            else
                            {
                                lbMensagem.Text = "Usuário inválido.";
                            }
                        }
                        else
                            Response.Redirect("~/Erro.aspx?cod=1");
                    }
                }
                else
                {
                    lbMensagem.Text = "Usuário inválido.";
                }
            }
        }

        protected void rbtnSair_Click(object sender, EventArgs e)
        {
            Session["nome"] = null;
            Session["login"] = null;
            Session["senha"] = null;
            Session["dominio"] = null;
            Session["admin"] = null;
            Session["grupo"] = null;
            Session["vip"] = null;

            this.Response.Close();
        }
    }
}