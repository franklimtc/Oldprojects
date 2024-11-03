<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowReport.aspx.cs" Inherits="reports_ShowReport" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="600px">
            <LocalReport ReportPath="reports\SuprimentosSolicitados.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="dsRequisicoesSuprimentos" Name="dsSuprimentos" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="dsRequisicoesSuprimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT * FROM [vw_reqSuprimentosDatalhado]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
