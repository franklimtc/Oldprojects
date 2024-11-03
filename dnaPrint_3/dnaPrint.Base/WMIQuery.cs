using System.Management;


namespace dnaPrint.Base
{
    class WMIQuery
    {

        public static string PrinterPort(string PortName)
        {
            string ip = null;

            ManagementClass printerPort = new ManagementClass("Win32_TCPIPPrinterPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_TCPIPPrinterPort");
            ManagementObjectCollection myPrintPortCollection = searcher.Get();

            foreach (var item in myPrintPortCollection)
            {
                string teste = item.ToString();
                if (item.ToString().Contains(PortName))
                {
                    ip = GetProperValue(item, "HostAddress").ToString();
                    break;
                }
            }

            return ip;
        }

        private static object GetProperValue(ManagementBaseObject items, string valor)
        {
            object value = null;
            foreach (var item in items.Properties)
            {
                if (item.Name == valor)
                    value = item.Value;
            }
            return value;
        }
    }
}
