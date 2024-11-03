<%@ Page Title="Unidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unidades.aspx.cs" Inherits="dnaPrint.Web.Unidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h3 class="text-default text-center">Cadastro de Unidades</h3>
        </div>
    </div>
    <hr />

    <div class="container-fluid">
	    <div class="row">
		    <div class="col-md-12">
			    <div class="row">
				    <div class="col-md-4">
                        <span class="label label-default">Estado</span><br />
                        <asp:DropDownList runat="server" ID="dpEstado" CssClass="btn btn-default dropdown-toggle" DataSourceID="dsOBJEstados" DataTextField="UF" DataValueField="idEstado" AutoPostBack="True" OnSelectedIndexChanged="dpEstado_SelectedIndexChanged">

                        </asp:DropDownList>
		                <asp:ObjectDataSource ID="dsOBJEstados" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Estado">
                            <SelectParameters>
                                <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                                <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
				    </div>
				    <div class="col-md-8">
                        <span class="label label-default">Cidade</span><br />
                                <asp:DropDownList runat="server" ID="dpCidade" CssClass="btn btn-default dropdown-toggle"  Width="100%" AutoPostBack="True" DataSourceID="dsOBJCidades" DataTextField="NomeCidade" DataValueField="idCidade" OnSelectedIndexChanged="dpCidade_SelectedIndexChanged"></asp:DropDownList>
                                <asp:ObjectDataSource ID="dsOBJCidades" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Cidade">
                                <SelectParameters>
                                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                                    <asp:ControlParameter ControlID="dpEstado" Name="idEstado" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                                </asp:ObjectDataSource>
				    </div>
			    </div><br />
			    <div class="row">
				    <div class="col-md-4">
                        <span class="label label-default">Unidade</span>
                        <asp:TextBox runat="server" ID="tbUnidade" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rbUnidade"  ErrorMessage="Informe o nome da unidade!" ControlToValidate="tbUnidade" CssClass="text-danger"></asp:RequiredFieldValidator>
				    </div>
				    <div class="col-md-8">
                        <span class="label label-default">Endereço</span>
                        <asp:TextBox runat="server" ID="tbEndereco" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rbEndereco" ErrorMessage="Informe o endereço da unidade!" ControlToValidate="tbEndereco" CssClass="text-danger"></asp:RequiredFieldValidator>
				    </div>
			    </div>
			    <div class="row">
				    <div class="col-md-4">
                        <span class="label label-default">Contato</span>
                        <asp:TextBox runat="server" ID="tbContato" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rbContato"  ErrorMessage="Informe o contato da unidade!" ControlToValidate="tbContato" CssClass="text-danger"></asp:RequiredFieldValidator>
				    </div>
				    <div class="col-md-4">
                        <span class="label label-default">Telefone</span>
                        <asp:TextBox runat="server" ID="tbTelefone" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rbTelefone" ErrorMessage="Informe o telefone da unidade!" ControlToValidate="tbTelefone" CssClass="text-danger"></asp:RequiredFieldValidator>
				    </div>
				    <div class="col-md-4">
                        <span class="label label-default">Email</span>
                        <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfEmail" ErrorMessage="Informe o email do contato na Unidade!" ControlToValidate="tbEmail" CssClass="text-danger"></asp:RequiredFieldValidator>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
	
   
    <br />
    <div class="row">
        <div class="col-md-3">
            <asp:Button runat="server" ID="tbAdicionar" Text="Adicionar" CssClass="btn btn-default" OnClick="tbAdicionar_Click" />
            <asp:Button runat="server" ID="TbSalvar" Text="Salvar" CssClass="btn btn-default" OnClick="TbSalvar_Click" Enabled ="false" />
            <asp:Button runat="server" ID="tbExcluir" Text="Excluir" CssClass="btn btn-default" OnClick="tbExcluir_Click" Enabled ="false" />
        </div>
    </div>
</div>

    <br />
    <div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gvUnidades" CssClass="table table-hover table-condensed" DataSourceID="DSObjUnidades" AutoGenerateColumns="False" OnRowCommand="gvUnidades_RowCommand" valid>
                <Columns>
                     <asp:TemplateField>
                         <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btEditar" CssClass="glyphicon glyphicon-pencil center-block" CausesValidation="false" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="idLocalidade" HeaderText="ID" SortExpression="idLocalidade" />
                    <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                    <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                    <asp:BoundField DataField="descricao" HeaderText="Unidade" SortExpression="descricao" />
                    <asp:BoundField DataField="endereco" HeaderText="Endereço" SortExpression="endereco" />
                    <asp:BoundField DataField="contato" HeaderText="Contato" SortExpression="contato" />

                    <asp:BoundField DataField="telefone" HeaderText="Telefone" SortExpression="telefone" />
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="DSObjUnidades" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Unidade">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpCidade" Name="_idCidade" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
    
</asp:Content>
