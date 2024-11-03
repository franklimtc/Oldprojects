<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="editar.aspx.cs" Inherits="Atendimentos_new_editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:TextBox runat="server" ID="tbidreq" Visible="false"></asp:TextBox>
    <br />
    <asp:GridView runat="server" ID="gvreqAtendimento" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idReqAtendimento" DataSourceID="dsreqAtendimentos" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="idReqAtendimento" HeaderText="idReqAtendimento" InsertVisible="False" ReadOnly="True" SortExpression="idReqAtendimento" />
            <asp:BoundField DataField="idCliente" HeaderText="idCliente" SortExpression="idCliente" />
            <asp:BoundField DataField="req" HeaderText="req" SortExpression="req" />
            <asp:BoundField DataField="endereco" HeaderText="endereco" SortExpression="endereco" />
            <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
            <asp:BoundField DataField="cliente" HeaderText="cliente" SortExpression="cliente" />
            <asp:BoundField DataField="telefone" HeaderText="telefone" SortExpression="telefone" />
            <asp:BoundField DataField="falha" HeaderText="falha" SortExpression="falha" />
            <asp:BoundField DataField="solicitante" HeaderText="solicitante" SortExpression="solicitante" />
            <asp:BoundField DataField="tipo" HeaderText="tipo" SortExpression="tipo" />
            <asp:BoundField DataField="tecnico" HeaderText="tecnico" SortExpression="tecnico" />
            <asp:BoundField DataField="emailTecnico" HeaderText="emailTecnico" SortExpression="emailTecnico" />
            <asp:BoundField DataField="dtAbertura" DataFormatString="{0:d}" HeaderText="dtAbertura" SortExpression="dtAbertura" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
            <asp:CheckBoxField DataField="emailEnviado" HeaderText="emailEnviado" SortExpression="emailEnviado" />
            <asp:CheckBoxField DataField="eqptoParado" HeaderText="eqptoParado" SortExpression="eqptoParado" />
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
    <br />
    <asp:Button runat="server" ID="tbVoltar" Text="Voltar" Width="90px" OnClick="tbVoltar_Click"/>
    <asp:SqlDataSource ID="dsreqAtendimentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" DeleteCommand="DELETE FROM [reqAtendimentos] WHERE [idReqAtendimento] = @idReqAtendimento" InsertCommand="INSERT INTO [reqAtendimentos] ([idCliente], [req], [endereco], [serie], [cliente], [telefone], [falha], [solicitante], [tipo], [tecnico], [emailTecnico], [dtAbertura], [dtFechamento], [status], [emailEnviado], [dtAtual], [eqptoParado]) VALUES (@idCliente, @req, @endereco, @serie, @cliente, @telefone, @falha, @solicitante, @tipo, @tecnico, @emailTecnico, @dtAbertura, @dtFechamento, @status, @emailEnviado, @dtAtual, @eqptoParado)" SelectCommand="SELECT * FROM [reqAtendimentos] WHERE ([idReqAtendimento] = @idReqAtendimento)" UpdateCommand="UPDATE [reqAtendimentos] SET [idCliente] = @idCliente, [req] = @req, [endereco] = @endereco, [serie] = @serie, [cliente] = @cliente, [telefone] = @telefone, [falha] = @falha, [solicitante] = @solicitante, [tipo] = @tipo, [tecnico] = @tecnico, [emailTecnico] = @emailTecnico, [dtAbertura] = @dtAbertura, [dtFechamento] = @dtFechamento, [status] = @status, [emailEnviado] = @emailEnviado, [dtAtual] = @dtAtual, [eqptoParado] = @eqptoParado WHERE [idReqAtendimento] = @idReqAtendimento">
        <DeleteParameters>
            <asp:Parameter Name="idReqAtendimento" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="idCliente" Type="Int32" />
            <asp:Parameter Name="req" Type="String" />
            <asp:Parameter Name="endereco" Type="String" />
            <asp:Parameter Name="serie" Type="String" />
            <asp:Parameter Name="cliente" Type="String" />
            <asp:Parameter Name="telefone" Type="String" />
            <asp:Parameter Name="falha" Type="String" />
            <asp:Parameter Name="solicitante" Type="String" />
            <asp:Parameter Name="tipo" Type="Int16" />
            <asp:Parameter Name="tecnico" Type="String" />
            <asp:Parameter Name="emailTecnico" Type="String" />
            <asp:Parameter Name="dtAbertura" Type="DateTime" />
            <asp:Parameter Name="dtFechamento" Type="DateTime" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="emailEnviado" Type="Boolean" />
            <asp:Parameter Name="dtAtual" Type="DateTime" />
            <asp:Parameter Name="eqptoParado" Type="Boolean" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="tbidreq" Name="idReqAtendimento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="idCliente" Type="Int32" />
            <asp:Parameter Name="req" Type="String" />
            <asp:Parameter Name="endereco" Type="String" />
            <asp:Parameter Name="serie" Type="String" />
            <asp:Parameter Name="cliente" Type="String" />
            <asp:Parameter Name="telefone" Type="String" />
            <asp:Parameter Name="falha" Type="String" />
            <asp:Parameter Name="solicitante" Type="String" />
            <asp:Parameter Name="tipo" Type="Int16" />
            <asp:Parameter Name="tecnico" Type="String" />
            <asp:Parameter Name="emailTecnico" Type="String" />
            <asp:Parameter Name="dtAbertura" Type="DateTime" />
            <asp:Parameter Name="dtFechamento" Type="DateTime" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="emailEnviado" Type="Boolean" />
            <asp:Parameter Name="dtAtual" Type="DateTime" />
            <asp:Parameter Name="eqptoParado" Type="Boolean" />
            <asp:Parameter Name="idReqAtendimento" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

