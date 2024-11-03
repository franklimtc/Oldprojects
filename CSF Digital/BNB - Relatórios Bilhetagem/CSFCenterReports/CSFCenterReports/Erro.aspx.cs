using System;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections;
using System.Collections.Generic;
using CSFCenterReports.Controls;
using System.DirectoryServices;

namespace CSFCenterReports
{
    public partial class Erro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Request.QueryString.ToString().Contains("cod"))
                {
                    string codigo = Request.QueryString["cod"].ToString();

                    ConfiguraMensagem(codigo);
                }
                else
                    Response.Redirect("~/Principal.aspx");
            }
        }

        protected void rbtnRecarregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Principal.aspx");
        }

        private void ConfiguraMensagem(string codigo)
        {
            switch (codigo)
            {
                case "0":
                    {
                        lbMensagem.Text = "Não foi possível detectar domínios de rede.";
                    }
                    break;
                case "1":
                    {
                        lbMensagem.Text = "Não foi possível detectar dominio local.";
                    }
                    break;
                case "2":
                    {
                        lbMensagem.Text = "Não foi possível carregar os grupos dos usuários.";
                    }
                    break;
                case "3":
                    {
                        lbMensagem.Text = "Não foi possível carregar a lista de relatórios.";
                    }
                    break;
                case "4":
                    {
                        lbMensagem.Text = "";
                    }
                    break;


                default:
                    break;
            }
        }
    }
}