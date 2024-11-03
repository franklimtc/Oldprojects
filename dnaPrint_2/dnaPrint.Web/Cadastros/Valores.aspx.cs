using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace dnaPrint.Web.Cadastros
{
    public partial class Valores : System.Web.UI.Page
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
            dnaPrint.Base.ValorPagina valor = new Base.ValorPagina();

            valor.valorpba4 = float.Parse(tbPBA4.Text);
            valor.valorpba3 = float.Parse(tbPBA3.Text);
            valor.valorcolora4 = float.Parse(tbColorA4.Text);
            valor.valorcolora3 = float.Parse(tbColorA3.Text);
            valor.valorscana4 = float.Parse(tbScannerA4.Text);
            valor.valorscana3 = float.Parse(tbScannerA3.Text);

            if (valor.Adicionar(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
            {
                Limpar();
            }
        }

        protected void TbSalvar_Click(object sender, EventArgs e)
        {
            dnaPrint.Base.ValorPagina valor = new Base.ValorPagina();
            valor.valorpba4 = float.Parse(tbPBA4.Text);
            valor.valorpba3 = float.Parse(tbPBA3.Text);
            valor.valorcolora4 = float.Parse(tbColorA4.Text);
            valor.valorcolora3 = float.Parse(tbColorA3.Text);
            valor.valorscana4 = float.Parse(tbScannerA4.Text);
            valor.valorscana3 = float.Parse(tbScannerA3.Text);
            if (valor.Atualizar(Session["ConnString"].ToString(), DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
            {
                Limpar();
            }

        }

        private void Limpar()
        {
            tbPBA4.Text = "";
            tbPBA3.Text = "";
            tbColorA4.Text = "";
            tbColorA3.Text = "";
            tbScannerA4.Text = "";
            tbScannerA3.Text = "";
            gvValor.DataBind();
        }

        protected void gvValor_DataBound(object sender, EventArgs e)
        {
            if (gvValor.Rows.Count > 0)
            {
                tbAdicionar.Visible = false;
                TbSalvar.Enabled = true;
            }
        }

        protected void gvValor_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            GridViewRow row = gvValor.Rows[Convert.ToInt32(e.CommandArgument)];
            Session["idEquipamento"] = row.Cells[1].Text;

            tbPBA4.Text = row.Cells[1].Text;
            tbPBA3.Text = row.Cells[2].Text;
            tbColorA4.Text = row.Cells[3].Text;
            tbColorA3.Text = row.Cells[4].Text;
            tbScannerA4.Text = row.Cells[5].Text;
            tbScannerA3.Text = row.Cells[6].Text;
        }
    }
}