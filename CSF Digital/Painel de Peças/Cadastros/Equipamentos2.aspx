<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equipamentos2.aspx.cs" Inherits="Cadastros_Equipamentos2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 style="text-align:center">Cadastro de Equipamentos</h2>
    <p></p>
    <p></p>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6 table table-bordered">
                <div class="row">
                    <div class="col-md-3">
                        <span>Cliente</span>
                        <asp:DropDownList ID="dpClientes" runat="server" Width="100%" DataSourceID="dsClientes" DataTextField="cliente" DataValueField="idCliente" AutoPostBack="false" CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                         <span>Técnico</span>
                        <asp:DropDownList ID="dpTecnico" runat="server" Width="100%" DataSourceID="dsTecnicos" DataTextField="nome" DataValueField="idTecnico" AutoPostBack="False" CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <span>Modelo</span>
                        <asp:DropDownList ID="dpModelo" runat="server" Width="100%" DataSourceID="dsModelos" DataTextField="mod" DataValueField="idModelo"  CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <span>Série</span> <asp:RequiredFieldValidator ID="rfSerie" runat="server" ControlToValidate="tbSerie" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbSerie" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnTextChanged="tbSerie_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span>UF</span>
                        <asp:DropDownList ID="dpUF" runat="server" AutoPostBack="True" DataSourceID="dsUf" DataTextField="uf" DataValueField="idEstado" Width="100%"  CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <span>Cidade</span>
                        <asp:DropDownList ID="dpCidade" runat="server" DataTextField="cidade" DataValueField="cidade" AutoPostBack="True" DataSourceID="dsCidades" Width="100%"  CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <span>Unidade</span>
                        <asp:TextBox ID="tbUnidade" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <span>Setor</span>
                        <asp:TextBox ID="tbSetor" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                         <span>Endereço</span><asp:RequiredFieldValidator ID="rfEndereco" runat="server" ControlToValidate="tbEndereco" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbEndereco" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Num</span><asp:RequiredFieldValidator ID="rfNumero" runat="server" ControlToValidate="tbNum" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbNum" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <span>Bairro</span><asp:RequiredFieldValidator ID="rfBairro" runat="server" ControlToValidate="tbBairro" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbBairro" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>

                    </div>
                    <div class="col-md-4">
                        <span>CEP</span><asp:RequiredFieldValidator ID="rfCep" runat="server" ControlToValidate="tbCEP" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbCEP" runat="server" CssClass="form-control" Width="100%" placeholder="Apenas números!"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <span>Contato</span><asp:RequiredFieldValidator ID="rfContato" runat="server" ControlToValidate="tbContato" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbContato" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <span>Fone</span><asp:RequiredFieldValidator ID="rfFone" runat="server" ControlToValidate="tbFone" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbFone" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>

                    </div>
                    <div class="col-md-6">
                        <span>Email</span><asp:RequiredFieldValidator ID="rfEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="*" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" Width="100%" TextMode="Email"></asp:TextBox>
                        <p></p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btAdicionar" runat="server" CssClass="btn btn-secondary" Text="Adicionar" Width="100%" OnClick="btAdicionar_Click"/>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btSalvar" runat="server" CssClass="btn btn-secondary" Text="Salvar" Width="100%" OnClick="btSalvar_Click"/>
                        <p></p>
                    </div>
                    <div class="col-md-9"></div>
                </div>
            </div>
            <div class="col-md-3"></div>
       </div>
    </div>

    <asp:CustomValidator id="CVCEP" runat="server"  OnServerValidate="ValidarCEP" ControlToValidate="tbCEP"></asp:CustomValidator>
    <asp:CustomValidator id="CVSerie" runat="server"  OnServerValidate="ValidarSerie" ControlToValidate="tbSerie"></asp:CustomValidator>

    <%--DataSources--%>

    <asp:SqlDataSource ID="dsUf" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT idEstado, uf FROM estados">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCidades" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select idCidade, cidade from cidades where idEstado = @idEstado">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpUF" Name="idEstado" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTecnicos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select idTecnico, nome + ' - ' + contato 'nome' from tecnicos"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsModelos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select idModelo, mod  from modelos where status = 1 order by padrao desc, fabricante, modelo"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT [idCliente], [cliente] FROM [vw_clientes]"></asp:SqlDataSource>
    <%--DataSources--%>
</asp:Content>

