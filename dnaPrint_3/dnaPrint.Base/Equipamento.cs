using dnaPrint.DAO;
using System.Collections.Generic;
using System.Data;
using System.Printing;
using System;

namespace dnaPrint.Base
{
    public class Equipamento : IBase<Equipamento>
    {
        #region Campos

        public int idEquipamento { get; set; }
        public string IP { get; set; }
        public string Descricao { get; set; }
        public string Serie { get; set; }
        public List<OID> Oids { get; set; }
        public Tipo TipoEquipamento { get; set; }
        public enum Tipo { Local, Rede }
        public int idEstado { get; set; }
        public int idCidade { get; set; }
        public int idLocalidade { get; set; }
        public int idSetor { get; set; }
        public int idModeloEquipamento { get; set; }
        public bool cor { get; set; }
        public string nome { get; set; }

        #endregion

        public static List<Equipamento> Listar(Tipo _tipoEquipamento, Operacoes.tipo Database, string connString)
        {
            List<Equipamento> Lista = new List<Equipamento>();

            switch (_tipoEquipamento)
            {
                case Tipo.Local:
                    Lista = ListarEquipamentosLocais();
                    break;
                case Tipo.Rede:
                    Lista = ListarEquipamentosRede(Database, connString);
                    break;
            }
            return Lista;
        }

        public static List<Equipamento> ListarEquipamentosRede(Operacoes.tipo TipoDB, string connString)
        {
            List<Equipamento> Lista = new List<Equipamento>();

            //            string query = @"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where idequipamento in 
            //(select idEquipamento from vw_disponibilidade where mac is null and ip not like '192.%' );";
            string query = @"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where status = '1';";

            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.TipoEquipamento = Tipo.Rede;
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.idModeloEquipamento = int.Parse(eqpto["idModeloEquipamento"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        public static List<Equipamento> ListarEquipamentosRede(Operacoes.tipo TipoDB, string connString, string serie)
        {
            List<Equipamento> Lista = new List<Equipamento>();

            //            string query = @"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where idequipamento in 
            //(select idEquipamento from vw_disponibilidade where mac is null and ip not like '192.%' );";
            string query = $@"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where serie = '{serie}';";

            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.TipoEquipamento = Tipo.Rede;
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.idModeloEquipamento = int.Parse(eqpto["idModeloEquipamento"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        public static List<Equipamento> ListarEquipamentosSemLeitura(Operacoes.tipo TipoDB, string connString)
        {
            List<Equipamento> Lista = new List<Equipamento>();

            //            string query = @"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where idequipamento in 
            //(select idEquipamento from vw_disponibilidade where mac is null and ip not like '192.%' );";
            string query = $@"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos 
            where idequipamento in(	select idEquipamento from vw_disponibilidade where (qtddias is null or qtddias > 0) and ip not like '192.168.%');";
            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.TipoEquipamento = Tipo.Rede;
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.idModeloEquipamento = int.Parse(eqpto["idModeloEquipamento"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        public static List<Equipamento> ListarEquipamentosPorIp(Operacoes.tipo TipoDB, string connString, string ip)
        {
            List<Equipamento> Lista = new List<Equipamento>();

            //            string query = @"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where idequipamento in 
            //(select idEquipamento from vw_disponibilidade where mac is null and ip not like '192.%' );";
            string query = $@"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos where ip = '{ip}';";
            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.TipoEquipamento = Tipo.Rede;
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.idModeloEquipamento = int.Parse(eqpto["idModeloEquipamento"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        public static List<Equipamento> ListarEquipamento(Operacoes.tipo TipoDB, string connString, string ip)
        {
            List<Equipamento> Lista = new List<Equipamento>();

            //            string query = @"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where idequipamento in 
            //(select idEquipamento from vw_disponibilidade where mac is null and ip not like '192.%' );";
            string query = $@"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos 
            where idequipamento in(	select idEquipamento from vw_disponibilidade where ip = '{ip}');";
            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.TipoEquipamento = Tipo.Rede;
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.idModeloEquipamento = int.Parse(eqpto["idModeloEquipamento"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        private static List<Equipamento> ListarEquipamentosLocais()
        {
            List<Equipamento> Lista = new List<Equipamento>();

            LocalPrintServer server = new LocalPrintServer();
            PrintQueueCollection impressoras = server.GetPrintQueues();

            foreach (PrintQueue print in impressoras)
            {
                string ip = WMIQuery.PrinterPort(print.QueuePort.Name);

                if (!string.IsNullOrEmpty(ip))
                {
                    Equipamento eqp = new Equipamento();
                    eqp.IP = ip;
                    eqp.TipoEquipamento = Tipo.Rede;
                    Lista.Add(eqp);
                }


            }
            return Lista;
        }

        public void AdicionarBaseLocal(string connString)
        {
            Operacoes opBD = new Operacoes(connString, Operacoes.tipo.SQLite);
            int qtdEqp = int.Parse(opBD.ExecuteScalar($"select count(*) from cadastroequipamentos where serie = '{this.Serie}';").ToString());

            if (qtdEqp == 0)
            {
                opBD.ExecuteNonQuery($"insert into cadastroequipamentos(serie, ip) values('{this.Serie}','{this.IP}');");
            }
            else
            {
                int intTemp = 0;
                int.TryParse(opBD.ExecuteNonQuery($"insert into cadastroequipamentos(serie, ip) values('{this.Serie}','{this.IP}');").ToString(), out intTemp);
                this.idEquipamento = intTemp;
            }
        }

        public bool DisparoValido()
        {
            bool result = false;

            foreach (var oid in this.Oids)
            {
                bool oidValor = false;
                if (oid.Valor != null)
                {
                    if (oid.Tipo != "num")
                    {
                        oidValor = true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(oid.Valor.ToString()))
                        {
                            oidValor = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    oidValor = false;
                    break;
                }
                result = oidValor;
            }

            return result;
        }

        public bool SalvarDisparo(Operacoes.tipo TipoDB, string connString)
        {
            bool result = false;

            result = SalvarDisparoBD(TipoDB, connString);

            return result;
        }

        private bool SalvarDisparoBD(Operacoes.tipo TipoDB, string connString)
        {
            bool result = false;
            string queryBase = GerarInsert("DadosDisparos");
            List<string[]> parametros = GerarParametros();
            int linhas = 0;

            Operacoes opDB = new Operacoes(connString, TipoDB);
            linhas = opDB.ExecuteNonQuery(queryBase, parametros);

            if (linhas > 0)
                result = true;

            return result;
        }

        public List<string[]> GerarParametros()
        {
            List<string[]> parametros = new List<string[]>();
            foreach (var oid in this.Oids)
            {
                parametros.Add(new string[] { $"@{oid.Propriedade}", oid.Valor.ToString() });
            }
            return parametros;
        }

        public string GerarInsert(string tabela)
        {
            string NomeColunas = "idEquipamento";
            string VariaveisColunas = this.idEquipamento.ToString();

            foreach (var oid in this.Oids)
            {
                if (NomeColunas == null)
                {
                    NomeColunas += oid.Propriedade;
                    VariaveisColunas += $"@{oid.Propriedade}";
                }
                else
                {
                    NomeColunas += "," + oid.Propriedade;
                    VariaveisColunas += $", @{oid.Propriedade}";
                }
            }

            return $"Insert into {tabela}({NomeColunas}) Values({VariaveisColunas});";
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"update CadastroEquipamentos set ip = @ip, nome = @nome, idmodeloEquipamento = @idmodeloEquipamento where idEquipamento = @idEquipamento;";
            List<object[]> Parametros = new List<object[]>();
            Parametros.Add(new object[] { "@ip", this.IP });
            Parametros.Add(new object[] { "@nome", this.nome });
            Parametros.Add(new object[] { "@idmodeloEquipamento", this.idModeloEquipamento });
            Parametros.Add(new object[] { "@idEquipamento", this.idEquipamento });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = null;
            tsql = $"insert into CadastroEquipamentos(idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome, status) values(@idEstado, @idCidade, @idLocalidade, @idSetor, @idModeloEquipamento, @ip, '0', @serie, @nome, '1');";

            if (this.cor)
                tsql = $"insert into CadastroEquipamentos(idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome, status) values(@idEstado, @idCidade, @idLocalidade, @idSetor, @idModeloEquipamento, @ip, '1', @serie, @nome, '1');";

            List<string[]> Parametros = new List<string[]>();
            Parametros.Add(new string[] { "@idEstado",  this.idEstado.ToString()});
            Parametros.Add(new string[] { "@idCidade",  this.idCidade.ToString()});
            Parametros.Add(new string[] { "@idLocalidade",  this.idLocalidade.ToString()});
            Parametros.Add(new string[] { "@idSetor",  this.idSetor.ToString()});
            Parametros.Add(new string[] { "@idModeloEquipamento", this.idModeloEquipamento.ToString() });
            Parametros.Add(new string[] { "@ip", this.IP.ToString() });
           

            Parametros.Add(new string[] { "@serie", this.Serie });
            Parametros.Add(new string[] { "@nome", this.nome });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = $"update CadastroEquipamentos set status = '0' where idEquipamento = @idEquipamento;";
            List<string[]> Parametros = new List<string[]>();
            Parametros.Add(new string[] { "@idEquipamento", this.idEquipamento.ToString() });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, Parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public Equipamento ListarByID(string connString, Operacoes.tipo Tipo, int id)
        {
            string tsql = $"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where idEquipamento = {id};";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);
            Equipamento e = new Equipamento();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();
                    e.idModeloEquipamento = int.Parse(eqpto["idModeloEquipamento"].ToString());
                }
            }
            return e;
        }

        public static List<Equipamento> ListarBySetor(string connString, Operacoes.tipo Tipo, int idSetor)
        {
            List<Equipamento> Lista = new List<Equipamento>();
            string tsql = $"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where status = '1' and idSetor = {idSetor};";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();

                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        public static List<Equipamento> Listar(string connString, Operacoes.tipo Tipo, string idEstado, string idCidade, string idLocalidade, string idSetor)
        {
            List<Equipamento> Lista = new List<Equipamento>();
            string tsql = $"select idEquipamento, idEstado, idCidade, idLocalidade, idSetor, idModeloEquipamento, ip, cor, serie, nome from CadastroEquipamentos  where status = '1' ";
            int idTemp = 0;

            if (int.TryParse(idEstado, out idTemp))
                tsql += $" and idEstado = {idEstado}";
            if (int.TryParse(idCidade, out idTemp))
                tsql += $" and idcidade = {idCidade}";
            if (int.TryParse(idLocalidade, out idTemp))
                tsql += $" and idlocalidade = {idLocalidade}";
            if (int.TryParse(idSetor, out idTemp))
                tsql += $" and idSetor = {idSetor}";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    Equipamento e = new Equipamento();

                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.IP = eqpto["ip"].ToString();
                    e.Serie = eqpto["serie"].ToString();
                    e.idEstado = int.Parse(eqpto["idEstado"].ToString());
                    e.idCidade = int.Parse(eqpto["idCidade"].ToString());
                    e.idLocalidade = int.Parse(eqpto["idLocalidade"].ToString());
                    e.idSetor = int.Parse(eqpto["idSetor"].ToString());
                    e.cor = bool.Parse(eqpto["cor"].ToString());
                    e.nome = eqpto["nome"].ToString();

                    Lista.Add(e);
                }
            }
            return Lista;
        }

        public static bool FilaExiste(string connString, Operacoes.tipo Tipo, string _Fila)
        {
            bool result = false;

            string tsql = $"SELECT COUNT(*) FROM CadastroEquipamentos where nome = '{_Fila}';";

            int qtd = int.Parse(new DAO.Operacoes(connString, Tipo).ExecuteScalar(tsql).ToString());

            if (qtd > 0)
                result = true;
            return result;
        }

        public static bool SerieExiste(string connString, Operacoes.tipo Tipo, string _Serie)
        {
            bool result = false;

            string tsql = $"SELECT COUNT(*) FROM CadastroEquipamentos where serie = '{_Serie}';";

            int qtd = int.Parse(new DAO.Operacoes(connString, Tipo).ExecuteScalar(tsql).ToString());

            if (qtd > 0)
                result = true;
            return result;
        }

        public static object ProximoFila(string connString, Operacoes.tipo Tipo)
        {
            string tsql = "select Max(cast(right(LEFT(nome, 7), 3) as int)) from cadastroEquipamentos where status = '1' AND NOME LIKE 'X%'";
            object obj;

            try
            {
                obj = new DAO.Operacoes(connString, Tipo).ExecuteScalar(tsql);
                int intTemp;
                if (int.TryParse(obj.ToString(), out intTemp))
                {
                    intTemp++;
                    obj = $"XCM-{intTemp.ToString()} / XCC-{intTemp.ToString()}";
                }
            }
            catch (Exception ex)
            {
                obj = ex;
            }
            return obj;
        }


    }
}
