<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="AjudaIE8.aspx.cs" Inherits="CSFCenterReports.AjudaIE8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div>
        <table >
            <tr valign="top">
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="1 -> Clicar na opção 'Ferramentas' e logo após em 'Configurações do Modo de Exibição de Compatibilidade'."></asp:Label>
                    <br />
                    <br />
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="2 -> Desmarcar a opção 'Exibir sites da intranet no Modo de Exibição de Compatibilidade' e clicar em 'Fechar'."></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr valign="top">
                <td align=center>
                    <img src="img/Tela01.jpg" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td align=center>
                    <img src="img/Tela02.jpg" />
                    <br />
                    <br />
                    <asp:HyperLink ID="PageInternetExplorer8" runat="server" NavigateUrl="~/Principal.aspx">Voltar para página de relatórios.</asp:HyperLink>
                </td>
            </tr>            
        </table>   
    </div>
</asp:Content>