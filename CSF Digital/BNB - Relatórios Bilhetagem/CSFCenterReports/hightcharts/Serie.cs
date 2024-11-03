using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace hightcharts
{
    public class Serie
    {
        #region campos
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _data;

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        #endregion

        public Serie(string name, string[] data)
        {
            this.Name = name;
            string temp = null;

            if (data.Length > 0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data.Length == 1)
                    {
                        // 1 elemento
                        temp += string.Format("{0}", data[i].ToString());
                    }
                    else
                    {
                        // Vários elementos
                        if (i != (data.Length - 1))
                        {
                            temp += string.Format("{0},", data[i].ToString());
                        }
                        else
                        {
                            //Último elemento
                            temp += string.Format("{0}", data[i].ToString());
                        }
                    }
                }
            }

            this.Text = @"{name: '" + this.Name + "', data: [" + temp + "]}";
        }

        public static string RetornaString(List<Serie> listaSeries)
        {
            string result = null;
            int cont = 1;
            if (listaSeries.Count > 0)
            {
                foreach (Serie s in listaSeries)
                {
                    if (listaSeries.Count != cont)
                    {
                        result += s.text + ",";
                    }
                    else
                    {
                        result += s.text;
                    }
                    cont++;
                }
            }

            return result;
        }

        public static string RetornaString(DataTable dtSeries)
        {
            string result = null;
            int coluns = dtSeries.Columns.Count;
            List<Serie> listaSeries = new List<Serie>();

            foreach (DataRow sRow in dtSeries.Rows)
            {
                string name = sRow[0].ToString();
                string[] data = new string[coluns-1];
                
                for (int i = 1; i < coluns; i++)
                {
                    data[i - 1] = sRow[i].ToString();
                }
                Serie sTemp = new Serie(name, data);
                listaSeries.Add(sTemp);
            }

            result = RetornaString(listaSeries);
            return result;
        }
    }
}
