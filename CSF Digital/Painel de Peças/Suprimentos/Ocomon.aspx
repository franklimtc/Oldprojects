<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Ocomon.aspx.cs" Inherits="Relatórios_Ocomon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:GridView ID="gvOcomon" runat="server" DataSourceID="dbResumoOcomon" 
        Width="98%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="None">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="numero" HeaderText="Número" ItemStyle-HorizontalAlign="Center"
            SortExpression="numero" />
        <asp:BoundField DataField="local" HeaderText="Local" SortExpression="local" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center"
            SortExpression="status" />
        <asp:BoundField DataField="serie" HeaderText="Série" ItemStyle-HorizontalAlign="Center"
            SortExpression="serie" />
        <asp:BoundField DataField="Suprimento" HeaderText="Suprimento" ItemStyle-HorizontalAlign="Center"
            SortExpression="Suprimento" ReadOnly="True" />
        <asp:BoundField DataField="Envio" ItemStyle-HorizontalAlign="Center"
            HeaderText="Forma de Envio" SortExpression="Envio" ReadOnly="True" />
        <asp:BoundField DataField="descricao" HeaderText="Descrição" 
            SortExpression="descricao" />
        <asp:BoundField DataField="data_abertura" ItemStyle-HorizontalAlign="Center"
            HeaderText="Aberto em" SortExpression="data_abertura" />
        <asp:BoundField DataField="qtdDias" HeaderText="Dias" ItemStyle-HorizontalAlign="Center"
            SortExpression="qtdDias" />
	<asp:BoundField DataField="Urgente" HeaderText="Situação" 
            SortExpression="Urgente" />
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


<asp:SqlDataSource ID = "dbResumoOcomon" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand = "select * from vwResumoOcomon order by 10 desc, 9 desc">
</asp:SqlDataSource>

</asp:Content>

