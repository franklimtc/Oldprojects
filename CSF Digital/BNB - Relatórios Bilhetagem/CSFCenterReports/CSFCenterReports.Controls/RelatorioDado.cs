using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class RelatorioDado
    {
        #region Atributos
        private string _NomeRelatorio;
        private string _NomeDataSet;
        private string _NomeDataSource;
        private string _Comando;
        private string _NomeCampo;
        private string _NomeCampoDado;
        private string _TipoCampoDado;
        private Relatorio _RelatorioRelacionado;
        #endregion

        #region Métodos Get / Set
        public string NomeRelatorio
        {
            get { return _NomeRelatorio; }
            set { _NomeRelatorio = value; }
        }
        public string NomeDataSet
        {
            get { return _NomeDataSet; }
            set { _NomeDataSet = value; }
        }
        public string NomeDataSource
        {
            get { return _NomeDataSource; }
            set { _NomeDataSource = value; }
        }
        public string Comando
        {
            get { return _Comando; }
            set { _Comando = value; }
        }
        public string NomeCampo
        {
            get { return _NomeCampo; }
            set { _NomeCampo = value; }
        }
        public string NomeCampoDado
        {
            get { return _NomeCampoDado; }
            set { _NomeCampoDado = value; }
        }
        public string TipoCampoDado
        {
            get { return _TipoCampoDado; }
            set { _TipoCampoDado = value; }
        }
        public Relatorio RelatorioRelacionado
        {
            get { return _RelatorioRelacionado; }
            set { _RelatorioRelacionado = value; }
        }
        
        #endregion

        #region Consulta - Dados
        public static List<RelatorioDado> RetornaDados(string relatorio)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" WITH XMLNAMESPACES (DEFAULT 'http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition', 'http://schemas.microsoft.com/SQLServer/reporting/reportdesigner' AS rd)");
            sql.Append(" SELECT DISTINCT");
            sql.Append(" ReportName = name,");
            sql.Append(" DataSetName = x.value('(@Name)[1]', 'VARCHAR(MAX)'),");
            sql.Append(" DataSourceName = x.value('(Query/DataSourceName)[1]','VARCHAR(MAX)'),");
            sql.Append(" CommandText = x.value('(Query/CommandText)[1]','VARCHAR(MAX)'),");
            sql.Append(" Fields = df.value('(@Name)[1]','VARCHAR(MAX)'),");
            sql.Append(" DataField = df.value('(DataField)[1]','VARCHAR(MAX)'),");
            sql.Append(" DataType = df.value('(rd:TypeName)[1]','VARCHAR(MAX)')");
            sql.Append(" FROM (SELECT C.Name,CONVERT(XML,CONVERT(VARBINARY(MAX),C.Content)) AS reportXML ");
            sql.Append(" FROM ReportServer.dbo.Catalog C ");
            sql.Append(" WHERE C.Content is not null ");
            sql.Append(" AND C.Type = 2 ");
            sql.Append(" AND C.Path = '" + relatorio + "'");
            sql.Append(" ) a ");
            sql.Append(" CROSS APPLY reportXML.nodes('/Report/DataSets/DataSet') r ( x ) ");
            sql.Append(" CROSS APPLY x.nodes('Fields/Field') f(df)  ");
            sql.Append(" ORDER BY name ");

            //return ProcessarDados(Banco.RetornarTabelaReportServer(sql.ToString()));
            return new List<RelatorioDado>();
        }
        #endregion

        #region Monta a lista de Parametros
        private static List<RelatorioDado> ProcessarDados(DataTable dataTableUSD)
        {
            List<RelatorioDado> lista = new List<RelatorioDado>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                RelatorioDado dado = ConstruirRelatorioParametro(rowReader);

                lista.Add(dado);
            }

            return lista;
        }

        private static RelatorioDado ConstruirRelatorioParametro(NullableDataRowReader rowReader)
        {
            RelatorioDado dado = new RelatorioDado();

            dado.NomeRelatorio = rowReader.GetNullableString("ReportName");
            dado.NomeDataSet = rowReader.GetNullableString("DataSetName");
            dado.NomeDataSource = rowReader.GetNullableString("DataSourceName");
            dado.Comando = rowReader.GetNullableString("CommandText");
            dado.NomeCampo = rowReader.GetNullableString("Fields");
            dado.NomeCampoDado = rowReader.GetNullableString("DataField");
            dado.TipoCampoDado = rowReader.GetNullableString("DataType");

            return dado;
        }
        #endregion
    }
}