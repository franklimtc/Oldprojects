using System.Collections.Generic;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class MonitorEquipamento
    {
        #region Campos

        public int idEquipamento { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Unidade { get; set; }
        public string COD { get; set; }
        public string Ambiente { get; set; }
        public string Serie { get; set; }
        public string Fila { get; set; }
        public string IP { get; set; }
        public string Data { get; set; }
        public int QTDDias { get; set; }
        public string Mac { get; set; }

        #endregion

        public static List<MonitorEquipamento> Listar(string connString, Operacoes.tipo TipoDB)
        {
            List<MonitorEquipamento> Lista = new List<MonitorEquipamento>();
            string tsql = "select idEquipamento, uf, cidade, unidade, setor, fila, serie, ip, dt, qtdDias, mac  from vw_disponibilidade where status = '1' order by 3,4;";

            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(tsql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow eqpto in dt.Rows)
                {
                    MonitorEquipamento e = new MonitorEquipamento();
                    e.idEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                    e.UF = eqpto["uf"].ToString();
                    e.Cidade = eqpto["cidade"].ToString();
                    e.Unidade = eqpto["unidade"].ToString();
                    e.Ambiente = eqpto["setor"].ToString();
                    e.Serie = eqpto["serie"].ToString();
                    e.Fila = eqpto["fila"].ToString();
                    e.IP = eqpto["ip"].ToString();
                    e.Data = eqpto["dt"].ToString();
                    e.Mac = eqpto["mac"].ToString();
                    if (!string.IsNullOrEmpty(eqpto["qtdDias"].ToString()))
                        e.QTDDias = int.Parse(eqpto["qtdDias"].ToString());
                    else
                        e.QTDDias = -1;
                    Lista.Add(e);
                }
            }
            return Lista;
        }
    }
}
