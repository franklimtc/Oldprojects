using System.Collections.Generic;
using System.Data;
using dnaPrint.DAO;

namespace dnaPrint.Base
{
    public class OID
    {
        public int idPerfil { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string Firmware { get; set; }
        public string Oid { get; set; }
        public string Propriedade { get; set; }
        public object Valor { get; set; }

        public static List<OID> Listar(string connectionString, Operacoes.tipo TipoDB)
        {
            List<OID> Lista = new List<OID>();
            string query = "select * from vw_ListaOids";
            DataTable dt = new DataTable();

            dt = new Operacoes(connectionString, TipoDB).ReturnDt(query);

            foreach (DataRow o in dt.Rows)
            {
                OID oNew = new OID();
                oNew.idPerfil = int.Parse(o["idPerfil"].ToString());
                oNew.Fabricante = o["Fabricante"].ToString();
                oNew.Modelo = o["Modelo"].ToString();
                oNew.Firmware = o["Firmware"].ToString();
                oNew.Oid = o["Oid"].ToString();
                oNew.Propriedade = o["Propriedade"].ToString();
                Lista.Add(oNew);
            }

            return Lista;
        }

        public static List<OID> Listar(string descricao, string connectionString, Operacoes.tipo TipoDB)
        {
            List<OID> ListaTemp = Listar(connectionString, TipoDB);
            List<OID> Lista = new List<OID>(); ;
            if (!string.IsNullOrEmpty(descricao))
            {
                foreach (OID o in ListaTemp)
                {
                    if (descricao.Contains(o.Fabricante))
                    {
                        if (descricao.Contains(o.Modelo))
                        {
                            if (descricao.Contains(o.Firmware))
                            {
                                Lista.Add(o);
                            }
                        }
                    }
                }
            }
           
            return Lista;
        }

        public static bool Atualizar(string connectionString, Operacoes.tipo TipoDB, List<string[]> listaOids)
        {
            bool result = false;
            string tsqlTruncate = "delete from ListaOids";
            Operacoes opDB = new Operacoes(connectionString, TipoDB);
            opDB.ExecuteScalar(tsqlTruncate);
            int qtdLinhas = 0;

            foreach (var oid in listaOids)
            {
                qtdLinhas += opDB.ExecuteNonQuery( $"insert into ListaOids(idPerfil, fabricante, modelo, firmware, oid, propriedade) VALUES({oid[0]}, '{oid[1]}', '{oid[2]}', '{oid[3]}', '{oid[4]}', '{oid[5]}');");
            }

            if (listaOids.Count == qtdLinhas)
                result = true;
            
            return result;
        }

    }
}
