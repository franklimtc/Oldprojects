using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cadastros_Equipamentos2 : System.Web.UI.Page
{
    protected void Page_Load()
    {
        
    }

    protected void LoadComplete(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string serie = Request.QueryString["serie"];
            if (serie != null && serie != "")
            {
                CarregarDadosEquipamento(serie);
            }
        }
    }


    private void CarregarDadosEquipamento(string serie)
    {
        Equipamentos eqp = Equipamentos.Localizar(serie.Trim());
        if (eqp.Serie != null && eqp.Serie != "")
        {
            dpUF.ClearSelection();
            dpUF.Items.FindByText(eqp.Uf).Selected = true;

            dpCidade.DataBind();
            dpCidade.ClearSelection();
            dpCidade.Items.FindByText(eqp.Cidade).Selected = true;

            tbUnidade.Text = eqp.Unidade;

        }
    }

    protected void ValidarSerie(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !Equipamentos.Existe(args.Value);
    }

    protected void ValidarCEP(object source, ServerValidateEventArgs args)
    {
        try
        {
            Util.ValidarCep(args.Value.Trim());
            args.IsValid = true;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", string.Format("alert('Falha ao pesquisar o CEP ({0})! ERRO: {1}.')", args.Value, ex.Message), true);
            args.IsValid = false;
        }
    }

    protected void btAdicionar_Click(object sender, EventArgs e)
    {
        if (CVSerie.IsValid)
        {
            if (CVCEP.IsValid)
            {
                //Cadastrar Equipamento
                //idCliente, idModelo, uf, cidade, endereco, enderecoNumero, unidade, setor, bairro, cep, contato, fone, email, serie, atualizadoPor
                Equipamentos eqp = new Equipamentos();
                eqp.IdCliente = int.Parse(dpClientes.SelectedItem.Value);
                eqp.IdModelo = int.Parse(dpModelo.SelectedItem.Value);
                eqp.idTecnico = int.Parse(dpTecnico.SelectedItem.Value);

                eqp.Uf = dpUF.SelectedItem.Text;
                eqp.Cidade = dpCidade.SelectedItem.Text;
                eqp.Endereco = tbEndereco.Text;
                eqp.enderecoNumero = tbNum.Text;
                eqp.Unidade = tbUnidade.Text;
                eqp.Setor = tbSetor.Text;
                eqp.Bairro = tbBairro.Text;
                eqp.Cep = tbCEP.Text;
                eqp.Contato = tbContato.Text;
                eqp.Fone = tbFone.Text;
                eqp.Email = tbEmail.Text;
                eqp.Serie = tbSerie.Text;
                if (eqp.adicionar(User.Identity.Name))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Equipamento adicionado com sucesso')", true);
                    tbSerie.Text = "";
                    LimparCampos();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('ERRO: falha ao tentar adicionar o equipamento')", true);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('ERRO: Equipamento já está cadastrado!')", true);
        }
    }

    protected void tbSerie_TextChanged(object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Buscando equipamento')", true);
        Equipamentos eqp = new Equipamentos();

        try
        {
            eqp = Equipamentos.Localizar(tbSerie.Text.Trim());
        }
        catch 
        {
            LimparCampos();
        }

        if (eqp.Serie != null)
        {
            btAdicionar.Enabled = false;
            btSalvar.Enabled = true;

            tbEndereco.Text = eqp.Endereco;
            tbUnidade.Text = eqp.Unidade;
            tbSetor.Text = eqp.Setor;
            tbBairro.Text = eqp.Bairro;
            tbCEP.Text = eqp.Cep;
            tbContato.Text = eqp.Contato;
            tbFone.Text = eqp.Fone;
            tbEmail.Text = eqp.Email;
            tbNum.Text = eqp.enderecoNumero;

            dpClientes.ClearSelection();
            dpClientes.Items.FindByValue(eqp.IdCliente.ToString()).Selected = true;

            dpTecnico.ClearSelection();
            dpTecnico.Items.FindByValue(eqp.idTecnico.ToString()).Selected = true;

            dpModelo.ClearSelection();
            dpModelo.Items.FindByValue(eqp.IdModelo.ToString()).Selected = true;

            dpUF.ClearSelection();
            dpUF.Items.FindByText(eqp.Uf).Selected = true;

            dpCidade.DataBind();

            dpCidade.ClearSelection();

            foreach (ListItem item in dpCidade.Items)
            {
                if (item.Text.ToLower().Equals(eqp.Cidade.ToLower()))
                {
                    dpCidade.Items.FindByText(item.Text).Selected = true;
                    break;
                }
            }
        }
        else
        {
            btAdicionar.Enabled = true;
            btSalvar.Enabled = false;
            LimparCampos();
        }
        
    }

    private void LimparCampos()
    {
        tbUnidade.Text = "";
        tbSetor.Text = "";
        tbEndereco.Text = "";
        tbNum.Text = "";
        tbBairro.Text = "";
        tbCEP.Text = "";
        tbContato.Text = "";
        tbFone.Text = "";
        tbEmail.Text = "";
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if (CVCEP.IsValid)
        {
            Equipamentos eqp = Equipamentos.Localizar(tbSerie.Text.Trim());
            eqp.Uf = dpUF.SelectedItem.Text;
            eqp.Cidade = dpCidade.SelectedItem.Text;
            eqp.idTecnico = int.Parse(dpTecnico.SelectedItem.Value);
            eqp.Endereco = tbEndereco.Text;
            eqp.enderecoNumero = tbNum.Text;
            eqp.Unidade = tbUnidade.Text;
            eqp.Setor = tbSetor.Text;
            eqp.Bairro = tbBairro.Text;
            eqp.Cep = tbCEP.Text;
            eqp.Contato = tbContato.Text;
            eqp.Fone = tbFone.Text;
            eqp.Email = tbEmail.Text;

            if (eqp.Atualizar(User.Identity.Name))
            {
                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('Equipamento atualizado com sucesso')", true);
                tbSerie.Text = "";
                LimparCampos();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alerta", "alert('ERRO: falha ao tentar atualizar o equipamento')", true);

            }
        }
    }
}