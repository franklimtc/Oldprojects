using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnaPrint.Base
{
    public class Graficos
    {
        public static void Principal(string connString, DAO.Operacoes.tipo Tipo)
        {
            DataTable dtDados = new DAO.Operacoes(connString, Tipo).ReturnDt("select tipo, sum(franquia) franquia, sum(contFinal - contInicial) volume  from bilhetagem('20170901','20170930') group by tipo order by 1");

            string[] listaVolume = new string[6];
            string[] listaFranquia = new string[6];
            if (dtDados.Rows.Count == 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    listaFranquia[i + 1] = dtDados.Rows[i][1].ToString();
                    listaVolume[i + 1] = dtDados.Rows[i][2].ToString();

                }
            }


        }
    }
}
