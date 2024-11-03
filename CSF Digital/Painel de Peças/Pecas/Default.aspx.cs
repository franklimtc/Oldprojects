using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pecas_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void tbRequisicao_TextChanged(object sender, EventArgs e)
    {
        if (RV_IdRequisicao.IsValid && tbRequisicao.Text != "")
        {
            reqPecas r = reqPecas.ListarPorId(int.Parse(tbRequisicao.Text));

            if (r != null)
            {
                if (r.SerieEqpto != "" && r.SerieEqpto != null)
                {
                    tbSerie.Text = r.SerieEqpto;
                    tbPartNumber.Text = r.PartNumber;
                    tbPostagem.Focus();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), null, "alert('Informe um número de requisição válido!')", true);
                    LimparCampos();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), null, "alert('Informe um número de requisição válido!')", true);
                LimparCampos();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), null, "alert('Informe um número de requisição válido!')", true);
            LimparCampos();
        }

    }

    private void LimparCampos()
    {
        tbSerie.Text = "";
        tbPartNumber.Text = "";
        tbPostagem.Text = "";
        tbAR.Text = "";
        tbRequisicao.Focus();
        tbValor.Text = "";
    }

    protected void btLimpar_Click(object sender, EventArgs e)
    {
        LimparCampos();
    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (CVValor.IsValid)
        {


            bool result = false;
            if (tbSerie.Text != "")
            {
                //result = AtulPeca.Enviar(dpEstoque.SelectedItem.Text, dptpEnvio.SelectedValue, int.Parse(tbRequisicao.Text), tbSerie.Text, tbPartNumber.Text, tbPostagem.Text, tbObs.Text, User.Identity.Name, tbAR.Text);
                result = AtulPeca.Enviar(dpEstoque.SelectedItem.Text, dptpEnvio.SelectedValue, int.Parse(tbRequisicao.Text), tbSerie.Text, tbPartNumber.Text, tbPostagem.Text, tbObs.Text, User.Identity.Name, tbAR.Text, double.Parse(tbValor.Text));
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), null, "alert('Informe um número de requisição válido ou a postagem do envio!')", true);
            }

            if (result)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), null, "alert('Registro realizado com sucesso!')", true);
                LimparCampos();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), null, "alert('Falha ao tentar inserir o registro!')", true);
            }
        }
    }

    protected void MoneyValidate(object source, ServerValidateEventArgs args)
    {
        double result = 0;
        args.IsValid = double.TryParse(args.Value, out result);
    }

    protected void tbValor_TextChanged(object sender, EventArgs e)
    {

    }
}