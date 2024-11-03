<%@ Page Title="Atualizar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Atualizar.aspx.cs" Inherits="Atualizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="container-fluid">
         <div class="row">
             <div class="col-md-9">
                 <h2>Atualização de Envios de Peças</h2>
                 <hr />
             </div>
         </div>
        <div class="row">
            <div class="col-md-3">ID: <asp:TextBox ID="tbIdReqPeca" runat="server" CssClass="form-control"></asp:TextBox></div>
            <div class="col-md-3">Status: <asp:DropDownList runat="server" ID="dpStatus" Width="100%" CssClass="form-control" 
                onselectedindexchanged="dpStatus_SelectedIndexChanged">
            <asp:ListItem Selected="True">Pendente Almoxarifado</asp:ListItem>
            <asp:ListItem>Pendente Técnica</asp:ListItem>
            <asp:ListItem>Pendente Compra</asp:ListItem>
            <asp:ListItem>Pendente Compra + 5</asp:ListItem>
            <asp:ListItem>Pendente Compra + 10</asp:ListItem>
            <asp:ListItem>Pendente Compra Sem Previsão</asp:ListItem>
            <asp:ListItem>Comprada</asp:ListItem>
            <asp:ListItem>Cancelado</asp:ListItem>
            <%--<asp:ListItem>Atendido</asp:ListItem>--%>
            </asp:DropDownList></div>
            <div class="col-md-3">Previsão de chegada: <asp:TextBox ID="tbdtPrevisao" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox></div>
        </div>
         <div class="row">
             <div class="col-md-9">
                 Observações: <asp:TextBox runat="server" ID="tbObs" TextMode="MultiLine"  CssClass="form-control" Width="100%"></asp:TextBox>
             </div>
         </div>
         <div class="row">
             <div class="col-md-9">
                 <br />
                <asp:Button runat="server" ID="btAtualizar" Text="Atualizar" onclick="btAtualizar_Click" CssClass="btn btn-secondary" />

                 <asp:TextBox ID="tbMensagem" runat="server" TextMode="MultiLine" Visible="false" Enabled="false" Width="100%" CssClass="form-control">
                 </asp:TextBox>
             </div>
         </div>
         <div class="row">
             <div class="col-md-9">
                 <br />
                 <asp:GridView runat="server" ID="gvReq" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idreqPeca" 
                     DataSourceID="dsReq" ForeColor="#333333" GridLines="None" Width="100%" CssClass="table table-striped table-bordered table-condensed table-hover">
                     <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="idreqPeca" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="idreqPeca" />
                        <asp:BoundField DataField="reqUSD" HeaderText="USD" SortExpression="reqUSD" />
                        <asp:BoundField DataField="peca" HeaderText="Peça" SortExpression="peca" />
                        <asp:BoundField DataField="partNumber" HeaderText="Part Number" SortExpression="partNumber" />
                        <asp:BoundField DataField="qtd" HeaderText="QTD" SortExpression="qtd" />
                        <asp:BoundField DataField="serieEqpto" HeaderText="Série" SortExpression="serieEqpto" />
                        <asp:BoundField DataField="dtCriacao" DataFormatString="{0:d}" HeaderText="Data" SortExpression="dtCriacao" />
                        <asp:BoundField DataField="eqpParado" HeaderText="Parado?" SortExpression="eqpParado" />
                    </Columns>
                </asp:GridView>
             </div>
         </div>
         <div class="row">
             <div class="col-md-9 align-items-center">
                 <h3 class="text-center">Histórico de atualizações</h3>
             </div>
         </div>
         <div class="row">
             <br />
             <div class="col-md-9">
                 <asp:GridView runat="server" ID="detalheChamado" DataSourceID="consultaDetalhe" 
                    AutoGenerateColumns="False" DataKeyNames="idatulPeca" CellPadding="4" CssClass="table table-striped table-bordered table-condensed table-hover"
                    ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="idatulPeca" HeaderText="idatulPeca" 
                            InsertVisible="False" ReadOnly="True" SortExpression="idatulPeca" />
                        <asp:BoundField DataField="idreqPeca" HeaderText="idreqPeca" 
                            SortExpression="idreqPeca" />
                        <asp:BoundField DataField="postagem" HeaderText="postagem" 
                            SortExpression="postagem" />
                        <asp:BoundField DataField="obs" HeaderText="obs" SortExpression="obs" />
                        <asp:BoundField DataField="dtEnvio" HeaderText="dtEnvio" 
                            SortExpression="dtEnvio" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="dtCriacao" HeaderText="dtCriacao" 
                            SortExpression="dtCriacao" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="status" HeaderText="status" 
                            SortExpression="status" />
                    </Columns>
                </asp:GridView>
             </div>
         </div>
    </div>


    <%--//Antigo--%> 
        <%--<asp:Label runat="server" ID="lbdtPrevisao" Visible="false"></asp:Label>--%>
        <%--<asp:TextBox ID="tbPostagem" runat="server" Visible="false"  CssClass="form-control"></asp:TextBox>--%>
        <%--<asp:TextBox ID="tbData" runat="server" Visible="false"  CssClass="form-control"></asp:TextBox>--%>
        <%--<asp:TextBox ID="tbAR" runat="server" Visible="false"  CssClass="form-control"></asp:TextBox>--%>
    <%--Antigo--%>
    
    <%--DataSources--%>
    <asp:SqlDataSource ID="dsReq" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        DeleteCommand="DELETE FROM [reqPecas] WHERE [idreqPeca] = @original_idreqPeca AND (([reqUSD] = @original_reqUSD) OR ([reqUSD] IS NULL AND @original_reqUSD IS NULL)) AND [peca] = @original_peca AND (([partNumber] = @original_partNumber) OR ([partNumber] IS NULL AND @original_partNumber IS NULL)) AND (([qtd] = @original_qtd) OR ([qtd] IS NULL AND @original_qtd IS NULL)) AND (([serieEqpto] = @original_serieEqpto) OR ([serieEqpto] IS NULL AND @original_serieEqpto IS NULL)) AND (([dtCriacao] = @original_dtCriacao) OR ([dtCriacao] IS NULL AND @original_dtCriacao IS NULL)) AND (([eqpParado] = @original_eqpParado) OR ([eqpParado] IS NULL AND @original_eqpParado IS NULL))" 
        InsertCommand="INSERT INTO [reqPecas] ([reqUSD], [peca], [partNumber], [qtd], [serieEqpto], [dtCriacao], [eqpParado]) VALUES (@reqUSD, @peca, @partNumber, @qtd, @serieEqpto, @dtCriacao, @eqpParado)" OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT [idreqPeca], [reqUSD], [peca], [partNumber], [qtd], [serieEqpto], [dtCriacao], [eqpParado] FROM [reqPecas] WHERE ([idreqPeca] = @idreqPeca)" 
        UpdateCommand="UPDATE [reqPecas] SET [reqUSD] = @reqUSD, [peca] = @peca, [partNumber] = @partNumber, [qtd] = @qtd, [serieEqpto] = @serieEqpto WHERE [idreqPeca] = @original_idreqPeca">
        <DeleteParameters>
            <asp:Parameter Name="original_idreqPeca" Type="Int32" />
            <asp:Parameter Name="original_reqUSD" Type="String" />
            <asp:Parameter Name="original_peca" Type="String" />
            <asp:Parameter Name="original_partNumber" Type="String" />
            <asp:Parameter Name="original_qtd" Type="Int32" />
            <asp:Parameter Name="original_serieEqpto" Type="String" />
            <asp:Parameter Name="original_dtCriacao" Type="DateTime" />
            <asp:Parameter Name="original_eqpParado" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="reqUSD" Type="String" />
            <asp:Parameter Name="peca" Type="String" />
            <asp:Parameter Name="partNumber" Type="String" />
            <asp:Parameter Name="qtd" Type="Int32" />
            <asp:Parameter Name="serieEqpto" Type="String" />
            <asp:Parameter Name="dtCriacao" Type="DateTime" />
            <asp:Parameter Name="eqpParado" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="idreqPeca" QueryStringField="idreqPeca" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="original_idreqPeca" Type="Int32" />
               
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="consultaDetalhe" runat="server" 
        ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="select * from atulPecas where idreqpeca = @idreqPeca" >
                <SelectParameters>
                    <asp:ControlParameter ControlID="tbIdReqPeca" Name="idreqPeca" 
                        PropertyName="Text"/>
                </SelectParameters>
    </asp:SqlDataSource>
    <%--DataSources--%>

</asp:Content>

