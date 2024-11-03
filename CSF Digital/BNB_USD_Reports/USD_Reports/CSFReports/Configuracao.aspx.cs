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
using Telerik.Web.UI;

public partial class Configuracao : System.Web.UI.Page
{
    private static List<Chamado> listaRodape;
    private static int atualizacoesRodape = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Retorna Parametros
            Parametro.RetornarParametros(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Parametros.xml");
            //Retorna Avisos
            Aviso.RetornarAvisos(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Avisos.xml");
            //Retorna SLA - Toner
            SLA.RetornarSLA(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Toner.xml", true);
            //Retorna SLA - Assistência Técnica
            SLA.RetornarSLA(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Assistencia.xml", false);
            //Retorna Chaves
            Chave.RetornarChaves(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Chaves.xml");
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
        Response.Redirect("~/Configuracao.aspx");
    }

    #region Grid - Parâmetros
    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = Parametro.RetornaParametros();
        }
        finally
        {
            
        }
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridDataItem item = (GridDataItem)e.Item;
        string Nome = item["Nome"].Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Parametros.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Parametro"].Rows.Count; i++)
            {
                if (ds.Tables["Parametro"].Rows[i]["Nome"].ToString().Trim() == Nome.Trim())
                    ds.Tables["Parametro"].Rows[i].Delete();
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid1.Controls.Add(new LiteralControl("Não foi possível remover o parâmetro. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        string Nome = (editedItem["Nome"].Controls[0] as TextBox).Text;
        string Valor = (editedItem["Valor"].Controls[0] as TextBox).Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Parametros.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Parametro"].Rows.Count; i++)
            {
                if (ds.Tables["Parametro"].Rows[i]["Nome"].ToString().Trim() == Nome.Trim())
                    ds.Tables["Parametro"].Rows[i]["Valor"] = Valor;
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid1.Controls.Add(new LiteralControl("Não foi possível alterar o parâmetro. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

        string Nome = (insertedItem["Nome"].Controls[0] as TextBox).Text;
        string Valor = (insertedItem["Valor"].Controls[0] as TextBox).Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Parametros.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            DataRow dr = ds.Tables["Parametro"].NewRow();
            dr["Nome"] = Nome;
            dr["Valor"] = Valor;
            ds.Tables["Parametro"].Rows.Add(dr);

            ds.AcceptChanges();
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid1.Controls.Add(new LiteralControl("Não foi possível inserir novo parametro. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }
    #endregion

    #region Grid - Avisos
    protected void RadGrid2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid2.DataSource = Aviso.RetornaAvisos();
        }
        finally
        {

        }
    }

    protected void RadGrid2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridDataItem item = (GridDataItem)e.Item;
        string Id = item["Id"].Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Avisos.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Aviso"].Rows.Count; i++)
            {
                if (ds.Tables["Aviso"].Rows[i]["Id"].ToString().Trim() == Id.Trim())
                    ds.Tables["Aviso"].Rows[i].Delete();
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid1.Controls.Add(new LiteralControl("Não foi possível remover o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid2_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        string Id = (editedItem["Id"].Controls[0] as TextBox).Text;
        string Descricao = (editedItem["Descricao"].Controls[0] as TextBox).Text;
        string CriadoPor = (editedItem["CriadoPor"].Controls[0] as TextBox).Text;
        DateTime CriadoEm = Convert.ToDateTime((editedItem["CriadoEm"].Controls[0] as TextBox).Text);

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Avisos.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Aviso"].Rows.Count; i++)
            {
                if (ds.Tables["Aviso"].Rows[i]["Id"].ToString().Trim() == Id.Trim())
                {
                    ds.Tables["Aviso"].Rows[i]["Descricao"] = Descricao;
                    //ds.Tables["Aviso"].Rows[i]["CriadoPor"] = CriadoPor;
                    //ds.Tables["Aviso"].Rows[i]["CriadoEm"] = Util.DataAtual();
                }
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid1.Controls.Add(new LiteralControl("Não foi possível alterar o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid2_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

        string Id = (insertedItem["Id"].Controls[0] as TextBox).Text;
        string Descricao = (insertedItem["Descricao"].Controls[0] as TextBox).Text;
        string CriadoPor = (insertedItem["CriadoPor"].Controls[0] as TextBox).Text;
        DateTime CriadoEm = Convert.ToDateTime((insertedItem["CriadoEm"].Controls[0] as TextBox).Text);

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Avisos.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            DataRow dr = ds.Tables["Aviso"].NewRow();
            dr["Id"] = Id;
            dr["Descricao"] = Descricao;
            dr["CriadoPor"] = CriadoPor;
            dr["CriadoEm"] = CriadoEm;
            ds.Tables["Aviso"].Rows.Add(dr);

            ds.AcceptChanges();
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid1.Controls.Add(new LiteralControl("Não foi possível inserir novo aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }
    #endregion

    #region Grid - SLAs - Assitencia
    protected void RadGrid3_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid3.DataSource = SLA.RetornaQuadro_Assistencia();
        }
        finally
        {

        }
    }

    protected void RadGrid3_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridDataItem item = (GridDataItem)e.Item;

        string Id = item["Id"].Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Assistencia.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["SLA"].Rows.Count; i++)
            {
                if (ds.Tables["SLA"].Rows[i]["Id"].ToString().Trim() == Id.Trim())
                    ds.Tables["SLA"].Rows[i].Delete();
            }

            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid3.Controls.Add(new LiteralControl("Não foi possível remover o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid3_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        int Id = int.Parse((editedItem["Id"].Controls[0] as TextBox).Text);
        int Prioridade = int.Parse((editedItem["Prioridade"].Controls[0] as TextBox).Text);
        Chamado.Severidade Severidade = Chamado.RetornaNivelSeveridade(Prioridade);
        Localidade.TipoLocalidade TipoLocalidade = Localidade.RetornaTipoLocalidadeNome((editedItem["Local"].Controls[0] as TextBox).Text.Trim().ToUpper());
        TimeSpan Limite = TimeSpan.Zero;
        TimeSpan LimiteContingencia = TimeSpan.Zero;

        try
        {
            string[] tempo = (editedItem["LimiteFormatado"].Controls[0] as TextBox).Text.Trim().Split(':');

            Limite = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
        }
        finally
        { }

        try
        {
            string[] tempo = (editedItem["LimiteContingenciaFormatado"].Controls[0] as TextBox).Text.Trim().Split(':');

            LimiteContingencia = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
        }
        finally
        { }

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Assistencia.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["SLA"].Rows.Count; i++)
            {
                if (ds.Tables["SLA"].Rows[i]["Id"].ToString().Trim() == Id.ToString())
                {
                    ds.Tables["SLA"].Rows[i]["Prioridade"] = Prioridade;
                    ds.Tables["SLA"].Rows[i]["TipoLocalidade"] = TipoLocalidade;
                    ds.Tables["SLA"].Rows[i]["Limite"] = Util.FormatarTimeSpan(Limite);
                    ds.Tables["SLA"].Rows[i]["LimiteContingencia"] = Util.FormatarTimeSpan(LimiteContingencia);
                }
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid3.Controls.Add(new LiteralControl("Não foi possível alterar o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid3_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

        int Prioridade = int.Parse((insertedItem["Prioridade"].Controls[0] as TextBox).Text);
        Chamado.Severidade Severidade = Chamado.RetornaNivelSeveridade(Prioridade);
        Localidade.TipoLocalidade TipoLocalidade = Localidade.RetornaTipoLocalidadeNome((insertedItem["Local"].Controls[0] as TextBox).Text.Trim().ToUpper());
        TimeSpan Limite = TimeSpan.Zero;
        TimeSpan LimiteContingencia = TimeSpan.Zero;
        
        try
        {
            try
            {
                string[] tempo = (insertedItem["LimiteFormatado"].Controls[0] as TextBox).Text.Trim().Split(':');

                Limite = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
            }
            catch (Exception ex)
            { }
        }
        finally
        { }

        try
        {
            try
            {
                string[] tempo = (insertedItem["LimiteContingenciaFormatado"].Controls[0] as TextBox).Text.Trim().Split(':');

                LimiteContingencia = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
            }
            catch (Exception ex)
            { }
        }
        finally
        { }

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Assistencia.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            DataRow dr = ds.Tables["SLA"].NewRow();

            if (ds.Tables["SLA"].Rows.Count > 0)
                dr["Id"] = int.Parse(ds.Tables["SLA"].Rows[ds.Tables["SLA"].Rows.Count - 1]["Id"].ToString()) + 1;
            else
                dr["Id"] = 1;

            dr["Prioridade"] = Prioridade;
            dr["TipoLocalidade"] = TipoLocalidade;
            dr["Limite"] = Util.FormatarTimeSpan(Limite);
            dr["LimiteContingencia"] = Util.FormatarTimeSpan(LimiteContingencia);
            ds.Tables["SLA"].Rows.Add(dr);

            ds.AcceptChanges();
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            e.Canceled = true;
        }
    }
    #endregion

    #region Grid - SLAs - Toner
    protected void RadGrid4_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid4.DataSource = SLA.RetornaQuadro_Toner();
        }
        finally
        {

        }
    }

    protected void RadGrid4_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridDataItem item = (GridDataItem)e.Item;

        string Id = item["Id"].Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Toner.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["SLA"].Rows.Count; i++)
            {
                if (ds.Tables["SLA"].Rows[i]["Id"].ToString().Trim() == Id.Trim())
                    ds.Tables["SLA"].Rows[i].Delete();
            }

            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid4.Controls.Add(new LiteralControl("Não foi possível remover o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid4_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        int Id = int.Parse((editedItem["Id"].Controls[0] as TextBox).Text);
        Localidade.Regiao Regiao = Localidade.RetornaRegiao((editedItem["LocalRegiao"].Controls[0] as TextBox).Text.Trim());
        Localidade.TipoLocalidade TipoLocalidade = Localidade.RetornaTipoLocalidadeNome((editedItem["Local"].Controls[0] as TextBox).Text.Trim().ToUpper());
        TimeSpan Limite = TimeSpan.Zero;

        try
        {
            try
            {
                string[] tempo = (editedItem["LimiteFormatado"].Controls[0] as TextBox).Text.Trim().Split(':');

                Limite = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
            }
            catch (Exception ex)
            { }
        }
        finally
        { }

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Toner.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["SLA"].Rows.Count; i++)
            {
                if (ds.Tables["SLA"].Rows[i]["Id"].ToString().Trim() == Id.ToString())
                {
                    ds.Tables["SLA"].Rows[i]["Regiao"] = Localidade.RetornaRegiaoNome(Regiao);
                    ds.Tables["SLA"].Rows[i]["TipoLocalidade"] = TipoLocalidade;
                    ds.Tables["SLA"].Rows[i]["Limite"] = Util.FormatarTimeSpan(Limite);
                }
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid4.Controls.Add(new LiteralControl("Não foi possível alterar o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid4_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

        Localidade.Regiao Regiao = Localidade.RetornaRegiao((insertedItem["LocalRegiao"].Controls[0] as TextBox).Text.Trim());
        Localidade.TipoLocalidade TipoLocalidade = Localidade.RetornaTipoLocalidadeNome((insertedItem["Local"].Controls[0] as TextBox).Text.Trim().ToUpper());
        TimeSpan Limite = TimeSpan.Zero;

        try
        {
            try
            {
                string[] tempo = (insertedItem["LimiteFormatado"].Controls[0] as TextBox).Text.Trim().Split(':');

                Limite = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
            }
            catch (Exception ex)
            { }
        }
        finally
        { }

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\SLA_Toner.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            DataRow dr = ds.Tables["SLA"].NewRow();
            
            if (ds.Tables["SLA"].Rows.Count > 0)
                dr["Id"] = int.Parse(ds.Tables["SLA"].Rows[ds.Tables["SLA"].Rows.Count - 1]["Id"].ToString()) + 1;
            else
                dr["Id"] = 1;

            dr["Regiao"] = Localidade.RetornaRegiaoNome(Regiao);
            dr["TipoLocalidade"] = TipoLocalidade;
            dr["LimiteFormatado"] = Util.FormatarTimeSpan(Limite);
            ds.Tables["SLA"].Rows.Add(dr);

            ds.AcceptChanges();
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid4.Controls.Add(new LiteralControl("Não foi possível inserir novo aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }
    #endregion

    #region Grid - Chaves
    protected void RadGrid5_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid5.DataSource = Chave.RetornarChaves();
        }
        finally
        {

        }
    }

    protected void RadGrid5_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridDataItem item = (GridDataItem)e.Item;

        string Valor = item["Valor"].Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Chaves.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Chave"].Rows.Count; i++)
            {
                if (ds.Tables["Chave"].Rows[i]["Valor"].ToString().Trim() == Valor.Trim())
                {
                    ds.Tables["Chave"].Rows[i].Delete();
                }
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid5.Controls.Add(new LiteralControl("Não foi possível remover o parâmetro. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid5_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        string ValorAntigo = (editedItem["ValorAntigo"].Controls[0] as TextBox).Text;
        string Valor = (editedItem["Valor"].Controls[0] as TextBox).Text;
        string Traducao = (editedItem["Traducao"].Controls[0] as TextBox).Text;
        bool Ativo = (editedItem["Ativo"].Controls[0] as CheckBox).Checked;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Chaves.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Chave"].Rows.Count; i++)
            {
                if (ds.Tables["Chave"].Rows[i]["Valor"].ToString().Trim() == Valor.Trim())
                {
                    ds.Tables["Chave"].Rows[i]["ValorAntigo"] = ValorAntigo;
                    ds.Tables["Chave"].Rows[i]["Traducao"] = Traducao;
                    ds.Tables["Chave"].Rows[i]["Ativo"] = Ativo;
                }
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid5.Controls.Add(new LiteralControl("Não foi possível alterar o parâmetro. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid5_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

        string ValorAntigo = (insertedItem["ValorAntigo"].Controls[0] as TextBox).Text;
        string Valor = (insertedItem["Valor"].Controls[0] as TextBox).Text;
        string Traducao = (insertedItem["Traducao"].Controls[0] as TextBox).Text;
        bool Ativo = (insertedItem["Ativo"].Controls[0] as CheckBox).Checked;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Chaves.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            DataRow dr = ds.Tables["Parametro"].NewRow();
            dr["ValorAntigo"] = ValorAntigo;
            dr["Valor"] = Valor;
            dr["Traducao"] = Traducao;
            dr["Ativo"] = Ativo;
            ds.Tables["Parametro"].Rows.Add(dr);

            ds.AcceptChanges();
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid5.Controls.Add(new LiteralControl("Não foi possível inserir novo parametro. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }
    #endregion

    #region Grid - Feriados
    protected void RadGrid6_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid6.DataSource = Feriado.RetornaListaFeriados();
        }
        finally
        {

        }
    }

    protected void RadGrid6_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridDataItem item = (GridDataItem)e.Item;

        string Cidade = item["Cidade"].Text;
        string Estado = item["Estado"].Text;
        DateTime DataFeriado = DateTime.Parse(item["DataFeriado"].Text);
        string Descricao = item["Descricao"].Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Feriados.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Feriado"].Rows.Count; i++)
            {
                if (ds.Tables["Feriado"].Rows[i]["Data"].ToString() == DataFeriado.ToString() &&
                    ds.Tables["Feriado"].Rows[i]["UF"].ToString() == Estado.ToString() &&
                    ds.Tables["Feriado"].Rows[i]["Cidade"].ToString() == Cidade.ToString())
                {
                    ds.Tables["Feriado"].Rows[i].Delete();
                }

            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid6.Controls.Add(new LiteralControl("Não foi possível remover o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid6_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        string Cidade = (editedItem["Cidade"].Controls[0] as TextBox).Text;
        string Estado = (editedItem["Estado"].Controls[0] as TextBox).Text;
        DateTime DataFeriado = DateTime.Parse((editedItem["DataFeriado"].Controls[0] as TextBox).Text);
        string Descricao = (editedItem["Descricao"].Controls[0] as TextBox).Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Feriados.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            for (int i = 0; i < ds.Tables["Feriado"].Rows.Count; i++)
            {
                if (ds.Tables["Feriado"].Rows[i]["Data"].ToString() == DataFeriado.ToString() &&
                    ds.Tables["Feriado"].Rows[i]["UF"].ToString() == Estado.ToString() &&
                    ds.Tables["Feriado"].Rows[i]["Cidade"].ToString() == Cidade.ToString())
                {
                    ds.Tables["Feriado"].Rows[i]["Descricao"] = Descricao;
                }
            }
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid6.Controls.Add(new LiteralControl("Não foi possível alterar o aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }

    protected void RadGrid6_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

        string Cidade = (insertedItem["Cidade"].Controls[0] as TextBox).Text;
        string Estado = (insertedItem["Estado"].Controls[0] as TextBox).Text;
        DateTime DataFeriado = DateTime.Parse((insertedItem["DataFeriado"].Controls[0] as TextBox).Text);
        string Descricao = (insertedItem["Descricao"].Controls[0] as TextBox).Text;

        try
        {
            string diretorioXML = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Dados\Feriados.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(diretorioXML);

            DataRow dr = ds.Tables["Feriado"].NewRow();
            dr["Cidade"] = Cidade;
            dr["UF"] = Estado;
            dr["Data"] = String.Format("{0:dd/MM/yyyy}", DataFeriado);
            dr["Descricao"] = Descricao;
            ds.Tables["Feriado"].Rows.Add(dr);

            ds.AcceptChanges();
            ds.WriteXml(diretorioXML);

            Response.Redirect("~/Configuracao.aspx");
        }
        catch (Exception ex)
        {
            RadGrid6.Controls.Add(new LiteralControl("Não foi possível inserir novo aviso. Motivo : " + ex.Message));
            e.Canceled = true;
        }
    }
    #endregion
}