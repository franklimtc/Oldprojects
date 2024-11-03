using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Relatorio4Dados
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
        public static List<Relatorio4Dados> RetornaDados(DateTime dataInicial, DateTime dataFinal, string Uf, string Cidade, string Unidade, string Celula, string Ambiente, string impressoras, string usuarios)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" '1' as NUMBER,");
            sql.Append(" a.lote as Lote,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.fila,7,4)))) as Modelo,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.numero_serie))) as Serie,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.fila))) as Fila,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.ip))) as IP,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.uf))) as UF,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) Cidade,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.unidade))) Unidade, ");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula,");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END Ambiente,");
            sql.Append(" b.id_arquivos_impressos as Id,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.nome_impressora))) Impressora,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.nome_usuario))) Usuario,");
            sql.Append(" LTRIM(RTRIM(b.nome_documento)) Documento,");
            sql.Append(" case when b.total_paginas = 0 then b.copias else b.total_paginas end Paginas,");
            sql.Append(" case when b.total_paginas = 0 then 1 else b.copias end Copias,");
            sql.Append(" case when b.total_paginas = 0 then b.copias else (b.total_paginas * b.copias) end Total,");
            sql.Append(" CASE b.formatoFolha");
            sql.Append(" WHEN '' THEN 'A4'");
            sql.Append(" WHEN '2 16 x 3 4\"' THEN 'Ofício'");
            sql.Append(" WHEN '210 x 330 mm' THEN 'Ofício'");
            sql.Append(" WHEN '215 x 315 mm' THEN 'Ofício'");
            sql.Append(" WHEN '215 x 330 mm' THEN 'Ofício'");
            sql.Append(" WHEN '8 26 x 11 14\"' THEN 'Carta'");
            sql.Append(" WHEN '8 27 x 10 7\"' THEN 'Carta'");
            sql.Append(" WHEN '8 27 x 11 4\"' THEN 'Carta'");
            sql.Append(" WHEN '8 27 x 11 7\"' THEN 'Carta'");
            sql.Append(" WHEN '8 27 x 12 1\"' THEN 'Carta'");
            sql.Append(" WHEN '8 27 x 12 2\"' THEN 'Carta'");
            sql.Append(" WHEN '8 28 x 11 67\"' THEN 'Carta'");
            sql.Append(" WHEN '8 3 x 11 6\"' THEN 'Carta'");
            sql.Append(" WHEN '8 5 x 13\"' THEN 'Ofício'");
            sql.Append(" WHEN '8.5 x 13\"' THEN 'Ofício'");
            sql.Append(" WHEN 'A3' THEN 'A3'");
            sql.Append(" WHEN 'A3 (297 x 420 mm)' THEN 'A3'");
            sql.Append(" WHEN 'A4' THEN 'A4'");
            sql.Append(" WHEN 'A4 (210 x 297 mm)' THEN 'A4'");
            sql.Append(" WHEN 'A4 PERSONALIZADO' THEN 'A4'");
            sql.Append(" WHEN 'A4 Transverse' THEN 'A4'");
            sql.Append(" WHEN 'A5' THEN 'A5'");
            sql.Append(" WHEN 'A5 (148 x 210 mm)' THEN 'A5'");
            sql.Append(" WHEN 'A6' THEN 'A6'");
            sql.Append(" WHEN 'A6 (105 x 148 mm)' THEN 'A6'");
            sql.Append(" WHEN 'B5 (JIS)' THEN 'B5'");
            sql.Append(" WHEN 'Carta' THEN 'Carta'");
            sql.Append(" WHEN 'Carta (8 5 x 11\")' THEN 'Carta'");
            sql.Append(" WHEN 'Envelope (6 x 9\")' THEN 'Envelope'");
            sql.Append(" WHEN 'Envelope C5' THEN 'Envelope'");
            sql.Append(" WHEN 'Envelope C6' THEN 'Envelope'");
            sql.Append(" WHEN 'Folio' THEN 'Fólio'");
            sql.Append(" WHEN 'Fólio' THEN 'Fólio'");
            sql.Append(" WHEN 'Legal' THEN 'Ofício'");
            sql.Append(" WHEN 'Legal (8.5 x 14\")' THEN 'Ofício'");
            sql.Append(" WHEN 'Letter' THEN 'Carta'");
            sql.Append(" WHEN 'Letter (8 5 x 11\")' THEN 'Carta'");
            sql.Append(" WHEN 'Letter (8 5\" x 11\")' THEN 'Carta'");
            sql.Append(" WHEN 'Letter (8.5 x 11\")' THEN 'Carta'");
            sql.Append(" WHEN 'Letter (8.5\" x 11\")' THEN 'Carta'");
            sql.Append(" WHEN 'Ofício' THEN 'Ofício'");
            sql.Append(" WHEN 'Ofício 9' THEN 'Ofício'");
            sql.Append(" WHEN 'Ofício I (8 5 x 14\")' THEN 'Ofício'");
            sql.Append(" WHEN 'Sem título 1' THEN 'A4'");
            sql.Append(" WHEN 'Tablóide (11 x 17\")' THEN 'Tablóide'");
            sql.Append(" ELSE 'Papel Personalizado'");
            sql.Append(" END AS FormatoFolha,");
            sql.Append(" b.colorido Cor,");
            sql.Append(" b.data_envio Data");
            sql.Append(" FROM");
            sql.Append(" (");
            sql.Append(" select distinct");
            sql.Append(" a.id_impr_snmp,");
            sql.Append(" a.uf,");
            sql.Append(" a.localizacao,");
            sql.Append(" a.unidade,");
            sql.Append(" a.setor,");
            sql.Append(" a.numero_serie,");
            sql.Append(" a.ip,");
            sql.Append(" a.lote,");
            sql.Append(" a.dt_instalacao,");
            sql.Append(" a.nome_ficticio as fila");
            sql.Append(" from impr_snmp as a");
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

            //if (Ambiente != "Todos" && Ambiente != "" && Ambiente != "%")
            //    sql.Append(" AND a.setor like '%" + Ambiente + "%'");

            if (impressoras != "Todos" && impressoras != "")
                sql.Append(" AND a.nome_ficticio IN (" + impressoras + ")");

            sql.Append(" ) as a");
            sql.Append(" INNER JOIN arquivos_impressos b on LTRIM(RTRIM(a.fila)) = LTRIM(RTRIM(b.nome_impressora)) and b.data_envio between @dt_inicial and @dt_final");

            if (usuarios != "Todos" && usuarios != "")
                sql.Append(String.Format(" and UPPER(LTRIM(RTRIM(b.nome_usuario))) IN ({0}) ", Util.MontarListaStringConsulta(usuarios, ',')));

            sql.Append(" ORDER BY 7,8,9,10,11,14,21");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);
            banco.AddParameter(cmd, "dt_inicial", dataInicial, SqlDbType.DateTime, ParameterDirection.Input);
            banco.AddParameter(cmd, "dt_final", dataFinal, SqlDbType.DateTime, ParameterDirection.Input);

            return ProcessarDados(banco.RetornarTabela(cmd));
        }
        #endregion

        #region Monta a lista de Dados
        private static List<Relatorio4Dados> ProcessarDados(DataTable dataTableUSD)
        {
            List<Relatorio4Dados> lista = new List<Relatorio4Dados>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Relatorio4Dados dado = ConstruirDado(rowReader);

                lista.Add(dado);
            }

            return lista;
        }

        private static Relatorio4Dados ConstruirDado(NullableDataRowReader rowReader)
        {
            Relatorio4Dados dado = new Relatorio4Dados();

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
            dado.Lote = rowReader.GetNullableString("Lote");
            dado.Modelo = rowReader.GetNullableString("Modelo");
            dado.Serie = rowReader.GetNullableString("Serie");
            dado.Fila = rowReader.GetNullableString("Fila");
            dado.IP = rowReader.GetNullableString("IP");
            dado.UF = rowReader.GetNullableString("UF");
            dado.Cidade = rowReader.GetNullableString("Cidade");
            dado.Unidade = rowReader.GetNullableString("Unidade");
            dado.Celula = rowReader.GetNullableString("Celula");
            dado.Ambiente = rowReader.GetNullableString("Ambiente");

            dado.Id = rowReader.GetNullableString("Id");
            dado.Impressora = rowReader.GetNullableString("Impressora");
            dado.Usuario = rowReader.GetNullableString("Usuario");
            dado.Documento = rowReader.GetNullableString("Documento");
            dado.Paginas = rowReader.GetInt32("Paginas");
            dado.Copias = rowReader.GetInt32("Copias");
            dado.Total = rowReader.GetInt32("Total");
            dado.FormatoFolha = rowReader.GetNullableString("FormatoFolha");
            dado.Cor = rowReader.GetNullableString("Cor");
            dado.Data = rowReader.GetDateTime("Data");

            return dado;
        }
        #endregion

        public static int RetornaTotal(List<Relatorio4Dados> dados)
        {
            int total = 0;

            foreach (Relatorio4Dados dado in dados)
            {
                total += dado.Total;
            }

            return total;
        }
    }
}