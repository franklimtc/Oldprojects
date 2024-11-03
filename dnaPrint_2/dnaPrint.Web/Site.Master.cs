using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["connString"] = ConfigurationManager.ConnectionStrings["db"].ToString();
                Session["TipoDB"] = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());
            }
        }
        protected void btLogoff_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("~/Logon/Default.aspx");
        }
    }
}