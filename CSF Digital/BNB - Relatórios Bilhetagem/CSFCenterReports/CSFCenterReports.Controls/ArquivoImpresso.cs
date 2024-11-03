using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class ArquivoImpresso
    {
        #region Atributos
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
        public static List<ArquivoImpresso> RetornaArquivosImpressos(DateTime DataInicial, DateTime DataFinal, List<Impressora> Impressoras, string usuarios, string formatoFolha, string cor)
        {
            if (Impressoras.Count > 0)
            {
                string impressoras = "";

                foreach (Impressora imp in Impressoras)
                {
                    foreach (FilaImpressao fila in imp.FilasImpressao)
                    {
                        impressoras += fila.NomeLogico + ",";
                    }
                    impressoras += imp.Fila + ",";
                }

                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT DISTINCT ");
                sql.Append(" a.id_arquivos_impressos as Id,");
                sql.Append(" UPPER(LTRIM(RTRIM(a.nome_impressora))) Impressora,");
                sql.Append(" UPPER(LTRIM(RTRIM(a.nome_usuario))) Usuario,");
                sql.Append(" LTRIM(RTRIM(a.nome_documento)) Documento,");
                sql.Append(" case when a.total_paginas = 0 then a.copias else a.total_paginas end Paginas,");
                sql.Append(" case when a.total_paginas = 0 then 1 else a.copias end Copias,");
                sql.Append(" case when a.total_paginas = 0 then a.copias else (a.total_paginas * a.copias) end Total,");

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
                sql.Append(" END AS FormatoFolha,");

                sql.Append(" a.colorido Cor,");
                sql.Append(" a.data_envio Data");
                sql.Append(" FROM arquivos_impressos as a ");
                sql.Append(" WHERE a.data_envio between @dt_inicial and @dt_final");

                if (impressoras != "Todos" && impressoras != "")
                    sql.Append(String.Format(" AND UPPER(LTRIM(RTRIM(a.nome_impressora))) IN ({0}) ", Util.MontarListaStringConsulta(impressoras, ',')));

                if (usuarios != "Todos" && usuarios != "")
                    sql.Append(String.Format(" AND UPPER(LTRIM(RTRIM(a.nome_usuario))) IN ({0}) ", Util.MontarListaStringConsulta(usuarios, ',')));

                if (cor != "Todas" && cor != "")
                    sql.Append(" AND  a.colorido like '" + cor + "'");

                if (formatoFolha != "Todas" && formatoFolha != "")
                {
                    sql.Append(" AND (CASE a.formatoFolha");
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
                    sql.Append(" END) = '" + formatoFolha + "'");
                }

                sql.Append(" ORDER BY 2,10");

                Banco banco = new Banco();
                SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);
                banco.AddParameter(cmd, "dt_inicial", DataInicial, SqlDbType.DateTime, ParameterDirection.Input);
                banco.AddParameter(cmd, "dt_final", DataFinal, SqlDbType.DateTime, ParameterDirection.Input);

                return ProcessarArquivosImpressos(banco.RetornarTabela(cmd));
            }
            else
                return new List<ArquivoImpresso>();
        }
        #endregion

        #region Monta a lista de Impressoras
        private static List<ArquivoImpresso> ProcessarArquivosImpressos(DataTable dataTableUSD)
        {
            List<ArquivoImpresso> lista = new List<ArquivoImpresso>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                ArquivoImpresso arquivo = ConstruirArquivoImpresso(rowReader);

                lista.Add(arquivo);
            }

            return lista;
        }

        private static ArquivoImpresso ConstruirArquivoImpresso(NullableDataRowReader rowReader)
        {
            ArquivoImpresso arquivo = new ArquivoImpresso();

            arquivo.Id = rowReader.GetNullableString("Id");
            arquivo.Impressora = rowReader.GetNullableString("Impressora");
            arquivo.Usuario = rowReader.GetNullableString("Usuario");
            arquivo.Documento = rowReader.GetNullableString("Documento");
            arquivo.Paginas = rowReader.GetInt32("Paginas");
            arquivo.Copias = rowReader.GetInt32("Copias");
            arquivo.Total = rowReader.GetInt32("Total");
            arquivo.FormatoFolha = rowReader.GetNullableString("FormatoFolha");
            arquivo.Cor = rowReader.GetNullableString("Cor");
            arquivo.Data = rowReader.GetDateTime("Data");

            return arquivo;
        }
        #endregion

        #region Processar
        public static void ProcessarArquivosImpressos(List<Impressora> Impressoras, List<ArquivoImpresso> ArquivosImpressos)
        {
            foreach (Impressora impressora in Impressoras)
            {
                foreach (ArquivoImpresso arquivo in ArquivosImpressos)
                {
                    if (arquivo.Impressora == impressora.Fila)
                    {
                        arquivo.UF = impressora.UF;
                        arquivo.Cidade = impressora.Cidade;
                        arquivo.Unidade = impressora.Unidade;
                        arquivo.Celula = impressora.Celula;
                        arquivo.Ambiente = impressora.Ambiente;
                        arquivo.Impressora = impressora.Fila;

                        impressora.ArquivosImpressos.Add(arquivo);
                    }
                    else
                    {
                        FilaImpressao fila = impressora.FilasImpressao.Find(f => f.NomeLogico.Trim() == arquivo.Impressora.Trim());

                        if (fila != null)
                        {
                            arquivo.UF = impressora.UF;
                            arquivo.Cidade = impressora.Cidade;
                            arquivo.Unidade = impressora.Unidade;
                            arquivo.Celula = impressora.Celula;
                            arquivo.Ambiente = impressora.Ambiente;
                            arquivo.Impressora = impressora.Fila;

                            impressora.ArquivosImpressos.Add(arquivo);
                        }
                    }
                }
            }
        }
        #endregion
    }
}