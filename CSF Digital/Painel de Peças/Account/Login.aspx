<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        input.placeholder {
            text-align: center;
            align-content: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                    <asp:Login ID="Login1" runat="server" EnableViewState="false" RenderOuterTable="false">
                    <LayoutTemplate>
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </span>
                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                             ValidationGroup="LoginUserValidationGroup"/>
                        <div class="table table-bordered">
                            <fieldset>
                                <p></p>
                                <p style="text-align:center"><img src="../Imagens/logoCSF.png" class="img-responsive" alt="" /></p>
                                <p>
                                    <%--<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuário:</asp:Label>--%>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Usuário" Width="100%" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                         CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                         ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <%--<asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>--%>
                                    <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password" placeholder="Senha" Width="100%" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                         CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                         ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p class="submitButton">
                                    <asp:Button ID="LoginButton" CssClass="btn btn-lg btn-primary btn-block" runat="server" Width="100%" CommandName="Login" Text="Entrar" ValidationGroup="LoginUserValidationGroup"/>
                                </p>
                            </fieldset>
                            
                        </div>
                    </LayoutTemplate>
                </asp:Login>
            </div>
            <div class="col-md-4"  style="width=33%"></div>
        </div>
    </div>
</asp:Content>



