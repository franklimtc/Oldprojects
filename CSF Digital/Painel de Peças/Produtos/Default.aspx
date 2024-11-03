<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Produtos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3 class="text-center">Lista de Produtos</h3>
    <div class="container-fluid">
	<div class="row">
        <fieldset>
            <legend>Novo Produto</legend>
            <div class="col-md-8">
			    <div class="row">
				    <div class="col-md-4">
                         <span class="label label-default">Produto:</span><asp:TextBox runat="server" ID="tbDescricao" CssClass="form-control"></asp:TextBox>
                            <span class="label label-default">PartNumber:</span><asp:TextBox runat="server" ID="tbParNumber" CssClass="form-control"></asp:TextBox>
                            <asp:Button runat="server" ID="btadicionar" Text ="Adicionar" CssClass="btn btn-primary" OnClick="btadicionar_Click" />
				    </div>
				    <div class="col-md-4">
				    </div>
				    <div class="col-md-4">
                        
				    </div>
			    </div>
		    </div>

		    <div class="col-md-4">
		    </div>
            
            <asp:RequiredFieldValidator ID="rfDescricao" runat="server" ErrorMessage="*Informe a descrição do produto!" ControlToValidate="tbDescricao" CssClass="label label-danger"></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator ID="rfPartNumber" runat="server" ErrorMessage="**Informe o partnumber do produto!" ControlToValidate="tbParNumber" CssClass="label label-danger"></asp:RequiredFieldValidator>
            <asp:Label ID="lbFalha" runat="server" Visible ="false" Text="Falha ao tentar inserir! Verifique os dados!" CssClass="label label-danger"></asp:Label>
            <asp:Label ID="lbUsuario" runat="server" Visible ="false" Text="Usuário não tem permissão para realizar esta operação!" CssClass="label label-danger"></asp:Label>
        </fieldset>
		    
	    </div>
    </div>

    <br />
    <asp:GridView ID="gvProdutos" runat="server" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="objDSProdutos">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Descricao" HeaderText="Descricao" SortExpression="Descricao" />
            <asp:BoundField DataField="Partnumber" HeaderText="Partnumber" SortExpression="Partnumber" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="objDSProdutos" runat="server" SelectMethod="Listar" TypeName="Produto"></asp:ObjectDataSource>
</asp:Content>

