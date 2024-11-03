using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace dnaPrint.Config
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void CarregarDados()
        {
            //using (Context ctx = new Context())
            //{
            //    List<config> lista = ctx.config.ToList();

            //    tbServer.Text = lista.Where(x => x.configuracao == "servidor").FirstOrDefault().valor;
            //    tbPort.Text = lista.Where(x => x.configuracao == "porta").FirstOrDefault().valor;
            //    tbBase.Text = lista.Where(x => x.configuracao == "base").FirstOrDefault().valor;
            //    tbUser.Text = lista.Where(x => x.configuracao == "usuario").FirstOrDefault().valor;
            //    tbPassword.Text = lista.Where(x => x.configuracao == "senha").FirstOrDefault().valor;

            //}
        }

        private void tbAtualizar_Click(object sender, EventArgs e)
        {

            //Context ctx = new Context();
            //List<config> lista = ctx.config.ToList();

            //foreach (var item in lista)
            //{
            //    switch (item.configuracao)
            //    {
            //        case "servidor":
            //            item.valor = tbServer.Text;
            //            break;
            //        case "porta":
            //            item.valor = tbPort.Text;
            //            break;
            //        case "base":
            //            item.valor = tbBase.Text;
            //            break;
            //        case "usuario":
            //            item.valor = tbUser.Text;
            //            break;
            //        case "senha":
            //            item.valor = tbPassword.Text;
            //            break;
            //        case "tipoBanco":
            //            item.valor = comboBox1.Text;                         
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //ctx.SaveChanges();

            using (dnaPrintWS.ExecuteClient client = new dnaPrintWS.ExecuteClient())
            {
                var listaOids = client.ListarOids("CsfDigit@l2016").ToList();
                dnaPrint.Base.OID.Atualizar(ConfigurationManager.ConnectionStrings["db"].ToString(), DAO.Operacoes.tipo.SQLServer, listaOids);
            }

            MessageBox.Show("Informações atualizadas!");
        }

        private void btExecutar_Click(object sender, EventArgs e)
        {
            DAO.SQLServer baseSql = new DAO.SQLServer();

            string connString = ConfigurationManager.ConnectionStrings["db"].ToString();

            if (baseSql.Conectar(connString))
            {
                if (tbComando.Text.ToLower().Contains("select"))
                {
                    try
                    {
                        gvResult.DataSource = baseSql.ReturnDt(connString, tbComando.Text);

                    }
                    catch (Exception ex)
                    {
                        tbMessage.Text = ex.Message;
                    }
                }
                else
                {
                    gvResult.DataSource = baseSql.ExecuteScalar(connString, tbComando.Text);
                }
            }

            
        }
    }
}
