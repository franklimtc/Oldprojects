using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class RendimentoSuprimento 
    {
        #region Campos

        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Unidade { get; set; }
        public string Setor { get; set; }
        public string Fila { get; set; }
        public string Serie { get; set; }
        public string Serial { get; set; }
        public string DTInicio { get; set; }
        public string DTFim { get; set; }
        public int ContInicial { get; set; }
        public int ContFinal { get; set; }
        public int Volume { get; set; }
        public int VolumeFab { get; set; }
        public int Percentual { get; set; }
        public string UltimaLeitura { get; set; }

        public RendimentoSuprimento()
        {

        }

        public RendimentoSuprimento(string _uf, string _cidade, string _unidade, string _setor, string _fila, string _serie, string _serial, string _dtInicio, string _dtFim
            , int _contInicial, int _contFinal, int _volume, int _volumeFab, int _percentual, string _ultimaLeitura)
        {
            this.UF = _uf;
            this.Cidade = _cidade;
            this.Unidade = _unidade;
            this.Setor = _setor;
            this.Fila = _fila;
            this.Serie = _serie;
            this.Serial = _serial;
            this.DTInicio = _dtInicio;
            this.DTFim = _dtFim;
            this.ContInicial = _contInicial;
            this.ContFinal = _contFinal;
            this.Volume = _volume;
            this.VolumeFab = _volumeFab;
            this.Percentual = _percentual;
            this.UltimaLeitura = _ultimaLeitura;
        }
        #endregion

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            throw new NotImplementedException();
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            throw new NotImplementedException();
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            throw new NotImplementedException();
        }

        public RendimentoSuprimento ListarByID(string connString, Operacoes.tipo Tipo, int id)
        {
            throw new NotImplementedException();
        }

        public static List<RendimentoSuprimento> Listar(string connString, Operacoes.tipo Tipo)
        {
            List<RendimentoSuprimento> Lista = new List<RendimentoSuprimento>();

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt($@"select a.uf, a.cidade, a.unidade, a.setor, a.fila, a.serie
, b.serialToner Serial
, b.dtInicio
, b.dtFim
, b.contInicial
, b.contFinal
, b.Volume
, b.volumeFab
, cast(b.producao as int) Percentual
, a.dt from vw_disponibilidade as a
left join vw_rendimentoSuprimentos as b on a.idEquipamento = b.idEquipamento
where b.serialToner is not null
order by 1, 2, 3, 4, 6, 9");

            if (dt.Rows.Count > 0)
            {
                int intemp = 0;
                foreach (DataRow _rend in dt.Rows)
                {
                    intemp = 0;
                    int.TryParse(_rend["Percentual"].ToString(), out intemp);
                    Lista.Add(new RendimentoSuprimento(_rend["uf"].ToString()
                        , _rend["cidade"].ToString()
                        , _rend["unidade"].ToString()
                        , _rend["setor"].ToString()
                        , _rend["fila"].ToString()
                        , _rend["serie"].ToString()
                        , _rend["Serial"].ToString()
                        , _rend["dtInicio"].ToString()
                        , _rend["dtFim"].ToString()
                        , int.Parse(_rend["contInicial"].ToString())
                        , int.Parse(_rend["contFinal"].ToString())
                        , int.Parse(_rend["Volume"].ToString())
                        , int.Parse(_rend["volumeFab"].ToString())
                        , intemp
                        , _rend["dt"].ToString()
                        ));
                }
            }

            return Lista;
        }
    }
}
