<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Solicitar.aspx.cs" Inherits="Suprimentos_Solicitacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Filtros" runat="server">
        <asp:DropDownList ID="dpUf" runat="server" DataSourceID="dsUF" DataTextField="uf" DataValueField="uf" AutoPostBack="True"></asp:DropDownList>
        <br />
        <asp:DropDownList ID="dpCidades" runat="server" DataSourceID="dsCidades" DataTextField="cidade" DataValueField="cidade" AutoPostBack="True"></asp:DropDownList>
        <br />
        <asp:DropDownList ID="dpSeries" runat="server" AutoPostBack="True" DataSourceID="dsSeries" DataTextField="serie" DataValueField="serie" OnSelectedIndexChanged="dpSeries_SelectedIndexChanged"></asp:DropDownList>
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
        <asp:SqlDataSource ID="dsUF" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select distinct uf from equipamentos order by 1"></asp:SqlDataSource>
    </asp:Panel>

    <br />
    <asp:Button ID="btHistorico" runat="server" Text="Histórico" OnClick="btHistorico_Click" />
    <asp:Button ID="btSolicitacao" runat="server" Text="Solicitar" OnClick="btSolicitacao_Click" />
    <br />
    <h2>Dados do Equipamento</h2>
    <asp:GridView ID="gvEquipamento" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idEquipamento" DataSourceID="dsEquipamento" ForeColor="#333333" GridLines="None" Width="100%" OnRowUpdated="gvEquipamento_RowUpdated">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowEditButton="True"   />
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

    <asp:SqlDataSource ID="dsEquipamento" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
        DeleteCommand="DELETE FROM [equipamentos] WHERE [idEquipamento] = @idEquipamento" 
        InsertCommand="INSERT INTO [equipamentos] ([serie], [uf], [cidade], [unidade], [setor], [endereco], [bairro], [cep], [contato], [fone], [email]) VALUES (@serie, @uf, @cidade, @unidade, @setor, @endereco, @bairro, @cep, @contato, @fone, @email)" 
        SelectCommand="SELECT [idEquipamento], [serie], [uf], [cidade], [unidade], [setor], [endereco], [bairro], [cep], [contato], [fone], [email] FROM [equipamentos] WHERE ([serie] = @serie)" 
        UpdateCommand="UPDATE [equipamentos] SET [serie] = @serie, [uf] = @uf, [cidade] = @cidade, [unidade] = @unidade, [setor] = @setor, [endereco] = @endereco, [bairro] = @bairro, [cep] = @cep, [contato] = @contato, [fone] = @fone, [email] = @email WHERE [idEquipamento] = @idEquipamento">
        <DeleteParameters>
            <asp:Parameter Name="idEquipamento" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="serie" Type="String" />
            <asp:Parameter Name="uf" Type="String" />
            <asp:Parameter Name="cidade" Type="String" />
            <asp:Parameter Name="unidade" Type="String" />
            <asp:Parameter Name="setor" Type="String" />
            <asp:Parameter Name="endereco" Type="String" />
            <asp:Parameter Name="bairro" Type="String" />
            <asp:Parameter Name="cep" Type="String" />
            <asp:Parameter Name="contato" Type="String" />
            <asp:Parameter Name="fone" Type="String" />
            <asp:Parameter Name="email" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="dpSeries" Name="serie" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="serie" Type="String" />
            <asp:Parameter Name="uf" Type="String" />
            <asp:Parameter Name="cidade" Type="String" />
            <asp:Parameter Name="unidade" Type="String" />
            <asp:Parameter Name="setor" Type="String" />
            <asp:Parameter Name="endereco" Type="String" />
            <asp:Parameter Name="bairro" Type="String" />
            <asp:Parameter Name="cep" Type="String" />
            <asp:Parameter Name="contato" Type="String" />
            <asp:Parameter Name="fone" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="idEquipamento" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <br />
    <asp:Panel ID="Abertos" runat="server">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                 
            <asp:View ID="Historico" runat="server">
                <h2>Histórico</h2>
                <asp:GridView ID="gvHistorico" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsHistoricoEnvios" ForeColor="#333333" GridLines="None">

                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="serie" HeaderText="serie" SortExpression="serie" />
                        <asp:BoundField DataField="partNumber" HeaderText="partNumber" SortExpression="partNumber" />
                        <asp:BoundField DataField="postagem" HeaderText="postagem" SortExpression="postagem" />
                        <asp:BoundField DataField="etiqueta" HeaderText="etiqueta" SortExpression="etiqueta" />
                        <asp:BoundField DataField="Suprimento" HeaderText="Suprimento" SortExpression="Suprimento" ReadOnly="True" />
                        <asp:BoundField DataField="tpEnvio" HeaderText="tpEnvio" SortExpression="tpEnvio" />
                        <asp:BoundField DataField="usuario" HeaderText="usuario" SortExpression="usuario" />
                        <asp:BoundField DataField="dtEnvio" HeaderText="dtEnvio" SortExpression="dtEnvio" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="statusEntrega" HeaderText="statusEntrega" SortExpression="statusEntrega" />
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
                <asp:SqlDataSource ID="dsHistoricoEnvios" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select serie, partNumber, postagem, etiqueta
, case
when etiqueta like '01%' or etiqueta like '1%' then 'Cilindro'
when etiqueta like '02%' or etiqueta like '2%'  then 'Tonner'
when etiqueta like '03%' or etiqueta like '3%'  then 'Tonner'
else 'ND' end as 'Suprimento'
, tpEnvio, usuario, dtEnvio, statusEntrega 
from enviosSuprimentos 
where serie = @serie order by dtEnvio desc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="dpSeries" Name="serie" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <h2>Chamados Abertos</h2>
                <asp:GridView ID="gvChamadosAbertosHistorico" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsChamadosAbertos" ForeColor="#333333" GridLines="None" DataKeyNames="idreqSuprimento" OnRowEditing="gvChamadosAbertos_RowEditing" OnRowCommand="gvChamadosAbertosHistorico_RowCommand">

                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btCancelar" runat="server" Text="Cancelar" CommandName="Cancelar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btPriorizar" runat="server" Text="Priorizar" CommandName="Priorizar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            
                        <asp:BoundField DataField="idreqSuprimento" HeaderText="ID" SortExpression="idreqSuprimento" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                        <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                        <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
                        <asp:BoundField DataField="emailUsuario" HeaderText="E-mail" SortExpression="emailUsuario" />
                        <asp:BoundField DataField="suprimento" HeaderText="Suprimento" SortExpression="suprimento" />
                        <asp:BoundField DataField="solicitante" HeaderText="Solicitante" SortExpression="solicitante" />
                        <asp:BoundField DataField="codUsd" HeaderText="USD" SortExpression="codUsd" />
                        <asp:BoundField DataField="dataSolicitacao" HeaderText="Data" SortExpression="dataSolicitacao" DataFormatString="{0:d}" />
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
            </asp:View>
            <asp:View ID="Solicitacao" runat="server">
                <fieldset>
                    <legend>Solicitação de Suprimentos</legend>
                    
                    <asp:Table ID="Table1" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>Endereço:
                                <asp:RequiredFieldValidator ID="rfEndereco" runat="server" ErrorMessage="*" ControlToValidate="tbEndereco"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3"><asp:TextBox ID="tbEndereco" runat="server" Width="100%" TabIndex="1"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Bairro:
                                <asp:RequiredFieldValidator ID="rfBairro" runat="server" ErrorMessage="*" ControlToValidate="tbBairro"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbBairro" runat="server" TabIndex="2"></asp:TextBox></asp:TableCell>
                            <asp:TableCell>CEP:
                                <asp:RequiredFieldValidator ID="rfCep" runat="server" ErrorMessage="*" ControlToValidate="tbCEP"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbCEP" runat="server" TabIndex="3"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Contato:
                                <asp:RequiredFieldValidator ID="rfContato" runat="server" ErrorMessage="*" ControlToValidate="tbContato"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbContato" runat="server" TabIndex="4"></asp:TextBox></asp:TableCell>
                            <asp:TableCell>Telefone
                                <asp:RequiredFieldValidator ID="rfTelefone" runat="server" ErrorMessage="*" ControlToValidate="tbTelefone"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbTelefone" runat="server" TabIndex="5"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Email:
                                <asp:RequiredFieldValidator ID="rfEmail" runat="server" ErrorMessage="*" ControlToValidate="tbEmailContato"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3"><asp:TextBox ID="tbEmailContato" runat="server" Width="100%" TabIndex="6"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>% Atual: 
                                <asp:RegularExpressionValidator ID="rfValorAtual" runat="server" Display="None" ErrorMessage="*" ControlToValidate="tbValorAtual" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbValorAtual" runat="server" TabIndex="7"></asp:TextBox></asp:TableCell>
                            <asp:TableCell>Dur. Estimada (d):
                                <asp:RegularExpressionValidator ID="rfDuracao" runat="server" Display="None" ErrorMessage="*" ControlToValidate="tbDurabilidadeEstimada" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbDurabilidadeEstimada" runat="server" TabIndex="8"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Contador:
                                <asp:RegularExpressionValidator ID="rftbContador" runat="server" Display="None" ErrorMessage="*" ControlToValidate="tbContador" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbContador" runat="server" TabIndex="9"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Cod. USD: 
                                <asp:RequiredFieldValidator ID="rfUSD" runat="server" ErrorMessage="*" ControlToValidate="tbUSD"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                            <asp:TableCell><asp:TextBox ID="tbUSD" runat="server" TabIndex="10"></asp:TextBox></asp:TableCell>
                            <asp:TableCell>Suprimento:</asp:TableCell>
                            <asp:TableCell><asp:DropDownList ID="dpSuprimento" runat="server" Width="100%" TabIndex="11">
                                <asp:ListItem Text="Tonner" Value="Tonner" Selected="True"></asp:ListItem> 
                                <asp:ListItem Text="Cilindro" Value="Cilindro"></asp:ListItem> 
                                           </asp:DropDownList></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">Suprimento anterior apresentou falha? </asp:TableCell>
                            <asp:TableCell><asp:RadioButton ID="rbFalhaNao" runat="server" Checked="true" Text="Não" GroupName="Falha"  TabIndex="12"/><asp:RadioButton ID="rbFalhaSim" runat="server"  Text="Sim" GroupName="Falha" TabIndex="13"/></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Obs:</asp:TableCell>
                            <asp:TableCell ColumnSpan="3"><asp:TextBox ID="tbObs" runat="server" Width="100%" TextMode="MultiLine" TabIndex="14"></asp:TextBox></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button ID="btSolicitar" runat="server" Text="Solicitar" Width="60px" OnClick="btSolicitar_Click" />
                    &nbsp;
                    <asp:Button ID="btLimpar" runat="server" Text="Limpar" Width="60px" OnClick="btLimpar_Click" />
                </fieldset>

                <h2>Chamados Abertos</h2>
                <asp:GridView ID="gvChamadosAbertos" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsChamadosAbertos" ForeColor="#333333" GridLines="None" DataKeyNames="idreqSuprimento" OnRowEditing="gvChamadosAbertos_RowEditing">

                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="idreqSuprimento" HeaderText="ID" SortExpression="idreqSuprimento" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                        <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                        <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
                        <asp:BoundField DataField="emailUsuario" HeaderText="E-mail" SortExpression="emailUsuario" />
                        <asp:BoundField DataField="suprimento" HeaderText="Suprimento" SortExpression="suprimento" />
                        <asp:BoundField DataField="solicitante" HeaderText="Solicitante" SortExpression="solicitante" />
                        <asp:BoundField DataField="codUsd" HeaderText="USD" SortExpression="codUsd" />
                        <asp:BoundField DataField="dataSolicitacao" HeaderText="Data" SortExpression="dataSolicitacao" DataFormatString="{0:d}" />
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
                <asp:SqlDataSource ID="dsChamadosAbertos" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                    SelectCommand="SELECT [idreqSuprimento], [uf], [cidade], [serie], [emailUsuario], [suprimento], [solicitante], [codUsd], [dataSolicitacao] FROM [reqSuprimentos] WHERE (([status] = @status) AND ([serie] = @serie))" 
                    DeleteCommand="DELETE FROM [reqSuprimentos] WHERE [idreqSuprimento] = @idreqSuprimento" 
                    InsertCommand="INSERT INTO [reqSuprimentos] ([uf], [cidade], [serie], [emailUsuario], [suprimento], [solicitante], [codUsd], [dataSolicitacao]) VALUES (@uf, @cidade, @serie, @emailUsuario, @suprimento, @solicitante, @codUsd, @dataSolicitacao)" 
                    UpdateCommand="UPDATE [reqSuprimentos] SET [uf] = @uf, [cidade] = @cidade, [serie] = @serie, [emailUsuario] = @emailUsuario, [suprimento] = @suprimento, [solicitante] = @solicitante, [codUsd] = @codUsd, [dataSolicitacao] = @dataSolicitacao WHERE [idreqSuprimento] = @idreqSuprimento">
                    
                    <DeleteParameters>
                        <asp:Parameter Name="idreqSuprimento" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="uf" Type="String" />
                        <asp:Parameter Name="cidade" Type="String" />
                        <asp:Parameter Name="serie" Type="String" />
                        <asp:Parameter Name="emailUsuario" Type="String" />
                        <asp:Parameter Name="suprimento" Type="String" />
                        <asp:Parameter Name="solicitante" Type="String" />
                        <asp:Parameter Name="codUsd" Type="String" />
                        <asp:Parameter Name="dataSolicitacao" Type="DateTime" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Aberto" Name="status" Type="String" />
                        <asp:ControlParameter ControlID="dpSeries" Name="serie" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="uf" Type="String" />
                        <asp:Parameter Name="cidade" Type="String" />
                        <asp:Parameter Name="serie" Type="String" />
                        <asp:Parameter Name="emailUsuario" Type="String" />
                        <asp:Parameter Name="suprimento" Type="String" />
                        <asp:Parameter Name="solicitante" Type="String" />
                        <asp:Parameter Name="codUsd" Type="String" />
                        <asp:Parameter Name="dataSolicitacao" Type="DateTime" />
                        <asp:Parameter Name="idreqSuprimento" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </asp:View>
        </asp:MultiView>
        
    </asp:Panel>
</asp:Content>

