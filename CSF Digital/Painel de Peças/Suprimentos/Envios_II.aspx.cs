using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Suprimentos_Envios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbValor.Enabled = false;
            tbReqOcomon.Focus();
        }
    }
    protected void btLimpar_Click(object sender, EventArgs e)
    {

    }

    protected void dptpEnvio_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (dptpEnvio.SelectedValue.ToString().Contains("MB-"))
        {
            tbPostagem.Enabled = false;
            tbValor.Enabled = true;
        }
        else
        {
            tbPostagem.Enabled = true;
            tbValor.Enabled = false;
        }
        tbReqOcomon.Focus();
    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (CustomValidator1.IsValid && partNumberValidator.IsValid && chamadoValidator.IsValid)
        {
            string tsql = null;
            if(tbPostagem.Enabled)
            {
                if(rbList.SelectedValue=="FOR")
                    tsql = string.Format("insert into enviosSuprimentos(serie, partNumber, etiqueta, tpEnvio, postagem, usuario, idRequisicao, ar) VALUES('{0}','{1}','{2}','{3}','{4}','{5}', '{6}','{7}');", tbSerie.Text, tbPartNumber.Text, tbEtiqueta.Text, dptpEnvio.SelectedValue.ToString(), tbPostagem.Text, User.Identity.Name, tbReqOcomon.Text, tbAR.Text);
                else
                    tsql = string.Format("insert into enviosSuprimentos(serie, partNumber, etiqueta, tpEnvio, postagem, usuario, idRequisicao,ar, filial) VALUES('{0}','{1}','{2}','{3}','{4}','{5}', '{6}','{7}','SLZ');", tbSerie.Text, tbPartNumber.Text, tbEtiqueta.Text, dptpEnvio.SelectedValue.ToString(), tbPostagem.Text, User.Identity.Name, tbReqOcomon.Text, tbAR.Text);

            }
            else
            {
                if (rbList.SelectedValue == "FOR")
                    tsql = string.Format("insert into enviosSuprimentos(serie, partNumber, etiqueta, tpEnvio, valor, usuario, idRequisicao, ar) VALUES('{0}','{1}','{2}','{3}',{4},'{5}', '{6}','{7}');", tbSerie.Text, tbPartNumber.Text, tbEtiqueta.Text, dptpEnvio.SelectedValue.ToString(), double.Parse(tbValor.Text).ToString().Replace(",", "."), User.Identity.Name, tbReqOcomon.Text, tbAR.Text);
                else
                    tsql = string.Format("insert into enviosSuprimentos(serie, partNumber, etiqueta, tpEnvio, valor, usuario, idRequisicao, ar, filial) VALUES('{0}','{1}','{2}','{3}',{4},'{5}', '{6}','{7}', 'SLZ');", tbSerie.Text, tbPartNumber.Text, tbEtiqueta.Text, dptpEnvio.SelectedValue.ToString(), double.Parse(tbValor.Text).ToString().Replace(",", "."), User.Identity.Name, tbReqOcomon.Text, tbAR.Text);

            }

            //Chamados.ChamadosSoapClient reqs = new Chamados.ChamadosSoapClient();
            //reqs.Fechar(tbReqOcomon.Text, tbEtiqueta.Text, tbPostagem.Text, User.Identity.Name, tbSerie.Text);

            reqSuprimentos req = reqSuprimentos.Get(int.Parse(tbReqOcomon.Text));
            bool fechamento = req.Fechar(User.Identity.Name);
            //req.informarOperador(string.Format("Suprimento enviado com postagem {0} para atendimento da demanda {1} do equipamento de série {2}.", tbPostagem.Text, req.ReqUSD, req.Serie));
            string suprimento = "";

            switch (tbEtiqueta.Text.Substring(0, 2))
            {
                case "01":
                    suprimento = "cilindro";
                    break;
                case "02":
                    suprimento = "toner";
                    break;
                case "03":
                    suprimento = "toner";
                    break;
                default:
                    suprimento = "toner";
                    break;

            }
            
            #region Email
            string mensagem = string.Format(@"Prezado(a),
            <br>
            <br>
            <p>Foi enviado um <b>{0}</b>  com o seguinte código de postagem <b>{2}</b> para a impressora multifuncional de série <b>{1}</b>.</p>
            <p>Você poderá rastrear esse objeto através da seção <u>rastreamento de objetos</u> do <i>site</i> dos Correios (<a href='http://www.correios.com.br/'>www.correios.com.br</a>), informando o código de postagem ou clicando no link abaixo:</p>
            <p><a href='http://websro.correios.com.br/sro_bin/txect01$.QueryList?P_LINGUA=001&P_TIPO=001&P_COD_UNI={2}'>http://websro.correios.com.br/sro_bin/txect01$.QueryList?P_LINGUA=001&P_TIPO=001&P_COD_UNI={2}</a></p>
            <p>Os dados de rastreio estarão disponíveis no próximo dia útil.</p>
            <p>Em caso de dúvidas, entre em contato conosco pelos telefones abaixo:</p>
            <ul>
                <li>Números VoIP: *5516 e *5517 </li>
                <li>Telefones: (85) 3299-5516 e (85) 3299-5517</li>
                <li>E-mail: sac@csfdigital.com.br </li>
            </ul>
            <br>
            <p><i>Mensagem automática enviada pela empresa CSF Digital.</i></p>", suprimento, req.Serie, tbPostagem.Text);
            req.informarOperador(mensagem);
            
            #endregion

            if (fechamento)
            {
                DAO.ExecuteNonQuery(DAO.connString(), tsql);

                gvEnvios.DataBind();
                Limpar();
            }
           
        }
        tbReqOcomon.Focus();
    }

    private void Limpar()
    {
        tbEtiqueta.Text = "";
        tbPartNumber.Text = "";
        tbPostagem.Text = "";
        tbSerie.Text = "";
        tbValor.Text = "";
        tbReqOcomon.Text = "";
        tbAR.Text = "";
        tbReqOcomon.Focus();
    }

    protected void PartNumberValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        if (Validacoes.validaPartNumber(tbPartNumber.Text))
        {
            args.IsValid = true;
        }
    }

    protected void ChamadoValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        if (Validacoes.validaChamado(tbReqOcomon.Text, tbSerie.Text))
        {
            args.IsValid = true;
        }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        if (tbSerie.Text != "")
        {
            if (tbPartNumber.Text != "")
            {
                if (tbEtiqueta.Text != "")
                {
                    if (tbPostagem.Enabled)
                    {
                        if (tbPostagem.Text != "")
                        {
                            args.IsValid = true;
                        }
                    }
                    else
                    {
                        if (tbValor.Enabled)
                        {
                            double value = 0;

                            if (double.TryParse(tbValor.Text,out value))
                            {
                                args.IsValid = true;
                            }
                        }
                    }
                }
            }
        }
    }

    protected void gvEnvios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CustomValidator1.Enabled = false;

        GridViewRow row = gvEnvios.Rows[Convert.ToInt32(e.CommandArgument)];

        switch (e.CommandName)
        {
            case "Excluir":
                #region Comando Excluir
                string user = User.Identity.AuthenticationType;
                if (Usuarios.Administrador(User.Identity.Name))
                {
                    Envios.Excluir(row.Cells[0].Text);
                }
                
                gvEnvios.DataBind();

                #endregion
                break;
            default:
                break;
        }
        CustomValidator1.Enabled = true;

    }
}