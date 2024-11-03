using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class RelatorioParametro
    {
        #region Atributos
        private string _Nome;
        private string _Tipo;
        private bool _Nulo;
        private bool _Vazio;
        private bool _MultiplosValores;
        private bool _UsadoNaQuery;
        private string _Campo;
        private bool _CampoDinamico;
        private bool _ExibirUsuario;
        private string _Estado;
        #endregion

        #region Métodos Get / Set
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public bool Nulo
        {
            get { return _Nulo; }
            set { _Nulo = value; }
        }
        public bool Vazio
        {
            get { return _Vazio; }
            set { _Vazio = value; }
        }
        public bool MultiplosValores
        {
            get { return _MultiplosValores; }
            set { _MultiplosValores = value; }
        }
        public bool UsadoNaQuery
        {
            get { return _UsadoNaQuery; }
            set { _UsadoNaQuery = value; }
        }
        public string Campo
        {
            get { return _Campo; }
            set { _Campo = value; }
        }
        public bool CampoDinamico
        {
            get { return _CampoDinamico; }
            set { _CampoDinamico = value; }
        }
        public bool ExibirUsuario
        {
            get { return _ExibirUsuario; }
            set { _ExibirUsuario = value; }
        }
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        #endregion

        #region Consulta - Parâmetros
        public static List<RelatorioParametro> RetornaParametros(string relatorio)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT DISTINCT");
            sql.Append(" Name = Paravalue.value('Name[1]', 'VARCHAR(MAX)'),");
            sql.Append(" Type = Paravalue.value('Type[1]', 'VARCHAR(MAX)'),");
            sql.Append(" Nullable = Paravalue.value('Nullable[1]', 'VARCHAR(MAX)'),");
            sql.Append(" AllowBlank = Paravalue.value('AllowBlank[1]', 'VARCHAR(MAX)'),");
            sql.Append(" MultiValue = Paravalue.value('MultiValue[1]', 'VARCHAR(MAX)'),");
            sql.Append(" UsedInQuery = Paravalue.value('UsedInQuery[1]', 'VARCHAR(MAX)'),");
            sql.Append(" Prompt = Paravalue.value('Prompt[1]', 'VARCHAR(MAX)'),");
            sql.Append(" DynamicPrompt = Paravalue.value('DynamicPrompt[1]', 'VARCHAR(MAX)'),");
            sql.Append(" PromptUser = Paravalue.value('PromptUser[1]', 'VARCHAR(MAX)'),");
            sql.Append(" State = Paravalue.value('State[1]', 'VARCHAR(MAX)')");
            sql.Append(" FROM (");
            sql.Append(" SELECT C.Name,CONVERT(XML,C.Parameter) AS ParameterXML");
            sql.Append(" FROM  ReportServer.dbo.Catalog C");
            sql.Append(" WHERE  C.Content is not null");
            sql.Append(" AND C.Path = '" + relatorio + "'");
            sql.Append(" AND C.Type = 2");
            sql.Append(" ) a ");
            sql.Append(" CROSS APPLY ParameterXML.nodes('//Parameters/Parameter') p ( Paravalue ) ");

            //return ProcessarParametros(Banco.RetornarTabelaReportServer(sql.ToString()));
            return new List<RelatorioParametro>();
        }
        #endregion

        #region Monta a lista de Parametros
        private static List<RelatorioParametro> ProcessarParametros(DataTable dataTableUSD)
        {
            List<RelatorioParametro> lista = new List<RelatorioParametro>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                RelatorioParametro parametro = ConstruirRelatorioParametro(rowReader);

                lista.Add(parametro);
            }

            return lista;
        }

        private static RelatorioParametro ConstruirRelatorioParametro(NullableDataRowReader rowReader)
        {
            RelatorioParametro parametro = new RelatorioParametro();

            parametro.Nome = rowReader.GetString("Name");
            parametro.Tipo = rowReader.GetString("Type");
            parametro.Nulo = rowReader.GetBoolean("Nullable");
            parametro.Vazio = rowReader.GetBoolean("AllowBlank");
            parametro.MultiplosValores = rowReader.GetBoolean("MultiValue");
            parametro.UsadoNaQuery = rowReader.GetBoolean("UsedInQuery");
            parametro.Campo = rowReader.GetString("Prompt");
            parametro.CampoDinamico = rowReader.GetBoolean("DynamicPrompt");
            parametro.ExibirUsuario = rowReader.GetBoolean("PromptUser");
            parametro.Estado = rowReader.GetString("State");

            return parametro;
        }
        #endregion
    }
}