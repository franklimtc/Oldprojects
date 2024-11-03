<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="CSFCenterReports.Erro" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ColCenter">
            <telerik:RadWindow Modal="true" AutoSize="true" ID="rWindow" runat="server" VisibleOnPageLoad="true" Animation="None" Behaviors="Minimize, Close, Maximize, Reload" VisibleStatusbar="False" VisibleTitlebar="False">
                <ContentTemplate>
                    <div class="ColCenter">
                        <table>                    
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lbMensagem" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="rbtRecarregar" runat="server" Text="Recarregar" OnClick="rbtnRecarregar_Click">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate> 
                <Localization Cancel="Cancelar" Close="Fechar" Maximize="Maximizar" Minimize="Minimizar" No="Não" Reload="Recarregar" Restore="Restaurar" />
            </telerik:RadWindow> 
        </div>
    </form>
</body>
</html>