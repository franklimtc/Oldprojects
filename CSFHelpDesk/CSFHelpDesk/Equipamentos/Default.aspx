<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Equipamentos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Equipamentos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="pnEquipamentos" runat="server">
        <table>
            <tr>
                <td>
                    <p><asp:Label ID="lbAssociar" runat="server" Visible ="false">Após selecionar os equipamentos desejados, clique aqui no botão abaixo para associá-los ao usuário e poder abrir requisições.</asp:Label></p>
                    <asp:Button ID="btAssociarEquipamentos" runat="server" Text="Associar Equipamentos Selecionados" Width="500px" Visible="false" OnClick="btAssociarEquipamentos_Click" />
                    <p><asp:Label ID="lbVerEqptos" runat="server" Visible="false">Clique no botão abaixo caso queira associar outros equipamentos!</asp:Label></p>
                    <asp:Button ID="btVerEqptos" runat="server" Text="Ver todos os equipamentos" Width="500px" Visible="false" OnClick="btVerEqptos_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:GridView ID="gvEquipamentos" runat="server" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="objEquipamentos" ForeColor="Black">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Selecionar">
                <EditItemTemplate>
                    <asp:CheckBox ID="chSelect" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IdEquipamento" HeaderText="ID" SortExpression="IdEquipamento" />
            <asp:BoundField DataField="Uf" HeaderText="UF" SortExpression="Uf" />
            <asp:BoundField DataField="Municipio" HeaderText="Município" SortExpression="Municipio" />
            <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
            <asp:BoundField DataField="Contato" HeaderText="Contato" SortExpression="Contato" />
            <asp:BoundField DataField="Serie" HeaderText="Série" SortExpression="Serie" />
            <asp:BoundField DataField="Nf" HeaderText="NF" SortExpression="Nf" />
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
    
    <asp:ObjectDataSource ID="objEquipamentos" runat="server" SelectMethod="Listar" TypeName="Equipamento"></asp:ObjectDataSource>
</asp:Content>

