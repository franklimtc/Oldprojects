using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Atendimentos_new_concluir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            tbidreq.Text = Request.QueryString["idreq"];
            dsReqAtendimento.DataBind();
            CarregarPendenciasPecas();
        }

    }

    private void CarregarPendenciasPecas()
    {
        GridViewRow row = gvreqAtendimento.Rows[0];
        string requsd = row.Cells[2].Text;
        string sqlUpdateHistorico = string.Format(@"select idreqPeca, partNumber, peca, status, dtSolicitacao
,(select top 1 postagem from atulPecas where idreqPeca = reqpecas.idreqPeca and postagem is not null and postagem <> '' )  'Postagem'
,(select top 1 dtEnvio from atulPecas where idreqPeca = reqpecas.idreqPeca and postagem is not null and postagem <> '' )  'dtEnvio'
 from reqPecas where reqUSD like '%{0}%'", requsd);
        dsHistoricoAgendamento.SelectCommand = sqlUpdateHistorico;
        gvHistoricoAgendamento.DataBind();
        
    }
    protected void tbVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"Default.aspx");
    }
    protected void tbAgendar_Click(object sender, EventArgs e)
    {
        string updateStatus = null;
        if (checkPendencias.Checked)
        {
            updateStatus = string.Format("update reqAtendimentos set status = 'Pendente Peças/Suprimentos' where idReqAtendimento = {0}", tbidreq.Text);
            
        }
        else
        {
            updateStatus = string.Format("update reqAtendimentos set status = 'Concluído' where idReqAtendimento = {0}", tbidreq.Text);
        }
        DAO.ExecuteNonQuery(DAO.connString(), updateStatus);
        Response.Redirect(@"Default.aspx");
    }
}