<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dnaPrint.Web.Seguranca.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="md-col-12">
                <h3 class="text-default text-center">Configurações de Seguranças</h3>
            </div>
            <hr /><br />
        </div>
        <div class="row">
		    <div class="col-md-12">
			    <div class="tabbable" id="tabs-766203">
				    <ul class="nav nav-tabs">
					    <li class="active">
						    <a href="#panel01" data-toggle="tab">Usuários</a>
					    </li>
					   <%-- <li>
						    <a href="#panel02" data-toggle="tab">Usuários</a>
					    </li>--%>
				    </ul>
				    <div class="tab-content">
					    <div class="tab-pane active" id="panel01">
                            <br /><hr /><br />

                            <div class="row">
                                 <div class="col-md-3">
                                    <span class="label label-default">Grupo</span>
                                    <asp:DropDownList runat="server" ID="dpGrupo" CssClass="btn btn-default dropdown-toggle" Width="100%" DataSourceID="dsObjGrupos" DataTextField="Grupo" DataValueField="ID">
                                    </asp:DropDownList>
                                     <asp:ObjectDataSource ID="dsObjGrupos" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.GrupoUsuario">
                                         <SelectParameters>
                                             <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                                             <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                                         </SelectParameters>
                                     </asp:ObjectDataSource>
                                </div>
                                <div class="col-md-3">
                                    <span class="label label-default">Nome</span>
                                    <asp:TextBox runat="server" ID="tbNome" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfNome" ControlToValidate="tbNome" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <span class="label label-default">Email</span>
                                    <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RFEmail" ControlToValidate="tbEmail" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <span class="label label-default">Senha</span>
                                    <asp:TextBox runat="server" ID="tbSenha1" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RFSenha1" ControlToValidate="tbSenha1" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <span class="label label-default">Confirmar senha</span>
                                    <asp:TextBox runat="server" ID="tbSenha2" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RFSenha2" ControlToValidate="tbSenha2" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="rfErroUsuarioDuplicado" Visible="false" Text="Usuário ou email já existem!" CssClass="text-danger"></asp:Label>
                                    <asp:Label runat="server" ID="rfErroSenha" Visible="false" Text="As senhas informadas não conferem!" CssClass="text-danger"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Button runat="server" ID="tbAdicionar" Text="Adicionar" CssClass="btn btn-default" OnClick="tbAdicionar_Click" />
                                </div>
                            </div>

					    </div>
					    <div class="tab-pane" id="panel02">
                            <br /><hr /><br />
                            
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="gvUsuarios" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsObjUsers">
                    <Columns>
                        <asp:BoundField DataField="idusuario" HeaderText="ID" SortExpression="idusuario" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Grupo" HeaderText="Grupo" SortExpression="Grupo" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsObjUsers" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Account">
                    <SelectParameters>
                        <asp:SessionParameter Name="ConnString" SessionField="ConnString" Type="String" />
                        <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>

    </div>
</asp:Content>
