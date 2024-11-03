using System.Collections.Generic;
using dnaPrint.DAO;
using System.Data;
using System.Linq;
using System;

namespace dnaPrint.Base
{
    public class Suprimentos
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
        private string _toner_pr;
        private string _toner_ma;
        private string _toner_am;
        private string _toner_ci;
        private string _cilindro;
        private DateTime _data;
        public DateTime DataTroca { get; set; }

        public string IdEquipamento { get => _idEquipamento; set => _idEquipamento = value; }
        public string Uf { get => _uf; set => _uf = value; }
        public string Cidade { get => _cidade; set => _cidade = value; }
        public string Unidade { get => _unidade; set => _unidade = value; }
        public string Setor { get => _setor; set => _setor = value; }
        public string Fila { get => _fila; set => _fila = value; }
        public string Serie { get => _serie; set => _serie = value; }
        public string Ip { get => _ip; set => _ip = value; }
        public string Toner_pr { get => _toner_pr; set => _toner_pr = value; }
        public string Toner_ma { get => _toner_ma; set => _toner_ma = value; }
        public string Toner_am { get => _toner_am; set => _toner_am = value; }
        public string Toner_ci { get => _toner_ci; set => _toner_ci = value; }
        public string Cilindro { get => _cilindro; set => _cilindro = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public string Modelo { get; set; }
        public string Total { get; set; }

        #endregion

        public static List<Suprimentos> Listar(string connString, Operacoes.tipo Tipo)
        {
            List<Suprimentos> Lista = new List<Suprimentos>();

            string tsql = "select idEquipamento, uf, cidade, unidade, setor, fila, serie, modelo, ip, toner_pr, toner_ma, toner_am, toner_ci, cilindro, data, total, datatrocatoner from vw_suprimentos";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);
            if (dt.Rows.Count > 0)
            {
                DateTime dtTemp;

                foreach (DataRow orow in dt.Rows)
                {
                    Suprimentos supri = new Suprimentos();

                    supri.IdEquipamento = orow["idEquipamento"].ToString();
                    supri.Uf = orow["uf"].ToString();
                    supri.Cidade = orow["cidade"].ToString();
                    supri.Unidade = orow["unidade"].ToString();
                    supri.Setor = orow["setor"].ToString();
                    supri.Fila = orow["fila"].ToString();
                    supri.Serie = orow["serie"].ToString();
                    supri.Ip = orow["ip"].ToString();
                    supri.Toner_pr = orow["toner_pr"].ToString();
                    supri.Toner_ma = orow["toner_ma"].ToString();
                    supri.Toner_am = orow["toner_am"].ToString();
                    supri.Toner_ci = orow["toner_ci"].ToString();
                    supri.Cilindro = orow["cilindro"].ToString();
                    if (DateTime.TryParse(orow["data"].ToString(), out dtTemp))
                        supri.Data = dtTemp;
                    supri.Modelo = orow["modelo"].ToString();
                    supri.Total = orow["total"].ToString();
                    if (DateTime.TryParse(orow["datatrocatoner"].ToString(), out dtTemp))
                        supri.DataTroca = dtTemp;
                    Lista.Add(supri);
                }
            }

            return Lista.OrderBy(x => x.Uf).OrderBy(x=> x.Cidade).ToList();
        }
    }
}
