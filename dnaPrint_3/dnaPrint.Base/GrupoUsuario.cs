using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class GrupoUsuario 
    {
        #region
        public int ID { get; set; }
        public string Grupo { get; set; }
        #endregion

        public List<GrupoUsuario> Listar(string connString, Operacoes.tipo Tipo)
        {
            List<GrupoUsuario> Lista = new List<GrupoUsuario>();

            string tsql = "select idgrupousuario, grupo from GruposUsuarios where status = '1';";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    GrupoUsuario grupo = new GrupoUsuario();
                    grupo.ID = int.Parse(orow["idgrupousuario"].ToString());
                    grupo.Grupo = orow["grupo"].ToString();
                    Lista.Add(grupo);
                }
            }

            return Lista;
        }
    }
}
