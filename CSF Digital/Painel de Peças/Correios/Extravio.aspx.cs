using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Correios_Extravio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btPostagem_Click(object sender, EventArgs e)
    {
        string tsqlPostagem = string.Format(@"select b.uf, b.cidade, b.endereco, b.bairro, b.cep, a.serie, a.postagem 
from enviosSuprimentos as a 
left join equipamentos as b on a.serie = b.serie
where a.postagem = '{0}'", tbPostagem.Text);
        dsPostagem.SelectCommand = tsqlPostagem;
        dsPostagem.DataBind();
        gvPostagem.DataBind();
        tbPostagemNova.Text = tbPostagem.Text;
    }

    protected void btInserirChamado_Click(object sender, EventArgs e)
    {
        Correios.AbrirReclamacao(tbPostagemNova.Text, User.Identity.Name, tbProtocoloNovo.Text);
    }
}