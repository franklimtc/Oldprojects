using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using dnaPrint.Base;

namespace dnaPrint.Service
{
    public class Operacoes
    {
        static string connString = ConfigurationManager.ConnectionStrings["dnaPrint"].ToString();
        static DAO.Operacoes.tipo tpDB = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());
        static string key = "317498F1EB83DFA4C3D999F5C1D4A5B591AAAAD0BFDFE6939E6B6E5F26036E3B";

        public static void EfetuarLeitura()
        {
            bool continuar = false;
            if (ConfigurationManager.AppSettings["tipoAgente"].ToString() == "Distribuido")
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
                                //eqp.Oids = OID.Listar(eqp.Descricao, connString, tpDB);
                                ws.OperacoesClient op = new ws.OperacoesClient();
                                eqp.Oids = new List<OID>();
                                // #########CONTINUAR AQUI######!!

                                //var listaOids = op.ListarOids("317498F1EB83DFA4C3D999F5C1D4A5B591AAAAD0BFDFE6939E6B6E5F26036E3B").ToList();
                                eqp.Oids = op.ListarOidsParcial(key, eqp.Descricao).ToList();

                                // #########CONTINUAR AQUI######!!

                                if (eqp.Oids.Count > 0)
                                {
                                    foreach (var disp in eqp.Oids)
                                    {
                                        disp.Valor = SNMP.SNMP.Get(disp.Oid, eqp.IP);
                                    }

                                    if (eqp.DisparoValido())
                                    {
                                        eqp.Serie = eqp.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString();

                                        int idEqptoTemp = 0;

                                        if (int.TryParse(op.RetornaValor(key, $"select idequipamento from cadastroequipamentos where serie = '{eqp.Serie}' and status = '1'"), out idEqptoTemp))
                                        {
                                            eqp.idEquipamento = idEqptoTemp;
                                            string tsqlInsert = eqp.GerarInsert("dadosdisparos");
                                            List<string[]> parametros = eqp.GerarParametros();
                                            bool resultInsert = op.Exec(key, tsqlInsert, parametros.ToArray());
                                            if (resultInsert)
                                            {
                                                // Adicionar Controle de Suprimentos
                                            }
                                        }

                                        
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

                //List<Equipamento> lista = new List<Equipamento>();
                //Equipamento eqp1 = new Equipamento();
                //eqp1.IP = "172.25.128.146";
                //lista.Add(eqp1);

                if (lista.Count > 0)
                {
                    foreach (Equipamento eqp in lista)
                    {
                        try
                        {
                            //if (eqp.IP == "172.25.128.5")
                            //{
                            //    bool result = true;
                            //}
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

        public static void EfetuarLeitura(List<Equipamento> lista)
        {
            bool continuar = false;
            if (ConfigurationManager.AppSettings["tipoAgente"].ToString() == "Distribuido")
            {
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
                                //eqp.Oids = OID.Listar(eqp.Descricao, connString, tpDB);
                                ws.OperacoesClient op = new ws.OperacoesClient();
                                eqp.Oids = new List<OID>();
                                // #########CONTINUAR AQUI######!!

                                //var listaOids = op.ListarOids("317498F1EB83DFA4C3D999F5C1D4A5B591AAAAD0BFDFE6939E6B6E5F26036E3B").ToList();
                                eqp.Oids = op.ListarOidsParcial(key, eqp.Descricao).ToList();

                                // #########CONTINUAR AQUI######!!

                                if (eqp.Oids.Count > 0)
                                {
                                    foreach (var disp in eqp.Oids)
                                    {
                                        disp.Valor = SNMP.SNMP.Get(disp.Oid, eqp.IP);
                                    }

                                    if (eqp.DisparoValido())
                                    {
                                        eqp.Serie = eqp.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString();

                                        int idEqptoTemp = 0;

                                        if (int.TryParse(op.RetornaValor(key, $"select idequipamento from cadastroequipamentos where serie = '{eqp.Serie}' and status = '1'"), out idEqptoTemp))
                                        {
                                            eqp.idEquipamento = idEqptoTemp;
                                            string tsqlInsert = eqp.GerarInsert("dadosdisparos");
                                            List<string[]> parametros = eqp.GerarParametros();
                                            bool resultInsert = op.Exec(key, tsqlInsert, parametros.ToArray());
                                            if (resultInsert)
                                            {
                                                // Adicionar Controle de Suprimentos
                                            }
                                        }


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
                if (lista.Count > 0)
                {
                    foreach (Equipamento eqp in lista)
                    {
                        try
                        {
                            eqp.Descricao = SNMP.SNMP.Get(".1.3.6.1.2.1.1.1.0", eqp.IP);
                            eqp.Oids = OID.Listar(eqp.Descricao, connString, tpDB);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Falha na leitura do equipamento de ip {eqp.IP}!");
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
                                    Console.WriteLine($"Leitura efetuada para o equipamento de serie {eqp.Serie}!");

                                    //Armazenar localmente o disparo

                                    eqp.SalvarDisparo(tpDB, connString);

                                    // Atualiza Controle de Suprimentos
                                    AtualizarControleSuprimentos(eqp);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Nenhuma oid encontrada para o equipamento {eqp.IP} - Descr: {eqp.Descricao}!");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Nenhuma oid encontrada para o equipamento {eqp.IP} - Descr: {eqp.Descricao}!");
                        }
                    }
                }
            }
        }

        public static void EfetuarLeituraRapida()
        {
            List<Equipamento> lista = Equipamento.Listar(Equipamento.Tipo.Rede, tpDB, connString);

            if (lista.Count > 0)
            { 
                foreach (Equipamento eqp in lista)
                {
                    DisparoSNMP disp = new DisparoSNMP(eqp);
                    Thread t = new Thread(new ThreadStart(disp.Disparar));
                    t.Start();
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
