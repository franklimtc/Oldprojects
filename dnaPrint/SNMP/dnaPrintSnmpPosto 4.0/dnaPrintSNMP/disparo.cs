using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Net;

namespace dnaPrintSNMP
{
    public static class disparo
    {
        static int[] qtdEquipamentosLidos = new int[3];
        static List<List<equipamento>> listaEquipamentosGeral = new List<List<equipamento>>();
        public static void iniciar()
        {
            parametros par = new parametros();
            string tpAgente = parametros.retornaParametro("agente");
            List<equipamento> lEquipamentos = new List<equipamento>();

            switch (tpAgente)
            {
                case "local":
                    //lEquipamentos = equipamento.retornaEquipamentos();
                    string tsql = "select idEquipamento, ip from cadastroEquipamentos";

                    DataTable dtEquipamentos = DAO.RetornaDt(Util.Descriptografar(parametros.retornaParametro("connectionString")), tsql);
                    List<equipamento> listaEqp = new List<equipamento>();

                    qtdEquipamentosLidos[0] = dtEquipamentos.Rows.Count;
                    log.escrever("Leitora SNMP", string.Format("Serão lidos {0} equipamentos.", qtdEquipamentosLidos[0].ToString()));
                    foreach (DataRow eqp in dtEquipamentos.Rows)
                    {
                        equipamento equip = new equipamento(eqp["ip"].ToString(), eqp["idEquipamento"].ToString());
                        listaEqp.Add(equip);
                    }

                    int qtdEqptos = 300;

                    int qtdListas = (listaEqp.Count / qtdEqptos) + 1;
                    int count = 0;
                    if (qtdListas >= 1)
                    {
                        listaEquipamentosGeral.Clear();
                        for (int i = 0; i < qtdListas; i++)
                        {
                            if (count + qtdEqptos < listaEqp.Count)
                            {
                                List<equipamento> lParcial = listaEqp.GetRange(count, qtdEqptos);
                                listaEquipamentosGeral.Add(lParcial);
                                Thread t = new Thread(new ParameterizedThreadStart(disparoParcial));
                                t.Start(i);
                                count += qtdEqptos;
                            }
                            else
                            {
                                List<equipamento> lParcial = listaEqp.GetRange(count, listaEqp.Count - count);
                                listaEquipamentosGeral.Add(lParcial);
                                Thread t = new Thread(new ParameterizedThreadStart(disparoParcial));
                                t.Start(i);
                            }
                        }
                    }
                    else
                    {
                        listaEquipamentosGeral.Add(listaEqp);
                    }

                    break;
                case "externo":
                    //lEquipamentos = equipamento.retornaEquipamentosxml(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/config/equipamentos.xml");
                    lEquipamentos = equipamento.retornaEquipamentosLocais();
                    foreach (equipamento eqp in lEquipamentos)
                    {
                        log.escrever("INFO", string.Format("Lendo equipamento de ip {0}.", eqp.Ip));

                        if (eqp.retornaOids(equipamento.tpAgente.externo))
                        {
                            if (eqp.disparoSnmp())
                            {
                                if (!eqp.gravar(true))
                                {
                                    log.escrever("ERRO", string.Format("Falha na gravação de log do equipamento de ip {0}.", eqp.Ip));
                                }
                            }
                            else
                            {
                                log.escrever("ERRO", string.Format("Falha na leitura do equipamento de ip {0}.", eqp.Ip));
                            }
                        }
                        else
                        {
                            log.escrever("ERRO", string.Format("Modelo {0} não encontrado.", snmp.getValue(".1.3.6.1.2.1.1.1.0", eqp.Ip)));  
                        }
                    }
                    enviarArquivos();

                    break;
            }
        }

        static void enviarArquivos()
        {
            string dirdados = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\dados";
            List<string> lista = Util.ListarArquivos(dirdados, "xml");
            if (lista.Count > 0)
            {
                foreach (string s in lista)
                {
                    if (Util.Upload(Util.Descriptografar(parametros.retornaParametro("usuarioftp")), Util.Descriptografar(parametros.retornaParametro("senhaftp")), Util.Descriptografar(parametros.retornaParametro("destinoftp")), s))
                    {
                        if (Util.DeletarArquivo(s))
                        {
                            log.escrever("Envio", "arquivo enviado com sucesso.");
                        }
                    }
                }
            }
        }

        static void disparoParcial(object idLista)
        {
            List<equipamento> listaEqp = new List<equipamento>();
            //List<equipamento> lEquipamentos = new List<equipamento>();

            listaEqp = listaEquipamentosGeral[int.Parse(idLista.ToString())];

            foreach (equipamento eqp in listaEqp)
            {
                //Console.WriteLine(string.Format("Disparo para o equipamento {0}.", eqp.IdEquipamento));
                if (eqp.retornaOids(equipamento.tpAgente.local))
                {
                    if (eqp.disparoSnmp())
                    {
                        if (eqp.gravar())
                        {
                            //DAO.ExecutaSQL(ConfigurationManager.ConnectionStrings["dnaPrint"].ToString(), string.Format("insert into log(mensagem) values('concluido:{0}:Disparo concluído com êxito.')", eqp.IdEquipamento));
                            qtdEquipamentosLidos[1]++;
                            log.escrever("Leitora SNMP", string.Format("{0} de {1} equipamentos lidos. {2} apresentaram falha.", qtdEquipamentosLidos[1].ToString(), qtdEquipamentosLidos[0].ToString(), qtdEquipamentosLidos[2].ToString()));
                        }
                        else
                        {
                            qtdEquipamentosLidos[2]++;
                            // DAO.ExecutaSQL(ConfigurationManager.ConnectionStrings["dnaPrint"].ToString(), string.Format("insert into log(mensagem) values('erro:{0}:Falha na gravação.')", eqp.IdEquipamento));
                        }
                    }
                    else
                    {
                        qtdEquipamentosLidos[2]++;
                        //DAO.ExecutaSQL(ConfigurationManager.ConnectionStrings["dnaPrint"].ToString(), string.Format("insert into log(mensagem) values('erro:{0}:Falha no disparo.')", eqp.IdEquipamento));
                    }
                }
                else
                {
                    //DAO.ExecutaSQL(ConfigurationManager.ConnectionStrings["dnaPrint"].ToString(), string.Format("insert into log(mensagem) values('erro:{0}:Não encontruo oids.')", eqp.IdEquipamento));
                    qtdEquipamentosLidos[2]++;
                }
            }
        }
    }
}
