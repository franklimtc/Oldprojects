using System;
using System.Web.UI.WebControls;

public partial class Atualizacoes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        string[] Atualizacao = new string[6];
        string url;
        //string idreqPeca = Convert.ToString(row.Cells[1].Text);
        //string uf = Convert.ToString(row.Cells[2].Text);
        //string cidade = Convert.ToString(row.Cells[3].Text);
        //string partNumber = Convert.ToString(row.Cells[4].Text);
        //string descricao = Convert.ToString(row.Cells[5].Text);
        //string dtSolicitacao = Convert.ToString(row.Cells[6].Text);

        //Atualizacao[0] = Convert.ToString(row.Cells[1].Text);
        //Atualizacao[1] = Convert.ToString(row.Cells[2].Text);
        //Atualizacao[2] = Convert.ToString(row.Cells[3].Text);
        //Atualizacao[3] = Convert.ToString(row.Cells[4].Text);
        //Atualizacao[4] = Convert.ToString(row.Cells[5].Text);
        //Atualizacao[5] = Convert.ToString(row.Cells[6].Text);

        url = "idreqPeca=" + Convert.ToString(row.Cells[1].Text);
        url = url + "&uf=" + Convert.ToString(row.Cells[4].Text);
        url = url + "&cidade=" + Convert.ToString(row.Cells[5].Text);
        url = url + "&Serie=" + Convert.ToString(row.Cells[6].Text);
        url = url + "&partNumber=" + Convert.ToString(row.Cells[7].Text);
        url = url + "&descricao=" + Convert.ToString(row.Cells[8].Text);
        url = url + "&dtSolicitacao=" + Convert.ToString(row.Cells[12].Text);
        Response.Redirect("Atualizar.aspx?" + url.Replace("\n", ""));
    }
}