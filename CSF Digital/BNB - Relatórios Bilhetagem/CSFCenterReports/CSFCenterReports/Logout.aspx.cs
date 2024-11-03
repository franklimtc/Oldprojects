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


namespace CSFCenterReports
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["nome"] = null;
            Session["login"] = null;
            Session["senha"] = null;
            Session["dominio"] = null;
            Session["admin"] = null;
            Session["grupo"] = null;

            Response.Redirect("Login.aspx");
        }
    }
}