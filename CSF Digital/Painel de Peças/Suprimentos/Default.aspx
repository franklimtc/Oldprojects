<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Suprimentos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="~/Content/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Resumo</h2>
    <br />
    <asp:GridView ID="gvResumo" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsResumoSuprimentos" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Suprimento" HeaderText="Suprimento" SortExpression="Suprimento" />
            <asp:BoundField DataField="QTD" HeaderText="QTD" ReadOnly="True" SortExpression="QTD" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>

    <asp:SqlDataSource ID="dsResumoSuprimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT Suprimento, count(*) 'QTD' FROM [vw_reqSuprimentosDatalhado] GROUP BY suprimento"></asp:SqlDataSource>

    <br />
    <h2>Suprimentos Solicitados</h2>
    <br />
    <asp:GridView ID="gvSuprimentosDetalhado" runat="server" AutoGenerateColumns="False" DataKeyNames="idreqSuprimento" CssClass="table table-striped table-bordered table-condensed table-hover" DataSourceID="dsSuprimentosDetalhado" CellPadding="4" OnRowCommand="gvSuprimentosDetalhado_RowCommand">
        <Columns>
            <asp:BoundField DataField="idreqSuprimento" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="idreqSuprimento" />
            <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
            <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
            <asp:BoundField DataField="endereco" HeaderText="Endereço" SortExpression="endereco" />
            <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
            <asp:BoundField DataField="suprimento" HeaderText="Supr" SortExpression="suprimento" />
            <asp:BoundField DataField="dataSolicitacao" HeaderText="Data" SortExpression="dataSolicitacao" DataFormatString="{0:d}" />
            <asp:BoundField DataField="DuracaoEstimada" HeaderText="Duração" ReadOnly="True" SortExpression="DuracaoEstimada" />
            <asp:BoundField DataField="Operador" HeaderText="Operador" ReadOnly="True" SortExpression="Operador" />
            <%--<asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />--%>
            <%--<asp:BoundField DataField="Fone" HeaderText="Fone" SortExpression="Fone" />--%>
            <%--<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />--%>
            <asp:BoundField DataField="USD" HeaderText="USD" SortExpression="USD" />
            <asp:BoundField DataField="Falha" HeaderText="F" ReadOnly="True" SortExpression="Falha"/>
            <asp:BoundField DataField="priorizar" HeaderText="P" ReadOnly="True" SortExpression="priorizar" />
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btPrint" 
                        Text="Print" CommandName="Print" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btEditar" 
                        Text="Edit" CommandName="Editar" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsSuprimentosDetalhado" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="SELECT * FROM [vw_reqSuprimentosDatalhado] order by priorizar desc, Falha desc, DuracaoEstimada, dataSolicitacao">

    </asp:SqlDataSource>
</asp:Content>

