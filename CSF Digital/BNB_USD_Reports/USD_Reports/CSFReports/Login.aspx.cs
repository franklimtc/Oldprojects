using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AppLogin.TitleText = "Efetuar login";
            AppLogin.UserNameLabelText = "Usuário";
            AppLogin.PasswordLabelText = "Senha";
            AppLogin.LoginButtonText = "Entrar";
            AppLogin.FailureText = "Nome de Usuário ou Senha incorretos!";
            AppLogin.PasswordRequiredErrorMessage = "Digite sua senha!";
            AppLogin.RememberMeText = "Salvar dados!";
            AppLogin.UserNameRequiredErrorMessage = "Digite seu nome de usuário!";
        }
    }

    protected void AppLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string Usuario = AppLogin.UserName;
        string Senha = AppLogin.Password;

        if (FormsAuthentication.Authenticate(Usuario, Senha))
        {
            e.Authenticated = true;
            FormsAuthentication.RedirectFromLoginPage(AppLogin.UserName, false);
            Response.Redirect("Home.aspx");
        }
        else
        {
            e.Authenticated = false;
        }
    }
}