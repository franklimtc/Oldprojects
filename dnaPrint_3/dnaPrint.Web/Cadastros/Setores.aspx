<%@ Page Title="Setores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setores.aspx.cs" Inherits="dnaPrint.Web.Setores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h3 class="text-default text-center">
				Cadastro de Setores
			</h3>
        </div>
    </div>
    <hr />
	<div class="row">
		<div class="col-md-3">
            <span class="label label-default">Estado</span><br />
            <asp:DropDownList runat="server" ID="dpEstado" CssClass="btn btn-default dropdown-toggle" DataSourceID="dsOBJEstados" DataTextField="UF" DataValueField="idEstado" AutoPostBack="True" OnSelectedIndexChanged="dpEstado_SelectedIndexChanged"></asp:DropDownList>
		    <asp:ObjectDataSource ID="dsOBJEstados" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Estado">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
		</div>

         <div class="col-md-3">
            <span class="label label-default">Cidade</span>
             <asp:DropDownList runat="server" ID="dpCidade" CssClass="btn btn-default dropdown-toggle" Width="100%" AutoPostBack="True" DataSourceID="dsOBJCidades" DataTextField="NomeCidade" DataValueField="idCidade" OnSelectedIndexChanged="dpCidade_SelectedIndexChanged"></asp:DropDownList>
		    <asp:ObjectDataSource ID="dsOBJCidades" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Cidade">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpEstado" Name="idEstado" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
		</div>

	</div>
   
    <br />
    <div class="row">
        <div class="col-md-3">
            <span class="label label-default">Unidade</span>
             <asp:DropDownList runat="server" ID="dpUnidades" CssClass="btn btn-default dropdown-toggle" Width="100%" AutoPostBack="True" DataSourceID="dsObjUnidade" DataTextField="descricao" DataValueField="idLocalidade" OnSelectedIndexChanged="dpUnidades_SelectedIndexChanged" OnTextChanged="dpUnidades_SelectedIndexChanged">
               
             </asp:DropDownList>
		   
		    <asp:ObjectDataSource ID="dsObjUnidade" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Unidade" OnDataBinding="dsObjUnidade_DataBinding" OnSelected="dsObjUnidade_Selected" OnUpdated="dsObjUnidade_Updated">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpCidade" Name="_idCidade" PropertyName="SelectedValue" Type="Int32" DefaultValue="1" />
                </SelectParameters>
            </asp:ObjectDataSource>
		</div>

         <div class="col-md-3">
            <span class="label label-default">Setor</span>
            <asp:TextBox runat="server" ID="tbSetor" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rbSetor"  ErrorMessage="Informe o nome do Setor!" ControlToValidate="tbSetor" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>

    </div>
    
    <div class="row">
        <div class="col-md-3">
            <span class="label label-default">Centro de Custo</span>
            <asp:TextBox runat="server" ID="tbCentroCusto" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rbCenroCusto"  ErrorMessage="Informe o Centro de Custo!" ControlToValidate="tbCentroCusto" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>

         <div class="col-md-3">
            <span class="label label-default">Cota Mensal</span>
             <asp:TextBox runat="server" ID="tbCotaMensal" CssClass="form-control" Text ="10000"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rbCotaMensal"  ErrorMessage="informe a cota mensal do setor!" ControlToValidate="tbCotaMensal" CssClass="text-danger"></asp:RequiredFieldValidator>
            <br /><asp:RangeValidator runat="server" ID="rvCotaMensal" ErrorMessage="Informe um valor válido para a cota!" ControlToValidate="tbCotaMensal" MinimumValue="0" MaximumValue="10000000" Type="Integer" CssClass="text-danger"></asp:RangeValidator>
        </div>
   
    </div>
    <br />
     <div class="row">
        <div class="col-md-3">
            <asp:Button runat="server" ID="tbAdicionar" Text="Adicionar" CssClass="btn btn-default" OnClick="tbAdicionar_Click"  />
            <asp:Button runat="server" ID="TbSalvar" Text="Salvar" CssClass="btn btn-default" OnClick="TbSalvar_Click" Enabled="false" />
            <asp:Button runat="server" ID="tbExcluir" Text="Excluir" CssClass="btn btn-default" OnClick="tbExcluir_Click" Enabled="false" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gvSetor" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsOBJSetores" OnRowCommand="gvSetor_RowCommand" OnDataBinding="gvSetor_DataBinding">
                <Columns>
                    <asp:TemplateField>
                         <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btEditar" CssClass="glyphicon glyphicon-pencil center-block" CausesValidation="false" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="idSetor" HeaderText="ID" SortExpression="idSetor"/>
                    <%--<asp:BoundField DataField="idLocalidade" HeaderText="idLocalidade" SortExpression="idLocalidade" />--%>
                    <asp:BoundField DataField="Descricao" HeaderText="Setor" SortExpression="Descricao" />
                    <asp:BoundField DataField="CentroCusto" HeaderText="Centro de Custo" SortExpression="CentroCusto" />
                    <%--<asp:CheckBoxField DataField="Status" HeaderText="Status" SortExpression="Status" />--%>
                    <asp:BoundField DataField="CotaMensal" HeaderText="Cota Mensal" SortExpression="CotaMensal" />
                </Columns>

            </asp:GridView>
            <asp:ObjectDataSource ID="dsOBJSetores" runat="server" SelectMethod="ListarByUnidade" TypeName="dnaPrint.Base.Setor">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpUnidades" DefaultValue="" Name="idUnidade" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
    
</div>
</asp:Content>
