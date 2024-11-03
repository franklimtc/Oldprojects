using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Suprimentos_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FormatarFalhas();
        }
    }

    private void FormatarFalhas()
    {
        foreach (GridViewRow gvR in gvSuprimentosDetalhado.Rows)
        {
            if (gvR.Cells[10].Text == "S")
            {
                //gvR.Cells[13].Font.Bold = true;
                //gvR.Cells[13].ForeColor = System.Drawing.Color.Red;
                gvR.Cells[10].CssClass = "danger";

            }
            if (gvR.Cells[2].Text.ToLower().Contains("fortaleza"))
            {
                for (int i = 0; i < gvR.Cells.Count; i++)
                {
                    gvR.Cells[i].Font.Bold = true;
                }
            }
        }
    }

    protected void gvSuprimentosDetalhado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = gvSuprimentosDetalhado.Rows[Convert.ToInt32(e.CommandArgument)];
        string requisicao = Server.UrlEncode(row.Cells[0].Text);
        string endereco = Server.UrlEncode(row.Cells[3].Text);
        string serie = Server.UrlEncode(row.Cells[4].Text);
        string material = Server.UrlEncode(row.Cells[5].Text);
        string contato = Server.UrlEncode(row.Cells[9].Text);
        string telefone = Server.UrlEncode(row.Cells[10].Text);

        switch (e.CommandName)
        {
            case "Print":
                #region Comando Print
                Equipamentos eqp = Equipamentos.Localizar(Server.UrlEncode(row.Cells[4].Text));
                if (true)
                {
                    //string url = string.Format("../reports/Report.aspx?material={0}&endereco={1}&contato={2}&telefone={3}&serie={4}&requisicao={5}&unidade={6}&setor={7}", material, endereco, contato, telefone, serie, requisicao, eqp.Unidade, eqp.Setor);
                    string url = string.Format("http://192.168.2.10/Reports/report/Log%C3%ADstica/Comprovante_Entrega_Material?id={0}", requisicao);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("window.open('{0}', '_blank');", url), true);
                }
                
                #endregion
                break;
            case "Priorizar":
                #region Comando Priorizar
                GridViewRow rowPrio = gvSuprimentosDetalhado.Rows[Convert.ToInt32(e.CommandArgument)];
                reqSuprimentos.Priorizar(rowPrio.Cells[0].Text);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("alert('Hello!I am an alert box!!');"), true);

                #endregion
                break;
            case "Editar":
                if (true)
                {
                    string url = string.Format("../Suprimentos/editar.aspx?id={0}", requisicao);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", string.Format("window.open('{0}', '_blank');", url), true);
                }
                break;
            default:
                break;
        }

    }
}