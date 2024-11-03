<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Novarequisicao.aspx.cs" Inherits="Requisicoes_Novarequisicao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Nova Requisição</h2>
    <table>
        <tr>
            <td>Requisição:</td>
            <td><asp:TextBox ID="tbcodReqFinal" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
            <td>Equipamento:</td>
            <td><asp:DropDownList ID="dpEqpto" runat="server" Width="200px" DataSourceID="dsEqptosUsuario" DataTextField="Serie" DataValueField="IdEquipamento"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Categoria:</td>
            <td colspan="3">
                <asp:DropDownList ID="dpCategoria" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="dpCategoria_SelectedIndexChanged">
                    <asp:ListItem>Atendimento Técnico</asp:ListItem>
                    <asp:ListItem>Solicitação de suprimentos</asp:ListItem>
                    <asp:ListItem>Outras solicitações</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
           
            <td>Contador:</td>
            <td><asp:TextBox ID="tbContador" runat="server" Width="100px"></asp:TextBox>
                                
            </td>
            <td>% Suprimento:</td>
            <td><asp:TextBox ID="tbSuprimento" runat="server" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                    <asp:RangeValidator ID="rvtbContador" runat="server" ControlToValidate="tbContador" MinimumValue="0" MaximumValue="2000000" Type="Integer" ErrorMessage="* Contador de equipamento inválido!"></asp:RangeValidator>
            </td>
            <td colspan="2">
                    <asp:RangeValidator ID="rvtbSuprimento" runat="server" ControlToValidate="tbSuprimento" MinimumValue="0" MaximumValue="100"   Enable="false" Type="Integer" ErrorMessage="* Valor de suprimento inválido!"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td>Resumo:</td>
            <td colspan="3"><asp:TextBox ID="tbResumo" runat="server" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Descrição:</td>
            <td colspan="3"><asp:TextBox ID="tbDescricao" runat="server" TextMode="MultiLine" Width="100%" Height="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btSalvar" runat="server" Text="Salvar" Width="75px" OnClick="btSalvar_Click" />
                <asp:Button ID="btCancelar" runat="server" Text="Cancelar" Width="75px" OnClick="btCancelar_Click" CausesValidation="false" />
                <asp:Button ID="btVoltar" runat="server" Text="Voltar" Width="75px" OnClick="btVoltar_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:RequiredFieldValidator ID="rfResumo" runat="server" ControlToValidate="tbResumo" ErrorMessage="*Favor adicionar o resumo da requisição!"></asp:RequiredFieldValidator><br />
    <asp:RequiredFieldValidator ID="rfDescricao" runat="server" ControlToValidate="tbDescricao" ErrorMessage="**Favor descrever o problema!"></asp:RequiredFieldValidator><br />
    <asp:RequiredFieldValidator ID="rftbContador" runat="server" ControlToValidate="tbContador" ErrorMessage="***Favor informar o contador do equipamento!"></asp:RequiredFieldValidator><br />
    <asp:RequiredFieldValidator ID="rftbSuprimento" Enabled ="false" runat="server" ControlToValidate="tbSuprimento" ErrorMessage="****Favor informar o percentual do suprimento!"></asp:RequiredFieldValidator>
    <br />
    
    <asp:TextBox ID="tbCodReq" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="tbUserName" runat="server" Visible="false"></asp:TextBox>

    <asp:ObjectDataSource ID="dsEqptosUsuario" runat="server" SelectMethod="ListarporUsuario" TypeName="Equipamento">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbUserName" Name="UserId" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

