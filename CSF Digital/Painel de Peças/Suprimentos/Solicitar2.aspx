<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Solicitar2.aspx.cs" Inherits="Suprimentos_Solicitar2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 style="text-align:center">Solicitação de Suprimentos</h2>
    <p></p>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="row">
                    <div class="col-md-3">
                        <span>Cliente</span>
                        <asp:DropDownList ID="dpClientes" runat="server" Width="100%" DataSourceID="dsClientes" DataTextField="cliente" 
                            DataValueField="idCliente" AutoPostBack="True" CssClass="btn dropdown btn-secondary" ></asp:DropDownList>
                        <asp:TextBox ID="tbCliente" runat="server" Enabled="false" Visible="false" Width="100%"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <span>Série</span>
                        <asp:TextBox ID="tbSerie" runat="server" CssClass="form-control" Width="100%" placeholder="Digite a série" 
                            AutoPostBack="true" OnTextChanged="tbSerie_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span>UF</span>
                        <asp:DropDownList ID="dpUF" runat="server" AutoPostBack="True" DataSourceID="dsUf" DataTextField="uf" DataValueField="uf" Width="100%"  
                            CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                        <asp:TextBox ID="tbUF" runat="server" Enabled="false" Visible="false" Width="100%"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <span>Cidade</span>
                        <asp:DropDownList ID="dpCidade" runat="server" DataTextField="cidade" DataValueField="cidade" AutoPostBack="True" 
                            DataSourceID="dsCidades" Width="100%"  CssClass="btn dropdown btn-secondary"></asp:DropDownList>
                        <asp:TextBox ID="tbCidade" runat="server" Enabled="false" Visible="false" Width="100%"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <span>Unidade</span>
                        <asp:DropDownList ID="dpUnidade" runat="server" AutoPostBack="True" DataSourceID="dsUnidades" 
                            DataTextField="unidade" DataValueField="unidade" CssClass="btn dropdown btn-secondary" Width="100%"></asp:DropDownList>
                        <asp:TextBox ID="tbUnidade" runat="server" Enabled="false" Visible="false" Width="100%"  CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="col-md-4">
                        <span style="text-align:center">Série</span>
                            <asp:DropDownList ID="dpSeries" runat="server" Width="100%" DataSourceID="dsSeries" DataTextField="serie" 
                                DataValueField="serie" AutoPostBack="True" CssClass="btn dropdown btn-secondary" 
                                OnSelectedIndexChanged="dpSeries3_SelectedIndexChanged"></asp:DropDownList>
                         <asp:TextBox ID="tbSerie2" runat="server" Enabled="false" Visible="false" Width="100%"  CssClass="form-control"></asp:TextBox>
                    </div>
                        
                </div>
                <div class="row">
                     <div class="col-md-2">
                        <span>Requisição</span><asp:RequiredFieldValidator ID="ReqFReq" runat="server" ControlToValidate="tbReq" ErrorMessage="*" CssClass="alert-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbReq" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="col-md-3">
                        <span>Contato</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbContato" ErrorMessage="*" CssClass="alert-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbContato" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                    </div>
                   
                   <div class="col-md-5">
                        <span>Email</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmail" ErrorMessage="*" CssClass="alert-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbEmail" runat="server" Width="100%" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Telefone</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbFone" ErrorMessage="*" CssClass="alert-danger">
                                             </asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbFone" runat="server"  Width="100%" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <span>Suprimento</span>
                        <%--<asp:DropDownList ID="dpSuprimento" runat="server" CssClass="btn dropdown btn-secondary" Width="100%" SelectionMode="Multiple">
                        </asp:DropDownList>--%>
                        <asp:ListBox ID="dpSuprimento" runat="server" Width="100%" SelectionMode="Multiple" CssClass="list-group-item" Height="60px"></asp:ListBox>
                    </div>
                    <div class="col-md-2">
                        <span>Contador</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbContador" ErrorMessage="*" CssClass="alert-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbContador" runat="server" Width="100%" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Valor Atual</span>
                        <asp:RequiredFieldValidator ID="ReqvSuprimento" runat="server" ControlToValidate="tbSupriAtual" ErrorMessage="*" CssClass="alert-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbSupriAtual" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Dur. Estimada</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbDurEstimada" ErrorMessage="*" CssClass="alert-danger">
                                             </asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbDurEstimada" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>Falha</span><br />
                        <asp:DropDownList ID="dpFalha" runat="server" CssClass="btn dropdown btn-secondary">
                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Não" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                <div class="col-md-12">
                    <span>Observações</span>
                        <asp:TextBox ID="tbObs" runat="server" TextMode="MultiLine" Width="100%" Height="50px" CssClass="form-control"></asp:TextBox>
                </div>
                </div>
                <div class="row">
                <div class="col-md-12">
                    <hr />
                </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <p></p>
                        <asp:Button ID="btSolicitar" runat="server" Text="Solicitar" CssClass="btn btn-secondary" OnClick="btSolicitar_Click" />
                    </div>
                    <div class="col-md-2">
                        <p></p>
                        <asp:Button ID="btAtualizar" runat="server" Text="Atualizar" Enabled="false" CausesValidation="false" CssClass="btn btn-secondary" OnClick="btAtualizar_Click" />
                    </div>
                    <div class="col-md-2">
                        <p></p>
                    <asp:Button ID="btHistórico" runat="server" Text="Histórico" Enabled="true" CausesValidation="false" 
                        CssClass="btn btn-secondary" OnClick="btHistórico_Click" />
                    </div>
                </div>

                <p></p>
                <div class="row">
                    <div class="col-md-12">
                        <p>
                            <h2>Suprimentos solicitados</h2>
                            <hr />
                        </p>
                        <asp:GridView ID="gvSuprimentosSolicitados" runat="server" CssClass="table table-hover table-condensed" DataSourceID="dsRequisicoes" AutoGenerateColumns="False" DataKeyNames="idreqSuprimento" OnRowCommand="gvSuprimentosSolicitados_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="idreqSuprimento" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="idreqSuprimento" />
                                <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                                <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                                <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
                                <asp:BoundField DataField="suprimento" HeaderText="Suprimento" SortExpression="suprimento" />
                                <asp:BoundField DataField="dataSolicitacao" HeaderText="Data" SortExpression="dataSolicitacao" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="DuracaoEstimada" HeaderText="Dur Estimada" ReadOnly="True" SortExpression="DuracaoEstimada" />
                                <asp:BoundField DataField="Operador" HeaderText="Operador" ReadOnly="True" SortExpression="Operador" />
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btPriorizar" runat="server" Text="Priorizar" CommandName="Priorizar" CausesValidation="false" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-2"></div>
        </div>
       
    </div>

    <%--DataSources--%>
        <asp:SqlDataSource ID="dsUf" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select distinct uf from equipamentos where idCliente = @idCliente order by 1">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
            </SelectParameters>
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
        <asp:SqlDataSource ID="dsSeries" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select 'Selecionar' 'serie', 'Selecionar' 'modelo'
                            UNION ALL
                            select distinct a.serie, b.modelo from equipamentos as a left join modelos as b on a.idmodelo = b.idmodelo  
                            where a.idcliente = @idCliente and a.uf = @uf and a.cidade = @cidade and a.unidade = @unidade">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpClientes" Name="idCliente" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpUF" Name="uf" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpCidade" Name="cidade" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dpUnidade" Name="unidade" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsClientes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="SELECT [idCliente], [cliente] FROM [vw_clientes]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsRequisicoes" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                SelectCommand="select top 10 * from vw_reqsuprimentosResumo where serie = @serie">
            <SelectParameters>
                <asp:ControlParameter ControlID="dpSeries" Name="serie" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsRequisicoes2" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                SelectCommand="select top 10 * from vw_reqsuprimentosResumo where serie = @serie">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbSerie" Name="serie" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    <%--DataSources--%>

    <script type="text/javascript">
        function AtualizarCadastro(serie) {
            confirm('Atualizar a série ' + serie);
            window.open('../Cadastros/Equipamentos2.aspx?serie=' + serie);
            console.log(serie);
        }

        function alerta(){
            alert('Testando alerta');
        }
    </script>
</asp:Content>

