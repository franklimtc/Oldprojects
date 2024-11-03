using dnaPrint.Base;
using dnaPrint.DAO;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Cadastros
{
    public partial class Equipamentos : System.Web.UI.Page
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
            Equipamento eqp = new Equipamento();
            eqp.idEstado = int.Parse(dpEstado.SelectedValue);
            eqp.idCidade = int.Parse(dpCidade.SelectedValue);
            eqp.idLocalidade = int.Parse(dpUnidades.SelectedValue);
            eqp.idSetor = int.Parse(dpSetor.SelectedValue);
            eqp.idModeloEquipamento = int.Parse(dpModeloEqpto.SelectedValue);
            eqp.IP = tbIP.Text;
            eqp.Serie = tbSerie.Text;
            eqp.nome = tbFila.Text;
            eqp.cor = false;


            bool serieExiste = ValiarSerie();
            bool filaExiste = ValiarFila();
            if (!serieExiste)
            {
                if (!filaExiste)
                {
                    if (eqp.Adicionar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
                    {
                        LimparCampos();
                    }
                }
            }
        }

        protected void TbSalvar_Click(object sender, EventArgs e)
        {
            Equipamento eqp = new Equipamento().ListarByID(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(Session["idEquipamento"].ToString()));

            eqp.IP = tbIP.Text;
            eqp.nome = tbFila.Text;

            if (eqp.Atualizar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
            {
                LimparCampos();
            }
        }

        protected void tbExcluir_Click(object sender, EventArgs e)
        {
            if (Session["idEquipamento"].ToString() != "")
            {
                Equipamento eqp = new Equipamento().ListarByID(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(Session["idEquipamento"].ToString()));
                if (eqp.Excluir(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
                {
                    LimparCampos();
                }
            }
        }

        private void LimparCampos()
        {
            tbIP.Text = "";
            tbSerie.Text = "";
            tbFila.Text = "";
            Session["idEquipamento"] = "";
            lbFila.Visible = false;
            lbSerie.Visible = false;
            dpModeloEqpto.SelectedIndex = 0;

            TbSalvar.Enabled = false;
            tbExcluir.Enabled = false;
            tbAdicionar.Enabled = true;

            gvEquipamentos.DataBind();
        }

        protected void gvEquipamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = gvEquipamentos.Rows[Convert.ToInt32(e.CommandArgument)];
            Session["idEquipamento"] = row.Cells[1].Text;

            Equipamento eqp = new Equipamento().ListarByID(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), int.Parse(Session["idEquipamento"].ToString()));

            tbIP.Text = eqp.IP;
            tbSerie.Text = eqp.Serie;
            tbFila.Text = eqp.nome;
            tbExcluir.Enabled = true;
            TbSalvar.Enabled = true;
            tbAdicionar.Enabled = false;
            dpModeloEqpto.SelectedIndex = eqp.idModeloEquipamento - 1;
        }

        protected void dpSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEquipamentos.DataBind();
        }

        protected bool ValiarFila()
        {
            bool result = Equipamento.FilaExiste(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), tbFila.Text.Trim());
            if (result)
                lbFila.Visible = true;
            return result;
        }

        protected bool ValiarSerie()
        {

            bool result =  Equipamento.SerieExiste(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString()), tbSerie.Text.Trim());
            if (result)
                lbSerie.Visible = true;
            return result;
        }

        protected void dpUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpSetor.DataSource = null;
            dpSetor.DataSourceID = "dsObjSetor";
            dpSetor.DataBind();
        }

        protected void dpEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpCidade.DataBind();
            dpUnidades.DataBind();
            dpSetor.DataBind();
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
                        dpSetor.DataSourceID = null;
                        dpSetor.DataSource = Setor.ListarByUnidade(Session["ConnString"].ToString(), Operacoes.DefinirTipo(Session["TipoDB"].ToString()), idUnidade);
                    }
                }
                catch (Exception)
                {

                    //throw;
                }

            }
            else
            {
                dpSetor.DataSourceID = "dsObjSetor";
                dpSetor.DataSource = null;
            }
        }

        protected void dpCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimparCampos();
            dsObjUnidade.DataBind();
            dpUnidades.DataBind();
            dsObjSetor.DataBind();
            dpSetor.DataBind();
        }
    }
}