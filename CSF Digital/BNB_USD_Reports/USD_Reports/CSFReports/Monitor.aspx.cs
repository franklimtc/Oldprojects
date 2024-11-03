using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using CSFDigital.Controls;
using System.Data;
using System.Data.OleDb;

public partial class Monitor : System.Web.UI.Page
{
    private static List<Chamado> listaRodape;
    private static int atualizacoesRodape = 0;

    private void MontaMonitor(List<Chamado> listaChamados, string NChamado, string NSerie, string NDatabaertura, string NStatus, string NCidade, string NUF, string NCategoria, string NResponsavel)
    {
        List<string> lstChamado = new List<string>();
        List<string> lstSerie = new List<string>();
        List<string> lstDataAbertura = new List<string>();
        List<string> lstStatus = new List<string>();
        List<string> lstCidade = new List<string>();
        List<string> lstUf = new List<string>();
        List<string> lstCategoria = new List<string>();
        List<string> lstResponsavel = new List<string>();

        #region Monta Monitor
        //foreach (TableRow cabecalho in tblMonitor.Rows)
        //{
        //    foreach (TableCell celula in cabecalho.Cells)
        //    {
        //        if (celula.ID != "NN")
        //        {
        //            CheckBoxOrdenaCrescente.Items.Add(new ListItem(celula.ID, celula.ID));
        //            CheckBoxOrdenaDecrescente.Items.Add(new ListItem(celula.ID, celula.ID));
        //        }
        //    }
        //    break;
        //}

        foreach (Chamado chamado in listaChamados)
        {
            if (!lstChamado.Contains(chamado.Referencia))
            {
                lstChamado.Add(chamado.Referencia);
            }

            if (!lstSerie.Contains(chamado.MaquinaRelacionada.Serie))
            {
                lstSerie.Add(chamado.MaquinaRelacionada.Serie);
            }

            if (!lstDataAbertura.Contains(chamado.Data_Abertura.Value.ToString("dd/MM/yyyy")))
            {
                lstDataAbertura.Add(chamado.Data_Abertura.Value.ToString("dd/MM/yyyy"));
            }

            if (!lstStatus.Contains(chamado.StatusChamado.Descricao))
            {
                lstStatus.Add(chamado.StatusChamado.Descricao);
            }

            if (!lstCidade.Contains(chamado.MaquinaRelacionada.Local.Cidade))
            {
                lstCidade.Add(chamado.MaquinaRelacionada.Local.Cidade);
            }

            if (!lstUf.Contains(chamado.MaquinaRelacionada.Local.Estado))
            {
                lstUf.Add(chamado.MaquinaRelacionada.Local.Estado);
            }

            if (chamado.Categoria != null)
            {
                if (chamado.Categoria.Contains("TONNER"))
                {
                    if (!lstCategoria.Contains("Solicitação de Toner"))
                        lstCategoria.Add("Solicitação de Toner");
                }
                else if (chamado.Categoria.Contains("TONER"))
                {
                    if (!lstCategoria.Contains("Recolhimento de Toner"))
                        lstCategoria.Add("Recolhimento de Toner");
                }
                else
                {
                    if (!lstCategoria.Contains("Assistência Técnica"))
                        lstCategoria.Add("Assistência Técnica");
                }
            }

            if (!lstResponsavel.Contains(chamado.Responsavel))
            {
                lstResponsavel.Add(chamado.Responsavel);
            }

            TableRow linha = new TableRow();

            TableCell celulaContador = new TableCell();

            if ((listaChamados.IndexOf(chamado) + 1) < 10)
                celulaContador.Text = "00" + (listaChamados.IndexOf(chamado) + 1).ToString();
            else if (listaChamados.IndexOf(chamado) < 100)
                celulaContador.Text = "0" + (listaChamados.IndexOf(chamado) + 1).ToString();
            else
                celulaContador.Text = (listaChamados.IndexOf(chamado) + 1).ToString();

            celulaContador.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celulaContador);

            TableCell celula1 = new TableCell();
            celula1.Text = chamado.Referencia;
            celula1.Style.Add(HtmlTextWriterStyle.TextAlign, "center");

            HyperLink link = new HyperLink();
            link.NavigateUrl = chamado.EnderecoWeb;
            link.Text = chamado.Referencia;
            link.Target = "_blank";
            celula1.Controls.Add(link);

            linha.Cells.Add(celula1);

            TableCell celula2 = new TableCell();
            celula2.Text = chamado.MaquinaRelacionada.Serie;
            celula2.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula2);

            TableCell celula12 = new TableCell();
            celula12.Text = chamado.Data_Abertura.Value.ToString("dd/MM/yyyy hh:mm");
            celula12.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula12);

            TableCell celula3 = new TableCell();
            celula3.Text = chamado.StatusChamado.Descricao;
            celula3.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula3);

            TableCell celula4 = new TableCell();

            /*
            if (chamado.Atendente.LocalContato != null)
                celula4.Text = chamado.Atendente.LocalContato.Cidade;
            else
                celula4.Text = "";
            */
            celula4.Text = chamado.MaquinaRelacionada.Local.Cidade;

            celula4.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula4);

            TableCell celula5 = new TableCell();

            /*
            if (chamado.Atendente.LocalContato != null)
                celula5.Text = chamado.Atendente.LocalContato.Estado;
            else
                celula5.Text = "";
            */
            celula5.Text = chamado.MaquinaRelacionada.Local.Estado;

            celula5.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula5);

            TableCell celula10 = new TableCell();

            if (chamado.Categoria != null)
            {
                if (chamado.Categoria.Contains("TONNER"))
                    celula10.Text = "Solicitação de Toner";
                else if (chamado.Categoria.Contains("TONER"))
                    celula10.Text = "Recolhimento de Toner";
                else
                    celula10.Text = "Assistência Técnica";
            }
            else
                celula10.Text = "";

            celula10.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula10);

            TableCell celula11 = new TableCell();

            if (chamado.Responsavel != null)
                celula11.Text = chamado.Responsavel;
            else
                celula11.Text = "";

            celula11.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula11);

            TableCell celula6 = new TableCell();
            celula6.Text = chamado.TempoSLAFormatado;
            celula6.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula6);

            TableCell celula7 = new TableCell();
            celula7.Text = chamado.TempoSLARestanteFormatado;
            celula7.Style.Add(HtmlTextWriterStyle.TextAlign, "center");

            if (chamado.SlaDefinida.DeAcordo == "**SLO")
            {
                celula7.Text = "**SLO";
                celula7.Style.Add(HtmlTextWriterStyle.Color, "green");
            }
            else
            {
                if (chamado.TempoSLARestante == TimeSpan.Zero)
                    celula7.Style.Add(HtmlTextWriterStyle.Color, "red");
                else if (new TimeSpan(0, 0, (((int)chamado.SlaDefinida.Limite.TotalSeconds * 20) / 100)) > chamado.TempoSLARestante)
                    celula7.Style.Add(HtmlTextWriterStyle.Color, "orange");
                else
                    celula7.Style.Add(HtmlTextWriterStyle.Color, "green");

                if (chamado.Categoria != null)
                {
                    if (chamado.Categoria.Contains("TONER"))
                        celula7.Text = "---";
                }
            }

            linha.Cells.Add(celula7);

            TableCell celula8 = new TableCell();
            celula8.Text = Util.FormatarTimeSpan(chamado.SlaDefinida.Limite);
            celula8.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula8);

            TableCell celula9 = new TableCell();
            celula9.Text = chamado.TempoTotalFormatado;
            celula9.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            linha.Cells.Add(celula9);

            if (NChamado != "" && NChamado != "Todos")
            {
                if (NChamado == chamado.Referencia)
                    tblMonitor.Rows.Add(linha);
            }
            else if (NSerie != "" && NSerie != "Todos")
            {
                if (NSerie == celula2.Text)
                    tblMonitor.Rows.Add(linha);
            }
            else if (NDatabaertura != "" && NDatabaertura != "Todos")
            {
                if (NDatabaertura == chamado.Data_Abertura.Value.ToString("dd_MM_yyyy"))
                    tblMonitor.Rows.Add(linha);
            }
            else
            {
                tblMonitor.Rows.Add(linha);
            }
        }

        //lstChamado.Add("Todos");
        //lstSerie.Add("Todas");
        //lstDataAbertura.Add("Todas");
        //lstStatus.Add("Todos");
        //lstCidade.Add("Todas");
        //lstUf.Add("Todas");
        //lstCategoria.Add("Todas");
        //lstResponsavel.Add("Todos");

        lstChamado.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstChamado)
        {
            ddlChamado.Items.Add(new ListItem(item, item));
        }

        lstSerie.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstSerie)
        {
            ddlSerie.Items.Add(new ListItem(item, item));
        }

        lstDataAbertura.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstDataAbertura)
        {
            ddlDtAbertura.Items.Add(new ListItem(item, item));
        }

        lstStatus.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstStatus)
        {
            ddlStatus.Items.Add(new ListItem(item, item));
        }

        lstCidade.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstCidade)
        {
            ddlCidade.Items.Add(new ListItem(item, item));
        }

        lstUf.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstUf)
        {
            ddlUf.Items.Add(new ListItem(item, item));
        }

        lstCategoria.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstCategoria)
        {
            ddlCategoria.Items.Add(new ListItem(item, item));
        }

        lstResponsavel.Sort(delegate(string s1, string s2)
        { return s1.CompareTo(s2); });

        foreach (string item in lstResponsavel)
        {
            ddlResponsavel.Items.Add(new ListItem(item, item));
        }

        #endregion
    }

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

            #region Carrega Monitor
            string FormaOrdenacao = "";
            string CampoOrdenacao = "";

            #region Filtros
            string NChamado = "";
            string NSerie = "";
            string NDataAbertura = "";
            string NStatus = "";
            string NCidade = "";
            string NUF = "";
            string NCategoria = "";
            string NResponsavel = "";

            if (Request.QueryString.ToString().Contains("chamado"))
                NChamado = Request.QueryString["chamado"];

            if (Request.QueryString.ToString().Contains("serie"))
                NSerie = Request.QueryString["serie"];

            if (Request.QueryString.ToString().Contains("dtAbertura"))
                NDataAbertura = Request.QueryString["dtAbertura"];

            if (Request.QueryString.ToString().Contains("status"))
                NStatus = Request.QueryString["status"];

            if (Request.QueryString.ToString().Contains("cidade"))
                NCidade = Request.QueryString["cidade"];

            if (Request.QueryString.ToString().Contains("uf"))
                NUF = Request.QueryString["uf"];

            if (Request.QueryString.ToString().Contains("categoria"))
                NCategoria = Request.QueryString["categoria"];

            if (Request.QueryString.ToString().Contains("responsavel"))
                NResponsavel = Request.QueryString["responsavel"];
            #endregion

            List<Chamado> listaChamados = Chamado.MontaChamados(null, null, null, TipoChaves.Ativas, null, null);

            if (!Request.QueryString.ToString().Contains("ordenacao"))
            {
                listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                { return chamado2.TempoSLARestante.TotalSeconds.CompareTo(chamado1.TempoSLARestante.TotalSeconds); });

                MontaMonitor(listaChamados, NChamado, NSerie, NDataAbertura, NStatus, NCidade, NUF, NCategoria, NResponsavel);
            }
            else
            {
                FormaOrdenacao = Request.QueryString["tipo"];
                CampoOrdenacao = Request.QueryString["ordenacao"];

                #region Crescente
                if (FormaOrdenacao == "asc")
                {
                    if (CampoOrdenacao == "Chamado")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.Referencia.CompareTo(chamado1.Referencia); });
                    }
                    else if (CampoOrdenacao == "Serie")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.MaquinaRelacionada.Serie.CompareTo(chamado1.MaquinaRelacionada.Serie); });
                    }
                    else if (CampoOrdenacao == "Status")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.StatusChamado.Descricao.CompareTo(chamado1.StatusChamado.Descricao); });
                    }
                    else if (CampoOrdenacao == "Cidade")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.MaquinaRelacionada.Local.Cidade.CompareTo(chamado1.MaquinaRelacionada.Local.Cidade); });

                        //listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        //{ return chamado2.Atendente.LocalContato.Cidade.CompareTo(chamado1.Atendente.LocalContato.Cidade); });
                    }
                    else if (CampoOrdenacao == "Uf")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.MaquinaRelacionada.Local.Estado.CompareTo(chamado1.MaquinaRelacionada.Local.Estado); });

                        //listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        //{ return chamado2.Atendente.LocalContato.Estado.CompareTo(chamado1.Atendente.LocalContato.Estado); });
                    }
                    else if (CampoOrdenacao == "Categoria")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.Categoria.CompareTo(chamado1.Categoria); });
                    }
                    else if (CampoOrdenacao == "Responsavel")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.Responsavel.CompareTo(chamado1.Responsavel); });
                    }
                    else if (CampoOrdenacao == "TempoSLA")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "TempoSLARestante")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.TempoSLARestante.TotalSeconds.CompareTo(chamado1.TempoSLARestante.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "SLADefinida")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.SlaDefinida.Limite.TotalSeconds.CompareTo(chamado1.SlaDefinida.Limite.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "TempoTotal")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.TempoTotal.TotalSeconds.CompareTo(chamado1.TempoTotal.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "DataAbertura")
                    {
                        listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                        { return chamado2.Data_Abertura.Value.CompareTo(chamado1.Data_Abertura.Value); });
                    }
                }
                #endregion

                #region Decrescente
                if (FormaOrdenacao == "desc")
                {
                    if (CampoOrdenacao == "Chamado")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.Referencia.CompareTo(chamado1.Referencia); });
                    }
                    else if (CampoOrdenacao == "Serie")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.MaquinaRelacionada.Serie.CompareTo(chamado1.MaquinaRelacionada.Serie); });
                    }
                    else if (CampoOrdenacao == "Status")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.StatusChamado.Descricao.CompareTo(chamado1.StatusChamado.Descricao); });
                    }

                    else if (CampoOrdenacao == "Cidade")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.MaquinaRelacionada.Local.Cidade.CompareTo(chamado1.MaquinaRelacionada.Local.Cidade); });

                        //listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        //{ return chamado2.Atendente.LocalContato.Cidade.CompareTo(chamado1.Atendente.LocalContato.Cidade); });
                    }
                    else if (CampoOrdenacao == "Uf")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.MaquinaRelacionada.Local.Estado.CompareTo(chamado1.MaquinaRelacionada.Local.Estado); });

                        //listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        //{ return chamado2.Atendente.LocalContato.Estado.CompareTo(chamado1.Atendente.LocalContato.Estado); });
                    }
                    else if (CampoOrdenacao == "Categoria")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.Categoria.CompareTo(chamado1.Categoria); });
                    }
                    else if (CampoOrdenacao == "Responsavel")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.Responsavel.CompareTo(chamado1.Responsavel); });
                    }
                    else if (CampoOrdenacao == "TempoSLA")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "TempoSLARestante")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.TempoSLARestante.TotalSeconds.CompareTo(chamado1.TempoSLARestante.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "SLADefinida")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.SlaDefinida.Limite.TotalSeconds.CompareTo(chamado1.SlaDefinida.Limite.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "TempoTotal")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.TempoTotal.TotalSeconds.CompareTo(chamado1.TempoTotal.TotalSeconds); });
                    }
                    else if (CampoOrdenacao == "DataAbertura")
                    {
                        listaChamados.Sort(delegate(Chamado chamado1, Chamado chamado2)
                        { return chamado2.Data_Abertura.Value.CompareTo(chamado1.Data_Abertura.Value); });
                    }
                }
                #endregion

                MontaMonitor(listaChamados, NChamado, NSerie, NDataAbertura, NStatus, NCidade, NUF, NCategoria, NResponsavel);
            }
            #endregion

            #region Atualiza chamados rodapé
            listaRodape = new List<Chamado>();

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
        }
    }

    protected void CheckBoxOrdenaCrescente_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Ordenacao = "";

        //foreach (ListItem item in CheckBoxOrdenaCrescente.Items)
        //{
        //    if (item.Selected == true)
        //        Ordenacao = item.Value;
        //}

        //string aux = "";
        //for (int i = 0; i < Ordenacao.Length; i++)
        //{
        //    if (Ordenacao[i] != ' ')
        //        aux += Ordenacao[i].ToString();
        //}
        //Ordenacao = aux;

        //string config = "location=no,resizable=yes,scrollbars=yes";
        //string script = String.Format(" window.location.replace('Monitor.aspx?tipo={0}&ordenacao={1}','','{2}');",
        //"asc", Ordenacao, config);

        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", script, true);
    }

    protected void CheckBoxOrdenaDecrescente_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Ordenacao = "";

        //foreach (ListItem item in CheckBoxOrdenaDecrescente.Items)
        //{
        //    if (item.Selected == true)
        //        Ordenacao = item.Value;
        //}

        //string aux = "";
        //for (int i = 0; i < Ordenacao.Length; i++)
        //{
        //    if (Ordenacao[i] != ' ')
        //        aux += Ordenacao[i].ToString();
        //}
        //Ordenacao = aux;

        //string config = "location=no,resizable=yes,scrollbars=yes";
        //string script = String.Format(" window.location.replace('Monitor.aspx?tipo={0}&ordenacao={1}','','{2}');",
        //"desc", Ordenacao, config);

        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", script, true);
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
        Response.Redirect("~/Monitor.aspx");
    }
    protected void imgOrd_Click(object sender, ImageClickEventArgs e)
    {
        string OrdenacaoItem = "";
        string OrdenacaoTipo = "";

        ImageButton btn = (ImageButton)sender;

        OrdenacaoTipo = btn.ID.Substring(3);
        OrdenacaoTipo = OrdenacaoTipo.Substring(OrdenacaoTipo.Length - 3).ToLower();

        OrdenacaoItem = btn.ID.Substring(3);


        if (OrdenacaoTipo != "asc")
        {
            OrdenacaoTipo = "desc";

            OrdenacaoItem = OrdenacaoItem.Substring(0, OrdenacaoItem.LastIndexOf("Dec"));
        }
        else
        {
            OrdenacaoItem = OrdenacaoItem.Substring(0, OrdenacaoItem.LastIndexOf("Asc"));
        }

        string config = "location=no,resizable=yes,scrollbars=yes";
        string script = String.Format(" window.location.replace('Monitor.aspx?tipo={0}&ordenacao={1}','','{2}');",
        OrdenacaoTipo, OrdenacaoItem, config);

        ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", script, true);
    }
    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!Request.QueryString.ToString().Contains("ordenacao"))
        {
            string NChamado = ddlChamado.Items[ddlChamado.SelectedIndex].Value;
            string NSerie = ddlSerie.Items[ddlSerie.SelectedIndex].Value;
            string NDataAbertura = ddlDtAbertura.Items[ddlDtAbertura.SelectedIndex].Value;
            string NStatus = ddlStatus.Items[ddlStatus.SelectedIndex].Value;
            string NCidade = ddlCidade.Items[ddlCidade.SelectedIndex].Value;
            string NUF = ddlUf.Items[ddlUf.SelectedIndex].Value;
            string NCategoria = ddlCategoria.Items[ddlCategoria.SelectedIndex].Value;
            string NResponsavel = ddlResponsavel.Items[ddlResponsavel.SelectedIndex].Value;

            NDataAbertura = NDataAbertura.Replace('/', '_');
            
            string config = "location=no,resizable=yes,scrollbars=yes";
            string script = String.Format(" window.location.replace('Monitor.aspx?chamado={0}&serie={1}&dtAbertura={2}&status={3}&cidade={4}&uf={5}&categoria={6}&responsavel={7}','','{8}');",
            NChamado, NSerie, NDataAbertura, NStatus, NCidade, NUF, NCategoria, NResponsavel, config);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", script, true);
        }
        else
        {
            string OrdenacaoTipo = Request.QueryString["tipo"];
            string OrdenacaoItem = Request.QueryString["ordenacao"];

            string NChamado = ddlChamado.Items[ddlChamado.SelectedIndex].Value;
            string NSerie = ddlSerie.Items[ddlSerie.SelectedIndex].Value;
            string NDataAbertura = ddlDtAbertura.Items[ddlDtAbertura.SelectedIndex].Value;
            string NStatus = ddlStatus.Items[ddlStatus.SelectedIndex].Value;
            string NCidade = ddlCidade.Items[ddlCidade.SelectedIndex].Value;
            string NUF = ddlUf.Items[ddlUf.SelectedIndex].Value;
            string NCategoria = ddlCategoria.Items[ddlCategoria.SelectedIndex].Value;
            string NResponsavel = ddlResponsavel.Items[ddlResponsavel.SelectedIndex].Value;

            NDataAbertura = NDataAbertura.Replace('/', '_');

            string config = "location=no,resizable=yes,scrollbars=yes";
            string script = String.Format(" window.location.replace('Monitor.aspx?tipo={0}&ordenacao={1}&chamado={2}&serie={3}&dtAbertura={4}&status={5}&cidade={6}&uf={7}&categoria={8}&responsavel={9}','','{10}');",
            OrdenacaoTipo, OrdenacaoItem, NChamado, NSerie, NDataAbertura, NStatus, NCidade, NUF, NCategoria, NResponsavel, config);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", script, true);
        }

    }
}