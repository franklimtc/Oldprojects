using System;
using System.Collections;
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
            //tbSerie.Focus();
            //tbRequisicao.Focus();
            tbPartNumber.Focus();
            dpEstoque.DataBind();
            dpEstoque.SelectedIndex = dpEstoque.Items.IndexOf(dpEstoque.Items.FindByText("Filial - FOR"));

        }
    }

    private void CarregarClienteDefault()
    {
        dpCliente.ClearSelection();
        dpCliente.Items.FindByText("Selecionar").Selected = true;
        tbSerie.Enabled = true;
    }

    protected void btLimpar_Click(object sender, EventArgs e)
    {
        Limpar();
    }

    protected void dptpEnvio_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (dptpEnvio.SelectedValue.ToString().Contains("Motoboy"))
        {
            tbPostagem.Enabled = false;
            tbValor.Enabled = true;
        }
        else
        {
            tbPostagem.Enabled = true;
            tbValor.Enabled = false;
        }
        //tbSerie.Focus();
        tbRequisicao.Focus();
    }

    protected void btInserir_Click(object sender, EventArgs e)
    {
        if (CustomValidator1.IsValid)
        {
            int qtdEnvios = int.Parse(tbQtd.Text);
            bool continuar = false;

            continuar = ValidaPartNumber();

            if (continuar)
            {
                if (qtdEnvios == 1 && dpCliente.SelectedItem.Text=="Selecionar") //Envio único
                {
                    EnvioSuprimento novoEnvio = new EnvioSuprimento(tbSerie.Text, tbPartNumber.Text, tbEtiqueta.Text, dptpEnvio.SelectedItem.Text, User.Identity.Name);
                    novoEnvio.Postagem = tbPostagem.Text;

                    //1 - Verificar Estoque
                    //2 - Registrar Envio
                    //3 - Baixa de Estoque
                    //4 - Informar operador/usuário

                    switch (dpEstoque.SelectedItem.Text)
                    {
                        case "CAPGV":
                            novoEnvio.Filial = "CAPGV";
                            break;
                        case "Filial - FOR":
                            novoEnvio.Filial = "FOR";
                            break;
                        case "Filial - SLZ":
                            novoEnvio.Filial = "SLZ";
                            break;
                        default:
                            break;

                    }
                    if (Produto.Saldo(tbPartNumber.Text, dpEstoque.SelectedItem.Text) > 0)
                    {
                        double valor = 0;
                        if (double.TryParse(tbValor.Text, out valor))
                        {
                            novoEnvio.Valor = valor;
                        }

                        if (novoEnvio.Adicionar())
                        {
                            gvEnvios.DataBind();
                            novoEnvio.BaixaEstoque(int.Parse(dpEstoque.SelectedItem.Value), Produto.Buscar(tbPartNumber.Text).Id, User.Identity.Name, tbSerie.Text);
                        }
                    }

                    if (tbRequisicao.Text != "")
                    {
                        reqSuprimentos req = new reqSuprimentos();

                        try
                        {
                            req = reqSuprimentos.Get(int.Parse(tbRequisicao.Text));
                        }
                        catch
                        {
                        }
                        if (!string.IsNullOrEmpty(req.ReqUSD))
                        {
                            novoEnvio.IdRequisicao = int.Parse(req.IdreqSuprimento);
                            if (req.Fechar(User.Identity.Name))
                            {
                                continuar = true;
                                #region Mensagem
                                string mensagem = string.Format(@"
                        Prezado(a),
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
            <p><i>Mensagem automática enviada pela empresa CSF Digital.</i></p>", Produto.Buscar(tbPartNumber.Text).Descricao, req.Serie, tbPostagem.Text);
                                #endregion
                                req.informarOperador(mensagem);
                            }
                        }
                        else
                        {
                            lbErroReq.Visible = true;
                        }
                    }
                    else
                    {
                        continuar = true;
                    }
                    Limpar();
                }
                else //Envios Múltiplos ou diretamente para o cliente sem Série
                {
                    string Filial = null;
                    switch (dpEstoque.SelectedItem.Text)
                    {
                        case "CAPGV":
                            Filial = "CAPGV";
                            break;
                        case "Filial - FOR":
                            Filial = "FOR";
                            break;
                        case "Filial - SLZ":
                            Filial = "SLZ";
                            break;
                        default:
                            break;

                    }

                    if (Produto.Saldo(tbPartNumber.Text, dpEstoque.SelectedItem.Text) >= qtdEnvios)
                    {
                        for (int i = 0; i < qtdEnvios; i++)
                        {
                            EnvioSuprimento novoEnvio = new EnvioSuprimento(dpCliente.SelectedItem.Value, tbPartNumber.Text, tbEtiqueta.Text, dptpEnvio.SelectedItem.Text, User.Identity.Name);
                            novoEnvio.Postagem = tbPostagem.Text;
                            novoEnvio.Filial = Filial;

                            double valor = 0;
                            if (double.TryParse(tbValor.Text, out valor))
                            {
                                novoEnvio.Valor = valor;
                            }

                            if (novoEnvio.Adicionar())
                            {
                                gvEnvios.DataBind();
                                novoEnvio.BaixaEstoque(int.Parse(dpEstoque.SelectedItem.Value), Produto.Buscar(tbPartNumber.Text).Id, User.Identity.Name, dpCliente.SelectedItem.Value);
                            }
                        }

                        if (!string.IsNullOrEmpty(tbRequisicao.Text))
                        {
                            reqSuprimentos req = new reqSuprimentos();
                            req.IdreqSuprimento = tbRequisicao.Text;
                            req.Fechar(User.Identity.Name);
                        }

                        Page.ClientScript.RegisterStartupScript(GetType(), "message", string.Format(@"alert('Foram registrados {0} envios!')", qtdEnvios), true);
                        Limpar();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", @"alert('Estoque selecionado não possui saldo suficiente!')", true);
                    }
                }
            }
            else
            {
                lbErroPartNumber.Visible = true;
                Limpar();
            }

        }
    }

    private bool ValidaPartNumber()
    {
        return Produto.Existe(tbPartNumber.Text);
    }

    private void Limpar()
    {
        tbEtiqueta.Text = "";
        tbPostagem.Text = "";
        tbSerie.Text = "";
        tbValor.Text = "";
        tbAR.Text = "";
        tbPartNumber.Text = "";
        lbErroPartNumber.Visible = false;
        //tbSerie.Focus();
        lbErroSaldo.Visible = false;
        //tbRequisicao.Focus();
        lbErroReq.Visible = false;
        tbRequisicao.Text = "";
        tbQtd.Text = "1";
        CarregarClienteDefault();   
        tbPartNumber.Focus();
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        if (tbSerie.Text != "" && tbSerie.Enabled == true)
        {
            //if (tbPartNumber.Text != "")
            if (true)
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

                            if (double.TryParse(tbValor.Text, out value))
                            {
                                args.IsValid = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (dpCliente.SelectedItem.Value != "Selecionar")
            {
                args.IsValid = true;
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

    protected void tbPartNumber_TextChanged(object sender, EventArgs e)
    {
        if (!ValidaPartNumber())
        {
            lbErroPartNumber.Visible = true;
            tbPartNumber.Focus();
        }
        else
        {
            lbErroPartNumber.Visible = false;
            tbRequisicao.Focus();
        }
    }

    protected void dpCliente_DataBinding(object sender, EventArgs e)
    {
        CarregarClienteDefault();
    }

    protected void dpCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dpCliente.SelectedItem.Value != "Selecionar")
        {
            tbSerie.Enabled = false;
            tbSerie.Text = "";
        }
        else
        {
            tbSerie.Enabled = true;
        }
    }
}