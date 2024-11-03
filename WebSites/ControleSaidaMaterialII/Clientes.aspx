<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />

    <fieldset>
        <legend>Cadastrar Novo Cliente</legend>
        <asp:Table runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>ID Ilux</asp:TableHeaderCell>
                <asp:TableHeaderCell>Razão Social</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell><asp:TextBox ID="tbidIlux" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell><asp:TextBox ID="tbRazaoSocial" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="btInserir" runat="server" Text="Inserir" OnClick="btInserir_Click" />
    </fieldset>

    <p></p>

    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsClientes" ForeColor="Black" GridLines="Vertical" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" SortExpression="IdCliente" />
            <asp:BoundField DataField="IdClienteIlux" HeaderText="IdClienteIlux" SortExpression="IdClienteIlux" />
            <asp:BoundField DataField="RazaoSocial" HeaderText="RazaoSocial" SortExpression="RazaoSocial" />
            <asp:BoundField DataField="DtCadastro" HeaderText="DtCadastro" SortExpression="DtCadastro" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
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
    <asp:ObjectDataSource ID="dsClientes" runat="server" SelectMethod="Listar" TypeName="Controls.Cliente"></asp:ObjectDataSource>
</asp:Content>

