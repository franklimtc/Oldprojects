using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using CSFDigital.Controls;
using Telerik.Web.UI;

public partial class Home : System.Web.UI.Page
{
    private static List<Chamado> listaRodape;
    private static int atualizacoesRodape = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Retorna Parametros
            Parametro.RetornarParametros(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Parametros.xml");
            //Retorna Chaves
            Chave.RetornarChaves(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Chaves.xml");
            //Retorna SLA - Toner
            SLA.RetornarSLA(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Toner.xml", true);
            //Retorna SLA - Assistência Técnica
            SLA.RetornarSLA(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Assistencia.xml", false);

            //Retorna Feriados
            Feriado.RetornaListaFeriados(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Feriados.xml");

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
        Response.Redirect("~/Acompanhamento.aspx");
    }
    protected void rbtExportarExcel_Click(object sender, EventArgs e)
    {
        this.RadGridAcompanhamento.ExportSettings.IgnorePaging = true;
        this.RadGridAcompanhamento.ExportSettings.OpenInNewWindow = true;
        this.RadGridAcompanhamento.MasterTableView.ExportToExcel();
    }
    protected void rbtExportarCSV_Click(object sender, EventArgs e)
    {
        this.RadGridAcompanhamento.ExportSettings.IgnorePaging = true;
        this.RadGridAcompanhamento.ExportSettings.OpenInNewWindow = true;
        this.RadGridAcompanhamento.MasterTableView.ExportToCSV();
    }
    protected void rbtExportarWord_Click(object sender, EventArgs e)
    {
        this.RadGridAcompanhamento.ExportSettings.IgnorePaging = true;
        this.RadGridAcompanhamento.ExportSettings.OpenInNewWindow = true;
        this.RadGridAcompanhamento.MasterTableView.ExportToWord();
    }
    protected void rbtExportarPDF_Click(object sender, EventArgs e)
    {
        this.RadGridAcompanhamento.ExportSettings.IgnorePaging = true;
        this.RadGridAcompanhamento.ExportSettings.OpenInNewWindow = true;
        this.RadGridAcompanhamento.MasterTableView.ExportToPdf();
    }
}