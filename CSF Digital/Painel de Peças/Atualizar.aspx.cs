using System;
using System.Data;

public partial class Atualizar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String s = Request.QueryString["idreqPeca"];
        tbIdReqPeca.Text = s;
    }
    protected void btAtualizar_Click(object sender, EventArgs e)
    {
        if (tbdtPrevisao.Text == "")
        {
            tbdtPrevisao.Text = DateTime.Now.ToShortDateString();
        }

        if (reqPecas.Atualizar(tbIdReqPeca.Text, null, DateTime.Now.ToString("yyyyMMdd"), tbObs.Text, dpStatus.SelectedValue, User.Identity.Name, tbdtPrevisao.Text))
        {
            detalheChamado.DataBind();
            consultaDetalhe.DataBind();

            DataTable dtReq = DAO.RetornaDt(DAO.connString(), string.Format("select reqUSD, peca, serieEqpto, solicitante from reqpecas where idreqpeca = {1}", null, tbIdReqPeca.Text)); 
            string email =  DAO.ExecuteScalar(DAO.connString(), string.Format("select Email from aspnetdb.dbo.aspnet_Membership where UserId in (select UserId from aspnetdb.dbo.aspnet_Users where username = '{0}')", dtReq.Rows[0]["solicitante"].ToString()));
            string mensagem = null;
            string assunto = null;
            
            if (dpStatus.SelectedValue == "Atendido")
            {
                //Enviar email informando envio
                assunto = string.Format("{0} - {1} - Peça Enviada!", dtReq.Rows[0]["reqUSD"].ToString(), dtReq.Rows[0]["serieEqpto"].ToString());
                mensagem = string.Format("Peça ({0}) enviada com postagem {1}.", dtReq.Rows[0]["peca"].ToString(), null);
            }
            else
            { 
                //Enviar email informando mudança de status
                assunto = string.Format("{0} - {1} - {2}!", dtReq.Rows[0]["reqUSD"].ToString(), dtReq.Rows[0]["serieEqpto"].ToString(), dpStatus.SelectedValue);
                mensagem = string.Format(tbObs.Text);
            }

            //EmailWeb.Email msg = new EmailWeb.Email();
            EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();

            msg.Enviar(email, assunto, mensagem,"Painel de Peças");

            Limpar();
            //tbMensagem.Text = "Atualização realizada com sucesso!";
            Page.ClientScript.RegisterStartupScript(GetType(), "message", @"alert('Atualização realizada com sucesso!')", true);

        }
        else
        {
            //tbMensagem.Text = "Falha na atualização!";
            Page.ClientScript.RegisterStartupScript(GetType(), "message", @"alert('Falha na atualização!')", true);

        }
    }

    private void Limpar()
    {
        //tbIdReqPeca.Text = "";
        //tbPostagem.Text = "";
        //tbData.Text = "";
        tbObs.Text = "";
        dpStatus.SelectedIndex = 5;
    }
    protected void dpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (dpStatus.SelectedValue)
        { 
            //case "Atendido":
            //    tbPostagem.Enabled = true;
            //    lbdtPrevisao.Visible = false;
            //    tbdtPrevisao.Visible = false;
            //    break;
            case "Comprada":
                tbdtPrevisao.Visible = true;
                break;
            default:
                tbdtPrevisao.Visible = false;
                break;
        }
    }
    
}