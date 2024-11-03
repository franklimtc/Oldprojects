using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controls;

public partial class Saidas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            tbNotaFiscal.Focus();
        }
    }


    protected void tbPartNumber_TextChanged(object sender, EventArgs e)
    {
        Material mat = new Material(tbPartNumber.Text);
        string descricao = mat.Modelo + " - " + mat.Descricao;

        if (mat.Descricao == "" || mat.Descricao == null)
        {
            descricao = "Material não encontrado!";
            btInserir.Enabled = false;
            tbPartNumber.Focus();
        }
        else
        {
            btInserir.Enabled = true;
        }
        tbDescricao.Text = descricao;
    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (tbPartNumber.Text != "" && tbNotaFiscal.Text != "" && tbQtd.Text != "")
        {
            Saida s = new Saida();
            Material mat = new Material(tbPartNumber.Text);

            s.IdCliente = dpClientes.SelectedValue;
            s.IdEquipamento = dpEquipamentos.SelectedValue;
            s.TipoOperacao = dpOperacao.SelectedValue;
            s.Operador = User.Identity.Name;
            s.IdSolicitante = dpSolicitante.SelectedValue;
            s.NotaFiscal = tbNotaFiscal.Text;
            s.Qtd = tbQtd.Text;
            s.IdMaterial = mat.IdMaterial;

            if (s.Adicionar())
            {
                Limpar();
            }         
        }
        
    }

    private void Limpar()
    {
        tbPartNumber.Text = "";
        tbDescricao.Text = "";
        tbQtd.Text = "";
        tbNotaFiscal.Text = "";
        dsPendenctes.DataBind();
        gvPendentes.DataBind();
    }

    protected void gvPendentes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvPendentes.Rows[Convert.ToInt32(e.CommandArgument)];
        int idSaida = Convert.ToInt32(row.Cells[1].Text);

        switch (e.CommandName)
        {
          
            case "Concluir":
                if (Saida.ConcluirIlux(idSaida, User.Identity.Name))
                {
                    dsPendenctes.DataBind();
                    gvPendentes.DataBind();
                }
                
                break;
            default:
                break;

        }
    }
}