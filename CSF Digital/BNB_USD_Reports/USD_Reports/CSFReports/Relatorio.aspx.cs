using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using CSFDigital.Controls;

public partial class Relatorio : System.Web.UI.Page
{
    private static List<Chamado> listaRodape;
    private static int atualizacoesRodape = 0;

    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
            //Retorna Parametros
            Parametro.RetornarParametros(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Parametros.xml");
            //Retorna SLA - Toner
            SLA.RetornarSLA(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Toner.xml", true);
            //Retorna SLA - Assistência Técnica
            SLA.RetornarSLA(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Assistencia.xml", false);

            //Retorna Feriados
            Feriado.RetornaListaFeriados(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Feriados.xml");

            #region Define parametros
            for (int i = 0; i < Enum.GetNames(typeof(Relatorios)).Length; i++)
            {
                Relatorios relatorio = ((Relatorios)i);
                string nomeRelatorio = String.Empty;
                switch (relatorio)
                {
                    case Relatorios.RelatorioToner:
                        nomeRelatorio = "Relatório de Chamados de Solicitação de Toner";
                        break;
                    case Relatorios.RelatorioAssitenciaTecnica:
                        nomeRelatorio = "Relatório de Chamados de Atendimento Técnico";
                        break;
                    case Relatorios.RelatorioConsolidado:
                        nomeRelatorio = "Relatório Consolidado";
                        break;
                    case Relatorios.RelatorioAnalitico:
                        nomeRelatorio = "Relatório Análitico de Chamados";
                        break;
                    case Relatorios.RelatorioAssitenciaTecnicaMulta:
                        nomeRelatorio = "Relatório de Chamados de Atendimento Técnico - Multa";
                        break;

                    default:
                        nomeRelatorio = relatorio.ToString();
                        break;
                }
                RadioButtonListRelatorio.Items.Add(new ListItem(nomeRelatorio, i.ToString()));
            }
            RadioButtonListRelatorio.SelectedIndex = 0;

            for (int i = 0; i < Enum.GetNames(typeof(TipoChaves)).Length; i++)
            {
                TipoChaves chave = ((TipoChaves)i);
                string nomeChave = String.Empty;
                switch (chave)
                {
                    case TipoChaves.NumChamado:
                        nomeChave = "Chamado";
                        break;
                    case TipoChaves.NumSerie:
                        nomeChave = "Série";
                        break;
                    case TipoChaves.AbertosOuFechados:
                        nomeChave = "Abertos/Fechados no período";
                        break;
                    case TipoChaves.Ativas:
                        nomeChave = "Ativos";
                        break;
                    case TipoChaves.DtAbertura:
                        nomeChave = "Abertos no período";
                        break;
                    case TipoChaves.DtFechamento:
                        nomeChave = "Fechados nos período";
                        break;

                    default:
                        nomeChave = chave.ToString();
                        break;
                }
                RadioButtonListTipo.Items.Add(new ListItem(nomeChave, i.ToString()));
            }
            RadioButtonListTipo.SelectedIndex = 0;

            for (int i = 0; i < Enum.GetNames(typeof(VisoesConsolidado)).Length; i++)
            {
                VisoesConsolidado visao = ((VisoesConsolidado)i);
                string nomeVisao = String.Empty;
                switch (visao)
                {
                    case VisoesConsolidado.VisaoBNB:
                        nomeVisao = "BNB";
                        break;
                    case VisoesConsolidado.VisaoGeral:
                        nomeVisao = "Geral";
                        break;

                    default:
                        nomeVisao = visao.ToString();
                        break;
                }
                DropDownListRelatorioAssistencia.Items.Add(new ListItem(nomeVisao, i.ToString()));
            }
            DropDownListRelatorioAssistencia.SelectedIndex = 0;

            for (int i = 0; i < Enum.GetNames(typeof(VisoesAnalitico)).Length; i++)
            {
                VisoesAnalitico visao = ((VisoesAnalitico)i);
                string nomeVisao = String.Empty;
                switch (visao)
                {
                    case VisoesAnalitico.VisaoBNB:
                        nomeVisao = "BNB";
                        break;
                    case VisoesAnalitico.VisaoGeral:
                        nomeVisao = "Geral";
                        break;

                    default:
                        nomeVisao = visao.ToString();
                        break;
                }
                DropDownListRelatorioAnalitico.Items.Add(new ListItem(nomeVisao, i.ToString()));
            }
            DropDownListRelatorioAnalitico.SelectedIndex = 0;

            for (int i = 0; i < Enum.GetNames(typeof(TipoEmissao)).Length; i++)
            {
                TipoEmissao emissao = ((TipoEmissao)i);
                string nomeEmissao = String.Empty;
                switch (emissao)
                {
                    case TipoEmissao.Planilha:
                        nomeEmissao = "Planilha";
                        break;
                    case TipoEmissao.Relatorio:
                        nomeEmissao = "Relatório";
                        break;

                    default:
                        nomeEmissao = emissao.ToString();
                        break;
                }
                RadioButtonListTipoEmissao.Items.Add(new ListItem(nomeEmissao, i.ToString()));
            }
            RadioButtonListTipoEmissao.SelectedIndex = 0;
            #endregion

            #region Carrega datas
            DateTime dtMesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20);
            DateTime dtMesAnterior = dtMesAtual.AddMonths(-1);
            dtMesAnterior = new DateTime(dtMesAnterior.Year, dtMesAnterior.Month, 21);

            rdpDtInicial.SelectedDate = dtMesAnterior;
            rdpDtFinal.SelectedDate = dtMesAtual;
            rdpDtInicialMulta.SelectedDate = dtMesAnterior;
            rdpDtFinalMulta.SelectedDate = dtMesAtual;

            rdpDtInicial.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            rdpDtFinal.Style.Add(HtmlTextWriterStyle.TextAlign, "center");

            rdpDtInicialMulta.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            rdpDtFinalMulta.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            #endregion

            #region Atualiza chamados rodapé
            listaRodape = new List<Chamado>();
            List<Chamado> listaChamados = Chamado.MontaChamados(null, null, null, TipoChaves.Ativas, null, null);

            listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
            { return chamado2.TempoSLARestante.TotalSeconds.CompareTo(chamado1.TempoSLARestante.TotalSeconds); });

            foreach (Chamado chamado in listaChamados)
            {
                if ((new TimeSpan(0, 0, (((int)chamado.SlaDefinida.Limite.TotalSeconds * 20) / 100)) > chamado.TempoSLARestante) && chamado.TempoSLARestante != TimeSpan.Zero)
                {
                    if (listaRodape.Count <= 4)
                        listaRodape.Add(chamado);
                    else
                        break;
                }
            }
            if (listaRodape.Count <= 4)
            {
                foreach (Chamado chamado in listaChamados)
                {
                    if (chamado.TempoSLARestante != TimeSpan.Zero && new TimeSpan(0, 0, (((int)chamado.SlaDefinida.Limite.TotalSeconds * 20) / 100)) < chamado.TempoSLARestante)
                    {
                        if (listaRodape.Count <= 4)
                            listaRodape.Add(chamado);
                        else
                            break;
                    }
                }
            }

            PassaChamadosRodape();
            #endregion

            Label8.Visible = true;
            rdpDtFinal.Visible = true;
            Label9.Visible = true;
            rdpDtInicial.Visible = true;
            Label4.Visible = false;
            txbChamado.Visible = false;
            Label1.Visible = true;
            rdpDtFinalMulta.Visible = true;
            Label2.Visible = true;
            rdpDtInicialMulta.Visible = true;
        }
    }

    protected void btnEmitir_Click(object sender, EventArgs e)
    {
        if (rdpDtInicial.SelectedDate.HasValue && rdpDtFinal.SelectedDate.HasValue)
        {
            Button btn = (Button)sender;

            string chave = txbChamado.Text.Trim();
            DateTime dtInicial = new DateTime();
            DateTime dtFinal = new DateTime();

            DateTime dtInicialMulta = new DateTime();
            DateTime dtFinalMulta = new DateTime();

            dtInicial = rdpDtInicial.SelectedDate.Value;
            dtFinal = rdpDtFinal.SelectedDate.Value;

            dtInicialMulta = rdpDtInicial.SelectedDate.Value;
            dtFinalMulta = rdpDtFinal.SelectedDate.Value;

            string config = "location=no,width=1110,height=850,top=10,left=10,resizable=yes";
            string script = String.Format("window.open('ShowReports.aspx?relatorio={0}&dtInicial={1}&dtFinal={2}&chave={3}&filtro={4}&visao={5}&emissao={6}&dtInicialMulta={7}&dtFinalMulta={8}','','{9}');",
                RadioButtonListRelatorio.SelectedValue, dtInicial.ToBinary(), dtFinal.ToBinary(), chave, RadioButtonListTipo.SelectedValue, DropDownListRelatorioAnalitico.SelectedValue, RadioButtonListTipoEmissao.SelectedValue, dtInicialMulta.ToBinary(), dtFinalMulta.ToBinary(), config);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "report", script, true);
        }
    }

    protected void TimerInfoRodape_Tick(object sender, EventArgs e)
    {
        TimerInfoRodape.Enabled = false;

        PassaChamadosRodape();

        TimerInfoRodape.Enabled = true;
    }

    #region Passa chamados no rodapé
    private void PassaChamadosRodape()
    {
        if (listaRodape == null)
            listaRodape = new List<Chamado>();

        listaRodape.Sort(delegate(Chamado chamado2, Chamado chamado1)
        { return chamado2.TempoSLARestante.TotalSeconds.CompareTo(chamado1.TempoSLARestante.TotalSeconds); });

        Chamado chamadoRodape = null;
        foreach (Chamado chamado in listaRodape)
        {
            if (listaRodape.IndexOf(chamado) == atualizacoesRodape)
            {
                chamadoRodape = chamado;

                if (atualizacoesRodape > 4)
                    atualizacoesRodape = 0;
                else
                    atualizacoesRodape++;

                break;
            }
        }

        if (chamadoRodape != null)
        {
            if (chamadoRodape.TempoSLARestante == TimeSpan.Zero)
                lbInfoRodape.Style.Add(HtmlTextWriterStyle.Color, "red");
            else if (new TimeSpan(0, 0, (((int)chamadoRodape.SlaDefinida.Limite.TotalSeconds * 20) / 100)) > chamadoRodape.TempoSLARestante)
                lbInfoRodape.Style.Add(HtmlTextWriterStyle.Color, "orange");
            else
                lbInfoRodape.Style.Add(HtmlTextWriterStyle.Color, "green");

            lbInfoRodape.Text = "Chamado: " + chamadoRodape.Referencia + "; Responsável : " + chamadoRodape.Responsavel +
                "; Tempo restante : " + chamadoRodape.TempoSLARestanteFormatado;
        }
        else
        {
            if (atualizacoesRodape <= 5)
                atualizacoesRodape = 0;
        }

        if (listaRodape.Count == 0)
            lbInfoRodape.Text = "Não há chamados a serem listados.";
    }
    #endregion

    protected void TimerAtualizaPagina_Tick(object sender, EventArgs e)
    {
        Response.Redirect("~/Relatorio.aspx");
    }

    protected void RadioButtonListTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoChaves chave = (TipoChaves)int.Parse(RadioButtonListTipo.SelectedValue);

        if (chave == TipoChaves.AbertosOuFechados || chave == TipoChaves.DtAbertura || chave == TipoChaves.DtFechamento)
        {
            Label8.Visible = true;
            rdpDtFinal.Visible = true;
            Label9.Visible = true;
            rdpDtInicial.Visible = true;
            Label4.Visible = false;
            txbChamado.Visible = false;

            Label1.Visible = true;
            rdpDtFinalMulta.Visible = true;
            Label2.Visible = true;
            rdpDtInicialMulta.Visible = true;
        }
        else if (chave == TipoChaves.Ativas)
        {
            Label8.Visible = false;
            rdpDtFinal.Visible = false;
            Label9.Visible = false;
            rdpDtInicial.Visible = false;
            Label4.Visible = false;
            txbChamado.Visible = false;

            Label1.Visible = true;
            rdpDtFinalMulta.Visible = true;
            Label2.Visible = true;
            rdpDtInicialMulta.Visible = true;
        }
        else if (chave == TipoChaves.NumChamado || chave == TipoChaves.NumSerie)
        {
            Label8.Visible = false;
            rdpDtFinal.Visible = false;
            Label9.Visible = false;
            rdpDtInicial.Visible = false;
            Label4.Visible = true;
            txbChamado.Visible = true;

            Label1.Visible = true;
            rdpDtFinalMulta.Visible = true;
            Label2.Visible = true;
            rdpDtInicialMulta.Visible = true;
        }
    }
}