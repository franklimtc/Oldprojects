<%@ Page Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeFile="Acompanhamento.aspx.cs" Inherits="Home" Title="CSF Digital" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Cabecalho" ContentPlaceHolderID="ContentPlaceHolderCabecalho" runat="server">
    <div class="menuHomeAtivo">  
        <a href="Home.aspx" class="link">home</a>      	
    </div>
    <div class="menuRelatorio">  
        <a href="Relatorio.aspx" class="link">relatorio</a>      	
    </div>
    <div class="menuMonitor">  
        <a href="Monitor.aspx" class="link">monitor</a>      	
    </div>
    <div class="menuConfiguracao">  
        <a href="Configuracao.aspx" class="link">configuracao</a>      	
    </div>
    <div class="menuSuporte">  
        <a href="Suporte.aspx" class="link">suporte</a>      	
    </div>
</asp:Content>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div id="conteudoCentro" runat="server" style="margin: -7px 135px;" >
        <table class= "content" width="100%">
            <tr>
                <td align="center">
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadButton ID="rbtExportarExcel" runat="server" Text="Excel" onclick="rbtExportarExcel_Click"></telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="rbtExportarCSV" runat="server" Text="CSV" 
                                    onclick="rbtExportarCSV_Click"></telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="rbtExportarWord" runat="server" Text="Word" 
                                    onclick="rbtExportarWord_Click"></telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="rbtExportarPDF" runat="server" Text="PDF" 
                                    onclick="rbtExportarPDF_Click"></telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%">
                    <telerik:RadGrid ID="RadGridAcompanhamento" runat="server" height="768px" CellSpacing="0" 
                        GridLines="Horizontal" ShowGroupPanel="True" ShowFooter="True" 
                        ShowStatusBar="True" DataSourceID="ObjectDataSourceAcao" 
                        AutoGenerateColumns="False">
                        <HeaderContextMenu CssClass="GridContextMenu_Default"></HeaderContextMenu>
                        <GroupPanel Text="">
                        </GroupPanel>
                        <MasterTableView 
                            DataSourceID="ObjectDataSourceAcao" NoDetailRecordsText="" 
                            NoMasterRecordsText="Não há chamados a serem listados.">
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Data_Acao" 
                                    FilterControlAltText="Filter Data_Acao column" HeaderText="Data da ação" 
                                    SortExpression="Data_Acao" UniqueName="Data_Acao" 
                                    DataType="System.DateTime">
                                    <FooterStyle Width="120px" Wrap="False" />
                                    <HeaderStyle Width="120px" Wrap="False" />
                                    <ItemStyle Width="120px" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Atuante" 
                                    FilterControlAltText="Filter Atuante column" HeaderText="Atuante" 
                                    SortExpression="Atuante" UniqueName="Atuante">
                                    <FooterStyle Width="300px" Wrap="False" />
                                    <HeaderStyle Width="300px" Wrap="False" />
                                    <ItemStyle Width="300px" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Descricao" 
                                    FilterControlAltText="Filter Descricao column" HeaderText="Descrição" 
                                    SortExpression="Descricao" UniqueName="Descricao">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Descricao_FluxoAtual" 
                                    FilterControlAltText="Filter Descricao_FluxoAtual column" HeaderText="Fluxo atual" 
                                    SortExpression="Descricao_FluxoAtual" UniqueName="Descricao_FluxoAtual">
                                </telerik:GridBoundColumn>  
                                <telerik:GridBoundColumn DataField="Descricao_FluxoAlternativo" 
                                    FilterControlAltText="Filter Descricao_FluxoAlternativo column" HeaderText="Fluxo alternativo" 
                                    SortExpression="Descricao_FluxoAlternativo" 
                                    UniqueName="Descricao_FluxoAlternativo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CodigoFluxo" 
                                    FilterControlAltText="Filter CodigoFluxo column" HeaderText="Código do fluxo" 
                                    SortExpression="CodigoFluxo" UniqueName="CodigoFluxo">
                                    <FooterStyle Width="150px" Wrap="False" />
                                    <HeaderStyle Width="150px" Wrap="False" />
                                    <ItemStyle Width="150px" Wrap="False" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField  FieldAlias="Responsavel" FieldName="Responsavel" 
                                            FormatString="" HeaderText="Responsável" SortOrder="Ascending" />
                                        <telerik:GridGroupByField FieldAlias="Referencia" FieldName="Referencia" 
                                            FormatString="" HeaderText="Chamado" SortOrder="Ascending" />
                                        <telerik:GridGroupByField  FieldAlias="TempoRestanteSLAContingencia" FieldName="TempoRestanteSLAContingencia" 
                                            FormatString="" HeaderText="SLA de Contingência restante" SortOrder="Ascending" />
                                        <telerik:GridGroupByField  FieldAlias="TempoRestanteSLAFinal" FieldName="TempoRestanteSLAFinal" 
                                            FormatString="" HeaderText="SLA Final restante" SortOrder="Ascending" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Responsavel" FieldName="Responsavel" 
                                            FormatString="" HeaderText="Responsável" SortOrder="Ascending" />
                                        <telerik:GridGroupByField FieldAlias="Referencia" FieldName="Referencia" 
                                            FormatString="" HeaderText="Chamado" SortOrder="Ascending" />
                                        <telerik:GridGroupByField FieldAlias="TempoRestanteSLAContingencia" FieldName="TempoRestanteSLAContingencia" 
                                            FormatString="" HeaderText="SLA de Contingência restante" SortOrder="Ascending" />
                                        <telerik:GridGroupByField FieldAlias="TempoRestanteSLAFinal" FieldName="TempoRestanteSLAFinal" 
                                            FormatString="" HeaderText="SLA Final restante" SortOrder="Ascending" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <GroupingSettings CollapseTooltip="Recolher" ExpandTooltip="Expandir" GroupSplitDisplayFormat="Exibindo {0} de {1} itens." UnGroupButtonTooltip="Desagrupar" UnGroupTooltip="Arraste o item para fora da barra para desagrupar" />
                        <ClientSettings AllowDragToGroup="True">
                            <Selecting AllowRowSelect="True" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                        <FilterMenu EnableImageSprites="True"></FilterMenu>
                        <ExportSettings>
                            <Pdf FontType="Subset" PaperSize="Letter" />
                            <Excel Format="Html" />
                            <Csv ColumnDelimiter="Colon" RowDelimiter="NewLine" />
                        </ExportSettings>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSourceAcao" runat="server" 
                        SelectMethod="RetornaAcoes" TypeName="CSFDigital.Controls.Acao">
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Rodape" ContentPlaceHolderID="ContentPlaceHolderRodape" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRodape" runat="server">
        <ContentTemplate>
            <asp:Timer ID="TimerInfoRodape" Interval="60000" runat="server" ontick="TimerInfoRodape_Tick"></asp:Timer>
            <asp:Label ID="lbInfoRodape" runat="server" Text="Espaço reservado ao cliente."></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>