using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Grupo
    {
        #region Atributos
        private string _Codigo;
        private string _Uf;
        private string _Cidade;
        private string _Unidade;
        private string _Setor;
        #endregion

        #region Métodos Get / Set
        public string Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
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
        public string Setor
        {
            get { return _Setor; }
            set { _Setor = value; }
        }
        public string Descricao
        {
            get { return String.Format("{0} | {1}-{2} : {3} : {4}", Codigo, Uf, Cidade, Unidade, Setor); }
        }
        #endregion

        #region Consulta - Grupo
        public static Grupo RetornaGrupo(string Codigo)
        {
            //return RetornaGrupos().Find(g => g.Codigo == Codigo);
            Grupo g = new Grupo();

            string htmlGrupo = HtmlReader.htmlToString(string.Format(@"http://d001wwv06/Agencias/Conteudo/org_informacoes.asp?Agencia={0}", Codigo));
            htmlGrupo = HtmlReader.LimparHtml(htmlGrupo);

            g.Codigo = Codigo;
            g.Uf = HtmlReader.retornarCampoHtml("UF", htmlGrupo);
            g.Cidade = HtmlReader.retornarCampoHtml("Município", htmlGrupo);
            g.Unidade = HtmlReader.retornarCampoHtml("Unidade", htmlGrupo);
            g.Setor = HtmlReader.retornarCampoHtml("Unidade", htmlGrupo);
            g.Setor = g.Setor.Substring(g.Setor.IndexOf("-")).Replace("-", "").Trim();

            return g;

        }
        #endregion

        #region Consulta Grupo - Site BNB

        #endregion

        #region Consulta - Grupos
        public static List<Grupo> RetornaGrupos(string codigo, string uf, string cidade, string unidade, string setor, string ativo)
        {
            bool vlativo = true;

            if (ativo != null)
                if (ativo != "")
                    vlativo = bool.Parse(ativo);

            StringBuilder sql = new StringBuilder();
            
            #region Método Original

            //sql.Append("SELECT");
            //sql.Append(" UPPER(LTRIM(RTRIM(a.uf))) as UF,");
            //sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) Cidade,");
            //sql.Append(" UPPER(LTRIM(RTRIM(a.unidade))) Unidade, ");
            //sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula,");
            //sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END Ambiente");

            //sql.Append(" FROM impr_snmp  as a");

            //if (vlativo)
            //    sql.Append(" WHERE (localizacao NOT LIKE '%DESATIVADA%')");
            //else
            //    sql.Append(" WHERE (localizacao LIKE '%DESATIVADA%')");

            //if (codigo != null)
            //    if (codigo != "")
            //        sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END) = '" + codigo + "'");

            //if (uf != null)
            //    if (uf != "")
            //        sql.Append(" AND UPPER(LTRIM(RTRIM(a.uf))) like '" + uf + "'");

            //if (cidade != null)
            //    if (cidade != "")
            //        sql.Append(" AND UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) like '" + cidade + "'");

            //if (unidade != null)
            //    if (unidade != "")
            //        sql.Append(" AND UPPER(LTRIM(RTRIM(a.unidade))) like '" + unidade + "'");

            //if (setor != null)
            //    if (setor != "")
            //        sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END) like '" + setor + "'");
            #endregion

            sql.Append(" SELECT UF, Cidade, Unidade, Celula, Ambiente FROM vw_gruposRelatorios");
            if (vlativo)
                sql.Append(" WHERE (Cidade NOT LIKE '%DESATIVADA%')");
            else
                sql.Append(" WHERE (Cidade LIKE '%DESATIVADA%')");

            if (codigo != null)
                if (codigo != "")
                    sql.Append(" AND Celula = '" + codigo + "'");
            
            if (uf != null)
                if (uf != "")
                    sql.Append(" AND  UF = '" + uf + "'");

            if (cidade != null)
                if (cidade != "")
                    sql.Append(" AND Cidade like '" + cidade + "'");

            if (unidade != null)
                if (unidade != "")
                    sql.Append(" AND Unidade like '" + unidade + "'");

            if (setor != null)
                if (setor != "")
                    sql.Append(" AND Ambiente like '" + setor + "'");
            
            sql.Append(" ORDER BY 1,2,3,4,5");

            Banco banco = new Banco();

            //return ProcessarGrupos(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Dados));
            return ProcessarGrupos(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Transacional));
        }

        public static List<Grupo> RetornaGrupos()
        {
            StringBuilder sql = new StringBuilder();

            #region Método Original

            //sql.Append("SELECT DISTINCT");
            //sql.Append(" UPPER(LTRIM(RTRIM(a.uf))) as UF,");
            //sql.Append(" UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) Cidade,");
            //sql.Append(" UPPER(LTRIM(RTRIM(a.unidade))) Unidade, ");
            //sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula,");
            //sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END Ambiente");
            //sql.Append(" FROM impr_snmp  as a");
            //sql.Append(" WHERE (localizacao NOT LIKE '%DESATIVADA%')");
            //sql.Append(" and a.id_projeto = 1");
            //sql.Append(" ORDER BY 1,2,3,4,5");
            #endregion

            sql.Append(" SELECT UF, Cidade, Unidade, Celula, Ambiente FROM vw_gruposRelatorios");
            sql.Append(" ORDER BY 1,2,3,4,5");

            Banco banco = new Banco();

            //return ProcessarGrupos(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Dados));
            return ProcessarGrupos(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Transacional));
        }
        #endregion

        #region Monta a lista de Grupos
        private static List<Grupo> ProcessarGrupos(DataTable dataTableUSD)
        {
            List<Grupo> lista = new List<Grupo>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Grupo grupo = ConstruirGrupo(rowReader);

                lista.Add(grupo);
            }

            return lista;
        }

        private static Grupo ConstruirGrupo(NullableDataRowReader rowReader)
        {
            Grupo grupo = new Grupo();

            grupo.Codigo = rowReader.GetString("Celula");
            grupo.Uf = rowReader.GetNullableString("UF");
            grupo.Cidade = rowReader.GetNullableString("Cidade");
            grupo.Unidade = rowReader.GetNullableString("Unidade");
            grupo.Setor = rowReader.GetNullableString("Ambiente");

            return grupo;
        }
        #endregion

        public static List<string> RetornaListaCodigos(List<Grupo> ListaGrupos)
        {
            List<string> ListaCodigos = new List<string>();

            if (ListaGrupos == null)
            {
                foreach (Grupo grupo in RetornaGrupos())
                    if (!ListaCodigos.Contains(grupo.Codigo.Trim()))
                        ListaCodigos.Add(grupo.Codigo.Trim());
            }
            else
            {
                foreach (Grupo grupo in ListaGrupos)
                    if (!ListaCodigos.Contains(grupo.Codigo.Trim()))
                        ListaCodigos.Add(grupo.Codigo.Trim());
            }

            return ListaCodigos;
        }

        public static List<string> RetornaListaUf(List<Grupo> ListaGrupos)
        {
            List<string> Lista = new List<string>();
            List<string> ListaTemp = new List<string>();
            Lista.Add("Todos");

            if (ListaGrupos == null)
            {
                foreach (Grupo grupo in RetornaGrupos())
                    if (!ListaTemp.Contains(grupo.Uf.Trim()))
                        ListaTemp.Add(grupo.Uf.Trim());
            }
            else
            {
                foreach (Grupo grupo in ListaGrupos)
                    if (!ListaTemp.Contains(grupo.Uf.Trim()))
                        ListaTemp.Add(grupo.Uf.Trim());
            }

            ListaTemp.Sort(delegate(string str1, string str2)
            { return str1.CompareTo(str2); });

            foreach (string item in ListaTemp)
                Lista.Add(item);

            return Lista;
        }

        public static List<string> RetornaListaCidades(string uf)
        {
            List<string> Lista = new List<string>();
            List<string> ListaTemp = new List<string>();
            Lista.Add("Todas");

            if (uf != null)
            {
                foreach (Grupo grupo in RetornaGrupos("", uf, "", "", "", ""))
                    if (!ListaTemp.Contains(grupo.Cidade.Trim()))
                        ListaTemp.Add(grupo.Cidade.Trim());
            }

            ListaTemp.Sort(delegate(string str1, string str2)
            { return str1.CompareTo(str2); });

            foreach (string item in ListaTemp)
                Lista.Add(item);

            return Lista;
        }

        public static List<string> RetornaListaUnidades(string uf, string cidade)
        {
            List<string> Lista = new List<string>();
            List<string> ListaTemp = new List<string>();
            Lista.Add("Todas");

            if (uf != null && cidade != null)
            {
                foreach (Grupo grupo in RetornaGrupos("", uf, cidade, "", "", ""))
                    if (!ListaTemp.Contains(grupo.Unidade.Trim()))
                        ListaTemp.Add(grupo.Unidade.Trim());
            }

            ListaTemp.Sort(delegate(string str1, string str2)
            { return str1.CompareTo(str2); });

            foreach (string item in ListaTemp)
                Lista.Add(item);

            return Lista;
        }

        public static List<string> RetornaListaCelulas(string uf, string cidade, string unidade)
        {
            List<string> Lista = new List<string>();
            List<string> ListaTemp = new List<string>();
            Lista.Add("Todos");

            if (uf != null && cidade != null && unidade != null)
            {
                foreach (Grupo grupo in RetornaGrupos("", uf, cidade, unidade, "", ""))
                    if (!ListaTemp.Contains(grupo.Codigo.Trim()))
                        ListaTemp.Add(grupo.Codigo.Trim());
            }

            ListaTemp.Sort(delegate(string str1, string str2)
            { return str1.CompareTo(str2); });

            foreach (string item in ListaTemp)
                Lista.Add(item);

            return Lista;
        }

        public static List<string> RetornaListaSetores(string uf, string cidade, string unidade)
        {
            List<string> Lista = new List<string>();
            List<string> ListaTemp = new List<string>();
            Lista.Add("Todos");

            if (uf != null && cidade != null && unidade != null)
            {
                foreach (Grupo grupo in RetornaGrupos("", uf, cidade, unidade, "", ""))
                    if (!ListaTemp.Contains(grupo.Setor.Trim()))
                        ListaTemp.Add(grupo.Setor.Trim());
            }

            ListaTemp.Sort(delegate(string str1, string str2)
            { return str1.CompareTo(str2); });

            foreach (string item in ListaTemp)
                Lista.Add(item);

            return Lista;
        }

        public static string RetornaCelula(string uf, string cidade, string unidade, string setor)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT DISTINCT");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END Celula");
            sql.Append(" FROM impr_snmp  as a");
            sql.Append(" WHERE (localizacao NOT LIKE '%DESATIVADA%')");
            sql.Append(" and a.id_projeto = 1");
            if (uf != null)
                if (uf != "")
                    sql.Append(" AND UPPER(LTRIM(RTRIM(a.uf))) like '" + uf + "'");

            if (cidade != null)
                if (cidade != "")
                    sql.Append(" AND UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) like '" + cidade + "'");

            if (unidade != null)
                if (unidade != "")
                    sql.Append(" AND UPPER(LTRIM(RTRIM(a.unidade))) like '" + unidade + "'");

            if (setor != null)
                if (setor != "")
                    sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END) like '" + setor + "'");

            sql.Append(" ORDER BY 1");

            List<string> Impressoras = new List<string>();
            Banco banco = new Banco();
            DataTable dt = banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Dados);

            string celula = "";
            foreach (DataRow row in dt.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                celula = rowReader.GetString("Celula");
            }

            return celula;
        }

        public static string RetornaAmbiente(string Celula)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT DISTINCT");
            sql.Append(" CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END AS Ambiente");
            sql.Append(" FROM impr_snmp  as a");
            sql.Append(" WHERE (localizacao NOT LIKE '%DESATIVADA%')");
            sql.Append(" and a.id_projeto = 1");
            sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END) like '" + Celula + "'");
            sql.Append(" ORDER BY 1");

            List<string> Impressoras = new List<string>();
            Banco banco = new Banco();
            DataTable dt = banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Dados);

            string ambiente = "";
            foreach (DataRow row in dt.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                ambiente = rowReader.GetString("Ambiente");
            }

            return ambiente;
        }

        #region Consulta - Impressoras
        public static List<string> RetornaImpressoras(string uf, string cidade, string unidade, string celula, string setor, string ativo)
        {
            bool vlativo = true;

            if (ativo != null)
                if (ativo != "")
                    vlativo = bool.Parse(ativo);

            StringBuilder sql = new StringBuilder();
            #region CodigoOriginal

            //sql.Append("SELECT");
            //sql.Append(" a.nome_ficticio as CODIGO");
            //sql.Append(" from impr_snmp  as a");

            //if (vlativo)
            //    sql.Append(" WHERE (a.localizacao NOT LIKE '%DESATIVADA%')");
            //else
            //    sql.Append(" WHERE (a.localizacao LIKE '%DESATIVADA%')");

            //if (celula != null)
            //    if (celula != "")
            //        sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END) = '" + celula + "'");

            //if (uf != null)
            //    if (uf != "")
            //        sql.Append(" AND UPPER(LTRIM(RTRIM(a.uf))) like '" + uf + "'");

            //if (cidade != null)
            //    if (cidade != "")
            //        sql.Append(" AND UPPER(LTRIM(RTRIM(SUBSTRING(a.localizacao, 6, 300)))) like '" + cidade + "'");

            //if (unidade != null)
            //    if (unidade != "")
            //        sql.Append(" AND UPPER(LTRIM(RTRIM(a.unidade))) like '" + unidade + "'");

            ////if (setor != null)
            ////    if (setor != "")
            ////        sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN UPPER(LTRIM(RTRIM(a.setor))) ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,8,300)))) END) like '" + setor + "'");
            
            //sql.Append(" group by a.uf, a.localizacao, a.unidade, a.setor, a.numero_serie, a.ip, a.lote, a.dt_instalacao, a.nome_ficticio");

            //sql.Append(" ORDER BY 1");

            #endregion
            
            //Consulta na base baoprintbnb

            sql.Append(" select fila 'CODIGO' from baoprintbnb.dbo.vw_listaEquipamentos  as a");
            if (vlativo)
                sql.Append(" WHERE (a.cidade NOT LIKE '%DESATIVADA%')");
            else
                sql.Append(" WHERE (a.cidade LIKE '%DESATIVADA%')");
            if (celula != null)
                if (celula != "")
                    sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END) = '" + celula + "'");
            if (uf != null)
                if (uf != "")
                    sql.Append(" AND a.UF like '" + uf + "'");
            if (cidade != null)
                if (cidade != "")
                    sql.Append(" AND a.Cidade like '" + cidade + "'");
            if (unidade != null)
                if (unidade != "")
                    sql.Append(" AND UPPER(LTRIM(RTRIM(a.unidade))) like '" + unidade + "'");

            sql.Append(" group by a.UF, a.cidade, a.unidade, a.setor, a.serie, a.ip,  a.dt_instalacao, a.dt_consulta, a.fila ");

            sql.Append(" UNION ALL");

            //Consulta na base dnaprint

            sql.Append(" select fila 'CODIGO' from dnaprint.dbo.vw_listaEquipamentos  as a");
            if (vlativo)
                sql.Append(" WHERE (a.cidade NOT LIKE '%DESATIVADA%')");
            else
                sql.Append(" WHERE (a.cidade LIKE '%DESATIVADA%')");
            if (celula != null)
                if (celula != "")
                    sql.Append(" AND (CASE WHEN UPPER(LTRIM(RTRIM(a.setor))) not like '____ - %' THEN '--' ELSE UPPER(LTRIM(RTRIM(SUBSTRING(a.setor,0,5)))) END) = '" + celula + "'");
            if (uf != null)
                if (uf != "")
                    sql.Append(" AND a.UF like '" + uf + "'");
            if (cidade != null)
                if (cidade != "")
                    sql.Append(" AND a.Cidade like '" + cidade + "'");
            if (unidade != null)
                if (unidade != "")
                    sql.Append(" AND UPPER(LTRIM(RTRIM(a.unidade))) like '" + unidade + "'");

            sql.Append(" group by a.UF, a.cidade, a.unidade, a.setor, a.serie, a.ip,  a.dt_instalacao, a.dt_consulta, a.fila ");

            sql.Append(" ORDER BY 1");

            List<string> Impressoras = new List<string>();
            Banco banco = new Banco();
            //DataTable dt = banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Dados);
            DataTable dt = banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.DadosReplicado);

            foreach (DataRow row in dt.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                string grupo = rowReader.GetString("CODIGO");

                Impressoras.Add(grupo);
            }

            return Impressoras;
        }
        #endregion
    }
}