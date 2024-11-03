using System.Collections.Generic;
using System.Data;

namespace dnaPrint.Base
{
    public class Estado
    {
        #region Campos
        public int idEstado { get; set; }
        public string UF { get; set; }
        public string NomeEstado { get; set; }
        #endregion

        public Estado()
        {

        }

        public Estado(int id, string _uf, string descr)
        {
            this.idEstado = id;
            this.UF = _uf;
            this.NomeEstado = descr;
        }

        public static List<Estado> Listar(string connString, dnaPrint.DAO.Operacoes.tipo TipoDB)
        {
            List<Estado> lista = new List<Estado>();

            DataTable dt = new DAO.Operacoes(connString, TipoDB).ReturnDt("select idEstado, uf, estado from cadastroestado");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _estado in dt.Rows)
                {
                    Estado e = new Estado(
                        int.Parse(_estado["idEstado"].ToString())
                        , _estado["uf"].ToString()
                        , _estado["estado"].ToString()
                        );
                    lista.Add(e);
                }
            }

            return lista;
        }

    }
}
