using dnaPrint.Base;
using dnaPrint.DAO;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace dnaPrint.Web
{
    public partial class Setores : System.Web.UI.Page
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

        protected void tbAdicionar_Click(object sender, EventArgs e)
        {
            Setor set = new Setor(int.Parse(dpUnidades.SelectedValue), tbSetor.Text, tbCentroCusto.Text, int.Parse(tbCotaMensal.Text));
            if (set.Adicionar(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
            {
                LimparCampos();
                gvSetor.DataSourceID = "dsOBJSetores";
                gvSetor.DataSource = null;
                gvSetor.DataBind();
            }
        }

        private void LimparCampos()
        {
            tbSetor.Text = "";
            tbCentroCusto.Text = "";
            tbCotaMensal.Text = "10000";
            //gvSetor.DataBind();
            Session["idSetor"] = "";

            TbSalvar.Enabled = false;
            tbExcluir.Enabled = false;
            tbAdicionar.Enabled = true;

            tbSetor.Focus();
        }

        protected void TbSalvar_Click(object sender, EventArgs e)
        {
            Setor set = new Setor().ListarByID(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(Session["idSetor"].ToString()));
            set.Descricao = tbSetor.Text;
            set.CentroCusto = tbCentroCusto.Text;
            set.CotaMensal = int.Parse(tbCotaMensal.Text);

            if (set.Atualizar(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
            {
                LimparCampos();
                gvSetor.DataSourceID = "dsOBJSetores";
                gvSetor.DataSource = null;
                gvSetor.DataBind();
            }
        }

        protected void tbExcluir_Click(object sender, EventArgs e)
        {
            Setor set = new Setor().ListarByID(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(Session["idSetor"].ToString()));
            if(set.Excluir(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
            {
                LimparCampos();
            }

        }

        protected void gvSetor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = gvSetor.Rows[Convert.ToInt32(e.CommandArgument)];
            Session["idSetor"] = row.Cells[1].Text;
            Setor set = new Setor().ListarByID(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(Session["idSetor"].ToString()));

            TbSalvar.Enabled = true;
            tbExcluir.Enabled = true;
            tbAdicionar.Enabled = false;

            tbSetor.Text = set.Descricao;
            tbCentroCusto.Text = set.CentroCusto;
            tbCotaMensal.Text = set.CotaMensal.ToString();
        }

        protected void dpUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dpUnidades.DataBind();
        }

        protected void dpCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimparCampos();
        }

        protected void dsObjUnidade_DataBinding(object sender, EventArgs e)
        {
            LimparCampos();
        }

        protected void dsObjUnidade_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (dpUnidades.Items.Count == 0)
            {
                try
                {
                    int idUnidade = Unidade.Listar(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(dpCidade.SelectedItem.Value)).First().idLocalidade;
                    if (idUnidade > 0)
                    {
                        gvSetor.DataSourceID = null;
                        gvSetor.DataSource = Setor.ListarByUnidade(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), idUnidade).OrderBy(x => x.Descricao);
                    }
                }
                catch (Exception)
                {

                    //throw;
                }

            }
            else
            {
                gvSetor.DataSourceID = "dsOBJSetores";
                gvSetor.DataSource = null;
                gvSetor.DataBind();
            }
                
        }

        protected void dsObjUnidade_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            LimparCampos();
        }

        protected void gvSetor_DataBinding(object sender, EventArgs e)
        {
            
        }

        protected void dpEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpCidade.DataBind();
            dpUnidades.DataBind();
        }
    }
}