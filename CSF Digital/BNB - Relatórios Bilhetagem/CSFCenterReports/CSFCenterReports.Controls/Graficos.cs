using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CSFCenterReports.Controls
{
    public class Graficos
    {
        public static DataTable VolumeTotalContrato()
        {

            string tsql = null;
            //string tsql = string.Format("select uf, jan, fev, mar, abr, mai, jun, jul, ago, [set], [out], nov, dez from dnaprint.rep.volumeMensal where ano = {0}", DateTime.Now.ToString("yyyy"));

            switch (DateTime.Now.ToString("MM"))
            {
                case "01":
                    tsql = string.Format("select UF, SUM(jan) 'jan' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "02":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "03":
                    tsql = string.Format("select 'UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "04":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "05":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "06":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "07":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "08":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "09":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "10":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set', SUM([out]) 'out' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "11":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set', SUM([out]) 'out', SUM(nov) 'nov' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
                case "12":
                    tsql = string.Format("select UF, SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set', SUM([out]) 'out', SUM(nov) 'nov', SUM(dez) 'dez' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) GROUP BY UF ORDER BY 1");
                    break;
            }

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(tsql, banco.Conexao_DadosTransacionais);

            return banco.RetornarTabela(cmd);
        }

        public static DataTable VolumeUsuarios()
        {
            StringBuilder tsql = new StringBuilder();

            tsql.Append(" select top 10 nome_usuario 'Usuário', SUM((total_paginas *copias)) 'Total' ");
            tsql.Append(" from arquivos_impressos ");
            tsql.Append(string.Format(" WHERE DATA_ENVIO > '{0}'",DateTime.Now.ToString("yyyyMM") + "01"));
            tsql.Append(" GROUP BY nome_usuario ORDER BY Total DESC");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(tsql.ToString(), banco.Conexao_DadosReplicado);

            return banco.RetornarTabela(cmd);
        }

        public static DataTable VolumeImpressoras()
        {
            StringBuilder tsql = new StringBuilder();

            tsql.Append(" select top 10 nome_impressora 'Impressora', SUM((total_paginas *copias)) 'Total' ");
            tsql.Append(" from arquivos_impressos ");
            tsql.Append(string.Format(" WHERE DATA_ENVIO > '{0}'", DateTime.Now.ToString("yyyyMM") + "01"));
            tsql.Append(" GROUP BY nome_impressora ORDER BY Total DESC");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(tsql.ToString(), banco.Conexao_DadosReplicado);

            return banco.RetornarTabela(cmd);
        }

        public static DataTable VolumeTotalContrato(Grupo grupo)
        {
            string tsql = null;
            switch (DateTime.Now.ToString("MM"))
            {
                case "01":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "02":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "03":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "04":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "05":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "06":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "07":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "08":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "09":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "10":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set', SUM([out]) 'out' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "11":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set', SUM([out]) 'out', SUM(nov) 'nov' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
                case "12":
                    tsql = string.Format("select 'Total' 'Vol', SUM(jan) 'jan', SUM(fev) 'fev',  SUM(mar) 'mar', SUM(abr) 'abr', SUM(mai) 'mai', SUM(jun) 'jun', SUM(jul) 'jul', SUM(ago) 'ago', SUM([set]) 'set', SUM([out]) 'out', SUM(nov) 'nov', SUM(dez) 'dez' from dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo);
                    break;
            }

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(tsql, banco.Conexao_DadosTransacionais);

            return banco.RetornarTabela(cmd);
        }

        public static DataTable VolumeUsuarios(Grupo grupo)
        {
            StringBuilder tsql = new StringBuilder();

            tsql.Append(" select top 10 nome_usuario 'Usuário', SUM((total_paginas *copias)) 'Total' ");
            tsql.Append(" from arquivos_impressos ");
            tsql.Append(string.Format(" WHERE DATA_ENVIO > '{0}'", DateTime.Now.ToString("yyyyMM") + "01"));
            tsql.Append(string.Format(" AND  nome_impressora like '%{0}%'", grupo.Codigo));
            tsql.Append(" GROUP BY nome_usuario ORDER BY Total DESC");

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(tsql.ToString(), banco.Conexao_DadosReplicado);

            return banco.RetornarTabela(cmd);
        }

        public static DataTable VolumeImpressoras(Grupo grupo)
        {
            string mes = null;
            switch (DateTime.Now.ToString("MM"))
            {
                case "01":
                    mes = "jan";
                    break;
                case "02":
                    mes = "fev";
                    break;
                case "03":
                    mes = "mar";
                    break;
                case "04":
                    mes = "abr";
                    break;
                case "05":
                    mes = "mai";
                    break;
                case "06":
                    mes = "jun";
                    break;
                case "07":
                    mes = "jul";
                    break;
                case "08":
                    mes = "ago";
                    break;
                case "09":
                    mes = "set";
                    break;
                case "10":
                    mes = "out";
                    break;
                case "11":
                    mes = "nov";
                    break;
                case "12":
                    mes = "dez";
                    break;
            }

            string tsql = string.Format("SELECT fila, {1} FROM dnaprint.rep.volumeMensalEquipamento where ano = YEAR(GETDATE()) AND CAST(codag AS INT) = CAST('{0}' AS INT)", grupo.Codigo, mes);

            Banco banco = new Banco();
            SqlCommand cmd = new SqlCommand(tsql, banco.Conexao_DadosTransacionais);

            return banco.RetornarTabela(cmd);
        }
    }
}