using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitacoes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tbSolicitante.Text = User.Identity.Name;
    }
    protected void btSolicitar_Click(object sender, EventArgs e)
    {
        List<reqPecas> lista = new List<reqPecas>();

        if (partNumber1.Enabled)
        {
            reqPecas newReq = new reqPecas();

            newReq.SerieEqpto = dpSerie.SelectedValue;
            newReq.ReqUSD = tbReqUSD.Text;
            newReq.DtSolicitacao = dtReqUSD.Text;
            newReq.Solicitante = tbSolicitante.Text;
            newReq.Tecnico = tbTecnico.Text;
            newReq.IdCliente = dpClientes.SelectedValue;

            newReq.PartNumber = partNumber1.Text;
            newReq.Peca = peca1.Text;
            newReq.Qtd = qtd1.Text;
            if (check1.Checked)
            {
                newReq.Critico = "Sim";
            }
            else
            {
                newReq.Critico = "Não";
            }

            newReq.Cadastrar();
            lista.Add(newReq);
        }

        if (partNumber2.Enabled)
        {
            reqPecas newReq = new reqPecas();

            newReq.SerieEqpto = dpSerie.SelectedValue;
            newReq.ReqUSD = tbReqUSD.Text;
            newReq.DtSolicitacao = dtReqUSD.Text;
            newReq.Solicitante = tbSolicitante.Text;
            newReq.Tecnico = tbTecnico.Text;
            newReq.IdCliente = dpClientes.SelectedValue;

            newReq.PartNumber = partNumber2.Text;
            newReq.Peca = peca2.Text;
            newReq.Qtd = qtd2.Text;

            if (check2.Checked)
            {
                newReq.Critico = "Sim";
            }
            else
            {
                newReq.Critico = "Não";
            }

            newReq.Cadastrar();
            lista.Add(newReq);
        }

        if (partNumber3.Enabled)
        {
            reqPecas newReq = new reqPecas();

            newReq.SerieEqpto = dpSerie.SelectedValue;
            newReq.ReqUSD = tbReqUSD.Text;
            newReq.DtSolicitacao = dtReqUSD.Text;
            newReq.Solicitante = tbSolicitante.Text;
            newReq.Tecnico = tbTecnico.Text;
            newReq.IdCliente = dpClientes.SelectedValue;

            newReq.PartNumber = partNumber3.Text;
            newReq.Peca = peca3.Text;
            newReq.Qtd = qtd3.Text;

            if (check3.Checked)
            {
                newReq.Critico = "Sim";
            }
            else
            {
                newReq.Critico = "Não";
            }

            newReq.Cadastrar();
            lista.Add(newReq);
        }

        if (partNumber4.Enabled)
        {
            reqPecas newReq = new reqPecas();

            newReq.SerieEqpto = dpSerie.SelectedValue;
            newReq.ReqUSD = tbReqUSD.Text;
            newReq.DtSolicitacao = dtReqUSD.Text;
            newReq.Solicitante = tbSolicitante.Text;
            newReq.Tecnico = tbTecnico.Text;
            newReq.IdCliente = dpClientes.SelectedValue;

            newReq.PartNumber = partNumber4.Text;
            newReq.Peca = peca4.Text;
            newReq.Qtd = qtd4.Text;

            if (check4.Checked)
            {
                newReq.Critico = "Sim";
            }
            else
            {
                newReq.Critico = "Não";
            }

            newReq.Cadastrar();
            lista.Add(newReq);
        }

        if (partNumber5.Enabled)
        {
            reqPecas newReq = new reqPecas();

            newReq.SerieEqpto = dpSerie.SelectedValue;
            newReq.ReqUSD = tbReqUSD.Text;
            newReq.DtSolicitacao = dtReqUSD.Text;
            newReq.Solicitante = tbSolicitante.Text;
            newReq.Tecnico = tbTecnico.Text;
            newReq.IdCliente = dpClientes.SelectedValue;

            newReq.PartNumber = partNumber5.Text;
            newReq.Peca = peca5.Text;
            newReq.Qtd = qtd5.Text;

            if (check5.Checked)
            {
                newReq.Critico = "Sim";
            }
            else
            {
                newReq.Critico = "Não";
            }

            newReq.Cadastrar();
            lista.Add(newReq);
        }



        gvDados.DataBind();
        SolicitacoesPecas.DataBind();

        string assunto = "Solicitação de Peças para o Equipamento de Série: " + dpSerie.SelectedValue + " por " + tbSolicitante.Text;
        string emailLista = "djalma@csfdigital.com.br,alvaro@csfdigital.com.br,magda@csfdigital.com.br,priscila@csfdigital.com.br,ednalda@csfdigital.com.br";
        //string emailLista = "franklim@csfdigital.com.br";
        string mensagem = "";
        foreach (reqPecas req in lista)
        {
            mensagem += "\nPeça: " + req.Peca + " - " + "Part Number: " + req.PartNumber + " - Qtd: " + req.Qtd;
        }

        mensagem += "\n\nSolicitado em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".";
        Email.Enviar(assunto, mensagem, emailLista);

        Limpar();

    }

    private void Limpar()
    {
        tbReqUSD.Text = "";
        dtReqUSD.Text = "";
        tbSolicitante.Text = "";
        tbTecnico.Text = "";
        partNumber1.Text = "";
        partNumber2.Text = "";
        partNumber3.Text = "";
        partNumber4.Text = "";
        partNumber5.Text = "";
        peca1.Text = "";
        peca2.Text = "";
        peca3.Text = "";
        peca4.Text = "";
        peca5.Text = "";
        qtd1.Text = "";
        qtd2.Text = "";
        qtd3.Text = "";
        qtd4.Text = "";
        qtd5.Text = "";

        ativar2.Text = "Ativar";
        partNumber2.Enabled = false;
        peca2.Enabled = false;
        qtd2.Enabled = false;
        check2.Enabled = false;

        ativar3.Text = "Ativar";
        partNumber3.Enabled = false;
        peca3.Enabled = false;
        qtd3.Enabled = false;
        check3.Enabled = false;

        ativar4.Text = "Ativar";
        partNumber4.Enabled = false;
        peca4.Enabled = false;
        qtd4.Enabled = false;
        check4.Enabled = false;

        ativar5.Text = "Ativar";
        partNumber5.Enabled = false;
        peca5.Enabled = false;
        qtd5.Enabled = false;
        check5.Enabled = false;
    }

    protected void dpSerie_SelectedIndexChanged(object sender, EventArgs e)
    {
        SolicitacoesPecas.DataBind();
    }

    protected void ativar1_Click(object sender, EventArgs e)
    {
        if (ativar1.Text == "Ativar")
        {
            ativar1.Text = "Desativar";
            partNumber1.Enabled = true;
            peca1.Enabled = true;
            qtd1.Enabled = true;
        }
        else
        {
            ativar1.Text = "Ativar";
            partNumber1.Enabled = false;
            peca1.Enabled = false;
            qtd1.Enabled = false;
        }
    }
    protected void ativar2_Click(object sender, EventArgs e)
    {
        if (ativar2.Text == "Ativar")
        {
            ativar2.Text = "Desativar";
            partNumber2.Enabled = true;
            peca2.Enabled = true;
            qtd2.Enabled = true;
            check2.Enabled = true;
        }
        else
        {
            ativar2.Text = "Ativar";
            partNumber2.Enabled = false;
            peca2.Enabled = false;
            qtd2.Enabled = false;
            check2.Enabled = false;
        }
    }
    protected void ativar3_Click(object sender, EventArgs e)
    {
        if (ativar3.Text == "Ativar")
        {
            ativar3.Text = "Desativar";
            partNumber3.Enabled = true;
            peca3.Enabled = true;
            qtd3.Enabled = true;
            check3.Enabled = true;
        }
        else
        {
            ativar3.Text = "Ativar";
            partNumber3.Enabled = false;
            peca3.Enabled = false;
            qtd3.Enabled = false;
            check3.Enabled = false;
        }
    }
    protected void ativar4_Click(object sender, EventArgs e)
    {
        if (ativar4.Text == "Ativar")
        {
            ativar4.Text = "Desativar";
            partNumber4.Enabled = true;
            peca4.Enabled = true;
            qtd4.Enabled = true;
            check4.Enabled = true;

        }
        else
        {
            ativar4.Text = "Ativar";
            partNumber4.Enabled = false;
            peca4.Enabled = false;
            qtd4.Enabled = false;
            check4.Enabled = false;
        }
    }
    protected void ativar5_Click(object sender, EventArgs e)
    {
        if (ativar5.Text == "Ativar")
        {
            ativar5.Text = "Desativar";
            partNumber5.Enabled = true;
            peca5.Enabled = true;
            qtd5.Enabled = true;
            check5.Enabled = true;
        }
        else
        {
            ativar5.Text = "Ativar";
            partNumber5.Enabled = false;
            peca5.Enabled = false;
            qtd5.Enabled = false;
            check5.Enabled = false;
        }
    }
}