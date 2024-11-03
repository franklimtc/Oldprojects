using dnaPrint.Base;
using System;

namespace dnaPrint.Web.Cadastros
{
    public partial class Modelos : System.Web.UI.Page
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
            ModeloEquipamento mod = new ModeloEquipamento();
            mod.Fabricante = tbFabricante.Text;
            mod.Modelo = tbModelo.Text;
            mod.Franquia = int.Parse(tbFranquia.Text);
            float fTemp = 0;

            if (float.TryParse(tbValor.Text, out fTemp))
            {
                mod.Valor = fTemp;
                if (mod.Adicionar(Session["ConnString"].ToString(), dnaPrint.DAO.Operacoes.DefinirTipo(Session["TipoDB"].ToString())))
                {
                    LimparCampos();
                }
            }
            else
            {
                lbErroValor.Visible = true;
            }

        }

        private void LimparCampos()
        {
            tbFabricante.Text = "";
            tbModelo.Text = "";
            tbValor.Text = "";
            tbFranquia.Text = "";
            lbErroValor.Visible = false;
            gvModelos.DataBind();
            tbFabricante.Focus();
        }
    }
}