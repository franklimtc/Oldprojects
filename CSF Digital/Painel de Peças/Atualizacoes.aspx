<%@ Page Title="Atualizações" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Atualizacoes.aspx.cs" Inherits="Atualizacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

 <h2>ATUALIZAÇÕES DE PEÇAS</h2>
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False" DataSourceID="Pecas" ForeColor="#333333" AllowSorting="True" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        DataKeyNames="idreqPeca" CssClass="table table-striped table-bordered table-condensed table-hover">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Ver" />
                <asp:BoundField DataField="idreqPeca" HeaderText="ID" 
                    SortExpression="idreqPeca" ReadOnly="True" InsertVisible="False" />
                <asp:BoundField DataField="cliente" HeaderText="Cliente" 
                    SortExpression="cliente" />
                <asp:BoundField DataField="reqUSD" HeaderText="USD" 
                    SortExpression="reqUSD" />
                <asp:BoundField DataField="uf" HeaderText="UF" 
                    SortExpression="uf" />
                <asp:BoundField DataField="Cidade" HeaderText="Cidade" 
                    SortExpression="Cidade" ReadOnly="True" />
                <asp:BoundField DataField="serieEqpto" HeaderText="Série" 
                    SortExpression="serieEqpto" />
                <asp:BoundField DataField="partNumber" HeaderText="P.Number" SortExpression="partNumber" />
                <asp:BoundField DataField="peca" HeaderText="Peça" SortExpression="peca" />
                <asp:BoundField DataField="obs" HeaderText="obs" 
                    SortExpression="obs" Visible="false" />
                <asp:BoundField DataField="solicitante" HeaderText="Solic." 
                    SortExpression="solicitante" />
                <asp:BoundField DataField="status" 
                    HeaderText="Status" SortExpression="status" />
                <asp:BoundField DataField="dtSolicitacao" DataFormatString="{0:d}" HeaderText="Data" SortExpression="dtSolicitacao" />
                <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" ReadOnly="True" SortExpression="qtdDias" />
                <asp:BoundField DataField="Entrega em" DataFormatString="{0:d}" HeaderText="Entrega em" ReadOnly="True" SortExpression="Entrega em" Visible="false" />
                <asp:ImageField DataImageUrlField="S" DataImageUrlFormatString="~\Imagens\{0}" HeaderText="S">
                    <ControlStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:ImageField>
            </Columns>
            <%--<EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
        </asp:GridView>
        <asp:SqlDataSource ID="Pecas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select  a.idreqPeca, c.cliente, a.reqUSD, 
b.uf, b.cidade 'Cidade',
a.serieEqpto,a.partNumber,a.peca, a.obs,a.solicitante,a.status, a.dtSolicitacao, 
datediff(day,a.dtSolicitacao,getdate()) 'qtdDias' , 
case when a.status = 'Comprada' then (select top 1 dtPrevisaoEntrega from atulPecas where idreqpeca = a.idreqpeca and status = 'Comprada' order by dtCriacao desc) else null end as 'Entrega em' ,
case when eqpParado = 'Sim' Then 'ruim.jpg' else 'atenção.jpg' end as S
from reqPecas as a 
left join equipamentos as b on ltrim(rtrim(a.serieEqpto)) = b.serie
left join clientes as c on b.idcliente = c.idcliente
where a.status not in ('Atendido', 'Cancelado') and b.status = 1
order by qtdDias desc" >
        </asp:SqlDataSource>

</asp:Content>

