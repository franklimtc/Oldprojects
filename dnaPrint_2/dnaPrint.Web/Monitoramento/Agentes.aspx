<%@ Page Title="Agentes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Agentes.aspx.cs" Inherits="dnaPrint.Web.Monitoramento.Agentes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="md-col-12">
            <h3 class="text-default text-center">Monitoramento de Agentes</h3>
        </div>
        <hr />
    </div>

     <div class="row">
        <div class="md-col-12">
            <asp:GridView runat="server" ID="gvAgentes"  CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsObjAgentes" OnRowDataBound="gvAgentes_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image runat="server" ID="Image1" ImageUrl="~/img/icon_ok.png" Visible ="false" Height="20px" ImageAlign="Middle" />
                            <asp:Image runat="server" ID="Image2" ImageUrl="~/img/icon_alert.png" Visible ="false" Height="20px" ImageAlign="Middle" />
                            <asp:Image runat="server" ID="Image3" ImageUrl="~/img/icon_erro2.png" Visible ="false" Height="20px" ImageAlign="Middle" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                    <asp:BoundField DataField="Versao" HeaderText="Versao" SortExpression="Versao" />
                    <asp:BoundField DataField="QtdDias" HeaderText="QtdDias" ReadOnly="True" SortExpression="QtdDias" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="dsObjAgentes" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.Estacao">
                <SelectParameters>
                    <asp:SessionParameter Name="connString" SessionField="ConnString" Type="String" />
                    <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
