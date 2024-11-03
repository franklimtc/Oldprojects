using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Logon
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btEntrar_Click(object sender, EventArgs e)
        {
            if (Base.Account.ValidarUsuar(ConfigurationManager.ConnectionStrings["db"].ToString()
                , DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString())
                , tbEmail.Text
                , tbPassword.Text))
            {
                Session["User"] = tbEmail.Text;
                lbErro.Visible = false;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lbErro.Visible = true;
            }
            
        }
    }
}