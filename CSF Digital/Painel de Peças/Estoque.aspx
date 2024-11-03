<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Estoque.aspx.cs" Inherits="Suprimentos_Estoque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3 class="text-center">Estoque Atual</h3>
    <br />
    <div class="row">
		<div class="col-md-12">
			<div class="row">
				<div class="col-md-12">
                    <span class="label label-default">Estoque:</span>
                    <asp:DropDownList ID="dpEstoque" runat="server" CssClass="btn btn-default dropdown-toggle" AutoPostBack="True" DataSourceID="objDSEstoques" DataTextField="Descricao" DataValueField="Id" OnSelectedIndexChanged="dpEstoque_SelectedIndexChanged" >
                    </asp:DropDownList>
				</div>
			</div>
		</div>
	</div>
    
    <p></p>
    
    <asp:GridView ID="gvEstoqueAtual" runat="server" CssClass="table table-hover table-condensed"></asp:GridView>
    <asp:ObjectDataSource ID="objDSEstoques" runat="server" SelectMethod="Listar" TypeName="Estoque"></asp:ObjectDataSource>
</asp:Content>

