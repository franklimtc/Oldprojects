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
using System.Web.UI.DataVisualization.Charting;

public partial class Suporte : System.Web.UI.Page
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
            for (int i = 0; i < Enum.GetNames(typeof(TipoChaves)).Length; i++)
            {
                TipoChaves chave = ((TipoChaves)i);
                string nomeChave = String.Empty;
                switch (chave)
                {
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
                        nomeChave = "";
                        break;
                }
                if (nomeChave != "")
                    RadioButtonListTipo.Items.Add(new ListItem(nomeChave, i.ToString()));
            }
            #endregion

            #region Carrega datas
            DateTime dtMesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20);
            DateTime dtMesAnterior = dtMesAtual.AddMonths(-1);
            dtMesAnterior = new DateTime(dtMesAnterior.Year, dtMesAnterior.Month, 21);

            rdpDtInicial.SelectedDate = dtMesAnterior;
            rdpDtFinal.SelectedDate = dtMesAtual;

            rdpDtInicial.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            rdpDtFinal.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
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

            if (!Request.QueryString.ToString().Contains("filtro"))
            {
                Grafico1(listaChamados);
                Grafico2(listaChamados);
                Grafico3(listaChamados);
                Grafico4(listaChamados);

                RadioButtonListTipo.SelectedIndex = RadioButtonListTipo.Items.Count - 1;

                RadioButtonListTipo_SelectedIndexChanged(sender, e);
            }
            else
            {
                DateTime DataInicial = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtInicial"]));
                DateTime DataFinal = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtFinal"]));
                TipoChaves Filtro = (TipoChaves)int.Parse(Request.QueryString["filtro"]);

                rdpDtInicial.SelectedDate = DataInicial;
                rdpDtFinal.SelectedDate = DataFinal;
                RadioButtonListTipo.SelectedIndex = int.Parse(Request.QueryString["filtro"]);

                List<Chamado> listaGrafico = Chamado.MontaChamados(DataInicial, DataFinal, null, Filtro, null, null);
                Grafico1(listaGrafico);
                Grafico2(listaGrafico);
                Grafico3(listaGrafico);
                Grafico4(listaGrafico);
            }
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
        Response.Redirect("~/Suporte.aspx");
    }

    private void Grafico1(List<Chamado> listaChamado)
    {
        Dictionary<string, int> InfoGrafico = new Dictionary<string, int>();

        foreach (Chamado chamado in listaChamado)
        {
            if (chamado.SlaDefinida != null)
            {
                if (!InfoGrafico.ContainsKey(chamado.SlaDefinida.DeAcordo))
                    InfoGrafico.Add(chamado.SlaDefinida.DeAcordo, 1);
                else
                    InfoGrafico[chamado.SlaDefinida.DeAcordo] += 1;
            }
            else
            {
                if (!InfoGrafico.ContainsKey("N/A"))
                    InfoGrafico.Add("N/A", 1);
                else
                    InfoGrafico["N/A"] += 1;
            }
        }

        var table = new DataTable();

        table.Columns.Add("Sim", typeof(string));
        table.Columns.Add("Qtd.", typeof(int));
        table.Columns.Add("Lbl");

        foreach (var chave in InfoGrafico)
        {
            var row = table.NewRow();
            row["Sim"] = chave.Key;
            row["Qtd."] = chave.Value;
            table.Rows.Add(row);
        }

        Chart1.DataSource = table;
        Chart1.DataBind();
    }

    private void Grafico2(List<Chamado> listaChamado)
    {
        Dictionary<string, int> InfoGrafico = new Dictionary<string, int>();

        foreach (Chamado chamado in listaChamado)
        {
            if (chamado.Categoria != null)
            {
                if (chamado.Categoria.Contains("TONNER"))
                {
                    if (!InfoGrafico.ContainsKey("TONER"))
                        InfoGrafico.Add("TONER", 1);
                    else
                        InfoGrafico["TONER"] += 1;
                }
                else
                {
                    if (!InfoGrafico.ContainsKey("OUTROS"))
                        InfoGrafico.Add("OUTROS", 1);
                    else
                        InfoGrafico["OUTROS"] += 1;
                }
            }
            else
            {
                if (!InfoGrafico.ContainsKey("N/A"))
                    InfoGrafico.Add("N/A", 1);
                else
                    InfoGrafico["N/A"] += 1;
            }
        }

        var table = new DataTable();

        table.Columns.Add("Categoria", typeof(string));
        table.Columns.Add("Qtd.", typeof(int));
        table.Columns.Add("Lbl");

        foreach (var chave in InfoGrafico)
        {
            var row = table.NewRow();
            row["Categoria"] = chave.Key;
            row["Qtd."] = chave.Value;
            table.Rows.Add(row);
        }

        Chart2.DataSource = table;
        Chart2.DataBind();
    }

    private void Grafico3(List<Chamado> listaChamado)
    {
        Dictionary<string, int> InfoGrafico = new Dictionary<string, int>();

        foreach (Chamado chamado in listaChamado)
        {
            if (chamado.Atendente != null)
            {
                if (chamado.Atendente.LocalContato != null)
                {
                    if (!InfoGrafico.ContainsKey(chamado.Atendente.LocalContato.Estado))
                        InfoGrafico.Add(chamado.Atendente.LocalContato.Estado, 1);
                    else
                        InfoGrafico[chamado.Atendente.LocalContato.Estado] += 1;
                }
                else
                {
                    if (!InfoGrafico.ContainsKey("N/A"))
                        InfoGrafico.Add("N/A", 1);
                    else
                        InfoGrafico["N/A"] += 1;
                }
            }
        }

        var table = new DataTable();

        table.Columns.Add("UF", typeof(string));
        table.Columns.Add("Qtd.", typeof(int));
        table.Columns.Add("Lbl");

        foreach (var chave in InfoGrafico)
        {
            var row = table.NewRow();
            row["UF"] = chave.Key;
            row["Qtd."] = chave.Value;
            table.Rows.Add(row);
        }

        Chart3.DataSource = table;
        Chart3.DataBind();
    }

    private void Grafico4(List<Chamado> listaChamado)
    {
        Dictionary<string, int> InfoGrafico = new Dictionary<string, int>();

        foreach (Chamado chamado in listaChamado)
        {
            if (chamado.Responsavel != null)
            {
                if (!InfoGrafico.ContainsKey(chamado.Responsavel))
                    InfoGrafico.Add(chamado.Responsavel, 1);
                else
                    InfoGrafico[chamado.Responsavel] += 1;
            }
            else
            {
                if (!InfoGrafico.ContainsKey("N/A"))
                    InfoGrafico.Add("N/A", 1);
                else
                    InfoGrafico["N/A"] += 1;
            }
        }

        var table = new DataTable();

        table.Columns.Add("Técnico", typeof(string));
        table.Columns.Add("Qtd.", typeof(int));
        table.Columns.Add("Lbl");

        foreach (var chave in InfoGrafico)
        {
            var row = table.NewRow();
            row["Técnico"] = chave.Key;
            row["Qtd."] = chave.Value;
            table.Rows.Add(row);
        }

        Chart4.DataSource = table;
        Chart4.DataBind();
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
        }
        else if (chave == TipoChaves.Ativas)
        {
            Label8.Visible = false;
            rdpDtFinal.Visible = false;
            Label9.Visible = false;
            rdpDtInicial.Visible = false;
        }
    }

    protected void btnGerar_Click(object sender, EventArgs e)
    {
        if (rdpDtInicial.SelectedDate.HasValue && rdpDtFinal.SelectedDate.HasValue)
        {
            Button btn = (Button)sender;

            DateTime dtInicial = new DateTime();
            DateTime dtFinal = new DateTime();

            dtInicial = rdpDtInicial.SelectedDate.Value;
            dtFinal = rdpDtFinal.SelectedDate.Value;

            Response.Redirect(String.Format("Suporte.aspx?dtInicial={0}&dtFinal={1}&filtro={2}",
                dtInicial.ToBinary(), dtFinal.ToBinary(), RadioButtonListTipo.SelectedValue));
        }
    }
}