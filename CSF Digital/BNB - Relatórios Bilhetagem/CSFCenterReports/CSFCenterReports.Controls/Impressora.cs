using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Impressora
    {
        #region Atributos
        private string _Lote;
        private string _Modelo;
        private string _Serie;
        private string _Fila;
        private string _Ip;
        private string _UF;
        private string _Cidade;
        private string _Unidade;
        private string _Celula;
        private string _Ambiente;
        private int _Impressoes;
        private int _Copias;
        private int _Fax;
        private int _Total;
        private List<FilaImpressao> _FilasImpressao = new List<FilaImpressao>();
        private List<ArquivoImpresso> _ArquivosImpressos = new List<ArquivoImpresso>();
        #endregion

        #region Métodos Get / Set
        public string Lote
        {
            get { return _Lote; }
            set { _Lote = value; }
        }
        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }
        public string Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }
        public string Fila
        {
            get { return _Fila; }
            set { _Fila = value; }
        }
        public string Ip
        {
            get { return _Ip; }
            set { _Ip = value; }
        }
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
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
        public int Impressoes
        {
            get { return _Impressoes; }
            set { _Impressoes = value; }
        }
        public int Copias
        {
            get { return _Copias; }
            set { _Copias = value; }
        }
        public int Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public List<FilaImpressao> FilasImpressao
        {
            get { return _FilasImpressao; }
            set { _FilasImpressao = value; }
        }
        public List<ArquivoImpresso> ArquivosImpressos
        {
            get { return _ArquivosImpressos; }
            set { _ArquivosImpressos = value; }
        }
        #endregion propriedades

        #region Consulta - Dados
        public static List<Impressora> RetornaImpressoras(string Uf, string Cidade, string Unidade, string Celula, string Ambiente, string impressoras)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT DISTINCT");
            sql.Append(" a.lote as Lote,");
            sql.Append(" CASE SUBSTRING(numero_serie, 1, 3)");
            sql.Append(" WHEN 'ART' THEN '4510'");
            sql.Append(" WHEN 'BB1' THEN '3635'");
            sql.Append(" WHEN 'LBP' THEN '3635'");
            sql.Append(" WHEN 'MAE' THEN '4260'");
            sql.Append(" WHEN 'WTM' THEN '5675'");
            sql.Append(" END AS Modelo,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.numero_serie))) as Serie,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.nome_ficticio))) as Fila,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.ip))) as IP,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.uf))) as UF,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) Cidade,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.unidade))) Unidade, ");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula,");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END Ambiente");
            sql.Append(" FROM impr_snmp  as a");
            sql.Append(" WHERE a.localizacao not like '%desativada%'");

            if (Uf != "Todas" && Uf != "" && Uf != "%")
                sql.Append(" AND a.uf like '" + Uf + "'");

            if (Cidade != "Todas" && Cidade != "" && Cidade != "%")
                sql.Append(" AND SUBSTRING(a.localizacao, 6, 300) like '" + Cidade + "'");

            if (Unidade != "Todas" && Unidade != "" && Unidade != "%")
                sql.Append(" AND a.unidade like '" + Unidade + "'");

            if (Celula != "Todos" && Celula != "" && Celula != "%")
                sql.Append(" AND a.setor like '" + Celula + "%'");

            //if (Ambiente != "Todos" && Ambiente != "" && Ambiente != "%")
            //    sql.Append(" AND a.setor like '%" + Ambiente + "%'");

            if (impressoras != "Todos" && impressoras != "Todas" && impressoras != "")
                sql.Append(" AND a.nome_ficticio IN (" + impressoras + ")");

            sql.Append(" UNION ALL");

            //CONSULTA DNAPRINT

            sql.Append(" select top 1 1 'lote', CASE SUBSTRING(SERIE, 1, 3)WHEN 'ART' THEN '4510' WHEN 'BB1' THEN '3635' WHEN 'LBP' THEN '3635' WHEN 'MAE' THEN '4260' WHEN 'WTM' THEN '5675' ELSE '6555' END AS Modelo, ");
            sql.Append(" serie, fila, ip, uf, cidade, unidade, left(setor,4), LTRIM(RTRIM(substring(setor, 8,80)))  from dnaprint.dbo.vw_listaEquipamentos as a");
            sql.Append(" WHERE a.cidade not like '%desativada%'");
            
            if (Uf != "Todas" && Uf != "" && Uf != "%")
                sql.Append(" AND a.uf like '" + Uf + "'");
            
            if (Cidade != "Todas" && Cidade != "" && Cidade != "%")
                sql.Append(" AND a.cidade like '" + Cidade + "'");

            if (Unidade != "Todas" && Unidade != "" && Unidade != "%")
                sql.Append(" AND a.unidade like '" + Unidade + "'");

            if (Celula != "Todos" && Celula != "" && Celula != "%")
                sql.Append(" AND a.setor like '" + Celula + "%'");

            if (impressoras != "Todos" && impressoras != "Todas" && impressoras != "")
                sql.Append(" AND a.fila IN (" + impressoras + ")");


            sql.Append(" ORDER BY 6,7,8,9,10");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);

            return ProcessarImpressoras(banco.RetornarTabela(cmd));
        }
        #endregion

        #region Monta a lista de Impressoras
        private static List<Impressora> ProcessarImpressoras(DataTable dataTableUSD)
        {
            List<Impressora> lista = new List<Impressora>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Impressora impressora = ConstruirImpressora(rowReader);

                lista.Add(impressora);
            }

            return lista;
        }

        private static Impressora ConstruirImpressora(NullableDataRowReader rowReader)
        {
            Impressora impressora = new Impressora();

            impressora.Lote = rowReader.GetNullableString("Lote");
            impressora.Modelo = rowReader.GetNullableString("Modelo");
            impressora.Serie = rowReader.GetNullableString("Serie");
            impressora.Fila = rowReader.GetNullableString("Fila");
            impressora.Ip = rowReader.GetNullableString("IP");
            impressora.UF = rowReader.GetNullableString("UF");
            impressora.Cidade = rowReader.GetNullableString("Cidade");
            impressora.Unidade = rowReader.GetNullableString("Unidade");
            impressora.Celula = rowReader.GetNullableString("Celula");
            impressora.Ambiente = rowReader.GetNullableString("Ambiente");
            //impressora.Impressoes = rowReader.GetInt32("Copias");
            //impressora.Copias = rowReader.GetInt32("Copias");
            //impressora.Fax = rowReader.GetInt32("Copias");
            //impressora.Total = rowReader.GetInt32("Total");

            return impressora;
        }
        #endregion
    }
}