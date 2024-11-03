using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class Preventivos : Form
    {
        public Preventivos()
        {
            InitializeComponent();
        }

        private void btSelecionar_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            tbFile.Text = openFileDialog1.FileName;
        }

        private void btSolicitar_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            

            List<RegistroSuprimento> lista = null;
            if (tbFile.Text != "")
            {
                if (File.Exists(tbFile.Text))
                {
                    lista = RegistroSuprimento.Listar(tbFile.Text);
                }
            }
            else
            {
                MessageBox.Show("Selecionar o arquivo csv primeiro!");
            }

            progressBar1.Maximum = lista.Count;
            Suprimentos.Suprimentos req = new Suprimentos.Suprimentos();

            int qtdToner = 0;
            int qtdCilindro = 0;
            foreach (RegistroSuprimento reg in lista)
            {
                DateTime data;

                DateTime.TryParse(reg.DataUltimaLeitura, out data);
                if (!reg.Isnull())
                {
                    if (reg.Unidade.ToLower().Trim() != "capgv")
                    {
                        if (DateTime.Now.Subtract(data) < TimeSpan.FromDays(3))
                        {
                            if (reg.SugestaoCilindro == "Enviar")
                            {
                                if (reg.CilindroSerial != "")
                                {
                                    bool result = req.Solicitar(reg.Serie, "Cilindro", reg.Cilindro, reg.CilindroEstimativaDias, reg.ContadorAtual);
                                    if (result)
                                        qtdCilindro++;
                                }
                            }

                            if (reg.SugestaoToner == "Enviar")
                            {
                                if(reg.TonerSerial != "")
                                {
                                    bool result = req.Solicitar(reg.Serie, "Tonner", reg.Toner, reg.TonerEstimativaDias, reg.ContadorAtual);
                                    if (result)
                                        qtdToner++;
                                }
                            }
                        }
                    }
                }
                progressBar1.PerformStep();
            }

            string msg = string.Format("Foram solicitados {0} tonners e {1} cilindros!", qtdToner.ToString(), qtdCilindro.ToString());
            MessageBox.Show(msg);
        }
    }
}
