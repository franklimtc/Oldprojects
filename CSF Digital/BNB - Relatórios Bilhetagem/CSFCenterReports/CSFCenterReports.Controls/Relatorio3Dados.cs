using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Relatorio3Dados
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
        public static List<Relatorio3Dados> RetornaDados(DateTime dataInicial, DateTime dataFinal, string Uf, string Cidade, string Unidade, string Celula, string Ambiente, string usuario, string impressoras, string formatoFolha, string cor)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            //sql.Append(" row_number() over(order by");
            //sql.Append(" UPPER(LTRIM(RTRIM(a.nome_usuario))),");
            //sql.Append(" UPPER(LTRIM(RTRIM(b.Fila))),");
            //sql.Append(" a.data_envio,");
            //sql.Append(" UPPER(LTRIM(RTRIM(b.uf))),	");
            //sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(b.localizacao, 6, 300)))),");
            //sql.Append(" UPPER(LTRIM(RTRIM(b.unidade))),");
            //sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(b.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(b.setor,0,5)))) END,");
            //sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(b.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(b.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(b.setor,8,300)))) END");
            //sql.Append(" ) as NUMBER,");
            sql.Append(" a.id_arquivos_impressos as NUMBER,");
            sql.Append(" b.lote as Lote,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(b.Fila,7,4)))) as Modelo,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.numero_serie))) as Serie,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.Fila))) as Fila,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.ip))) as IP,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.uf))) as UF,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(b.localizacao, 6, 300)))) Cidade,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.unidade))) Unidade, ");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(b.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(b.setor,0,5)))) END Celula,");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(b.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(b.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(b.setor,8,300)))) END Ambiente,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.nome_impressora))) Impressora,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.nome_usuario))) Usuario,");
            //sql.Append(" LTRIM(RTRIM(a.nome_documento)) Documento,");
            sql.Append(" '' Documento,");
            sql.Append(" case when a.total_paginas = 0 then a.copias else a.total_paginas end Paginas,");
            sql.Append(" case when a.total_paginas = 0 then 1 else a.copias end Copias,");
            sql.Append(" case when a.total_paginas = 0 then a.copias else (a.total_paginas * a.copias) end Total,");
            sql.Append(" a.FormatoDaFolha FormatoFolha,");
            sql.Append(" a.colorido Cor,");
            sql.Append(" a.data_envio Data");
            sql.Append(" FROM ");

            sql.Append(" (");
            sql.Append(" select	");
            sql.Append(" a.uf,");
            sql.Append(" a.localizacao,");
            sql.Append(" a.unidade,");
            sql.Append(" a.setor,");
            sql.Append(" a.numero_serie,");
            sql.Append(" a.ip,");
            sql.Append(" a.lote,");
            sql.Append(" a.dt_instalacao,");
            sql.Append(" DEPARA.fila");
            sql.Append(" from impr_snmp  as a");
            sql.Append(" left outer join validabaoprint.dbo.filas  as DEPARA on a.nome_ficticio = DEPARA.FilaFisica");
            sql.Append(" where localizacao not like '%desativada%'");
            
            if (Uf != "Todas" && Uf != "")
                sql.Append(" AND UPPER(LTRIM(RTRIM(a.uf))) like '" + Uf + "'");

            if (Cidade != "Todas" && Cidade != "")
                sql.Append(" AND UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) like '" + Cidade + "'");

            if (Unidade != "Todas" && Unidade != "")
                sql.Append(" AND UPPER(LTRIM(RTRIM(a.unidade))) like '" + Unidade + "'");

            if (Celula != "Todos" && Celula != "")
                sql.Append(" AND a.setor like '%" + Celula + "%'");

            //if (Ambiente != "Todos" && Ambiente != "")
            //    sql.Append(" AND a.setor like '%" + Ambiente + "%'");

            if (impressoras != "Todos" && impressoras != "")
                sql.Append(" AND DEPARA.fila IN (" + impressoras + ")");

            sql.Append(" group by a.uf, a.localizacao, a.unidade, a.setor, a.numero_serie, a.ip, a.lote, a.dt_instalacao, DEPARA.fila");

            sql.Append(" ) as b");

            sql.Append(" inner join");

            sql.Append(" (");
            sql.Append(" select * ,case when DEPARA.Fila is null then a.nome_impressora else DEPARA.Fila end AS FilaNova,");

            sql.Append(" CASE a.formatoFolha");
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
            sql.Append(" END AS FormatoDaFolha");

            sql.Append(" from");
            sql.Append(" arquivos_impressos  as a");
            sql.Append(" left outer join validabaoprint.dbo.filas as DEPARA on a.nome_impressora = DEPARA.FilaLogica");
            sql.Append(" WHERE a.data_envio between @dt_inicial and @dt_final");

            if (usuario != "Todos" && usuario != "")
                sql.Append(String.Format(" AND UPPER(LTRIM(RTRIM(a.nome_usuario))) IN ({0}) ", Util.MontarListaStringConsulta(usuario, ',')));

            if (cor != "Todas" && cor != "")
                sql.Append(" AND  a.colorido like '" + cor + "'");

            sql.Append(" ) as a");
            sql.Append(" on b.Fila = a.FilaNova");

            if (formatoFolha != "Todas" && formatoFolha != "")
                sql.Append(" WHERE a.FormatoDaFolha = '" + formatoFolha + "'");

            //sql.Append(" ORDER BY NUMBER");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);
            banco.AddParameter(cmd, "dt_inicial", dataInicial, SqlDbType.DateTime, ParameterDirection.Input);
            banco.AddParameter(cmd, "dt_final", dataFinal, SqlDbType.DateTime, ParameterDirection.Input);

            return ProcessarDados(banco.RetornarTabela(cmd));
        }
        #endregion

        #region Monta a lista de Dados
        private static List<Relatorio3Dados> ProcessarDados(DataTable dataTableUSD)
        {
            List<Relatorio3Dados> lista = new List<Relatorio3Dados>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Relatorio3Dados dado = ConstruirDado(rowReader);

                lista.Add(dado);
            }

            return lista;
        }

        private static Relatorio3Dados ConstruirDado(NullableDataRowReader rowReader)
        {
            Relatorio3Dados dado = new Relatorio3Dados();

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
             * Impressora
             * Usuario
             * Documento
             * Paginas
             * Copias
             * Total
             * FormatoFolha
             * Cor
             * Data
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
            dado.Impressora = rowReader.GetNullableString("Impressora");
            dado.Usuario = rowReader.GetNullableString("Usuario");
            dado.Documento = rowReader.GetNullableString("Documento");
            dado.Paginas = rowReader.GetInt32("Paginas");
            dado.Copias = rowReader.GetInt32("Total");
            dado.Total = rowReader.GetInt32("Total");
            dado.FormatoFolha = rowReader.GetNullableString("FormatoFolha");
            dado.Cor = rowReader.GetNullableString("Cor");
            dado.Data = rowReader.GetDateTime("Data");

            return dado;
        }
        #endregion

        public static int RetornaTotal(List<Relatorio3Dados> dados)
        {
            int total = 0;

            foreach (Relatorio3Dados dado in dados)
            {
                total += dado.Total;
            }

            return total;
        }
    }
}