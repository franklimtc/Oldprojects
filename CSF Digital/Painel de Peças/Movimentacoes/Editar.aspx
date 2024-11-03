<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Movimentacoes_Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			<div class="row">
				<div class="col-md-4">
				</div>
				<div class="col-md-4">
                     <ul class="list-group" style="list-style-type: none;">
                                <li><asp:Label runat="server" ID="Label2" CssClass="label label-default">ID: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbId" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label1" CssClass="label label-default">Tipo: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbDescricao" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label3" CssClass="label label-default">Qtd: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbQtd" CssClass="form-control" Enabled="true"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label4" CssClass="label label-default">Data: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbData" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label5" CssClass="label label-default">PartNumber: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbPartNumber" CssClass="form-control" Enabled="true"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label6" CssClass="label label-default">Estoque: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbEstoque" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label7" CssClass="label label-default">Série: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbSerie" CssClass="form-control" Enabled="True"></asp:TextBox>
                                </li>
                                <li><asp:Label runat="server" ID="Label8" CssClass="label label-default">Etiqueta: </asp:Label> 
                                    <asp:TextBox runat="server" ID="tbEtiqueta" CssClass="form-control" Enabled="True"></asp:TextBox>
                                </li>
                    </ul>
                   

                    <br />

					<asp:Button runat="server" ID="btSalvar" Text ="Salvar"  CssClass="btn btn-primary" OnClick="btSalvar_Click"/>
					<asp:Button runat="server" ID="btCancelar" Text ="Cancelar"  CssClass="btn btn-primary" OnClick="btCancelar_Click"/>
                    <br /><asp:Label runat="server" ID="lbErroPartNumber" Text="*PartNumber não cadastrado!" CssClass="label label-danger" Visible="false"></asp:Label>
                    <br /><asp:Label runat="server" ID="lbErroQuantidade" Text="**Informe uma quantidade válida!" CssClass="label label-danger" Visible="false"></asp:Label>
                    <br /><asp:Label runat="server" ID="lbErroSaldo" Text="****Saldo em estoque insuficiente!" CssClass="label label-danger" Visible="false"></asp:Label>
                    <br /><asp:Label runat="server" ID="lbSucessoSalvar" Text="*Dados atualizados com sucesso!" CssClass="label label-success" Visible="false"></asp:Label>
                    <br /><asp:Label runat="server" ID="lbErroSalvar" Text="**Falha na atualização dos dados!" CssClass="label label-danger" Visible="false"></asp:Label>

				</div>
				<div class="col-md-4">
				</div>
			</div>
		</div>
	</div>
</div>

</asp:Content>

