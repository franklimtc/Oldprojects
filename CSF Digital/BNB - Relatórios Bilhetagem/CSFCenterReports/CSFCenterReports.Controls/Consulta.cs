using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Consulta
    {
        #region Atributos
        private int _Id;
        private string _CodUsuario;
        private int _CodRelatorio;
        private string _Uf;
        private string _Cidade;
        private string _Unidade;
        private string _Celula;
        private string _Ambiente;
        private string _Impressoras;
        private string _Usuarios;
        private string _FormatoFolha;
        private string _Cor;
        private DateTime _DtInicial;
        private DateTime _DtFinal;
        private DateTime _DataConsulta;
        #endregion

        #region Métodos Get / Set
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string CodUsuario
        {
            get { return _CodUsuario; }
            set { _CodUsuario = value; }
        }
        public int CodRelatorio
        {
            get { return _CodRelatorio; }
            set { _CodRelatorio = value; }
        }
        public string Uf
        {
            get { return _Uf; }
            set { _Uf = value; }
        }
        public string Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }
        public string Unidade
        {
            get { return _Unidade; }
            set { _Unidade = value; }
        }
        public string Celula
        {
            get { return _Celula; }
            set { _Celula = value; }
        }
        public string Ambiente
        {
            get { return _Ambiente; }
            set { _Ambiente = value; }
        }
        public string Impressoras
        {
            get { return _Impressoras; }
            set { _Impressoras = value; }
        }
        public string Usuarios
        {
            get { return _Usuarios; }
            set { _Usuarios = value; }
        }
        public string FormatoFolha
        {
            get { return _FormatoFolha; }
            set { _FormatoFolha = value; }
        }
        public string Cor
        {
            get { return _Cor; }
            set { _Cor = value; }
        }
        public DateTime DtInicial
        {
            get { return _DtInicial; }
            set { _DtInicial = value; }
        }
        public DateTime DtFinal
        {
            get { return _DtFinal; }
            set { _DtFinal = value; }
        }
        public DateTime DataConsulta
        {
            get { return _DataConsulta; }
            set { _DataConsulta = value; }
        }
        #endregion

        #region Construtor
        public Consulta()
        { }

        public Consulta(int Id, string CodUsuario, int CodRelatorio, string Uf, string Cidade, string Unidade, string Celula, string Ambiente, string Impressoras, string Usuarios, string FormatoFolha, string Cor, DateTime DataInicial, DateTime DataFinal, DateTime DataConsulta)
        {
            this.Id = Id;
            this.CodUsuario = CodUsuario;
            this.CodRelatorio = CodRelatorio;

            if (Uf != "Todas" && Uf != "" && Uf != "%")
                this.Uf = Uf;

            if (Cidade != "Todas" && Cidade != "" && Cidade != "%")
                this.Cidade = Cidade;

            if (Unidade != "Todas" && Unidade != "" && Unidade != "%")
                this.Unidade = Unidade;

            if (Celula != "Todos" && Celula != "" && Celula != "%")
                this.Celula = Celula;

            if (Ambiente != "Todos" && Ambiente != "" && Ambiente != "%")
                this.Ambiente = Ambiente;

            if (Impressoras != "Todos" && Impressoras != "")
                this.Impressoras = Impressoras;

            if (Usuarios != "Todos" && Usuarios != "")
                this.Usuarios = Usuarios;

            if (FormatoFolha != "Todas" && FormatoFolha != "")
                this.FormatoFolha = FormatoFolha;

            if (Cor != "Todas" && Cor != "")
                this.Cor = Cor;

            this.DtInicial = DataInicial;
            this.DtFinal = DataFinal;
            this.DataConsulta = DataConsulta;
        }
        #endregion

        #region Consulta - Consulta
        public static List<Consulta> RetornaConsultas()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" id AS Id,");
            sql.Append(" usuario AS CodUsuario,");
            sql.Append(" relatorio AS CodRelatorio,");
            sql.Append(" uf AS Uf,");
            sql.Append(" cidade AS Cidade,");
            sql.Append(" unidade AS Unidade,");
            sql.Append(" celula AS Celula,");
            sql.Append(" ambiente AS Ambiente,");
            sql.Append(" impressoras AS Impressoras,");
            sql.Append(" usuarios AS Usuarios,");
            sql.Append(" formatofolha AS FormatoFolha,");
            sql.Append(" cor AS Cor,");
            sql.Append(" dtinicial AS DataInicial,");
            sql.Append(" dtfinal AS DataFinal,");
            sql.Append(" data AS DataConsulta");
            sql.Append(" FROM Consultas");
            sql.Append(" ORDER BY 1");

            Banco banco = new Banco();

            return ProcessarConsultas(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Sistema));
        }
        #endregion

        #region Monta a lista de Consultas
        private static List<Consulta> ProcessarConsultas(DataTable dataTableUSD)
        {
            List<Consulta> lista = new List<Consulta>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Consulta usuario = ConstruirConsulta(rowReader);

                lista.Add(usuario);
            }

            return lista;
        }

        private static Consulta ConstruirConsulta(NullableDataRowReader rowReader)
        {
            Consulta consulta = new Consulta();

            consulta.Id = rowReader.GetInt32("Id");
            consulta.CodUsuario = rowReader.GetNullableString("CodUsuario");
            consulta.CodRelatorio = rowReader.GetInt32("CodRelatorio");
            consulta.Uf = rowReader.GetNullableString("Uf");
            consulta.Cidade = rowReader.GetNullableString("Cidade");
            consulta.Unidade = rowReader.GetNullableString("Unidade");
            consulta.Celula = rowReader.GetNullableString("Celula");
            consulta.Ambiente = rowReader.GetNullableString("Ambiente");
            consulta.Impressoras = rowReader.GetNullableString("Impressoras");
            consulta.Usuarios = rowReader.GetNullableString("Usuarios");
            consulta.FormatoFolha = rowReader.GetNullableString("FormatoFolha");
            consulta.Cor = rowReader.GetNullableString("Cor");
            consulta.DtInicial = rowReader.GetDateTime("DataInicial");
            consulta.DtFinal = rowReader.GetDateTime("DataFinal");
            consulta.DataConsulta = rowReader.GetDateTime("DataConsulta");

            return consulta;
        }
        #endregion

        #region Inserir - Consulta
        public static bool Inserir(Consulta consulta)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO Consultas (usuario,relatorio,uf,cidade,unidade,celula,ambiente,impressoras,usuarios,formatofolha,cor,dtinicial,dtfinal,data)");
            sql.Append(" VALUES(");
            sql.Append(" '" + consulta.CodUsuario + "',");
            sql.Append(" " + consulta.CodRelatorio + ",");

            if (consulta.Uf != null)
                sql.Append(" '" + consulta.Uf + "',");
            else
                sql.Append(" null,");

            if (consulta.Cidade != null)
                sql.Append(" '" + consulta.Cidade + "',");
            else
                sql.Append(" null,");

            if (consulta.Unidade != null)
                sql.Append(" '" + consulta.Unidade + "',");
            else
                sql.Append(" null,");

            if (consulta.Celula != null)
                sql.Append(" '" + consulta.Celula + "',");
            else
                sql.Append(" null,");

            if (consulta.Ambiente != null)
                sql.Append(" '" + consulta.Ambiente + "',");
            else
                sql.Append(" null,");

            if (consulta.Impressoras != null)
                sql.Append(" '" + consulta.Impressoras.Replace("'","_") + "',");
            else
                sql.Append(" null,");

            if (consulta.Usuarios != null)
                sql.Append(" '" + consulta.Usuarios.Replace("'", "_") + "',");
            else
                sql.Append(" null,");

            if (consulta.FormatoFolha != null)
                sql.Append(" '" + consulta.FormatoFolha + "',");
            else
                sql.Append(" null,");

            if (consulta.Cor != null)
                sql.Append(" '" + consulta.Cor + "',");
            else
                sql.Append(" null,");
            
            sql.Append(" CAST('" + Util.FormatarDateTimeSQL(consulta.DtInicial) + "' as datetime),");
            sql.Append(" CAST('" + Util.FormatarDateTimeSQL(consulta.DtFinal) + "' as datetime),");
            sql.Append(" CAST('" + Util.FormatarDateTimeSQL(DateTime.Now) + "' as datetime)");
            sql.Append(")");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion
    }
}