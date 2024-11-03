<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Movimentacoes_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3 class="text-center">Movimentação de produtos</h3>
    <br />
    <div class="container-fluid">
	    <div class="row">
		    <div class="col-md-12">
			    <div class="row">
				    <div class="col-md-4">
					    <div class="row">
						    <div class="col-md-6">
							     <span class="label label-default">Descrição</span>
                                <asp:DropDownList runat="server" ID="dpDescricao" CssClass="btn btn-default dropdown-toggle" AutoPostBack="True" OnSelectedIndexChanged="dpDescricao_SelectedIndexChanged">
                                    <asp:ListItem Text="Entrada" Value="Entrada"></asp:ListItem>
                                    <asp:ListItem Text="Saida" Value="Saida" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
						    </div>
						    <div class="col-md-6">
							     <span class="label label-default">Estoque</span>
                                <asp:DropDownList runat="server" ID="dpEstoque" CssClass="btn btn-default dropdown-toggle" DataSourceID="dsObjEstoques" DataTextField="Descricao" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="dpEstoque_SelectedIndexChanged"></asp:DropDownList>
						         <asp:ObjectDataSource ID="dsObjEstoques" runat="server" SelectMethod="Listar" TypeName="Estoque"></asp:ObjectDataSource>
						    </div>
					    </div>
				    </div>
				    <div class="col-md-4">
					    <div class="row">
						    <div class="col-md-6">
							     <span class="label label-default">PartNumber</span>
                                <%--<asp:TextBox runat="server" ID="tbPartNumber" CssClass="form-control"></asp:TextBox>--%>
                                <asp:DropDownList runat="server" ID="tbPartNumber" CssClass="btn btn-default dropdown-toggle" DataSourceID="objDSPartNumber" DataTextField="Partnumber" DataValueField="Id"></asp:DropDownList>
						         <asp:ObjectDataSource ID="objDSPartNumber" runat="server" SelectMethod="Listar" TypeName="Produto"></asp:ObjectDataSource>
						    </div>
						    <div class="col-md-6">
							     <span class="label label-default">Qtd</span>
                                 <asp:TextBox runat="server" ID="tbQtd" CssClass="form-control"></asp:TextBox>
						    </div>
					    </div>
				    </div>
				    <div class="col-md-4">
					    <div class="row">
						    <div class="col-md-6">
							     <span class="label label-default">Série</span>
                                <asp:TextBox runat="server" ID="tbSerie" CssClass="form-control"></asp:TextBox>
						    </div>
						    <div class="col-md-6">
							     <span class="label label-default">Etiqueta</span>
                                    <asp:TextBox runat="server" ID="tbEtiqueta" CssClass="form-control"></asp:TextBox>
						    </div>
					    </div>
				    </div>
			    </div>
                <br /><asp:Label runat="server" ID="lbErroPartNumber" Text="*PartNumber não cadastrado!" CssClass="label label-danger" Visible="false"></asp:Label>
                <br /><asp:Label runat="server" ID="lbErroQuantidade" Text="**Informe uma quantidade válida!" CssClass="label label-danger" Visible="false"></asp:Label>
                <br /><asp:Label runat="server" ID="lbErroSerieEtiqueta" Text="***Informe a Série ou Etiqueta válida!" CssClass="label label-danger" Visible="false"></asp:Label>
                <br /><asp:Label runat="server" ID="lbErroSaldo" Text="****Saldo em estoque insuficiente!" CssClass="label label-danger" Visible="false"></asp:Label>



		    </div>
	    </div>
        <asp:Button ID="btAdicionar" Text="Adicionar" runat="server" CssClass="btn btn-primary" OnClick="btAdicionar_Click"/>
    </div>
    <br />

    <asp:GridView runat="server" ID="gvMovimentacoes" DataSourceID="dsobjMovimentacoes" CssClass="table table-hover table-condensed" OnRowCommand="gvMovimentacoes_RowCommand">
    </asp:GridView>
    <asp:ObjectDataSource ID="dsobjMovimentacoes" runat="server" SelectMethod="ListarMovimentacoesDia" TypeName="movimentacao" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpEstoque" Name="estoque" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
</asp:ObjectDataSource>
    
</asp:Content>

