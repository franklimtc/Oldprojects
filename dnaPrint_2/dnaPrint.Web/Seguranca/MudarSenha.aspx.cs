using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Seguranca
{
    public partial class MudarSenha : System.Web.UI.Page
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

                if (!string.IsNullOrEmpty(user))
                {
                }

            }
        }

        protected void TbSalvar_Click(object sender, EventArgs e)
        {
            if (Base.Account.ValidarUsuar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), Session["User"].ToString(), tbSenhaAntiga.Text))
            {
                if (tbSenhaNova1.Text == tbSenhaNova2.Text)
                {
                    Base.Account usuario = Base.Account.FindByEmail(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), Session["user"].ToString());
                    usuario.Senha = tbSenhaNova1.Text;
                    if (usuario.Atualizar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
                    {
                        LimparDados();
                        lbSenhaModificada.Visible = true;
                    }
                }
                else
                {
                    lbErroSenhaNova.Visible = true;
                }
            }
            else
            {
                lbErroSenhaAntiga.Visible = true;
            }
        }

        private void LimparDados()
        {
            tbSenhaAntiga.Text = "";
            tbSenhaNova1.Text = "";
            tbSenhaNova2.Text = "";

        }
    }
}