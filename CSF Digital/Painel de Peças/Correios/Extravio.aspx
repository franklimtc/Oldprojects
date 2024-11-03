<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Extravio.aspx.cs" Inherits="Correios_Extravio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Registro de Extravios - Correios</h1>
    <br />
    <table>
        <tr>
            <td>Postagem:</td>
            <td><asp:TextBox ID="tbPostagem" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="btPostagem" runat="server" Text="Buscar" OnClick="btPostagem_Click" /></td>
            <td></td>
        </tr>
    </table>

    <fieldset>
        <legend>Dados da Postagem</legend>
        <asp:GridView ID="gvPostagem" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsPostagem" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                <asp:BoundField DataField="cidade" HeaderText="cidade" SortExpression="cidade" />
                <asp:BoundField DataField="endereco" HeaderText="endereco" SortExpression="endereco" />
                <asp:BoundField DataField="bairro" HeaderText="bairro" SortExpression="bairro" />
                <asp:BoundField DataField="cep" HeaderText="cep" SortExpression="cep" />
                <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
                <asp:BoundField DataField="postagem" HeaderText="postagem" SortExpression="postagem" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:SqlDataSource ID="dsPostagem" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select b.uf, b.cidade, b.endereco, b.bairro, b.cep, a.serie, a.postagem 
from enviosSuprimentos as a 
left join equipamentos as b on a.serie = b.serie
where a.postagem =  null"></asp:SqlDataSource>
    </fieldset>

    <fieldset>
        <legend>Registrar Reclamação</legend>
        <table>
            <tr>
                <th>Postagem</th>
                <th>Protocolo</th>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbPostagemNova" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="tbProtocoloNovo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Button ID="btInserirChamado" runat="server" Text="Inserir" OnClick="btInserirChamado_Click" /></td>
            </tr>
        </table>
    </fieldset>

     <h2>CSF DIGITAL</h2>
            <h2></h2>
            <p>Rua Raimundo Oliveira Filho, nº 332, Papicu, Fortaleza-CE.</p>
            <p>CEP: 60175-175 - Fone: (85) 3022-0900</p>
            <p> CNPJ: 08953969000199</p>
        <h2>Sites Auxiliares</h2>
            <p><a href="http://www2.correios.com.br/sistemas/falecomoscorreios/">Fale com os Correios</a></p>
            <p><a href="http://www2.correios.com.br/sistemas/precosPrazos/">Cálculo de preços e prazos de entrega</a></p>
            <p><a href="http://www2.correios.com.br/sistemas/rastreamento/">Rastreamento de objetos</a></p>
    
</asp:Content> 

