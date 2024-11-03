<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Etiquetas.aspx.cs" Inherits="Suprimentos_Etiquetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Associar Etiquetas AOS SERIAIS (CRUM)</h2>
    <br />
    <table>
        <tr>
            <td>IP:</td>
            <td><asp:TextBox ID="tbIP" runat="server" Text="192.168.2.40" TabIndex="1"></asp:TextBox></td>
            <td>Suprimento:</td>
            <td>
                <asp:DropDownList ID="dpSuprimento" runat="server" TabIndex="2">
                <asp:ListItem>Toner</asp:ListItem>
                <asp:ListItem Selected="True">Cilindro</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>Etiqueta:</td>
            <td><asp:TextBox ID="tbEtiqueta" runat="server" Text="" TabIndex="3"></asp:TextBox>
            </td>
            
            <td>Serial:</td>
            <td><asp:TextBox ID="tbSerial" runat="server" Text="" AutoPostBack="true" OnTextChanged="tbSerial_TextChanged" TabIndex="4"></asp:TextBox></td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="rFildEtiqueta" runat="server" ControlToValidate="tbEtiqueta" ErrorMessage="Favor informar o código da etiqueta!" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btSerial" runat="server" Text="Serial" OnClick="btSerial_Click" Width="80px"/>
    &nbsp;
    <asp:Button ID="btInserir" runat="server" Text="Inserir" Enabled="false" OnClick="btInserir_Click" OnClientClick="" Width="80px"/>
    &nbsp;
    <asp:Button ID="btEmitirRelatorio" runat="server" Text="Relatorio" Enabled="true" Width="80px"/>

    <br />
    <br />

    <asp:GridView ID="gvEtiquetas" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="dsEtiquetas" CssClass="table table-striped table-bordered table-condensed table-hover">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="suprimento" HeaderText="Suprimento" SortExpression="suprimento" />
            <asp:BoundField DataField="serialSuprimento" HeaderText="Serial Suprimento" SortExpression="serialSuprimento" />
            <asp:BoundField DataField="etiqueta" HeaderText="Etiqueta" SortExpression="etiqueta" />
            <asp:BoundField DataField="operador" HeaderText="Operador" SortExpression="operador" />
            <asp:BoundField DataField="data" HeaderText="Data" SortExpression="data" DataFormatString="{0:d}" />
            <asp:BoundField DataField="OBS" HeaderText="OBS" ReadOnly="True" SortExpression="OBS" />
        </Columns>
<%--        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
    </asp:GridView>

    <asp:SqlDataSource ID="dsEtiquetas" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        DeleteCommand="DELETE FROM [etiquetasSuprimentos] WHERE [id] = @id" 
        InsertCommand="INSERT INTO [etiquetasSuprimentos] ([suprimento], [serialSuprimento], [etiqueta], [operador], [data]) VALUES (@suprimento, @serialSuprimento, @etiqueta, @operador, @data)" 
        SelectCommand="SELECT * FROM vwUltimasETiquetas ORDER BY OBS" 
        UpdateCommand="UPDATE [etiquetasSuprimentos] SET [suprimento] = @suprimento, [serialSuprimento] = @serialSuprimento, [etiqueta] = @etiqueta, [operador] = @operador, [data] = @data WHERE [id] = @id" OnDeleted="dsEtiquetas_Deleted">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="suprimento" Type="String" />
            <asp:Parameter Name="serialSuprimento" Type="String" />
            <asp:Parameter Name="etiqueta" Type="String" />
            <asp:Parameter Name="operador" Type="String" />
            <asp:Parameter Name="data" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="suprimento" Type="String" />
            <asp:Parameter Name="serialSuprimento" Type="String" />
            <asp:Parameter Name="etiqueta" Type="String" />
            <asp:Parameter Name="operador" Type="String" />
            <asp:Parameter Name="data" Type="DateTime" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>

