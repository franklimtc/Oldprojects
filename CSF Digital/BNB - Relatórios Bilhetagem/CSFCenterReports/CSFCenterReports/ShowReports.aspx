<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowReports.aspx.cs" Inherits="CSFCenterReports.ShowReports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CSF DIGITAL</title>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="overflow:auto;">
            <table style="font-family:Arial ; background-color: #D3D3D3; text-align: center; width: 100%; min-width: 1024px">
                <tr>
                    <td colspan="3" style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbRelatorioValor" runat="server" Text=""></asp:Label>
                    </td>
                    <td colspan="2" style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbPeriodoValor" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid white;">
                        <asp:Label ID="lbUF" runat="server" Text="UF"></asp:Label>
                    </td>
                    <td style="border: 1px solid white;">
                        <asp:Label ID="lbCidade" runat="server" Text="Cidade"></asp:Label>
                    </td>
                    <td style="border: 1px solid white;">
                        <asp:Label ID="lbUnidade" runat="server" Text="Unidade"></asp:Label>
                    </td>
                    <td style="border: 1px solid white;">
                        <asp:Label ID="lbCentroCusto" runat="server" Text="Centro de Custo"></asp:Label>
                    </td>
                    <td style="border: 1px solid white;">
                        <asp:Label ID="lbAmbiente" runat="server" Text="Ambiente"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbUFValor" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbCidadeValor" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbUnidadeValor" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbCentroCustoValor" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="background-color: #F8F8FF; border: 1px solid white;">
                        <asp:Label ID="lbAmbienteValor" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="DefaultReportViewer" runat="server" Width="1024" Height="768" SizeToReportContent="true" BackColor="LightSteelBlue"
                        ShowPrintButton="False" ZoomMode="Percent" PromptAreaCollapsed="True" ShowBackButton="False"></rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>