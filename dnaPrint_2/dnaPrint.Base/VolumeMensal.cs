using System;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class VolumeMensal
    {
        #region Campos

        public string Serie { get; set; }
        public int ContInicial { get; set; }
        public int ContFinal { get; set; }
        public int Volume { get; set; }
        private int _MediaDia;
        public int MediaDia
        {
            get
            {
                if (_MediaDia > 0)
                    return _MediaDia;
                else
                    return 100;
            }
            set
            {
                _MediaDia = value;
            }
        }

        #endregion

        public VolumeMensal(string serie, Operacoes.tipo Tipo, string connString)
        {
            this.Serie = serie;
            //string tsql = $"SELECT * FROM VolumeMensal('{serie}')";
            string tsql = $"select * from volumeMensal('{serie}','{DateTime.Now.AddDays(DateTime.Now.Day*-1).ToString("yyyy-MM-dd")}','{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}')";

            DataTable dt = new DataTable();

            try
            {
                dt = new Operacoes(connString, Tipo).ReturnDt(tsql);
            }
            catch
            {

            }
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow linha in dt.Rows)
                {

                    int temp = 0;
                    if (int.TryParse(linha["contInicial"].ToString(), out temp))
                        this.ContInicial = temp;

                    if (int.TryParse(linha["contfinal"].ToString(), out temp))
                        this.ContFinal = temp;

                    if (int.TryParse(linha["volume"].ToString(), out temp))
                    {
                        this.Volume = temp;

                    }
                    else
                    {
                        this.Volume = 0;
                        this.MediaDia = 100;
                    }
                }
            }

        }
    }
}
