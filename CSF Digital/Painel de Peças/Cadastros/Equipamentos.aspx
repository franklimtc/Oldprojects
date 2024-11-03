<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Equipamentos.aspx.cs" Inherits="Cadastros_Equipamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Filtros" runat="server">
        <asp:DropDownList ID="dpClientes" runat="server" AutoPostBack="True" DataSourceID="dsClientes" DataTextField="cliente" DataValueField="idCliente" Height="18px" Width="155px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT [idCliente], [cliente] FROM [clientes]"></asp:SqlDataSource>
        <asp:DropDownList ID="dpUf" runat="server" DataSourceID="dsUF" DataTextField="uf" DataValueField="uf" AutoPostBack="True"></asp:DropDownList>
        <br />
        <asp:DropDownList ID="dpCidades" runat="server" DataSourceID="dsCidades" DataTextField="cidade" DataValueField="cidade" AutoPostBack="True"></asp:DropDownList>
        <br />
        <asp:DropDownList ID="dpSeries" runat="server" AutoPostBack="True" DataSourceID="dsSeries" DataTextField="serie" DataValueField="serie"></asp:DropDownList>
        <asp:SqlDataSource ID="dsSeries" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct serie from equipamentos where uf = @uf and cidade = @cidade order by 1">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpUf" Name="uf" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpCidades" Name="cidade" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsCidades" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct cidade from equipamentos where uf = @uf order by 1">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpUf" Name="uf" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsUF" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct uf from equipamentos where idCliente = @idcliente order by 1">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpClientes" Name="idcliente" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label">UF: </asp:Label><asp:TextBox ID="tbUf" runat="server" Width="30px"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="Label">Cidade: </asp:Label><asp:TextBox ID="tbCidade" runat="server" Width="250px"></asp:TextBox>
    <asp:Label ID="Label5" runat="server" Text="Label">Modelo: </asp:Label><asp:DropDownList ID="dpModelos" runat="server" DataSourceID="dsModelos" Width="150px" DataTextField="modelo" DataValueField="idModelo"></asp:DropDownList>
    <asp:SqlDataSource ID="dsModelos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="select idModelo, fabricante + ' - ' + Modelo 'modelo' from modelos">
    </asp:SqlDataSource>
    <asp:Label ID="Label4" runat="server" Text="Label">Série: </asp:Label><asp:TextBox ID="tbSerie" runat="server" Width="250px"></asp:TextBox>
    <asp:Label ID="Label3" runat="server" Text="Label">IP: </asp:Label><asp:TextBox ID="tbIP" runat="server" Width="150px"></asp:TextBox>  
    <br /><br />
    <asp:Button ID="btInserir" runat="server" Text="Inserir" OnClick="btInserir_Click" />
    <br /><asp:RequiredFieldValidator ID="ufRequerida" runat="server" ErrorMessage="Digite o Estado em que o equipamento está locado." ControlToValidate="tbUf"></asp:RequiredFieldValidator>
    <br /><asp:RequiredFieldValidator ID="cidadeRequerida" runat="server" ErrorMessage="Digite a cidade em que o equipamento está locado." ControlToValidate="tbCidade"></asp:RequiredFieldValidator>
    <br /><asp:RequiredFieldValidator ID="serieRequerida" runat="server" ErrorMessage="Digite a série do equipamento." ControlToValidate="tbSerie"></asp:RequiredFieldValidator>
    <div id="divMensagem" runat="server"></div>
    <div>
        <asp:GridView runat="server" ID="gvEquipamentos" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idEquipamento" DataSourceID="dsEquipamentos" ForeColor="#333333" GridLines="None" Width="100%" Font-Size="XX-Small" OnRowEditing="gvEquipamentos_RowEditing" OnRowUpdated="gvEquipamentos_RowUpdated">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="idEquipamento" HeaderText="idEquipamento" InsertVisible="False" ReadOnly="True" SortExpression="idEquipamento" />
                <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
                <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                <asp:BoundField DataField="cidade" HeaderText="cidade" SortExpression="cidade" />
                <asp:BoundField DataField="unidade" HeaderText="unidade" SortExpression="unidade" />
                <asp:BoundField DataField="setor" HeaderText="setor" SortExpression="setor" />
                <asp:BoundField DataField="endereco" HeaderText="endereco" SortExpression="endereco" />
                <asp:BoundField DataField="bairro" HeaderText="bairro" SortExpression="bairro" />
                <asp:BoundField DataField="cep" HeaderText="cep" SortExpression="cep" />
                <asp:BoundField DataField="contato" HeaderText="contato" SortExpression="contato" />
                <asp:BoundField DataField="fone" HeaderText="fone" SortExpression="fone" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="nome" HeaderText="Técnico" SortExpression="nome" />
               
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


        <asp:SqlDataSource ID="dsEquipamentos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            DeleteCommand="DELETE FROM [equipamentos] WHERE [idEquipamento] = @idEquipamento" 
            InsertCommand="INSERT INTO [equipamentos] ([idCliente], [idModelo], [serie], [uf], [cidade], [endereco], [bairro], [cep], [contato], [fone], [email], [status], [unidade], [setor], [dtAtualizacao], [atualizadoPor]) VALUES (@idCliente, @idModelo, @serie, @uf, @cidade, @endereco, @bairro, @cep, @contato, @fone, @email, @status, @unidade, @setor, @dtAtualizacao, @atualizadoPor)" 
            SelectCommand="SELECT a.idEquipamento, a.idCliente, a.idModelo, a.serie, a.uf, a.endereco, a.bairro, a.cep, a.contato, a.fone, a.email, a.status, a.cidade, a.unidade, a.setor, b.nome FROM [equipamentos] as a left join tecnicos as b on a.idtecnico = b.idtecnico WHERE a.idCliente = @idCliente AND a.uf = @uf and a.cidade = @cidade  ORDER BY a.uf, a.cidade, a.unidade" 
            UpdateCommand="UPDATE [equipamentos] SET [idCliente] = @idCliente, [idModelo] = @idModelo, [serie] = @serie, [uf] = @uf, [cidade] = @cidade, [endereco] = @endereco, [bairro] = @bairro, [cep] = @cep, [contato] = @contato, [fone] = @fone, [email] = @email, [status] = @status, [unidade] = @unidade, [setor] = @setor, [dtAtualizacao] = @dtAtualizacao, [atualizadoPor] = @atualizadoPor WHERE [idEquipamento] = @idEquipamento">
            <DeleteParameters>
                <asp:Parameter Name="idEquipamento" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="serie" Type="String" />
                <asp:Parameter Name="uf" Type="String" />
                <asp:Parameter Name="cidade" Type="String" />
                <asp:Parameter Name="endereco" Type="String" />
                <asp:Parameter Name="bairro" Type="String" />
                <asp:Parameter Name="cep" Type="String" />
                <asp:Parameter Name="contato" Type="String" />
                <asp:Parameter Name="fone" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="unidade" Type="String" />
                <asp:Parameter Name="setor" Type="String" />
                <asp:Parameter Name="dtAtualizacao" Type="DateTime" />
                <asp:Parameter Name="atualizadoPor" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpUf" Name="uf" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpCidades" Name="cidade" PropertyName="SelectedValue" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="serie" Type="String" />
                <asp:Parameter Name="uf" Type="String" />
                <asp:Parameter Name="cidade" Type="String" />
                <asp:Parameter Name="endereco" Type="String" />
                <asp:Parameter Name="bairro" Type="String" />
                <asp:Parameter Name="cep" Type="String" />
                <asp:Parameter Name="contato" Type="String" />
                <asp:Parameter Name="fone" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="unidade" Type="String" />
                <asp:Parameter Name="setor" Type="String" />
                <asp:Parameter Name="dtAtualizacao" Type="DateTime" />
                <asp:Parameter Name="atualizadoPor" Type="String" />
                <asp:Parameter Name="idEquipamento" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>

