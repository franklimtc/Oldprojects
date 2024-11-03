<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dnaPrint.Web.Logon.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
       <br />
        
	    <div class="row">

		    <div class="col-md-6">
				    <div class="form-group">
					    <h3 class="text-default">Login</h3>
					    <span class="label label-default">Email</span>
                        <asp:TextBox runat="server" ID="tbEmail" TextMode="Email" CssClass="form-control"></asp:TextBox>

				    </div>
				    <div class="form-group">
					    <span class="label label-default">Senha</span>
                        <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
				    </div>
                    <asp:Button ID="btEntrar" runat="server" CssClass="btn btn-default"  Text="Entrar" OnClick="btEntrar_Click"/>
                    <br /><asp:Label runat="server" ID="lbErro" Text="Usuário ou senha incorretos!" CssClass="text-danger" Visible="false"></asp:Label>
				   
		    </div>
	    </div>
    </div>
</asp:Content>
