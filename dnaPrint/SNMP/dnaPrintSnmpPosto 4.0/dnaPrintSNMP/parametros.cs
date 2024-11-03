using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace dnaPrintSNMP
{
    public class parametros
    {
        private static DataTable dtParametros = new DataTable();

        public parametros()
        {
            dtParametros = DAO.RetornaDtCompact(string.Format("Data Source={0};Password=Senh@123", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config\oids.sdf"),
                "select * from parametros");
        }

        public static string retornaParametro(string parametro)
        {
            string value = null;
            if (dtParametros.Rows.Count > 0)
            {
                foreach (DataRow par in dtParametros.Rows)
                {
                    if (par["parametro"].ToString().Trim() == parametro)
                    {
                        value = par["value"].ToString().Trim();
                        break;
                    }
                }
            }
            return value;
        }
    }
}
