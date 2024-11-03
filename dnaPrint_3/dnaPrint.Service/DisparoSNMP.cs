using System;
using System.Configuration;
using System.Linq;
using dnaPrint.Base;

namespace dnaPrint.Service
{
    public class DisparoSNMP
    {
        private Equipamento Eqpto;
        static string connString = ConfigurationManager.ConnectionStrings["dnaPrint"].ToString();
        static DAO.Operacoes.tipo tpDB = DAO.Operacoes.DefinirTipo(ConfigurationManager.AppSettings["DBType"].ToString());

        public DisparoSNMP(Equipamento eqp)
        {
            this.Eqpto = eqp;
        }

        public void Disparar()
        {
            try
            {
                Eqpto.Descricao = SNMP.SNMP.Get(".1.3.6.1.2.1.1.1.0", Eqpto.IP);
                Eqpto.Oids = OID.Listar(Eqpto.Descricao, connString, tpDB);
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(Eqpto.Descricao))
            {
                if (Eqpto.Oids != null)
                {
                    if (Eqpto.Oids.Count > 0)
                    {
                        foreach (var disp in Eqpto.Oids)
                        {
                            disp.Valor = SNMP.SNMP.Get(disp.Oid, Eqpto.IP);
                        }

                        if (Eqpto.DisparoValido())
                        {
                            try
                            {
                                Eqpto.Serie = Eqpto.Oids.Where(x => x.Propriedade == "serie").First().Valor.ToString().Trim();
                                if (Eqpto.SalvarDisparo(tpDB, connString))
                                {
                                    Console.WriteLine($"{DateTime.Now.ToString()} | Equipamento de id {Eqpto.idEquipamento} e série {Eqpto.Serie} lido com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine($"{DateTime.Now.ToString()} | Falha ao tentar salvar disparo do equipamento de IP {Eqpto.IP}!");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{DateTime.Now.ToString()} | Falha ao tentar salvar disparo do equipamento de IP {Eqpto.IP}! Mensagem: {ex.Message}");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"{DateTime.Now.ToString()} | Falha ao tentar salvar disparo do equipamento de IP {Eqpto.IP}!");
            }
        }
    }
}
