<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Envios_II.aspx.cs" Inherits="Suprimentos_Envios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
     <fieldset style="width:200px">
        <legend>Filial</legend>
        <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Text="Fortaleza" Value="FOR" Selected="True"></asp:ListItem>
            <asp:ListItem Text="São Luís" Value="SLZ"></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
<fieldset>
    <legend>Envios de Suprimentos Diários</legend>
    <table width="100%">
        <tr style="text-align:center">
            <td>Tipo de Envio</td>
            <td>Requisição</td>
            <td>Série</td>
            <td>PartNumber</td>
            <td>Etiqueta</td>
            <td>Postagem</td>
            <td>Código AR</td>
            <td>Valor</td>
        </tr>
        <tr style="text-align:center">
            <td><asp:DropDownList ID="dptpEnvio" runat="server" Width="100%" onselectedindexchanged="dptpEnvio_SelectedIndexChanged"  TabIndex="1" AutoPostBack="True">
                <asp:ListItem Selected="True">PAC</asp:ListItem>
                <asp:ListItem>SEDEX</asp:ListItem>
                <asp:ListItem>MB-Rapidez</asp:ListItem>
                
                <asp:ListItem>OUTRO</asp:ListItem>
            </asp:DropDownList></td>
            <td><asp:TextBox ID="tbReqOcomon" runat="server" width="100%" TabIndex="2"></asp:TextBox></td>
            <td><asp:TextBox ID="tbSerie" runat="server" width="100%" TabIndex="3"></asp:TextBox></td>
            <td><asp:TextBox ID="tbPartNumber" runat="server" width="100%" TabIndex="4"></asp:TextBox></td>
            <td><asp:TextBox ID="tbEtiqueta" runat="server" width="100%" TabIndex="5"></asp:TextBox></td>
            <td><asp:TextBox ID="tbPostagem" runat="server" width="100%" TabIndex="6"></asp:TextBox></td>
            <td><asp:TextBox ID="tbAR" runat="server" width="100%" TabIndex="7"></asp:TextBox></td>
            <td><asp:TextBox ID="tbValor" runat="server" width="100%" TabIndex="8"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:RequiredFieldValidator ID="revReq" runat="server" ErrorMessage="** Informar número da requisição!" ControlToValidate="tbReqOcomon" ForeColor="Red"></asp:RequiredFieldValidator>
                <br /><asp:RequiredFieldValidator ID="revSerie" runat="server" ErrorMessage="* Informar número de série do equipamento!" ControlToValidate="tbSerie" ForeColor="Red"></asp:RequiredFieldValidator>
                <br /><asp:CustomValidator ID="chamadoValidator" runat="server" ErrorMessage="Requisição não encontrada!" ControlToValidate="tbReqOcomon" OnServerValidate="ChamadoValidate" ></asp:CustomValidator>
                <br /><asp:CustomValidator ID="partNumberValidator" runat="server" ErrorMessage="Part Number não identificado!" ControlToValidate="tbPartNumber" OnServerValidate="PartNumberValidate" ></asp:CustomValidator>
            </td>
        </tr>
        <tr style="text-align:center">
            <td colspan="3"><asp:Button ID="btInserir" runat="server" Text="Inserir" Width="80px" TabIndex="9"
                    onclick="btInserir_Click" /></td>
            <td colspan="3"><asp:Button ID="btLimpar" runat="server" Text="Limpar"  Width="80px"
                    onclick="btLimpar_Click" /></td>
        </tr>
    </table>
</fieldset>
<asp:GridView ID="gvEnvios" runat="server" DataSourceID="dbEnvios" Width="100%" 
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idEnvio" 
        ForeColor="#333333" GridLines="None" OnRowCommand="gvEnvios_RowCommand">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="idEnvio" HeaderText="idEnvio" InsertVisible="False" 
            ReadOnly="True" SortExpression="idEnvio" />
        <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
        <asp:BoundField DataField="partNumber" HeaderText="partNumber" 
            SortExpression="partNumber" />
        <asp:BoundField DataField="etiqueta" HeaderText="etiqueta" 
            SortExpression="etiqueta" />
        <asp:BoundField DataField="tpEnvio" HeaderText="tpEnvio" 
            SortExpression="tpEnvio" />
        <asp:BoundField DataField="postagem" HeaderText="postagem" 
            SortExpression="postagem" />
        <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" />
        <asp:BoundField DataField="usuario" HeaderText="usuario" 
            SortExpression="usuario" />
        <asp:BoundField DataField="dtEnvio" DataFormatString="{0:d}" 
            HeaderText="dtEnvio" SortExpression="dtEnvio" />
        <asp:BoundField DataField="filial" HeaderText="Filial" 
            SortExpression="filial" />
         <asp:TemplateField>
             <ItemTemplate>
                    <asp:Button runat="server" ID="btExcluir" 
                        Text="Excluir" CommandName="Excluir" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
            </ItemTemplate>
         </asp:TemplateField>
                
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
 <asp:CustomValidator ID="CustomValidator1" runat="server" 
        ErrorMessage="CustomValidator" 
        onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>

<asp:SqlDataSource ID="dbEnvios" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select* from enviosSuprimentos where convert(char(10),dtEnvio ,103) = convert(char(10),getdate(),103)  order by idEnvio DESC" >
        </asp:SqlDataSource>
</asp:Content>

