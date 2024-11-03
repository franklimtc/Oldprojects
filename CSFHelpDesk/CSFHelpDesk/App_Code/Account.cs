using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Account
/// </summary>


public class Account
{
    #region Campos
    private string _UserId;
    private string _UserName;
    private string _RoleId;
    private string _RoleName;

    public string UserId
    {
        get
        {
            return _UserId;
        }

        set
        {
            _UserId = value;
        }
    }

    public string UserName
    {
        get
        {
            return _UserName;
        }

        set
        {
            _UserName = value;
        }
    }

    public string RoleId
    {
        get
        {
            return _RoleId;
        }

        set
        {
            _RoleId = value;
        }
    }

    public string RoleName
    {
        get
        {
            return _RoleName;
        }

        set
        {
            _RoleName = value;
        }
    }
    #endregion

    public Account()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static bool associarUsuarioGrupo(string UserID, string RoleID)
    {
        bool result = false;
        string tsql = string.Format("INSERT INTO UsersInRoles(UserId, RoleId) VALUES('{0}','{1}');", UserID, RoleID);
        result = DAO.ExecuteNonQuery("DefaultConnection", tsql);
        return result;
    }

    public static bool Administrador(string UserName)
    {
        bool result = false;

        if (UserName != "")
        {
            string tsql = string.Format(@"select c.RoleName from UsersInRoles as a 
            left join Users as b on a.UserId = b.UserId left join Roles as c on a.RoleId = c.RoleId
            where UserName = '{0}'", UserName);
            try
            {
                if (DAO.ExecuteScalar("DefaultConnection", tsql).ToString() == "Administradores")
                    result = true;
            }
            catch
            {
            }
            
        }
        return result;
    }

    public static string RetornaGrupo(string UserName)
    {
        string grupo = "";

        if (UserName != "")
        {
            string tsql = string.Format(@"select c.RoleName from UsersInRoles as a 
            left join Users as b on a.UserId = b.UserId left join Roles as c on a.RoleId = c.RoleId
            where UserName = '{0}'", UserName);
            try
            {
                grupo = DAO.ExecuteScalar("DefaultConnection", tsql).ToString();
            }
            catch
            {
                grupo = "Indefinido";
            }
        }
        
        return grupo;
    }

    public static string RetornaUserId(string UserName)
    {
        string tsqlUser = string.Format("select UserId from Users where LOWER(UserName) = '{0}'", UserName.ToLower());
        return DAO.ExecuteScalar(DAO.connection.DefaultConnection.ToString(), tsqlUser).ToString();
    }

    public static List<Account> ListarContas()
    {
        List<Account> lista = new List<Account>();
        string tsql = string.Format("SELECT * FROM vw_usersinroles  order by UserName");
        DataTable dt = DAO.retornadt(DAO.connection.DefaultConnection.ToString(), tsql);
        foreach (DataRow ac in dt.Rows)
        {
            Account a = new Account();
            a.UserId = ac["UserId"].ToString();
            a.UserName = ac["UserName"].ToString();
            a.RoleId = ac["RoleId"].ToString();
            a.RoleName = ac["RoleName"].ToString();
            lista.Add(a);
        }
        return lista;
    }

    public static List<Account> ListarContas(string RoleName)
    {
        List<Account> lista = new List<Account>();
        string tsql = string.Format("SELECT * FROM vw_usersinroles where RoleName = '{0}' order by UserName", RoleName);
        DataTable dt = DAO.retornadt(DAO.connection.DefaultConnection.ToString(), tsql);
        foreach (DataRow ac in dt.Rows)
        {
            Account a = new Account();
            a.UserId = ac["UserId"].ToString();
            a.UserName = ac["UserName"].ToString();
            a.RoleId = ac["RoleId"].ToString();
            a.RoleName = ac["RoleName"].ToString();
            lista.Add(a);
        }
        return lista;
    }

}