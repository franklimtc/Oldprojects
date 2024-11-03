<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Correios_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnOcorrencia" runat="server" Visible="false">
        
        <h2>Editar Ocorrência dos Correios</h2>
        <br />
        <table>
            <tr>
                <th>ID</th>
                <th>Postagem</th>
                <th>Operador</th>
                <th>Protocolo</th>
                <th>Abertura</th>
                <th>Fechamento</th>
                <th>Observações</th>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbId" runat="server" Width="30px"></asp:TextBox></td>
                <td><asp:TextBox ID="tbPostagem" runat="server" Width="120px"></asp:TextBox></td>
                <td><asp:TextBox ID="tbOperador" runat="server" Width="80px"></asp:TextBox></td>
                <td><asp:TextBox ID="tbProtocolo" runat="server" Width="80px"></asp:TextBox></td>
                <td><asp:TextBox ID="tbAbertura" runat="server" Width="80px"></asp:TextBox></td>
                <td><asp:TextBox ID="tbFechamento" runat="server" Width="80px"></asp:TextBox></td>
                <td><asp:TextBox ID="tbObs" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox></td>
            </tr>
        </table>
      
        <asp:Button ID="btAtualizar" runat="server" Text="Atualizar" OnClick="btAtualizar_Click" />
        <%--<asp:Button ID="btcancelar" runat="server" Text="Cancelar" OnClick="btcancelar_Click" />--%>
        <asp:Button ID="btConcluir" runat="server" Text="Concluir" OnClick="btConcluir_Click" />
        <br />
        <asp:Panel ID="pnConcluir" runat="server" Visible="false">
            <span>A solicitação foi deferida?</span><br />
            <asp:Button ID="btSim" runat="server" Text="Sim" OnClick="btSim_Click" />
            &nbsp;
            <asp:Button ID="btNao" runat="server" Text="Não" OnClick="btNao_Click" />
        </asp:Panel>

        <br />
        <asp:Panel ID="pnReverso" runat="server" Visible="false">
            <asp:GridView ID="gvReverso" runat="server" DataSourceID="dsReverso"></asp:GridView>
            <asp:SqlDataSource ID="dsReverso" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select cepOrigem, cepDestino, postagem, dtEnvio, dtEntrega, prazoEntrega, entregueEm from postagensReversas where postagem = 'LE247163982BR'">

            </asp:SqlDataSource>
        </asp:Panel>

        <asp:Panel ID="pnDadosEquipamento" runat="server">
             <h3>Dados do Equipamento</h3>
        <asp:GridView ID="gvDadosEqpto" runat="server" DataSourceID="dsDadosEqpto" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvDadosEqpto_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="UF" HeaderText="UF" SortExpression="UF" />
                <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" />
                <asp:BoundField DataField="Endereco" HeaderText="Endereco" SortExpression="Endereco" />
                <asp:BoundField DataField="Bairro" HeaderText="Bairro" SortExpression="Bairro" />
                <asp:BoundField DataField="Cep" HeaderText="Cep" SortExpression="Cep" />
                <asp:BoundField DataField="Contato" HeaderText="Contato" SortExpression="Contato" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btCorrigirCep" 
                            Text="Corrigir Cep" CommandName="Corrigir" 
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
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
        <asp:SqlDataSource ID="dsDadosEqpto" runat="server"  ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                SelectCommand="SELECT * FROM [equipamentos]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [equipamentos] WHERE [idEquipamento] = @original_idEquipamento AND (([idCliente] = @original_idCliente) OR ([idCliente] IS NULL AND @original_idCliente IS NULL)) AND (([idModelo] = @original_idModelo) OR ([idModelo] IS NULL AND @original_idModelo IS NULL)) AND (([serie] = @original_serie) OR ([serie] IS NULL AND @original_serie IS NULL)) AND (([uf] = @original_uf) OR ([uf] IS NULL AND @original_uf IS NULL)) AND (([cidade] = @original_cidade) OR ([cidade] IS NULL AND @original_cidade IS NULL)) AND (([endereco] = @original_endereco) OR ([endereco] IS NULL AND @original_endereco IS NULL)) AND (([bairro] = @original_bairro) OR ([bairro] IS NULL AND @original_bairro IS NULL)) AND (([cep] = @original_cep) OR ([cep] IS NULL AND @original_cep IS NULL)) AND (([contato] = @original_contato) OR ([contato] IS NULL AND @original_contato IS NULL)) AND (([fone] = @original_fone) OR ([fone] IS NULL AND @original_fone IS NULL)) AND (([email] = @original_email) OR ([email] IS NULL AND @original_email IS NULL)) AND (([status] = @original_status) OR ([status] IS NULL AND @original_status IS NULL)) AND (([unidade] = @original_unidade) OR ([unidade] IS NULL AND @original_unidade IS NULL)) AND (([setor] = @original_setor) OR ([setor] IS NULL AND @original_setor IS NULL)) AND (([dtAtualizacao] = @original_dtAtualizacao) OR ([dtAtualizacao] IS NULL AND @original_dtAtualizacao IS NULL)) AND (([atualizadoPor] = @original_atualizadoPor) OR ([atualizadoPor] IS NULL AND @original_atualizadoPor IS NULL)) AND (([idTecnico] = @original_idTecnico) OR ([idTecnico] IS NULL AND @original_idTecnico IS NULL))" InsertCommand="INSERT INTO [equipamentos] ([idCliente], [idModelo], [serie], [uf], [cidade], [endereco], [bairro], [cep], [contato], [fone], [email], [status], [unidade], [setor], [dtAtualizacao], [atualizadoPor], [idTecnico]) VALUES (@idCliente, @idModelo, @serie, @uf, @cidade, @endereco, @bairro, @cep, @contato, @fone, @email, @status, @unidade, @setor, @dtAtualizacao, @atualizadoPor, @idTecnico)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [equipamentos] SET [idCliente] = @idCliente, [idModelo] = @idModelo, [serie] = @serie, [uf] = @uf, [cidade] = @cidade, [endereco] = @endereco, [bairro] = @bairro, [cep] = @cep, [contato] = @contato, [fone] = @fone, [email] = @email, [status] = @status, [unidade] = @unidade, [setor] = @setor, [dtAtualizacao] = @dtAtualizacao, [atualizadoPor] = @atualizadoPor, [idTecnico] = @idTecnico WHERE [idEquipamento] = @original_idEquipamento AND (([idCliente] = @original_idCliente) OR ([idCliente] IS NULL AND @original_idCliente IS NULL)) AND (([idModelo] = @original_idModelo) OR ([idModelo] IS NULL AND @original_idModelo IS NULL)) AND (([serie] = @original_serie) OR ([serie] IS NULL AND @original_serie IS NULL)) AND (([uf] = @original_uf) OR ([uf] IS NULL AND @original_uf IS NULL)) AND (([cidade] = @original_cidade) OR ([cidade] IS NULL AND @original_cidade IS NULL)) AND (([endereco] = @original_endereco) OR ([endereco] IS NULL AND @original_endereco IS NULL)) AND (([bairro] = @original_bairro) OR ([bairro] IS NULL AND @original_bairro IS NULL)) AND (([cep] = @original_cep) OR ([cep] IS NULL AND @original_cep IS NULL)) AND (([contato] = @original_contato) OR ([contato] IS NULL AND @original_contato IS NULL)) AND (([fone] = @original_fone) OR ([fone] IS NULL AND @original_fone IS NULL)) AND (([email] = @original_email) OR ([email] IS NULL AND @original_email IS NULL)) AND (([status] = @original_status) OR ([status] IS NULL AND @original_status IS NULL)) AND (([unidade] = @original_unidade) OR ([unidade] IS NULL AND @original_unidade IS NULL)) AND (([setor] = @original_setor) OR ([setor] IS NULL AND @original_setor IS NULL)) AND (([dtAtualizacao] = @original_dtAtualizacao) OR ([dtAtualizacao] IS NULL AND @original_dtAtualizacao IS NULL)) AND (([atualizadoPor] = @original_atualizadoPor) OR ([atualizadoPor] IS NULL AND @original_atualizadoPor IS NULL)) AND (([idTecnico] = @original_idTecnico) OR ([idTecnico] IS NULL AND @original_idTecnico IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_idEquipamento" Type="Int32" />
                <asp:Parameter Name="original_idCliente" Type="Int32" />
                <asp:Parameter Name="original_idModelo" Type="Int32" />
                <asp:Parameter Name="original_serie" Type="String" />
                <asp:Parameter Name="original_uf" Type="String" />
                <asp:Parameter Name="original_cidade" Type="String" />
                <asp:Parameter Name="original_endereco" Type="String" />
                <asp:Parameter Name="original_bairro" Type="String" />
                <asp:Parameter Name="original_cep" Type="String" />
                <asp:Parameter Name="original_contato" Type="String" />
                <asp:Parameter Name="original_fone" Type="String" />
                <asp:Parameter Name="original_email" Type="String" />
                <asp:Parameter Name="original_status" Type="String" />
                <asp:Parameter Name="original_unidade" Type="String" />
                <asp:Parameter Name="original_setor" Type="String" />
                <asp:Parameter Name="original_dtAtualizacao" Type="DateTime" />
                <asp:Parameter Name="original_atualizadoPor" Type="String" />
                <asp:Parameter Name="original_idTecnico" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="idCliente" Type="Int32" />
                <asp:Parameter Name="idModelo" Type="Int32" />
                <asp:Parameter Name="serie" Type="String" />
                <asp:Parameter Name="uf" Type="String" />
                <asp:Parameter Name="cidade" Type="String" />
                <asp:Parameter Name="endereco" Type="String" />
                <asp:Parameter Name="bairro" Type="String" />
                <asp:Parameter Name="cep" Type="String" />
                <asp:Parameter Name="contato" Type="String" />
                <asp:Parameter Name="fone" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="unidade" Type="String" />
                <asp:Parameter Name="setor" Type="String" />
                <asp:Parameter Name="dtAtualizacao" Type="DateTime" />
                <asp:Parameter Name="atualizadoPor" Type="String" />
                <asp:Parameter Name="idTecnico" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="idCliente" Type="Int32" />
                <asp:Parameter Name="idModelo" Type="Int32" />
                <asp:Parameter Name="serie" Type="String" />
                <asp:Parameter Name="uf" Type="String" />
                <asp:Parameter Name="cidade" Type="String" />
                <asp:Parameter Name="endereco" Type="String" />
                <asp:Parameter Name="bairro" Type="String" />
                <asp:Parameter Name="cep" Type="String" />
                <asp:Parameter Name="contato" Type="String" />
                <asp:Parameter Name="fone" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="status" Type="String" />
                <asp:Parameter Name="unidade" Type="String" />
                <asp:Parameter Name="setor" Type="String" />
                <asp:Parameter Name="dtAtualizacao" Type="DateTime" />
                <asp:Parameter Name="atualizadoPor" Type="String" />
                <asp:Parameter Name="idTecnico" Type="Int32" />
                <asp:Parameter Name="original_idEquipamento" Type="Int32" />
                <asp:Parameter Name="original_idCliente" Type="Int32" />
                <asp:Parameter Name="original_idModelo" Type="Int32" />
                <asp:Parameter Name="original_serie" Type="String" />
                <asp:Parameter Name="original_uf" Type="String" />
                <asp:Parameter Name="original_cidade" Type="String" />
                <asp:Parameter Name="original_endereco" Type="String" />
                <asp:Parameter Name="original_bairro" Type="String" />
                <asp:Parameter Name="original_cep" Type="String" />
                <asp:Parameter Name="original_contato" Type="String" />
                <asp:Parameter Name="original_fone" Type="String" />
                <asp:Parameter Name="original_email" Type="String" />
                <asp:Parameter Name="original_status" Type="String" />
                <asp:Parameter Name="original_unidade" Type="String" />
                <asp:Parameter Name="original_setor" Type="String" />
                <asp:Parameter Name="original_dtAtualizacao" Type="DateTime" />
                <asp:Parameter Name="original_atualizadoPor" Type="String" />
                <asp:Parameter Name="original_idTecnico" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        </asp:Panel>
       
        <asp:Panel ID="pnCep" runat="server" Visible="false">
           <table>
               <tr>
                   <th>CEP Antigo</th>
                   <th>CEP Novo</th>
               </tr>
               <tr>
                   <td><asp:TextBox ID="tbcepAntigo" runat="server" Enabled="false"></asp:TextBox></td>
                   <td><asp:TextBox ID="tbcepNovo" runat="server" Enabled="True"></asp:TextBox></td>
               </tr>
           </table>
            <asp:Button ID="btCorrigirCep" runat="server" Text="Corrigir" OnClick="btCorrigirCep_Click" />
        </asp:Panel>
        <br />
            <h2>CSF DIGITAL</h2>
            <h2></h2>
            <p>
                Rua Raimundo Oliveira Filho, nº 332, Papicu, Fortaleza-CE.</p>
            <p>
                CEP: 60175-175 - Fone: (85) 3022-0900</p>
            <p> CNPJ: 08953969000199</p>
        <h2>Sites Auxiliares</h2>
            <p><a href="http://www2.correios.com.br/sistemas/falecomoscorreios/">Fale com os Correios</a></p>
            <p><a href="http://www2.correios.com.br/sistemas/precosPrazos/">Cálculo de preços e prazos de entrega</a></p>
            <p><a href="http://www2.correios.com.br/sistemas/rastreamento/">Rastreamento de objetos</a></p>
    </asp:Panel>
    <asp:Panel ID="pnResumo" runat="server">
        <table>
            <tr>
                <td style="width:300px;vertical-align:baseline">
<h2>Resumo das Solicitações</h2><br />
 <asp:GridView ID="gvResumo" runat="server" DataSourceID="dsChart" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
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
        <asp:SqlDataSource ID="dsChart" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="SELECT  CASE WHEN status IS NULL THEN 'Disponivel' ELSE status END as 'Status', count(*) 'total' FROM vw_ChamadosCorreios group by CASE WHEN status IS NULL THEN 'Pendentes' ELSE 'Abertas' END, status order by 1"></asp:SqlDataSource>
    <br />
                </td>
                <td style="vertical-align:baseline; width:300px">
  <h2>CSF DIGITAL</h2>
            <h2></h2>
            <p>Rua Raimundo Oliveira Filho, nº 332.</p>
            <p>Papicu, Fortaleza-CE</p>
            <p>CEP: 60175-175 - Fone: (85) 3022-0900</p>
            <p>CNPJ: 08953969000199</p>
        

                </td>
                <td style="vertical-align:baseline">
                    <h2>Sites Auxiliares</h2>
            <p><a href="http://www2.correios.com.br/sistemas/falecomoscorreios/">Fale com os Correios</a></p>
            <p><a href="http://www2.correios.com.br/sistemas/precosPrazos/">Cálculo de preços e prazos de entrega</a></p>
            <p><a href="http://www2.correios.com.br/sistemas/rastreamento/">Rastreamento de objetos</a></p>
                </td>
            </tr>
        </table>
       
    </asp:Panel>
    
    <asp:Panel ID="pnGeral" runat="server">
        <h2>Lista de Postagens fora do Prazo</h2>
        <br />
        <asp:GridView ID="gvCorreios" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="4" DataSourceID="dsCorreios" ForeColor="#333333" GridLines="None" OnRowCommand="gvCorreios_RowCommand" OnSelectedIndexChanged="gvCorreios_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btAvaliar" 
                            Text="Avaliar" CommandName="Avaliar" 
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="origem" HeaderText="origem" ReadOnly="True" SortExpression="origem" />
                <asp:BoundField DataField="destino" HeaderText="destino" SortExpression="destino" />
                <asp:BoundField DataField="Série" HeaderText="Série" ReadOnly="True" SortExpression="Série" />
                <asp:BoundField DataField="Postagem" HeaderText="Postagem" ReadOnly="True" SortExpression="Postagem" />
                <asp:BoundField DataField="tpEnvio" DataFormatString="{0:d}" HeaderText="tpEnvio" SortExpression="tpEnvio" />
                <asp:BoundField DataField="dtEnvio" DataFormatString="{0:d}" HeaderText="dtEnvio" SortExpression="dtEnvio" />
                <asp:BoundField DataField="dtEntrega" DataFormatString="{0:d}" HeaderText="dtEntrega" SortExpression="dtEntrega" />
                <asp:BoundField DataField="prazoEntrega" HeaderText="prazoEntrega" SortExpression="prazoEntrega" />
                <asp:BoundField DataField="entregueEm" HeaderText="entregueEm" ReadOnly="True" SortExpression="entregueEm" />
                <asp:BoundField DataField="operador" HeaderText="operador" SortExpression="operador" />
                <asp:BoundField DataField="protocolo" HeaderText="protocolo" SortExpression="protocolo" />
                <asp:BoundField DataField="dtAbertura" DataFormatString="{0:d}" HeaderText="dtAbertura" SortExpression="dtAbertura" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btAtualizar" 
                            Text="Atualizar" CommandName="Atualizar" 
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
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
        <asp:SqlDataSource ID="dsCorreios" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select '60175175' 'origem', b.cep 'destino', UPPER(a.serie) 'Série', UPPER(a.postagem) 'Postagem', a.tpEnvio, a.dtEnvio, a.dtEntrega, a.prazoEntrega, a.entregueEm 
    , c.operador, c.protocolo, c.dtAbertura, c.status
    from enviosSuprimentos AS a 
    left join equipamentos as b on a.serie = b.serie
    left join chamadosCorreios as c on a.postagem = c.postagem
    where a.entregueEm &gt; a.prazoEntrega and a.verificado = 0 and c.status not in ('Atendido')" 
            UpdateCommand="UPDATE chamadosCorreios set protocolo = @protocolo, dtAbertura = getdate() where postagem = @postagem">
            <UpdateParameters>
                <asp:Parameter Name="protocolo" />
                <asp:Parameter Name="postagem" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </asp:Panel>
    </asp:Content>

