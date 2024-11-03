<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Manutencao.aspx.cs" Inherits="Atendimentos_Manutencao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12" style="text-align:center">
                <h1>Solicitações de Atendimento Técnico</h1>
                <h2>Manutenção</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span>Cliente</span>
                <asp:DropDownList ID="dpClientes" runat="server" Width="100%" DataSourceID="dsClientes" DataTextField="cliente" DataValueField="idCliente" AutoPostBack="True" CssClass="btn dropdown btn-secondary"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <span>UF</span>
                <asp:DropDownList ID="dpUF" runat="server" AutoPostBack="True" DataSourceID="dsUf" DataTextField="uf" DataValueField="uf" Width="100%"  CssClass="btn dropdown btn-secondary"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <span>Cidade</span>
                <asp:DropDownList ID="dpCidade" runat="server" DataTextField="cidade" DataValueField="cidade" AutoPostBack="True" DataSourceID="dsCidades" Width="100%"  CssClass="btn dropdown btn-secondary"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <span>Unidade</span>
                <asp:DropDownList ID="dpUnidade" runat="server" AutoPostBack="True" DataSourceID="dsUnidades" DataTextField="unidade" DataValueField="unidade" CssClass="btn dropdown btn-secondary" Width="100%"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span>Série</span>
                <asp:DropDownList ID="dpSeries" runat="server" Width="100%" DataSourceID="dsSeries" DataTextField="serie" DataValueField="modelo" AutoPostBack="True" CssClass="btn dropdown btn-secondary"> </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <span>Requisição</span>
                <asp:TextBox ID="tbReq" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <span>Contato</span>
                <asp:TextBox ID="tbContato" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span>Telefone</span>
                <asp:TextBox ID="tbFone" runat="server"  Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <span>Endereço</span>
                <asp:TextBox ID="tbEndereco" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <span>Falha</span>
                    <asp:TextBox ID="tbFalha" runat="server" TextMode="MultiLine" Width="100%" Height="50px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span>Técnico</span>
                <asp:TextBox ID="tbTecnico" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <span>Email</span>
                <asp:TextBox ID="tbEmailTecnico" runat="server"  Width="100%" CssClass="form-control"></asp:TextBox>
                <asp:TextBox ID="tbemailCopia" runat="server" Width="100%" Visible="false"></asp:TextBox>
                <asp:CheckBox ID="checkRegistro" runat="server" Text="Registrar Somente?" AutoPostBack="True" Visible="false"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1">
                <p></p>
                <asp:Button ID="btSolicitar" runat="server" Text="Solicitar" OnClick="btSolicitar_Click" CssClass="btn btn-secondary" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <p></p>
                <h3>Histórico de Solicitações</h3>
                <br />
                <asp:GridView ID="gvSolicitacoes" runat="server" AutoGenerateColumns="False" DataSourceID="dsSolicitacoes" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-condensed table-hover">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="req" HeaderText="USD" SortExpression="req" >
                        <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="falha" HeaderText="Falha" SortExpression="falha">
                        <ItemStyle Width="700px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tecnico" HeaderText="Técnico" SortExpression="tecnico" >
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dtAbertura" HeaderText="Data Abertura" SortExpression="dtAbertura" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="dtFechamento" HeaderText="Data Fechamento" SortExpression="dtFechamento" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <br />
   

    <%--DataSources--%>
     <asp:SqlDataSource ID="dsUf" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct uf from equipamentos order by 1">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCidades" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct cidade from equipamentos where idcliente = @idCliente and uf = @uf order by 1">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsUnidades" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct unidade from equipamentos where idcliente = @idCliente and uf = @uf and cidade = @cidade order by 1">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="dpCidade" Name="cidade" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsSeries" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct a.serie, b.modelo from equipamentos as a left join modelos as b on a.idmodelo = b.idmodelo  where a.idcliente = @idCliente and a.uf = @uf and a.cidade = @cidade and a.unidade = @unidade order by 1">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="dpCidade" Name="cidade" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="dpUnidade" Name="unidade" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT [idCliente], [cliente] FROM [vw_clientes]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsSolicitacoes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select req, falha, tecnico, dtAbertura, dtFechamento, status from reqAtendimentos where serie = @serie">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpSeries" Name="serie" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <%--DataSources--%>

    <%--Validações--%>
    <asp:Label ID="lbMensagem" runat="server" Visible="false"></asp:Label>
    <asp:RequiredFieldValidator ID="rValidatorContato" runat="server" ControlToValidate="tbContato" ErrorMessage="*Insira o nome de uma pessoa de contato." ForeColor="Red"></asp:RequiredFieldValidator>
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbFone" ErrorMessage="*Insira um telefone de contato." ForeColor="Red"></asp:RequiredFieldValidator>    
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEndereco" ErrorMessage="*Insira o endereço do atendimento." ForeColor="Red"></asp:RequiredFieldValidator>    
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbFalha" ErrorMessage="*Informe a falha apresentada pelo equipamento." ForeColor="Red"></asp:RequiredFieldValidator>    
    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbEmailTecnico" ErrorMessage="*Informe o email para encaminhar a solicitação." ForeColor="Red"></asp:RequiredFieldValidator>    
    <%--Validações--%>
</asp:Content>

