<%@ Page Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeFile="Relatorio.aspx.cs" Inherits="Relatorio" Title="CSF Digital" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Cabecalho" ContentPlaceHolderID="ContentPlaceHolderCabecalho" runat="server">
    <div class="menuHome">  
        <a href="Home.aspx" class="link">home</a>      	
    </div>

    <div class="menuRelatorioAtivo">  
        <a href="Relatorio.aspx" class="link">relatorio</a>      	
    </div>
    <div class="menuMonitor">  
        <a href="Monitor.aspx" class="link">monitor</a>      	
    </div>
    <div class="menuConfiguracao">  
        <a href="Configuracao.aspx" class="link">configuracao</a>      	
    </div>
    <div class="menuSuporte">  
        <a href="Suporte.aspx" class="link">suporte</a>      	
    </div>
</asp:Content>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div id="conteudoCentro" runat="server" style="margin: -7px 135px;" >
        <table class= "content" width="100%">
            <tr>
                <td class= "content" width="492px" align="center" valign="top">
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbRelatorios" runat="server" Text="Relatórios" Font-Bold="True" 
                                        Font-Names="Lucida Sans" Font-Size="Medium" Font-Underline="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbVisoes" runat="server" Text="Visões" Font-Bold="True" 
                                        Font-Names="Lucida Sans" Font-Size="Medium" Font-Underline="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:RadioButtonList ID="RadioButtonListRelatorio" runat="server"></asp:RadioButtonList>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownListRelatorioAssistencia" runat="server" Width="150px"></asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:DropDownList ID="DropDownListRelatorioAnalitico" runat="server" Width="150px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanelCampos" runat="server">
                        <ContentTemplate>
                            <div>
                                <table style="padding-top:10px;">
                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Data Inicial"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="rdpDtInicial" runat="server"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Data Final"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="rdpDtFinal" runat="server"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Nº"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txbChamado" Width="100px" MaxLength="30" Wrap="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Data Inicial - Multa"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="rdpDtInicialMulta" runat="server"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Data Final - Multa"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="rdpDtFinalMulta" runat="server"></telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        
                                    </tr>

                                </table>
                            </div>
                            <div>
                                <table style="padding-top:10px;">
                                    <tr>
                                        <td class= "content">
                                            <table>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text="Condição"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RadioButtonListTipo" runat="server" RepeatColumns="3" 
                                                                        CellPadding="3" AutoPostBack="True" 
                                                                        onselectedindexchanged="RadioButtonListTipo_SelectedIndexChanged"></asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div>
                        <table style="padding-top:10px;">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonListTipoEmissao" runat="server" 
                                        RepeatDirection="Horizontal"></asp:RadioButtonList>
                                </td>
                                <td valign="bottom">
                                    <asp:Button ID="btnAnalitico" runat="server" Text="Emitir" onclick="btnEmitir_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td class= "content" width="492px" align="center" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Rodape" ContentPlaceHolderID="ContentPlaceHolderRodape" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRodape" runat="server">
        <ContentTemplate>
            <asp:Timer ID="TimerInfoRodape" Interval="60000" runat="server" ontick="TimerInfoRodape_Tick"></asp:Timer>
            <asp:Label ID="lbInfoRodape" runat="server" Text="Espaço reservado ao cliente."></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>