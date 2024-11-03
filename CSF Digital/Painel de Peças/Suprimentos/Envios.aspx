<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Envios.aspx.cs" Inherits="Suprimentos_Envios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
<fieldset>
    <legend>Envios de Suprimentos Diários</legend>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <span class="label label-default">Estoque: </span>
                <asp:DropDownList runat="server" ID="dpEstoque" CssClass="btn btn-secondary dropdown-toggle" 
                    Width="100%" DataSourceID="objDSEstoques" DataTextField="Descricao" DataValueField="Id"></asp:DropDownList>
                <asp:ObjectDataSource ID="objDSEstoques" runat="server" SelectMethod="Listar" TypeName="Estoque"></asp:ObjectDataSource>
            </div>
            <div class="col-md-2">
                <span class="label label-default">Tipo: </span>
                <asp:DropDownList ID="dptpEnvio" runat="server" Width="100%"  CssClass="btn btn-secondary dropdown-toggle"
                        AutoPostBack="True" onselectedindexchanged="dptpEnvio_SelectedIndexChanged">
                    <asp:ListItem Selected="True">PAC</asp:ListItem>
                    <asp:ListItem>SEDEX</asp:ListItem>
                    <asp:ListItem>Técnico</asp:ListItem>
                    <asp:ListItem>Motoboy</asp:ListItem>
                    <asp:ListItem>Transportadora</asp:ListItem>
                    <asp:ListItem>Outros</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <span class="label label-default">PartNumber: </span>
                <%--<asp:DropDownList runat="server" ID="dpPartNumber" CssClass="btn btn-default dropdown-toggle" DataSourceID="objDSProdutos" DataTextField="Partnumber" DataValueField="Id"></asp:DropDownList>--%>
                <asp:TextBox runat="server" ID="tbPartNumber" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbPartNumber_TextChanged"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span class="label label-default">Quantidade: </span><asp:RangeValidator ID="RV_Quantidade" runat="server" ControlToValidate="tbQtd" Type="Integer"  MinimumValue="1" MaximumValue="100" ErrorMessage="*" CssClass="alert-danger"></asp:RangeValidator>
                <%--<asp:DropDownList runat="server" ID="dpPartNumber" CssClass="btn btn-default dropdown-toggle" DataSourceID="objDSProdutos" DataTextField="Partnumber" DataValueField="Id"></asp:DropDownList>--%>
                <asp:TextBox runat="server" ID="tbQtd" CssClass="form-control" Text="1" TextMode="Number"></asp:TextBox>
                

            </div>     
            <div class="col-md-2">
                <span>Cliente:</span>
                <asp:DropDownList runat="server" ID="dpCliente" OnDataBound="dpCliente_DataBinding" 
                    CssClass="btn btn-secondary dropdown-toggle" Width="100%" DataSourceID="dsClientes" 
                    DataTextField="cliente" DataValueField="serie" AutoPostBack="true" OnSelectedIndexChanged="dpCliente_SelectedIndexChanged">

                </asp:DropDownList>
            </div>
        </div>

        <br />

	    <div class="row">
            <div class="col-md-2">
                <span class="label label-default">Requisição: </span>
                <asp:TextBox ID="tbRequisicao" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
		    <div class="col-md-2">
                <span class="label label-default">Série: </span>
                <asp:TextBox ID="tbSerie" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
		    <div class="col-md-2">
                <span class="label label-default">Etiqueta: </span>
                <asp:TextBox ID="tbEtiqueta" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
		    <div class="col-md-2">
                <span class="label label-default">Postagem: </span>
                <asp:TextBox ID="tbPostagem" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
		    <div class="col-md-2">
                <span class="label label-default">Código AR: </span>
                <asp:TextBox ID="tbAR" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
		    <div class="col-md-2">
                 <span class="label label-default">Valor: </span>
                <asp:TextBox ID="tbValor" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
	    </div>
    </div>

    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btInserir" runat="server" Text="Inserir" Width="80px" TabIndex="8" CssClass="btn btn-default"
                    onclick="btInserir_Click" />
                <asp:Button ID="btLimpar" runat="server" Text="Limpar"  Width="80px"  CssClass="btn btn-default"
                    onclick="btLimpar_Click" />
            </div>
            <div class="col-md-2">
                
            </div>
            <div class="col-md-8"></div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br /><asp:Label runat="server" ID="lbErroPartNumber" Text="**Verificar o PartNumber informado!" CssClass="label label-danger" Visible="false"></asp:Label>
                <br /><asp:Label runat="server" ID="lbErroReq" Text="***Verificar o número da requisição!" CssClass="label label-danger" Visible="false"></asp:Label>
                <br /><asp:Label runat="server" ID="lbErroSaldo" Text="****Saldo em estoque insuficiente!" CssClass="label label-danger" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

    <br /><%--<asp:CustomValidator ID="partNumberValidator" runat="server" ErrorMessage="Part Number não identificado!" ControlToValidate="tbPartNumber" OnServerValidate="PartNumberValidate" ></asp:CustomValidator>--%>
</fieldset>
<asp:GridView ID="gvEnvios" runat="server" DataSourceID="dbEnvios" Width="100%" 
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idEnvio" 
        ForeColor="#333333" GridLines="None" OnRowCommand="gvEnvios_RowCommand">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="idEnvio" HeaderText="idEnvio" InsertVisible="False" 
            ReadOnly="True" SortExpression="idEnvio" />
        <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
        <asp:BoundField DataField="partNumber" HeaderText="partNumber" 
            SortExpression="partNumber" />
        <asp:BoundField DataField="etiqueta" HeaderText="etiqueta" 
            SortExpression="etiqueta" />
        <asp:BoundField DataField="tpEnvio" HeaderText="tpEnvio" 
            SortExpression="tpEnvio" />
        <asp:BoundField DataField="postagem" HeaderText="postagem" 
            SortExpression="postagem" />
        <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" />
        <asp:BoundField DataField="usuario" HeaderText="usuario" 
            SortExpression="usuario" />
        <asp:BoundField DataField="dtEnvio" DataFormatString="{0:d}" 
            HeaderText="dtEnvio" SortExpression="dtEnvio" />
         <asp:BoundField DataField="filial" HeaderText="Filial" 
            SortExpression="filial" />
        <asp:TemplateField>
             <ItemTemplate>
                    <asp:Button runat="server" ID="btExcluir" 
                        Text="Excluir" CommandName="Excluir" 
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
            </ItemTemplate>
         </asp:TemplateField>
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
 <asp:CustomValidator ID="CustomValidator1" runat="server" 
        ErrorMessage="CustomValidator" 
        onservervalidate="CustomValidator1_ServerValidate"></asp:CustomValidator>

<asp:SqlDataSource ID="dbEnvios" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select* from enviosSuprimentos where convert(char(10),dtEnvio ,103) = convert(char(10),getdate(),103)  order by idEnvio DESC" >
        </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsClientes" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select idCliente, cliente, serie from clientes where status = 1
UNION ALL SELECT '0', 'Selecionar', 'Selecionar'
order by 2" >
        </asp:SqlDataSource>
</asp:Content>

