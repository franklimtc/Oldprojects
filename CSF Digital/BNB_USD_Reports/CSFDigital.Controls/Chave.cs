using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;

namespace CSFDigital.Controls
{
    public class Chave
    {
        public static List<Chave> Chaves = new List<Chave>();

        #region Atributos
        private string _valorAntigo;
        private string _valor;
        private string _traducao;
        private bool _ativo;
        #endregion

        #region Métodos Get / Set
        public string ValorAntigo
        {
            get { return _valorAntigo; }
            set { _valorAntigo = value; }
        }
        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        public string Traducao
        {
            get { return _traducao; }
            set { _traducao = value; }
        }
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        #endregion propriedadesr

        public Chave(string valorAntigo, string valor, string traducao, bool ativo)
        {
            this.ValorAntigo = valorAntigo;
            this.Valor = valor;
            this.Traducao = traducao;
            this.Ativo = ativo;
        }

        public static List<Chave> RetornarChaves()
        {
            return Chaves;
        }

        public static void RetornarChaves(string diretorio)
        {
            Chaves = RetornarListaChaves(diretorio);
        }

        private static List<Chave> RetornarListaChaves(string diretorio)
        {
            string diretorioXML = diretorio;

            List<Chave> Chaves = new List<Chave>();

            if (File.Exists(diretorioXML))
            {
                DataSet ds = new DataSet();

                try
                {
                    ds.ReadXml(diretorioXML);

                    if (ds.Tables.Contains("Chave"))
                    {
                        DataTable dt = ds.Tables["Chave"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ValorAntigo = "";
                            string Valor = "";
                            string Traducao = "";
                            bool Ativo = false;

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Columns[j].Caption == "ValorAntigo")
                                    ValorAntigo = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Valor")
                                    Valor = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Traducao")
                                    Traducao = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Ativo")
                                    Ativo = bool.Parse(dt.Rows[i].ItemArray[j].ToString());
                            }

                            Chave chave = new Chave(ValorAntigo, Valor, Traducao, Ativo);
                            Chaves.Add(chave);
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { }
            }

            return Chaves;
        }
    }
}