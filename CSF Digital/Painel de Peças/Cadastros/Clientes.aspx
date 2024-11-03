<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="Cadastros_Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h2 style="text-align:center">Cadastro de Clientes</h2>

    <div class="container">
        <%--Formulário--%>
            <div class="row">
                <div class="col-md-3">
                    <span>Cliente</span>
                    <asp:TextBox ID="tbCliente" runat="server" Width="100%" CssClass="form-control" PlaceHolder="Cliente"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <span>Endereço</span>
                    <asp:TextBox ID="tbEndereco" runat="server" Width="100%" CssClass="form-control" PlaceHolder="Endereço"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <span>Responsável</span>
                    <asp:TextBox ID="tbResponsavel" runat="server" Width="100%" CssClass="form-control" PlaceHolder="Responsável"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <span>Telefone</span>
                    <asp:TextBox ID="tbTelefone" runat="server" Width="100%" CssClass="form-control" PlaceHolder="Telefone"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <span>Email</span>
                    <asp:TextBox ID="tbEmail" runat="server" Width="100%" CssClass="form-control" PlaceHolder="Email"></asp:TextBox><br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <asp:Button ID="tbInserir" Text="Inserir" runat="server"  Width="80px" CssClass="btn btn-secondary" 
                    onclick="tbInserir_Click"/>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="tbSalvar" Text="Salvar" runat="server"  Width="80px"  CssClass="btn btn-secondary" Visible="false" />
                </div>
            </div>
         <%--Formulário--%>

        <br />

        <div class="row">
            <div class="col-md-12">
                    <asp:GridView ID="gvClientes" runat="server" DataSourceID="dsClientes" 
                        Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-condensed table-hover">
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="Qtd" HeaderText="Qtd" 
                                SortExpression="Qtd" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                                SortExpression="Cliente" />
                            <asp:BoundField DataField="Endereco" HeaderText="Endereco" 
                                SortExpression="Endereco" />
                            <asp:BoundField DataField="Responsavel" HeaderText="Responsavel" 
                                SortExpression="Responsavel" />
                            <asp:BoundField DataField="Telefone" HeaderText="Telefone" SortExpression="Telefone" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="IdCliente" HeaderText="IdCliente" SortExpression="IdCliente" Visible="false" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="dsClientes" runat="server" SelectMethod="Listar" TypeName="Clientes" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            </div>
        </div>
    </div>
<div>

</div>

</asp:Content>

