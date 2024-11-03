<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Atendimentos_new_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="gvAtendimentos" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idReqAtendimento" DataSourceID="dsAtendimentos" ForeColor="#333333" GridLines="None" OnRowCommand="gvAtendimentos_RowCommand" AllowSorting="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="cliente" HeaderText="Cliente" SortExpression="cliente" />
            <asp:BoundField DataField="idReqAtendimento" HeaderText="ID" ReadOnly="True" SortExpression="idReqAtendimento" />
            <asp:BoundField DataField="req" HeaderText="USD" SortExpression="req">
            <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="serie" HeaderText="Série" />
            <asp:BoundField DataField="localizacao" HeaderText="Local" SortExpression="localizacao" />
            <asp:BoundField DataField="telefone" HeaderText="Fone" SortExpression="telefone" />
            <asp:BoundField DataField="Operador" HeaderText="Operador" ReadOnly="True" SortExpression="Operador" />
            <asp:BoundField DataField="dtAbertura" DataFormatString="{0:d}" HeaderText="Data" SortExpression="dtAbertura" />
            <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" ReadOnly="True" SortExpression="qtdDias">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Parado" HeaderText="Parado" ReadOnly="True" SortExpression="Parado" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btAgendar"
                        Text="A" CommandName="agendar" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btRetorno"
                        Text="R" CommandName="retorno" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btEditar" 
                        Text="E" CommandName="editar" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btConcluir" 
                        Text="C" CommandName="concluir" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="dtPrevisao" DataFormatString="{0:d}" HeaderText="Previsão" />
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
    <asp:SqlDataSource ID="dsAtendimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT * FROM [vwReqAtendimentos] order by  qtdDias DESC"></asp:SqlDataSource>
</asp:Content>

