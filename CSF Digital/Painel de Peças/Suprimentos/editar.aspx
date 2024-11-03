<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="editar.aspx.cs" Inherits="Suprimentos_editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Editar Solicitação de Suprimento</h2>
    <asp:Table ID="Table1" runat="server" Width="98%">
        <asp:TableRow>
            <asp:TableCell>ID</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbID" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Solicitante</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbSolicitante" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>UF</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbUF" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Cidade</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbCidade" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Série</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbSerie" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Suprimento</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbSuprimento" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>% Atual</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbSuprAtual" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Dur. Estimada:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbDurEstimada" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Contador:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbContador" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Falha anterior:</asp:TableCell>
            <asp:TableCell><asp:CheckBox ID="chFalha" runat="server" /></asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell>Usuário:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbUsuario" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Email:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbEmail" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>Telefone:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbTelefone" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Endereço:</asp:TableCell>
            <asp:TableCell ColumnSpan="5"><asp:TextBox ID="tbEndereco" runat="server" Width="98%"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Bairro:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbBairro" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>CEP:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbCep" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell>USD:</asp:TableCell>
            <asp:TableCell><asp:TextBox ID="tbUSD" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>OBS:</asp:TableCell>
            <asp:TableCell ColumnSpan="5"><asp:TextBox ID="tbOBS" runat="server" Width="98%"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Button ID="btAtualizar" runat="server"  Text="Atualizar" OnClick="btAtualizar_Click"/>
    &nbsp;
    <asp:Button ID="btCancelar" runat="server"  Text="Cancelar"/>

</asp:Content>

