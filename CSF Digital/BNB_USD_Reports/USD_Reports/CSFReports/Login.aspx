<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - CSFReports</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td align="center" valign="middle">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:Login ID="AppLogin" runat="server" BackColor="#F7F6F3" 
                            BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" 
                            onauthenticate="AppLogin_Authenticate">
                            <TextBoxStyle Font-Size="0.8em" />
                            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            <TitleTextStyle BackColor="#CC3300" Font-Bold="True" Font-Size="0.9em" 
                                ForeColor="White" />
                        </asp:Login>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>