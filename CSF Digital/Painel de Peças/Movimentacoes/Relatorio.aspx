<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Relatorio.aspx.cs" Inherits="Movimentacoes_Relatorio" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3 class="text-center">Relatório de Movimentações</h3>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>--%>
    <rsweb:ReportViewer  ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>
</asp:Content>

