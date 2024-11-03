using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string serie = Request.QueryString["serie"];
            if (!string.IsNullOrEmpty(serie))
            {
                List<enviosSuprimentos> lista = enviosSuprimentos.ListarPorSerie(serie);
                if (lista.Count > 0)
                {
                    gvEnvios.DataSource = lista;
                    gvEnvios.DataBind();
                    lbErroSerie.Visible = false;
                }
                else
                {
                    lbErroSerie.Visible = true;
                }
            }
            else
            {
                lbErroSerie.Visible = true;
            }
        }
    }
}