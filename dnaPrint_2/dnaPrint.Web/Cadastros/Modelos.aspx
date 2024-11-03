<%@ Page Title="Modelos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Modelos.aspx.cs" Inherits="dnaPrint.Web.Cadastros.Modelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h3 class="text-default text-center">
				Modelos de Equipamentos
			</h3>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-3">
            <span class="label label-default">Fabricante</span>
            <asp:TextBox runat="server" ID="tbFabricante" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rbFabricante"  ErrorMessage="Informe o nome do fabricante!" ControlToValidate="tbFabricante" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-3">
            <span class="label label-default">Modelo</span>
            <asp:TextBox runat="server" ID="tbModelo" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rbModelo"  ErrorMessage="Informe o modelo do equipamento!" ControlToValidate="tbFabricante" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-3">
            <span class="label label-default">Franquia</span>
            <asp:TextBox runat="server" ID="tbFranquia" TextMode="Number" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rbFranquia" ErrorMessage="Informe o valor da franquia!" ControlToValidate="tbFranquia" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-3">
            <span class="label label-default">Valor</span>
            <asp:TextBox runat="server" ID="tbValor" CssClass="form-control"></asp:TextBox>
            <asp:Label runat="server" ID="lbErroValor" Text="Informe um valor válido!" Visible="false" CssClass="text-danger"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <asp:Button runat="server" ID="tbAdicionar" Text="Adicionar" CssClass="btn btn-default" OnClick="tbAdicionar_Click"  />
            <asp:Button runat="server" ID="TbSalvar" Text="Salvar" CssClass="btn btn-default" Visible="false" />
            <asp:Button runat="server" ID="tbExcluir" Text="Excluir" CssClass="btn btn-default" Visible ="false" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gvModelos"  CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsObjModelosEquipamentos">
                <Columns>
                    <asp:BoundField DataField="idModeloEquipamento" HeaderText="ITEM" SortExpression="idModeloEquipamento" />
                    <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" SortExpression="Fabricante" />
                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                    <asp:BoundField DataField="Franquia" HeaderText="Franquia" SortExpression="Franquia" />
                    <asp:BoundField DataField="Valor" HeaderText="Valor" SortExpression="Valor" />
                    <%--<asp:CheckBoxField DataField="Status" HeaderText="Status" SortExpression="Status" />--%>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="dsObjModelosEquipamentos" runat="server" SelectMethod="ListarTodos" TypeName="dnaPrint.Base.ModeloEquipamento">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</div>
</asp:Content>
