<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Ocomon_II.aspx.cs" Inherits="Suprimentos_Ocomon_II" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectOcomon" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="Numero" HeaderText="Numero" SortExpression="Numero" />
        <asp:BoundField DataField="Local" HeaderText="Local" SortExpression="Local" />
        <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
        <asp:BoundField DataField="Descricao" HeaderText="Descricao" SortExpression="Descricao" />
        <asp:BoundField DataField="Toner" HeaderText="Toner" SortExpression="Toner" />
        <asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="Foto" />
        <asp:BoundField DataField="DataAbertura" HeaderText="DataAbertura" SortExpression="DataAbertura" DataFormatString="{0:d}" />
        <asp:BoundField DataField="QtdDias" HeaderText="QtdDias" SortExpression="QtdDias" />
        <asp:BoundField DataField="TpEnvio" HeaderText="TpEnvio" SortExpression="TpEnvio" />
        <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
    </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectOcomon" runat="server" SelectMethod="retornaWebOcomon" TypeName="OcomonWeb"></asp:ObjectDataSource>
</asp:Content>

