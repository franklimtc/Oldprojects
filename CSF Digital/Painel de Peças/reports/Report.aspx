<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="reports_Report" validateRequest="false"  %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 50px;
            width: 268435488px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
      
            <tr>
                <td><img src="../Imagens/logoCSF.png" /></td>
                <td></td>
                <td colspan="2" style="width:100%;text-align:center"><h2>Comprovante de Entrega de Material</h2></td>
            </tr>
            <tr>
                <td style="height:100px"></td>
                <td style="height:100px"></td>
                <td style="height:100px"></td>
                <td style="height:100px"></td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:large; height: 50px">Requisição:</td>
                <td><asp:Label runat="server" ID="lbReqID" Text="Req" Font-Bold="true"></asp:Label></td>
            </tr> 
            <tr>
                <td style="height:100px"></td>
                <td style="height:100px"></td>
                <td style="height:100px"></td>
                <td style="height:100px"></td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:large; height: 50px">Material:</td>
                <td><asp:Label runat="server" ID="lbMAterial" Text="Tonner"></asp:Label></td>
                <td style="text-align:right">Data: <asp:Label runat="server" ID="lbData" Text="Data"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:large; height: 50px">Endereço:</td>
                <td colspan="2"><asp:Label runat="server" ID="lbEndereco" Text="Endereco"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" style="font-size:large; height: 50px">Unidade: <asp:Label runat="server" ID="lbUnidade" Text="Endereco"></asp:Label></td>
                <td style="font-size:large; height: 50px">Setor: <asp:Label runat="server" ID="lbSetor" Text="Endereco"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:large; height: 50px">Serie:</td>
                <td colspan="2"><asp:Label runat="server" ID="lbSerie" Text="Série"></asp:Label></td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="2" style="font-size:large; height: 50px">Contato:</td>
                <td colspan="2" style="font-size:large; height: 50px; width:50%; text-align:center"><asp:Label runat="server" ID="lbContato" Text="Contato"></asp:Label></td>
                <td colspan="2" style="font-size:large; " class="auto-style1">Telefone:</td>
                <td colspan="2" style="font-size:large; height: 50px; width:50%; text-align:center"><asp:Label runat="server" ID="lbTelefone" Text="Telefone"></asp:Label></td>

            </tr>
            <tr>
                <td style="height:150px"></td>
                <td style="height:150px"></td>
                <td style="height:150px"></td>
                <td style="height:150px"></td>
            </tr>
            
        </table>
        <table>
            <tr>
                <td style="font-size:large; height: 80px; width:400px; text-align:center; border-top-style:solid; border-top-width:2px; border-top-color:black">Entregue por:</td>
                <td style="width:50px"></td>
                <td style="font-size:large; width:400px; text-align:center; border-top-style:solid; border-top-width:2px; border-top-color:black">Recebido por:</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
