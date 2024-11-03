<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Monitor.aspx.cs" Inherits="Atendimentos_Monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="text-align:center">
        <h1>Solicitações de Atendimento Técnico</h1>
        <h2>Monitoramento</h2>
    </div>
    
    <br /><br />

    <asp:GridView ID="gvMonitor" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idReqAtendimento" DataSourceID="dsMonitor" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvMonitor_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="cliente" HeaderText="cliente" SortExpression="cliente" />
            <asp:BoundField DataField="idReqAtendimento" HeaderText="idReqAtendimento" SortExpression="idReqAtendimento" ReadOnly="True" />
            <asp:BoundField DataField="req" HeaderText="req" SortExpression="req" />
            <asp:BoundField DataField="localizacao" HeaderText="localizacao" SortExpression="localizacao" >
            </asp:BoundField>
            <asp:BoundField DataField="Contato" HeaderText="Contato" SortExpression="Contato" ReadOnly="True" />
            <asp:BoundField DataField="telefone" HeaderText="telefone" SortExpression="telefone" />
            <asp:BoundField DataField="Operador" HeaderText="Operador" SortExpression="Operador" ReadOnly="True" />
            <asp:BoundField DataField="Técnico" HeaderText="Técnico" SortExpression="Técnico" ReadOnly="True">
            </asp:BoundField>
            <asp:BoundField DataField="dtAbertura" HeaderText="dtAbertura" SortExpression="dtAbertura" />
            <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" ReadOnly="True" SortExpression="qtdDias" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
            <asp:BoundField DataField="Parado" HeaderText="Parado" SortExpression="Parado" ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="dtPrevisao" HeaderText="dtPrevisao" ReadOnly="True" SortExpression="dtPrevisao" />
            <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:SqlDataSource ID="dsMonitor" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select * from vwreqAtendimentos"></asp:SqlDataSource>
</asp:Content>

