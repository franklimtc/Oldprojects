<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Gerenciar.aspx.cs" Inherits="Account_Gerenciar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Associar usuário à grupo</h2>
    <table>
        <tr>
            <td><asp:DropDownList ID="dpUser" runat="server" DataSourceID="dsUsers" DataTextField="UserName" DataValueField="UserId"></asp:DropDownList></td>
            <td><asp:DropDownList ID="dpRole" runat="server" DataSourceID="dsRoles" DataTextField="RoleName" DataValueField="RoleId"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:Button ID="btAssociar" Text ="Associar" runat="server" OnClick="btAssociar_Click" /></td>
        </tr>
    </table>
            
    <br />
    <asp:GridView ID="gvUsuarios" runat="server" DataSourceID="dsUsersinRoles" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="361px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Usuário" HeaderText="Usuário" SortExpression="Usuário" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Grupo" HeaderText="Grupo" SortExpression="Grupo" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
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

    <asp:SqlDataSource ID="dsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="Select * from dbo.Users where UserId not in (SELECT UserId FROM UsersInRoles)"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsRoles" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select RoleId, RoleName from dbo.Roles"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsUsersinRoles" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select b.UserName 'Usuário', c.RoleName 'Grupo' from UsersInRoles as a left join Users as b on a.UserId= b.UserId left join Roles as c on a.RoleId = c.RoleId"></asp:SqlDataSource>

</asp:Content>

