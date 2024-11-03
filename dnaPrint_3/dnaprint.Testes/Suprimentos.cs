using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnaprint.Testes
{
    public class Suprimentos
    {
        #region Campos

        private string _serie;
        private string _serialToner;
        private DateTime _dtInicial;
        private DateTime _dtFinal;
        private int _contInicial;
        private int _contFinal;
        private int _producao;
        private bool _emUso;

        public string Serie { get => _serie; set => _serie = value; }
        public string SerialToner { get => _serialToner; set => _serialToner = value; }
        public DateTime DtInicial { get => _dtInicial; set => _dtInicial = value; }
        public DateTime DtFinal { get => _dtFinal; set => _dtFinal = value; }
        public int ContInicial { get => _contInicial; set => _contInicial = value; }
        public int ContFinal { get => _contFinal; set => _contFinal = value; }
        public int Producao { get => _contFinal - _contInicial; }
        public bool EmUso { get => _emUso; set => _emUso = value; }
        #endregion

        public static List<Suprimentos> Listar()
        {
            List<Suprimentos> Lista = new List<Suprimentos>();
            string tsql = @"SELECT serie, serialToner, MIN(DATA) dtInicial, MAX(DATA) dtFinal, min(totalcolor) + min(totalmono) contInicial, max(totalcolor) + max(totalmono) contFinal
FROM dadosDisparos  WHERE serie <> '' AND serialToner <> ''
GROUP BY serie, serialToner";

            DataTable dt = new dnaPrint.DAO.Operacoes(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.tipo.Postgre).ReturnDt(tsql);

            foreach (DataRow sup in dt.Rows)
            {
                Suprimentos newSup = new Suprimentos();
                newSup.Serie = sup["serie"].ToString();
                newSup.SerialToner = sup["serialToner"].ToString();
                newSup.DtInicial  = DateTime.Parse(sup["dtInicial"].ToString());
                newSup.DtFinal = DateTime.Parse(sup["dtFinal"].ToString());
                newSup.ContInicial = int.Parse(sup["contInicial"].ToString());
                newSup.ContFinal = int.Parse(sup["contFinal"].ToString());

                Lista.Add(newSup);
            }

            return Lista;
        }

        public static void DefinirSuprimentoAtual(List<Suprimentos> lista)
        {
            foreach (Suprimentos sup in lista)
            {
                var supProximo = lista.Find(x => x.DtFinal > sup.DtFinal);
                if (supProximo == null)
                {
                    sup.EmUso = true;
                }
            }
        }

        public static Suprimentos BuscarSuprimento(string serial, string serie)
        {
            List<Suprimentos> Lista = new List<Suprimentos>();
            string tsql = $@"SELECT serie, serialToner, MIN(DATA) dtInicial, MAX(DATA) dtFinal, min(totalcolor) + min(totalmono) contInicial, max(totalcolor) + max(totalmono) contFinal
FROM dadosDisparos  WHERE serialToner = '{serial}' and serie = '{serie}'
GROUP BY serie, serialToner";

            DataTable dt = new dnaPrint.DAO.Operacoes(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.tipo.Postgre).ReturnDt(tsql);

            foreach (DataRow sup in dt.Rows)
            {
                Suprimentos newSup = new Suprimentos();
                newSup.Serie = sup["serie"].ToString();
                newSup.SerialToner = sup["serialToner"].ToString();
                newSup.DtInicial = DateTime.Parse(sup["dtInicial"].ToString());
                newSup.DtFinal = DateTime.Parse(sup["dtFinal"].ToString());
                newSup.ContInicial = int.Parse(sup["contInicial"].ToString());
                newSup.ContFinal = int.Parse(sup["contFinal"].ToString());

                Lista.Add(newSup);
            }
            if (Lista.Count > 0)
                return Lista.First();
            else
                return null;
        }
    }
}
