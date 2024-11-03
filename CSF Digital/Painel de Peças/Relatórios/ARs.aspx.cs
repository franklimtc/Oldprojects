using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatórios_ARs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void tbPesquisar_Click(object sender, EventArgs e)
    {
        if (tbSerie.Text != "")
        {
            dsPecas.SelectCommand = string.Format(@"select a.idreqPeca, a.reqUSD 'USD', a.partNumber, a.peca 'Peça', a.Solicitante, a.Tecnico, b.postagem, b.AR,b.dtEnvio from reqpecas as a
inner join atulPecas as b on a.idreqpeca = b.idreqpeca
where a.serieEqpto = '{0}'", tbSerie.Text);
            dsSuprimentos.SelectCommand = string.Format(@"select idEnvio, serie, partNumber, etiqueta, tpEnvio, postagem, AR, statusentrega from enviosSuprimentos where serie = '{0}'", tbSerie.Text);

            gvPecas.DataBind();
            gvSuprimentos.DataBind();
        }
    }
}