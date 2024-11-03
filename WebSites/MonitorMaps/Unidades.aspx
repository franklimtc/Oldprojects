<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unidades.aspx.cs" Inherits="Unidades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Lista de Unidades Cadastradas</h1>
        <fieldset style="max-width:300px">
            <legend>Cadastrar Nova Unidade</legend>
            <table>
                <tr>
                    <th>Descrição</th>
                    <th>Nome</th>
                    <th>Latitude</th>
                    <th>Longitude</th>
                </tr>
                <tr>
                    <td><asp:TextBox ID="tbdescricao" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="tbNome" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="tbLat" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="tbLng" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <br />
            <asp:Button ID="tbInserir" runat="server" Text="Inserir" OnClick="tbInserir_Click" />
            <asp:Button ID="tbMapa" runat="server" Text="Ver Mapa" OnClick="tbMapa_Click" />
        </fieldset>
        <br />
        <asp:GridView ID="gvUnidades" runat="server" DataSourceID="dbMaps" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <asp:SqlDataSource ID="dbMaps" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MapsConnection %>" 
            SelectCommand="SELECT * FROM [markers]" DeleteCommand="DELETE FROM [markers] WHERE [idmarker] = @idmarker" InsertCommand="INSERT INTO [markers] ([descricao], [nomeSimples], [lat], [lng], [data]) VALUES (@descricao, @nomeSimples, @lat, @lng, @data)" UpdateCommand="UPDATE [markers] SET [descricao] = @descricao, [nomeSimples] = @nomeSimples, [lat] = @lat, [lng] = @lng, [data] = @data WHERE [idmarker] = @idmarker">
            <DeleteParameters>
                <asp:Parameter Name="idmarker" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="descricao" Type="String" />
                <asp:Parameter Name="nomeSimples" Type="String" />
                <asp:Parameter Name="lat" Type="String" />
                <asp:Parameter Name="lng" Type="String" />
                <asp:Parameter Name="data" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="descricao" Type="String" />
                <asp:Parameter Name="nomeSimples" Type="String" />
                <asp:Parameter Name="lat" Type="String" />
                <asp:Parameter Name="lng" Type="String" />
                <asp:Parameter Name="data" Type="DateTime" />
                <asp:Parameter Name="idmarker" Type="Int32" />
            </UpdateParameters>
            
        </asp:SqlDataSource>
        
    </div>
    </form>
</body>
</html>
