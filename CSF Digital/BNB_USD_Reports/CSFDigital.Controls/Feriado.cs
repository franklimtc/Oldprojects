using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace CSFDigital.Controls
{
    public class Feriado
    {
        #region Atributos
        private string _cidade;
        private string _estado;
        private DateTime _data;
        private string _descricao;
        #endregion

        #region Métodos Get / Set
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public DateTime DataFeriado
        {
            get { return _data; }
            set { _data = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        #endregion

        #region Retorna Datas dos Feriados
        public static List<Feriado> Feriados = new List<Feriado>();

        public static List<Feriado> RetornaListaFeriados()
        {
            return Feriados;
        }

        public static void RetornaListaFeriados(string diretorio)
        {
            Feriados = RetornarDatasFeriados(diretorio);
        }

        private static List<Feriado> RetornarDatasFeriados(string diretorio)
        {
            string diretorioXML = diretorio;

            List<Feriado> Feriados = new List<Feriado>();

            if (File.Exists(diretorioXML))
            {
                DataSet ds = new DataSet();

                try
                {
                    ds.ReadXml(diretorioXML);

                    if (ds.Tables.Contains("Feriado"))
                    {
                        DataTable dt = ds.Tables["Feriado"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string UF = "";
                            string Cidade = "";
                            string Data = "";
                            string Descricao = "";

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Columns[j].Caption == "UF")
                                    UF = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Cidade")
                                    Cidade = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Data")
                                    Data = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "Descricao")
                                    Descricao = dt.Rows[i].ItemArray[j].ToString();
                            }

                            Feriado feriado = new Feriado();
                            feriado.Estado = UF;
                            feriado.Cidade = Cidade;
                            feriado.DataFeriado = DateTime.Parse(Data);
                            feriado.Descricao = Descricao;

                            Feriados.Add(feriado);
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { }
            }

            return Feriados;
        }
        #endregion
    }
}