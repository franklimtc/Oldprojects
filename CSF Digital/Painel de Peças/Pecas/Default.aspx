<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pecas_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset>
    <legend>Envios de Peças</legend>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <span class="label label-default">Estoque: </span>
                <asp:DropDownList runat="server" ID="dpEstoque" CssClass="btn btn-secondary dropdown-toggle" DataSourceID="objDSEstoques" DataTextField="Descricao" DataValueField="Id">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="objDSEstoques" runat="server" SelectMethod="Listar" TypeName="Estoque"></asp:ObjectDataSource>
            </div>
            <div class="col-md-2">
                <span class="label label-default">Tipo: </span>
                <asp:DropDownList ID="dptpEnvio" runat="server" Width="100%"  CssClass="btn btn-secondary dropdown-toggle">
                    <asp:ListItem Selected="True">PAC</asp:ListItem>
                    <asp:ListItem>SEDEX</asp:ListItem>
                    <asp:ListItem>Técnico</asp:ListItem>
                    <asp:ListItem>Motoboy</asp:ListItem>
                    <asp:ListItem>Transportadora</asp:ListItem>
                    <asp:ListItem>Outros</asp:ListItem>
                </asp:DropDownList>
            </div>
           
        </div>

        <br />

	    <div class="row">
            <div class="col-md-2">
                <span class="label label-default">Requisição: </span>
                <asp:RequiredFieldValidator runat="server" ID="RV_IdRequisicao" ForeColor="Red"  
                        ControlToValidate="tbRequisicao" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="tbRequisicao" runat="server" width="100%" CssClass="form-control" 
                    OnTextChanged="tbRequisicao_TextChanged" AutoPostBack="true" TextMode="Number"></asp:TextBox>
               
		    </div>
             <div class="col-md-2">
                <span class="label label-default">Série: </span>
                <asp:TextBox ID="tbSerie" runat="server" width="100%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
		    </div>
            <div class="col-md-2">
                <span class="label label-default">PartNumber: </span>
                <asp:TextBox runat="server" ID="tbPartNumber" CssClass="form-control" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
            </div>
		    <div class="col-md-2">
                <span class="label label-default">Postagem: </span>
                <asp:RequiredFieldValidator runat="server" ID="RFV_Postagem" ForeColor="Red" 
                        ControlToValidate="tbPostagem" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="tbPostagem" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
		    <div class="col-md-2">
                <span class="label label-default">Código AR: </span>
                <asp:TextBox ID="tbAR" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
		    </div>
             <div class="col-md-2">
                <span class="label label-default">Valor: </span>
                <asp:TextBox ID="tbValor" runat="server" width="100%" CssClass="form-control" AutoPostBack="true" OnTextChanged="tbValor_TextChanged"></asp:TextBox>
                 <asp:CustomValidator id="CVValor" runat="server"  OnServerValidate="MoneyValidate"  
                     ControlToValidate="tbValor"  ErrorMessage="Insira um valor monetário válido.">
                 </asp:CustomValidator>
		    </div>
	    </div>
        <br />
        <div class="row">
            <div class="col-md-10">
                <span class="label label-default">Observações: </span>
                <asp:TextBox runat="server" ID="tbObs" TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>
            </div>
        </div>
    </div>

    <br />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btInserir" runat="server" Text="Inserir" Width="80px" TabIndex="8" CssClass="btn btn-default" OnClick="btInserir_Click" />
                <asp:Button ID="btLimpar" runat="server" Text="Limpar"  Width="80px"  CssClass="btn btn-default" OnClick="btLimpar_Click" />
            </div>
            <div class="col-md-2">
                
            </div>
            <div class="col-md-8"></div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br />
                <br />
                <br />
            </div>
        </div>
    </div>

    <br /><%--<asp:CustomValidator ID="partNumberValidator" runat="server" ErrorMessage="Part Number não identificado!" ControlToValidate="tbPartNumber" OnServerValidate="PartNumberValidate" ></asp:CustomValidator>--%>
</fieldset>
</asp:Content>

