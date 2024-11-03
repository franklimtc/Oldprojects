<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Requisicoes_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Requisições</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="btNova" runat="server" Text="Nova requisição" OnClick="btNova_Click" />
    <asp:Panel ID="pnReq" runat="server" Visible="false" BackColor="Wheat">
        <h2>Requisição <asp:Label ID="lbReq" runat="server"></asp:Label></h2>
        <table>
            <tr>
                <td>Requisição:</td>
                <td><asp:TextBox ID="tbcodReqFinal" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
                <td>Equipamento:</td>
                <td><asp:TextBox ID="tbEqpto" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Status:</td>
                <td><asp:TextBox ID="tbStatus" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
                <td>Última Modificação:</td>
                <td><asp:TextBox ID="tbUltimaModificacao" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Categoria:</td>
                <td colspan="3">
                    <asp:TextBox ID="tbCategoria" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Resumo:</td>
                <td colspan="3"><asp:TextBox ID="tbResumo" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Descrição:</td>
                <td colspan="3"><asp:TextBox ID="tbDescricao" runat="server" TextMode="MultiLine" Width="100%" Height="85px" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvLogs" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsLogs" ForeColor="Black" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" Visible="false" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                            <asp:BoundField DataField="Descricao" HeaderText="Descricao" SortExpression="Descricao" />
                            <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="dsLogs" runat="server" SelectMethod="Listar" TypeName="LogReq">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="tbcodReqFinal" Name="ReqCode" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btComentario" runat="server" Text="Comentar" Width="85px" OnClick="btComentario_Click" />
                    <asp:Button ID="btencerrar" runat="server" Text="Encerrar" Width="85px" OnClick="btencerrar_Click" />
                    <asp:Button ID="btFechar" runat="server" Text="Sair"  Width="85px" OnClick="btFechar_Click" />

                    <asp:Panel ID="pnComentario" runat="server" Visible ="false">
                        <h3>Adicione o comentáiro aqui!</h3>
                        <asp:TextBox ID="tbNovoComentario" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        <asp:Button ID="btSalvarComentario" runat="server" Text="Salvar" OnClick="btSalvarComentario_Click" />
                    </asp:Panel>
                </td>
            </tr>
        
        </table>
    </asp:Panel>
    <asp:Panel ID="pnEncerrar" runat="server" Visible="false">
                       <h3>Tem certeza que deseja encerrar a requisição <asp:Label ID="lbReq1" runat="server"></asp:Label> ?</h3>
                        <asp:Button ID="btSimEncerra" runat="server" Text="Sim"  Width="85px" OnClick="btSimEncerra_Click" />
                        <asp:Button ID="btNaoEncerra" runat="server" Text="Não"  Width="85px" OnClick="btFechar_Click" />
                    </asp:Panel>
    <br />
    <h2>Lista de Requisições Abertas</h2>
    <asp:GridView ID="gvRequisicoes" runat="server" AutoGenerateColumns="False" BackColor="White" Width="100%" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="dsRequisicoes" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="gvRequisicoes_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="Abrir" />
            <asp:BoundField DataField="Idrequisicao" HeaderText="Idrequisicao" SortExpression="Idrequisicao" Visible="false" />
            <asp:BoundField DataField="CodReq" HeaderText="Código" SortExpression="CodReq" />
            <asp:BoundField DataField="Serie" HeaderText="Série" SortExpression="Serie" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categoria" />
            <asp:BoundField DataField="Resumo" HeaderText="Resumo" SortExpression="Resumo" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao"  Visible="false"/>
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="DtAbertura" HeaderText="Abertura" SortExpression="DtAbertura" Visible="false" />
            <asp:BoundField DataField="DtFechamento" HeaderText="DtFechamento" SortExpression="DtFechamento"  Visible="false"/>
            <asp:BoundField DataField="DtModificacao" HeaderText="Última Modificação" SortExpression="DtModificacao"  Visible="true"/>
            <asp:BoundField DataField="AbertorPor" HeaderText="AbertorPor" SortExpression="AbertorPor" Visible="false" />
            <asp:BoundField DataField="ModificadoPor" HeaderText="ModificadoPor" SortExpression="ModificadoPor"  Visible="false"/>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente"  Visible="false"/>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <asp:ObjectDataSource ID="dsRequisicoes" runat="server" SelectMethod="Listar" TypeName="Requisicao">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbUsername" Name="UserName" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Panel ID="pnVariaveis" runat="server">
        <asp:TextBox ID="tbUsername" runat="server" Visible="false"></asp:TextBox>
    </asp:Panel>
</asp:Content>

