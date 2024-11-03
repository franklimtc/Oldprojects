<%@ Page Title="Mudar_Senha" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MudarSenha.aspx.cs" Inherits="dnaPrint.Web.Seguranca.MudarSenha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container-fluid">
          <br />
          <div class="row">
            <div class="col-md-12">
                <h3 class="text-default text-center">
				    Mudar Senha do Usuário
			    </h3>
                <hr />
            </div>
        </div>
          <div class="row">
              <div class="col-md-3">
                    <span class="label label-default">Senha Antiga:</span>
                    <asp:TextBox runat="server" ID="tbSenhaAntiga" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rbSenhaAntiga"  ErrorMessage="*" ControlToValidate="tbSenhaAntiga" CssClass="text-danger"></asp:RequiredFieldValidator>
                    <br /><asp:Label runat="server" ID="lbErroSenhaAntiga" Text="Senha não confere!" CssClass="text-danger" Visible="false"></asp:Label>
              </div>
          </div>
           <div class="row">
              <div class="col-md-3">
                    <span class="label label-default">Senha Nova:</span>
                    <asp:TextBox runat="server" ID="tbSenhaNova1" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rbSenhaNova1"  ErrorMessage="**" ControlToValidate="tbSenhaNova1" CssClass="text-danger"></asp:RequiredFieldValidator>
                    
              </div>
          </div>
          <div class="row">
              <div class="col-md-3">
                    <span class="label label-default">Digite a senha novamente:</span>
                    <asp:TextBox runat="server" ID="tbSenhaNova2" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rbSenhaNova2"  ErrorMessage="***" ControlToValidate="tbSenhaNova2" CssClass="text-danger"></asp:RequiredFieldValidator>
                    <br /><asp:Label runat="server" ID="lbSenhaModificada" Text="Senha modificada com sucesso!" CssClass="text-success" Visible="false"></asp:Label>
                    <br /><asp:Label runat="server" ID="lbErroSenhaNova" Text="As senhas informadas são diferentes!" CssClass="text-danger" Visible="false"></asp:Label>
              </div>
          </div>
          <div class="row">
              <div class="col-md-3">
                   <asp:Button runat="server" ID="TbSalvar" Text="Salvar" CssClass="btn btn-default" OnClick="TbSalvar_Click" />
              </div>
          </div>
      </div>
</asp:Content>
