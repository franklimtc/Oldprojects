using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_new_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gvAtendimentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvAtendimentos.Rows[Convert.ToInt32(e.CommandArgument)];
        int idReqAtendimento = Convert.ToInt32(row.Cells[1].Text);

        switch (e.CommandName)
        { 
            case "editar":
                Response.Redirect(string.Format("editar.aspx?idreq={0}", idReqAtendimento.ToString()));
                break;
            case "agendar":
                Response.Redirect(string.Format("agendar.aspx?idreq={0}",idReqAtendimento.ToString()));
                break;
            case "concluir":
                Response.Redirect(string.Format("concluir.aspx?idreq={0}", idReqAtendimento.ToString()));
                break;
            case "retorno":
                SolicitarRetorno(idReqAtendimento);
                break;

        }
    }

    private void SolicitarRetorno(int idreq)
    {
        string updateStatus = string.Format("update reqAtendimentos set status = 'Aguardando Previsão de Retorno', emailEnviado = 0 where idReqAtendimento = {0}", idreq.ToString());
        DAO.ExecuteNonQuery(DAO.connString(), updateStatus);
        gvAtendimentos.DataBind();
    }
}