using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Relatorio5Dados
    {
        #region Atributos
        private int _NUMBER;
        private string _Lote;
        private string _Modelo;
        private string _Serie;
        private string _Fila;
        private string _IP;
        private string _UF;
        private string _Cidade;
        private string _Unidade;
        private string _Celula;
        private string _Ambiente;
        private string _Id;
        private string _Impressora;
        private string _Usuario;
        private string _Documento;
        private int _Paginas;
        private int _Copias;
        private int _Total;
        private string _FormatoFolha;
        private string _Cor;
        private DateTime _Data;
        #endregion

        #region Métodos Get / Set
        public int NUMBER
        {
            get { return _NUMBER; }
            set { _NUMBER = value; }
        }
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
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
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
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Impressora
        {
            get { return _Impressora; }
            set { _Impressora = value; }
        }
        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        public int Paginas
        {
            get { return _Paginas; }
            set { _Paginas = value; }
        }
        public int Copias
        {
            get { return _Copias; }
            set { _Copias = value; }
        }
        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
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
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        #endregion

        #region Consulta - Dados
        public static List<Relatorio5Dados> RetornaDados(DateTime dataInicial, DateTime dataFinal, string Uf, string Cidade, string Unidade, string Celula, string Ambiente, string impressoras, string usuarios)
        {
            StringBuilder sql = new StringBuilder();

            #region Código Antigo

            //sql.Append("SELECT");
            //sql.Append(" '1' as NUMBER,");
            //sql.Append(" UPPER(LTRIM(RTRIM(b.nome_usuario))) Usuario,");
            //sql.Append(" SUM(case when b.total_paginas = 0 then b.copias else (b.total_paginas * b.copias) end) Total");
            //sql.Append(" FROM");
            //sql.Append(" (");
            //sql.Append(" select distinct");
            //sql.Append(" a.id_impr_snmp,");
            //sql.Append(" a.uf,");
            //sql.Append(" a.localizacao,");
            //sql.Append(" a.unidade,");
            //sql.Append(" a.setor,");
            //sql.Append(" a.numero_serie,");
            //sql.Append(" a.ip,");
            //sql.Append(" a.lote,");
            //sql.Append(" a.dt_instalacao,");
            //sql.Append(" a.nome_ficticio as fila");
            //sql.Append(" from impr_snmp as a");
            //sql.Append(" where a.localizacao not like '%desativada%'");
            //sql.Append(" and LEN(a.nome_ficticio) > 0");

            //if (Uf != "Todas" && Uf != "" && Uf != "%")
            //    sql.Append(" AND a.uf like '" + Uf + "'");

            //if (Cidade != "Todas" && Cidade != "" && Cidade != "%")
            //    sql.Append(" AND SUBSTRING(a.localizacao, 6, 300) like '" + Cidade + "'");

            //if (Unidade != "Todas" && Unidade != "" && Unidade != "%")
            //    sql.Append(" AND a.unidade like '" + Unidade + "'");

            //if (Celula != "Todos" && Celula != "" && Celula != "%")
            //    sql.Append(" AND a.setor like '" + Celula + "%'");

            ////if (Ambiente != "Todos" && Ambiente != "" && Ambiente != "%")
            ////    sql.Append(" AND a.setor like '%" + Ambiente + "%'");

            //if (impressoras != "Todos" && impressoras != "")
            //    sql.Append(" AND a.nome_ficticio IN (" + impressoras + ")");

            //sql.Append(" ) as a");
            //sql.Append(" INNER JOIN arquivos_impressos b on LTRIM(RTRIM(a.fila)) = LTRIM(RTRIM(b.nome_impressora)) and b.data_envio between @dt_inicial and @dt_final");

            //if (usuarios != "Todos" && usuarios != "")
            //    sql.Append(String.Format(" and UPPER(LTRIM(RTRIM(b.nome_usuario))) IN ({0}) ", Util.MontarListaStringConsulta(usuarios, ',')));

            //sql.Append(" GROUP BY UPPER(LTRIM(RTRIM(b.nome_usuario)))");
            //sql.Append(" ORDER BY 2 desc,1");

            #endregion

            sql.Append("SELECT");
            sql.Append(" '1' as NUMBER,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.nome_usuario))) Usuario,");
            sql.Append(" SUM(case when b.total_paginas = 0 then b.copias else (b.total_paginas * b.copias) end) Total");
            sql.Append(" FROM");
            sql.Append(" (");
            
            //Consulta BAOPRINT

            sql.Append(" select distinct a.nome_ficticio as 'fila' from baoprintbnb.dbo.impr_snmp as a");
            sql.Append(" where a.localizacao not like '%desativada%'");
            sql.Append(" and LEN(a.nome_ficticio) > 0");

            if (Uf != "Todas" && Uf != "" && Uf != "%")
                sql.Append(" AND a.uf like '" + Uf + "'");

            if (Cidade != "Todas" && Cidade != "" && Cidade != "%")
                sql.Append(" AND SUBSTRING(a.localizacao, 6, 300) like '" + Cidade + "'");

            if (Unidade != "Todas" && Unidade != "" && Unidade != "%")
                sql.Append(" AND a.unidade like '" + Unidade + "'");

            if (Celula != "Todos" && Celula != "" && Celula != "%")
                sql.Append(" AND a.setor like '" + Celula + "%'");

            if (impressoras != "Todos" && impressoras != "")
                sql.Append(" AND a.nome_ficticio IN (" + impressoras + ")");

            sql.Append(" UNION ALL");

            //Consulta DNAPRINT

            sql.Append(" select distinct a.fila from dnaprint.dbo.vw_listaEquipamentos as a");
            sql.Append(" where a.cidade not like '%desativada%'");
            sql.Append(" and LEN(a.fila) > 0");

            if (Uf != "Todas" && Uf != "" && Uf != "%")
                sql.Append(" AND a.uf like '" + Uf + "'");

            if (Cidade != "Todas" && Cidade != "" && Cidade != "%")
                sql.Append(" AND a.cidade like '" + Cidade + "'");

            if (Unidade != "Todas" && Unidade != "" && Unidade != "%")
                sql.Append(" AND a.unidade like '" + Unidade + "'");

            if (Celula != "Todos" && Celula != "" && Celula != "%")
                sql.Append(" AND a.setor like '" + Celula + "%'");

            if (impressoras != "Todos" && impressoras != "")
                sql.Append(" AND a.fila IN (" + impressoras + ")");

            sql.Append(" ) as a");
            sql.Append(" INNER JOIN arquivos_impressos b on LTRIM(RTRIM(a.fila)) = LTRIM(RTRIM(b.nome_impressora)) and b.data_envio between @dt_inicial and @dt_final");

            if (usuarios != "Todos" && usuarios != "")
                sql.Append(String.Format(" and UPPER(LTRIM(RTRIM(b.nome_usuario))) IN ({0}) ", Util.MontarListaStringConsulta(usuarios, ',')));

            sql.Append(" INNER JOIN dnaprint.dbo.arquivoimpresso as c on LTRIM(RTRIM(a.fila)) = LTRIM(RTRIM(c.PrinterName)) and c.Submitted between @dt_inicial and @dt_final ");


            sql.Append(" GROUP BY UPPER(LTRIM(RTRIM(b.nome_usuario)))");
            sql.Append(" ORDER BY 2 desc,1");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);
            banco.AddParameter(cmd, "dt_inicial", dataInicial, SqlDbType.DateTime, ParameterDirection.Input);
            banco.AddParameter(cmd, "dt_final", dataFinal, SqlDbType.DateTime, ParameterDirection.Input);

            return ProcessarDados(banco.RetornarTabela(cmd));
        }
        #endregion

        #region Monta a lista de Dados
        private static List<Relatorio5Dados> ProcessarDados(DataTable dataTableUSD)
        {
            List<Relatorio5Dados> lista = new List<Relatorio5Dados>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Relatorio5Dados dado = ConstruirDado(rowReader);

                lista.Add(dado);
            }

            return lista;
        }

        private static Relatorio5Dados ConstruirDado(NullableDataRowReader rowReader)
        {
            Relatorio5Dados dado = new Relatorio5Dados();

            /*
             * NUMBER
             * Lote
             * Modelo
             * Serie
             * Fila
             * IP
             * UF
             * Cidade
             * Unidade
             * Celula
             * Ambiente
             * Impressoes
             * Copias
             * Fax
             * Total
             */

            dado.NUMBER = rowReader.GetInt32("NUMBER");
            //dado.Lote = rowReader.GetNullableString("Lote");
            //dado.Modelo = rowReader.GetNullableString("Modelo");
            //dado.Serie = rowReader.GetNullableString("Serie");
            //dado.Fila = rowReader.GetNullableString("Fila");
            //dado.IP = rowReader.GetNullableString("IP");
            //dado.UF = rowReader.GetNullableString("UF");
            //dado.Cidade = rowReader.GetNullableString("Cidade");
            //dado.Unidade = rowReader.GetNullableString("Unidade");
            //dado.Celula = rowReader.GetNullableString("Celula");
            //dado.Ambiente = rowReader.GetNullableString("Ambiente");

            //dado.Id = rowReader.GetNullableString("Id");
            //dado.Impressora = rowReader.GetNullableString("Impressora");
            dado.Usuario = rowReader.GetNullableString("Usuario");
            //dado.Documento = rowReader.GetNullableString("Documento");
            //dado.Paginas = rowReader.GetInt32("Paginas");
            //dado.Copias = rowReader.GetInt32("Copias");
            dado.Total = rowReader.GetInt32("Total");
            //dado.FormatoFolha = rowReader.GetNullableString("FormatoFolha");
            //dado.Cor = rowReader.GetNullableString("Cor");
            //dado.Data = rowReader.GetDateTime("Data");

            return dado;
        }
        #endregion

        public static int RetornaTotal(List<Relatorio5Dados> dados)
        {
            int total = 0;

            foreach (Relatorio5Dados dado in dados)
            {
                total += dado.Total;
            }

            return total;
        }
    }
}