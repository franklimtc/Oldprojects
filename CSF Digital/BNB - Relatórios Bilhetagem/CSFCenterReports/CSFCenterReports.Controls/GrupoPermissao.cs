using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class GrupoPermissao
    {
        #region Atributos
        private string _CdGrupo;
        private string _Tela;
        private bool _PodeInserir;
        private bool _PodeAlterar;
        private bool _PodeExcluir;
        private bool _Administrador;

        #endregion

        #region Métodos Get / Set
        public string CdGrupo
        {
            get { return _CdGrupo; }
            set { _CdGrupo = value; }
        }
        public string Tela
        {
            get { return _Tela; }
            set { _Tela = value; }
        }
        public bool PodeInserir
        {
            get { return _PodeInserir; }
            set { _PodeInserir = value; }
        }
        public bool PodeAlterar
        {
            get { return _PodeAlterar; }
            set { _PodeAlterar = value; }
        }
        public bool PodeExcluir
        {
            get { return _PodeExcluir; }
            set { _PodeExcluir = value; }
        }
        public bool Administrador
        {
            get { return _Administrador; }
            set { _Administrador = value; }
        }

        #endregion

        #region Consulta - Grupo Permissões
        public static List<GrupoPermissao> RetornaGrupoPermissoes(int cdGrupo)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" cdGrupo AS GRUPO,");
            sql.Append(" dsTela AS TELA,");
            sql.Append(" stInserir AS INSERIR,");
            sql.Append(" stAlterar AS ALTERAR,");
            sql.Append(" stExcluir AS EXCLUIR");
            sql.Append(" stAdministrador AS ADMIN");
            sql.Append(" FROM GrupoPermissoes");

            if (cdGrupo != 0)
                sql.Append(" WHERE cdGrupo = " + cdGrupo.ToString());

            sql.Append(" ORDER BY 2,1");

            Banco banco = new Banco();

            return ProcessarGrupoPermissoes(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Sistema));
        }
        #endregion

        #region Monta a lista de Permissões
        private static List<GrupoPermissao> ProcessarGrupoPermissoes(DataTable dataTableUSD)
        {
            List<GrupoPermissao> lista = new List<GrupoPermissao>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                GrupoPermissao permissao = ConstruirPermissao(rowReader);

                lista.Add(permissao);
            }

            return lista;
        }

        private static GrupoPermissao ConstruirPermissao(NullableDataRowReader rowReader)
        {
            GrupoPermissao permissao = new GrupoPermissao();

            permissao.CdGrupo = rowReader.GetString("GRUPO");
            permissao.Tela = rowReader.GetString("TELA");
            permissao.PodeInserir = Util.Status(rowReader.GetInt32("INSERIR"));
            permissao.PodeAlterar = Util.Status(rowReader.GetInt32("ALTERAR"));
            permissao.PodeExcluir = Util.Status(rowReader.GetInt32("EXCLUIR"));
            permissao.Administrador = Util.Status(rowReader.GetInt32("ADMIN"));

            return permissao;
        }
        #endregion

        #region Inserir - Permissão
        public static bool Inserir(GrupoPermissao permissao)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO GrupoPermissoes (cdGrupo, dsTela, stInserir, stAlterar, stExcluir, stAdministrador)");
            sql.Append(" VALUES (");
            sql.Append("'" + permissao.CdGrupo + "',");
            sql.Append("'" + permissao.Tela + "',");
            sql.Append(Util.Status(permissao.PodeInserir).ToString() + ",");
            sql.Append(Util.Status(permissao.PodeAlterar).ToString() + ",");
            sql.Append(Util.Status(permissao.PodeExcluir).ToString() + ",");
            sql.Append(Util.Status(permissao.Administrador).ToString() + "");
            sql.Append(" )");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Alterar - Permissão
        public static bool Alterar(GrupoPermissao permissao)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE GrupoPermissoes SET ");
            sql.Append(" stInserir = " + Util.Status(permissao.PodeInserir).ToString() + ",");
            sql.Append(" stAlterar = " + Util.Status(permissao.PodeAlterar).ToString() + ",");
            sql.Append(" stExcluir = " + Util.Status(permissao.PodeExcluir).ToString() + ",");
            sql.Append(" stAdministrador = " + Util.Status(permissao.Administrador).ToString());
            sql.Append(" WHERE cdGrupo = '" + permissao.CdGrupo + "'");
            sql.Append(" AND dsTela = '" + permissao.Tela + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Remover - Permissão
        public static bool Remover(GrupoPermissao permissao)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("DELETE FROM GrupoPermissoes");
            sql.Append(" WHERE cdGrupo = '" + permissao.CdGrupo + "'");
            sql.Append(" AND dsTela = '" + permissao.Tela + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion
    }
}