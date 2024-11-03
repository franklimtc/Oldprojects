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
using System.Data.OleDb;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace CSFCenterReports
{
    public partial class ShowReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("pt-BR");
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

                #region Configura relatório
                if (!Request.QueryString.ToString().Contains("relatorio"))
                {
                    Response.Redirect("~/Principal.aspx");
                }
                else
                {
                    TipoRelatorio = (Relatorios)int.Parse(Request.QueryString["relatorio"]);

                    Uf = Request.QueryString["uf"];
                    Cidade = Request.QueryString["cidade"];
                    Unidade = Request.QueryString["unidade"];
                    Celula = Request.QueryString["celula"];
                    Ambiente = Request.QueryString["ambiente"];
                    Impressoras = Request.QueryString["impressoras"];
                    Usuarios = Request.QueryString["usuarios"];
                    FormatoFolha = Request.QueryString["formatofolha"];
                    Cor = Request.QueryString["cor"];
                    DataInicial = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtInicial"]));
                    DataFinal = DateTime.FromBinary(Convert.ToInt64(Request.QueryString["dtFinal"]));

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
        private string Uf
        {
            get { return (string)ViewState["uf"]; }
            set { ViewState["uf"] = (string)value; }
        }
        private string Cidade
        {
            get { return (string)ViewState["cidade"]; }
            set { ViewState["cidade"] = (string)value; }
        }
        private string Unidade
        {
            get { return (string)ViewState["unidade"]; }
            set { ViewState["unidade"] = (string)value; }
        }
        private string Celula
        {
            get { return (string)ViewState["celula"]; }
            set { ViewState["celula"] = (string)value; }
        }
        private string Ambiente
        {
            get { return (string)ViewState["ambiente"]; }
            set { ViewState["ambiente"] = (string)value; }
        }
        private string Impressoras
        {
            get { return (string)ViewState["impressoras"]; }
            set { ViewState["impressoras"] = (string)value; }
        }
        private string Usuarios
        {
            get { return (string)ViewState["usuarios"]; }
            set { ViewState["usuarios"] = (string)value; }
        }
        private string FormatoFolha
        {
            get { return (string)ViewState["formatofolha"]; }
            set { ViewState["formatofolha"] = (string)value; }
        }
        private string Cor
        {
            get { return (string)ViewState["cor"]; }
            set { ViewState["cor"] = (string)value; }
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
        #endregion

        private void ConfiguraRelatorio(Relatorios relatorio)
        {
            DefaultReportViewer.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
            DefaultReportViewer.ZoomPercent = 100;

            try
            {
                try
                {
                    Dictionary<string, object> parametrosReport = new Dictionary<string, object>();

                    #region Ajusta datas
                    DateTime dataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);
                    DateTime dataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);
                    #endregion

                    if (Uf != "Todos" && Uf != "")
                        lbUFValor.Text = Uf;
                    else
                        lbUFValor.Text = "-";

                    if (Cidade != "Todas" && Cidade != "")
                        lbCidadeValor.Text = Cidade;
                    else
                        lbCidadeValor.Text = "-";

                    if (Unidade != "Todas" && Unidade != "")
                        lbUnidadeValor.Text = Unidade;
                    else
                        lbUnidadeValor.Text = "-";

                    if (Celula != "Todos" && Celula != "")
                        lbCentroCustoValor.Text = Celula;

                    else
                        lbCentroCustoValor.Text = "-";

                    if (Ambiente != "Todos" && Ambiente != "")
                        lbAmbienteValor.Text = Ambiente;
                    else
                        lbAmbienteValor.Text = "-";
                     
                    lbPeriodoValor.Text = Util.FormatarDateTime(dataInicial) + " 00:00:00 a " + Util.FormatarDateTime(dataFinal) + " 23:59:59";

                    switch (relatorio)
                    {
                        #region 1 - Produção (Cópia/Impressão/Fax)
                        case Relatorios.R1_Produção_CopiaImpressaoFax:
                            {
                                lbRelatorioValor.Text = "Relatório de Produção (Cópia / Impressão / Fax)";

                                string impressoras = Impressoras;
                                if (impressoras.Contains(","))
                                {
                                    string[] imp = impressoras.Split(',');
                                    impressoras = "";
                                    for (int i = 0; i < imp.Length - 1; i++)
                                    {
                                        impressoras += "'" + imp[i] + "',";
                                    }
                                    impressoras = impressoras.Remove(impressoras.Length - 1);
                                }

                                string pUf = "%";
                                string pCidade = "%";
                                string pUnidade = "%";
                                string pCelula = "%";
                                string pAmbiente = "%";
                                string pImpressoras = "";

                                if (Uf != "Todos" && Uf != "")
                                    pUf = Uf;
                                if (Cidade != "Todas" && Cidade != "")
                                    pCidade = Cidade;
                                if (Unidade != "Todas" && Unidade != "")
                                    pUnidade = Unidade;
                                if (Celula != "Todos" && Celula != "")
                                    pCelula = Celula;
                                if (Ambiente != "Todos" && Ambiente != "")
                                    pAmbiente = Ambiente;
                                if (Impressoras != "Todas" && Impressoras != "")
                                    pImpressoras = impressoras;

                                parametrosReport.Add("dt_inicial", dataInicial);
                                parametrosReport.Add("dt_final", dataFinal);
                                parametrosReport.Add("uf", pUf);
                                parametrosReport.Add("cidade", pCidade);
                                parametrosReport.Add("unidade", pUnidade);
                                parametrosReport.Add("ambiente", pAmbiente);

                                Consulta.Inserir(new Consulta(0, Request.QueryString["user"], 1, pUf, pCidade, pUnidade, pCelula, pAmbiente, pImpressoras, null, null, null, dataInicial, dataFinal, Util.DataAtual()));
                                List<Relatorio1Dados> Dados = Relatorio1Dados.RetornaDados(dataInicial, dataFinal, pUf, pCidade, pUnidade, pCelula, pAmbiente, pImpressoras);
                                parametrosReport.Add("p_TotalImpressoes", Relatorio1Dados.RetornaTotal(Dados));

                                DefaultReportViewer.LocalReport.DisplayName = "Relatório 1";
                                //DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio1.rdlc";
                                DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio1_Planilha.rdlc";

                                //DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Relatorio1Dados", Dados));
                                DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetRelatorio1", Dados));
                            }
                            break;
                        #endregion

                        #region 2 - Analítico de bilhetagem
                        case Relatorios.R2_AnaliticoDeBilhetagem:
                            {
                                lbRelatorioValor.Text = "Relatório Analítico de Bilhetagem";

                                string impressoras = Impressoras;
                                if (impressoras.Contains(","))
                                {
                                    string[] imp = impressoras.Split(',');
                                    impressoras = "";
                                    for (int i = 0; i < imp.Length - 1; i++)
                                    {
                                        impressoras += "'" + imp[i] + "',";
                                    }
                                    impressoras = impressoras.Remove(impressoras.Length - 1);
                                }

                                string pUf = "%";
                                string pCidade = "%";
                                string pUnidade = "%";
                                string pCelula = "%";
                                string pAmbiente = "%";
                                string pImpressoras = "";

                                if (Uf != "Todos" && Uf != "")
                                    pUf = Uf;
                                if (Cidade != "Todas" && Cidade != "")
                                    pCidade = Cidade;
                                if (Unidade != "Todas" && Unidade != "")
                                    pUnidade = Unidade;
                                if (Celula != "Todos" && Celula != "")
                                    pCelula = Celula;
                                if (Ambiente != "Todos" && Ambiente != "")
                                    pAmbiente = Ambiente;
                                if (Impressoras != "Todas" && Impressoras != "")
                                    pImpressoras = impressoras;

                                parametrosReport.Add("dt_inicial", dataInicial);
                                parametrosReport.Add("dt_final", dataFinal);
                                parametrosReport.Add("uf", pUf);
                                parametrosReport.Add("cidade", pCidade);
                                parametrosReport.Add("unidade", pUnidade);
                                parametrosReport.Add("ambiente", pAmbiente);

                                Consulta.Inserir(new Consulta(0, Request.QueryString["user"], 2, pUf, pCidade, pUnidade, pCelula, pAmbiente, pImpressoras, null, null, null, dataInicial, dataFinal, Util.DataAtual()));
                                List<Relatorio2Dados> Dados = Relatorio2Dados.RetornaDados(dataInicial, dataFinal, pUf, pCidade, pUnidade, pCelula, pAmbiente, pImpressoras);
                                parametrosReport.Add("p_TotalImpressoes", Relatorio2Dados.RetornaTotal(Dados));

                                DefaultReportViewer.LocalReport.DisplayName = "Relatório 2";
                                //DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio2.rdlc";
                                DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio2_Planilha.rdlc";

                                //DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Relatorio2Dados", Dados));
                                DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetRelatorio2", Dados));
                            }
                            break;
                        #endregion

                        #region 3 - Impressão por usuário
                        case Relatorios.R3_ImpressaoPorUsuario:
                            {
                                lbRelatorioValor.Text = "Relatório de Impressões por usuário";

                                string impressoras = Impressoras;
                                if (impressoras.Contains(","))
                                {
                                    string[] imp = impressoras.Split(',');
                                    impressoras = "";
                                    for (int i = 0; i < imp.Length - 1; i++)
                                    {
                                        impressoras += "'" + imp[i] + "',";
                                    }
                                    impressoras = impressoras.Remove(impressoras.Length - 1);
                                }

                                string pUf = "%";
                                string pCidade = "%";
                                string pUnidade = "%";
                                string pCelula = "%";
                                string pAmbiente = "%";
                                string pImpressoras = "";

                                if (Uf != "Todos" && Uf != "")
                                    pUf = Uf;
                                if (Cidade != "Todas" && Cidade != "")
                                    pCidade = Cidade;
                                if (Unidade != "Todas" && Unidade != "")
                                    pUnidade = Unidade;
                                if (Celula != "Todos" && Celula != "")
                                    pCelula = Celula;
                                if (Ambiente != "Todos" && Ambiente != "")
                                    pAmbiente = Ambiente;
                                if (Impressoras != "Todos" && Impressoras != "Todas" && Impressoras != "")
                                    pImpressoras = impressoras;

                                parametrosReport.Add("dt_inicial", dataInicial);
                                parametrosReport.Add("dt_final", dataFinal);
                                parametrosReport.Add("uf", pUf);
                                parametrosReport.Add("cidade", pCidade);
                                parametrosReport.Add("unidade", pUnidade);
                                parametrosReport.Add("ambiente", pAmbiente);

                                Consulta.Inserir(new Consulta(0, Request.QueryString["user"], 3, pUf, pCidade, pUnidade, pCelula, pAmbiente, impressoras, Usuarios, FormatoFolha, Cor, dataInicial, dataFinal, Util.DataAtual()));

                                List<Impressora> ListaImpressoras = Impressora.RetornaImpressoras(pUf, pCidade, pUnidade, pCelula, pAmbiente, impressoras);
                                FilaImpressao.RetornaFilasImpressao(ListaImpressoras);
                                List<ArquivoImpresso> Arquivos = ArquivoImpresso.RetornaArquivosImpressos(dataInicial, dataFinal, ListaImpressoras, Usuarios, FormatoFolha, Cor);
                                ArquivoImpresso.ProcessarArquivosImpressos(ListaImpressoras, Arquivos);
                                parametrosReport.Add("p_TotalImpressoes", 0);

                                //List<Relatorio3Dados> Dados = Relatorio3Dados.RetornaDados(dataInicial, dataFinal, pUf, pCidade, pUnidade, pCelula, pAmbiente, Usuarios, pImpressoras, FormatoFolha, Cor);
                                //parametrosReport.Add("p_TotalImpressoes", Relatorio3Dados.RetornaTotal(Dados));

                                DefaultReportViewer.LocalReport.DisplayName = "Relatório 3";
                                //DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio3.rdlc";
                                DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio3_Planilha.rdlc";

                                //DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Relatorio3Dados", Dados));
                                DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetRelatorio3", Arquivos));
                            }
                            break;
                        #endregion

                        #region 4 - Impressão por usuário (Detalhado)
                        case Relatorios.R4_ImpressaoPorUsuario_Detalhado:
                            {
                                lbRelatorioValor.Text = "Relatório de Impressões por usuário (Detalhado)";

                                string impressoras = Impressoras;
                                if (impressoras.Contains(","))
                                {
                                    string[] imp = impressoras.Split(',');
                                    impressoras = "";
                                    for (int i = 0; i < imp.Length - 1; i++)
                                    {
                                        impressoras += "'" + imp[i] + "',";
                                    }
                                    impressoras = impressoras.Remove(impressoras.Length - 1);
                                }

                                string pUf = "%";
                                string pCidade = "%";
                                string pUnidade = "%";
                                string pCelula = "%";
                                string pAmbiente = "%";
                                string pImpressoras = "";

                                if (Uf != "Todos" && Uf != "")
                                    pUf = Uf;
                                if (Cidade != "Todas" && Cidade != "")
                                    pCidade = Cidade;
                                if (Unidade != "Todas" && Unidade != "")
                                    pUnidade = Unidade;
                                if (Celula != "Todos" && Celula != "")
                                    pCelula = Celula;
                                if (Ambiente != "Todos" && Ambiente != "")
                                    pAmbiente = Ambiente;
                                if (Impressoras != "Todas" && Impressoras != "")
                                    pImpressoras = impressoras;

                                parametrosReport.Add("dt_inicial", dataInicial);
                                parametrosReport.Add("dt_final", dataFinal);
                                parametrosReport.Add("uf", pUf);
                                parametrosReport.Add("cidade", pCidade);
                                parametrosReport.Add("unidade", pUnidade);
                                parametrosReport.Add("ambiente", pAmbiente);

                                Consulta.Inserir(new Consulta(0, Request.QueryString["user"], 4, pUf, pCidade, pUnidade, pCelula, pAmbiente, impressoras, Usuarios, FormatoFolha, Cor, dataInicial, dataFinal, Util.DataAtual()));

                                List<Impressora> ListaImpressoras = Impressora.RetornaImpressoras(pUf, pCidade, pUnidade, pCelula, pAmbiente, impressoras);
                                FilaImpressao.RetornaFilasImpressao(ListaImpressoras);
                                List<ArquivoImpresso> Arquivos = ArquivoImpresso.RetornaArquivosImpressos(dataInicial, dataFinal, ListaImpressoras, Usuarios, FormatoFolha, Cor);
                                ArquivoImpresso.ProcessarArquivosImpressos(ListaImpressoras, Arquivos);
                                parametrosReport.Add("p_TotalImpressoes", 0);

                                //List<Relatorio4Dados> Dados = Relatorio4Dados.RetornaDados(dataInicial, dataFinal, pUf, pCidade, pUnidade, pCelula, pAmbiente, Usuarios, pImpressoras, FormatoFolha, Cor);
                                //parametrosReport.Add("p_TotalImpressoes", Relatorio4Dados.RetornaTotal(Dados));

                                DefaultReportViewer.LocalReport.DisplayName = "Relatório 4";
                                //DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio4.rdlc";
                                DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio4_Planilha.rdlc";

                                //DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Relatorio4Dados", Dados));
                                DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetRelatorio4", Arquivos));
                            }
                            break;
                        #endregion

                        #region 5 - Impressão por usuário (Consolidado)
                        case Relatorios.R5_ImpressaoPorUsuario_Consolidado:
                            {
                                lbRelatorioValor.Text = "Relatório de Impressões por usuário (Consolidado)";

                                string impressoras = Impressoras;
                                if (impressoras.Contains(","))
                                {
                                    string[] imp = impressoras.Split(',');
                                    impressoras = "";
                                    for (int i = 0; i < imp.Length - 1; i++)
                                    {
                                        impressoras += "'" + imp[i] + "',";
                                    }
                                    impressoras = impressoras.Remove(impressoras.Length - 1);
                                }

                                string pUf = "%";
                                string pCidade = "%";
                                string pUnidade = "%";
                                string pCelula = "%";
                                string pAmbiente = "%";
                                string pImpressoras = "";

                                if (Uf != "Todos" && Uf != "")
                                    pUf = Uf;
                                if (Cidade != "Todas" && Cidade != "")
                                    pCidade = Cidade;
                                if (Unidade != "Todas" && Unidade != "")
                                    pUnidade = Unidade;
                                if (Celula != "Todos" && Celula != "")
                                    pCelula = Celula;
                                if (Ambiente != "Todos" && Ambiente != "")
                                    pAmbiente = Ambiente;
                                if (Impressoras != "Todas" && Impressoras != "")
                                    pImpressoras = impressoras;

                                parametrosReport.Add("dt_inicial", dataInicial);
                                parametrosReport.Add("dt_final", dataFinal);
                                parametrosReport.Add("uf", pUf);
                                parametrosReport.Add("cidade", pCidade);
                                parametrosReport.Add("unidade", pUnidade);
                                parametrosReport.Add("ambiente", pAmbiente);

                                Consulta.Inserir(new Consulta(0, Request.QueryString["user"], 5, pUf, pCidade, pUnidade, pCelula, pAmbiente, impressoras, Usuarios, FormatoFolha, Cor, dataInicial, dataFinal, Util.DataAtual()));

                                List<Relatorio5Dados> Dados = Relatorio5Dados.RetornaDados(dataInicial, dataFinal, pUf, pCidade, pUnidade, pCelula, pAmbiente, pImpressoras, Usuarios);
                                parametrosReport.Add("p_TotalImpressoes", Relatorio5Dados.RetornaTotal(Dados));

                                DefaultReportViewer.LocalReport.DisplayName = "Relatório 5";
                                //DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio2.rdlc";
                                DefaultReportViewer.LocalReport.ReportPath = @"Relatorios\Relatorio5_Planilha.rdlc";

                                //DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Relatorio2Dados", Dados));
                                DefaultReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetRelatorio5", Dados));
                            }
                            break;
                        #endregion

                        default:
                            break;
                    }
                    if (parametrosReport.Count > 0)
                        SetReportParameters(parametrosReport);
                }
                finally
                { }
            }
            finally
            { }
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
}