<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EnviosDiarios.aspx.cs" Inherits="Relatórios_EnviosDiarios"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
    <br />

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h2>Relatório de Envios Diários</h2>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                Informe a data inicial:
                <asp:TextBox ID="tbdata" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />
            </div>
           
        </div>
        <div class="row">
             <div class="col-md-2"><asp:Button ID="btEmitir" runat="server" Text="Emitir" CssClass="btn btn-secondary"
        onclick="btEmitir_Click" /></div> <br />
        </div>
        <div class="row">
            <div class="col-md-12">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>

