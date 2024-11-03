<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Recolhimentos2.aspx.cs" Inherits="Suprimentos_Recolhimentos2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:TextBox runat="server" ID="tbIdReq" Visible="false"></asp:TextBox>
    <asp:TextBox runat="server" ID="tbUserName" Visible="false"></asp:TextBox>

    <div class="container-fluid">
	    <div class="row">
		    <div class="col-md-12">
			    <h3 class="text-center">
				    Controle de Retorno de Suprimentos
			    </h3>
		    </div>
	    </div>
    </div>
    <br />

      <div class="container-fluid">
	    <div class="row">
		    <div class="col-md-4">
			    <h3 class="text-center"><asp:Label runat="server" ID="lbUser"></asp:Label></h3>
                    
                    <asp:GridView runat="server" ID="gvResumo" AutoGenerateColumns="False" DataSourceID="dsResumoRecolhimentos" CssClass="table table-condensed">
                        <Columns>
                            <asp:BoundField DataField="tipo" HeaderText="tipo" SortExpression="tipo" />
                            <asp:BoundField DataField="StatusSolicitacao" HeaderText="StatusSolicitacao" SortExpression="StatusSolicitacao" />
                            <asp:BoundField DataField="qtd" HeaderText="qtd" ReadOnly="True" SortExpression="qtd" />
                        </Columns>

                    </asp:GridView>
				    <asp:SqlDataSource ID="dsResumoRecolhimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select tipo, StatusSolicitacao, sum(Quantidade) 'qtd' 
from vw_ControlePostagensReversas where Usuario = @usuario
group by tipo, StatusSolicitacao
" OnUpdated="dsResumoRecolhimentos_Updated">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="tbUserName" Name="usuario" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
		    </div>
            <div class="col-md-4">
                <h3 class="text-center">Resumo Geral</h3>
                <asp:GridView runat="server" ID="gvResumoGeral" CssClass="table table-condensed" AutoGenerateColumns="False" DataSourceID="dsResumoGeral">
                    <Columns>
                        <asp:BoundField DataField="Suprimento" HeaderText="Suprimento" SortExpression="Suprimento" />
                        <asp:BoundField DataField="QTD" HeaderText="QTD" ReadOnly="True" SortExpression="QTD" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="dsResumoGeral" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select tipo 'Suprimento', sum(quantidade) 'QTD' 
from vw_ControlePostagensReversas 
where status &lt; 3
group by tipo
"></asp:SqlDataSource>
            </div>
            <div class="col-md-4"></div>
	    </div>
    </div>
    <br />

    <div class="container-fluid">
	    <div class="row">
		    <div class="col-md-12">
			    <div class="row">
				    <div class="col-md-6">
					    <div class="row">
						    <div class="col-md-6">
                                <span class="label label-default">Coleta:</span>
                                <asp:TextBox runat="server" ID="tbColeta" CssClass="form-control" TextMode="Number"></asp:TextBox>
						    </div>
						    <div class="col-md-6">
                                <span class="label label-default">Postagem:</span>
                                <asp:TextBox runat="server" ID="tbPostagem" CssClass="form-control"></asp:TextBox>

						    </div>
					    </div>
				    </div>
				    <div class="col-md-6">
					    <div class="row">
						    <div class="col-md-6">
                                <span class="label label-default">Tipo:</span>
                                <asp:DropDownList runat="server" ID="dpTipo" CssClass="form-control">
                                    <asp:ListItem Text="Toner" Value="1" Selected="False"></asp:ListItem>
                                    <asp:ListItem Text="Cilindro" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Peça" Value="3"></asp:ListItem>
                                </asp:DropDownList>

						    </div>
						    <div class="col-md-6">
                                <span class="label label-default">Quantidade:</span>
                                <asp:TextBox runat="server" ID="tbQuantidade" CssClass="form-control"></asp:TextBox>

						    </div>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>

    <br />
    <div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			 <asp:Button runat="server" ID="btAdicionar" Text="Inserir" CssClass="btn btn-default" OnClick="btAdicionar_Click" />
			 &nbsp;
			 <asp:Button runat="server" ID="btAtualizar" Text="Atualizar" CssClass="btn btn-default" Visible ="false" OnClick="btAtualizar_Click" />
            &nbsp;
			 <asp:Button runat="server" ID="btExcluir" Text="Excluir" CssClass="btn btn-default" Visible ="false" OnClick="btExcluir_Click" />

		</div>
	</div>
        <div class="row">
		<div class="col-md-12">
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Informe o valor correto da coleta!" CssClass="label label-danger" ControlToValidate="tbColeta" Type="Integer" MinimumValue="1" MaximumValue ="999999999"></asp:RangeValidator> <br />
            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Informe uma quantidade válida!" CssClass="label label-danger" ControlToValidate="tbQuantidade" Type="Integer" MinimumValue="1" MaximumValue ="10"></asp:RangeValidator> <br />

		</div>
	</div>
</div>
    
    <br />

    <asp:GridView runat="server" ID="gvListaReversos" CssClass="table table-condensed table-hover" AutoGenerateColumns="False" DataSourceID="objDSReversos" OnRowCommand="gvListaReversos_RowCommand">
        <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btEditar" CssClass="glyphicon glyphicon-pencil center-block" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="CodColeta" HeaderText="CodColeta" SortExpression="CodColeta" />
            <asp:BoundField DataField="Postagem" HeaderText="Postagem" SortExpression="Postagem" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" />
            <asp:BoundField DataField="data" HeaderText="data" SortExpression="data" />
            <asp:BoundField DataField="StatusSolicitacao" HeaderText="StatusSolicitacao" SortExpression="StatusSolicitacao" />
        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource ID="objDSReversos" runat="server" SelectMethod="ListarPorUsurio" TypeName="ControlePostagenReversa">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbUserName" Name="Usuario" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

