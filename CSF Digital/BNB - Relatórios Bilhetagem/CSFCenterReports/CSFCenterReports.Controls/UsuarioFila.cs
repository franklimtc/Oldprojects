using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class UsuarioFila
    {
        #region Atributos
        private string _Id;
        private string _CodUsuario;
        private string _CodFila;
        private DateTime _DtCadastro;        
        private DateTime? _DtAlteracao;
        private bool _Ativo;        
        #endregion

        #region Métodos Get / Set
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string CodUsuario
        {
            get { return _CodUsuario; }
            set { _CodUsuario = value; }
        }
        public string CodFila
        {
            get { return _CodFila; }
            set { _CodFila = value; }
        }
        public DateTime DtCadastro
        {
            get { return _DtCadastro; }
            set { _DtCadastro = value; }
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
        #endregion

        #region Consulta - Filas do Usuário
        public static List<UsuarioFila> RetornaFilasUsuario(string usuario)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT");
            sql.Append(" a.id AS ID,");
            sql.Append(" a.usuario AS USUARIO,");
            sql.Append(" a.fila AS FILA,");
            sql.Append(" a.dtCadastro AS DTCADASTRO,");
            sql.Append(" a.dtAlteracao AS DTALTERACAO,");
            sql.Append(" a.status AS STATUS");
            sql.Append(" FROM UsuarioFila AS a");

            if (usuario != null)
            {
                sql.Append(" WHERE a.usuario = '" + usuario.Trim() + "'");
                sql.Append(" and a.status = 0");
            }
            else
            {
                sql.Append(" WHERE a.usuario = ''");
            }

            sql.Append(" ORDER BY 1");

            Banco banco = new Banco();

            return ProcessarFilasUsuario(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Sistema));
        }        
        #endregion

        #region Monta a lista de Filas de Usuário
        private static List<UsuarioFila> ProcessarFilasUsuario(DataTable dataTableUSD)
        {
            List<UsuarioFila> lista = new List<UsuarioFila>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                UsuarioFila filaUsuario = ConstruirFilaUsuario(rowReader);

                lista.Add(filaUsuario);
            }

            return lista;
        }

        private static UsuarioFila ConstruirFilaUsuario(NullableDataRowReader rowReader)
        {
            UsuarioFila filaUsuario = new UsuarioFila();

            filaUsuario.Id = rowReader.GetString("ID");
            filaUsuario.CodUsuario = rowReader.GetNullableString("USUARIO");
            filaUsuario.CodFila = rowReader.GetNullableString("FILA");
            filaUsuario.DtCadastro = rowReader.GetDateTime("DTCADASTRO");            
            filaUsuario.DtAlteracao = rowReader.GetNullableDateTime("DTALTERACAO");
            filaUsuario.Ativo = Util.Status(rowReader.GetInt32("STATUS"));            

            return filaUsuario;
        }
        #endregion

        #region Inserir - Usuário
        public static bool Inserir(UsuarioFila usuarioFila)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO UsuarioFila(usuario,fila,dtCadastro,dtAlteracao,status)");
            sql.Append(" VALUES(");
            sql.Append(" '" + usuarioFila.CodUsuario + "',");
            sql.Append(" '" + usuarioFila.CodFila + "',");            
            sql.Append(" CAST('" + Util.FormatarDateTimeSQL(usuarioFila.DtCadastro) + "' as datetime),");            
            sql.Append(" null,");
            sql.Append(Util.Status(usuarioFila.Ativo).ToString() + "");            
            sql.Append(")");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Alterar - Usuário
        public static bool Alterar(UsuarioFila usuarioFila)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE UsuarioFila SET ");
            sql.Append(" usuario = '" + usuarioFila.CodUsuario + "',");
            sql.Append(" fila = '" + usuarioFila.CodFila + "',");
            sql.Append(" dtAlteracao = CAST('" + Util.FormatarDateTimeSQL(DateTime.Now) + "' as datetime),");
            sql.Append(" status = " + Util.Status(usuarioFila.Ativo) + "");
            sql.Append(" WHERE id = '" + usuarioFila.Id.Trim() + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Remover - Usuário
        public static bool Remover(UsuarioFila usuarioFila)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE UsuarioFila SET ");            
            sql.Append(" status = " + Util.Status(usuarioFila.Ativo) + "");
            sql.Append(" WHERE id = '" + usuarioFila.Id.Trim() + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion
    }
}