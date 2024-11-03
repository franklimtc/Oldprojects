<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EnviosClientes.aspx.cs" Inherits="Relatórios_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    Cliente:
    <br />
    <asp:DropDownList ID="dpClientes" runat="server" AutoPostBack="True" DataSourceID="dsClientes" DataTextField="cliente" DataValueField="cliente"></asp:DropDownList>
    Período:
    <br />
    <table>
        <tr>
            <th>Data Inicial</th>
            <th>Data Final</th>
        </tr>
        <tr>
            <td><asp:TextBox ID="tbDataInicial" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="tbDataFinal" runat="server"></asp:TextBox></td>
            <td><asp:Button id="btEmitir" runat="server" Text="Emitir" OnClick="btEmitir_Click" style="height: 26px" /></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvDados" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsDadosEnvios" Width="100%" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="serie" HeaderText="serie" ReadOnly="True" SortExpression="serie" />
            <asp:BoundField DataField="dtEnvio" HeaderText="dtEnvio" SortExpression="dtEnvio" />
            <asp:BoundField DataField="qtd" HeaderText="qtd" ReadOnly="True" SortExpression="qtd" />
            <asp:BoundField DataField="tpSuprimento" HeaderText="tpSuprimento" ReadOnly="True" SortExpression="tpSuprimento" />
            <asp:BoundField DataField="tpEnvio" HeaderText="tpEnvio" SortExpression="tpEnvio" />
            <asp:BoundField DataField="origem" HeaderText="origem" ReadOnly="True" SortExpression="origem" />
            <asp:BoundField DataField="postagem" HeaderText="postagem" ReadOnly="True" SortExpression="postagem" />
            <asp:BoundField DataField="etiqueta" HeaderText="etiqueta" SortExpression="etiqueta" />
            <asp:BoundField DataField="partNumber" HeaderText="partNumber" ReadOnly="True" SortExpression="partNumber" />
            <asp:BoundField DataField="Interno" HeaderText="Interno" ReadOnly="True" SortExpression="Interno" />
            <asp:BoundField DataField="cliente" HeaderText="cliente" SortExpression="cliente" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="dsDadosEnvios" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT * FROM [vw_listaEnvios] WHERE (([dtEnvio] &gt;= @dtEnvio) AND ([dtEnvio] &lt;= @dtEnvio2) AND ([cliente] = @cliente))">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbDataInicial" Name="dtEnvio" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="tbDataFinal" Name="dtEnvio2" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="dpClientes" Name="cliente" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select idCliente, cliente from clientes"></asp:SqlDataSource>
</asp:Content>

