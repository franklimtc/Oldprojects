<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Solicitacoes2.aspx.cs" Inherits="Solicitacoes2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			<div class="row">
				<div class="col-md-4">
                    <span class="label label-default">Cliente:</span>
                    <asp:DropDownList runat="server" ID="dpClientes" CssClass="btn btn-default dropdown-toggle" DataSourceID="objDSClientes" DataTextField="Cliente" DataValueField="IdCliente" AutoPostBack="True"></asp:DropDownList>
				    <asp:ObjectDataSource ID="objDSClientes" runat="server" SelectMethod="Listar" TypeName="Clientes"></asp:ObjectDataSource>
                    

				</div>
				<div class="col-md-4">
                    <div class="row">
					    <div class="col-md-12">
                            <span class="label label-default">UF:</span>
                            <asp:DropDownList runat="server" ID="dpUF" CssClass="btn btn-default dropdown-toggle" DataSourceID="objUfs" DataTextField="Uf" DataValueField="Uf" AutoPostBack="True"></asp:DropDownList>
				            <asp:ObjectDataSource ID="objUfs" runat="server" SelectMethod="Listar" TypeName="UF">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
					    </div>
					</div>
                    <div class="row">
					    <div class="col-md-12">
                            <span class="label label-default">Cidade:</span>
                            <asp:DropDownList runat="server" ID="dpCidade" CssClass="btn btn-default dropdown-toggle" AutoPostBack="True" DataSourceID="objCidades" DataTextField="CidadeDescricao" DataValueField="CidadeDescricao" Width="100%"></asp:DropDownList>
				            <asp:ObjectDataSource ID="objCidades" runat="server" SelectMethod="Listar" TypeName="Cidade">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
					    </div>
					</div>
                    <div class="row">
					    <div class="col-md-12">
                            <span class="label label-default">Unidade:</span>
                            <asp:DropDownList runat="server" ID="dpUnidade" CssClass="btn btn-default dropdown-toggle" AutoPostBack="True" DataSourceID="objUnidades" DataTextField="UnidadeDescricao" DataValueField="UnidadeDescricao" Width="100%"></asp:DropDownList>
					        <asp:ObjectDataSource ID="objUnidades" runat="server" SelectMethod="Listar" TypeName="Unidade">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="dpCidade" Name="cidade" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
					    </div>
					</div>
				</div>
				<div class="col-md-4">
                    <div class="row">
					    <div class="col-md-12">
                            <span class="label label-default">Equipamentos:</span>
                            <asp:DropDownList runat="server" ID="dpEquipamentos" CssClass="btn btn-default dropdown-toggle" AutoPostBack="True" DataSourceID="objDSEquipamentos" DataTextField="Serie" DataValueField="Serie" Width="100%"></asp:DropDownList>

					        <asp:ObjectDataSource ID="objDSEquipamentos" runat="server" SelectMethod="Listar" TypeName="Equipamentos">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="dpCidade" Name="cidade" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="dpUnidade" Name="unidade" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

					    </div>
					</div>
				</div>
			</div>
		</div>
	</div>


</div>
</asp:Content>

