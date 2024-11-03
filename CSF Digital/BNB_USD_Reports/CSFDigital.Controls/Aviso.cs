using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;

namespace CSFDigital.Controls
{
    public class Aviso
    {
        #region Atributos
        private int _id;        
        private string _descricao;
        private string _criadoPor;
        private DateTime _criadoEm;

        #endregion

        #region Métodos Get / Set
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public string CriadoPor
        {
            get { return _criadoPor; }
            set { _criadoPor = value; }
        }
        public DateTime CriadoEm
        {
            get { return _criadoEm; }
            set { _criadoEm = value; }
        }
        #endregion propriedades

        #region Construtor
        public Aviso(int Id, string Descricao, string CriadoPor, DateTime CriadoEm)
        {
            this.Id = Id;
            this.Descricao = Descricao;
            this.CriadoPor = CriadoPor;
            this.CriadoEm = CriadoEm;
        }
        #endregion

        #region Retorna Avisos
        public static List<Aviso> Avisos = new List<Aviso>();

        public static List<Aviso> RetornaAvisos()
        {
            return Avisos;
        }

        public static void RetornarAvisos(string diretorio)
        {
            Avisos = RetornaListaAvisos(diretorio);
        }

        private static List<Aviso> RetornaListaAvisos(string diretorio)
        {
            string diretorioXML = diretorio;

            List<Aviso> Avisos = new List<Aviso>();

            if (File.Exists(diretorioXML))
            {
                DataSet ds = new DataSet();

                try
                {
                    ds.ReadXml(diretorioXML);

                    if (ds.Tables.Contains("Aviso"))
                    {
                        DataTable dt = ds.Tables["Aviso"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int Id = 0;
                            string Descricao = "";
                            string CriadoPor = "";
                            DateTime CriadoEm = DateTime.Now;

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Columns[j].Caption == "Id")
                                    Id = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                if (dt.Columns[j].Caption == "Descricao")
                                    Descricao = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "CriadoPor")
                                    CriadoPor = dt.Rows[i].ItemArray[j].ToString();
                                if (dt.Columns[j].Caption == "CriadoEm")
                                    CriadoEm = DateTime.Parse(dt.Rows[i].ItemArray[j].ToString());
                            }

                            Aviso aviso = new Aviso(Id, Descricao, CriadoPor, CriadoEm);
                            Avisos.Add(aviso);
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { }
            }

            return Avisos;
        }
        #endregion
    }
}
