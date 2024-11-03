using System.Data;
using System.Text;
using System.Collections.Generic;
namespace dnaPrint
{
    class Listas
    {
        public static List<Equipamentos> CriarListaEquipamentos()
        {
            List<Equipamentos> listaExemplo = new List<Equipamentos>();
            Equipamentos novoEquipamento = new Equipamentos();
            novoEquipamento.IdEquipamento = "1";
            novoEquipamento.Ip = "10.85.4.67";
            novoEquipamento.Fabricante = "Xerox";

            listaExemplo.Add(novoEquipamento);
            return listaExemplo;
        }

        public static List<Oids> ListaOids(string connString, Disparo.TipoConexao tipo)
        {
            List<Oids> listadeOids = new List<Oids>();
            StringBuilder sbOids = new StringBuilder();
            //sbOids.AppendLine(" SELECT a.idPerfil,b.fabricante, b.firmware, a.oid, c.propriedade");
            //sbOids.AppendLine(" FROM oids AS a");
            //sbOids.AppendLine(" inner join perfis AS b ON a.idperfil = b.idperfil");
            //sbOids.AppendLine(" inner join propriedades AS c ON a.idpropriedade=c.idpropriedade");

            //Implementada a função TRIM devido o SqlCompact passar espaços em branco nas consultas.

            sbOids.AppendLine("SELECT * FROM listaOids");


            string sqlOids = sbOids.ToString();

            DataTable dtOids = new DataTable();
            if (tipo == Disparo.TipoConexao.sqlserver)
            {
                dtOids = DAO.RetornaDt(connString, sqlOids);
            }
            else
            {
                dtOids = DAO.RetornaDtSqlCompact(connString, sqlOids);
            }

            foreach (DataRow linha in dtOids.Rows)
            {
                Oids oid = new Oids(
                    linha[1].ToString(),
                    linha[2].ToString(),
                    linha[3].ToString(),
                    linha[4].ToString(),
                    linha[0].ToString()
                    );
                listadeOids.Add(oid);
            }
            return listadeOids;
        }

        public static List<oidsPadrao> ListaOidPadrao(string connString, Disparo.TipoConexao tipo)
        {

            string sqlOidsCadastradas = @"Select * from CadastroPerfilOid";


            DataTable dtOidsCadastradas = new DataTable();
            if (tipo == Disparo.TipoConexao.sqlserver)
            {
                dtOidsCadastradas = DAO.RetornaDt(connString, sqlOidsCadastradas);
            }
            else
            {
                dtOidsCadastradas = DAO.RetornaDtSqlCompact(connString, sqlOidsCadastradas);
            }

            List<oidsPadrao> listadeOidsPadrao = new List<oidsPadrao>(); // Lista as OID´s padrões por fabricante.

            foreach (DataRow linha in dtOidsCadastradas.Rows)
            {
                oidsPadrao nOid = new oidsPadrao();
                nOid.Fabricante = linha["fabricante"].ToString();
                nOid.Firmware = linha["firmware"].ToString();
                nOid.Oid = linha["oidPadrao"].ToString();

                listadeOidsPadrao.Add(nOid);
            }

            return listadeOidsPadrao;
        }

        public static List<List<Equipamentos>> RetornarListas(int qtd)
        {
            List<List<Equipamentos>> Listas = new List<List<Equipamentos>>();

            for (int i = 1; i <= qtd; i++)
            {
                List<Equipamentos> subLista = new List<Equipamentos>();
                Listas.Add(subLista);
            }
            return Listas;
        }

        public static List<List<object>> RetornarListas(int qtd, object objeto)
        {

            List<List<object>> Listas = new List<List<object>>();

            for (int i = 1; i <= qtd; i++)
            {
                List<object> subLista = new List<object>();
                Listas.Add(subLista);
            }
            return Listas;
        }
    }
}