using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Relatorio1Dados
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
        private int _Impressoes;
        private int _Copias;
        private int _Fax;
        private int _Total;
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
        #endregion

        #region Consulta - Dados
        public static List<Relatorio1Dados> RetornaDados(DateTime dataInicial, DateTime dataFinal, string Uf, string Cidade, string Unidade, string Celula, string Ambiente, string impressoras)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" '1' as NUMBER,");
            sql.Append(" a.lote as Lote,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.fila,7,4)))) as Modelo,");
            sql.Append(" UPPER(LTRIM(RTRIM(b.serie))) as Serie,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.fila))) as Fila,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.ip))) as IP,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.uf))) as UF,");
            sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) Cidade,");
            sql.Append(" UPPER(LTRIM(RTRIM(a.unidade))) Unidade, ");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula,");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END Ambiente,");
            sql.Append(" case when b.Impressoes is null then 0 else b.Impressoes end as Impressoes,");
            sql.Append(" case when b.Copias is null then 0 else b.Copias end as Copias,");
            sql.Append(" case when b.Fax is null then 0 else b.Fax end as Fax,");
            sql.Append(" case when (b.TOTAL) is null then 0 else (b.TOTAL) end as Total");
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
            sql.Append(" LEFT OUTER JOIN func_bilhetagem_fisica(@dt_inicial, @dt_final) b on a.id_impr_snmp = b.id_impr_snmp");

            sql.Append(" UNION ALL");

            //Consulta DNAPRINT

            sql.Append(@"   SELECT '1' as NUMBER, '1' as Lote,  UPPER(LTRIM(RTRIM(SUBSTRING(a.fila,7,4)))) as Modelo,   UPPER(LTRIM(RTRIM(a.serie))) as Serie, UPPER(LTRIM(RTRIM(a.fila))) as Fila,   
                            UPPER(LTRIM(RTRIM(a.ip))) as IP, UPPER(LTRIM(RTRIM(a.uf))) as UF,   UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) Cidade,   
                            UPPER(LTRIM(RTRIM(a.unidade))) Unidade,    CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula,   
                            CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END Ambiente,   
                            case when Impressoes is null then 0 else Impressoes end as Impressoes,
                            case when Copias is null then 0 else Copias end as Copias,
                            case when Fax is null then 0 else Fax end as Fax,
                            case when Total is null then 0 else Total end as Total");
            sql.Append(" FROM ( ");

            sql.Append(" select distinct 0 'ID', uf, uf + ' - ' + cidade 'localizacao', unidade, setor,serie, ip, 1 'lote',dt_instalacao, fila from dnaprint.dbo.vw_listaEquipamentos as a ");
            sql.Append(" where a.cidade not like '%desativada%' and LEN(a.fila) > 0");

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

            sql.Append(" LEFT OUTER JOIN(");

            sql.Append(@" select a.idEquipamento, a.serie , 
                        CASE  WHEN f.[Initotal] IS NULL THEN 0 ELSE f.[Initotal] END AS 'ContadorInicial', 
                        CASE  WHEN g.[Fintotal] IS NULL THEN 0 ELSE g.[Fintotal] END AS 'ContadorFinal', 
                        (CASE  WHEN g.Finimpressoes IS NULL THEN 0 ELSE g.Finimpressoes END )-(CASE  WHEN f.Iniimpressoes IS NULL THEN 0 ELSE f.Iniimpressoes end) 'Impressoes' ,
                        (CASE  WHEN g.Fincopias IS NULL THEN 0 ELSE g.Fincopias END )-(CASE  WHEN f.Inicopias IS NULL THEN 0 ELSE f.Inicopias end) 'Copias' ,
                        (CASE  WHEN g.Finfax IS NULL THEN 0 ELSE g.Finfax END )-(CASE  WHEN f.Inifax IS NULL THEN 0 ELSE f.Inifax end) 'Fax' ,
                        (CASE  WHEN g.[Fintotal] IS NULL THEN 0 ELSE g.[Fintotal] END )-(CASE  WHEN f.[Initotal] IS NULL THEN 0 ELSE f.[Initotal] end) 'Total' 

                        from dnaprint.dbo.CadastroEquipamentos as a   
                        left join (
                        select idEquipamento, serie
                        , CASE WHEN impr_pf_color IS NULL THEN 0 ELSE  impr_pf_color  END + CASE WHEN impr_pf_mono IS NULL THEN 0 ELSE  impr_pf_mono  END + CASE WHEN impr_gf_color IS NULL THEN 0 ELSE  impr_gf_color  END	 + CASE WHEN impr_gf_mono IS NULL THEN 0 ELSE  impr_gf_mono  END	 + CASE WHEN impr_pf_mono_simples IS NULL THEN 0 ELSE  impr_pf_mono_simples  END	 + CASE WHEN impr_pf_mono_duplex IS NULL THEN 0 ELSE  impr_pf_mono_duplex  END	 + CASE WHEN report_a4 IS NULL THEN 0 ELSE  report_a4  END 'Iniimpressoes'   
                        , CASE WHEN copia_pf_color IS NULL THEN 0 ELSE  copia_pf_color  END	 + CASE WHEN copia_pf_mono IS NULL THEN 0 ELSE  copia_pf_mono  END	 + CASE WHEN copia_gf_color IS NULL THEN 0 ELSE  copia_gf_color  END	 + CASE WHEN copia_gf_mono IS NULL THEN 0 ELSE  copia_gf_mono  END	 + CASE WHEN copia_pf_mono_simples IS NULL THEN 0 ELSE  copia_pf_mono_simples  END	 + CASE WHEN copia_pf_mono_duplex IS NULL THEN 0 ELSE  copia_pf_mono_duplex  END 'Inicopias'  
                        , CASE WHEN fax_pf_color IS NULL THEN 0 ELSE  fax_pf_color  END	 + CASE WHEN fax_pf_mono IS NULL THEN 0 ELSE  fax_pf_mono  END	 + CASE WHEN fax_gf_color IS NULL THEN 0 ELSE  fax_gf_color  END	 + CASE WHEN fax_gf_mono IS NULL THEN 0 ELSE  fax_gf_mono  END	 + CASE WHEN fax_pf_mono_simples IS NULL THEN 0 ELSE  fax_pf_mono_simples  END	 + CASE WHEN fax_pf_mono_duplex IS NULL THEN 0 ELSE  fax_pf_mono_duplex  END 'Inifax'  
                        , CASE WHEN total_pf_color IS NULL THEN 0 ELSE  total_pf_color  END + CASE WHEN total_pf_mono IS NULL THEN 0 ELSE  total_pf_mono  END + CASE WHEN total_gf_color IS NULL THEN 0 ELSE  total_gf_color  END + CASE WHEN total_gf_mono IS NULL THEN 0 ELSE  total_gf_mono  END + CASE WHEN total_pf_mono_simples IS NULL THEN 0 ELSE  total_pf_mono_simples  END + CASE WHEN total_pf_mono_duplex IS NULL THEN 0 ELSE  total_pf_mono_duplex  END 'Initotal'
                        from dnaprint.dbo.DadosDisparos where idDisparo in   
                        (
	                        select max(idDisparo) 'idDisparo' from dnaprint.dbo.DadosDisparos  WHERE data < @dt_inicial  GROUP BY idEquipamento 
                        )

                        ) as f on a.serie=f.serie   

                        left join ( 
                        select idEquipamento, serie 
                        , CASE WHEN impr_pf_color IS NULL THEN 0 ELSE  impr_pf_color  END + CASE WHEN impr_pf_mono IS NULL THEN 0 ELSE  impr_pf_mono  END + CASE WHEN impr_gf_color IS NULL THEN 0 ELSE  impr_gf_color  END	 + CASE WHEN impr_gf_mono IS NULL THEN 0 ELSE  impr_gf_mono  END	 + CASE WHEN impr_pf_mono_simples IS NULL THEN 0 ELSE  impr_pf_mono_simples  END	 + CASE WHEN impr_pf_mono_duplex IS NULL THEN 0 ELSE  impr_pf_mono_duplex  END	 + CASE WHEN report_a4 IS NULL THEN 0 ELSE  report_a4  END 'Finimpressoes'   
                        , CASE WHEN copia_pf_color IS NULL THEN 0 ELSE  copia_pf_color  END	 + CASE WHEN copia_pf_mono IS NULL THEN 0 ELSE  copia_pf_mono  END	 + CASE WHEN copia_gf_color IS NULL THEN 0 ELSE  copia_gf_color  END	 + CASE WHEN copia_gf_mono IS NULL THEN 0 ELSE  copia_gf_mono  END	 + CASE WHEN copia_pf_mono_simples IS NULL THEN 0 ELSE  copia_pf_mono_simples  END	 + CASE WHEN copia_pf_mono_duplex IS NULL THEN 0 ELSE  copia_pf_mono_duplex  END 'Fincopias'  
                        , CASE WHEN fax_pf_color IS NULL THEN 0 ELSE  fax_pf_color  END	 + CASE WHEN fax_pf_mono IS NULL THEN 0 ELSE  fax_pf_mono  END	 + CASE WHEN fax_gf_color IS NULL THEN 0 ELSE  fax_gf_color  END	 + CASE WHEN fax_gf_mono IS NULL THEN 0 ELSE  fax_gf_mono  END	 + CASE WHEN fax_pf_mono_simples IS NULL THEN 0 ELSE  fax_pf_mono_simples  END	 + CASE WHEN fax_pf_mono_duplex IS NULL THEN 0 ELSE  fax_pf_mono_duplex  END 'Finfax'  
                        , CASE WHEN total_pf_color IS NULL THEN 0 ELSE  total_pf_color  END	 + CASE WHEN total_pf_mono IS NULL THEN 0 ELSE  total_pf_mono  END	 + CASE WHEN total_gf_color IS NULL THEN 0 ELSE  total_gf_color  END	 + CASE WHEN total_gf_mono IS NULL THEN 0 ELSE  total_gf_mono  END	 + CASE WHEN total_pf_mono_simples IS NULL THEN 0 ELSE  total_pf_mono_simples  END	 + CASE WHEN total_pf_mono_duplex IS NULL THEN 0 ELSE  total_pf_mono_duplex  END 'Fintotal'  
                        from dnaprint.dbo.DadosDisparos where idDisparo in   
                        (
	                        select max(idDisparo) 'idDisparo' from dnaprint.dbo.DadosDisparos WHERE data < dateadd(day,1,@dt_final) GROUP BY idEquipamento
                        )
                        ) as g on a.idEquipamento=g.idEquipamento ");

            sql.Append(" ) b on a.serie = b.serie  ");

            sql.Append(" ORDER BY 7,8,9,10,11");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(sql.ToString(), banco.Conexao_DadosReplicado);
            banco.AddParameter(cmd, "dt_inicial", dataInicial, SqlDbType.DateTime, ParameterDirection.Input);
            banco.AddParameter(cmd, "dt_final", dataFinal, SqlDbType.DateTime, ParameterDirection.Input);

            return ProcessarDados(banco.RetornarTabela(cmd));
        }
        #endregion

        #region Monta a lista de Dados
        private static List<Relatorio1Dados> ProcessarDados(DataTable dataTableUSD)
        {
            List<Relatorio1Dados> lista = new List<Relatorio1Dados>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Relatorio1Dados dado = ConstruirDado(rowReader);

                lista.Add(dado);
            }

            return lista;
        }

        private static Relatorio1Dados ConstruirDado(NullableDataRowReader rowReader)
        {
            Relatorio1Dados dado = new Relatorio1Dados();

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
            dado.Impressoes = rowReader.GetInt32("Impressoes");
            dado.Copias = rowReader.GetInt32("Copias");
            dado.Fax = rowReader.GetInt32("Fax");
            dado.Total = rowReader.GetInt32("Total");

            return dado;
        }
        #endregion

        public static int RetornaTotal(List<Relatorio1Dados> dados)
        {
            int total = 0;

            foreach (Relatorio1Dados dado in dados)
            {
                total += dado.Total;
            }

            return total;
        }
    }
}