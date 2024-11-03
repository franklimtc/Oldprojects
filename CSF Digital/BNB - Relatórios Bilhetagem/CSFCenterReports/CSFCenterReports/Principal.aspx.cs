using System;
using System.Web;
using System.Data;
using System.Web.UI;

using System.Collections;
using System.Web.Security;
using CSFCenterReports.Controls;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using Telerik.Web.UI;

namespace CSFCenterReports
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                #region Define parametros
                for (int i = 0; i < Enum.GetNames(typeof(Relatorios)).Length; i++)
                {
                    Relatorios relatorio = ((Relatorios)i);
                    string nomeRelatorio = String.Empty;
                    switch (relatorio)
                    {
                        case Relatorios.R1_Produção_CopiaImpressaoFax:
                            nomeRelatorio = "1 - Relatório de Produção (Cópia / Impressão / Fax)";
                            break;
                        case Relatorios.R2_AnaliticoDeBilhetagem:
                            nomeRelatorio = "2 - Relatório Analítico de Bilhetagem";
                            break;
                        case Relatorios.R3_ImpressaoPorUsuario:
                            nomeRelatorio = "3 - Relatório de Impressões por usuário";
                            break;
                        case Relatorios.R4_ImpressaoPorUsuario_Detalhado:
                            nomeRelatorio = "4 - Relatório de Impressões por usuário (Detalhado)";
                            break;
                        case Relatorios.R5_ImpressaoPorUsuario_Consolidado:
                            nomeRelatorio = "5 - Relatório de Impressões por usuário (Consolidado)";
                            break;

                        default:
                            nomeRelatorio = "";
                            break;
                    }
                    if (nomeRelatorio != "")
                        rbRelatorios.Items.Add(new ListItem(nomeRelatorio, i.ToString()));
                }
                rbRelatorios.SelectedIndex = 0;
                lbDescricaoRelatorio.Text = "Lista o total de impressões, cópias e faxes realizados por impressora.";

                rbRelatorios_SelectedIndexChanged(sender, null);

                rcbUf.Enabled = true;
                rcbCidade.Items.Clear();
                rcbCidade.Enabled = false;
                rcbUnidade.Items.Clear();
                rcbUnidade.Enabled = false;
                rcbCelula.Items.Clear();
                rcbCelula.Enabled = false;
                rcbAmbiente.Items.Clear();
                rcbAmbiente.Enabled = false;
                rlbImpressoras.Items.Clear();
                rlbImpressoras.Enabled = false;
                rtbUsuario.Text = "";
                #endregion

                #region Carrega datas
                rdpInicial.SelectedDate = DateTime.Now.AddDays(-1).Date;
                rdpFinal.SelectedDate = DateTime.Now.AddDays(-1).Date;
                #endregion

                if ((bool)Session["vip"] || (bool)Session["admin"])
                {

                }
                else
                {
                    Grupo grupo = Grupo.RetornaGrupo(Session["grupo"].ToString());

                    if (grupo != null)
                    {
                        lbUfValor.Text = grupo.Uf;
                        lbCidadeValor.Text = grupo.Cidade;
                        lbUnidadeValor.Text = grupo.Unidade;
                        lbCelulaValor.Text = grupo.Codigo;
                        lbAmbienteValor.Text = grupo.Setor;

                        rlbImpressoras.Enabled = true;
                        rlbImpressoras.Items.Clear();

                        rlbImpressoras.Items.Add(new RadListBoxItem("Todas", "Todas"));
                        rlbImpressoras.Items[0].Selected = true;

                        //foreach (string imp in Grupo.RetornaImpressoras(lbUfValor.Text, lbCidadeValor.Text, lbUnidadeValor.Text, lbCelulaValor.Text, "", "true"))
                        //{
                        //    rlbImpressoras.Items.Add(new RadListBoxItem(imp, imp));
                        //}

                        foreach (UsuarioFila u in UsuarioFila.RetornaFilasUsuario(Session["login"].ToString()))
                        {
                            rlbImpressoras.Items.Add(new RadListBoxItem(u.CodFila, u.CodFila));
                        }
                    }
                }
            }
            rcbAmbiente.Visible = false;
            lbAmbiente.Visible = false;
        }
        protected void rbEmitir_Click(object sender, EventArgs e)
        {
            bool emitir = false;

            string Uf = "";
            string Cidade = "";
            string Unidade = "";
            string Celula = "";
            string Ambiente = "";
            string Impressoras = "";
            string Usuarios = "";
            string FormatoFolha = "";
            string Cor = "";

            DateTime dtInicial = new DateTime();
            DateTime dtFinal = new DateTime();

            Relatorios relatorio = (Relatorios)int.Parse(rbRelatorios.SelectedValue.ToString());

            if (relatorio == Relatorios.R1_Produção_CopiaImpressaoFax)
            {
                if (rcbUf.Visible)
                {
                    Uf = rcbUf.SelectedValue;
                    Cidade = rcbCidade.SelectedValue;
                    Unidade = rcbUnidade.SelectedValue;
                    Celula = rcbCelula.SelectedValue;
                    Ambiente = rcbAmbiente.SelectedValue;
                }
                else
                {
                    Uf = lbUfValor.Text;
                    Cidade = lbCidadeValor.Text;
                    Unidade = lbUnidadeValor.Text;
                    Celula = lbCelulaValor.Text;
                    Ambiente = lbAmbienteValor.Text;
                }
                foreach (RadListBoxItem item in rlbImpressoras.Items)
                {
                    if (item.Checked)
                    {
                        if (item.Value == "Todas")
                        {
                            Impressoras = "Todas";
                            break;
                        }
                        else
                        {
                            Impressoras += item.Value.Trim() + ",";
                        }
                    }
                }

                Usuarios = "";
                FormatoFolha = "";
                Cor = "";

                emitir = true;
            }
            else if (relatorio == Relatorios.R2_AnaliticoDeBilhetagem)
            {
                if (rcbUf.Visible)
                {
                    Uf = rcbUf.SelectedValue;
                    Cidade = rcbCidade.SelectedValue;
                    Unidade = rcbUnidade.SelectedValue;
                    Celula = rcbCelula.SelectedValue;
                    Ambiente = rcbAmbiente.SelectedValue;
                }
                else
                {
                    Uf = lbUfValor.Text;
                    Cidade = lbCidadeValor.Text;
                    Unidade = lbUnidadeValor.Text;
                    Celula = lbCelulaValor.Text;
                    Ambiente = lbAmbienteValor.Text;
                }

                foreach (RadListBoxItem item in rlbImpressoras.Items)
                {
                    if (item.Checked)
                    {
                        if (item.Value == "Todas")
                        {
                            Impressoras = "Todas";
                            break;
                        }
                        else
                        {
                            Impressoras += item.Value + ",";
                        }
                    }
                }

                Usuarios = "";
                FormatoFolha = "";
                Cor = "";

                emitir = true;
            }
            else if (relatorio == Relatorios.R3_ImpressaoPorUsuario || relatorio == Relatorios.R5_ImpressaoPorUsuario_Consolidado)
            {
                if (rcbUf.Visible)
                {
                    Uf = rcbUf.SelectedValue;
                    Cidade = rcbCidade.SelectedValue;
                    Unidade = rcbUnidade.SelectedValue;
                    Celula = rcbCelula.SelectedValue;
                    Ambiente = rcbAmbiente.SelectedValue;
                }
                else
                {
                    Uf = lbUfValor.Text;
                    Cidade = lbCidadeValor.Text;
                    Unidade = lbUnidadeValor.Text;
                    Celula = lbCelulaValor.Text;
                    Ambiente = lbAmbienteValor.Text;
                }
                Impressoras = "";

                foreach (RadListBoxItem item in rlbImpressoras.Items)
                {
                    if (item.Checked)
                    {
                        if (item.Value == "Todas")
                        {
                            Impressoras = "Todas";
                            break;
                        }
                        else
                        {
                            Impressoras += item.Value + ",";
                        }
                    }
                }

                if (rtbUsuario.Text.Trim() != "")
                {
                    Usuarios = rtbUsuario.Text;

                    if (rcbFormatoFolha.SelectedIndex >= 0)
                    {
                        FormatoFolha = rcbFormatoFolha.SelectedValue;

                        if (rcbCor.SelectedIndex >= 0)
                        {
                            Cor = rcbCor.SelectedValue;
                            emitir = true;
                        }
                    }
                }
            }
            else if (relatorio == Relatorios.R4_ImpressaoPorUsuario_Detalhado)
            {
                if (rcbUf.Visible)
                {
                    Uf = rcbUf.SelectedValue;
                    Cidade = rcbCidade.SelectedValue;
                    Unidade = rcbUnidade.SelectedValue;
                    Celula = rcbCelula.SelectedValue;
                    Ambiente = rcbAmbiente.SelectedValue;
                }
                else
                {
                    Uf = lbUfValor.Text;
                    Cidade = lbCidadeValor.Text;
                    Unidade = lbUnidadeValor.Text;
                    Celula = lbCelulaValor.Text;
                    Ambiente = lbAmbienteValor.Text;
                }
                Impressoras = "";

                foreach (RadListBoxItem item in rlbImpressoras.Items)
                {
                    if (item.Checked)
                    {
                        if (item.Value == "Todas")
                        {
                            Impressoras = "Todas";
                            break;
                        }
                        else
                        {
                            Impressoras += item.Value + ",";
                        }
                    }
                }

                if (rcbFormatoFolha.SelectedIndex >= 0)
                {
                    FormatoFolha = rcbFormatoFolha.SelectedValue;

                    if (rcbCor.SelectedIndex >= 0)
                    {
                        Cor = rcbCor.SelectedValue;
                        emitir = true;
                    }
                }

                if (rtbUsuario.Text.Trim() != "")
                {
                    Usuarios = rtbUsuario.Text;                    
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Para emitir o relatório defina pelo menos um usuário.');", true);
                    rtbUsuario.Focus();
                    return;
                }
            }

            if (rdpInicial.SelectedDate.HasValue)
            {
                dtInicial = rdpInicial.SelectedDate.Value;
                if (rdpFinal.SelectedDate.HasValue)
                {
                    dtFinal = rdpFinal.SelectedDate.Value;
                    emitir = true;
                }
            }

            if (dtFinal.Subtract(dtInicial).Days > 91)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Para emitir o relatório defina um intervalo de até 90 dias.');", true);
                rdpInicial.Focus();
                emitir = false;
            }
            else
            {
                emitir = true;
            }

            if (emitir)
            {
                if ((bool)Session["vip"] || (bool)Session["admin"])
                {
                    string config = "location=no,width=1110,height=850,top=10,left=10,resizable=yes";
                    string script = String.Format("window.open('ShowReports.aspx?relatorio={0}&uf={1}&cidade={2}&unidade={3}&celula={4}&ambiente={5}&impressoras={6}&usuarios={7}&formatofolha={8}&cor={9}&dtInicial={10}&dtFinal={11}&user={12}','','{13}');",
                        rbRelatorios.SelectedValue, Uf, Cidade, Unidade, Celula, Ambiente, Impressoras, Usuarios, FormatoFolha, Cor, dtInicial.ToBinary(), dtFinal.ToBinary(), Session["login"].ToString(), config);

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "report", script, true);
                }
                else
                {
                    if (Impressoras == "Todas" || Impressoras == "")
                    {
                        foreach (RadListBoxItem item in rlbImpressoras.Items)
                        {
                            Impressoras += item.Value + ",";
                        }
                    }

                    string config = "location=no,width=1110,height=850,top=10,left=10,resizable=yes";
                    string script = String.Format("window.open('ShowReports.aspx?relatorio={0}&uf={1}&cidade={2}&unidade={3}&celula={4}&ambiente={5}&impressoras={6}&usuarios={7}&formatofolha={8}&cor={9}&dtInicial={10}&dtFinal={11}&user={12}','','{13}');",
                        rbRelatorios.SelectedValue, "", "", "", "", "", Impressoras, Usuarios, FormatoFolha, Cor, dtInicial.ToBinary(), dtFinal.ToBinary(), Session["login"].ToString(), config);

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "report", script, true);
                }
            }
        }

        protected void rcbUf_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCidade.Enabled = true;
            rcbCidade.Items.Clear();

            rcbUnidade.Enabled = true;
            rcbUnidade.Items.Clear();
            rcbUnidade.Enabled = false;

            rcbCelula.Enabled = true;
            rcbCelula.Items.Clear();
            rcbCelula.Enabled = false;

            rcbAmbiente.Enabled = true;
            rcbAmbiente.Items.Clear();
            rcbAmbiente.Enabled = false;

            rlbImpressoras.Enabled = true;
            rlbImpressoras.Items.Clear();
            rlbImpressoras.Enabled = false;

            string strUf = rcbUf.SelectedValue.ToString().Trim();

            if (strUf != "")
            {
                ObjectDataSourceCidade.SelectParameters[0].DefaultValue = strUf;
            }
            else
            {
                ObjectDataSourceCidade.SelectParameters[0].DefaultValue = null;
            }

            ObjectDataSourceCidade.DataBind();
            rcbCidade.DataBind();
        }
        protected void rcbCidade_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbUnidade.Enabled = true;
            rcbUnidade.Items.Clear();

            rcbCelula.Enabled = true;
            rcbCelula.Items.Clear();
            rcbCelula.Enabled = false;

            rcbAmbiente.Enabled = true;
            rcbAmbiente.Items.Clear();
            rcbAmbiente.Enabled = false;

            rlbImpressoras.Enabled = true;
            rlbImpressoras.Items.Clear();
            rlbImpressoras.Enabled = false;

            string strUf = rcbUf.SelectedValue.ToString().Trim();
            string strCidade = rcbCidade.SelectedValue.ToString().Trim();

            if (strUf != "" && strCidade != "")
            {
                ObjectDataSourceUnidade.SelectParameters[0].DefaultValue = strUf;
                ObjectDataSourceUnidade.SelectParameters[1].DefaultValue = strCidade;
            }
            else
            {
                ObjectDataSourceUnidade.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceUnidade.SelectParameters[1].DefaultValue = null;
            }

            ObjectDataSourceUnidade.DataBind();
            rcbUnidade.DataBind();
        }
        protected void rcbUnidade_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCelula.Enabled = true;
            rcbCelula.Items.Clear();
            rcbAmbiente.Enabled = true;
            rcbAmbiente.Items.Clear();

            rlbImpressoras.Enabled = true;
            rlbImpressoras.Items.Clear();
            rlbImpressoras.Enabled = false;

            string strUf = rcbUf.SelectedValue.ToString().Trim();
            string strCidade = rcbCidade.SelectedValue.ToString().Trim();
            string strUnidade = rcbUnidade.SelectedValue.ToString().Trim();

            if (strUf != "" && strCidade != "")
            {
                ObjectDataSourceCelula.SelectParameters[0].DefaultValue = strUf;
                ObjectDataSourceCelula.SelectParameters[1].DefaultValue = strCidade;
                ObjectDataSourceCelula.SelectParameters[2].DefaultValue = strUnidade;

                ObjectDataSourceSetor.SelectParameters[0].DefaultValue = strUf;
                ObjectDataSourceSetor.SelectParameters[1].DefaultValue = strCidade;
                ObjectDataSourceSetor.SelectParameters[2].DefaultValue = strUnidade;
            }
            else
            {
                ObjectDataSourceCelula.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceCelula.SelectParameters[1].DefaultValue = null;
                ObjectDataSourceCelula.SelectParameters[2].DefaultValue = null;

                ObjectDataSourceSetor.SelectParameters[0].DefaultValue = null;
                ObjectDataSourceSetor.SelectParameters[1].DefaultValue = null;
                ObjectDataSourceSetor.SelectParameters[2].DefaultValue = null;
            }

            ObjectDataSourceCelula.DataBind();
            rcbCelula.DataBind();

            ObjectDataSourceSetor.DataBind();
            rcbAmbiente.DataBind();
        }

        protected void rcbCelula_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string strUf = rcbUf.SelectedValue.ToString().Trim();
            string strCidade = rcbCidade.SelectedValue.ToString().Trim();
            string strUnidade = rcbUnidade.SelectedValue.ToString().Trim();
            string strCelula = rcbCelula.SelectedValue.ToString().Trim();
            string strAmbiente = rcbAmbiente.SelectedValue.ToString().Trim();

            rlbImpressoras.Enabled = true;
            rlbImpressoras.Items.Clear();

            rlbImpressoras.Items.Add(new RadListBoxItem("Todas", "Todas"));
            rlbImpressoras.Items[0].Selected = true;

            if (strCelula == "Todos")
            {
                rcbAmbiente.Items[0].Selected = true;
            }
            else
            {
                string ambiente = Grupo.RetornaAmbiente(strCelula);
                foreach (RadComboBoxItem amb in rcbAmbiente.Items)
                {
                    if (amb.Value == ambiente)
                        amb.Selected = true;
                }
            }

            foreach (string imp in Grupo.RetornaImpressoras(strUf, strCidade, strUnidade, strCelula, "", "true"))
            {
                rlbImpressoras.Items.Add(new RadListBoxItem(imp, imp));
            }
        }

        protected void rcbAmbiente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string strUf = rcbUf.SelectedValue.ToString().Trim();
            string strCidade = rcbCidade.SelectedValue.ToString().Trim();
            string strUnidade = rcbUnidade.SelectedValue.ToString().Trim();
            string strCelula = rcbCelula.SelectedValue.ToString().Trim();
            string strAmbiente = rcbAmbiente.SelectedValue.ToString().Trim();

            rlbImpressoras.Enabled = true;
            rlbImpressoras.Items.Clear();

            rlbImpressoras.Items.Add(new RadListBoxItem("Todas", "Todas"));
            rlbImpressoras.Items[0].Selected = true;

            if (strAmbiente == "Todos")
            {
                rcbCelula.Items[0].Selected = true;
            }
            else
            {
                string celula = Grupo.RetornaCelula(strUf, strCidade, strUnidade, strAmbiente);
                foreach (RadComboBoxItem cel in rcbCelula.Items)
                {
                    if (cel.Value == celula)
                        cel.Selected = true;
                }
            }

            foreach (string imp in Grupo.RetornaImpressoras(strUf, strCidade, strUnidade, "", strAmbiente, "true"))
            {
                rlbImpressoras.Items.Add(new RadListBoxItem(imp, imp));
            }
        }

        protected void rbRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relatorios relatorio = (Relatorios)int.Parse(rbRelatorios.SelectedValue.ToString());

            if (relatorio == Relatorios.R1_Produção_CopiaImpressaoFax)
            {
                lbDescricaoRelatorio.Text = "-> Lista o total de impressões, cópias e faxes realizados por impressora.";
                ControleVisibilidade(true, true, true, true, true, true, false, false, false);
            }
            else if (relatorio == Relatorios.R2_AnaliticoDeBilhetagem)
            {
                lbDescricaoRelatorio.Text = "-> Lista os contadores físicos inicial e final por impressora.";
                ControleVisibilidade(true, true, true, true, true, true, false, false, false);
            }
            else if (relatorio == Relatorios.R3_ImpressaoPorUsuario || relatorio == Relatorios.R5_ImpressaoPorUsuario_Consolidado)
            {
                lbUsuarios.Text = "Usuários";
                lbDescricaoRelatorio.Text = " -> Lista informações sobre os documentos impressos, como usuário, quantidade de páginas impressas, data, hora e formato de folha.";
                ControleVisibilidade(true, true, true, true, true, true, true, true, true);
            }
            else if (relatorio == Relatorios.R4_ImpressaoPorUsuario_Detalhado)
            {
                lbUsuarios.Text = "*Usuários";
                lbDescricaoRelatorio.Text = "-> Lista informações sobre os documentos impressos, como nome do documento, usuário, quantidade de páginas impressas, data, hora e formato de folha.";
                ControleVisibilidade(true, true, true, true, true, true, true, true, true);
            }
            else
            {
                lbUsuarios.Text = "Usuários";
                lbDescricaoRelatorio.Text = "-> Lista a quantidade de páginas impressas por usuário.";
                ControleVisibilidade(false, false, false, false, false, false, false, false, false);
            }
        }
        protected void rlbImpressoras_ItemCheck(object sender, RadListBoxItemEventArgs e)
        {
            if (e.Item.Value == "Todas")
            {
                foreach (RadListBoxItem item in rlbImpressoras.Items)
                {
                    item.Checked = e.Item.Checked;
                }
            }
        }

        private void ControleVisibilidade(bool uf, bool cidade, bool unidade, bool celula, bool ambiente, bool impressoras, bool usuarios, bool formatoFolha, bool cor)
        {
            lbUf.Visible = uf;
            lbCidade.Visible = cidade;
            lbUnidade.Visible = unidade;
            lbCelula.Visible = celula;
            lbAmbiente.Visible = ambiente;

            if ((bool)Session["vip"] || (bool)Session["admin"])
            {
                rcbUf.Visible = uf;
                lbUfValor.Visible = false;

                rcbCidade.Visible = cidade;
                lbCidadeValor.Visible = false;

                rcbUnidade.Visible = unidade;
                lbUnidadeValor.Visible = false;

                rcbCelula.Visible = celula;
                lbCelulaValor.Visible = false;

                rcbAmbiente.Visible = ambiente;
                lbAmbienteValor.Visible = false;
            }
            else
            {
                rcbUf.Visible = false;
                lbUfValor.Visible = uf;

                rcbCidade.Visible = false;
                lbCidadeValor.Visible = cidade;

                rcbUnidade.Visible = false;
                lbUnidadeValor.Visible = unidade;

                rcbCelula.Visible = false;
                lbCelulaValor.Visible = celula;

                rcbAmbiente.Visible = false;
                lbAmbienteValor.Visible = ambiente;
            }

            lbImpressoras.Visible = impressoras;
            rlbImpressoras.Visible = impressoras;

            lbUsuarios.Visible = usuarios;
            rtbUsuario.Visible = usuarios;

            lbFormatoFolha.Visible = formatoFolha;
            rcbFormatoFolha.Visible = formatoFolha;

            lbCor.Visible = cor;
            rcbCor.Visible = cor;

            rcbAmbiente.Visible = false;
            lbAmbiente.Visible = false;
            lbAmbienteValor.Visible = false;
        }
    }
}