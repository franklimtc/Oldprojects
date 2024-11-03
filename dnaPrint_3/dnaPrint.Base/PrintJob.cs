using System;
using System.Collections.Generic;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class PrintJob
    {
        #region Campos

        private string _id;
        private string _Color;
        private string _Copies;
        private string _DataType;
        private string _Deleted;
        private string _Deleting;
        private string _Document;
        private string _DriverName;
        private string _InError;
        private string _JobId;
        private string _JobSize;
        private string _Landscape;
        private string _MachineName;
        private string _NotifyUserName;
        private string _Offline;
        private string _PagesPrinted;
        private string _PaperKind;
        private string _PaperLength;
        private string _PaperOut;
        private string _PaperSource;
        private string _PaperWidth;
        private string _Parameters;
        private string _Paused;
        private string _Position;
        private string _Printed;
        private string _PrinterName;
        private string _PrinterResolutionKind;
        private string _PrinterResolutionX;
        private string _PrinterResolutionY;
        private string _Printing;
        private string _PrintProcessorName;
        private string _Priority;
        private string _QueuedTime;
        private string _Spooling;
        private string _StatusDescription;
        private string _Submitted;
        private string _TimeWindow;
        private string _TotalPages;
        private string _UserInterventionRequired;
        private string _UserName;
        private string _server;

        public string Id { get => _id; set => _id = value; }
        public string Color { get => _Color; set => _Color = value; }
        public string Copies { get => _Copies; set => _Copies = value; }
        public string DataType { get => _DataType; set => _DataType = value; }
        public string Deleted { get => _Deleted; set => _Deleted = value; }
        public string Deleting { get => _Deleting; set => _Deleting = value; }
        public string Document { get => _Document; set => _Document = value; }
        public string DriverName { get => _DriverName; set => _DriverName = value; }
        public string InError { get => _InError; set => _InError = value; }
        public string JobId { get => _JobId; set => _JobId = value; }
        public string JobSize { get => _JobSize; set => _JobSize = value; }
        public string Landscape { get => _Landscape; set => _Landscape = value; }
        public string MachineName { get => _MachineName; set => _MachineName = value; }
        public string NotifyUserName { get => _NotifyUserName; set => _NotifyUserName = value; }
        public string Offline { get => _Offline; set => _Offline = value; }
        public string PagesPrinted { get => _PagesPrinted; set => _PagesPrinted = value; }
        public string PaperKind { get => _PaperKind; set => _PaperKind = value; }
        public string PaperLength { get => _PaperLength; set => _PaperLength = value; }
        public string PaperOut { get => _PaperOut; set => _PaperOut = value; }
        public string PaperSource { get => _PaperSource; set => _PaperSource = value; }
        public string PaperWidth { get => _PaperWidth; set => _PaperWidth = value; }
        public string Parameters { get => _Parameters; set => _Parameters = value; }
        public string Paused { get => _Paused; set => _Paused = value; }
        public string Position { get => _Position; set => _Position = value; }
        public string Printed { get => _Printed; set => _Printed = value; }
        public string PrinterName { get => _PrinterName; set => _PrinterName = value; }
        public string PrinterResolutionKind { get => _PrinterResolutionKind; set => _PrinterResolutionKind = value; }
        public string PrinterResolutionX { get => _PrinterResolutionX; set => _PrinterResolutionX = value; }
        public string PrinterResolutionY { get => _PrinterResolutionY; set => _PrinterResolutionY = value; }
        public string Printing { get => _Printing; set => _Printing = value; }
        public string PrintProcessorName { get => _PrintProcessorName; set => _PrintProcessorName = value; }
        public string Priority { get => _Priority; set => _Priority = value; }
        public string QueuedTime { get => _QueuedTime; set => _QueuedTime = value; }
        public string Spooling { get => _Spooling; set => _Spooling = value; }
        public string StatusDescription { get => _StatusDescription; set => _StatusDescription = value; }
        public string Submitted { get => _Submitted; set => _Submitted = value; }
        public string TimeWindow { get => _TimeWindow; set => _TimeWindow = value; }
        public string TotalPages { get => _TotalPages; set => _TotalPages = value; }
        public string UserInterventionRequired { get => _UserInterventionRequired; set => _UserInterventionRequired = value; }
        public string UserName { get => _UserName; set => _UserName = value; }
        public string Server { get => _server; set => _server = value; }

        #endregion

        public static List<PrintJob> ListByUser(string connString, Operacoes.tipo Tipo, string User, DateTime dtInicial, DateTime dtFinal)
        {
            string tsql = $"SELECT PrinterName, UserName, Document, Color, Copies, PagesPrinted, TotalPages, Submitted  FROM arquivoimpresso where UserName = '{User}' and submitted between @dtInicil and @dtFinal;";
            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@dtInicil", DateTime.Parse(dtInicial.ToShortDateString()) });
            parametros.Add(new object[] { "@dtFinal", DateTime.Parse(dtFinal.AddDays(1).ToShortDateString()) });

            List<PrintJob> Lista = new List<PrintJob>();

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql, parametros);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    PrintJob p = new PrintJob();
                    p.PrinterName = orow["PrinterName"].ToString();
                    p.UserName = orow["UserName"].ToString();
                    p.Document = orow["Document"].ToString();
                    p.Color = orow["Color"].ToString();
                    p.Copies = orow["Copies"].ToString();
                    p.PagesPrinted = orow["PagesPrinted"].ToString();
                    p.TotalPages = orow["TotalPages"].ToString();
                    p.Submitted = orow["Submitted"].ToString();

                    Lista.Add(p);
                }
            }

            return Lista;
        }

        public static List<PrintJob> ListByPrinter(string connString, Operacoes.tipo Tipo, string Printer, DateTime dtInicial, DateTime dtFinal)
        {
            string tsql = $"SELECT PrinterName, UserName, Document, Color, Copies, PagesPrinted, TotalPages, Submitted  FROM arquivoimpresso where PrinterName = '{Printer}' and submitted between @dtInicil and @dtFinal;";
            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@dtInicil", DateTime.Parse(dtInicial.ToShortDateString()) });
            parametros.Add(new object[] { "@dtFinal", DateTime.Parse(dtFinal.AddDays(1).ToShortDateString()) });

            List<PrintJob> Lista = new List<PrintJob>();

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql, parametros);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    PrintJob p = new PrintJob();
                    p.PrinterName = orow["PrinterName"].ToString();
                    p.UserName = orow["UserName"].ToString();
                    p.Document = orow["Document"].ToString();
                    p.Color = orow["Color"].ToString();
                    p.Copies = orow["Copies"].ToString();
                    p.PagesPrinted = orow["PagesPrinted"].ToString();
                    p.TotalPages = orow["TotalPages"].ToString();
                    p.Submitted = orow["Submitted"].ToString();

                    Lista.Add(p);
                }
            }

            return Lista;
        }

    }
}
