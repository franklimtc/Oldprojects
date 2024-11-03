using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class Bilhetagem 
    {

        #region Campos

        private string _idEquipamento;
        private string _uf;
        private string _cidade;
        private string _unidade;
        private string _setor;
        private string _fila;
        private string _serie;
        private string _ip;
        private string _contInicial;
        private string _contFinal;
        private string _volume;
        public string Franquia { get; set; }

        public string IdEquipamento { get => _idEquipamento; set => _idEquipamento = value; }
        public string Uf { get => _uf; set => _uf = value; }
        public string Cidade { get => _cidade; set => _cidade = value; }
        public string Unidade { get => _unidade; set => _unidade = value; }
        public string Setor { get => _setor; set => _setor = value; }
        public string Fila { get => _fila; set => _fila = value; }
        public string Serie { get => _serie; set => _serie = value; }
        public string Ip { get => _ip; set => _ip = value; }
        public string ContInicial { get => _contInicial; set => _contInicial = value; }
        public string ContFinal { get => _contFinal; set => _contFinal = value; }
        public string Volume { get => _volume; set => _volume = value; }
        public string Tipo { get; set; }
        public string DataAtivacao { get; set; }

        #endregion

        public static List<Bilhetagem> Listar(string connString, Operacoes.tipo Tipo, DateTime dtInicial, DateTime dtFinal)
        {
            string tsql = "select idEquipamento ,uf,cidade,unidade,setor,fila,serie,ip,contInicial,contFinal,volume, franquia, tipo, dtAtivacao from bilhetagem(@dtInicil, @dtFinal);";
            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@dtInicil", DateTime.Parse(dtInicial.ToShortDateString()) });
            parametros.Add(new object[] { "@dtFinal", DateTime.Parse(dtFinal.AddDays(1).ToShortDateString()) });

            List<Bilhetagem> Lista = new List<Bilhetagem>();

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql, parametros);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    Bilhetagem b = new Bilhetagem();

                    b.IdEquipamento = orow["idEquipamento"].ToString();
                    b.Uf = orow["uf"].ToString();
                    b.Cidade = orow["cidade"].ToString();
                    b.Unidade = orow["unidade"].ToString();
                    b.Setor = orow["setor"].ToString();
                    b.Fila = orow["fila"].ToString();
                    b.Serie = orow["serie"].ToString();
                    b.Ip = orow["ip"].ToString();
                    b.ContInicial = orow["contInicial"].ToString();
                    b.ContFinal = orow["contFinal"].ToString();
                    b.Volume = orow["volume"].ToString();
                    b.Franquia = orow["franquia"].ToString();
                    DateTime date = new DateTime();
                    if (DateTime.TryParse(orow["dtAtivacao"].ToString(), out date))
                    {
                        b.DataAtivacao = date.ToShortDateString();

                    }

                    b.Tipo = "Tipo " + orow["Tipo"].ToString();

                    Lista.Add(b);
                }
            }

            return Lista;
        }
    }
}
