<%@ Page Title="Equipamentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipamentos.aspx.cs" Inherits="dnaPrint.Web.Cadastros.Equipamentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h3 class="text-default text-center">
				Cadastro de Equipamentos
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
             <asp:DropDownList runat="server" ID="dpUnidades" CssClass="btn btn-default dropdown-toggle" Width="100%" AutoPostBack="True" DataSourceID="dsObjUnidade" DataTextField="descricao" DataValueField="idLocalidade"  OnSelectedIndexChanged="dpUnidades_SelectedIndexChanged">
               
             </asp:DropDownList>
		   
		    <asp:ObjectDataSource ID="dsObjUnidade" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Unidade" OnSelected="dsObjUnidade_Selected">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpCidade" Name="_idCidade" PropertyName="SelectedValue" Type="Int32" DefaultValue="1" />
                </SelectParameters>
            </asp:ObjectDataSource>
		   


		</div>

        <div class="col-md-3">
            <span class="label label-default">Setor</span>
             <asp:DropDownList runat="server" ID="dpSetor" CssClass="btn btn-default dropdown-toggle" Width="100%" AutoPostBack="True" DataTextField="Descricao" DataValueField="idSetor" OnSelectedIndexChanged="dpSetor_SelectedIndexChanged">

             </asp:DropDownList>
		   
		    <asp:ObjectDataSource ID="dsObjSetor" runat="server" SelectMethod="ListarByUnidade" TypeName="dnaPrint.Base.Setor">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpUnidades" Name="idUnidade" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
		   
		</div>

          <div class="col-md-3">
            <span class="label label-default">Modelo</span>
             <asp:DropDownList runat="server" ID="dpModeloEqpto" CssClass="btn btn-default dropdown-toggle" Width="100%" DataSourceID="objDSModelos" DataTextField="ItemModelo" DataValueField="idModeloEquipamento" AutoPostBack="True" >

             </asp:DropDownList>
		    <asp:ObjectDataSource ID="objDSModelos" runat="server" SelectMethod="ListarTodos" TypeName="dnaPrint.Base.ModeloEquipamento">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
		</div>

    </div>
        <br />
    <div class="row">
        <div class="col-md-3">
            <span class="label label-default">IP</span>
            <asp:TextBox runat="server" ID="tbIP" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <span class="label label-default">Série</span>
            <asp:TextBox runat="server" ID="tbSerie" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <span class="label label-default">Fila</span>
            <asp:TextBox runat="server" ID="tbFila" CssClass="form-control">
            </asp:TextBox>
        </div>

    </div>
        <br /> 
         <div class="row">
            <div class="col-md-3">
                <asp:Button runat="server" ID="tbAdicionar" Text="Adicionar" CssClass="btn btn-default" OnClick="tbAdicionar_Click" />
                <asp:Button runat="server" ID="TbSalvar" Text="Salvar" CssClass="btn btn-default" OnClick="TbSalvar_Click" Enabled ="false" />
                <asp:Button runat="server" ID="tbExcluir" Text="Excluir" CssClass="btn btn-default" OnClick="tbExcluir_Click" Enabled ="false"/>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <br /><asp:RequiredFieldValidator runat="server" ID="rbFila"  ErrorMessage="Informe a fila de impressão do equipamento!" ControlToValidate="tbFila" CssClass="text-danger"></asp:RequiredFieldValidator>
                <br /><asp:Label runat="server" ID="lbFila" Text="Fila de impressão já cadastrada!" CssClass="text-danger" Visible="false"></asp:Label>
                <br /><asp:RequiredFieldValidator runat="server" ID="rbSerie"  ErrorMessage="Informe a série do equipamento!" ControlToValidate="tbSerie" CssClass="text-danger"></asp:RequiredFieldValidator>
                <br /><asp:Label runat="server" ID="lbSerie" Text="Série já cadastrada!" CssClass="text-danger" Visible="false"></asp:Label>
                <br /><asp:RequiredFieldValidator runat="server" ID="rbIP"  ErrorMessage="Informe o ip do equipamento!" ControlToValidate="tbIP" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>
        </div>

    

    <div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gvEquipamentos" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsOBJEquipamentos" OnRowCommand="gvEquipamentos_RowCommand">
                <Columns>
                    <asp:TemplateField>
                         <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btEditar" CssClass="glyphicon glyphicon-pencil center-block" CausesValidation="false" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="idEquipamento" HeaderText="ID" SortExpression="idEquipamento" />
                    <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                    <asp:BoundField DataField="Serie" HeaderText="Série" SortExpression="Serie" />
                    <asp:BoundField DataField="nome" HeaderText="Fila" SortExpression="nome" />
                </Columns>

            </asp:GridView>
            <asp:ObjectDataSource ID="dsOBJEquipamentos" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Equipamento">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                    <asp:ControlParameter ControlID="dpEstado" Name="idEstado" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="dpCidade" Name="idCidade" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="dpUnidades" Name="idLocalidade" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="dpSetor" Name="idSetor" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
         </div>
    </div>
</div>
</asp:Content>
