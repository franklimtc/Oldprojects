using System;
using System.Collections.Generic;
using System.Net;
using System.Printing;
using System.Text.RegularExpressions;

namespace dnaPrint
{
    class Portas
    {
        public static IPAddress[] ipTemp = null;

        public static List<IPAddress> RetornaIps()
        {
            LocalPrintServer pServer = new LocalPrintServer();
            PrintQueueCollection printers = pServer.GetPrintQueues();
            List<IPAddress> ips = new List<IPAddress>();

            foreach (PrintQueue p in printers)
            {
                if (p.QueueDriver.Name.Contains("Xerox") || p.QueueDriver.Name.Contains("Kyocera"))
                {
                    if (!Duplicados(ips, validaIp(p.QueuePort.Name)))
                    {
                        ips.Add(validaIp(p.QueuePort.Name));
                    }
                }
            }
            return ips;
        }

        static bool Duplicados(List<IPAddress> lista, IPAddress ip)
        {
            bool result = false;
            if (ip != null)
            {
                foreach (IPAddress i in lista)
                {
                    if (i.ToString() == ip.ToString())
                        result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        static IPAddress validaIp(string ip)
        {
            IPAddress ipValido = null;
            string pattern = "([0-9]+)(.)([0-9]+)(.)([0-9]+)(.)([0-9]+)";


            if (isIpAddres(ip))
            {
                ipValido = IPAddress.Parse(Regex.Match(ip, pattern).ToString());
            }
            else
            {
                if (isHostName(ip))
                {
                    ipValido = ipTemp[0];
                    ipTemp = null;
                }
            }
            return ipValido;
        }

        static bool isIpAddres(string ip)
        {
            bool result = false;

            string pattern = "([0-9]+)(.)([0-9]+)(.)([0-9]+)(.)([0-9]+)";
            string ipTemp = null;

            ipTemp = Regex.Match(ip, pattern).ToString();

            try
            {
                IPAddress.Parse(ipTemp);
                result = true;
            }
            catch
            {

            }
            return result;
        }

        static bool isHostName(string host)
        {
            bool result = false;

            try
            {
                ipTemp = Dns.GetHostAddresses(host);
                result = true;
            }
            catch
            { }
            return result;
        }
    }
}