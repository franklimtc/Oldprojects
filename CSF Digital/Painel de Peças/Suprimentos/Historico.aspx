<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Historico.aspx.cs" Inherits="Suprimentos_Historico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h2 style="text-align:center">Suprimentos Solicitados</h2>
                <asp:GridView ID="gvSuprimentosSolicitados" runat="server" CssClass="table table-hover table-condensed" DataSourceID="dsRequisicoes" AutoGenerateColumns="False" DataKeyNames="idreqSuprimento">
                    <Columns>
                        <asp:BoundField DataField="idreqSuprimento" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="idreqSuprimento" />
                        <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                        <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                        <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
                        <asp:BoundField DataField="suprimento" HeaderText="Suprimento" SortExpression="suprimento" />
                        <asp:BoundField DataField="dataSolicitacao" HeaderText="Data" SortExpression="dataSolicitacao" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="DuracaoEstimada" HeaderText="Dur Estimada" ReadOnly="True" SortExpression="DuracaoEstimada" />
                        <asp:BoundField DataField="Operador" HeaderText="Operador" ReadOnly="True" SortExpression="Operador" />
                    </Columns>
                </asp:GridView>
                <hr />
                <p></p>
                <p></p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2 style="text-align:center">Histórico de Envio de Suprimentos</h2>

                <asp:GridView ID="gvSuprimentos" runat="server" CssClass="table table-hover table-condensed" Width="100%" DataSourceID="dsRequisicoes2" AutoGenerateColumns="False" DataKeyNames="idEnvio">
                    <Columns>
                        <asp:BoundField DataField="idEnvio" HeaderText="idEnvio" InsertVisible="False" ReadOnly="True" SortExpression="idEnvio" />
                        <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
                        <asp:BoundField DataField="partnumber" HeaderText="partnumber" SortExpression="partnumber" />
                        <asp:BoundField DataField="suprimento" HeaderText="suprimento" SortExpression="suprimento" />
                        <asp:BoundField DataField="etiqueta" HeaderText="etiqueta" SortExpression="etiqueta" />
                        <asp:BoundField DataField="tpEnvio" HeaderText="tpEnvio" SortExpression="tpEnvio" />
                        <asp:BoundField DataField="postagem" HeaderText="postagem" SortExpression="postagem" />
                        <asp:BoundField DataField="dtEnvio" HeaderText="dtEnvio" SortExpression="dtEnvio" DataFormatString="{0:d}"  />
                        <asp:BoundField DataField="dtEntrega" HeaderText="dtEntrega" SortExpression="dtEntrega" DataFormatString="{0:d}"  />
                        <asp:BoundField DataField="statusentrega" HeaderText="statusentrega" SortExpression="statusentrega" />
                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>

    <%--Datasources--%>

    <asp:SqlDataSource ID="dsRequisicoes2" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="select a.idEnvio, a.serie, a.partnumber, b.suprimento, a.etiqueta, tpEnvio, postagem, dtEnvio
, dtEntrega, statusentrega 
from enviosSuprimentos as a
INNER JOIN equipamentos as c on a.serie = c.serie and c.status = 1
LEFT JOIN modelosSuprimentos as b on b.idmodelo = c.idmodelo
where a.serie = @serie
ORDER BY 1 DESC">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="serie" QueryStringField="serie" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsRequisicoes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select top 10 * from vw_reqsuprimentosResumo where serie = @serie">
        <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="serie" QueryStringField="serie" />
        </SelectParameters>
    </asp:SqlDataSource>
    <%--Datasources--%>
</asp:Content>

