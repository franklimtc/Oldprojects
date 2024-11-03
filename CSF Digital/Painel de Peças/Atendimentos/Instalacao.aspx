<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Instalacao.aspx.cs" Inherits="Atendimentos_Instalacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
        <div style="text-align:center">
        <h1>Solicitações de Atendimento Técnico</h1>
        <h2>Instalação</h2>
    </div>
    <asp:DropDownList ID="dpClientes" runat="server" DataSourceID="dsClientes" DataTextField="cliente" DataValueField="idCliente" AutoPostBack="True">
    </asp:DropDownList>
    <table>
            <tr>
                <td>Série:</td>
                <td><asp:TextBox ID="tbSerie" runat="server"></asp:TextBox></td>
                <td>Requisição:</td>
                <td><asp:TextBox ID="tbReq" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Contato:</td>
                <td><asp:TextBox ID="tbContato" runat="server"></asp:TextBox></td>
                <td>Telefone:</td>
                <td><asp:TextBox ID="tbFone" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Endereço:</td>
                <td colspan="3"><asp:TextBox ID="tbEndereco" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="height:75px">
                    Falha:<br />
                    <asp:TextBox ID="tbFalha" runat="server" TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>
                </td>
            </tr>
        <tr>
            <td>Técnico:</td>
            <td><asp:TextBox ID="tbTecnico" runat="server"></asp:TextBox></td>
            <td>Email:</td>
            <td><asp:TextBox ID="tbEmailTecnico" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="checkRegistro" runat="server" Text="Registrar Somente?" AutoPostBack="True"/></td>
        </tr>
        <tr>
            <td><asp:Button ID="btSolicitar" runat="server" Text="Solicitar" OnClick="btSolicitar_Click" /></td>
        </tr>
    </table>
    <br />
    <fieldset>
        <legend>Histórico de Solicitações</legend>
        <asp:GridView ID="gvSolicitacoes" runat="server" AutoGenerateColumns="False" DataSourceID="dsSolicitacoes" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="req" HeaderText="req" SortExpression="req" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="falha" HeaderText="falha" SortExpression="falha" >
                <ItemStyle Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="tecnico" HeaderText="tecnico" SortExpression="tecnico" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="dtAbertura" HeaderText="dtAbertura" SortExpression="dtAbertura" DataFormatString="{0:d}" />
                <asp:BoundField DataField="dtFechamento" HeaderText="dtFechamento" SortExpression="dtFechamento" DataFormatString="{0:d}" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
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
        
    </fieldset>

    <%--DataSources--%>
    <asp:SqlDataSource ID="dsSeries" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select id_impr_snmp, numero_serie from impr_snmp where status = 1 and idCliente = @idCliente">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT [idCliente], [cliente] FROM [clientes]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsSolicitacoes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select req, falha, tecnico, dtAbertura, dtFechamento, status from reqAtendimentos where tipo = 0 and status not in ('Cancelado', 'Concluído')">
    </asp:SqlDataSource>
    <%--DataSources--%>

    <%--Validações--%>
    <asp:Label ID="lbMensagem" runat="server" Visible="false"></asp:Label>
    <br /><asp:RequiredFieldValidator ID="rValidatorContato" runat="server" ControlToValidate="tbContato" ErrorMessage="*Insira o nome de uma pessoa de contato." ForeColor="Red"></asp:RequiredFieldValidator>
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbFone" ErrorMessage="*Insira um telefone de contato." ForeColor="Red"></asp:RequiredFieldValidator>    
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEndereco" ErrorMessage="*Insira o endereço do atendimento." ForeColor="Red"></asp:RequiredFieldValidator>    
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbFalha" ErrorMessage="*Informe a falha apresentada pelo equipamento." ForeColor="Red"></asp:RequiredFieldValidator>    
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbEmailTecnico" ErrorMessage="*Informe o email para encaminhar a solicitação." ForeColor="Red"></asp:RequiredFieldValidator>    
    <%--Validações--%>
</asp:Content>

