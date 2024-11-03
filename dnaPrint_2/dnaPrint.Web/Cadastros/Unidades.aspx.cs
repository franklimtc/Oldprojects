using dnaPrint.Base;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace dnaPrint.Web
{
    public partial class Unidades : System.Web.UI.Page
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

        protected void dpCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvUnidades.DataBind();
        }

        protected void gvUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridViewRow row = gvUnidades.Rows[Convert.ToInt32(e.CommandArgument)];
            Session["idUnidade"] = row.Cells[1].Text;
            Unidade und = new Unidade().ListarByID(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString()), int.Parse(row.Cells[1].Text));
            tbUnidade.Text = und.descricao;
            tbEndereco.Text = und.endereco;
            tbTelefone.Text = und.telefone;
            tbContato.Text = und.contato;
            tbEmail.Text = und.email;
            tbAdicionar.Enabled = false;
            TbSalvar.Enabled = true;
            tbExcluir.Enabled = true;

        }

        protected void TbSalvar_Click(object sender, EventArgs e)
        {
            int idLocal = int.Parse(Session["idUnidade"].ToString());
            Unidade und = new Unidade(idLocal, int.Parse(dpCidade.SelectedValue), tbUnidade.Text, tbEndereco.Text, tbTelefone.Text, tbContato.Text, Unidade.statusUnidade.Ativo);
            und.email = tbEmail.Text;
            if (und.Atualizar(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString())))
            {
                LimparCampos();
            }

        }

        protected void tbAdicionar_Click(object sender, EventArgs e)
        {
            Unidade und = new Unidade();
            und.idCidade = int.Parse(dpCidade.SelectedValue);
            und.descricao = tbUnidade.Text;
            und.endereco = tbEndereco.Text;
            und.contato = tbContato.Text;
            und.telefone = tbTelefone.Text;
            und.email = tbEmail.Text;
            if (und.Adicionar(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString())))
            {
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            tbUnidade.Text = "";
            tbEndereco.Text = "";
            tbTelefone.Text = "";
            tbContato.Text = "";
            tbEmail.Text = "";
            TbSalvar.Enabled = false;
            tbExcluir.Enabled = false;
            tbAdicionar.Enabled = true;
            gvUnidades.DataBind();
            tbUnidade.Focus();
        }

        protected void tbExcluir_Click(object sender, EventArgs e)
        {
            Unidade und = new Unidade().ListarByID(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString()), int.Parse(Session["idUnidade"].ToString()));
            if (und.Excluir(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString())))
            {
                LimparCampos();
            }
        }

        protected void dpEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpCidade.DataBind();
        }
    }
}