<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="Cadastros_Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Grupos de Usuários</h2>
<p></p>
    <div>
   <table align="center">
    <tr>
        <td>Nome: <asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
        <td>Descrição: <asp:TextBox ID="tbDescricao" runat='server' Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btAdicionar" runat="server" Text="Adicionar" 
                onclick="btAdicionar_Click" /></td>
    </tr>
   </table>
</div>
<p></p>
<div>
    <asp:GridView ID="gvRoles" runat="server" DataSourceID="dtRoles"  Width="100%"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="RoleName" HeaderText="Nome" 
                SortExpression="RoleName" />
            <asp:BoundField DataField="Description" HeaderText="Descrição" 
                SortExpression="Description" />
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
</div>

<p></p>
<h2>Associar Usuário à Grupos</h2>
<p></p>
<table>
    <tr>
        <td>
        <fieldset>
        <legend>Usuários</legend>
            <asp:ListBox ID="lbUsuarios" runat="server" DataSourceID="dtUsuarios"  Width="200px"
                DataTextField="UserName" DataValueField="UserId" AutoPostBack="True" 
                onselectedindexchanged="lbUsuarios_SelectedIndexChanged"></asp:ListBox>
        </fieldset>
        </td>
        <td>
        <fieldset>
        <legend>Grupos</legend>
            <asp:CheckBoxList id="cklRoles" runat='server' Width="200px" 
                DataSourceID="dtUsersinRoles" DataTextField="RoleName" DataValueField="Value" 
                ondatabound="cklRoles_DataBound"></asp:CheckBoxList>
        </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button runat="server" ID="btAtualizar" Text="Atualizar" 
                Width="80px" onclick="btAtualizar_Click" /></td>
    </tr>
</table>


    <asp:SqlDataSource ID="dtRoles" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT [RoleName], [Description] FROM [vw_aspnet_Roles]">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dtUsuarios" runat="server" >
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="dtUsersinRoles" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="select a.RoleId, a.RoleName, case 
                        WHEN COUNT(b.UserId) > 0 THEN CAST(a.RoleId AS VARCHAR(MAX)) + ' ' + 'True'
                        ELSE CAST(a.RoleId AS VARCHAR(MAX)) + ' ' + 'False'
                        end as Value from aspnet_Roles as a
                        left join (select * from aspnet_UsersInRoles where UserId=@UserId
                        ) as b on a.RoleId=b.RoleId group by a.RoleId, a.RoleName">
            <SelectParameters>
                <asp:ControlParameter ControlID="lbUsuarios" Name="UserId" 
                    PropertyName="SelectedValue" />
            </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


