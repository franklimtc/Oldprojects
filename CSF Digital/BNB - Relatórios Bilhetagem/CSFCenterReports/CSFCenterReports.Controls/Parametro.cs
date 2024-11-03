using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class Parametro
    {
        #region Atributos
        private string _nome;
        private string _valor;
        #endregion

        #region Métodos Get / Set
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }       
        #endregion propriedades

        #region Consulta - Parametro
        public static Parametro RetornaParametro(string nome)
        {
            return RetornaParametros(null).Find(p => p.Nome == nome);
        }
        #endregion

        #region Consulta - Parametros
        public static List<Parametro> RetornaParametros(string nome)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT ");
            sql.Append(" nome AS NOME,");
            sql.Append(" valor AS VALOR");
            sql.Append(" FROM Parametros");

            if (nome != null)
                sql.Append(" and nome like '%" + nome + "%'");
           
            sql.Append(" ORDER BY 1,2");

            Banco banco = new Banco();

            return ProcessarParametros(banco.RetornarTabela(sql.ToString(), Banco.TipoConexao.Sistema));
        }       
        #endregion

        #region Monta a lista de Parametros
        private static List<Parametro> ProcessarParametros(DataTable dataTableUSD)
        {
            List<Parametro> lista = new List<Parametro>();

            foreach (DataRow row in dataTableUSD.Rows)
            {
                NullableDataRowReader rowReader = new NullableDataRowReader(row);

                Parametro parametro = ConstruirGrupo(rowReader);

                lista.Add(parametro);
            }

            return lista;
        }

        private static Parametro ConstruirGrupo(NullableDataRowReader rowReader)
        {
            Parametro parametro = new Parametro();

            parametro.Nome = rowReader.GetString("NOME");
            parametro.Valor = rowReader.GetNullableString("VALOR");

            return parametro;
        }
        #endregion        

        #region Inserir - Parametro
        public static bool Inserir(Parametro parametro)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO Parametros (nome,valor)");
            sql.Append(" VALUES(");
            sql.Append(" '" + parametro.Nome + "',");
            sql.Append(" '" + parametro.Valor + "'");            
            sql.Append(")");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion

        #region Alterar - Parametro
        public static bool Alterar(Parametro parametro)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE Parametros SET ");
            sql.Append(" valor = '" + parametro.Valor + "'");
            sql.Append(" WHERE nome = '" + parametro.Nome + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }        
        #endregion

        #region Remover - Parametro
        public static bool Remover(Parametro parametro)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("DELETE FROM Parametros WHERE nome = '" + parametro.Nome + "'");

            Banco banco = new Banco();

            return banco.ExecutarComando(sql.ToString(), Banco.TipoConexao.Sistema);
        }
        #endregion
    }
}