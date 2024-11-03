using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Xml.Linq;
using System.Collections;
using System.Web.Security;
using CSFDigital.Controls;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls.WebParts;

public partial class ShowReports : System.Web.UI.Page
{
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

            #region Configura relatório
            if (!Request.QueryString.ToString().Contains("relatorio"))
            {
                Response.Redirect("~/Relatorio.aspx");
            }
            else
            {
                TipoRelatorio = (Relatorios)int.Parse(Request.QueryString["relatorio"]);
                DataInicial = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtInicial"]));
                DataFinal = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtFinal"]));

                DataInicialMulta = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtInicialMulta"]));
                DataFinalMulta = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtFinalMulta"]));

                Chave = Request.QueryString["chave"];
                Filtro = (TipoChaves)int.Parse(Request.QueryString["filtro"]);
                Emissao = (TipoEmissao)int.Parse(Request.QueryString["emissao"]);

                if (TipoRelatorio == Relatorios.RelatorioAnalitico)
                    VisaoAnalitico = (VisoesAnalitico)int.Parse(Request.QueryString["visao"]);
                else
                    VisaoAssistencia = (VisoesConsolidado)int.Parse(Request.QueryString["visao"]);

                ConfiguraRelatorio(TipoRelatorio);
            }
            #endregion
        }
    }

    #region Atributos ShowReports
    private Relatorios TipoRelatorio
    {
        get { return (Relatorios)ViewState["relatorio"]; }
        set { ViewState["relatorio"] = (Relatorios)value; }
    }
    private VisoesConsolidado VisaoAssistencia
    {
        get { return (VisoesConsolidado)ViewState["visao"]; }
        set { ViewState["visao"] = (VisoesConsolidado)value; }
    }
    private VisoesAnalitico VisaoAnalitico
    {
        get { return (VisoesAnalitico)ViewState["visao"]; }
        set { ViewState["visao"] = (VisoesAnalitico)value; }
    }
    private DateTime DataInicial
    {
        get { return (DateTime)ViewState["dataInicial"]; }
        set { ViewState["dataInicial"] = (DateTime)value; }
    }
    private DateTime DataFinal
    {
        get { return (DateTime)ViewState["dataFinal"]; }
        set { ViewState["dataFinal"] = (DateTime)value; }
    }
    private DateTime DataInicialMulta
    {
        get { return (DateTime)ViewState["dataInicialMulta"]; }
        set { ViewState["dataInicialMulta"] = (DateTime)value; }
    }
    private DateTime DataFinalMulta
    {
        get { return (DateTime)ViewState["dataFinalMulta"]; }
        set { ViewState["dataFinalMulta"] = (DateTime)value; }
    }
    private string Chave
    {
        get { return (string)ViewState["chave"]; }
        set { ViewState["chave"] = (string)value; }
    }
    private TipoChaves Filtro
    {
        get { return (TipoChaves)ViewState["filtro"]; }
        set { ViewState["filtro"] = (TipoChaves)value; }
    }
    private TipoEmissao Emissao
    {
        get { return (TipoEmissao)ViewState["emissao"]; }
        set { ViewState["emissao"] = (TipoEmissao)value; }
    }
    #endregion

    private void ConfiguraRelatorio(Relatorios relatorio)
    {
        DefaultReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
        DefaultReportViewer.ZoomPercent = 100;
        //DefaultReportViewer.DataBind();

        Dictionary<string, object> parametrosReport = new Dictionary<string, object>();

        switch (relatorio)
        {
            #region Relatório de Chamados de Solicitação de Toner
            case Relatorios.RelatorioToner:
                {
                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

                    parametrosReport.Add("p_DataInicial", dataInicial);
                    parametrosReport.Add("p_DataFinal", dataFinal);
                    #endregion

                    List<Chamado> listaChamadosCompleto = Chamado.MontaChamados(dataInicial, dataFinal, Chave, Filtro, DataInicialMulta, DataFinalMulta);
                    List<Chamado> listaChamados = new List<Chamado>();
                    foreach (Chamado chamado in listaChamadosCompleto)
                    {
                        if (chamado.Categoria.Contains("TONNER"))
                            listaChamados.Add(chamado);
                    }

                    Contabilizacao.ProcessarMulta(listaChamados);

                    listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                    { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });

                    if (VisaoAssistencia == VisoesConsolidado.VisaoBNB)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "**SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "**SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório de Chamados de Solicitação de Toner";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTonerPlanilha.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioToner.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                    else if (VisaoAssistencia == VisoesConsolidado.VisaoGeral)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório de Chamados de Solicitação de Toner";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTonerPlanilha.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioToner.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                }
                break;
            #endregion

            #region Relatório de Chamados de Atendimento Técnico
            case Relatorios.RelatorioAssitenciaTecnica:
                {
                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

                    parametrosReport.Add("p_DataInicial", dataInicial);
                    parametrosReport.Add("p_DataFinal", dataFinal);
                    #endregion

                    List<Chamado> listaChamadosCompleto = Chamado.MontaChamados(dataInicial, dataFinal, Chave, Filtro, DataInicialMulta, DataFinalMulta);
                    List<Chamado> listaChamados = new List<Chamado>();
                    foreach (Chamado chamado in listaChamadosCompleto)
                    {
                        if (!chamado.Categoria.Contains("TONNER"))
                            listaChamados.Add(chamado);
                    }

                    listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                    { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });

                    if (VisaoAssistencia == VisoesConsolidado.VisaoBNB)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "**SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "**SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório de Chamados de Atendimento Técnico";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnicoPlanilha.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnico.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                    else if (VisaoAssistencia == VisoesConsolidado.VisaoGeral)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório de Chamados de Atendimento Técnico";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnicoPlanilha.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnico.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                }
                break;
            #endregion

            #region Relatório Consolidado
            case Relatorios.RelatorioConsolidado:
                {
                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

                    parametrosReport.Add("p_DataInicial", dataInicial);
                    parametrosReport.Add("p_DataFinal", dataFinal);
                    #endregion

                    List<Chamado> listaChamados = Chamado.MontaChamados(dataInicial, dataFinal, Chave, Filtro, DataInicialMulta, DataFinalMulta);

                    listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                    { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });

                    if (VisaoAssistencia == VisoesConsolidado.VisaoBNB)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "**SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "**SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório Chamados de Atendimento Técnico e Solicitação de Toner - Consolidado";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioPlanilha.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                    else if (VisaoAssistencia == VisoesConsolidado.VisaoGeral)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório Chamados de Atendimento Técnico e Solicitação de Toner - Consolidado";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioPlanilha.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                }
                break;
            #endregion

            #region Relatório Analítico
            case Relatorios.RelatorioAnalitico:
                {
                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

                    parametrosReport.Add("p_DataInicial", dataInicial);
                    parametrosReport.Add("p_DataFinal", dataFinal);
                    #endregion

                    List<Chamado> listaChamados = Chamado.MontaChamados(dataInicial, dataFinal, Chave, Filtro, DataInicialMulta, DataFinalMulta);
                    List<DetalhamentoChamado> listaDetalhe = DetalhamentoChamado.MontaDetalhamentoChamados(listaChamados);

                    parametrosReport.Add("p_TotalDemanda", listaChamados.Count.ToString());

                    DefaultReportViewer.LocalReport.DisplayName = "Relatório Analítico";
                    DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioAnalitico.rdlc";
                    DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DetalhamentoChamado", listaDetalhe));
                }
                break;
            #endregion

            #region Relatório de Chamados de Atendimento Técnico - Multa
            case Relatorios.RelatorioAssitenciaTecnicaMulta:
                {
                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

                    parametrosReport.Add("p_DataInicial", dataInicial);
                    parametrosReport.Add("p_DataFinal", dataFinal);
                    #endregion

                    List<Chamado> listaChamadosCompleto = Chamado.MontaChamados(dataInicial, dataFinal, Chave, Filtro, DataInicialMulta, DataFinalMulta);
                    List<Chamado> listaChamados = new List<Chamado>();

                    foreach (Chamado chamado in listaChamadosCompleto)
                    {
                        if (!chamado.Categoria.Contains("TONNER"))
                            listaChamados.Add(chamado);
                    }

                    Contabilizacao.ProcessarMulta(listaChamados);

                    listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                    { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });

                    if (VisaoAssistencia == VisoesConsolidado.VisaoBNB)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "**SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "**SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório de Chamados de Atendimento Técnico";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio_NOVO.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnicoMulta.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                    else if (VisaoAssistencia == VisoesConsolidado.VisaoGeral)
                    {
                        parametrosReport.Add("p_TotalDemanda", listaChamados.Count);

                        parametrosReport.Add("p_TotalDemanda_Requisicao", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Requisicao_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Requisição"), "SLO").Count);

                        parametrosReport.Add("p_TotalDemanda_Incidente", Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_SIM", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Sim").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLA_NAO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "Não").Count);
                        parametrosReport.Add("p_TotalDemanda_Incidente_SLO", Contabilizacao.RetornaListaChamadoSLA(Contabilizacao.RetornaListaTipoChamado(listaChamados, "Incidente"), "SLO").Count);

                        DefaultReportViewer.LocalReport.DisplayName = "Relatório de Chamados de Atendimento Técnico";

                        if (Emissao == TipoEmissao.Planilha)
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnicoPlanilhaMulta.rdlc";
                        else
                            DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioTecnicoMulta.rdlc";

                        DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                    }
                }
                break;
            #endregion

            case Relatorios.RelatorioMaquinasParadas:
                {
                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

                    parametrosReport.Add("p_DataInicial", dataInicial);
                    parametrosReport.Add("p_DataFinal", dataFinal);
                    #endregion

                    List<Chamado> listaChamados = Chamado.MontaChamados(dataInicial, dataFinal, Chave, Filtro, DataInicialMulta, DataFinalMulta);
                    //List<Chamado> listaChamados = new List<Chamado>();

                    //foreach (Chamado chamado in listaChamadosCompleto)
                    //{
                    //    if (!chamado.Categoria.Contains("EQUIPAMENTOS DE TI.IMPRESSORAS.XEROX.INFORMAÇÕES") &&
                    //        !chamado.Categoria.Contains("EQUIPAMENTOS DE TI.IMPRESSORAS.XEROX.INSTALAÇÃO / CONFIGURAÇÃO") &&
                    //        !chamado.Categoria.Contains("EQUIPAMENTOS DE TI.IMPRESSORAS.XEROX.LIMPEZA DA FILA DE IMPRESSÃO") &&
                    //        !chamado.Categoria.Contains("EQUIPAMENTOS DE TI.IMPRESSORAS.XEROX.RECOLHIMENTO DE TONER VAZIO"))
                    //        listaChamados.Add(chamado);
                    //}

                    Contabilizacao.ProcessarMulta(listaChamados);

                    listaChamados.Sort(delegate(Chamado chamado2, Chamado chamado1)
                    { return chamado2.TempoSLA.TotalSeconds.CompareTo(chamado1.TempoSLA.TotalSeconds); });

                    if (Emissao == TipoEmissao.Planilha)
                        DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioMaquinasParadas.rdlc";
                    else
                        DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\RelatorioMaquinasParadas.rdlc";

                    DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Chamado", listaChamados));
                }
                break;

            default:
                break;
        }
        if (parametrosReport.Count > 0 && Emissao == TipoEmissao.Relatorio)
            SetReportParameters(parametrosReport);
    }
    private void SetReportParameters(Dictionary<string, object> dicionarioParametros)
    {
        ReportParameter[] listaReportParameter = new ReportParameter[dicionarioParametros.Count];

        int i = 0;
        foreach (string nome in dicionarioParametros.Keys)
        {
            listaReportParameter[i] = new ReportParameter(nome, dicionarioParametros[nome].ToString());
            i++;
        }
        DefaultReportViewer.LocalReport.SetParameters(listaReportParameter);
    }
}