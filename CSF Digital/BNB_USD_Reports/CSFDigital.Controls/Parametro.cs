using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;

namespace CSFDigital.Controls
{
    public class Parametro
    {
        public static List<Parametro> Parametros = new List<Parametro>();

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

        #region Construtor
        public Parametro(string nome, string valor)
        {
            this.Nome = nome;
            this.Valor = valor;
        }
        #endregion

        public static List<Parametro> RetornaParametros()
        {
            return Parametros;
        }

        public static void RetornarParametros(string diretorio)
        {
            Parametros = RetornarListaParametros(diretorio);
        }

        private static List<Parametro> RetornarListaParametros(string diretorio)
        {
            string diretorioXML = diretorio;

            List<Parametro> Parametros = new List<Parametro>();

            if (File.Exists(diretorioXML))
            {
                DataSet ds = new DataSet();

                try
                {
                    ds.ReadXml(diretorioXML);

                    if (ds.Tables.Contains("Parametro"))
                    {
                        DataTable dt = ds.Tables["Parametro"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Nome = "";
                            string Valor = "";

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Columns[j].Caption == "Nome")
                                    Nome = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Valor")
                                    Valor = dt.Rows[i].ItemArray[j].ToString();
                            }

                            Parametro parametro = new Parametro(Nome, Valor);
                            Parametros.Add(parametro);
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { }
            }

            return Parametros;
        }
    }
}