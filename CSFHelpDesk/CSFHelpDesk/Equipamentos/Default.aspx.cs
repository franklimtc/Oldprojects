using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Equipamentos_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Equipamento> listaEquipamentos = new List<Equipamento>();
            if (Account.Administrador(User.Identity.Name))
            {
                // Carregar todos os equipamentos
                listaEquipamentos = Equipamento.Listar();
                gvEquipamentos.Columns[0].Visible = false;
            }
            else
            {
                // Carregar equipamentos do usuário
                listaEquipamentos = Equipamento.ListarporUsuario(Account.RetornaUserId(User.Identity.Name));
                if (listaEquipamentos.Count == 0)
                {
                    // Carregar equipamentos do contrato
                    listaEquipamentos = Equipamento.ListarporContrato(Account.RetornaGrupo(User.Identity.Name), Account.RetornaUserId(User.Identity.Name));
                    addButtonEqpto();
                }
                else
                {
                    btVerEqptos.Visible = true;
                    gvEquipamentos.Columns[0].Visible = false;
                    lbVerEqptos.Visible = true;
                }
            }
            gvEquipamentos.DataSourceID = null;
            gvEquipamentos.DataSource = listaEquipamentos;
            gvEquipamentos.DataBind();
        }
    }

    private void addButtonEqpto()
    {
        btAssociarEquipamentos.Visible = true;
        gvEquipamentos.Columns[0].Visible = true;
    }

    protected void btAssociarEquipamentos_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow eqp in gvEquipamentos.Rows)
        {
            CheckBox ch = (CheckBox)eqp.FindControl("chSelect");
            if (ch != null)
            {
                if(ch.Checked)
                {
                    if(Equipamento.AssociarEquipamento(Account.RetornaUserId(User.Identity.Name), eqp.Cells[1].Text))
                    {
                        List<Equipamento> listaEquipamentos = new List<Equipamento>();
                        listaEquipamentos = Equipamento.ListarporContrato(Account.RetornaGrupo(User.Identity.Name), Account.RetornaUserId(User.Identity.Name));
                        gvEquipamentos.DataSource = listaEquipamentos;
                        gvEquipamentos.DataBind();
                    }
                }
            }
        }
    }

    protected void btVerEqptos_Click(object sender, EventArgs e)
    {
        List<Equipamento> listaEquipamentos = new List<Equipamento>();
        listaEquipamentos = Equipamento.ListarporContrato(Account.RetornaGrupo(User.Identity.Name), Account.RetornaUserId(User.Identity.Name));
        gvEquipamentos.DataSource = listaEquipamentos;
        gvEquipamentos.DataBind();
        gvEquipamentos.Columns[0].Visible = true;
        btAssociarEquipamentos.Visible = true;
        btVerEqptos.Visible = false;
        lbVerEqptos.Visible = false;
        lbAssociar.Visible = true;
    }
}