using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Seguranca
{
    public partial class Default : System.Web.UI.Page
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

        protected void tbAdicionar_Click(object sender, EventArgs e)
        {
            LimparErros();

            if (!Base.Account.UsuarioExiste(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), tbNome.Text.Trim(), tbEmail.Text.Trim()))
            {
                if (tbSenha1.Text == tbSenha2.Text)
                {
                    Base.Account novaConta = new Base.Account();
                    novaConta.Nome = tbNome.Text.Trim();
                    novaConta.Email = tbEmail.Text.Trim();
                    novaConta.Senha = tbSenha1.Text;
                    novaConta.idGrupo = int.Parse(dpGrupo.SelectedItem.Value);

                    if (novaConta.Adicionar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
                    {
                        LimparCampos();
                    }
                }
                else
                {
                    rfErroSenha.Visible = true;
                }
            }
            else
            {
                rfErroUsuarioDuplicado.Visible = true;
            }
           
        }

        private void LimparErros()
        {
            rfErroSenha.Visible = false;
            rfErroUsuarioDuplicado.Visible = false;
        }

        private void LimparCampos()
        {
            tbNome.Text = "";
            tbEmail.Text = "";
            

            gvUsuarios.DataBind();
        }
    }
}