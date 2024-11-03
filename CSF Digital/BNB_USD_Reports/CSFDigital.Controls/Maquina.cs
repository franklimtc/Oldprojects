using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFDigital.Controls
{
    public class Maquina
    {
        #region Atributos
        private string _serie;
        private string _ip;
        private Localidade _local;
        private DateTime? _dtInstalacao;
        private string _SLO;
        private float _bilhetagem;
        private DateTime? _dtUltimaLeitura;
        #endregion

        #region Métodos Get / Set
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public Localidade Local
        {
            get { return _local; }
            set { _local = value; }
        }
        public DateTime? DataInstalacao
        {
            get { return _dtInstalacao; }
            set { _dtInstalacao = value; }
        }
        public string SLO
        {
            get { return _SLO; }
            set { _SLO = value; }
        }
        public float Bilhetagem
        {
            get { return _bilhetagem; }
            set { _bilhetagem = value; }
        }
        public DateTime? DataUltimaLeitura
        {
            get { return _dtUltimaLeitura; }
            set { _dtUltimaLeitura = value; }
        }
        #endregion

        #region Métodos auxiliares
        public static string MontarListaStringMaquina(List<string> lista)
        {
            if (lista.Count == 0)
                return String.Empty;
            else
            {
                string listaId = String.Empty;

                foreach (string maquina in lista)
                {
                    listaId = String.Concat(listaId, "'", maquina, "',");
                }
                return listaId.Remove(listaId.Length - 1);
            }
        }
        public static string MontarListaStringMaquina(List<Chamado> lista)
        {
            if (lista.Count == 0)
                return String.Empty;
            else
            {
                string listaId = String.Empty;

                foreach (Chamado chamado in lista)
                {
                    if (chamado.MaquinaRelacionada != null)
                    {
                        if (chamado.MaquinaRelacionada.Serie != null)
                            listaId = String.Concat(listaId, "'", chamado.MaquinaRelacionada.Serie, "',");
                    }
                }
                return listaId.Remove(listaId.Length - 1);
            }
        }
        #endregion

        #region Buscar Máquinas
        private static List<string> BuscarMaquinas(List<Chamado> listaChamados)
        {
            List<string> listaMquinas = new List<string>();

            if (listaChamados.Count > 0)
            {
                string listaReqId = Chamado.MontarListaPersid(listaChamados);

                if (!String.IsNullOrEmpty(listaReqId))
                {
                    StringBuilder sql = new StringBuilder();

                    sql.Append(" SELECT");
                    sql.Append(" dbo.call_req.ref_num REFERENCIA,");
                    sql.Append(" CASE");
                    sql.Append(" WHEN UPPER(LTRIM(RTRIM(dbo.cr_prp.value))) like '%,%' THEN SUBSTRING(UPPER(LTRIM(RTRIM(dbo.cr_prp.value))),0,PATINDEX('%,%',UPPER(LTRIM(RTRIM(dbo.cr_prp.value)))))");
                    sql.Append(" ELSE UPPER(LTRIM(RTRIM(dbo.cr_prp.value)))");
                    sql.Append(" END AS SERIE,");
                    sql.Append(" dbo.cr_prp.description AS DESCRICAO");
                    sql.Append(" FROM dbo.call_req WITH (nolock) LEFT OUTER JOIN");
                    sql.Append(" dbo.cr_prp WITH (nolock) ON dbo.call_req.persid = dbo.cr_prp.owning_cr");
                    sql.Append(" WHERE dbo.cr_prp.label = 'Número de Série'");
                    sql.Append(" and dbo.cr_prp.value is not null");
                    sql.Append(" and dbo.cr_prp.value <> ''");
                    sql.Append(String.Format(" and dbo.call_req.persid IN ({0}) ", listaReqId));
                    sql.Append(" ORDER BY dbo.call_req.open_date ");

                    Banco banco = new Banco();
                    SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao);

                    listaMquinas = ProcessarMaquinas(banco.ExecuteDataTable(cmd), listaChamados);
                }
            }

            return listaMquinas;
        }
        #endregion

        #region Processa lista de Maquinas
        private static List<string> ProcessarMaquinas(DataTable dataTableUSD, List<Chamado> listaChamados)
        {
            List<string> Series = new List<string>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                string serie = rowReader.GetNullableString("SERIE");
                string referencia = rowReader.GetString("REFERENCIA");

                if (serie != null)
                {
                    string aux = "";
                    for (int i = 0; i < serie.Length; i++)
                    {
                        if (serie[i] != ' ')
                            aux += serie[i].ToString();
                    }
                    if (aux != "")
                        serie = aux.Trim().ToUpper();

                    Chamado chamado = listaChamados.Find(c => c.Referencia == referencia); ;
                    if (chamado != null)
                    {
                        chamado.MaquinaRelacionada.Serie = serie;

                        Series.Add(serie);
                    }
                }
            }
            return Series;
        }

        public static void PassaDatasInstalacao(DataTable dt, List<Chamado> listaChamados)
        {
            string serie = String.Empty;

            foreach (DataRow row in dt.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                serie = row["SERIE"].ToString();

                if (serie != "")
                {
                    string aux = "";
                    for (int i = 0; i < serie.Length; i++)
                    {
                        if (serie[i] != ' ')
                            aux += serie[i].ToString();
                    }
                    if (aux != "")
                        serie = aux.Trim().ToUpper();

                    Chamado chamado = listaChamados.Find(c => c.MaquinaRelacionada.Serie == serie);

                    if (chamado != null)
                    {
                        if (chamado.MaquinaRelacionada != null)
                        {
                            chamado.MaquinaRelacionada.DataInstalacao = rowReader.GetNullableDateTime("DATA");

                            if (chamado.MaquinaRelacionada.DataInstalacao.HasValue)
                            {
                                if (((int)chamado.Data_Abertura.Value.Subtract(chamado.MaquinaRelacionada.DataInstalacao.Value).TotalHours) < 1440)
                                {
                                    chamado.MaquinaRelacionada.SLO = "Sim";
                                    chamado.SlaDefinida.DeAcordo = "**SLO";
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Buscar Máquinas - CSF
        public static void BuscarMaquinasCSF(List<Chamado> listaChamados)
        {
            List<string> listaMaquinas = BuscarMaquinas(listaChamados);

            StringBuilder sql = new StringBuilder();

            //sql.Append(" SELECT");
            //sql.Append(" LTRIM(RTRIM(UF)) AS ESTADO,");
            //sql.Append(" SUBSTRING(impr_snmp.localizacao, 6, 300) CIDADE,");
            //sql.Append(" LTRIM(RTRIM(Unidade)) AS UNIDADE,");
            //sql.Append(" LTRIM(RTRIM(Setor)) AS SETOR,");
            //sql.Append(" LTRIM(RTRIM(numero_serie)) AS SERIE,");
            //sql.Append(" LTRIM(RTRIM(Ip)) AS IP");
            //sql.Append(" FROM impr_snmp WITH (nolock)");

            sql.Append(" SELECT");
            sql.Append(" LTRIM(RTRIM(a.UF)) AS ESTADO,");
            sql.Append(" SUBSTRING(a.localizacao, 6, 300) CIDADE,");
            sql.Append(" LTRIM(RTRIM(a.Unidade)) AS UNIDADE,");
            sql.Append(" LTRIM(RTRIM(a.Setor)) AS SETOR,");
            sql.Append(" LTRIM(RTRIM(a.numero_serie)) AS SERIE,");
            sql.Append(" LTRIM(RTRIM(a.Ip)) AS IP,");
            sql.Append(" cast(MAX(a.dt_instalacao) as date) DT_INSTALACAO,");
            sql.Append(" cast(MAX(b.data_cons) as date) DT_CONSULTA");
            sql.Append(" FROM impr_snmp as a LEFT OUTER JOIN");
            sql.Append(" cons_snmp_impr as b ON a.id_impr_snmp = b.id_impr_snmp");

            //sql.Append(" SELECT * FROM vw_equipamentosCSF");

            if (listaMaquinas.Count > 0)
            {
                sql.Append(String.Format(" WHERE LTRIM(RTRIM(a.numero_serie)) IN ({0}) ", MontarListaStringMaquina(listaMaquinas)));
                sql.Append(" and a.localizacao not like '%DESATIVADA%'");

                //sql.Append(String.Format(" WHERE LTRIM(RTRIM(SERIE)) IN ({0}) ", MontarListaStringMaquina(listaMaquinas)));
            }

            sql.Append(" GROUP BY a.UF,a.localizacao,a.Unidade,a.Setor,a.numero_serie,a.Ip");

            //sql.Append(" UNION ALL");
            //sql.Append(" SELECT uf, cidade, unidade, ambiente, serie, ip, dtativacao, data FROM dnaprint.dbo.vw_disponibilidade3");

            //if (listaMaquinas.Count > 0)
            //{
            //    sql.Append(String.Format(" WHERE LTRIM(RTRIM(serie)) IN ({0}) ", MontarListaStringMaquina(listaMaquinas)));
            //}

            sql.Append(" ORDER BY 6");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.ConexaoCsf);
            //SqlCommand cmd = new SqlCommand(sql.ToString(), banco.ConexaoDna);

            ProcessarMaquinasCSF(banco.ExecuteDataTableCsf(cmd), listaChamados);
        }

        private static void ProcessarMaquinasCSF(DataTable dataTableUSD, List<Chamado> listaChamados)
        {
            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                string serie = row["SERIE"].ToString();

                foreach (Chamado chamado in listaChamados)
                {
                    if (chamado.MaquinaRelacionada != null)
                    {
                        if (chamado.MaquinaRelacionada.Serie == serie)
                        {
                            chamado.MaquinaRelacionada = ConstruirMaquinaCSF(rowReader);

                            if (chamado.Categoria.Contains("TONNER"))
                            {
                                if (chamado.MaquinaRelacionada.Local.Tipo_Localidade == Localidade.TipoLocalidade.CAPGV)
                                    chamado.MaquinaRelacionada.Local.Tipo_Localidade = Localidade.TipoLocalidade.Capital;

                                SLA slaFind = SLA.SLA_EntregaInsumos().Find(sla => sla.LocalRegiao == chamado.MaquinaRelacionada.Local.LocalRegiao && sla.Local == chamado.MaquinaRelacionada.Local.Tipo_Localidade);

                                if (slaFind != null)
                                    chamado.SlaDefinida = new SLA(slaFind.Id, slaFind.Prioridade, slaFind.Nivel, slaFind.Limite, slaFind.Local, slaFind.LocalRegiao, slaFind.DeAcordo);
                            }
                            else
                            {
                                SLA slaFind = SLA.SLA_AtendimentoTecnico().Find(sla => sla.Nivel == chamado.Nivel && sla.Local == chamado.MaquinaRelacionada.Local.Tipo_Localidade);

                                if (slaFind != null)
                                    chamado.SlaDefinida = new SLA(slaFind.Id, slaFind.Prioridade, slaFind.Nivel, slaFind.Limite, slaFind.Local, slaFind.LocalRegiao, slaFind.DeAcordo);
                            }
                        }
                    }
                }
            }
        }

        private static Maquina ConstruirMaquinaCSF(NullableDataRowReader rowReader)
        {
            Maquina maquina = new Maquina();

            maquina.Serie = rowReader.GetNullableString("SERIE");
            maquina.Ip = rowReader.GetNullableString("IP");
            maquina.SLO = "Não";

            maquina.Local = new Localidade();
            maquina.Local.Cidade = rowReader.GetString("CIDADE");
            maquina.Local.Estado = rowReader.GetString("ESTADO");
            maquina.Local.LocalRegiao = Localidade.RetornaRegiao(rowReader.GetString("ESTADO"));
            maquina.Local.NomeUnidade = rowReader.GetNullableString("UNIDADE");
            maquina.Local.NomeAmbiente = rowReader.GetNullableString("SETOR");

            if (maquina.Local.NomeUnidade == "CAPGV")
                maquina.Local.Tipo_Localidade = Localidade.TipoLocalidade.CAPGV;
            else if (Localidade.EhCapital(Util.RemoveAcentos(maquina.Local.Cidade)))
                maquina.Local.Tipo_Localidade = Localidade.TipoLocalidade.Capital;
            else
                maquina.Local.Tipo_Localidade = Localidade.TipoLocalidade.Interior;

            maquina.Local.TipoUnidade = rowReader.GetNullableString("SETOR");

            maquina.DataInstalacao = rowReader.GetNullableDateTime("DT_INSTALACAO");
            maquina.DataUltimaLeitura = rowReader.GetNullableDateTime("DT_CONSULTA");

            return maquina;
        }

        public static void ProcessarBilhetagem(List<Chamado> listaChamados, DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT a.SERIE as Serie, a.TOTAL as Bilhetagem");
            sql.Append(" FROM func_bilhetagem_fisica(@data_inicial,@data_final) a");
            sql.Append(" LEFT OUTER JOIN impr_snmp b on b.ID_IMPR_SNMP = a.ID_IMPR_SNMP");

            if (listaChamados.Count > 0)
                sql.Append(String.Format(" WHERE a.SERIE IN ({0}) ", MontarListaStringMaquina(listaChamados)));
            
            sql.Append(" ORDER BY 1");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.ConexaoCsf);

            banco.AddParameter(cmd, "data_inicial", dataInicial, SqlDbType.DateTime, ParameterDirection.Input);
            banco.AddParameter(cmd, "data_final", dataFinal, SqlDbType.DateTime, ParameterDirection.Input);

            ProcessarBilhetagemMaquinasCSF(banco.ExecuteDataTable(cmd), listaChamados);
        }

        private static void ProcessarBilhetagemMaquinasCSF(DataTable dataTableUSD, List<Chamado> listaChamados)
        {
            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                string serie = rowReader.GetString("Serie");
                float bilhetagem = 0;

                if (rowReader.GetNullableFloat("Bilhetagem") != null)
                    bilhetagem = rowReader.GetFloat("Bilhetagem");

                foreach (Chamado chamado in listaChamados)
                {
                    if (chamado.MaquinaRelacionada != null)
                        if (chamado.MaquinaRelacionada.Serie != null)
                            if (chamado.MaquinaRelacionada.Serie.ToUpper().Trim() == serie.ToUpper().Trim())
                                    chamado.MaquinaRelacionada.Bilhetagem = bilhetagem;
                }
            }
        }
        #endregion
    }
}