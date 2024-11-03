using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace dnaPrint
{
    class Disparo
    {
        static string dirAtual = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string dirDados = dirAtual + @"\Dados";
        static string dirConfig = dirAtual + @"\Config";
        static string config = dirConfig + @"\Config.xml";
        static string dirLogs = dirAtual + @"\Logs";
        //static string data = DateTime.Now.ToString("ddMMyyyyHHmmss");
        static string msg = "";

        public enum TipoConexao { xml, sqlserver, sqlcompact, mysql, oracle, firebird, postgree };

        public void IniciarProcesso(TipoConexao tp)
        {
            Cripto des = new Cripto();

            Disparo d = new Disparo();
            switch (tp)
            {
                case TipoConexao.firebird:
                    throw new NotImplementedException();
                    break;
                case TipoConexao.mysql:
                    throw new NotImplementedException();
                    break;
                case TipoConexao.oracle:
                    throw new NotImplementedException();
                    break;
                case TipoConexao.postgree:
                    throw new NotImplementedException();
                    break;
                case TipoConexao.sqlserver:
                    String connString = @"Server=" + DAO.RetornaAtributoXml(config, "Servidor")
                        + "; Database=" + DAO.RetornaAtributoXml(config, "Database")
                        + "; User Id=" + DAO.RetornaAtributoXml(config, "Usuario")
                        + "; Password=" + des.Decriptar(DAO.RetornaAtributoXml(config, "Senha"));
                    d.Executar(1, connString);
                    break;
                case TipoConexao.xml:
                    List<Equipamentos> ListaEquipamentos = Equipamentos.RetornaEqpXml(dirConfig + @"\Equipamentos.xml");
                    List<oidsPadrao> ListaOidsPadrao = oidsPadrao.RetornaOidsPadraoXml(dirConfig + @"\Perfis.xml");
                    List<Oids> ListaOids = Oids.RetornaOidsXml((dirConfig + @"\Oids.xml"));
                    d.Executar(ListaEquipamentos, ListaOidsPadrao, ListaOids);
                    try
                    {
                        SMTP.PreparaEmail();
                    }
                    catch
                    {
                        msg = "Falha no envio de email. ";
                        Logs.GerarLogs(Logs.TipoLogs.email, msg);
                    }
                    break;
                case TipoConexao.sqlcompact:
                    string datasource = DAO.RetornaAtributoXml(config, "Servidor");
                    string senha = DAO.RetornaAtributoXml(config, "Senha");
                    string ConnectionString = "Data Source = " + datasource + " ; Password = " + senha;
                    d.Executar(ConnectionString);
                    break;
            }

        }

        public void Executar(List<Equipamentos> ListaEquipamentos, List<oidsPadrao> ListaOidsPadrao, List<Oids> ListaOids)
        {
            string data = DateTime.Now.ToString("ddMMyyyyHHmmss");
            foreach (Equipamentos eqp in ListaEquipamentos)
            {
                #region Verificar Modelos
                try
                {
                    eqp.TestarConexao(ListaOidsPadrao);
                }
                catch
                {
                    msg = "Falha ao testar o equipamento de IP: " + eqp.Ip.ToString() + ".";
                    Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                }
                #endregion
            }
            foreach (Equipamentos eqp in ListaEquipamentos)
            {
                #region Ler Parâmetros
                try
                {
                    eqp.LerDados(ListaOids);
                    if (eqp.Modelo == "" || eqp.Modelo == null)
                        eqp.LerDados(ListaOids);
                }
                catch
                {
                    msg = "Falha ao ler o equipamento de IP: " + eqp.Ip.ToString() + ".";
                    Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                }
                #endregion
            }
            foreach (Equipamentos eqp in ListaEquipamentos)
            {

                #region Gravar Dados
                if (eqp.StatusDados() == true)
                {
                    try
                    {
                        eqp.GravarDados(dirDados, @"\dados_" + data + ".csv");
                    }
                    catch (Exception ex)
                    {
                        msg = "Falha ao gravar o equipamento de IP: " + eqp.Ip.ToString() + "\n"
                         + ex.ToString();
                        Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                    }
                }

                #endregion
            }

        }

        public void Executar(string connString)
        {
            List<Oids> ListaOids = Listas.ListaOids(connString, TipoConexao.sqlcompact);
            List<oidsPadrao> ListaOidsPadrao = Listas.ListaOidPadrao(connString, TipoConexao.sqlcompact);
            List<Equipamentos> ListaEquipamentos = new List<Equipamentos>();
            DataTable dtEquipamentos = new DataTable();
            string sqlEquipamentos = "Select * from Equipamentos";

            dtEquipamentos = DAO.RetornaDtSqlCompact(connString, sqlEquipamentos);

            // Preenche a lista de equipamentos

            foreach (DataRow linha in dtEquipamentos.Rows)
            {
                Equipamentos nEqp = new Equipamentos();
                nEqp.IdEquipamento = linha["idEquipamento"].ToString();
                nEqp.Ip = linha["ip"].ToString();
                nEqp.Fabricante = linha["fabricante"].ToString();
                nEqp.Cor = linha["cor"].ToString();
                ListaEquipamentos.Add(nEqp);
            }

            foreach (Equipamentos eqp in ListaEquipamentos)
            {
                #region Verificar Modelos
                try
                {
                    eqp.TestarConexao(ListaOidsPadrao);
                }
                catch
                {
                    msg = "Falha ao testar o equipamento de IP: " + eqp.Ip.ToString() + ".";
                    Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                }
                #endregion
            }
            foreach (Equipamentos eqp in ListaEquipamentos)
            {
                #region Ler Parâmetros
                try
                {
                    eqp.LerDados(ListaOids);
                    if (eqp.Modelo == "" || eqp.Modelo == null)
                        eqp.LerDados(ListaOids);
                }
                catch
                {
                    msg = "Falha ao ler o equipamento de IP: " + eqp.Ip.ToString() + ".";
                    Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                }
                #endregion
            }
            foreach (Equipamentos eqp in ListaEquipamentos)
            {
                #region Gravar Dados
                if (eqp.StatusDados() == true)
                {
                    try
                    {
                        eqp.GravarDados(connString, TipoConexao.sqlcompact);
                    }
                    catch (Exception ex)
                    {
                        msg = "Falha ao gravar o equipamento de IP: " + eqp.Ip.ToString() + "\n"
                        + ex.ToString();
                        Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                    }
                }

                #endregion
            }
        }

        public void Executar(int DisparoCompleto, string ConnectionString)
        {
            int qtdListas = 10;

            #region Configura Banco

            string connString = ConnectionString;
            string sqlEquipamentos = "";
            if (DisparoCompleto == 1)
            {
                sqlEquipamentos = " SELECT * FROM consultarEquipamentosCompleta(" + qtdListas.ToString() + ")";
            }
            else
            {
                sqlEquipamentos = "SELECT * FROM consultarEquipamentosParcial(" + qtdListas.ToString() + ")";
            }
            #endregion

            List<Equipamentos> listaEquipamentos = new List<Equipamentos>();
            List<List<Equipamentos>> Listas10 = Listas.RetornarListas(qtdListas);

            #region PreencherListas

            List<Oids> ListaOids = Listas.ListaOids(connString, TipoConexao.sqlserver);
            List<oidsPadrao> ListaOidsPadrao = Listas.ListaOidPadrao(connString, TipoConexao.sqlserver);
            DataTable dtEquipamentos = new DataTable();



            if (!File.Exists(@"Config\Equipamentos.xml"))
            {
                dtEquipamentos = DAO.RetornaDt(connString, sqlEquipamentos);

                // Preenche a lista de equipamentos

                foreach (DataRow linha in dtEquipamentos.Rows)
                {
                    Equipamentos nEqp = new Equipamentos();
                    nEqp.IdEquipamento = linha["idEquipamento"].ToString();
                    nEqp.Ip = linha["ip"].ToString();
                    nEqp.Fabricante = linha["fabricante"].ToString();
                    nEqp.Grupo = linha["grupo"].ToString();
                    nEqp.Cor = linha["cor"].ToString();

                    Listas10[int.Parse(nEqp.Grupo) - 1].Add(nEqp);
                }
            }

            else
            {
                List<Equipamentos> ListaEquipamentosxml = Equipamentos.RetornaEqpXml(dirConfig + @"\Equipamentos.xml");
                foreach (Equipamentos eqp in ListaEquipamentosxml)
                {
                    Listas10[int.Parse(eqp.Grupo) - 1].Add(eqp);
                }
            }
            #endregion


            foreach (List<Equipamentos> ListaEquipamentos in Listas10)
            {
                foreach (Equipamentos eqp in ListaEquipamentos)
                {
                    #region Verificar Modelos
                    try
                    {
                        eqp.TestarConexao(ListaOidsPadrao);
                    }
                    catch
                    {
                        msg = "Falha ao testar o equipamento de IP: " + eqp.Ip.ToString() + ".";
                        Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                    }
                    #endregion
                }
                foreach (Equipamentos eqp in ListaEquipamentos)
                {
                    #region Ler Parâmetros
                    try
                    {
                        eqp.LerDados(ListaOids);
                        if (eqp.Modelo == "" || eqp.Modelo == null)
                            eqp.LerDados(ListaOids);
                    }
                    catch
                    {
                        msg = "Falha ao ler o equipamento de IP: " + eqp.Ip.ToString() + ".";
                        Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                    }
                    #endregion
                }
                foreach (Equipamentos eqp in ListaEquipamentos)
                {
                    #region Gravar Dados
                    if (eqp.StatusDados() == true)
                    {
                        try
                        {
                            eqp.GravarDados(ConnectionString, TipoConexao.sqlserver);
                        }
                        catch (Exception ex)
                        {
                            msg = "Falha ao gravar o equipamento de IP: " + eqp.Ip.ToString() + "\n"
                            + ex.ToString();
                            Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                        }
                    }

                    #endregion
                }
            }
        }

        public Disparo.TipoConexao DefineConexao(string ArquivoXml)
        {
            Disparo.TipoConexao tipoEntrada = new Disparo.TipoConexao();
            string tipo = DAO.RetornaAtributoXml(dirConfig + @"\Config.xml", "TipoEntrada");
            if (tipo == Disparo.TipoConexao.xml.ToString())
                tipoEntrada = Disparo.TipoConexao.xml;
            else if (tipo == Disparo.TipoConexao.sqlserver.ToString())
                tipoEntrada = Disparo.TipoConexao.sqlserver;
            else if (tipo == Disparo.TipoConexao.firebird.ToString())
                tipoEntrada = Disparo.TipoConexao.firebird;
            else if (tipo == Disparo.TipoConexao.mysql.ToString())
                tipoEntrada = Disparo.TipoConexao.mysql;
            else if (tipo == Disparo.TipoConexao.oracle.ToString())
                tipoEntrada = Disparo.TipoConexao.oracle;
            else if (tipo == Disparo.TipoConexao.postgree.ToString())
                tipoEntrada = Disparo.TipoConexao.postgree;
            else tipoEntrada = Disparo.TipoConexao.sqlcompact;
            return tipoEntrada;
        }
    }
}

