using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using dnaPrint.Base;

namespace dnaPrint.Service
{
    public class Operacoes
    {
        static string connString = ConfigurationManager.ConnectionStrings["db"].ToString();
        //static DAO.Operacoes.tipo tpDB = DAO.Operacoes.DefinirTipo(config.Verificar("tipoBanco"));
        static DAO.Operacoes.tipo tpDB = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());

        public static void EfetuarLeitura()
        {
            bool continuar = false;
            if (ConfigurationManager.AppSettings["tipoAgente"].ToString() == "distribuindo")
            {
                List<Equipamento> lista = Equipamento.Listar(Equipamento.Tipo.Local, tpDB, connString);

                if (lista.Count > 0)
                {
                    foreach (Equipamento eqp in lista)
                    {
                        if (SNMP.SNMP.OnLine(eqp.IP))
                        {
                            #region Disparo

                            try
                            {
                                eqp.Descricao = SNMP.SNMP.Get(".1.3.6.1.2.1.1.1.0", eqp.IP);
                                continuar = true;
                            }
                            catch (Exception ex)
                            {
                                //Log.Log.Adicionar(connString, Log.Log.Tipo.Erro, ex.Message);
                            }

                            if (continuar)
                            {
                                eqp.Oids = OID.Listar(eqp.Descricao, connString, tpDB);

                                if (eqp.Oids.Count > 0)
                                {
                                    foreach (var disp in eqp.Oids)
                                    {
                                        disp.Valor = SNMP.SNMP.Get(disp.Oid, eqp.IP);
                                    }

                                    if (eqp.DisparoValido())
                                    {
                                        eqp.Serie = eqp.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString();

                                        //Armazenar localmente o disparo

                                        eqp.AdicionarBaseLocal(connString);
                                        eqp.SalvarDisparo(tpDB, connString);

                                        // Atualiza Controle de Suprimentos
                                        AtualizarControleSuprimentos(eqp);
                                    }
                                }
                            }
                            #endregion
                        }

                    }
                }
            }
            else
            {
                List<Equipamento> lista = Equipamento.Listar(Equipamento.Tipo.Rede, tpDB, connString);
                if (lista.Count > 0)
                {
                    foreach (Equipamento eqp in lista)
                    {
                        try
                        {
                            if (eqp.IP == "172.25.129.22")
                            {
                                bool result = true;
                            }
                            eqp.Descricao = SNMP.SNMP.Get(".1.3.6.1.2.1.1.1.0", eqp.IP);
                            eqp.Oids = OID.Listar(eqp.Descricao, connString, tpDB);
                        }
                        catch 
                        {
                        }
                        
                        if (eqp.Oids != null)
                        {
                            if (eqp.Oids.Count > 0)
                            {
                                foreach (var disp in eqp.Oids)
                                {
                                    disp.Valor = SNMP.SNMP.Get(disp.Oid, eqp.IP);
                                }

                                if (eqp.DisparoValido())
                                {
                                    eqp.Serie = eqp.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString().Trim();

                                    //Armazenar localmente o disparo

                                    eqp.SalvarDisparo(tpDB, connString);

                                    // Atualiza Controle de Suprimentos
                                    AtualizarControleSuprimentos(eqp);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void AtualizarControleSuprimentos(Equipamento eqp)
        {
            VolumeMensal volume = new VolumeMensal(eqp.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString(), tpDB, connString);
            int totalmono = 0;
            int totalcolor = 0;

            if (eqp.Oids.Find(x => x.Propriedade == "serialtoner") != null)
            {
                controleSuprimento controleToner = controleSuprimento.BuscarPorSerial(connString, tpDB, eqp.Oids.Where(x => x.Propriedade.ToLower() == "serialtoner").First().Valor.ToString(), eqp.Serie);

                if (controleToner == null)
                {
                    int int_toner_total_pr = 0;
                    int int_toner_atual_pr = 0;
                    bool continuar = false;

                    if (volume != null)
                    {
                        if (int.TryParse(eqp.Oids.Where(x => x.Propriedade == "toner_total_pr").First().Valor.ToString(), out int_toner_total_pr))
                        {
                            if (int.TryParse(eqp.Oids.Where(x => x.Propriedade == "toner_atual_pr").First().Valor.ToString(), out int_toner_atual_pr))
                            {
                                if (volume.MediaDia > 0)
                                {
                                    continuar = true;
                                }

                            }
                        }
                    }

                    if (continuar)
                    {
                        controleToner = new controleSuprimento(controleSuprimento.TipoSuprimento.Toner
                         , eqp.Oids.Where(x => x.Propriedade.ToLower() == "serialtoner").First().Valor.ToString()
                         , volume.ContFinal
                         , int_toner_total_pr
                         , int_toner_atual_pr
                         , volume.MediaDia);
                        controleToner.serie = eqp.Serie;
                        controleToner.Excluir(connString, tpDB);

                        controleToner.Adicionar(connString, tpDB);
                        controleToner.DuracaoEstimada = controleToner.SuprimentoAtual / volume.MediaDia;
                    }

                    //controleToner = controleSuprimento.BuscarPorSerial(connString, DataBase.tipo.SQLServer, controleToner.Serial);

                }
                else
                {
                    int int_toner_atual_pr = 0;
                    bool continuar = false;

                    if (volume != null)
                    {
                        {
                            if (int.TryParse(eqp.Oids.Where(x => x.Propriedade == "toner_atual_pr").First().Valor.ToString(), out int_toner_atual_pr))
                            {
                                if (volume.MediaDia > 0)
                                {
                                    continuar = true;
                                }
                            }
                        }
                    }

                    if (continuar)
                    {
                        controleToner.ContFinal = volume.ContFinal;
                        controleToner.DtFinal = DateTime.Now;
                        controleToner.SuprimentoAtual = int_toner_atual_pr;
                        controleToner.MediaDiaria = volume.MediaDia;
                        controleToner.serie = eqp.Serie;

                        controleToner.Atualizar(connString, tpDB);
                    }
                }
            }

            if (eqp.Oids.Find(x => x.Propriedade.ToLower() == "serialfoto") != null)
            {
                controleSuprimento controleCilindro = controleSuprimento.BuscarPorSerial(connString, tpDB, eqp.Oids.Where(x => x.Propriedade.ToLower() == "serialfoto").First().Valor.ToString(), eqp.Serie);
                if (controleCilindro == null)
                {
                    int int_cilindro_total = 0;
                    int int_cilindro_atual = 0;
                    bool continuar = false;

                    if (volume != null)
                    {
                        if (int.TryParse(eqp.Oids.Where(x => x.Propriedade == "cilindro_total").First().Valor.ToString(), out int_cilindro_total))
                        {
                            if (int.TryParse(eqp.Oids.Where(x => x.Propriedade == "cilindro_atual").First().Valor.ToString(), out int_cilindro_atual))
                            {
                                if (volume.MediaDia > 0)
                                {
                                    continuar = true;
                                }
                            }
                        }
                    }

                    if (continuar)
                    {

                        controleCilindro = new controleSuprimento(controleSuprimento.TipoSuprimento.Cilindro
                         , eqp.Oids.Where(x => x.Propriedade.ToLower() == "serialfoto").First().Valor.ToString()
                         , volume.ContFinal
                         , int_cilindro_total
                         , int_cilindro_atual
                         , volume.MediaDia);
                        controleCilindro.Excluir(connString, tpDB);
                        controleCilindro.serie = eqp.Serie;

                        controleCilindro.Adicionar(connString, tpDB);
                        //controleCilindro = controleSuprimento.BuscarPorSerial(connString, DataBase.tipo.SQLServer, controleCilindro.Serial);
                        controleCilindro.DuracaoEstimada = controleCilindro.SuprimentoAtual / controleCilindro.MediaDiaria;
                    }
                }
                else
                {
                    int int_total_pf_mono_simples = 0;
                    int int_cilindro_atual = 0;
                    bool continuar = false;

                    if (volume != null)
                    {
                        if (int.TryParse(eqp.Oids.Where(x => x.Propriedade == "cilindro_atual").First().Valor.ToString(), out int_cilindro_atual))
                        {
                            if (volume.MediaDia > 0)
                            {
                                continuar = true;
                            }
                        }
                    }
                    if (continuar)
                    {
                        controleCilindro.ContFinal = int_total_pf_mono_simples;
                        controleCilindro.DtFinal = DateTime.Now;
                        controleCilindro.SuprimentoAtual = int_cilindro_atual;
                        controleCilindro.MediaDiaria = volume.MediaDia;
                        controleCilindro.serie = eqp.Serie;

                        controleCilindro.Atualizar(connString, tpDB);
                    }
                }
            }
        }
    }
}
