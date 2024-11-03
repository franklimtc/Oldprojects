<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="Relatórios_report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
<div>
<table>
    <tr>
        <td>
            <asp:label ID="label1" runat="server" Text="Data Inicial:" Visible='false'></asp:label> 
            <%--<asp:Calendar ID="dtInicial" runat="server" Visible="false"></asp:Calendar>--%>
            <asp:TextBox ID="tbdtInicial" runat="server" Visible="false"></asp:TextBox>
        </td>
        <td>
            <asp:label ID="label2" runat="server" Text="Data Final:" Visible="false"></asp:label> 
            <%--<asp:Calendar ID="dtFinal" runat="server" Visible="false"></asp:Calendar>--%>
             <asp:TextBox ID="tbdtFinal" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><asp:Button ID="tbEmitir" Text="Emitir" runat="server" Visible="false" 
                onclick="tbEmitir_Click" /></td>
    </tr>
</table>
</div>
    <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>--%>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
</asp:Content>

