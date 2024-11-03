using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace dnaPrint.Base
{
    public class Cidade
    {
        #region Campos

        public int idCidade { get; set; }
        public int idEstado { get; set; }
        public string NomeCidade { get; set; }

        #endregion

        public Cidade()
        {

        }

        public Cidade(int _idCidade, int _idEstado, string _nomeCidade)
        {
            this.idCidade = _idCidade;
            this.idEstado = _idEstado;
            this.NomeCidade = _nomeCidade;
        }

        public static List<Cidade> Listar(string connString, dnaPrint.DAO.Operacoes.tipo TipoDB, int idEstado)
        {
            List<Cidade> Lista = new List<Cidade>();

            DataTable dt = new DAO.Operacoes(connString, TipoDB).ReturnDt($"select idCidade, cidade from cadastroCidade where idEstado = {idEstado}");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _cidade in dt.Rows)
                {
                    Cidade c = new Cidade(
                        int.Parse(_cidade["idCidade"].ToString())
                        , idEstado
                        , _cidade["cidade"].ToString()
                        );
                    Lista.Add(c);
                }
            }

            return Lista.OrderBy(x => x.NomeCidade).ToList();
        }
    }
}
