<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equipamentos.aspx.cs" Inherits="Equipamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
        <br />

    <fieldset>
        <legend>Cadastrar Novo Equipamento</legend>
        <table>
            <tr>
                <th>Cliente</th>
                <th>Série</th>
            </tr>
            <tr>
                <td><asp:DropDownList ID="dpClientes" runat="server" Width="250px" DataSourceID="dsClientes" DataTextField="RazaoSocial" DataValueField="idCliente"></asp:DropDownList></td>
                <td><asp:TextBox ID="tbSerie" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID="btInserir" runat="server" Text="Inserir" OnClick="btInserir_Click" />
    </fieldset>

    <p></p>

    <asp:GridView ID="gvEquipamentos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsEquipamentos" ForeColor="Black" GridLines="Vertical" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="IdEquipamento" HeaderText="IdEquipamento" SortExpression="IdEquipamento" />
            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" SortExpression="IdCliente" />
            <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
            <asp:BoundField DataField="DtCadastro" HeaderText="DtCadastro" SortExpression="DtCadastro" />
            <asp:BoundField DataField="DtAtualizacao" HeaderText="DtAtualizacao" SortExpression="DtAtualizacao" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="Operador" HeaderText="Operador" SortExpression="Operador" />
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
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
    <asp:ObjectDataSource ID="dsEquipamentos" runat="server" SelectMethod="Listar" TypeName="Controls.Equipamento"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsClientes" runat="server" SelectMethod="Listar" TypeName="Controls.Cliente"></asp:ObjectDataSource>
</asp:Content>

