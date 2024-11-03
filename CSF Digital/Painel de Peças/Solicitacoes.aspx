<%@ Page Title="Solicitações" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Solicitacoes.aspx.cs" Inherits="Solicitacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>
        SOLICITAÇÕES DE PEÇAS
    </h2>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2">Cliente
                <asp:DropDownList ID="dpClientes" runat="server" AutoPostBack="True" DataSourceID="dsClientes" 
                DataTextField="cliente" DataValueField="idCliente" CssClass="btn btn-secondary dropdown-toggle" Width="90%"></asp:DropDownList>
            </div>
            <div class="col-md-3">Requisição USD
                <asp:TextBox id="tbReqUSD" runat="server" CssClass="form-control"  Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">Estado
                <asp:DropDownList ID="dpUf" runat="server" DataSourceID="dsUF" DataTextField="uf" 
                DataValueField="uf" AutoPostBack="True"  CssClass="btn btn-secondary dropdown-toggle" Width="90%"></asp:DropDownList>

            </div>

            <div class="col-md-3">Cidade
                <asp:DropDownList ID="dpCidades" runat="server" DataSourceID="dsCidades" 
                    DataTextField="cidade" DataValueField="cidade" AutoPostBack="True" CssClass="btn btn-secondary dropdown-toggle" Width="100%"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">Série
                <asp:DropDownList ID="dpSerie" runat="server" AutoPostBack="True" DataSourceID="dsSeries" Width="100%" 
                    DataTextField="serie" DataValueField="serie" CssClass="btn btn-secondary dropdown-toggle"></asp:DropDownList>
                <asp:TextBox runat="server" ID="dtReqUSD" Visible="false" ReadOnly="true"  CssClass="form-control"/>
                <asp:TextBox id="tbSolicitante" runat="server" ReadOnly="True" Visible="false"></asp:TextBox>
                <%--<asp:DropDownList ID="dpSerie" runat="server"  DataSourceID="Series"  Width="90%"
                    DataTextField="numero_serie" DataValueField="numero_serie" AutoPostBack="True" 
                    onselectedindexchanged="dpSerie_SelectedIndexChanged" CssClass="btn btn-default dropdown-toggle"></asp:DropDownList>--%>
            </div>
            <div class="col-md-3">Técnico
                <asp:TextBox id="tbTecnico" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>
        </div>
    </div>
    
    
    <asp:SqlDataSource ID="dsCidades" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="select distinct cidade from equipamentos where idCliente = @idcliente and uf = @uf order by 1">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpUf" Name="uf" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpClientes" Name="idcliente" PropertyName="SelectedValue" />

            </SelectParameters>
        </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsUF" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="select distinct uf from equipamentos where idCliente = @idcliente order by 1">
        <SelectParameters>
                <asp:ControlParameter ControlID="dpClientes" Name="idcliente" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="dsSeries" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand="select distinct serie from equipamentos where uf = @uf and cidade = @cidade and idCliente = @idcliente order by 1">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpUf" Name="uf" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpCidades" Name="cidade" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpClientes" Name="idcliente" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    <br />
   <table border="2px" class="table table-condensed">
        <tr>
            
            <th align="center">Usar</th>
            <th align="center">Part Number</th>
            <th align="center">Peça</th>
            <th align="center">Qtd</th>
            <th align="center">Crítico</th>
        </tr>
        <tr>           
            <td><asp:Button runat="server" ID="ativar1" Text="Ativar" Width = "80px" onclick="ativar1_Click"  Visible="false"/></td>
            <td><asp:TextBox runat="server" ID ="partNumber1" Width="100%"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="peca1" Width="100%"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="qtd1" Width="100%"></asp:TextBox></td>
            <td align="center"><asp:CheckBox runat="server" id="check1" Text=" " /></td>
        </tr>
        <tr>
           <td><asp:Button runat="server" ID="ativar2" Text="Ativar" Width = "80px" 
                   onclick="ativar2_Click"/></td>
            <td><asp:TextBox runat="server" ID ="partNumber2" Enabled="false"  Width="100%"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="peca2"   Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="qtd2"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td align="center"><asp:CheckBox runat="server" id="check2" Text=" " 
                    Enabled="False" /></td>
        </tr>
         <tr>
            <td><asp:Button runat="server" ID="ativar3" Text="Ativar" Width = "80px" 
                    onclick="ativar3_Click"/></td>
            <td><asp:TextBox runat="server" ID ="partNumber3"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="peca3"   Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="qtd3" Width="100%" Enabled="false"></asp:TextBox></td>
            <td align="center"><asp:CheckBox runat="server" id="check3" Text=" " 
                    Enabled="False" /></td>
        </tr>
        <tr>
            <td><asp:Button runat="server" ID="ativar4" Text="Ativar" Width = "80px" 
                    onclick="ativar4_Click"/></td>
            <td><asp:TextBox runat="server" ID ="partNumber4"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="peca4"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="qtd4"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td align="center"><asp:CheckBox runat="server" id="check4" Text=" " 
                    Enabled="False" /></td>
        </tr>
        <tr>
            <td><asp:Button runat="server" ID="ativar5" Text="Ativar" Width = "80px" 
                    onclick="ativar5_Click"/></td>
            <td><asp:TextBox runat="server" ID ="partNumber5"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="peca5"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td><asp:TextBox runat="server" ID ="qtd5"  Width="100%" Enabled="false"></asp:TextBox></td>
            <td align="center"><asp:CheckBox runat="server" id="check5" Text=" " 
                    Enabled="False" /></td>
        </tr>
   </table>
   <p></p>
    <div>
        <asp:Button runat="server" ID="btSolicitar" Text="Solicitar" 
                onclick="btSolicitar_Click" />
    </div>
        <p></p>
    <div>
        <asp:GridView runat="server" ID="gvDados" DataSourceID="SolicitacoesPecas" 
            OnSelectedIndexChanged="dpSerie_SelectedIndexChanged" Width="100%" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" 
            ForeColor="#333333" GridLines="None" style="margin-left: 4px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Série" HeaderText="Série" SortExpression="Série" />
                <asp:BoundField DataField="USD" HeaderText="USD" SortExpression="USD">
                </asp:BoundField>
                <asp:BoundField DataField="Part Number" HeaderText="Part Number" 
                    SortExpression="Part Number" />
                <asp:BoundField DataField="Peça" HeaderText="Peça" SortExpression="Peça" />
                <asp:BoundField DataField="Qtd" HeaderText="Qtd" SortExpression="Qtd" />
                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" 
                    SortExpression="Solicitante" />
                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" 
                    SortExpression="Data" />
                <asp:BoundField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />
                <asp:BoundField DataField="Postagem" HeaderText="Postagem" ReadOnly="True" 
                    SortExpression="Postagem" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EqpParado" HeaderText="EqpParado" 
                    SortExpression="EqpParado">
                <ItemStyle HorizontalAlign="Center" />
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
    </div>
    <div>
        <p>
            &nbsp;</p>
    </div>


    <asp:SqlDataSource ID = "Series" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        SelectCommand = "Select serie 'numero_serie', idEquipamento 'id_impr_snmp' from  equipamentos  where LTRIM(RTRIM(serie)) &lt;&gt; '' 
and idCliente = @idCliente
order by serie">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID = "SolicitacoesPecas" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>"         
        SelectCommand = "SELECT idreqPeca 'ID', serieEqpto 'Série', reqUSD 'USD', partNumber 'Part Number', peca 'Peça', Qtd, Solicitante, dtSolicitacao 'Data', Status,
        (select top(1) postagem from atulPecas as b  where a.idreqPeca = b.idreqPeca and status = 'Atendido' and postagem is not null) 'Postagem', EqpParado
        FROM reqPecas as a WHERE LTRIM(RTRIM(serieEqpto)) = @serie">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpSerie" Name="serie" PropertyName="SelectedValue" Type="String"/>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsClientes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="SELECT [idCliente], [cliente] FROM [clientes] where status = 1">
    </asp:SqlDataSource>

</asp:Content>

