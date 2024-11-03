using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace hightcharts
{
    public class XAxis
    {
        /// <summary>
        /// Retorna as categorias do Eixo X formatadas para o script.
        /// </summary>
        /// <param name="xAxis"></param>
        /// <returns></returns>
        public static string returnXAxis(string[] xAxis)
        {
            string result = null;

            if (xAxis.Length > 0)
            {
                for (int i = 0; i < xAxis.Length; i++)
                {
                    if (xAxis.Length == 1)
                    {
                        // 1 elemento
                        result += string.Format("'{0}'", xAxis[i]);
                    }
                    else
                    {
                        // Vários elementos
                        if (i != (xAxis.Length - 1))
                        {
                            result += string.Format("'{0}',", xAxis[i]);
                        }
                        else
                        {
                            //Último elemento
                            result += string.Format("'{0}'", xAxis[i]);
                        }
                    }
                }
            }
            return result;
        }

        public static string returnXAxis(DataTable dtAxis)
        {
            string result = null;
            int coluns = dtAxis.Columns.Count;

            if (coluns > 0)
            {
                for (int i = 1; i < coluns; i++)
                {
                    if (coluns == 1)
                    {
                        // 1 elemento
                        result += string.Format("'{0}'", dtAxis.Columns[i].ColumnName);
                    }
                    else
                    {
                        if (i != (coluns - 1))
                        {
                            result += string.Format("'{0}',", dtAxis.Columns[i].ColumnName);
                        }
                        else
                        {
                            //Último elemento
                            result += string.Format("'{0}'", dtAxis.Columns[i].ColumnName);
                        }
                    }
                }
            }
            return result;
        }
    }
}
