using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Correios_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dsCorreios.SelectCommand = string.Format("select * from vw_ChamadosCorreios where operador = '{0}'  and (status not in ('Atendido') OR STATUS IS NULL) or operador IS NULL ORDER BY operador desc, entregueEm DESC", User.Identity.Name.ToUpper());
        if (!IsPostBack)
        {
            dsCorreios.DataBind();
            gvCorreios.DataBind();
        }
    }
    protected void gvCorreios_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvCorreios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvCorreios.Rows[Convert.ToInt32(e.CommandArgument)];

        switch (e.CommandName)
        {
            case "Avaliar":
                if (!Correios.ReclamacaoAberta(row.Cells[4].Text))
                {
                    Correios.AbrirReclamacao(row.Cells[4].Text, User.Identity.Name.ToUpper());
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("alert('Ocorrencia já está sendo tratada!');"), true);
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "alert", string.Format("alert('Ocorrencia já está sendo tratada!');"));
                    Response.Redirect("Default.aspx");
                    
                }

                gvCorreios.DataBind();
                break;
            case "Atualizar":
                Correios logCorreio = new Correios(row.Cells[4].Text);
                if (logCorreio.IdChamadoCorreio != null)
                {
                    tbId.Text = logCorreio.IdChamadoCorreio;
                    tbPostagem.Text = logCorreio.Postagem;
                    tbOperador.Text = logCorreio.Operador;
                    tbProtocolo.Text = logCorreio.Protocolo;
                    if (logCorreio.DtAbertura != "")
                        tbAbertura.Text = DateTime.Parse(logCorreio.DtAbertura).ToString("dd/MM/yyyy");
                    if (logCorreio.DtFechamento != "")
                        tbFechamento.Text = DateTime.Parse(logCorreio.DtFechamento).ToString("dd/MM/yyyy");
                    tbObs.Text = logCorreio.Obs;
                }

                if (row.Cells[5].Text != "REVERSO")
                {
                    dsDadosEqpto.SelectCommand = string.Format("SELECT UF, Cidade, Endereco, Bairro, Cep, Contato FROM EQUIPAMENTOS WHERE SERIE = '{0}'", row.Cells[3].Text);
                    pnDadosEquipamento.Visible = true;
                }
                else
                {
                    pnDadosEquipamento.Visible = false;
                }

                pnOcorrencia.Visible = true;
                pnGeral.Visible = false;
                pnResumo.Visible = false;
                break;
            default:
                break;
        }
    }

    protected void gvOcorrencia_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        pnOcorrencia.Visible = false;
        dsCorreios.DataBind();
        gvCorreios.DataBind();
    }

    protected void btAtualizar_Click(object sender, EventArgs e)
    {
        Correios logCorreio = new Correios(tbPostagem.Text);
        logCorreio.Protocolo = tbProtocolo.Text;
        logCorreio.DtAbertura = tbAbertura.Text;
        logCorreio.DtFechamento = tbFechamento.Text;
        logCorreio.Obs = tbObs.Text;
        logCorreio.Atualizar();
        //dsCorreios.DataBind();
        //gvCorreios.DataBind();
        //pnOcorrencia.Visible = false;
        //pnGeral.Visible = true;
        //pnResumo.Visible = true;
        //pnConcluir.Visible = false;
        Limpar();
    }

    protected void btcancelar_Click(object sender, EventArgs e)
    {
        Correios.Cancelar(tbPostagem.Text, User.Identity.Name.ToUpper());
        dsCorreios.DataBind();
        gvCorreios.DataBind();
        pnOcorrencia.Visible = false;
        pnGeral.Visible = true;
        pnResumo.Visible = true;

    }

    protected void gvDadosEqpto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvDadosEqpto.Rows[Convert.ToInt32(e.CommandArgument)];

        switch (e.CommandName)
        {
            case "Corrigir":
                pnCep.Visible = true;
                tbcepAntigo.Text = row.Cells[4].Text;
                break;
            default:
                break;
        }
    }
    protected void btCorrigirCep_Click(object sender, EventArgs e)
    {
        string cepAntigo = tbcepAntigo.Text;
        string cepNovo = tbcepNovo.Text;
        if (cepAntigo != "" && cepNovo != "")
        {
            Equipamentos.CorrigirCeps(cepAntigo, cepNovo);
            reqSuprimentos.AtualizarPrazo(tbPostagem.Text);
            
        }
        pnCep.Visible = false;
        pnOcorrencia.Visible = false;
        pnGeral.Visible = true;
        pnResumo.Visible = true;
        pnConcluir.Visible = false;
    }

    protected void btConcluir_Click(object sender, EventArgs e)
    {
        pnConcluir.Visible = true;
    }

    protected void btSim_Click(object sender, EventArgs e)
    {
        Correios.Concluir(tbId.Text, true);
        Limpar();
    }

    

    protected void btNao_Click(object sender, EventArgs e)
    {
        Correios.Concluir(tbId.Text, false);
        Limpar();
    }

    private void Limpar()
    {
        dsCorreios.DataBind();
        dsChart.DataBind();
        gvCorreios.DataBind();
        gvResumo.DataBind();
        pnOcorrencia.Visible = false;
        pnGeral.Visible = true;
        pnResumo.Visible = true;
        pnConcluir.Visible = false;
    }
}
