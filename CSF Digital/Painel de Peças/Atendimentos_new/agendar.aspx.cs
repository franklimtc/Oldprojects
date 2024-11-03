using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_new_agendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbidReq.Text = Request.QueryString["idreq"];
        }
    }
    protected void tbAgendar_Click(object sender, EventArgs e)
    {
        string tsqlInsert = string.Format("insert into reqAtendimentosPrevisao(idReqAtendimento,dtPrevisao) values({0},'{1}');",
            tbidReq.Text,calendarAgenda.SelectedDate.ToString("yyyy-MM-dd 00:00:00.000"));
        string tsqlPesquisa = string.Format("select count(*) from reqAtendimentosPrevisao where idReqAtendimento = {0}", tbidReq.Text);
        string log = null;
        int qtdLinhas = 0;
        try { qtdLinhas = Convert.ToInt32(DAO.ExecuteScalar(DAO.connString(), tsqlPesquisa)); }
        catch { }
        string updateStatus = null;
        if (qtdLinhas > 0)
        {
            log = string.Format("Atendimento reagendado para {0}.", calendarAgenda.SelectedDate.ToString("dd/MM/yyyy"));
            string tsqlUpdate1 = string.Format("update reqAtendimentosPrevisao set status = 0 where idReqAtendimento = {0}", tbidReq.Text);
            updateStatus = string.Format("update reqAtendimentos set status = 'Aguardando Técnico - Reagendado' where idReqAtendimento = {0}", tbidReq.Text);
            DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate1);
            
        }
        else
        {
            updateStatus = string.Format("update reqAtendimentos set status = 'Aguardando Técnico' where idReqAtendimento = {0}", tbidReq.Text);
            log = string.Format("Atendimento agendado para {0}.", calendarAgenda.SelectedDate.ToString("dd/MM/yyyy"));
        }

        string tsqlLog = string.Format("insert into reqAtendimentosLogs(idReqAtendimento,operador, log) values({0},'{1}','{2}')", tbidReq.Text, User.Identity.Name, log);

        DAO.Execute(tsqlInsert, "pecasSigep01");
        DAO.ExecuteNonQuery(DAO.connString(), updateStatus);
        DAO.Execute(tsqlLog, "pecasSigep01");
        gvHistoricoAgendamento.DataBind();
    }
    protected void tbVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"Default.aspx");
    }
}