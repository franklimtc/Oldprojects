using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CSFCenterReports.Controls;
using Telerik.Web.UI;

namespace CSFCenterReports
{
    public partial class AppSiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["login"] != null)
                {
                    Usuario usuario = Usuario.RetornaUsuario(Session["login"].ToString());

                    if (usuario != null)
                        lblUsuario.Text = usuario.Codigo + " | " + usuario.Nome;

                    Parametro GrupoAdmin = Parametro.RetornaParametro("CodGrupoAdmin");

                    if (GrupoAdmin != null)
                    {
                        if (usuario.CdGrupo == GrupoAdmin.Valor)
                        {
                            PageUsuarios.Visible = true;
                            PageGrupos.Visible = true;
                            PageDefinicoes.Visible = true;
                            PageIndicadores.Visible = true;
                        }
                        else
                        {
                            PageUsuarios.Visible = false;
                            PageGrupos.Visible = false;
                            PageDefinicoes.Visible = false;
                            PageIndicadores.Visible = false;
                        }
                    }
                    else
                        Response.Redirect("~/Erro.aspx?cod=1");
                }
            }
        }
    }
}