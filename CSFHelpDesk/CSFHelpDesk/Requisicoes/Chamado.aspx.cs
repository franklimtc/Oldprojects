using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Chamado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string grupo = Account.RetornaGrupo(User.Identity.Name);
            if (grupo != "")
            {
                if (grupo == "Administradores" || grupo == "Operadores")
                {
                    Requisicao req = Requisicao.Buscar(Request.QueryString["req"]);
                    lbReq.Text = req.CodReq;
                    tbContato.Text = req.AbertorPor.ToUpper();
                    tbCliente.Text = req.Cliente.ToUpper();
                    tbResumo.Text = req.Resumo;
                    tbDescricao.Text = req.Descricao;
                    tbEquipamento.Text = req.Serie;
                    tbStatus.Text = req.Status;
                    tbCategoria.Text = req.Categoria;
                    tbSuprimento.Text = req.Suprimento;
                    tbContador.Text = req.Contador;
                    if (req.Responsavel != "")
                    {
                        tbResponsavel.Text = req.Responsavel;
                        tbResponsavel.Visible = true;
                        dpResponsavel.Visible = false;
                    }
                    else
                    {
                        tbResponsavel.Visible = true;
                        btAtender.Visible = true;
                    }

                    if (req.Status != "Aberto")
                    {
                        btAtualizarStatus.Visible = true;
                        btComentario.Visible = true;
                        btAtender.Visible = false;
                    }
                    else
                    {
                        btAtender.Visible = true;
                    }
                    
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }

    protected void btAtender_Click(object sender, EventArgs e)
    {
        Requisicao.Atender(lbReq.Text, User.Identity.Name);
        Atualizar();
    }
    public void Atualizar()
    {
        Response.Redirect(string.Format("Chamado.aspx?req={0}", lbReq.Text));
    }

    protected void btAtualizarStatus_Click(object sender, EventArgs e)
    {
        tbStatus.Visible = false;
        pnComentarioStatus.Visible = true;

        List<string> lStatus = Requisicao.ListaStatus();
        foreach (string status in lStatus)
        {
            dpStatus.Items.Add(status);
        }
        foreach (ListItem l in dpStatus.Items)
        {
            if (l.Value.ToLower() == tbStatus.Text.ToLower())
            {
                dpStatus.SelectedIndex = dpStatus.Items.IndexOf(l);
            }
        }
        dpStatus.Visible = true;
    }

    protected void btSalvarcomentarioStatus_Click(object sender, EventArgs e)
    {
        if (tbStatus.Visible == true)
        {
            Comentar(LogReq.tpComentario.Comentario, tbComentarioStatus.Text);
        }
        else
        {
            Requisicao.status status = Requisicao.status.Aberto;

            switch (dpStatus.Text.ToLower())
            {
                case "aberto":
                    status = Requisicao.status.Aberto;
                    break;
                case "em atendimento":
                    status = Requisicao.status.Em_atendimento;
                    break;
                case "pendente":
                    status = Requisicao.status.Pendente;
                    break;
                case "pendente com usuario":
                    status = Requisicao.status.Pendente_com_Usuario;
                    break;
                case "fechado":
                    status = Requisicao.status.Fechado;
                    break;
                default:
                    break;
            }
            if (tbStatus.Text != dpStatus.SelectedItem.Value)
            {
                Requisicao.AtualizarStatus(lbReq.Text, User.Identity.Name, status);
                LogReq l = new LogReq();
                if (tbComentarioStatus.Text != "")
                    if (Comentar(LogReq.tpComentario.Atualizar_Status, tbComentarioStatus.Text))
                    {
                        tbComentarioStatus.Text = "";
                        pnComentarioStatus.Visible = false;
                    }
            }
        }
        Atualizar();
    }

    public bool Comentar(LogReq.tpComentario tpComentario, string comentario)
    {
        LogReq l = new LogReq();
        return l.Adicionar(lbReq.Text, tpComentario, User.Identity.Name, comentario);
    }



    protected void btCancelar_Click(object sender, EventArgs e)
    {
        pnComentarioStatus.Visible = false;
        tbComentarioStatus.Text = "";
        dpStatus.Visible = false;
        tbStatus.Visible = true;
    }

    protected void btComentario_Click(object sender, EventArgs e)
    {
        pnComentarioStatus.Visible = true;
    }
}