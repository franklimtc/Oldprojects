using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using dnaPrint.Base;
using System.Diagnostics;
using System.IO;

namespace dnaPrint.Config
{
    public partial class Inicio : Form
    {
        Timer myTimer = new Timer();
        Equipamento eqpto;
        static string connString = ConfigurationManager.ConnectionStrings["db"].ToString();

        public Inicio()
        {
            InitializeComponent();

            myTimer.Tick += new EventHandler(EfetuarDisparo);
            myTimer.Interval = 10000;
            myTimer.Start();
        }

        private void EfetuarDisparo(object sender, EventArgs e)
        {
            myTimer.Interval = 60000;

            bool ativo = false;

            if (config.Verificar(Agente.Configuracoes.StatusAtivacao.ToString()).Equals(Agente.Status.Ativo.ToString()))
            {
                DateTime dtAtivacao;
                if (DateTime.TryParse(config.Verificar(Agente.Configuracoes.DataAtivacao.ToString()), out dtAtivacao))
                {
                    if (DateTime.Now.Subtract(dtAtivacao).Days > 30)
                    {
                        DesativarAgente();
                    }
                    else
                    {
                        ativo = true;
                    }
                }
            }


            if (CarregarInformacoes())
            {
                if (ativo)
                {
                    //if (eqpto.DisparoValido())
                    //{
                    //    if (eqpto.SalvarDisparo(DataBase.tipo.SQLite, connString))
                    //    {

                    //        if (SalvarDisparoWS(eqpto))
                    //        {
                    //            popup = new Notificacao("Info", $"Ïnformações da impressora de série {eqpto.Oids.Find(x => x.Propriedade == "serie").Valor} atualizadas com sucesso!");
                    //        }
                    //        else
                    //        {
                    //            popup = new Notificacao("Falha", $"Falha ao tentar enviar as informações da impressora!");

                    //        }
                    //    }
                    //    else
                    //    {
                    //        popup = new Notificacao("Falha", $"Falha ao tentar ler as informações da impressora!");
                    //    }
                    //    popup.Exibir();
                    //}
                }
                else
                {
                    if (AtivarAgente(eqpto.Oids.Where(x => x.Propriedade == "serie").First().Valor))
                        Notificacao.Exibir("Info", "Solução ativada com sucesso!");
                    else
                        Notificacao.Exibir("Falha", "Falha na ativação da solução! Contacte a CSF Digital informando sobre a falha");
                }
            }
        }

        private bool AtivarAgente(object serie)
        {
            bool result = false;

            using (dnaPrintWSAgente.AgenteClient agenteCliente = new dnaPrintWSAgente.AgenteClient())
            {
                string key = "CsfDigit@l2016";
                result = agenteCliente.Adicionar(key, Environment.MachineName, serie.ToString());
            }

            if (result)
            {
                DAO.SQLServer sql = new DAO.SQLServer();
                string tsqlUpdte = $"update config set valor = GETDATE() where configuracao = 'DataAtivacao';";
                sql.ExecuteNonQuery(connString, tsqlUpdte);

                tsqlUpdte = $"update config set valor = '{Agente.Status.Ativo.ToString()}' where configuracao = '{Agente.Configuracoes.StatusAtivacao.ToString()}'";
                sql.ExecuteNonQuery(connString, tsqlUpdte);
            }
            else
            {
                //Log.Log.Adicionar(connString, Log.Log.Tipo.Erro, $"Não foi possível ativar o equipamento de série {serie}!");
                Notificacao.Exibir("Erro", $"Não foi possível ativar o equipamento de série {serie}!");
            }
            return result;
        }

        private static bool DesativarAgente()
        {
            DAO.SQLServer sql = new DAO.SQLServer();
            string tsqlUpdateStatus = $"update config set valor = '{Agente.Status.Inativo.ToString()}' where configuracao = 'StatusAticacao';";

            if (sql.ExecuteNonQuery(connString, tsqlUpdateStatus) > 0)
                return true;
            else
                return false;
        }
    
        private bool CarregarInformacoes()
        {
            eqpto = null;

            bool continuar = false;
            if (config.Verificar("tipoAgente") == "local")
            {
                DAO.Operacoes.tipo tpDB = DAO.Operacoes.tipo.SQLite;
                connString = $"Data Source={Directory.GetCurrentDirectory()}\\App.s3db;Version=3;";

                List<Equipamento> lista = Equipamento.Listar(Equipamento.Tipo.Local, tpDB, connString);

                if (lista.Count >0)
                {
                    foreach (Equipamento eqp in lista)
                    {
                        continuar = false;

                        if (SNMP.SNMP.OnLine(eqp.IP))
                        {
                            continuar = true;
                        }
                        else
                        {
                            Notificacao.Exibir("Equipamento inacessível!", $"Verifique se a impressora de IP {eqp.IP} encontra-se ligada com o cabo de rede conectado!");
                        }
                        if (continuar)
                        {
                            try
                            {
                                eqp.Descricao = SNMP.SNMP.Get(".1.3.6.1.2.1.1.1.0", eqp.IP);
                                continuar = true;
                            }
                            catch (Exception)
                            {
                                lbErro.Text = "Não foi possível conectar ao equipamento!";
                                lbErro.Visible = true;
                                lbCilindro.Visible = false;
                                lbToner.Visible = false;
                                groupBox2.Visible = false;
                                //MessageBox.Show("Não foi possível conectar ao equipamento!");
                            }
                        }
                       
                        if (continuar)
                        {
                            continuar = false;
                            lbErro.Visible = !true;
                            lbCilindro.Visible = !false;
                            lbToner.Visible = !false;
                            groupBox2.Visible = !false;

                            eqp.Oids = OID.Listar(eqp.Descricao, connString, tpDB);

                            foreach (var disp in eqp.Oids)
                            {
                                disp.Valor = SNMP.SNMP.Get(disp.Oid, eqp.IP);
                            }

                            if (eqp.DisparoValido())
                            {
                                //tabControl1.Visible = true;
                                eqp.AdicionarBaseLocal(connString);
                                DAO.Operacoes.tipo dbTipo = DAO.Operacoes.tipo.SQLite;

                                eqp.SalvarDisparo(dbTipo, connString);
                                VolumeMensal volume = new VolumeMensal(eqp.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString(), dbTipo, connString);
                                controleSuprimento controleToner = controleSuprimento.BuscarPorSerial(connString, dbTipo, eqp.Oids.Where(x => x.Propriedade == "serialToner").First().Valor.ToString());
                                controleSuprimento controleCilindro = controleSuprimento.BuscarPorSerial(connString, dbTipo, eqp.Oids.Where(x => x.Propriedade == "serialFoto").First().Valor.ToString());

                                if (controleToner == null)
                                {
                                    controleToner = new controleSuprimento(controleSuprimento.TipoSuprimento.Toner
                                         , eqp.Oids.Where(x => x.Propriedade == "serialToner").First().Valor.ToString()
                                         , int.Parse(eqp.Oids.Where(x => x.Propriedade == "total_pf_mono_simples").First().Valor.ToString())
                                         , int.Parse(eqp.Oids.Where(x => x.Propriedade == "toner_total_pr").First().Valor.ToString())
                                         , int.Parse(eqp.Oids.Where(x => x.Propriedade == "toner_atual_pr").First().Valor.ToString())
                                         , volume.MediaDia);
                                    controleToner.Adicionar(connString, dbTipo);
                                    //controleToner.DuracaoEstimada = controleToner.SuprimentoAtual / controleToner.MediaDiaria;
                                }

                                if (controleCilindro == null)
                                {
                                    controleCilindro = new controleSuprimento(controleSuprimento.TipoSuprimento.Cilindro
                                         , eqp.Oids.Where(x => x.Propriedade == "serialFoto").First().Valor.ToString()
                                         , int.Parse(eqp.Oids.Where(x => x.Propriedade == "total_pf_mono_simples").First().Valor.ToString())
                                         , int.Parse(eqp.Oids.Where(x => x.Propriedade == "cilindro_total").First().Valor.ToString())
                                         , int.Parse(eqp.Oids.Where(x => x.Propriedade == "cilindro_atual").First().Valor.ToString())
                                         , volume.MediaDia);
                                    controleCilindro.Adicionar(connString, dbTipo);
                                    //controleCilindro.DuracaoEstimada = controleCilindro.SuprimentoAtual / controleCilindro.MediaDiaria;
                                }
                                continuar = true;
                            }
                        }

                        if (continuar)
                        {
                            if (SalvarDisparoWS(eqp))
                            {
                                Notificacao.Exibir("Info", $"Ïnformações da impressora de série {eqp.Oids.Find(x => x.Propriedade == "serie").Valor} atualizadas com sucesso!");
                                continuar = true;
                            }
                            else
                            {
                                Notificacao.Exibir("Falha", $"Falha ao tentar enviar as informações da impressora de ip {eqp.IP}!");
                                continuar = false;
                            }
                            eqpto = eqp;
                        }
                    }
                    //Equipamento eqp = lista[0];
                }
            }
            return continuar;
        }

        private void btEntrar_Click(object sender, EventArgs e)
        {
            using (Context ctx = new Context())
            {
                List<config> lista = ctx.config.ToList();
                config userAdmin = lista.Where(x => x.configuracao == "admin").First();
                config userAdminPassword = lista.Where(x => x.configuracao == "adminPassword").First();

                if (userAdmin.valor == tbUser.Text && userAdminPassword.valor == tbSenha.Text)
                {
                    AbrirConfiguration();
                }
                else
                {
                    lbMessage.Text = "**Usuário ou senha inválido!";
                    lbMessage.Visible = true;
                }
            }
        }

        public void AbrirConfiguration()
        {
            this.Hide();
            Form f = new Configuration();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CarregarInformacoes())
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                //MessageBox.Show("Informações atualizadas com sucesso!", "Sucesso", MessageBoxButtons.OK);
                Notificacao.Exibir("Sucesso", "Informações atualizadas com sucesso!");

            }
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                //MessageBox.Show("Falha ao tentar atualizar as informações!", "Falha", MessageBoxButtons.OK);
                Notificacao.Exibir("Falha", "Falha ao tentar atualizar as informações!");
            }
        }

        private void enviarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (eqpto.DisparoValido())
            {
                //eqpto.SalvarDisparo(DataBase.tipo.SQLServer, ConfigurationManager.ConnectionStrings["db"].ToString());
                SalvarDisparoWS(eqpto);
            }
        }

        private bool SalvarDisparoWS(Equipamento eqpto)
        {
            string key = "CsfDigit@l2016";
            bool result = false;
            using (dnaPrintWS.ExecuteClient client = new dnaPrintWS.ExecuteClient())
            {

                string query = $"select idEquipamento from cadastroequipamentos where serie = '{eqpto.Oids.Find(x => x.Propriedade == "serie").Valor.ToString()}' ";
                string queryInsert = null;
                List<string[]> parametros = new List<string[]>();
                // 1 - Validação de série
                int intTemp = 0;
                int.TryParse(client.RetornaValor(key, query), out intTemp);


                if (intTemp > 0)
                {
                    //Série está cadastrada
                    eqpto.idEquipamento = intTemp;
                    intTemp = 0;
                    query = $"SELECT MAX(TOTAL_PF_MONO_SIMPLES) FROM DadosDisparos WHERE idEquipamento = {eqpto.idEquipamento}";
                    int.TryParse(client.RetornaValor(key, query), out intTemp);
                    int intTempContador = 0;
                    int.TryParse(eqpto.Oids.Find(x => x.Propriedade == "total_pf_mono_simples").Valor.ToString(), out intTempContador);

                    if (intTemp <= intTempContador)
                    {
                        //Contador atual MAIOR do que o último contador cadastrado
                        queryInsert = eqpto.GerarInsert("DadosDisparos");
                    }
                    else
                    {
                        //Contador atual MENOR do que o último contador cadastrado
                        queryInsert = eqpto.GerarInsert("DadosDisparosSuspeitos");
                    }
                    parametros = eqpto.GerarParametros();
                }
                else
                {
                    // Série não está cadastrada
                    // Inserir disparos na tabela DadosDisparosErros
                    eqpto.idEquipamento = 0;
                    queryInsert = eqpto.GerarInsert("DadosDisparosErros");
                    parametros = eqpto.GerarParametros();
                }

                if (client.Exec(key, queryInsert, parametros.ToArray()))
                    //MessageBox.Show("Informações enviadas com sucesso!");
                    result = true;
                return result;
            }
        }

        private void postagensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (eqpto != null)
            {
                string serie = eqpto.Oids.Find(x => x.Propriedade == "serie").Valor.ToString().Trim();

                string url = config.Verificar("urlHistoricoEnvios") + $"/?serie={serie}";
                Process.Start("http://" + url);
            }
            else
            {
                MessageBox.Show("Atualize as informações do equipamento!","ERRO!!");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void Inicio_Move(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                //CarregarInformacoesBD();
                this.Show();
            }
        }

        private void CarregarInformacoesBD()
        {
            DAO.Operacoes.tipo tipoDB = DAO.Operacoes.tipo.SQLite;
            connString = $"Data Source={Directory.GetCurrentDirectory()}\\App.s3db;Version=3;";
            List<controleSuprimento> cAtual = controleSuprimento.ListarEmUso(connString, tipoDB);

            if (cAtual.Count > 0)
            {
                Equipamento eqp = Equipamento.Listar(Equipamento.Tipo.Rede, tipoDB, connString).FirstOrDefault();
                VolumeMensal volume = new VolumeMensal(eqp.Serie, tipoDB, connString);
                controleSuprimento controleToner = cAtual.Where(x => x.Tipo == controleSuprimento.TipoSuprimento.Toner).First();
                controleSuprimento controleCilindro = cAtual.Where(x => x.Tipo == controleSuprimento.TipoSuprimento.Cilindro).First();

                lbSerialToner.Text = controleToner.Serial.Replace("Black Toner Cartridge S/N:", "");
                lbSerialCilindro.Text = controleCilindro.Serial.Replace("Black Imaging Unit S/N:", "");
                lbTotal.Text = volume.ContFinal.ToString();
                lbTotalMes.Text = volume.Volume.ToString();

                if (controleToner.SuprimentoMedio == 0)
                    lbDuracaoToner.Text = $"{controleToner.DuracaoEstimada} dias";
                else
                    lbDuracaoToner.Text = $"{((controleToner.SuprimentoValor / 100) * controleToner.SuprimentoMedio) / controleToner.MediaDiaria } dias";

                if (controleCilindro.SuprimentoMedio == 0)
                    lbDuracaoCilindro.Text = $"{controleCilindro.DuracaoEstimada} dias";
                else
                    lbDuracaoCilindro.Text = $"{((controleCilindro.SuprimentoValor / 100) * controleCilindro.SuprimentoMedio) / controleCilindro.MediaDiaria } dias";

                if (controleToner.SuprimentoValor < 20)
                    lbToner.ForeColor = Color.Red;
                else
                    lbToner.ForeColor = Color.Green;

                if (controleCilindro.SuprimentoValor < 20)
                    lbCilindro.ForeColor = Color.Red;
                else
                    lbCilindro.ForeColor = Color.Green;

                lbToner.Text = $"{controleToner.SuprimentoValor.ToString()}%";
                lbCilindro.Text = $"{controleCilindro.SuprimentoValor.ToString()}%";

                AtivarLabels();
            }
        }

        private void AtivarLabels()
        {
            lbCilindro.Visible = true;
            lbToner.Visible = true;
            groupBox2.Visible = true;
            groupBox1.Visible = true;
        }
    }
}
