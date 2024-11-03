using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace PPSWeb.Models
{
    public class RoleViewModels
    {
        public string RoleID { get; set; }
        public string Name { get; set; }
        public RoleViewModels( string id, string nome)
        {
            this.RoleID = id;
            this.Name = nome;
        }

        public static List<RoleViewModels> Listar()
        {
            List<RoleViewModels> Lista = new List<RoleViewModels>();

            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            string query = "select id, name from AspNetRoles";

            DataTable dtRoles = Controls.DAO.Retornadt(connString, query);
            if (dtRoles.Rows.Count > 0)
            {
                foreach (DataRow role in dtRoles.Rows)
                {
                    Lista.Add(new RoleViewModels(role["id"].ToString(), role["name"].ToString()));
                }
            }

            return Lista;
        }
    }
}