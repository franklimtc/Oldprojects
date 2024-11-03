<%@ Page Title="Materiais" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Materiais.aspx.cs" Inherits="Materiais" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <fieldset>
        <legend>Cadastrar Novo Material</legend>
        <table>
            <tr>
                <th>Modelo</th>
                <th>Partnumber</th>
                <th>Descrição</th>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbModelo" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="tbPartnumber" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="tbDescricao" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID="btInserir" runat="server" Text="Inserir" OnClick="btInserir_Click"  />
    </fieldset>
    
    <asp:GridView ID="gvMateriais" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsMateriais" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="IdMaterial" HeaderText="IdMaterial" SortExpression="IdMaterial" />
            <asp:BoundField DataField="Descricao" HeaderText="Descricao" SortExpression="Descricao" />
            <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
            <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
            <asp:BoundField DataField="DtCadastro" HeaderText="DtCadastro" SortExpression="DtCadastro" />
            <asp:BoundField DataField="DtAtualizacao" HeaderText="DtAtualizacao" SortExpression="DtAtualizacao" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="Operador" HeaderText="Operador" SortExpression="Operador" />
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

    <asp:ObjectDataSource ID="dsMateriais" runat="server" SelectMethod="Listar" TypeName="Controls.Material"></asp:ObjectDataSource>

</asp:Content>

