<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dnaPrint.Web._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div id="Chart1"></div>
    </div>
    
    <div class="row">
        <div class="col-md-6">
            <div id="Chart2"></div>
        </div>
        
        <div class="col-md-6">
            <div id="Chart3"></div>
        </div>
    </div>

    <script type="text/javascript" src="../Scripts/Chart/highcharts.js"></script>
    <script type="text/javascript" src="../Scripts/Chart/modules/exporting.js"></script>

</asp:Content>
