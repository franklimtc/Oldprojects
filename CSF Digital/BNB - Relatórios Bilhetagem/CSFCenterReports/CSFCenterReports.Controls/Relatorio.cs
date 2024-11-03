using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Relatorio
    {
        #region Atributos
        private string _Pasta;
        private string _Diretorio;
        private string _Nome;
        private DateTime? _CriadoEm;
        private DateTime? _ModificadoEm;
        private int? _TotalExecucoes;
        private List<RelatorioPermissao> _Permissoes = new List<RelatorioPermissao>();
        private List<RelatorioFonte> _Fontes = new List<RelatorioFonte>();
        private List<RelatorioDado> _Dados = new List<RelatorioDado>();
        private List<RelatorioParametro> _Parametros = new List<RelatorioParametro>();
        #endregion

        #region Métodos Get / Set
        public string Pasta
        {
            get { return _Pasta; }
            set { _Pasta = value; }
        }
        public string Diretorio
        {
            get { return _Diretorio; }
            set { _Diretorio = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public DateTime? CriadoEm
        {
            get { return _CriadoEm; }
            set { _CriadoEm = value; }
        }
        public DateTime? ModificadoEm
        {
            get { return _ModificadoEm; }
            set { _ModificadoEm = value; }
        }
        public int? TotalExecucoes
        {
            get { return _TotalExecucoes; }
            set { _TotalExecucoes = value; }
        }
        public List<RelatorioPermissao> Permissoes
        {
            get { return _Permissoes; }
            set { _Permissoes = value; }
        }
        public List<RelatorioFonte> Fontes
        {
            get { return _Fontes; }
            set { _Fontes = value; }
        }
        public List<RelatorioDado> Dados
        {
            get { return _Dados; }
            set { _Dados = value; }
        }
        public List<RelatorioParametro> Parametros
        {
            get { return _Parametros; }
            set { _Parametros = value; }
        }
        #endregion

        #region Consulta - Relatórios
        public static List<Relatorio> RetornaRelatorios()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" CatalogParent.Name AS PASTA,");
            sql.Append(" Catalog.Path AS DIRETORIO,");
            sql.Append(" Catalog.Name AS NOME,");
            sql.Append(" Catalog.CreationDate AS DTCRIACAO,");
            sql.Append(" Catalog.ModifiedDate AS DTMODIFICACAO,");
            sql.Append(" CountExecution.CountStart AS TOTALEXECUCOES");
            sql.Append(" FROM Catalog INNER JOIN");
            sql.Append(" Catalog AS CatalogParent ON Catalog.ParentID = CatalogParent.ItemID LEFT OUTER JOIN");
            sql.Append(" (SELECT ReportID, COUNT(TimeStart) AS CountStart");
            sql.Append(" FROM ExecutionLog GROUP BY ReportID) AS CountExecution ON Catalog.ItemID = CountExecution.ReportID");
            sql.Append(" WHERE (Catalog.Type = 2)");

            Parametro PastaReportServer = Parametro.RetornaParametro("PastaReportServer");

            if (PastaReportServer != null)
                sql.Append(" AND (Catalog.Path LIKE '%/" + PastaReportServer.Valor + "%')");
            else
                sql.Append(" AND (Catalog.Path LIKE '%/Relatórios BNB%')");

            sql.Append(" ORDER BY 1, 2");

            //return ProcessarRelatorios(Banco.RetornarTabelaReportServer(sql.ToString()));
            return new List<Relatorio>();
        }
        #endregion

        #region Monta a lista de Relatorios
        private static List<Relatorio> ProcessarRelatorios(DataTable dataTableUSD)
        {
            List<Relatorio> lista = new List<Relatorio>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Relatorio relatorio = ConstruirRelatorio(rowReader);

                lista.Add(relatorio);
            }

            return lista;
        }

        private static Relatorio ConstruirRelatorio(NullableDataRowReader rowReader)
        {
            Relatorio relatorio = new Relatorio();

            relatorio.Pasta = rowReader.GetString("PASTA");
            relatorio.Diretorio = rowReader.GetString("DIRETORIO");
            relatorio.Nome = rowReader.GetString("NOME");
            relatorio.CriadoEm = rowReader.GetNullableDateTime("DTCRIACAO");
            relatorio.ModificadoEm = rowReader.GetNullableDateTime("DTMODIFICACAO");
            relatorio.TotalExecucoes = rowReader.GetNullableInt32("TOTALEXECUCOES");

            return relatorio;
        }
        #endregion
    }
}