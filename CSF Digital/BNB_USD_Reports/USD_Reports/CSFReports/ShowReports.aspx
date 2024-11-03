<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowReports.aspx.cs" Inherits="ShowReports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CSF DIGITAL</title>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="overflow:auto; height:850px;">
            <rsweb:ReportViewer ID="DefaultReportViewer" runat="server" Width="1110" Height="820" SizeToReportContent="true" BackColor="LightSteelBlue" ShowPrintButton="False"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>