<%@ Page Title="Bilhetagem" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bilhetagem.aspx.cs" Inherits="dnaPrint.Web.Relatorios.Bilhetagem" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="md-col-12">
                <h3 class="text-default text-center">Relatórios de Bilhetagem</h3>
            </div>
            <hr />
        </div>
        <div class="row">
            <div class="col-md-3">
                <span class="label label-default">Data Inicial</span><br />
                <asp:TextBox runat="server" ID="tbdtInicial" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <span class="label label-default">Data Final</span><br />
                <asp:TextBox runat="server" ID="tbdtFinal" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <span class="label label-default"></span><br />
                <asp:Button runat="server" ID="tbExibir" Text="Exibir" CssClass="btn btn-default" Width="100px" OnClick="tbExibir_Click" />
                <asp:Button runat="server" ID="tbExportar" Text="Exportar" Enabled="false" CssClass="btn btn-default" Width="100px" OnClick="tbExportar_Click"/>
            </div>
        </div><br />

         <div class="row">
            <div class="md-col-12">
                <asp:GridView runat="server" ID="gvBilhetagem" CssClass="table table-hover table-condensed" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="IdEquipamento" HeaderText="ID" SortExpression="IdEquipamento" />
                        <asp:BoundField DataField="Uf" HeaderText="UF" SortExpression="Uf" />
                        <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
                        <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                        <asp:BoundField DataField="Setor" HeaderText="Setor" SortExpression="Setor" />
                        <asp:BoundField DataField="Fila" HeaderText="Fila" SortExpression="Fila" />
                        <asp:BoundField DataField="Serie" HeaderText="Série" SortExpression="Serie" />
                        <asp:BoundField DataField="Ip" HeaderText="IP" SortExpression="Ip" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                        <asp:BoundField DataField="Franquia" HeaderText="Franquia" SortExpression="Franquia" />
                        <asp:BoundField DataField="ContInicial" HeaderText="ContInicial" SortExpression="ContInicial" />
                        <asp:BoundField DataField="ContFinal" HeaderText="ContFinal" SortExpression="ContFinal" />
                        <asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" />
                        <asp:BoundField DataField="DataAtivacao" HeaderText="Data Instalação" SortExpression="DataAtivacao" DataFormatString="{0:d}" />
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
