using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_Atualizar : System.Web.UI.Page
{
    private string _idreqAtendimento;
    protected void Page_Load(object sender, EventArgs e)
    {
       // if (!IsPostBack)
        {
            _idreqAtendimento = Request.QueryString["idreqAtendimento"];
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        tbData.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        tbDataConclusao.Text = Calendar2.SelectedDate.ToString("dd/MM/yyyy");
    }
    protected void btSolAgendar_Click(object sender, EventArgs e)
    {
        mvAtualizar.ActiveViewIndex = 1;
    }
    protected void btConcluir_Click(object sender, EventArgs e)
    {
        mvAtualizar.ActiveViewIndex = 2;
    }
    protected void btCancelar_Click(object sender, EventArgs e)
    {
        reqAtendimentos.Cancelar(_idreqAtendimento, User.Identity.Name, "");
        Response.Redirect("Monitor.aspx");
    }

    protected void VoltarMonitor(object sender, EventArgs e)
    {
        Response.Redirect("Monitor.aspx");
    }
    

    protected void btAgendar_Click(object sender, EventArgs e)
    {
        reqAtendimentos.Agendar(_idreqAtendimento, tbData.Text, User.Identity.Name);
        dsHistoricoAgendamento.DataBind();
        gvHistoricoAgendamento.DataBind();
    }
    protected void btConcluir2_Click(object sender, EventArgs e)
    {
        if(checkPendencias.Checked)
        {
            reqAtendimentos.Concluir(_idreqAtendimento, tbDataConclusao.Text, User.Identity.Name, "1");
            Response.Redirect("Monitor.aspx");
        }
        else
        {
            reqAtendimentos.Concluir(_idreqAtendimento,tbDataConclusao.Text,User.Identity.Name,"0");
            Response.Redirect("Monitor.aspx");
        }
        
    }
    protected void btRetorno_Click(object sender, EventArgs e)
    {
        reqAtendimentos.SolicitarRetorno(_idreqAtendimento, User.Identity.Name);
        Response.Redirect("Monitor.aspx");
    }
}