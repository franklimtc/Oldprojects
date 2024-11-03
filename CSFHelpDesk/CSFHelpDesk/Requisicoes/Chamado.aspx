<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Chamado.aspx.cs" Inherits="Chamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <table>
        <tr>
            <td>Requisição:</td>
            <td><h3><asp:Label ID="lbReq" runat="server"></asp:Label></h3></td>
            <td>Status:</td>
            <td>
                <asp:TextBox ID="tbStatus" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                <asp:DropDownList ID="dpStatus" runat="server" Visible ="false" Width="100%"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Contato:</td>
            <td><asp:TextBox ID="tbContato" runat="server" Enabled="false"  Width="100%"></asp:TextBox></td>
            <td>Responsável:</td>
            <td>
                <asp:TextBox ID="tbResponsavel" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                <asp:DropDownList ID="dpResponsavel" runat="server" Width="100%" DataSourceID="dsAccounts" DataTextField="UserName" DataValueField="UserId" Visible="false" Enabled="false"></asp:DropDownList>
                <asp:ObjectDataSource ID="dsAccounts" runat="server" SelectMethod="ListarContas" TypeName="Account">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Operadores" Name="RoleName" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>Cliente:</td>
            <td><asp:TextBox ID="tbCliente" runat="server" Enabled="false" Width="100%"></asp:TextBox></td>
            <td>Equipamento:</td>
            <td><asp:TextBox ID="tbEquipamento" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Contador:</td>
            <td><asp:TextBox ID="tbContador" runat="server" Enabled="false"></asp:TextBox></td>
            <td>Suprimento:</td>
            <td><asp:TextBox ID="tbSuprimento" runat="server" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Categoria:</td>
            <td colspan="3"><asp:TextBox ID="tbCategoria" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Resumo:</td>
            <td colspan="3"><asp:TextBox ID="tbResumo" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr><td>Descrição:</td></tr>
        <tr><td colspan="4"><asp:TextBox ID="tbDescricao" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Enabled="false"></asp:TextBox></td></tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnBotoes" runat="server" Visible="true">
                    <asp:Button ID="btAtender" runat="server" Text="Atender" Visible="false" OnClick="btAtender_Click" />
                    <asp:Button ID="btAtualizarStatus" runat="server" Text="Atualizar Status" Visible="false" OnClick="btAtualizarStatus_Click"/>
                    <asp:Button ID="btComentario" runat="server" Text="Adicionar Comentário" Visible="false" OnClick="btComentario_Click"/>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnComentarioStatus" runat="server" Visible="false">
                    <h3>Comentário:</h3>
                    <asp:TextBox ID="tbComentarioStatus" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox><br />
                    <asp:Button ID="btSalvarcomentarioStatus" runat="server" Text="Salvar" Width="75px" OnClick="btSalvarcomentarioStatus_Click" />
                    <asp:Button ID="btCancelar" runat="server" Text="Cancelar" Width="75px" OnClick="btCancelar_Click" />

                </asp:Panel>
            </td>
        </tr>
        <tr><td><h3>Comentários:</h3></td></tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvLogs" runat="server" AutoGenerateColumns="False" BackColor="White" Width="100%" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsLogs" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Idlog" HeaderText="Idlog" SortExpression="Idlog" Visible="false" />
                        <asp:BoundField DataField="CodReq" HeaderText="CodReq" SortExpression="CodReq" Visible="false" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                        <asp:BoundField DataField="Descricao" HeaderText="Descricao" SortExpression="Descricao" />
                        <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
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
                <asp:ObjectDataSource ID="dsLogs" runat="server" SelectMethod="Listar" TypeName="LogReq">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lbReq" Name="ReqCode" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

