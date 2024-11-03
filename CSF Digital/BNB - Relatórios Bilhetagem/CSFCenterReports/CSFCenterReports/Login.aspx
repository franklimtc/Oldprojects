<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CSFCenterReports.Login" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="ColCenter">
    <telerik:RadWindow Modal="true" AutoSize="True" ID="rWindow" runat="server" Animation="None" 
            Behaviors="Minimize, Close" VisibleStatusbar="False" 
            VisibleTitlebar="False" EnableShadow="True">
        <ContentTemplate>
            <div class="ColCenter">
                <table style="width: 300px; height:200px;">
                    <tr>
                        <td align="right">
                            <asp:Image ID="imgLogin" runat="server" ImageUrl="~/img/loginMember.png" Width="20px" Height="20px"/>
                        </td>
                        <td align="left"
                            <asp:Label ID="lbLogin" runat="server" Text="Login"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtxtLogin" runat="server" MaxLength="10" Wrap="False" style="text-transform: uppercase;" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Image ID="imgSenha" runat="server" ImageUrl="~/img/1392_128x128.png" Width="20px" Height="20px"/>
                        </td>
                        <td align="left">
                            <asp:Label ID="lbSenha" runat="server" Text="Senha"></asp:Label></td>
                        <td>
                            <telerik:RadTextBox ID="rtxtSenha" runat="server" MaxLength="50" Wrap="False" TextMode="Password"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/logo_web.png" Width="20px" Height="20px"/>
                        </td>
                        <td align="left">
                            <asp:Label ID="lbDominio" runat="server" Text="Dominío"></asp:Label></td>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbDominio" runat="server" Width="100px">
                            </telerik:RadComboBox>    
                        </td>
                    </tr>                    
                    <tr>
                        <td colspan="2" align="center">
                            <br />
                            <telerik:RadButton ID="rbtnEntrar" runat="server" Text="Entrar" onclick="rbtnEntrar_Click">
                            </telerik:RadButton>
                        </td>
                        <td align="center">
                            <br />
                            <telerik:RadButton ID="rbtnSair" runat="server" Text="Sair" onclick="rbtnSair_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lbMensagem" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate> 
        <Localization Cancel="Cancelar" Close="Fechar" Maximize="Maximizar" Minimize="Minimizar" No="Não" Reload="Recarregar" Restore="Restaurar" />
        </telerik:RadWindow> 
    </div>
</asp:Content>
