<%@ Page Title="Valores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Valores.aspx.cs" Inherits="dnaPrint.Web.Cadastros.Valores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h3 class="text-default text-center">
				    Cadastro de Valores
			    </h3>
            </div>
            <hr />
            <div class="col-md-6">
                <fieldset>
                    <legend>A4</legend>
                    <span class="label label-default">Preto e Branco</span>
                    <asp:TextBox runat="server" ID="tbPBA4" CssClass="form-control" Text="0,00"></asp:TextBox><br />
                    <span class="label label-default">Colorido</span>
                    <asp:TextBox runat="server" ID="tbColorA4" CssClass="form-control" Text="0,00"></asp:TextBox><br />
                    <span class="label label-default">Scanner</span>
                    <asp:TextBox runat="server" ID="tbScannerA4" CssClass="form-control" Text="0,00"></asp:TextBox><br />
                </fieldset>
            </div>
            
            <div class="col-md-6">
               <fieldset>
                    <legend>A3</legend>
                    <span class="label label-default">Preto e Branco</span>
                    <asp:TextBox runat="server" ID="tbPBA3" CssClass="form-control" Text="0,00"></asp:TextBox><br />
                    <span class="label label-default">Colorido</span>
                    <asp:TextBox runat="server" ID="tbColorA3" CssClass="form-control" Text="0,00"></asp:TextBox><br />
                    <span class="label label-default">Scanner</span>
                    <asp:TextBox runat="server" ID="tbScannerA3" CssClass="form-control" Text="0,00"></asp:TextBox><br />

                </fieldset>
            </div>


        </div>

         <div class="row">
            <div class="col-md-3">
                <asp:Button runat="server" ID="tbAdicionar" Text="Adicionar" CssClass="btn btn-default" OnClick="tbAdicionar_Click" />
                <asp:Button runat="server" ID="TbSalvar" Text="Salvar" CssClass="btn btn-default" Enabled="false" OnClick="TbSalvar_Click" />
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="gvValor" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsObjValores" OnDataBound="gvValor_DataBound" OnRowCommand="gvValor_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btEditar" CssClass="glyphicon glyphicon-pencil center-block" CausesValidation="false" CommandName="Editar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="valorpba4" HeaderText="Impressão PB A4" SortExpression="valorpba4" />
                        <asp:BoundField DataField="valorpba3" HeaderText="Impressão PB A3" SortExpression="valorpba3" />
                        <asp:BoundField DataField="valorcolora4" HeaderText="Impressão Color A4" SortExpression="valorcolora4" />
                        <asp:BoundField DataField="valorcolora3" HeaderText="Impressão Color A3" SortExpression="valorcolora3" />
                        <asp:BoundField DataField="valorscana4" HeaderText="Scaner A4" SortExpression="valorscana4" />
                        <asp:BoundField DataField="valorscana3" HeaderText="Scaner A3" SortExpression="valorscana3" />
                    </Columns>

                </asp:GridView>
                <asp:ObjectDataSource ID="dsObjValores" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.ValorPagina">
                    <SelectParameters>
                        <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                        <asp:SessionParameter Name="Tipo" SessionField="TipoDB" Type="Object" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
     </div>
</asp:Content>
