<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Monitor.aspx.cs" Inherits="Monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Monitor de Requisições</h2>
    <span>Atualizado em: <%: DateTime.Now %></span>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000">
    </asp:Timer>

    <asp:GridView ID="gvRequisicoes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" Width="100%" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsRequisicoes" ForeColor="Black" GridLines="Vertical" OnRowCommand="gvRequisicoes_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="true" SelectText="Abrir" />
            <asp:BoundField DataField="CodReq" HeaderText="Requisição" SortExpression="CodReq" />
            <asp:BoundField DataField="Idrequisicao" HeaderText="ID" SortExpression="Idrequisicao" Visible="false" />
            <asp:BoundField DataField="Serie" HeaderText="Série" SortExpression="Serie" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categoria" />
            <asp:BoundField DataField="Resumo" HeaderText="Resumo" SortExpression="Resumo" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" Visible="false" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="DtAbertura" HeaderText="Abertura" SortExpression="DtAbertura" DataFormatString="{0:d}" />
            <asp:BoundField DataField="DtFechamento" HeaderText="DtFechamento" SortExpression="DtFechamento" Visible="false" />
            <asp:BoundField DataField="DtModificacao" HeaderText="Modificação" SortExpression="DtModificacao" DataFormatString="{0:d}" />
            <asp:BoundField DataField="AbertorPor" HeaderText="AbertorPor" SortExpression="AbertorPor" Visible="false" />
            <asp:BoundField DataField="ModificadoPor" HeaderText="ModificadoPor" SortExpression="ModificadoPor" Visible="false" />
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
            <asp:BoundField DataField="TempoSlq" HeaderText="SLA" SortExpression="TempoSlq" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <asp:ObjectDataSource ID="dsRequisicoes" runat="server" SelectMethod="Listar" TypeName="Requisicao"></asp:ObjectDataSource>
</asp:Content>

