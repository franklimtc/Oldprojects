<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="Resumo.aspx.cs" Inherits="CSFCenterReports.Resumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">

<div id="body" runat="server" style="width: 100%; margin: 0 auto">
<div id="dataHora" style="width:100%; text-align:right"><asp:Label ID="lbHora" runat="server"></asp:Label></div>
    <div id="graficoGeral" style="min-width: 100%; height: 500px; margin: 0 auto; display:block"></div>
    <div style="display:inline">
        <div id="graficoUsuarios" style="min-width: 48%; height: 400px; margin: 0 auto; display:inline-block"></div>
        <div id="graficoVar" style="min-width: 48%; height: 400px; margin: 0 auto; display:inline-block"></div>
    </div>
</div>

<script type="text/javascript" src="../../Scripts/highcharts.js"></script>
<script type="text/javascript" src="../../Scripts/modules/exporting.js"></script>

</asp:Content>
