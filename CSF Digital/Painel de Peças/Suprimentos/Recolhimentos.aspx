<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Recolhimentos.aspx.cs" Inherits="Suprimentos_Recolhimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:Label ID="lbUsername" runat="server" Visible ="false"/>

    <table>
        <tr>
            <td style="vertical-align:text-top; width:400px">
                <h3>Quantidade de Chamados por Responsável</h3>
                <asp:GridView ID="gvResponsavel" runat="server" DataSourceID="dsResponsavel" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Responsável" HeaderText="Responsável" SortExpression="Responsável" ReadOnly="True" />
                        <asp:BoundField DataField="Qtd" HeaderText="Qtd" ReadOnly="True" SortExpression="Qtd" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="dsStatus" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>"
                     SelectCommand="select Status, count(*) 'Qtd' from controlerecolhimento group by status">
                </asp:SqlDataSource> 
            </td>
            <td style="vertical-align:text-top">
                <h3>Quantidade de Chamados por Status</h3>
                <asp:GridView ID="gvStatus" runat="server" DataSourceID="dsStatus" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                        <asp:BoundField DataField="Qtd" HeaderText="Qtd" ReadOnly="True" SortExpression="Qtd" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="dsResponsavel" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>"
                     SelectCommand="select case when responsavel is null then 'Disponível' else responsavel end as 'Responsável', count(*) 'Qtd' from controlerecolhimento WHERE status <> 'Entregue' group by responsavel">
                </asp:SqlDataSource> 

            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btChamadosUsuario" Text="Meus Recolhimentos" runat="server" OnClick="btChamadosUsuario_Click" /><br />
    <br />
    <asp:MultiView ID="mvRecolhimentos" runat="server">
        <asp:View ID="vwGeral" runat="server" EnableViewState="true">
            <asp:GridView ID="gvRecolhimentos" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idRecolhimento" DataSourceID="dsRecolhimentos" ForeColor="#333333" GridLines="None" OnRowCommand="gvRecolhimentos_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField><ItemTemplate><asp:Button runat="server" ID="btAtender" 
                        Text="Atender" CommandName="Atender" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" /></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="idRecolhimento" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="idRecolhimento" />
            <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="UF" />
            <asp:BoundField DataField="cidade" HeaderText="cidade" SortExpression="Cidade" />
            <asp:BoundField DataField="unidade" HeaderText="unidade" SortExpression="Unidade" />
            <asp:BoundField DataField="ambiente" HeaderText="ambiente" SortExpression="Ambiente" />
            <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="Série" />
            <asp:BoundField DataField="suprimento" HeaderText="suprimento" SortExpression="Suprimento" />
            <asp:BoundField DataField="serialSuprimento" HeaderText="serialSuprimento" SortExpression="Serial" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="Status" />
            <asp:BoundField DataField="protocolo" HeaderText="protocolo" SortExpression="Protocolo" />
            <asp:BoundField DataField="postagem" HeaderText="postagem" SortExpression="Postagem" />
            <asp:BoundField DataField="dtAbertura" HeaderText="dtAbertura" SortExpression="Data" DataFormatString="{0:d}" />
            <asp:BoundField DataField="responsavel" HeaderText="responsavel" SortExpression="Responsável" Visible="false" />
            
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
        </asp:View>
        <asp:View ID="vwUser" runat="server" EnableViewState="false">
            <asp:GridView ID="gvRecolhimentosUsuario" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idRecolhimento" DataSourceID="dsRecolhimentosUsuario" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="idRecolhimento" HeaderText="idRecolhimento" InsertVisible="False" ReadOnly="True" SortExpression="idRecolhimento" />
                    <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                    <asp:BoundField DataField="cidade" HeaderText="cidade" SortExpression="cidade" />
                    <asp:BoundField DataField="unidade" HeaderText="unidade" SortExpression="unidade" />
                    <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
                    <asp:BoundField DataField="suprimento" HeaderText="suprimento" SortExpression="suprimento" />
                    <asp:BoundField DataField="serialSuprimento" HeaderText="serialSuprimento" SortExpression="serialSuprimento" />
                    <asp:BoundField DataField="protocolo" HeaderText="protocolo" SortExpression="protocolo" />
                    <asp:BoundField DataField="postagem" HeaderText="postagem" SortExpression="postagem" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:SqlDataSource ID="dsRecolhimentosUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                DeleteCommand="DELETE FROM [controleRecolhimento] WHERE [idRecolhimento] = @idRecolhimento" 
                InsertCommand="INSERT INTO [controleRecolhimento] ([uf], [cidade], [unidade], [serie], [suprimento], [serialSuprimento], [protocolo], [postagem]) VALUES (@uf, @cidade, @unidade, @serie, @suprimento, @serialSuprimento, @protocolo, @postagem)" 
                SelectCommand="SELECT [idRecolhimento], [uf], [cidade], [unidade], [serie], [suprimento], [serialSuprimento], [protocolo], [postagem] FROM [controleRecolhimento] WHERE ([responsavel] = @responsavel)" 
                UpdateCommand="UPDATE [controleRecolhimento] SET [uf] = @uf, [cidade] = @cidade, [unidade] = @unidade, [serie] = @serie, [suprimento] = @suprimento, [serialSuprimento] = @serialSuprimento, [protocolo] = @protocolo, [postagem] = @postagem WHERE [idRecolhimento] = @idRecolhimento">
                <DeleteParameters>
                    <asp:Parameter Name="idRecolhimento" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="uf" Type="String" />
                    <asp:Parameter Name="cidade" Type="String" />
                    <asp:Parameter Name="unidade" Type="String" />
                    <asp:Parameter Name="serie" Type="String" />
                    <asp:Parameter Name="suprimento" Type="String" />
                    <asp:Parameter Name="serialSuprimento" Type="String" />
                    <asp:Parameter Name="protocolo" Type="String" />
                    <asp:Parameter Name="postagem" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="lbUsername" Name="responsavel" PropertyName="Text" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="uf" Type="String" />
                    <asp:Parameter Name="cidade" Type="String" />
                    <asp:Parameter Name="unidade" Type="String" />
                    <asp:Parameter Name="serie" Type="String" />
                    <asp:Parameter Name="suprimento" Type="String" />
                    <asp:Parameter Name="serialSuprimento" Type="String" />
                    <asp:Parameter Name="protocolo" Type="String" />
                    <asp:Parameter Name="postagem" Type="String" />
                    <asp:Parameter Name="idRecolhimento" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </asp:View>

    </asp:MultiView>

    <asp:SqlDataSource ID="dsRecolhimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        DeleteCommand="DELETE FROM [controleRecolhimento] WHERE [idRecolhimento] = @idRecolhimento" 
        InsertCommand="INSERT INTO [controleRecolhimento] ([uf], [cidade], [unidade], [ambiente], [serie], [suprimento], [serialSuprimento], [status], [protocolo], [postagem], [dtAbertura], [dtSolicitacao], [dtEntrega], [responsavel]) VALUES (@uf, @cidade, @unidade, @ambiente, @serie, @suprimento, @serialSuprimento, @status, @protocolo, @postagem, @dtAbertura, @dtSolicitacao, @dtEntrega, @responsavel)" 
        SelectCommand="SELECT * FROM [controleRecolhimento] WHERE status <> 'Entregue' and responsavel is null" 
        UpdateCommand="UPDATE [controleRecolhimento] SET [uf] = @uf, [cidade] = @cidade, [unidade] = @unidade, [ambiente] = @ambiente, [serie] = @serie, [suprimento] = @suprimento, [serialSuprimento] = @serialSuprimento, [status] = @status, [protocolo] = @protocolo, [postagem] = @postagem, [dtAbertura] = @dtAbertura, [dtSolicitacao] = @dtSolicitacao, [dtEntrega] = @dtEntrega, [responsavel] = @responsavel WHERE [idRecolhimento] = @idRecolhimento">
        <DeleteParameters>
            <asp:Parameter Name="idRecolhimento" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="uf" Type="String" />
            <asp:Parameter Name="cidade" Type="String" />
            <asp:Parameter Name="unidade" Type="String" />
            <asp:Parameter Name="ambiente" Type="String" />
            <asp:Parameter Name="serie" Type="String" />
            <asp:Parameter Name="suprimento" Type="String" />
            <asp:Parameter Name="serialSuprimento" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="protocolo" Type="String" />
            <asp:Parameter Name="postagem" Type="String" />
            <asp:Parameter Name="dtAbertura" Type="DateTime" />
            <asp:Parameter Name="dtSolicitacao" Type="DateTime" />
            <asp:Parameter Name="dtEntrega" Type="DateTime" />
            <asp:Parameter Name="responsavel" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="Entregue" Name="status" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="uf" Type="String" />
            <asp:Parameter Name="cidade" Type="String" />
            <asp:Parameter Name="unidade" Type="String" />
            <asp:Parameter Name="ambiente" Type="String" />
            <asp:Parameter Name="serie" Type="String" />
            <asp:Parameter Name="suprimento" Type="String" />
            <asp:Parameter Name="serialSuprimento" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="protocolo" Type="String" />
            <asp:Parameter Name="postagem" Type="String" />
            <asp:Parameter Name="dtAbertura" Type="DateTime" />
            <asp:Parameter Name="dtSolicitacao" Type="DateTime" />
            <asp:Parameter Name="dtEntrega" Type="DateTime" />
            <asp:Parameter Name="responsavel" Type="String" />
            <asp:Parameter Name="idRecolhimento" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>

