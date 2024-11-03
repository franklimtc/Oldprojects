using SnmpSharpNet;
using System;
using System.Net;
using System.Net.NetworkInformation;

namespace dnaPrint.SNMP
{
    public class SNMP
    {
        public static string Get(string OID, string IP)
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
            catch (Exception ex)
            {
                //Console.Write("Erro(NULL) ->  ");
                //throw new Exception(ex.Message);
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

        public static bool OnLine(string IP)
        {
            bool result = false;
            IPAddress newIp = null;
            if (IPAddress.TryParse(IP, out newIp))
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(newIp);

                if (pingReply.Status == IPStatus.Success)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
