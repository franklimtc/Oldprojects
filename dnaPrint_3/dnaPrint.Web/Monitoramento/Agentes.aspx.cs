using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Monitoramento
{
    public partial class Agentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string user = null;

                try
                {
                    user = Session["User"].ToString();
                }
                catch
                {
                    Response.Redirect(@"~\Logon\Default.aspx");
                }
            }
        }

        protected void gvAgentes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var lbAnexo = e.Row.Cells[4].Text;
            if (!lbAnexo.Equals("QtdDias"))
            {
                int iTemp = 0;
                if (int.TryParse(lbAnexo.ToLower(), out iTemp))
                {

                    if (iTemp >= 0 && iTemp < 2)
                    {
                        e.Row.FindControl("Image1").Visible = true;
                    }
                    else
                    {
                        if (iTemp >= 0 && iTemp < 15)
                        {
                            e.Row.FindControl("Image2").Visible = true;
                        }
                        else
                        {
                            e.Row.FindControl("Image3").Visible = true;
                        }
                    }

                }
            }
        }
    }
}