<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConfirmarEntrega.aspx.cs" Inherits="Suprimentos_ConfirmarEntrega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12" style="text-align: center">
                <p></p>
                <h2>Confirmação de Entrega de Suprimentos</h2>
                <p></p>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="gvConfirmar" DataSourceID="dsLista" 
                    CssClass="table table-hover table-striped" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="gvConfirmar_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                        <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                        <asp:BoundField DataField="postagem" HeaderText="Postagem" SortExpression="postagem" />
                        <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
                        <asp:BoundField DataField="dtEntrega" HeaderText="Entregue" SortExpression="dtEntrega" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="servico" HeaderText="Serviço" SortExpression="servico" />
                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                        <asp:BoundField DataField="contato" HeaderText="Contato" SortExpression="contato" />
                        <asp:BoundField DataField="fone" HeaderText="Fone" SortExpression="fone" />
                        <asp:BoundField DataField="observacoes" HeaderText="OBS" SortExpression="observacoes" Visible="false" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btConfirmar" 
                                    Text="Confirmar" CommandName="Confirmar" 
                                    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <%--Data Sources--%>
    <asp:SqlDataSource runat="server" ID="dsLista" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>"
        SelectCommand="select * from vw_confirmarEntregasCorreios where status <> 'Concluido' order by uf, cidade"></asp:SqlDataSource>
    <%--Data Sources--%>

</asp:Content>

