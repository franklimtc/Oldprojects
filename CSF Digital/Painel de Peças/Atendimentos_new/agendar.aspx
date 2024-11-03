<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="agendar.aspx.cs" Inherits="Atendimentos_new_agendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:TextBox runat="server" ID ="tbidReq" Visible ="false"></asp:TextBox><p></p>
    <br />
    <asp:GridView runat="server" ID="gvreqAtendimento" DataSourceID="dsReqAtendimento" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idReqAtendimento" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
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
    <asp:SqlDataSource ID="dsReqAtendimento" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT * FROM [reqAtendimentos] WHERE ([idReqAtendimento] = @idReqAtendimento)">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbidReq" Name="idReqAtendimento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <fieldset style="width:250px">
        <legend>Agendar</legend>
        <asp:Calendar runat="server" ID="calendarAgenda"></asp:Calendar>
        <br />
        <asp:Button runat="server" ID="tbVoltar" Text="Voltar" Width="90px" OnClick="tbVoltar_Click"/>
        &nbsp;
        <asp:Button runat="server" ID="tbAgendar" Text="Agendar" Width="90px" OnClick="tbAgendar_Click" />
    </fieldset>
    <h2>Histórico de agendamento</h2>
    <asp:GridView ID="gvHistoricoAgendamento" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idPrevisao" DataSourceID="dsHistoricoAgendamento" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="idPrevisao" HeaderText="idPrevisao" InsertVisible="False" ReadOnly="True" SortExpression="idPrevisao" />
            <asp:BoundField DataField="idReqAtendimento" HeaderText="idReqAtendimento" SortExpression="idReqAtendimento" />
            <asp:BoundField DataField="dtPrevisao" DataFormatString="{0:d}" HeaderText="dtPrevisao" SortExpression="dtPrevisao" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
            <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" SortExpression="data" />
            <asp:BoundField DataField="obs" HeaderText="obs" SortExpression="obs" />
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

    <asp:SqlDataSource ID="dsHistoricoAgendamento" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT * FROM [reqAtendimentosPrevisao] WHERE ([idReqAtendimento] = @idReqAtendimento)">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbidReq" Name="idReqAtendimento" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

