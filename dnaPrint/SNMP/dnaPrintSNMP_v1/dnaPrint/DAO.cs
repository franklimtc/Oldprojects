using System;
using System.Data;
using System.Xml;
using SnmpSharpNet;
using System.Net;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace dnaPrint
{
    class DAO
    {
        private static string msg = "";

        public static DataTable RetornaDt(string ConnString, string Query)
        {
            DataTable dt = new DataTable();

            SqlConnection conexao = new SqlConnection(ConnString);
            SqlCommand comando = new SqlCommand(Query, conexao);
            SqlDataAdapter da = new SqlDataAdapter(comando);

            try
            {
                conexao.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                msg = "Falha na conexão como BD.\n" + ex.ToString();
                Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                conexao.Close();
            }
            finally
            {
                conexao.Close();
            }

            return dt;
        }

        public static void ExecutaSQL(string ConnString, string Query)
        {

            SqlConnection conexao = new SqlConnection(ConnString);
            SqlCommand comando = new SqlCommand(Query, conexao);
            SqlDataAdapter da = new SqlDataAdapter(comando);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                msg = "Falha na execução do comando.\n" + ex.ToString();
                Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                conexao.Close();
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
            }

        }

        public static DataTable RetornaDtSqlCompact(string ConnectionString, string query)
        {
            DataTable dt = new DataTable();
            SqlCeConnection conexao = new SqlCeConnection(ConnectionString);
            SqlCeCommand cmd = conexao.CreateCommand();
            cmd.CommandText = query;
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);

            try
            {
                conexao.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                msg = "Falha na conexão como BD.\n" + ex.ToString();
                Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                conexao.Close();
            }
            finally
            {
                conexao.Close();
            }

            return dt;
        }

        public static void ExecutaSqlCompact(string ConnectionString, string query)
        {
            SqlCeConnection conexao = new SqlCeConnection(ConnectionString);
            SqlCeCommand cmd = conexao.CreateCommand();
            cmd.CommandText = query;

            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                msg = "Falha na conexão como BD.\n" + ex.ToString();
                Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                conexao.Close();
            }
            finally
            {
                conexao.Close();
            }
        }

        public static void GerarTXT(string NomeArquivo, string Texto)
        {
            if (!File.Exists(NomeArquivo))
            {
                File.Create(NomeArquivo).Close();
            }
            TextWriter arquivo = File.AppendText(NomeArquivo);
            arquivo.WriteLine(Texto);
            arquivo.Flush();
            arquivo.Close();
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

        public static List<string> LerTxt(string Arquivo)
        {
            List<string> sb = new List<string>();
            string line;

            try
            {
                StreamReader sr = new StreamReader(Arquivo);
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Add(line.ToString());
                }
                sr.Close();
            }
            catch
            {
                sb.Add("");
            }

            return sb;
        }

        public static string RetornaAtributoXml(string arquivo, string atributo)
        {
            XmlTextReader reader = new XmlTextReader(arquivo);
            string temp = "";
            while (reader.Read())
            {
                if (reader.AttributeCount > 0)
                {
                    reader.MoveToAttribute(atributo);
                    temp = reader.Value;
                }
            }
            return temp;
        }

        public static int ContarArquivos(string Diretorio)
        {
            DirectoryInfo di = new DirectoryInfo(Diretorio);
            FileInfo[] lista = di.GetFiles();
            int contador = 0;
            foreach (FileInfo f in lista)
            {
                contador++;
            }
            return contador;
        }

    }
}
