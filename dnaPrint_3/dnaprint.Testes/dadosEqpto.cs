using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnaprint.Testes
{
    public class dadosEqpto
    {
        #region Campos

        private int idEquipamento;
        private string serie;
        private string uf;
        private string cidade;
        private string unidade;
        private string setor;
        private string fila;
        private string ip;
        private int toner_pr;
        private int toner_ma;
        private int toner_am;
        private int toner_ci;
        private int cilindro;
        private DateTime data;
        private string modelo;
        private int total;
        private string serieDisparo;
        private int contInicial;
        private int contFinal;
        private int volume;
        private int media;
        private string serialToner;

        private Suprimentos suprimento;

        public int IdEquipamento { get => idEquipamento; set => idEquipamento = value; }
        public string Serie { get => serie; set => serie = value; }
        public string Uf { get => uf; set => uf = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Unidade { get => unidade; set => unidade = value; }
        public string Setor { get => setor; set => setor = value; }
        public string Fila { get => fila; set => fila = value; }
        public string Ip { get => ip; set => ip = value; }
        public int Toner_pr { get => toner_pr; set => toner_pr = value; }
        public int Toner_ma { get => toner_ma; set => toner_ma = value; }
        public int Toner_am { get => toner_am; set => toner_am = value; }
        public int Toner_ci { get => toner_ci; set => toner_ci = value; }
        public int Cilindro { get => cilindro; set => cilindro = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public int Total { get => total; set => total = value; }
        public string SerieDisparo { get => serieDisparo; set => serieDisparo = value; }
        public int ContInicial { get => contInicial; set => contInicial = value; }
        public int ContFinal { get => contFinal; set => contFinal = value; }
        public int Volume { get => volume; set => volume = value; }
        public int Media { get => media; set => media = value; }
        public Suprimentos Suprimento { get => suprimento; set => suprimento = value; }
        public string SerialToner { get => serialToner; set => serialToner = value; }

        #endregion

        public static List<dadosEqpto> Listar()
        {
            string tsql = @"SELECT a.idEquipamento, a.serie, a.uf, a.cidade, a.unidade, a.setor, a.fila, a.ip
, a.toner_pr, a.toner_ma, a.toner_am, a.toner_ci,a.cilindro, a.data, a.modelo, a.total, a.serialToner
, b.serie serieDisparo, b.contInicial, b.contFinal, b.volume, b.media  
FROM VW_SUPRIMENTOS a left join  vw_volumeDiario b on a.idEquipamento = b.idEquipamento";
            DataTable dt = new dnaPrint.DAO.Operacoes(ConfigurationManager.ConnectionStrings["db"].ToString(), dnaPrint.DAO.Operacoes.tipo.Postgre).ReturnDt(tsql);
            List<dadosEqpto> Lista = new List<dadosEqpto>();
            foreach (DataRow eqpto in dt.Rows)
            {
                dadosEqpto d = new dadosEqpto();
                d.IdEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                d.Serie = eqpto["serie"].ToString();
                d.Uf = eqpto["uf"].ToString();
                d.Cidade = eqpto["cidade"].ToString();
                d.Unidade = eqpto["unidade"].ToString();
                d.Setor = eqpto["setor"].ToString();
                d.Fila = eqpto["fila"].ToString();
                d.Ip = eqpto["ip"].ToString();
                d.SerieDisparo = eqpto["serieDisparo"].ToString();
                d.SerialToner = eqpto["serialToner"].ToString();
                int tonerTemp;
                if (int.TryParse(eqpto["toner_pr"].ToString(), out tonerTemp))
                {
                    d.Toner_pr = tonerTemp;
                }
                if (int.TryParse(eqpto["toner_ma"].ToString(), out tonerTemp))
                {
                    d.Toner_ma = tonerTemp;
                }
                if (int.TryParse(eqpto["toner_am"].ToString(), out tonerTemp))
                {
                    d.Toner_am = tonerTemp;
                }
                if (int.TryParse(eqpto["toner_ci"].ToString(), out tonerTemp))
                {
                    d.Toner_ci = tonerTemp;
                }
                if (int.TryParse(eqpto["cilindro"].ToString(), out tonerTemp))
                {
                    d.Cilindro = tonerTemp;
                }

                if (int.TryParse(eqpto["total"].ToString(), out tonerTemp))
                {
                    d.Total = tonerTemp;
                }

                if (int.TryParse(eqpto["contInicial"].ToString(), out tonerTemp))
                {
                    d.ContInicial = tonerTemp;
                }

                if (int.TryParse(eqpto["contFinal"].ToString(), out tonerTemp))
                {
                    d.ContFinal = tonerTemp;
                }
                if (int.TryParse(eqpto["volume"].ToString(), out tonerTemp))
                {
                    d.Volume = tonerTemp;
                }
                if (int.TryParse(eqpto["media"].ToString(), out tonerTemp))
                {
                    d.Media = tonerTemp;
                }

                DateTime dttemp;
                if (DateTime.TryParse(eqpto["data"].ToString(), out dttemp))
                {
                    d.Data = dttemp;
                }

                d.Modelo = eqpto["modelo"].ToString();

                Lista.Add(d);

            }
            return Lista;
        }
    }
}
