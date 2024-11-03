<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Postagens Reversas.aspx.cs" Inherits="Correios_Postagens_Reversas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="gvReverso" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idPostagemReversa" DataSourceID="dsReverso" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="idPostagemReversa" HeaderText="idPostagemReversa" InsertVisible="False" ReadOnly="True" SortExpression="idPostagemReversa" />
            <asp:BoundField DataField="cepOrigem" HeaderText="cepOrigem" SortExpression="cepOrigem" />
            <asp:BoundField DataField="cepDestino" HeaderText="cepDestino" SortExpression="cepDestino" />
            <asp:BoundField DataField="postagem" HeaderText="postagem" SortExpression="postagem" />
            <asp:BoundField DataField="operador" HeaderText="operador" SortExpression="operador" />
            <asp:BoundField DataField="dtEnvio" HeaderText="dtEnvio" SortExpression="dtEnvio" />
            <asp:BoundField DataField="dtEntrega" HeaderText="dtEntrega" SortExpression="dtEntrega" />
            <asp:BoundField DataField="prazoEntrega" HeaderText="prazoEntrega" SortExpression="prazoEntrega" />
            <asp:BoundField DataField="entregueEm" HeaderText="entregueEm" ReadOnly="True" SortExpression="entregueEm" />
            <asp:BoundField DataField="statusentrega" HeaderText="statusentrega" SortExpression="statusentrega" />
            <asp:BoundField DataField="protocolo" HeaderText="protocolo" SortExpression="protocolo" />
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
    <asp:SqlDataSource ID="dsReverso" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT [idPostagemReversa], [cepOrigem], [cepDestino], [postagem], [operador], [dtEnvio], [dtEntrega], [prazoEntrega], [entregueEm], [statusentrega], [protocolo] FROM [postagensReversas] WHERE ([verificado] = @verificado)" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [postagensReversas] WHERE [idPostagemReversa] = @original_idPostagemReversa AND (([cepOrigem] = @original_cepOrigem) OR ([cepOrigem] IS NULL AND @original_cepOrigem IS NULL)) AND (([cepDestino] = @original_cepDestino) OR ([cepDestino] IS NULL AND @original_cepDestino IS NULL)) AND (([postagem] = @original_postagem) OR ([postagem] IS NULL AND @original_postagem IS NULL)) AND (([operador] = @original_operador) OR ([operador] IS NULL AND @original_operador IS NULL)) AND (([dtEnvio] = @original_dtEnvio) OR ([dtEnvio] IS NULL AND @original_dtEnvio IS NULL)) AND (([dtEntrega] = @original_dtEntrega) OR ([dtEntrega] IS NULL AND @original_dtEntrega IS NULL)) AND (([prazoEntrega] = @original_prazoEntrega) OR ([prazoEntrega] IS NULL AND @original_prazoEntrega IS NULL)) AND (([entregueEm] = @original_entregueEm) OR ([entregueEm] IS NULL AND @original_entregueEm IS NULL)) AND (([statusentrega] = @original_statusentrega) OR ([statusentrega] IS NULL AND @original_statusentrega IS NULL)) AND (([protocolo] = @original_protocolo) OR ([protocolo] IS NULL AND @original_protocolo IS NULL))" InsertCommand="INSERT INTO [postagensReversas] ([cepOrigem], [cepDestino], [postagem], [operador], [dtEnvio], [dtEntrega], [prazoEntrega], [entregueEm], [statusentrega], [protocolo]) VALUES (@cepOrigem, @cepDestino, @postagem, @operador, @dtEnvio, @dtEntrega, @prazoEntrega, @entregueEm, @statusentrega, @protocolo)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [postagensReversas] SET [cepOrigem] = @cepOrigem, [cepDestino] = @cepDestino, [postagem] = @postagem, [operador] = @operador, [dtEnvio] = @dtEnvio, [dtEntrega] = @dtEntrega, [prazoEntrega] = @prazoEntrega, [entregueEm] = @entregueEm, [statusentrega] = @statusentrega, [protocolo] = @protocolo WHERE [idPostagemReversa] = @original_idPostagemReversa AND (([cepOrigem] = @original_cepOrigem) OR ([cepOrigem] IS NULL AND @original_cepOrigem IS NULL)) AND (([cepDestino] = @original_cepDestino) OR ([cepDestino] IS NULL AND @original_cepDestino IS NULL)) AND (([postagem] = @original_postagem) OR ([postagem] IS NULL AND @original_postagem IS NULL)) AND (([operador] = @original_operador) OR ([operador] IS NULL AND @original_operador IS NULL)) AND (([dtEnvio] = @original_dtEnvio) OR ([dtEnvio] IS NULL AND @original_dtEnvio IS NULL)) AND (([dtEntrega] = @original_dtEntrega) OR ([dtEntrega] IS NULL AND @original_dtEntrega IS NULL)) AND (([prazoEntrega] = @original_prazoEntrega) OR ([prazoEntrega] IS NULL AND @original_prazoEntrega IS NULL)) AND (([entregueEm] = @original_entregueEm) OR ([entregueEm] IS NULL AND @original_entregueEm IS NULL)) AND (([statusentrega] = @original_statusentrega) OR ([statusentrega] IS NULL AND @original_statusentrega IS NULL)) AND (([protocolo] = @original_protocolo) OR ([protocolo] IS NULL AND @original_protocolo IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_idPostagemReversa" Type="Int32" />
            <asp:Parameter Name="original_cepOrigem" Type="String" />
            <asp:Parameter Name="original_cepDestino" Type="String" />
            <asp:Parameter Name="original_postagem" Type="String" />
            <asp:Parameter Name="original_operador" Type="String" />
            <asp:Parameter Name="original_dtEnvio" Type="DateTime" />
            <asp:Parameter Name="original_dtEntrega" Type="DateTime" />
            <asp:Parameter Name="original_prazoEntrega" Type="Int32" />
            <asp:Parameter Name="original_entregueEm" Type="Int32" />
            <asp:Parameter Name="original_statusentrega" Type="String" />
            <asp:Parameter Name="original_protocolo" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="cepOrigem" Type="String" />
            <asp:Parameter Name="cepDestino" Type="String" />
            <asp:Parameter Name="postagem" Type="String" />
            <asp:Parameter Name="operador" Type="String" />
            <asp:Parameter Name="dtEnvio" Type="DateTime" />
            <asp:Parameter Name="dtEntrega" Type="DateTime" />
            <asp:Parameter Name="prazoEntrega" Type="Int32" />
            <asp:Parameter Name="entregueEm" Type="Int32" />
            <asp:Parameter Name="statusentrega" Type="String" />
            <asp:Parameter Name="protocolo" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="verificado" Type="Boolean" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="cepOrigem" Type="String" />
            <asp:Parameter Name="cepDestino" Type="String" />
            <asp:Parameter Name="postagem" Type="String" />
            <asp:Parameter Name="operador" Type="String" />
            <asp:Parameter Name="dtEnvio" Type="DateTime" />
            <asp:Parameter Name="dtEntrega" Type="DateTime" />
            <asp:Parameter Name="prazoEntrega" Type="Int32" />
            <asp:Parameter Name="entregueEm" Type="Int32" />
            <asp:Parameter Name="statusentrega" Type="String" />
            <asp:Parameter Name="protocolo" Type="String" />
            <asp:Parameter Name="original_idPostagemReversa" Type="Int32" />
            <asp:Parameter Name="original_cepOrigem" Type="String" />
            <asp:Parameter Name="original_cepDestino" Type="String" />
            <asp:Parameter Name="original_postagem" Type="String" />
            <asp:Parameter Name="original_operador" Type="String" />
            <asp:Parameter Name="original_dtEnvio" Type="DateTime" />
            <asp:Parameter Name="original_dtEntrega" Type="DateTime" />
            <asp:Parameter Name="original_prazoEntrega" Type="Int32" />
            <asp:Parameter Name="original_entregueEm" Type="Int32" />
            <asp:Parameter Name="original_statusentrega" Type="String" />
            <asp:Parameter Name="original_protocolo" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

