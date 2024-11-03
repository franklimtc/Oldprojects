using CSF_Correios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Postagens;

namespace Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox2.Text = "";
                try
                {
                    //CSF_Correios.Eventos ev = CSF_Correios.Eventos.Rastrear(textBox1.Text);
                    Postagens.logPostgem log = new Postagens.logPostgem(textBox1.Text);
                    textBox2.Text = string.Format("{0} - {1} - {2}", log.Status.Trim(), log.Local.Trim(), log.Data);
                }
                catch
                {
                    MessageBox.Show(string.Format("Falha ao consultar a postagem {0}!", textBox1.Text));
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<reqPeca> lista = reqPeca.listar();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = lista.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            foreach (reqPeca req in lista)
            {
                //CSF_Correios.Eventos evCorreios = CSF_Correios.Eventos.Rastrear(req.Postagem);
                Postagens.logPostgem log = Postagens.html.Rastrear(req.Postagem);

                if (log.Status != null && log.Status != "")
                {
                    req.AtualizarStatus(log.Status);

                    if (log.Status.Contains("Entrega Efetuada"))
                    {
                        //req.InformarUsuario(evCorreios.Descricao);
                        //MessageBox.Show(string.Format("Peça Entregue! ({0})", req.Postagem));
                        req.Entregue(log.Data);
                    }
                }
                progressBar1.PerformStep();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<reqSuprimento> lista = reqSuprimento.Listar();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = lista.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            foreach (reqSuprimento req in lista)
            {
                Postagens.logPostgem log = Postagens.html.Rastrear(req.Postagem);
                if (log.Status != null && log.Status != "")
                {
                    if (req.AtualizarStatus(log.Status))
                    {
                        //Console.WriteLine(string.Format("Postagem {0} atualizada!", req.Postagem));
                    }
                    else
                    {
                        //Console.WriteLine(string.Format("Falha ao atualizar a postagem {0}!", req.Postagem));
                    }
                    req.AtualizarPrazos();

                    //if (req.AtualizarStatus("Entrega Efetuada"))
                    //{
                    //    if (req.InformarUsuario(log.Status))
                    //    {
                    //        //Console.WriteLine(string.Format("Usuário informado. Postagem: {0}!", req.Postagem));
                    //    }
                    //    else
                    //    {
                    //        //Console.WriteLine(string.Format("Falha ao informar o usuário da postagem: {0}!", req.Postagem));
                    //    }
                    //}
                }
                progressBar1.PerformStep();
            }
        }

        private void btPrazo_Click(object sender, EventArgs e)
        {
            #region Antigo

            if (textBox1.Text != "")
            {
                logPostgem log = null;

                try
                {
                    log = new logPostgem(textBox1.Text.Trim());

                }
                catch
                {
                    return;
                }

                CSF_Correios.Eventos.servicoCorreios tpServico;
                if (log.TpEnvio.Contains("PAC") && !log.TpEnvio.Contains("REVERSO"))
                {
                    tpServico = Eventos.servicoCorreios.pac;
                }
                else if (log.TpEnvio.Contains("SEDEX") && !log.TpEnvio.Contains("REVERSO"))
                {
                    tpServico = Eventos.servicoCorreios.sedex;
                }
                else
                {
                    tpServico = Eventos.servicoCorreios.pac;
                }
               
                
                int prazo = CSF_Correios.Eventos.CalculaPrazo(tpServico, tbcepOrigem.Text, tbcepDestino.Text);
                textBox2.Text = string.Format("Prazo de enrega = {0} dias.", prazo.ToString());
            }
            else
            {
                List<EntregaSuprimentos> lista = EntregaSuprimentos.listar();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = lista.Count;
                progressBar1.Step = 1;
                progressBar1.Value = 0;

                foreach (EntregaSuprimentos entrega in lista)
                {
                    logPostgem log = null;

                    try
                    {
                        log = new logPostgem(entrega.Postagem);
                        CSF_Correios.Eventos.servicoCorreios tpServico;
                        if (log.TpEnvio.Contains("PAC") && !log.TpEnvio.Contains("REVERSO"))
                        {
                            tpServico = Eventos.servicoCorreios.pac;
                        }
                        else if (log.TpEnvio.Contains("SEDEX") && !log.TpEnvio.Contains("REVERSO"))
                        {
                            tpServico = Eventos.servicoCorreios.sedex;
                        }
                        else
                        {
                            tpServico = Eventos.servicoCorreios.pac;
                        }


                        if (log.CepDestino != null)
                        {
                            int prazo1 = CSF_Correios.Eventos.CalculaPrazo(tpServico, "65175175", log.CepDestino);
                            entrega.AtualizarPrazo(prazo1);
                        }
                       
                    }
                    catch
                    {
                    }

                   
                    progressBar1.PerformStep();
                }
            }
            #endregion

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Postagens.logPostgem log = new Postagens.logPostgem(textBox1.Text);
                //Eventos evCorreios = Eventos.Rastrear(textBox1.Text);
                if (log.Status.Contains("Objeto entregue") || log.Status.ToLower().Contains("aguardando retirada"))
                {
                    //reqPeca.Entregue(evCorreios.Data);
                    reqSuprimento.Entregue(log.Data, textBox1.Text);
                }
            }
            else
            {
                
                List<string> listaPostagens = reqSuprimento.ListarPostagensEntregues();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = listaPostagens.Count;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                foreach (string postagem in listaPostagens)
                {
                    Postagens.logPostgem log = null;
                    try
                    {
                        log = new Postagens.logPostgem(postagem);
                    }
                    catch
                    {

                    }

                    if (log != null)
                    {
                        if (log.Status.Contains("Objeto entregue") || log.Status.ToLower().Contains("aguardando retirada"))
                        {
                            //reqPeca.Entregue(evCorreios.Data);
                            reqSuprimento.Entregue(log.Data, postagem);
                        }
                    }
                    progressBar1.PerformStep();
                }
            }
            
        }

        private void btCeps_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                List<CepEqpto> lista = CepEqpto.Listar();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = lista.Count;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                foreach (CepEqpto cep in lista)
                {
                    cep.Validar(cep.Valido());
                    progressBar1.PerformStep();
                }
                MessageBox.Show("Processo concluído!");
            }
            else
            {
                if (CSF_Correios.Eventos.cepValido(textBox1.Text))
                {
                    MessageBox.Show("Cep válido!");
                }
                else
                {
                    MessageBox.Show("Cep inválido!");
                }
            }
           
        }

        private void btReversos_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Eventos evCorreios = Eventos.Rastrear(textBox1.Text);
                if (evCorreios.Descricao.Contains("Objeto entregue") || evCorreios.Descricao.Contains("Objeto aguardando retirada no endereço indicado"))
                {
                    //reqPeca.Entregue(evCorreios.Data);
                    //reqSuprimento.Entregue(evCorreios.Data, textBox1.Text);
                    PostagensReversas.Entregue(evCorreios.Data, textBox1.Text, evCorreios.Descricao);
                }
            }
            else
            {

                List<PostagensReversas> listaPostagens = PostagensReversas.Listar();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = listaPostagens.Count;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                foreach (PostagensReversas postagem in listaPostagens)
                {
                    if (postagem.PrazoEntrega == "")
                    {
                        postagem.CalcularPrazo();
                    }

                    Eventos evCorreios = Eventos.Rastrear(postagem.Postagem);
                    if (evCorreios != null)
                    {
                        if (evCorreios.Descricao.Contains("Objeto entregue") || evCorreios.Descricao.Contains("Objeto aguardando retirada no endereço indicado"))
                        {
                            //reqPeca.Entregue(evCorreios.Data);
                            PostagensReversas.Entregue(evCorreios.Data, postagem.Postagem, evCorreios.Descricao);
                        }
                    }
                    try
                    {
                        if (int.Parse(postagem.EntregueEm) <= int.Parse(postagem.PrazoEntrega))
                        {
                            postagem.Checado();
                        }
                    }
                    catch
                    { }
                    progressBar1.PerformStep();
                }
            }
        }
    }
}