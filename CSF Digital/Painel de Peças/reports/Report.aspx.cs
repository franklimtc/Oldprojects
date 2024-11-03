using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reports_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbData.Text = DateTime.Now.ToString();
            lbMAterial.Text =  Server.UrlDecode(Request.QueryString["material"]);
            lbEndereco.Text = Server.UrlDecode(Request.QueryString["endereco"]);
            lbContato.Text = Server.UrlDecode(Request.QueryString["contato"]);
            lbTelefone.Text = Server.UrlDecode(Request.QueryString["telefone"]);
            lbSerie.Text = Server.UrlDecode(Request.QueryString["serie"]);
            lbReqID.Text = Server.UrlDecode(Request.QueryString["requisicao"]);
            lbUnidade.Text = Server.UrlDecode(Request.QueryString["unidade"]);
            lbSetor.Text = Server.UrlDecode(Request.QueryString["setor"]);
        } 
    }
}