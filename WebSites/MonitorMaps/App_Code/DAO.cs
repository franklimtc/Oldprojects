using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DAO
/// </summary>
public class DAO
{
	public DAO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable retornaDt(string query)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MapsConnection"].ToString());
        SqlCommand comand = new SqlCommand(query,conn);
        SqlDataAdapter da = new SqlDataAdapter(comand);

        DataTable dt = new DataTable();

        try {
            conn.Open();
            da.Fill(dt);
            conn.Close();
        }
        catch {
            conn.Close();
        }
        return dt;
    }

    public static bool Execute(string query)
    {
        bool result = false;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MapsConnection"].ToString());
        SqlCommand comand = new SqlCommand(query, conn);
        try
        {
            conn.Open();
            comand.ExecuteNonQuery();
            conn.Close();
            result = true;
        }
        catch
        {
            conn.Close();
        }

        return result;
    }
}