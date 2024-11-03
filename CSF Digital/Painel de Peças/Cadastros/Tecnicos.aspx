<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Tecnicos.aspx.cs" Inherits="Cadastros_Tecnicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <h2 style="text-align:center">Cadastro de Técnicos</h2>
    <p></p>
    <asp:gridview runat="server" id="gvTecnicos" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idTecnico"
        CssClass="table table-striped table-bordered table-condensed table-hover" DataSourceID="dsTecnicos" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="idTecnico" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="idTecnico" />
            <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
            <asp:BoundField DataField="contato" HeaderText="Contato" SortExpression="contato" />

            <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
            <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
            <asp:BoundField DataField="endereco" HeaderText="Endereço" SortExpression="endereco" />
            <asp:BoundField DataField="numero" HeaderText="Nº" SortExpression="numero" />
            <asp:BoundField DataField="bairro" HeaderText="Bairro" SortExpression="bairro" />

            <asp:BoundField DataField="telefone" HeaderText="Telefone" SortExpression="telefone" />
            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />

        </Columns>
    </asp:gridview>
    <asp:SqlDataSource ID="dsTecnicos" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        DeleteCommand="DELETE FROM [tecnicos] WHERE [idTecnico] = @original_idTecnico AND [nome] = @original_nome AND (([email] = @original_email) OR ([email] IS NULL AND @original_email IS NULL)) AND (([telefone] = @original_telefone) OR ([telefone] IS NULL AND @original_telefone IS NULL)) AND (([endereco] = @original_endereco) OR ([endereco] IS NULL AND @original_endereco IS NULL)) AND (([numero] = @original_numero) OR ([numero] IS NULL AND @original_numero IS NULL)) AND (([bairro] = @original_bairro) OR ([bairro] IS NULL AND @original_bairro IS NULL)) AND (([cidade] = @original_cidade) OR ([cidade] IS NULL AND @original_cidade IS NULL)) AND (([uf] = @original_uf) OR ([uf] IS NULL AND @original_uf IS NULL)) AND (([contato] = @original_contato) OR ([contato] IS NULL AND @original_contato IS NULL))" 
        InsertCommand="INSERT INTO [tecnicos] ([nome], [email], [telefone], [endereco], [numero], [bairro], [cidade], [uf], [contato]) VALUES (@nome, @email, @telefone, @endereco, @numero, @bairro, @cidade, @uf, @contato)" OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [tecnicos]" 
        UpdateCommand="UPDATE [tecnicos] SET [nome] = @nome, [email] = @email, [telefone] = @telefone, [endereco] = @endereco, [numero] = @numero, [bairro] = @bairro, [cidade] = @cidade, [uf] = @uf, [contato] = @contato WHERE [idTecnico] = @original_idTecnico AND [nome] = @original_nome AND (([email] = @original_email) OR ([email] IS NULL AND @original_email IS NULL)) AND (([telefone] = @original_telefone) OR ([telefone] IS NULL AND @original_telefone IS NULL)) AND (([endereco] = @original_endereco) OR ([endereco] IS NULL AND @original_endereco IS NULL)) AND (([numero] = @original_numero) OR ([numero] IS NULL AND @original_numero IS NULL)) AND (([bairro] = @original_bairro) OR ([bairro] IS NULL AND @original_bairro IS NULL)) AND (([cidade] = @original_cidade) OR ([cidade] IS NULL AND @original_cidade IS NULL)) AND (([uf] = @original_uf) OR ([uf] IS NULL AND @original_uf IS NULL)) AND (([contato] = @original_contato) OR ([contato] IS NULL AND @original_contato IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_idTecnico" Type="Int32" />
            <asp:Parameter Name="original_nome" Type="String" />
            <asp:Parameter Name="original_email" Type="String" />
            <asp:Parameter Name="original_telefone" Type="String" />
            <asp:Parameter Name="original_endereco" Type="String" />
            <asp:Parameter Name="original_numero" Type="Int32" />
            <asp:Parameter Name="original_bairro" Type="String" />
            <asp:Parameter Name="original_cidade" Type="String" />
            <asp:Parameter Name="original_uf" Type="String" />
            <asp:Parameter Name="original_contato" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="nome" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="telefone" Type="String" />
            <asp:Parameter Name="endereco" Type="String" />
            <asp:Parameter Name="numero" Type="Int32" />
            <asp:Parameter Name="bairro" Type="String" />
            <asp:Parameter Name="cidade" Type="String" />
            <asp:Parameter Name="uf" Type="String" />
            <asp:Parameter Name="contato" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="nome" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="telefone" Type="String" />
            <asp:Parameter Name="endereco" Type="String" />
            <asp:Parameter Name="numero" Type="Int32" />
            <asp:Parameter Name="bairro" Type="String" />
            <asp:Parameter Name="cidade" Type="String" />
            <asp:Parameter Name="uf" Type="String" />
            <asp:Parameter Name="contato" Type="String" />
            <asp:Parameter Name="original_idTecnico" Type="Int32" />
            <asp:Parameter Name="original_nome" Type="String" />
            <asp:Parameter Name="original_email" Type="String" />
            <asp:Parameter Name="original_telefone" Type="String" />
            <asp:Parameter Name="original_endereco" Type="String" />
            <asp:Parameter Name="original_numero" Type="Int32" />
            <asp:Parameter Name="original_bairro" Type="String" />
            <asp:Parameter Name="original_cidade" Type="String" />
            <asp:Parameter Name="original_uf" Type="String" />
            <asp:Parameter Name="original_contato" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

