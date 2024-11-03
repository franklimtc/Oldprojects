using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace CSFDigital.Controls
{
    public class Atividade
    {
        #region Atributos
        private int _id;
        private string _persidReq;
        private string _referencia;
        private string _atuante;
        private string _descricao_Fluxo;
        private string _descricao;
        private TimeSpan _tmpDocumetacao;
        private TipoAtividade _tipo_Atividade;
        private DateTime? _data_Atividade;
        private TipoStatusChamado _status_Chamado = TipoStatusChamado.Null;

        public enum TipoAtividade
        {
            Outros,
            AtualizarStatus,
            Fechado,
            Inicio,
            Solucao,
            Transfer
        }
        #endregion

        #region Métodos Get / Set
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string PersidReq
        {
            get { return _persidReq; }
            set { _persidReq = value; }
        }
        public TipoAtividade Tipo_Atividade
        {
            get { return _tipo_Atividade; }
            set { _tipo_Atividade = value; }
        }
        public string Descricao_Fluxo
        {
            get { return _descricao_Fluxo; }
            set { _descricao_Fluxo = value; }
        }
        public DateTime? Data_Atividade
        {
            get { return _data_Atividade.Value; }
            set { _data_Atividade = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public string Atuante
        {
            get { return _atuante; }
            set { _atuante = value; }
        }
        public string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }
        public TimeSpan TempoDocumetacao
        {
            get { return _tmpDocumetacao; }
            set { _tmpDocumetacao = value; }
        }
        public TipoStatusChamado Status_Chamado
        {
            get { return _status_Chamado; }
            set { _status_Chamado = value; }
        }
        #endregion propriedades

        public override string ToString()
        {
            return String.Format("[{0}] Tipo de ação: {1} - Status: {2} - Data: {3}",
                _referencia.ToString(),
                _tipo_Atividade.ToString(),
                _status_Chamado.ToString(),
                _data_Atividade.ToString());
        }

        #region Método para obter o tipo da Atividade do Log de Atividades
        public static TipoAtividade ConvertToTipoAtividade(string code)
        {
            switch (code)
            {
                case "ST":                          // ST - Tipo: Atualização de Status
                    return TipoAtividade.AtualizarStatus;
                case "CL":                          // CL - Tipo: Fechado
                    return TipoAtividade.Fechado;
                case "INIT":                        // INIT - Tipo: Aberto(Iniciado)
                    return TipoAtividade.Inicio;
                case "SOLN":                        // SOLN - Tipo: Resolvido(Solução)
                    return TipoAtividade.Solucao;
                case "TR":                          // TR - Tipo: Tranferencia de Grupo ou Responsável
                    return TipoAtividade.Transfer;
                case "RO":                          // RO - Tipo: Reaberto
                    return TipoAtividade.AtualizarStatus;
                default:                            // Padrão - Outros
                    return TipoAtividade.Outros;
            }
        }
        #endregion

        #region Buscar e montar o log de atividades
        public static void CarregarLogAtividades(List<Chamado> listaChamados)
        {
            if (listaChamados.Count > 0)
            {
                string listaReqId = Chamado.MontarListaPersid(listaChamados);

                if (!String.IsNullOrEmpty(listaReqId))
                {
                    StringBuilder sql = new StringBuilder();

                    sql.Append(" SELECT ");
                    sql.Append(" dbo.act_log.id AS ID,");
                    sql.Append(" dbo.act_log.call_req_id AS REQ_ID,");
                    sql.Append(" dbo.act_log.type AS TIPO,");
                    sql.Append(" act_log.description AS DESCRICAO,");
                    sql.Append(" dbo.act_log.action_desc AS DESCRICAO_FLUXO,");
                    sql.Append(" DATEADD(second, dbo.act_log.time_stamp, '1969-12-31 21:00:00') AS DATA,");
                    sql.Append(" UPPER(LTRIM(RTRIM(ca_contact.last_name))) AS ATUANTE,");
                    sql.Append(" dbo.act_log.time_spent AS TMP_DOC");
                    sql.Append(" FROM dbo.act_log WITH (nolock) INNER JOIN");
                    sql.Append(" dbo.call_req WITH (nolock) ON dbo.act_log.call_req_id = dbo.call_req.persid LEFT OUTER JOIN");
                    sql.Append(" dbo.ca_contact WITH (nolock) ON dbo.act_log.analyst = dbo.ca_contact.contact_uuid");
                    sql.Append(" WHERE dbo.act_log.type IN ('INIT','ST','CL','SOLN','RO') ");
                    sql.Append(" and dbo.act_log.action_desc not like 'Nenhuma Mudança'");
                    sql.Append(String.Format(" and dbo.act_log.call_req_id IN ({0}) ", listaReqId));

                    sql.Append(" ORDER BY REQ_ID, DATA ");

                    Banco banco = new Banco();

                    ConstruirLogAtividades(banco.ExecuteDataTable(sql.ToString()), listaChamados);
                }
            }
        }

        private static void ConstruirLogAtividades(DataTable dataTableUSD, List<Chamado> listaChamados)
        {
            string reqId = String.Empty;
            Chamado chamado = null;

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                if (reqId != row["REQ_ID"].ToString())
                {
                    reqId = row["REQ_ID"].ToString();

                    chamado = listaChamados.Find(c => c.Persid == reqId);

                    if (chamado != null)
                        chamado.Log_Atividades.Add(ConstruirAtividade(rowReader));
                }
                else
                {
                    if (chamado != null)
                        chamado.Log_Atividades.Add(ConstruirAtividade(rowReader));
                }
            }
        }

        private static Atividade ConstruirAtividade(NullableDataRowReader rowReader)
        {
            Atividade atividade = new Atividade();

            atividade.Id = rowReader.GetInt32("ID");
            atividade.PersidReq = rowReader.GetString("REQ_ID");
            atividade.Tipo_Atividade = ConvertToTipoAtividade(rowReader.GetString("TIPO"));
            atividade.Descricao = rowReader.GetNullableString("DESCRICAO");
            atividade.Descricao_Fluxo = rowReader.GetNullableString("DESCRICAO_FLUXO");
            atividade.Data_Atividade = rowReader.GetDateTime("DATA");
            atividade.Atuante = rowReader.GetString("ATUANTE");

            atividade.Status_Chamado = (TipoStatusChamado)Util.ObterStatus(atividade.Tipo_Atividade, atividade.Descricao_Fluxo);
            atividade.TempoDocumetacao = new TimeSpan(0, 0, 0);

            string tmpDoc = rowReader.GetNullableString("TMP_DOC");

            if (atividade.Descricao == null)
                atividade.Descricao = "";

            if (atividade.Descricao_Fluxo == null)
                atividade.Descricao_Fluxo = "";

            if (tmpDoc != null)
            {
                if (tmpDoc != "")
                    atividade.TempoDocumetacao = new TimeSpan(0, 0, int.Parse(tmpDoc));
            }

            return atividade;
        }
        #endregion

        #region Buscar e montar o log de atividades - Alternativo
        public static void CarregarLogAtividadesAlternativo(List<Chamado> listaChamados)
        {
            if (listaChamados.Count > 0)
            {
                string listaReqId = Chamado.MontarListaPersid(listaChamados);

                if (!String.IsNullOrEmpty(listaReqId))
                {
                    StringBuilder sql = new StringBuilder();

                    sql.Append(" SELECT ");
                    sql.Append(" dbo.act_log.id AS ID,");
                    sql.Append(" dbo.act_log.call_req_id AS REQ_ID,");
                    sql.Append(" dbo.act_log.type AS TIPO,");
                    sql.Append(" act_log.description AS DESCRICAO,");
                    sql.Append(" dbo.act_log.action_desc AS DESCRICAO_FLUXO,");
                    sql.Append(" DATEADD(second, dbo.act_log.time_stamp, '1969-12-31 21:00:00') AS DATA,");
                    sql.Append(" UPPER(LTRIM(RTRIM(ca_contact.last_name))) AS ATUANTE,");
                    sql.Append(" dbo.act_log.time_spent AS TMP_DOC");
                    sql.Append(" FROM dbo.act_log WITH (nolock) INNER JOIN");
                    sql.Append(" dbo.call_req WITH (nolock) ON dbo.act_log.call_req_id = dbo.call_req.persid LEFT OUTER JOIN");
                    sql.Append(" dbo.ca_contact WITH (nolock) ON dbo.act_log.analyst = dbo.ca_contact.contact_uuid");
                    sql.Append(" WHERE dbo.act_log.type IN ('LOG','ST','CL','INIT','NF','RO','RE','CB','SOLN','TR') ");
                    sql.Append(String.Format(" and dbo.act_log.call_req_id IN ({0}) ", listaReqId));

                    sql.Append(" ORDER BY REQ_ID, DATA ");

                    Banco banco = new Banco();

                    ConstruirLogAtividadesAlternativo(banco.ExecuteDataTable(sql.ToString()), listaChamados);
                }
            }
        }

        private static void ConstruirLogAtividadesAlternativo(DataTable dataTableUSD, List<Chamado> listaChamados)
        {
            string reqId = String.Empty;
            Chamado chamado = null;

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                if (reqId != row["REQ_ID"].ToString())
                {
                    reqId = row["REQ_ID"].ToString();

                    chamado = listaChamados.Find(c => c.Persid == reqId);

                    if (chamado != null)
                        chamado.Log_Atividades_Alternativo.Add(ConstruirAtividade(rowReader));
                }
                else
                {
                    if (chamado != null)
                        chamado.Log_Atividades_Alternativo.Add(ConstruirAtividade(rowReader));
                }
            }
        }
        #endregion
    }
}
