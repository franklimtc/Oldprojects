using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for Usuarios
/// </summary>
public class Usuarios
{
    private string _username;
    private string email;

    public string Username
    {
        get
        {
            return _username;
        }

        set
        {
            _username = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }

        set
        {
            email = value;
        }
    }

    public Usuarios(string username)
    {
        DataTable dtUser = new DataTable();
        string tsqlUser = string.Format("select username, email from aspnetdb.dbo.vw_aspnet_membershipusers where username = '{0}'", username);
        dtUser = DAO.RetornaDt(DAO.connString(), tsqlUser);
        if (dtUser.Rows.Count > 0)
        {
            foreach (DataRow rUser in dtUser.Rows)
            {
                this.Username = rUser["username"].ToString();
                this.Email = rUser["email"].ToString();
            }
        }

    }

    public static bool Administrador(string username)
    {
        bool result = false;
        string tsql = string.Format(@"select count(*)  from vw_aspnet_Roles as a 
left join vw_aspnet_usersInRoles as b on a.RoleId = b.RoleId
left join vw_aspnet_users as c on c.UserId = b.UserId
where username = '{0}' and rolename = 'administrador'", username);
        string value = DAO.ExecuteScalar(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString(), tsql);
        int qtd = 0;

        if (int.TryParse(value, out qtd))
        {
            if (qtd > 0)
            {
                result = true;
            }
        }
        return result;
    }
}