<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pesquisa.aspx.cs" Inherits="Relatórios_Pesquisa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Histórico de Envio de Peças
    </h2>
    <p></p>
    <div>
    <table>
        <tr>
            <td>UF:</td>
            <td><asp:TextBox runat="server" ID="tbUf"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pUF" Text="Pesquisar" Visible="false" /></td>
            <td>Cidade: </td>
            <td><asp:TextBox ID="tbCidade" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pCidade" Text="Pesquisar" Visible="false" /></td>
        </tr>
        <tr>
            <td>Série: </td>
            <td><asp:TextBox ID="tbSerie" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pSerie" Text="Pesquisar" Visible="false" /></td>
            <td>Requisição USD:</td>
            <td><asp:TextBox ID="tbUsd" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pUsd" Text="Pesquisar" Visible="false" /></td>
        </tr>
        <tr>
            <td>Part Number: </td>
            <td><asp:TextBox ID="tbParNumber" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pPartNumber" Text="Pesquisar" Visible="false" /></td>
            <td>Solicitante: </td>
            <td><asp:TextBox ID="tbSolicitante" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pSolicitante" Text="Pesquisar" Visible="false" /></td>
        </tr>
        <tr>
            <td>Status: </td>
            <td><asp:TextBox ID="tbStatus" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pStatus" Text="Pesquisar" Visible="false" /></td>
            <td>Postagem: </td>
            <td><asp:TextBox ID="tbPostagem" runat="server"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="pPostagem" Text="Pesquisar" Visible="false" /></td>
        </tr>
        <tr>
            <td>Data Inicial: </td>
            <td><asp:TextBox ID="tbdtInicial" runat="server" TextMode="SingleLine"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="Button1" Text="Pesquisar" Visible="false" /></td>
            <td>Data Final: </td>
            <td><asp:TextBox ID="tbdtFinal" runat="server" TextMode="SingleLine"></asp:TextBox></td>
            <td><asp:Button runat="server" ID="Button2" Text="Pesquisar" Visible="false" /></td>
        </tr>

    </table>
    <asp:Button runat="server" ID="btPesquisa" Text="Pesquisar" 
            onclick="btPesquisa_Click" />
    </div>
    <div>
        <asp:GridView runat="server" ID="gvPesquisa" DataSourceID="dsPesquisa" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" 
            ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID" />
                <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                <asp:BoundField DataField="Cidade" HeaderText="Cidade" ReadOnly="True" 
                    SortExpression="Cidade" />
                <asp:BoundField DataField="Série" HeaderText="Série" SortExpression="Série" />
                <asp:BoundField DataField="USD" HeaderText="USD" SortExpression="USD" />
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" 
                    SortExpression="PartNumber" />
                <asp:BoundField DataField="Peça" HeaderText="Peça" SortExpression="Peça" />
                <asp:BoundField DataField="Qtd" HeaderText="Qtd" SortExpression="Qtd" />
                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" 
                    SortExpression="Solicitante" />
                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" 
                    SortExpression="Data" />
                <asp:BoundField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />
                <asp:BoundField DataField="Postagem" HeaderText="Postagem" ReadOnly="True"
                    SortExpression="Postagem" />
                <asp:TemplateField ItemStyle-HorizontalAlign = "Center">
                    <%--<ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                    </ItemTemplate>--%>
                </asp:TemplateField>
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
    </div>
    <asp:SqlDataSource ID="dsPesquisa" runat="server"
         ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>">
        </asp:SqlDataSource>
</asp:Content>
