using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace dnaPrintSNMP
{
    public class snmp
    {
        public static List<string[]> getList(List<string[]> listaOids, string IP)
        {
            // SNMP community name
            OctetString community = new OctetString("public");

            // Define agent parameters class
            AgentParameters param = new AgentParameters(community);
            // Set SNMP version to 1 (or 2)
            //param.Version = SnmpVersion.Ver1;
            param.Version = SnmpVersion.Ver2;
            // Construct the agent address object
            // IpAddress class is easy to use here because
            //  it will try to resolve constructor parameter if it doesn't
            //  parse to an IP address
            IpAddress agent = new IpAddress(IP);

            // Construct target
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

            // Pdu class used for all requests
            Pdu pdu = new Pdu(PduType.Get);
            foreach (string[] s in listaOids)
            {
                if (s[1] != null)
                    pdu.VbList.Add(s[1].Trim());
            }

            // Make SNMP request
            //SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);
            SnmpV2Packet result = null;
            List<string[]> resposta = new List<string[]>();
            try
            {
                result = (SnmpV2Packet)target.Request(pdu, param);
            }
            catch
            {
                string[] temp = new string[2];
                temp[0] = "ERRO: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss - ") + " Falha na consulta SNMP.";
                resposta.Add(temp);
            }


            // If result is null then agent didn't reply or we couldn't parse the reply.
            if (result != null)
            {
                // ErrorStatus other then 0 is an error returned by 
                // the Agent - see SnmpConstants for error definitions
                if (result.Pdu.ErrorStatus != 0)
                {
                    // agent reported an error with the request
                    string[] temp = new string[2];
                    temp[0] = string.Format("Error in SNMP reply. Error {0} index {1}",
                        result.Pdu.ErrorStatus, result.Pdu.ErrorIndex);
                    resposta.Add(temp);
                }
                else
                {
                    // Reply variables are returned in the same order as they were added
                    //  to the VbList
                    for (int i = 0; i < result.Pdu.VbList.Count; i++)
                    {
                        string[] temp = new string[2];
                        temp[0] = result.Pdu[i].Oid.ToString();
                        if (result.Pdu[i].Value.ToString() != "SNMP No-Such-Instance")
                            temp[1] = result.Pdu[i].Value.ToString();
                        else
                            temp[1] = "-1";
                        resposta.Add(temp);
                    }
                }
            }
            else
            {

            }
            target.Close();
            return resposta;
        }
        public static string getValue(string oid, string IP)
        {
            // SNMP community name
            OctetString community = new OctetString("public");

            // Define agent parameters class
            AgentParameters param = new AgentParameters(community);
            // Set SNMP version to 1 (or 2)
            //param.Version = SnmpVersion.Ver1;
            param.Version = SnmpVersion.Ver2;
            // Construct the agent address object
            // IpAddress class is easy to use here because
            //  it will try to resolve constructor parameter if it doesn't
            //  parse to an IP address
            IpAddress agent = null;
            try
            {
                agent = new IpAddress(IP);
            }

            catch
            {
                return null;
            }

            // Construct target
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 1000, 1);

            // Pdu class used for all requests
            Pdu pdu = new Pdu(PduType.Get);
            pdu.VbList.Add(oid);

            // Make SNMP request
            //SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);
            SnmpV2Packet result = null;
            string resposta = null;
            try
            {
                result = (SnmpV2Packet)target.Request(pdu, param);
            }
            catch
            {
                resposta = ("ERRO: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss - ") + " Falha na consulta SNMP.");
            }


            // If result is null then agent didn't reply or we couldn't parse the reply.
            if (result != null)
            {
                // ErrorStatus other then 0 is an error returned by 
                // the Agent - see SnmpConstants for error definitions
                if (result.Pdu.ErrorStatus != 0)
                {
                    // agent reported an error with the request
                    resposta = (string.Format("Error in SNMP reply. Error {0} index {1}",
                        result.Pdu.ErrorStatus, result.Pdu.ErrorIndex));
                }
                else
                {
                    // Reply variables are returned in the same order as they were added
                    //  to the VbList
                    for (int i = 0; i < result.Pdu.VbList.Count; i++)
                    {
                        resposta = (result.Pdu[i].Value.ToString());
                    }
                }
            }
            else
            {

            }
            target.Close();
            return resposta;
        }
        public static void AtualizarOids()
        {
            if (downloadFile())
            {
                string connString = string.Format("Data Source={0};Password=Senh@123", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config\oids.sdf");
                List<string> listaNova = DAO.LerTxt("config/cargaOids.txt");
                int qtdAtual = 0;
                try { qtdAtual = int.Parse(DAO.RetornaValorCompact(connString, "select count(*) from listaOids")); }
                catch { };
                if (qtdAtual > 0 && qtdAtual <= listaNova.Count)
                {
                    DAO.ExecuteCompact(connString, "delete from listaOids;");
                    DAO.ExecuteCompact(connString, "insert into atualizacoes(data) values(getdate())");
                    foreach (string sql in listaNova)
                    {
                        DAO.ExecuteCompact(connString, sql);
                    }
                }
            }
        }
        static bool downloadFile()
        {
            bool result = false;
            WebClient client = new WebClient();
            string htmlCode = null;
            string[] lines = null;
            try
            {
                htmlCode = client.DownloadString("http://www.csfdigital.com.br/cargaoids.txt");
                lines = Regex.Split(htmlCode, "\r\n");
            }
            catch { }
            if (htmlCode != null)
            {
                if (lines.Length > 1)
                {
                    try { File.Delete("config/cargaOids.txt"); }
                    catch { }
                    foreach (string line in lines)
                    {
                        DAO.GerarTXT("config/cargaOids.txt", line);
                        Console.WriteLine(string.Format("Linha {0}: {1}.", line.GetEnumerator(), line));
                    }
                    result = true;
                }
            }
            return result;
        }
    }
}
