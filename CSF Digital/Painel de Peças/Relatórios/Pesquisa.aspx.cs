using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Relatórios_Pesquisa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbdtInicial.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbdtFinal.Text = DateTime.Now.ToString("yyyy-MM-dd"); ;
        }
    }

    protected void btPesquisa_Click(object sender, EventArgs e)
    {
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE UF LIKE '%CE%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE CIDADE LIKE '%FORTALEZA%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE Série LIKE '%MAE505293%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE USD LIKE '%2014-3615645%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE PartNumber LIKE '%002N02805%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE Solicitante LIKE '%ALVARO%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE Status LIKE '%Aberto%'
        //select * from PecasFull('01/01/2010','30/01/2020') WHERE Postagem LIKE '%DG233982845BR%'
        
        string tsql = null;

        if (tbdtInicial.Text != "" && tbdtFinal.Text != "")
        {
            tsql = "select * from PecasFull('" + DateTime.Parse(tbdtInicial.Text).ToString("dd/MM/yyyy") + "  00:00:00.000" + "','" + DateTime.Parse(tbdtFinal.Text).ToString("dd/MM/yyyy") + " 23:59:59.999')";
        }
        else
        {
            tsql = "select * from PecasFull('01/01/2010','30/01/2020')";
        }

         
        if (tbCidade.Text != "" || tbParNumber.Text != "" || tbPostagem.Text != "" || tbSerie.Text != "" || tbSolicitante.Text != "" || tbStatus.Text != "" || tbUf.Text != "" || tbUsd.Text != "")
        {
            tsql += " WHERE ID > 1 ";
        }

        if (tbCidade.Text != "")
            tsql += " and cidade like '%" + tbCidade.Text + "%'";
        if (tbParNumber.Text != "")
            tsql += " and PartNumber like '%" + tbParNumber.Text + "%'";
        if (tbPostagem.Text != "")
            tsql += " and postagem like '%" + tbPostagem.Text + "%'";
        if (tbSerie.Text != "")
            tsql += " and Série like '%" + tbSerie.Text + "%'";
        if (tbSolicitante.Text != "")
            tsql += " and solicitante like '%" + tbSolicitante.Text + "%'";
        if (tbStatus.Text != "")
            tsql += " and status like '%" + tbStatus.Text + "%'";
        if (tbUf.Text != "")
            tsql += " and uf like '%" + tbUf.Text + "%'";
        if (tbUsd.Text != "")
            tsql += " and usd like '%" + tbUsd.Text + "%'";
        dsPesquisa.SelectCommand = tsql;
        dsPesquisa.DataBind();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["pecasConnectionString1"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select name, contentType, data from ratFiles where idratFile in (SELECT  idratFile FROM reqPecas where idreqPeca = @Id)";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }
                con.Close();
            }
        }

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}