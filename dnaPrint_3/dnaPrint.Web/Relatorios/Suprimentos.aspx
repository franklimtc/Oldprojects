<%@ Page Title="Suprimentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suprimentos.aspx.cs" Inherits="dnaPrint.Web.Relatorios.Suprimentos" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     

<div class="container-fluid">
	<div class="row">
        <div class="md-col-12">
            <h3 class="text-default text-center">Relatórios de Suprimentos</h3>
        </div>
        <hr /><br />
    </div>

    <div class="row">
        <div class="col-md-12">
            <span class="label label-default"></span><br />
            <asp:Button runat="server" ID="tbAtualizar" Text="Atualizar" CssClass="btn btn-default" Width="100px" OnClick="tbAtualizar_Click" />
            <asp:Button runat="server" ID="tbExportar" Text="Exportar" CssClass="btn btn-default" Width="100px" OnClick="tbExportar_Click"/>
        </div>
    </div><br />

    <div class="row">
        <div class="md-col-12">
            <asp:GridView runat="server" ID="gvSuprimentos" CssClass="table table-hover table-condensed" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Uf" HeaderText="UF" SortExpression="Uf" />
                    <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
                    <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                    <asp:BoundField DataField="Setor" HeaderText="Setor" SortExpression="Setor" />
                    <asp:BoundField DataField="Fila" HeaderText="Fila" SortExpression="Fila" />
                    <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                    <asp:BoundField DataField="Ip" HeaderText="IP" SortExpression="Ip" />
                    <asp:BoundField DataField="DataTroca" HeaderText="Data Troca" SortExpression="DataTroca" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="Toner_ci" HeaderText="C" SortExpression="Toner_ci" HeaderStyle-BackColor="CornflowerBlue">
<HeaderStyle BackColor="CornflowerBlue"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Toner_ma" HeaderText="M" SortExpression="Toner_ma" HeaderStyle-BackColor="Magenta" >
<HeaderStyle BackColor="Magenta"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Toner_am" HeaderText="Y" SortExpression="Toner_am" HeaderStyle-BackColor="Yellow">
<HeaderStyle BackColor="Yellow"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Toner_pr" HeaderText="K" SortExpression="Toner_pr" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
<HeaderStyle BackColor="Black" ForeColor="White"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

     <div class="row">
        <div class="md-col-12">
            <rsweb:ReportViewer ID="Report" Visible="false" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
            </rsweb:ReportViewer>
        </div>
    </div>
</div>
</asp:Content>
