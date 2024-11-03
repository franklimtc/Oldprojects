using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class RelatorioFonte
    {
        #region Atributos
        private string _NomeRelatorio;
        private string _NomeDataSource;
        private string _NomeDataProvider;
        private string _Conexao;
        private Relatorio _RelatorioRelacionado;
        #endregion

        #region Métodos Get / Set
        public string NomeRelatorio
        {
            get { return _NomeRelatorio; }
            set { _NomeRelatorio = value; }
        }
        public string NomeDataSource
        {
            get { return _NomeDataSource; }
            set { _NomeDataSource = value; }
        }
        public string NomeDataProvider
        {
            get { return _NomeDataProvider; }
            set { _NomeDataProvider = value; }
        }
        public string Conexao
        {
            get { return _Conexao; }
            set { _Conexao = value; }
        }
        public Relatorio RelatorioRelacionado
        {
            get { return _RelatorioRelacionado; }
            set { _RelatorioRelacionado = value; }
        }        
        #endregion

        #region Consulta - Fontes
        public static List<RelatorioFonte> RetornaFontes(string relatorio)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" WITH XMLNAMESPACES ( DEFAULT 'http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition', 'http://schemas.microsoft.com/SQLServer/reporting/reportdesigner' AS rd)");
            sql.Append(" SELECT DISTINCT");
            sql.Append(" ReportName = Path,");
            sql.Append(" DataSourceName = x.value('(@Name)[1]', 'VARCHAR(MAX)'),");
            sql.Append(" DataProvider = x.value('(ConnectionProperties/DataProvider)[1]','VARCHAR(MAX)'),");
            sql.Append(" ConnectionString = x.value('(ConnectionProperties/ConnectString)[1]','VARCHAR(MAX)')");
            sql.Append(" FROM (SELECT C.Path,C.Name,CONVERT(XML,CONVERT(VARBINARY(MAX),C.Content)) AS reportXML");
            sql.Append(" FROM  ReportServer.dbo.Catalog C ");
            sql.Append(" WHERE  C.Content is not null ");
            sql.Append(" AND C.Type = 2 ");
            sql.Append(" AND C.Path = '" + relatorio + "'");
            sql.Append(" ) a ");
            sql.Append(" CROSS APPLY reportXML.nodes('/Report/DataSources/DataSource') r ( x )");
            sql.Append(" ORDER BY 1");

            //return ProcessarFontes(Banco.RetornarTabelaReportServer(sql.ToString()));
            return new List<RelatorioFonte>();
        }
        #endregion

        #region Monta a lista de Parametros
        private static List<RelatorioFonte> ProcessarFontes(DataTable dataTableUSD)
        {
            List<RelatorioFonte> lista = new List<RelatorioFonte>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                RelatorioFonte fonte = ConstruirRelatorioParametro(rowReader);

                lista.Add(fonte);
            }

            return lista;
        }

        private static RelatorioFonte ConstruirRelatorioParametro(NullableDataRowReader rowReader)
        {
            RelatorioFonte fonte = new RelatorioFonte();

            fonte.NomeRelatorio = rowReader.GetNullableString("ReportName");
            fonte.NomeDataSource = rowReader.GetNullableString("DataSourceName");
            fonte.NomeDataProvider = rowReader.GetNullableString("DataProvider");
            fonte.Conexao = rowReader.GetNullableString("ConnectionString");

            return fonte;
        }
        #endregion
    }
}