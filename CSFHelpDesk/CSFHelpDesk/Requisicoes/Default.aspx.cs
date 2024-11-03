using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Requisicoes_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbUsername.Text = User.Identity.Name;
        }
    }

    protected void btNova_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Requisicoes/Novarequisicao.aspx");
    }

    protected void gvRequisicoes_SelectedIndexChanged(object sender, EventArgs e)
    {
        string codReq = gvRequisicoes.SelectedRow.Cells[2].Text;
        Requisicao r = Requisicao.Buscar(codReq);
        if (r != null)
        {
            tbcodReqFinal.Text = r.CodReq;
            lbReq.Text = r.CodReq;
            tbEqpto.Text = r.Serie;
            tbStatus.Text = r.Status;
            tbUltimaModificacao.Text = r.DtModificacao;
            tbCategoria.Text = r.Categoria;
            tbResumo.Text = r.Resumo;
            tbDescricao.Text = r.Descricao;

            pnReq.Visible = true;
        }
        else
        {
            pnReq.Visible = false;
        }
        pnComentario.Visible = false;

    }

    protected void btFechar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Requisicoes/Default.aspx");
    }

    protected void btComentario_Click(object sender, EventArgs e)
    {
        pnComentario.Visible = true;
    }

    protected void btSalvarComentario_Click(object sender, EventArgs e)
    {
        LogReq log = new LogReq();
        if (log.Adicionar(tbcodReqFinal.Text, LogReq.tpComentario.Comentario, User.Identity.Name, tbNovoComentario.Text))
        {
            dsLogs.DataBind();
            gvLogs.DataBind();
            pnComentario.Visible = false;
            tbNovoComentario.Text = "";
            gvRequisicoes.DataBind();
        }
        
    }

    protected void btSimEncerra_Click(object sender, EventArgs e)
    {
        Requisicao.AtualizarStatus(tbcodReqFinal.Text, User.Identity.Name, Requisicao.status.Fechado);
        Response.Redirect("~/Requisicoes/Default.aspx");
    }

    protected void btencerrar_Click(object sender, EventArgs e)
    {
        pnEncerrar.Visible = true;
        lbReq1.Text = tbcodReqFinal.Text;
        pnReq.Visible = false;
    }

}