using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using SnmpSharpNet;
using System.Net;
using System.Collections.Generic;

/// <summary>
/// Summary description for DAO
/// </summary>

public class DAO
{
    public static void Execute(string sql, string Connectionname)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Connectionname].ToString());
        SqlCommand comand = new SqlCommand(sql, conn);
        try
        {
            conn.Open();
            comand.ExecuteNonQuery();
        }
        catch
        {
            conn.Close();
        }
        finally
        {
            conn.Close();
        }
    }

    public static string connString()
    {
        //return "Data Source=localhost;Initial Catalog=EasyAccount;Integrated Security=True"; 
        return ConfigurationManager.ConnectionStrings["pecasSigep01"].ToString();
    }

    public static bool ExecuteNonQuery(string ConnectionString, string query)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        bool resposta;
        try
        {
            connection.Open();
            comand.ExecuteNonQuery();
            resposta = true;
        }
        catch(Exception ex)
        {
            connection.Close();
            resposta = false;

            throw new Exception(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return resposta;
    }

    public static bool ExecuteNonQuery(string ConnectionString, SqlCommand comand)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        comand.Connection = connection;
        bool resposta;
        try
        {
            connection.Open();
            comand.ExecuteNonQuery();
            resposta = true;
        }
        catch(Exception ex)
        {
            connection.Close();
            resposta = false;
            throw new Exception(ex.Message);

        }
        finally
        {
            connection.Close();
        }
        return resposta;
    }

    public static DataTable RetornaDt(string ConnectionString, string query, List<string[]> parametros)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        SqlDataAdapter da = new SqlDataAdapter(comand);

        DataTable dt = new DataTable();

        foreach (var item in parametros)
        {
            comand.Parameters.AddWithValue(item[0], item[1]);
        }

        try
        {
            connection.Open();
            da.Fill(dt);
        }
        catch(Exception ex)
        {
            connection.Close();
            throw new Exception(ex.Message);

        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static DataTable RetornaDt(string ConnectionString, string query, List<object[]> parametros)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        SqlDataAdapter da = new SqlDataAdapter(comand);

        DataTable dt = new DataTable();

        foreach (var item in parametros)
        {
            comand.Parameters.AddWithValue(item[0].ToString(), item[1]);
        }

        try
        {
            connection.Open();
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            connection.Close();
            throw new Exception(ex.Message);

        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static DataTable RetornaDt(string ConnectionString, string query)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        SqlDataAdapter da = new SqlDataAdapter(comand);

        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            connection.Close();
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static string ExecuteScalar(string ConnectionString, string query)
    {
        string result = null;
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        try
        {
            connection.Open();
            result = comand.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            connection.Close();
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return result;
    }
   

    static string retornaValor(string v, object tsqlOperador)
    {
        throw new NotImplementedException();
    }

    public static string ConsultaSNMP(string OID, string IP)
    {
        string value = "";
        // SNMP community name
        OctetString community = new OctetString("public");

        // Define agent parameters class
        AgentParameters param = new AgentParameters(community);
        // Set SNMP version to 1
        param.Version = SnmpVersion.Ver1;
        // Construct the agent address object
        // IpAddress class is easy to use here because
        //  it will try to resolve constructor parameter if it doesn't
        //  parse to an IP address
        IpAddress agent = new IpAddress(IP);

        // Construct target
        UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

        // Define Oid that is the root of the MIB
        //  tree you wish to retrieve
        Oid rootOid = new Oid(OID); // ifDescr

        // This Oid represents last Oid returned by
        //  the SNMP agent
        Oid lastOid = (Oid)rootOid.Clone();

        // Pdu class used for all requests
        Pdu pdu = new Pdu(PduType.Get);

        // Loop through results
        //while (lastOid != null)
        //{
        // When Pdu class is first constructed, RequestId is set to a random value
        // that needs to be incremented on subsequent requests made using the
        // same instance of the Pdu class.
        if (pdu.RequestId != 0)
        {
            pdu.RequestId += 1;
        }
        // Clear Oids from the Pdu class.
        pdu.VbList.Clear();
        // Initialize request PDU with the last retrieved Oid
        pdu.VbList.Add(lastOid);
        // Make SNMP request
        SnmpV1Packet result = new SnmpV1Packet();
        try
        {
            result = (SnmpV1Packet)target.Request(pdu, param);
        }
        catch (NullReferenceException)
        {
            //Console.Write("Erro(NULL) ->  ");
            result = null;
        }

        // You should catch exceptions in the Request if using in real application.

        // If result is null then agent didn't reply or we couldn't parse the reply.
        if (result != null)
        {
            // ErrorStatus other then 0 is an error returned by 
            // the Agent - see SnmpConstants for error definitions
            if (result.Pdu.ErrorStatus != 0)
            {
                // agent reported an error with the request
                //Console.WriteLine("Erro na resposta SNMP. Error {0} index {1}",
                //    result.Pdu.ErrorStatus,
                //    result.Pdu.ErrorIndex);
                lastOid = null;
                //break;
            }
            else
            {
                // Walk through returned variable bindings
                foreach (Vb v in result.Pdu.VbList)
                {
                    // Check that retrieved Oid is "child" of the root OID
                    if (rootOid.IsRootOf(v.Oid))
                    {
                        //Console.WriteLine("{0} ({1}): {2}",v.Oid.ToString(),SnmpConstants.GetTypeName(v.Value.Type),
                        value = v.Value.ToString();
                        //);
                        //lastOid = v.Oid;
                    }
                    else
                    {
                        // we have reached the end of the requested
                        // MIB tree. Set lastOid to null and exit loop
                        lastOid = null;
                    }
                }
            }
        }
        else
        {
            value = null;
        }
        //}
        target.Close();

        return value;

    }

    public static bool ExecuteNonQuery(string ConnectionString, string query, List<string[]> parametros)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        foreach (var item in parametros)
        {
            comand.Parameters.AddWithValue(item[0], item[1]);
        }
        bool resposta;
        try
        {
            connection.Open();
            comand.ExecuteNonQuery();
            resposta = true;
        }
        catch(Exception ex)
        {
            connection.Close();
            resposta = false;
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return resposta;
    }

    public static bool ExecuteNonQuery(string ConnectionString, string query, List<object[]> parametros)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        SqlCommand comand = new SqlCommand(query, connection);
        foreach (var item in parametros)
        {
            comand.Parameters.AddWithValue(item[0].ToString(), item[1]);
        }
        bool resposta;
        try
        {
            connection.Open();
            comand.ExecuteNonQuery();
            resposta = true;
        }
        catch (Exception ex)
        {
            connection.Close();
            resposta = false;
            throw new Exception(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return resposta;
    }
}