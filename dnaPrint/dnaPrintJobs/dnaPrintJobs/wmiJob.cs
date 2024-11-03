using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;


namespace dnaPrintJobs
{
    class wmiJob
    {
        public static string PaperSize(string JobId, string PrinterName)
        {
            string query = @"SELECT * FROM Win32_PrintJob where Caption like " + @"""" + "%" + PrinterName + "%" + @"""" + " and  JobId = " + JobId;
            string paper = null;
            ManagementObjectSearcher moSearch = new ManagementObjectSearcher(query);
            ManagementObjectCollection moCollection = moSearch.Get();

            foreach (ManagementObject mo in moCollection)
            {
                try
                {
                    paper = mo["PaperSize"].ToString();
                }
                catch 
                {
                    paper = "A4 (210 x 297 mm)";
                }
                break;
            } 
            
            return paper;
        }
    }
}
