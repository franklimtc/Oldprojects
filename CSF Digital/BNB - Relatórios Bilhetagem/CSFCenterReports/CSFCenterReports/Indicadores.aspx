<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="Indicadores.aspx.cs" Inherits="CSFCenterReports.Indicadores" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" Runat="Server">
    <div class="ColCenter">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            
        </asp:UpdatePanel>
    </div>
</asp:Content>