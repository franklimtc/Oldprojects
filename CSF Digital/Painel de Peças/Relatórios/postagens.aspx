<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="postagens.aspx.cs" Inherits="postagens" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Peças em Trânsito</h2>
    <asp:GridView runat="server" ID="gvPecas" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" DataSourceID="dsPecas" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="gvPecas_RowCommand">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="USD" HeaderText="USD" SortExpression="USD" />
            <asp:BoundField DataField="Operador" HeaderText="Operador" SortExpression="Operador" ReadOnly="True" />
            <asp:BoundField DataField="Postagem" HeaderText="Postagem" SortExpression="Postagem" ReadOnly="True" />
            <asp:BoundField DataField="Série" HeaderText="Série" ReadOnly="True" SortExpression="Série" />
            <asp:BoundField DataField="UF" HeaderText="UF" SortExpression="UF" />
            <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" SortExpression="Data" />
            <asp:BoundField DataField="Dias" HeaderText="Dias" ReadOnly="True" SortExpression="Dias" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btConfirmar"
                        Text="Confirmar" CommandName="Confirmar" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
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
    <asp:SqlDataSource ID="dsPecas" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT *, datediff(day,data,getdate()) 'Dias' FROM [vw_postagensPecas] ORDER BY [data]"></asp:SqlDataSource>
    <div>
        <h2>Suprimentos em Trânsito</h2>
        <asp:GridView runat="server" ID="gvSuprimentos" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" DataSourceID="dsSuprimentos" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="gvSuprimentos_RowCommand">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="USD" HeaderText="USD" SortExpression="USD" />
                <asp:BoundField DataField="Operador" HeaderText="Operador" SortExpression="Operador" />
                <asp:BoundField DataField="Postagem" HeaderText="Postagem" SortExpression="Postagem" ReadOnly="True" />
                <asp:BoundField DataField="Série" HeaderText="Série" SortExpression="Série" ReadOnly="True" />
                <asp:BoundField DataField="UF" HeaderText="UF" SortExpression="UF" />
                <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" DataFormatString="{0:d}" />
                <asp:BoundField DataField="prazoEntrega" HeaderText="Prazo" ReadOnly="True" SortExpression="prazoEntrega" />
                <asp:BoundField DataField="Dias" HeaderText="Dias" ReadOnly="True" SortExpression="Dias" />
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btConfirmarSupr"
                        Text="Confirmar" CommandName="Confirmar" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
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
        <asp:SqlDataSource ID="dsSuprimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT * , datediff(day,data,getdate()) 'Dias' FROM [vw_postagensSuprimentos] ORDER BY [data]"></asp:SqlDataSource>
    </div>
</asp:Content>

