<%@ Page Title="Equipamentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipamentos.aspx.cs" Inherits="dnaPrint.Web.Monitoramento.Equipamentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="md-col-12">
            <h3 class="text-default text-center">Monitoramento de Equipamentos</h3>
        </div>
        <hr />
    </div>
    <div class="row">
        <div class="md-col-12">
             <asp:GridView runat="server" id="gvMonitorEquipamentos" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="dsOBJMonitorEquipamentos" OnRowDataBound="gvMonitorEquipamentos_RowDataBound">
                 <Columns>
                      <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:Image runat="server" ID="Image1" ImageUrl="~/img/icon_ok.png" Visible ="false" Height="20px" ImageAlign="Middle" />
                                    <asp:Image runat="server" ID="Image2" ImageUrl="~/img/icon_alert.png" Visible ="false" Height="20px" ImageAlign="Middle" />
                                    <asp:Image runat="server" ID="Image3" ImageUrl="~/img/icon_erro2.png" Visible ="false" Height="20px" ImageAlign="Middle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                     <asp:BoundField DataField="idEquipamento" HeaderText="ID" SortExpression="idEquipamento" />
                     <asp:BoundField DataField="UF" HeaderText="UF" SortExpression="UF" />
                     <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
                     <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                     <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" SortExpression="Ambiente" />
                     <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
                     <asp:BoundField DataField="Fila" HeaderText="Fila" SortExpression="Fila" />
                     <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                     <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
                     <asp:BoundField DataField="QTDDias" HeaderText="QTDDias" SortExpression="QTDDias" />
                 </Columns>
             </asp:GridView>
             <asp:ObjectDataSource ID="dsOBJMonitorEquipamentos" runat="server" SelectMethod="Listar" TypeName="dnaPrint.Base.MonitorEquipamento">
                 <SelectParameters>
                     <asp:SessionParameter Name="connString" SessionField="connString" Type="String" />
                     <asp:SessionParameter Name="TipoDB" SessionField="TipoDB" Type="Object" />
                 </SelectParameters>
             </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
