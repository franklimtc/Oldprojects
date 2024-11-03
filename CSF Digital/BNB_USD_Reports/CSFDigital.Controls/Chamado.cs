using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFDigital.Controls
{
    public class Chamado
    {
        #region Severidade
        public enum Severidade
        {
            Alta,
            Baixa
        }

        public static Severidade RetornaNivelSeveridade(int prioridade)
        {
            switch (prioridade)
            {
                case 1:
                    return Severidade.Alta;
                case 3:
                    return Severidade.Baixa;
                case 5:
                    return Severidade.Baixa;

                default:
                    return Severidade.Baixa;
            }
        }
        #endregion

        #region Atributos
        private string _id;
        private string _persid;
        private string _tipo;
        private string _referencia;
        private string _resumo;
        private string _descricao;
        private string _grupo;
        private string _categoria;
        private int _prioridade;
        private Status _statusChamado;
        private SLA _slaDefinida;
        private Severidade _nivel;
        private Contato _atendente;
        private DateTime? _data_abertura;
        private DateTime? _data_fechamento;
        private Maquina _maquina;
        private TimeSpan _tempoSLAContingencia;
        private TimeSpan _tempoSLA;
        private TimeSpan _tempoDescontos;
        private TimeSpan _tempoTotal;
        private List<Atividade> _log_atividades = new List<Atividade>();
        private List<Atividade> _log_atividades_Alternativo = new List<Atividade>();
        private Dictionary<TipoStatusChamado, TimeSpan> _tempoPorStatus = new Dictionary<TipoStatusChamado, TimeSpan>();
        private string _enderecoWeb;
        private string _responsavel;
        private Atividade _ultimaSolucao;
        private Atividade _contingenciaSolucao;

        private double _multa;        
        #endregion

        #region Métodos Get / Set
        public string Id
        {
            get { return _id; }
            set { _id = (string)value; }
        }
        public string Persid
        {
            get { return _persid; }
            set { _persid = (string)value; }
        }
        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = (string)value; }
        }
        public string Referencia
        {
            get { return _referencia; }
            set { _referencia = (string)value; }
        }
        public string Resumo
        {
            get { return _resumo; }
            set { _resumo = (string)value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = (string)value; }
        }
        public string Grupo
        {
            get { return _grupo; }
            set { _grupo = (string)value; }
        }
        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = (string)value; }
        }
        public int Prioridade
        {
            get { return _prioridade; }
            set { _prioridade = (int)value; }
        }
        public Status StatusChamado
        {
            get { return _statusChamado; }
            set { _statusChamado = (Status)value; }
        }
        public SLA SlaDefinida
        {
            get { return _slaDefinida; }
            set { _slaDefinida = (SLA)value; }
        }
        public Severidade Nivel
        {
            get { return _nivel; }
            set { _nivel = (Severidade)value; }
        }
        public string NivelFormatado
        {
            get { return _nivel.ToString(); }
        }
        public Contato Atendente
        {
            get { return _atendente; }
            set { _atendente = (Contato)value; }
        }
        public DateTime? Data_Abertura
        {
            get { return _data_abertura; }
            set { _data_abertura = (DateTime?)value; }
        }
        public DateTime? Data_Fechamento
        {
            get { return _data_fechamento; }
            set { _data_fechamento = (DateTime?)value; }
        }
        public Maquina MaquinaRelacionada
        {
            get { return _maquina; }
            set { _maquina = value; }
        }
        public TimeSpan TempoSLAContingencia
        {
            get { return _tempoSLAContingencia; }
            set { _tempoSLAContingencia = (TimeSpan)value; }
        }
        public string TempoSLAContingenciaFormatado
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ContingenciaAtiva"] == "N")
                    return "---";
                else
                    return Util.FormatarTimeSpan(_tempoSLAContingencia);
            }
        }
        public string TempoSLAContingenciaExcedidoFormatado
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ContingenciaAtiva"] == "N")
                    return "---";
                else
                {
                    if (TempoSLAContingencia.Subtract(SlaDefinida.LimiteContingencia) < TimeSpan.Zero)
                        return Util.FormatarTimeSpan(TimeSpan.Zero);
                    else
                        return Util.FormatarTimeSpan(TempoSLAContingencia.Subtract(SlaDefinida.LimiteContingencia));
                }
            }
        }
        public string TempoSLAContingenciaLimiteFormatado
        {
            get { return Util.FormatarTimeSpan(_slaDefinida.LimiteContingencia); }
        }
        public TimeSpan TempoSLA
        {
            get { return _tempoSLA; }
            set { _tempoSLA = (TimeSpan)value; }
        }
        public string TempoSLAFormatado
        {
            get { return Util.FormatarTimeSpan(_tempoSLA); }
        }
        public string TempoSLAExcedidoFormatado
        {
            get
            {
                if (TempoSLA.Subtract(SlaDefinida.Limite) < TimeSpan.Zero)
                    return Util.FormatarTimeSpan(TimeSpan.Zero);
                else
                    return Util.FormatarTimeSpan(TempoSLA.Subtract(SlaDefinida.Limite));
            }
        }
        public string TempoSLALimiteFormatado
        {
            get { return Util.FormatarTimeSpan(_slaDefinida.Limite); }
        }
        public int DiasSLA
        {
            get { return (int)_tempoSLA.TotalDays; }
        }
        public int LimiteDiasSLA
        {
            get { return (int)_slaDefinida.Limite.TotalDays; }
        }
        public int DiasSLAExcedido
        {
            get
            {
                if (TempoSLA.Subtract(SlaDefinida.Limite) < TimeSpan.Zero)
                    return 0;
                else
                    return (int)TempoSLA.Subtract(SlaDefinida.Limite).TotalDays;
            }
        }
        public TimeSpan TempoSLARestante
        {
            get
            {
                if ((SlaDefinida.Limite - TempoSLA) < TimeSpan.Zero)
                    return TimeSpan.Zero;
                else
                    return SlaDefinida.Limite - TempoSLA;
            }
        }
        public TimeSpan TempoSLAContingenciaRestante
        {
            get
            {
                if ((SlaDefinida.LimiteContingencia - TempoSLAContingencia) < TimeSpan.Zero)
                    return TimeSpan.Zero;
                else
                    return SlaDefinida.LimiteContingencia - TempoSLAContingencia;
            }
        }
        public string TempoSLARestanteFormatado
        {
            get { return Util.FormatarTimeSpan(TempoSLARestante); }
        }
        public string TempoSLAContingenciaRestanteFormatado
        {
            get { return Util.FormatarTimeSpan(TempoSLAContingenciaRestante); }
        }
        public TimeSpan TempoDescontos
        {
            get { return _tempoDescontos; }
            set { _tempoDescontos = (TimeSpan)value; }
        }
        public string TempoDescontosFormatado
        {
            get { return Util.FormatarTimeSpan(_tempoDescontos); }
        }
        public TimeSpan TempoTotal
        {
            get { return _tempoTotal; }
            set { _tempoTotal = (TimeSpan)value; }
        }
        public string TempoTotalFormatado
        {
            get { return Util.FormatarTimeSpan(_tempoTotal); }
        }
        public List<Atividade> Log_Atividades
        {
            get { return _log_atividades; }
            set { _log_atividades = value; }
        }
        public List<Atividade> Log_Atividades_Alternativo
        {
            get { return _log_atividades_Alternativo; }
            set { _log_atividades_Alternativo = value; }
        }
        public Dictionary<TipoStatusChamado, TimeSpan> TempoPorStatus
        {
            get { return _tempoPorStatus; }
            set { _tempoPorStatus = value; }
        }
        public string EnderecoWeb
        {
            get { return _enderecoWeb; }
            set { _enderecoWeb = value; }
        }
        public string Responsavel
        {
            get { return _responsavel; }
            set { _responsavel = value; }
        }
        public Atividade UltimaSolucao
        {
            get { return _ultimaSolucao; }
            set { _ultimaSolucao = value; }
        }
        public Atividade ContingenciaSolucao
        {
            get { return _contingenciaSolucao; }
            set { _contingenciaSolucao = value; }
        }
        public double Multa
        {
            get { return _multa; }
            set { _multa = value; }
        }

        public string Serie
        {
            get { return _maquina.Serie; }
        }

        public string StatusAtual
        {
            get { return _statusChamado.Descricao; }
        }

        public string Contato
        {
            get { return _atendente.Nome; }
        }

        public string DescricaoSolucaoContingencia
        {
            get
            {
                if (_contingenciaSolucao.Descricao != "")
                    return _contingenciaSolucao.Descricao;
                else
                    return "---";
            }
        }

        public string DescricaoSolucaoFinal
        {
            get
            {
                if (_contingenciaSolucao.Descricao != "")
                    return _ultimaSolucao.Descricao;
                else
                    return "---";
            }
        }

        public string Cidade
        {
            get { return _maquina.Local.Cidade; }
        }

        public string Uf
        {
            get { return _maquina.Local.Estado; }
        }

        public string Unidade
        {
            get { return _maquina.Local.NomeUnidade; }
        }

        public string Ambiente
        {
            get { return _maquina.Local.NomeAmbiente; }
        }

        public string TipoLocalUnidade
        {
            get { return _slaDefinida.LocalFormatado; }
        }

        public string DeAcordoSLAContingencia
        {
            get { return _slaDefinida.DeAcordoContingencia; }
        }

        public string DeAcordoSLAFinal
        {
            get { return _slaDefinida.DeAcordo; }
        }

        public float Faturamento
        {
            get { return _maquina.Bilhetagem; }
        }

        public DateTime? DataUltimaLeitura
        {
            get { return _maquina.DataUltimaLeitura; }
        }

        public string TempoUltimaLeituraFormatado
        {
            get
            {
                if (this.MaquinaRelacionada != null)
                {
                    if (this.MaquinaRelacionada.DataUltimaLeitura.HasValue)
                        return Util.FormatarTimeSpan(DateTime.Now.Date.Subtract(MaquinaRelacionada.DataUltimaLeitura.Value.Date));
                    else
                        return "---";
                }
                else
                    return "---";
            }
        }

        #endregion

        public override string ToString()
        {
            return String.Format("Chamado {0} :: Tipo {1} - Status: {2}",
                _referencia.ToString(),
                _tipo.ToString(),
                _statusChamado.ToString());
        }

        #region Métodos para montar nova lista de busca específica (Log de Atividades)
        public static string MontarListaPersid(List<Chamado> listaChamado)
        {
            if (listaChamado.Count == 0)
                return String.Empty;
            else
            {
                string listaId = String.Empty;

                foreach (Chamado chamado in listaChamado)
                    listaId = String.Concat(listaId, "'", chamado.Persid, "',");

                return listaId.Remove(listaId.Length - 1);
            }
        }
        public static string MontarListaReferencia(List<string> lista)
        {
            if (lista.Count == 0)
                return String.Empty;
            else
            {
                string listaId = String.Empty;

                foreach (string ocorrencia in lista)
                {
                    listaId = String.Concat(listaId, "'", ocorrencia, "',");
                }
                return listaId.Remove(listaId.Length - 1);
            }
        }
        #endregion

        #region Monta lista de chamados
        public static List<Chamado> MontaChamados(DateTime? dataInicial, DateTime? dataFinal, string chave, TipoChaves filtro, DateTime? dataInicialMulta, DateTime? dataFinalMulta)
        {
            //List<Chamado> listaChamados = new List<Chamado>();

            ///*
            List<Chamado> listaChamados = Chamado.BuscarDados(dataInicial, dataFinal, chave, filtro);
            Maquina.BuscarMaquinasCSF(listaChamados);
            Atividade.CarregarLogAtividades(listaChamados);
            Contabilizacao.ProcessarTempoAtividade(listaChamados);

            if (listaChamados.Count > 0)
            {
                if (dataInicialMulta.HasValue && dataFinalMulta.HasValue)
                    Maquina.ProcessarBilhetagem(listaChamados, dataInicial.Value, dataFinal.Value);
            }

            //Maquina.PassaDatasInstalacao(Excel.RetornarDatasInstalacao(), listaChamados);
            //*/

            return listaChamados;
        }
        #endregion

        #region Dados - Relatórios
        public static List<Chamado> BuscarDados(DateTime? dataInicial, DateTime? dataFinal, string chave, TipoChaves filtro)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" dbo.call_req.id AS ID,");
            sql.Append(" dbo.call_req.persid AS PERSID,");
            sql.Append(" dbo.crt.sym AS TIPO,");
            sql.Append(" dbo.call_req.ref_num AS REFERENCIA,");
            sql.Append(" dbo.call_req.summary AS RESUMO, ");
            sql.Append(" dbo.call_req.description AS DESCRICAO,");
            sql.Append(" GRP.last_name AS GRUPO,");
            sql.Append(" dbo.prob_ctg.sym AS CATEGORIA,");
            sql.Append(" dbo.call_req.priority AS PRIORIDADE, ");
            sql.Append(" dbo.call_req.status AS STATUS_CODE,");
            sql.Append(" dbo.cr_stat.sym AS STATUS,");
            sql.Append(" UPPER(LTRIM(RTRIM(CTT.last_name))) AS CONTATO,");
            sql.Append(" UPPER(LTRIM(RTRIM(CL.city))) AS CIDADE,");
            sql.Append(" UPPER(LTRIM(RTRIM(CSP.symbol))) AS ESTADO,");
            sql.Append(" UPPER(LTRIM(RTRIM(ORG.org_name))) AS NOME_UNIDADE,");
            sql.Append(" UPPER(LTRIM(RTRIM(RSP.last_name))) AS RESPONSAVEL,");
            sql.Append(" DATEADD(SECOND, dbo.call_req.open_date, '1969-12-31 21:00:00') AS DATA_ABERTURA,");
            sql.Append(" DATEADD(SECOND, dbo.call_req.close_date, '1969-12-31 21:00:00') AS DATA_FECHAMENTO, ");
            sql.Append(" 'http://d001sdk01/CAisd/pdmweb.exe?OP=SEARCH+FACTORY=cr+SKIPLIST=1+QBE.EQ.id=' + CAST(dbo.call_req.id AS VARCHAR(30)) AS ENDERECO_WEB");
            sql.Append(" FROM dbo.call_req WITH (NOLOCK) LEFT OUTER JOIN");
            sql.Append(" dbo.crt WITH (NOLOCK) ON dbo.call_req.type = dbo.crt.code LEFT OUTER JOIN");
            sql.Append(" dbo.cr_stat WITH (NOLOCK) ON dbo.call_req.status = dbo.cr_stat.code LEFT OUTER JOIN");
            sql.Append(" dbo.prob_ctg WITH (NOLOCK) ON dbo.call_req.category = dbo.prob_ctg.persid LEFT OUTER JOIN");
            sql.Append(" dbo.ca_contact AS GRP WITH (NOLOCK) ON dbo.call_req.group_id = GRP.contact_uuid LEFT OUTER JOIN");
            sql.Append(" dbo.ca_contact AS CTT WITH (NOLOCK) ON dbo.call_req.customer = CTT.contact_uuid LEFT OUTER JOIN");
            sql.Append(" dbo.ca_contact AS RSP WITH (NOLOCK) ON dbo.call_req.assignee = RSP.contact_uuid LEFT OUTER JOIN");
            sql.Append(" dbo.ca_organization AS ORG WITH (NOLOCK) ON CTT.organization_uuid = ORG.organization_uuid LEFT OUTER JOIN");
            sql.Append(" dbo.ca_location AS CL WITH (NOLOCK) ON ORG.location_uuid = CL.location_uuid LEFT OUTER JOIN");
            sql.Append(" dbo.ca_state_province AS CSP WITH (NOLOCK) ON CL.state = CSP.id");

            Parametro ChamadosNoGrupo = Parametro.Parametros.Find(p => p.Nome == "ChamadosNoGrupo");

            if (ChamadosNoGrupo != null)
                sql.Append(" WHERE (GRP.last_name in (" + ChamadosNoGrupo.Valor + "))");
            else
                sql.Append(" WHERE (GRP.last_name = 'Xerox')");

            if (filtro == TipoChaves.NumChamado)
            {
                sql.Append(" AND dbo.call_req.ref_num like '%" + chave + "%'");
            }
            else if (filtro == TipoChaves.NumSerie)
            {
                sql.Append(" AND dbo.call_req.ref_num in (");
                sql.Append(" SELECT dbo.call_req.ref_num REFERENCIA");
                sql.Append(" FROM dbo.call_req (nolock) LEFT OUTER JOIN");
                sql.Append(" dbo.cr_prp (nolock) ON dbo.call_req.persid = dbo.cr_prp.owning_cr");
                sql.Append(" WHERE dbo.cr_prp.label = 'Número de Série'");
                sql.Append(" and dbo.cr_prp.value like '%" + chave + "%'");
                sql.Append(" )");
            }
            else if (filtro == TipoChaves.Ativas)
            {
                sql.Append(" AND " + Util.RetornaFiltroData(filtro));
            }
            else if (filtro == TipoChaves.AbertosOuFechados)
            {
                //sql.Append(" AND ((DATEADD(SECOND, dbo.call_req.open_date, '1969-12-31 21:00:00') >= @dtinicial");
                //sql.Append(" AND DATEADD(SECOND, dbo.call_req.open_date, '1969-12-31 21:00:00') <= @dtfinal)");
                //sql.Append(" or (DATEADD(SECOND, dbo.call_req.close_date, '1969-12-31 21:00:00') >= @dtinicial");
                //sql.Append(" AND DATEADD(SECOND, dbo.call_req.close_date, '1969-12-31 21:00:00') <= @dtfinal))");

                sql.Append(" AND ((dbo.call_req.close_date is null) OR");
                sql.Append(" (DATEADD(SECOND, dbo.call_req.close_date, '1969-12-31 21:00:00') >= @dtinicial");
                sql.Append(" AND DATEADD(SECOND, dbo.call_req.close_date, '1969-12-31 21:00:00') <= @dtfinal) OR");
                sql.Append(" (DATEADD(SECOND, dbo.call_req.open_date, '1969-12-31 21:00:00') >= @dtinicial");
                sql.Append(" AND DATEADD(SECOND, dbo.call_req.open_date, '1969-12-31 21:00:00') <= @dtfinal))");
            }
            else
            {
                sql.Append(" AND DATEADD(SECOND, " + Util.RetornaFiltroData(filtro) + ", '1969-12-31 21:00:00') >= @dtinicial");
                sql.Append(" AND DATEADD(SECOND, " + Util.RetornaFiltroData(filtro) + ", '1969-12-31 21:00:00') <= @dtfinal");
            }

            sql.Append(" ORDER BY dbo.call_req.open_date");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao);

            if (filtro == TipoChaves.AbertosOuFechados || filtro == TipoChaves.DtAbertura || filtro == TipoChaves.DtFechamento)
            {
                if (dataInicial.HasValue && dataFinal.HasValue)
                {
                    banco.AddParameter(cmd, "dtinicial", dataInicial, SqlDbType.DateTime, ParameterDirection.Input);
                    banco.AddParameter(cmd, "dtfinal", dataFinal, SqlDbType.DateTime, ParameterDirection.Input);
                }
            }

            return ProcessarChamados(banco.ExecuteDataTable(cmd));
        }
        #endregion

        #region Monta a lista de Chamados
        private static List<Chamado> ProcessarChamados(DataTable dataTableUSD)
        {
            List<Chamado> lista = new List<Chamado>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Chamado chamado = ConstruirChamado(rowReader);

                lista.Add(chamado);
            }
            return lista;
        }

        private static Chamado ConstruirChamado(NullableDataRowReader rowReader)
        {
            Chamado chamado = new Chamado();

            chamado.Id = rowReader.GetString("ID");
            chamado.Persid = rowReader.GetString("PERSID");
            chamado.Referencia = rowReader.GetString("REFERENCIA");
            chamado.Tipo = rowReader.GetString("TIPO");
            chamado.Resumo = rowReader.GetString("RESUMO");
            chamado.Descricao = rowReader.GetString("DESCRICAO");
            chamado.Grupo = rowReader.GetNullableString("GRUPO");
            chamado.Categoria = rowReader.GetNullableString("CATEGORIA");
            chamado.Prioridade = rowReader.GetInt32("PRIORIDADE");
            chamado.Nivel = RetornaNivelSeveridade(chamado.Prioridade);
            chamado.StatusChamado = Status.ConvertCodeToStatus(rowReader.GetString("STATUS_CODE"), rowReader.GetString("STATUS"));
            chamado.Responsavel = rowReader.GetNullableString("RESPONSAVEL");

            if (chamado.Categoria == null)
                chamado.Categoria = "";

            if (chamado.Responsavel == null)
                chamado.Responsavel = "";

            if (chamado.Categoria == "EQUIPAMENTOS DE TI.IMPRESSORAS.XEROX.FUNCIONAMENTO PARCIAL")
            {
                chamado.Prioridade = 3;
                chamado.Nivel = Severidade.Baixa;
            }

            if (rowReader.GetNullableString("CONTATO") != null)
            {
                chamado.Atendente = new Contato();
                chamado.Atendente.Nome = rowReader.GetNullableString("CONTATO");

                if (rowReader.GetNullableString("CIDADE") != null)
                {
                    chamado.Atendente.LocalContato = new Localidade();
                    chamado.Atendente.LocalContato.Cidade = rowReader.GetNullableString("CIDADE");
                    chamado.Atendente.LocalContato.Estado = rowReader.GetNullableString("ESTADO");
                    chamado.Atendente.LocalContato.LocalRegiao = Localidade.RetornaRegiao(chamado.Atendente.LocalContato.Estado);

                    chamado.Atendente.LocalContato.NomeUnidade = rowReader.GetNullableString("NOME_UNIDADE");

                    if (chamado.Atendente.LocalContato.NomeUnidade.Contains("AGENCIA"))
                    {
                        chamado.Atendente.LocalContato.TipoUnidade = "AGENCIA";

                        if (Localidade.EhCapital(chamado.Atendente.LocalContato.Cidade))
                            chamado.Atendente.LocalContato.Tipo_Localidade = Localidade.TipoLocalidade.Capital;
                        else
                            chamado.Atendente.LocalContato.Tipo_Localidade = Localidade.TipoLocalidade.Interior;
                    }
                    else
                        chamado.Atendente.LocalContato.Tipo_Localidade = Localidade.TipoLocalidade.CAPGV;
                }
            }

            if (chamado.Categoria.Contains("TONNER"))
            {
                if (chamado.Atendente.LocalContato != null)
                    if (chamado.Atendente.LocalContato.Tipo_Localidade == Localidade.TipoLocalidade.CAPGV)
                        chamado.Atendente.LocalContato.Tipo_Localidade = Localidade.TipoLocalidade.Capital;

                chamado.SlaDefinida = new SLA(1, 1, Severidade.Baixa, new TimeSpan(), Localidade.TipoLocalidade.Vazio, Localidade.Regiao.Vazio, "Indefinido");
            }
            else
            {
                SLA slaFind = SLA.SLA_AtendimentoTecnico().Find(sla => sla.Nivel == chamado.Nivel && sla.Local == Localidade.TipoLocalidade.Interior);

                if (slaFind != null)
                    chamado.SlaDefinida = new SLA(slaFind.Id, slaFind.Prioridade, slaFind.Nivel, slaFind.Limite, slaFind.Local, slaFind.LocalRegiao, slaFind.DeAcordo);
            }

            chamado.Data_Abertura = rowReader.GetNullableDateTime("DATA_ABERTURA");
            chamado.Data_Fechamento = rowReader.GetNullableDateTime("DATA_FECHAMENTO");
            chamado.EnderecoWeb = rowReader.GetNullableString("ENDERECO_WEB");

            chamado.MaquinaRelacionada = new Maquina();
            chamado.MaquinaRelacionada.Serie = "";
            chamado.MaquinaRelacionada.Ip = "";
            chamado.MaquinaRelacionada.SLO = "";
            chamado.MaquinaRelacionada.Local = new Localidade();
            chamado.MaquinaRelacionada.Local.Cidade = "";
            chamado.MaquinaRelacionada.Local.Estado = "";
            chamado.MaquinaRelacionada.Local.LocalRegiao = Localidade.Regiao.Vazio;
            chamado.MaquinaRelacionada.Local.NomeUnidade = "";
            chamado.MaquinaRelacionada.Local.Tipo_Localidade = Localidade.TipoLocalidade.Vazio;
            chamado.MaquinaRelacionada.Local.TipoUnidade = "";

            return chamado;
        }
        #endregion
    }
}