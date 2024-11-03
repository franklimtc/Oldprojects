using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Usuario
    {
        #region Atributos
        private string _Codigo;
        private string _Senha;
        private string _Nome;
        private DateTime _DtCadastro;
        private DateTime _DtValidade;
        private DateTime? _DtAlteracao;
        private bool _Ativo;
        private string _CdGrupo;
        private bool _UtilizaAD;
        #endregion

        #region Métodos Get / Set
        public string Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public DateTime DtCadastro
        {
            get { return _DtCadastro; }
            set { _DtCadastro = value; }
        }
        public DateTime DtValidade
        {
            get { return _DtValidade; }
            set { _DtValidade = value; }
        }
        public DateTime? DtAlteracao
        {
            get { return _DtAlteracao; }
            set { _DtAlteracao = value; }
        }
        public bool Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }
        public string CdGrupo
        {
            get { return _CdGrupo; }
            set { _CdGrupo = value; }
        }
        public bool UtilizaAD
        {
            get { return _UtilizaAD; }
            set { _UtilizaAD = value; }
        }
        #endregion

        #region Consulta - Usuário
        public static Usuario RetornaUsuario(string Codigo)
        {
            return RetornaUsuarios(Codigo, null).Find(u => u.Codigo.ToUpper() == Codigo.ToUpper());
        }
        #endregion

        #region Consulta - Usuários
        public static List<Usuario> RetornaUsuarios(string grupo)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" Codigo AS CODIGO,");
            sql.Append(" Senha AS SENHA,");
            sql.Append(" Nome AS NOME,");
            sql.Append(" dtCadastro AS DTCADASTRO,");
            sql.Append(" dtValidade AS DTVALIDADE,");
            sql.Append(" dtAlteracao AS DTALTERACAO,");
            sql.Append(" Status AS STATUS,");
            sql.Append(" cdGrupo AS GRUPO,");
            sql.Append(" ad AS AD");
            sql.Append(" FROM Usuarios");

            if (grupo != null)
            {
                sql.Append(" WHERE cdGrupo = '" + grupo.Trim() + "'");
            }
            else
            {
                sql.Append(" WHERE cdGrupo = ''");
            }

            sql.Append(" ORDER BY 3,1");

            Banco banco = new Banco();

            return ProcessarUsuarios(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Sistema));
        }

        public static List<Usuario> RetornaUsuarios(string login, string nome)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" Codigo AS CODIGO,");
            sql.Append(" Senha AS SENHA,");
            sql.Append(" Nome AS NOME,");
            sql.Append(" dtCadastro AS DTCADASTRO,");
            sql.Append(" dtValidade AS DTVALIDADE,");
            sql.Append(" dtAlteracao AS DTALTERACAO,");
            sql.Append(" Status AS STATUS,");
            sql.Append(" cdGrupo AS GRUPO,");
            sql.Append(" ad AS AD");
            sql.Append(" FROM Usuarios");

            if ((login != null) && (nome != null))
            {
                sql.Append(" WHERE Codigo LIKE '%" + login.Trim() + "%'");
                sql.Append(" and Nome LIKE '%" + nome.Trim() + "%'");
            }
            else
            {
                if (login != null)
                    sql.Append(" WHERE Codigo LIKE '%" + login.Trim() + "%'");

                if (nome != null)
                    sql.Append(" WHERE Nome LIKE '%" + nome.Trim() + "%'");
            }

            sql.Append(" ORDER BY 3,1");

            Banco banco = new Banco();

            return ProcessarUsuarios(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Sistema));
        }
        #endregion

        #region Monta a lista de Usuários
        private static List<Usuario> ProcessarUsuarios(DataTable dataTableUSD)
        {
            List<Usuario> lista = new List<Usuario>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Usuario usuario = ConstruirGrupo(rowReader);

                lista.Add(usuario);
            }

            return lista;
        }

        private static Usuario ConstruirGrupo(NullableDataRowReader rowReader)
        {
            Usuario usuario = new Usuario();

            usuario.Codigo = rowReader.GetString("CODIGO");
            usuario.Senha = rowReader.GetNullableString("SENHA");
            usuario.Nome = rowReader.GetNullableString("NOME");
            usuario.DtCadastro = rowReader.GetDateTime("DTCADASTRO");
            usuario.DtValidade = rowReader.GetDateTime("DTVALIDADE");
            usuario.DtAlteracao = rowReader.GetNullableDateTime("DTALTERACAO");
            usuario.Ativo = Util.Status(rowReader.GetInt32("STATUS"));
            usuario.CdGrupo = rowReader.GetString("GRUPO");
            usuario.UtilizaAD = Util.Status(rowReader.GetInt32("AD"));

            return usuario;
        }
        #endregion

        #region Inserir - Usuário
        public static bool Inserir(Usuario usuario)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO Usuarios (codigo,senha,nome,dtCadastro,dtValidade,dtAlteracao,status,cdGrupo,ad)");
            sql.Append(" VALUES(");
            sql.Append(" '" + usuario.Codigo + "',");
            sql.Append(" '" + usuario.Senha + "',");
            sql.Append(" '" + usuario.Nome + "',");
            sql.Append(" CAST('" + Util.FormatarDateTimeSQL(usuario.DtCadastro) + "' as datetime),");
            sql.Append(" CAST('" + Util.FormatarDateTimeSQL(usuario.DtValidade) + "' as datetime),");
            sql.Append(" null,");
            sql.Append(Util.Status(usuario.Ativo).ToString() + ",");
            sql.Append("'" + usuario.CdGrupo + "',");
            sql.Append(Util.Status(usuario.UtilizaAD).ToString());
            sql.Append(")");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Alterar - Usuário
        public static bool Alterar(Usuario usuario)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE Usuarios SET ");
            sql.Append(" senha = '" + usuario.Senha + "',");
            sql.Append(" nome = '" + usuario.Nome + "',");
            sql.Append(" dtValidade = CAST('" + Util.FormatarDateTimeSQL(usuario.DtValidade) + "' as datetime),");
            sql.Append(" dtAlteracao = CAST('" + Util.FormatarDateTimeSQL(DateTime.Now) + "' as datetime),");
            sql.Append(" status = " + Util.Status(usuario.Ativo) + ",");
            sql.Append(" cdGrupo = '" + usuario.CdGrupo + "',");
            sql.Append(" ad = " + Util.Status(usuario.UtilizaAD));
            sql.Append(" WHERE codigo = '" + usuario.Codigo.Trim() + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }

        public static bool ResetarSenha(Usuario usuario, string senha)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE Usuarios SET ");
            sql.Append(" senha = '" + senha + "'");
            sql.Append(" WHERE codigo = '" + usuario.Codigo.Trim() + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }

        public static bool AtualizarDataAcesso(Usuario usuario)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE Usuarios SET ");
            sql.Append(" dtAlteracao = CAST('" + Util.FormatarDateTimeSQL(DateTime.Now) + "' as datetime)");
            sql.Append(" WHERE codigo = '" + usuario.Codigo.Trim() + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Remover - Usuário
        public static bool Remover(Usuario usuario)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("DELETE FROM Usuarios WHERE codigo = '" + usuario.Codigo + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion
    }
}