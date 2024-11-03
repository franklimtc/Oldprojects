<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Saidas.aspx.cs" Inherits="Saidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <table>
        <tr>
            <th>Cliente:</th>
            <th><asp:DropDownList ID="dpClientes" runat="server" DataSourceID="dsClientes" DataTextField="RazaoSocial" DataValueField="idCliente" AutoPostBack="True"></asp:DropDownList></th>
            <th>Equipamento:</th>
            <th><asp:DropDownList ID="dpEquipamentos" runat="server" DataSourceID="dsEquipamentos" DataTextField="Serie" DataValueField="IdEquipamento" TabIndex="1"></asp:DropDownList></th>

        </tr>
        <tr>
            <th>Operação:</th>
            <th>
                <asp:DropDownList ID="dpOperacao" runat="server" TabIndex="2">
                <asp:ListItem Text="Locação" Value="Locação"></asp:ListItem>
                <asp:ListItem Text="Venda" Value="Venda"></asp:ListItem>
                </asp:DropDownList>
            </th>
            <th>Solicitante:</th> 
            <th><asp:DropDownList ID="dpSolicitante" runat="server" DataSourceID="dsSolicitantes" DataTextField="nome" DataValueField="idSolicitante" TabIndex="3"></asp:DropDownList></th>

        </tr>
        <tr>
            <th>Part Number:</th>
            <th><asp:TextBox ID="tbPartNumber" runat="server" AutoPostBack="true" OnTextChanged="tbPartNumber_TextChanged" TabIndex="4"></asp:TextBox></th>
            <th>Descrição:</th>
            <th><asp:TextBox ID="tbDescricao" runat="server" Enabled="false" TabIndex="5"></asp:TextBox></th>
        </tr>
        <tr>
            <th>Nota Fiscal:</th>
            <th><asp:TextBox ID="tbNotaFiscal" runat="server" TabIndex="6"></asp:TextBox></th>
            <th>Qtd:</th>
            <th><asp:TextBox ID="tbQtd" runat="server" TabIndex="7"></asp:TextBox></th>
        </tr>
        <tr>
            <td><asp:Button ID="btInserir" runat="server" Text ="Inserir" OnClick="btInserir_Click" /></td>
        </tr>
    </table>
    <p></p>

    <h3>Ítens Pendentes</h3>
    <asp:GridView ID="gvPendentes" runat="server" DataSourceID ="dsPendenctes" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="100%" OnRowCommand="gvPendentes_RowCommand">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="btconcluir"
                        Text="Concluir" CommandName="Concluir" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="idSaida" HeaderText="ID" SortExpression="idSaida">
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente">
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Série" HeaderText="Série" SortExpression="Série" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="partNumber" HeaderText="PartNumber" SortExpression="partNumber" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="descricao" HeaderText="Descrição" SortExpression="descricao" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Qtd" HeaderText="Qtd" SortExpression="Qtd" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Operador" HeaderText="Operador" SortExpression="Operador" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" SortExpression="Data" >
            <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <asp:ObjectDataSource ID="dsClientes" runat="server" SelectMethod="Listar" TypeName="Controls.Cliente"></asp:ObjectDataSource>
    <asp:SqlDataSource ID="dsEquipamentos" runat="server" ConnectionString="<%$ ConnectionStrings:controleSaidaMaterial %>" SelectCommand="SELECT [idEquipamento], [serie] FROM [Equipamentos] WHERE (([status] = @status) AND ([idCliente] = @idCliente))">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="status" Type="Boolean" />
            <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsSolicitantes" runat="server" ConnectionString="<%$ ConnectionStrings:controleSaidaMaterial %>" SelectCommand="select idSolicitante, nome from Solicitantes WHERE STATUS = 1">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsPendenctes" runat="server" ConnectionString="<%$ ConnectionStrings:controleSaidaMaterial %>" SelectCommand="SELECT * FROM vw_solicitacoesPendentesIlux">
    </asp:SqlDataSource>
</asp:Content>

