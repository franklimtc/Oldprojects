using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class FilaImpressao
    {
        #region Atributos
        private string _Nome;
        private string _NomeLogico;
        private string _NomeFisico;
        #endregion

        #region Métodos Get / Set
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public string NomeLogico
        {
            get { return _NomeLogico; }
            set { _NomeLogico = value; }
        }
        public string NomeFisico
        {
            get { return _NomeFisico; }
            set { _NomeFisico = value; }
        }
        #endregion

        #region Consulta - Dados
        public static void RetornaFilasImpressao(List<Impressora> Impressoras)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" UPPER(LTRIM(RTRIM(nome_ficticio))) as NOME,");
            sql.Append(" UPPER(LTRIM(RTRIM(nome_ficticio))) as LOGICO,");
            sql.Append(" UPPER(LTRIM(RTRIM(nome_ficticio))) as FISICO");
            sql.Append(" FROM impr_snmp");
            sql.Append(" WHERE localizacao not like '%desativada%' and len(nome_ficticio)>0");
            sql.Append(" ORDER BY 1,2,3");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);

            List<FilaImpressao> Filas = ProcessarFilasImpressao(banco.RetornarTabela(cmd));

            ProcessaFilasImpressao(Impressoras, Filas);
        }
        #endregion

        #region Monta a lista de Filas de Impressão
        private static List<FilaImpressao> ProcessarFilasImpressao(DataTable dataTableUSD)
        {
            List<FilaImpressao> lista = new List<FilaImpressao>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                FilaImpressao fila = ConstruirFilaImpressao(rowReader);

                lista.Add(fila);
            }

            return lista;
        }

        private static FilaImpressao ConstruirFilaImpressao(NullableDataRowReader rowReader)
        {
            FilaImpressao fila = new FilaImpressao();

            fila.Nome = rowReader.GetNullableString("NOME");
            fila.NomeFisico = rowReader.GetNullableString("FISICO");
            fila.NomeLogico = rowReader.GetNullableString("LOGICO");

            return fila;
        }

        private static void ProcessaFilasImpressao(List<Impressora> Impressoras, List<FilaImpressao> Filas)
        {
            foreach (Impressora impressora in Impressoras)
            {
                foreach (FilaImpressao fila in Filas)
                {
                    if (impressora.Fila == fila.Nome)
                    {
                        impressora.FilasImpressao.Add(fila);
                    }
                }
            }
        }
        #endregion
    }
}