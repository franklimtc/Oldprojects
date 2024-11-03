<%@ Page Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" Title="CSF Digital" %>
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
                <td align="left" valign="top" width="100%">
                    <telerik:RadGrid ID="RadGridAvisos" runat="server" CellSpacing="0" 
                        GridLines="Horizontal" ShowFooter="True" 
                        ShowStatusBar="True" DataSourceID="ObjectDataSource1" 
                        AutoGenerateColumns="False" GroupingEnabled="False">
                        <HeaderContextMenu CssClass="GridContextMenu_Default"></HeaderContextMenu>
                        <MasterTableView NoDetailRecordsText="Não há itens." 
                            NoMasterRecordsText="Não há itens." DataSourceID="ObjectDataSource1">
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id" DataType="System.Int32" 
                                    FilterControlAltText="Filter Id column" HeaderText="#" SortExpression="Id" 
                                    UniqueName="Id" AllowFiltering="False" AllowSorting="False" 
                                    Groupable="False" Reorderable="False" Resizable="False" ShowSortIcon="False" >
                                    <FooterStyle Width="20px" Wrap="False" />
                                    <HeaderStyle Width="20px" Wrap="False" />
                                    <ItemStyle Width="20px" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CriadoEm" DataType="System.DateTime" 
                                    FilterControlAltText="Filter CriadoEm column" HeaderText="Data" 
                                    SortExpression="CriadoEm" UniqueName="CriadoEm">
                                    <FooterStyle Width="120px" Wrap="False" />
                                    <HeaderStyle Width="120px" Wrap="False" />
                                    <ItemStyle Width="120px" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CriadoPor" 
                                    FilterControlAltText="Filter CriadoPor column" HeaderText="Criado Por" 
                                    SortExpression="CriadoPor" UniqueName="CriadoPor">
                                    <FooterStyle Width="300px" Wrap="False" />
                                    <HeaderStyle Width="300px" Wrap="False" />
                                    <ItemStyle Width="300px" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Descricao" 
                                    FilterControlAltText="Filter Descricao column" HeaderText="Descrição" 
                                    SortExpression="Descricao" UniqueName="Descricao">
                                </telerik:GridBoundColumn>
                            </Columns>
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
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="RetornaAvisos" TypeName="CSFDigital.Controls.Aviso">
                    </asp:ObjectDataSource>
                    <%--<table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Avisos" Font-Bold="True" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Table ID="tblAvisos" runat="server" BorderStyle="Ridge" BorderWidth="1px" GridLines="None">
                                    <asp:TableHeaderRow HorizontalAlign="Justify" VerticalAlign="Top" BorderStyle="None">
                                        <asp:TableHeaderCell Text="#" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Text="Criado em" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Text="Descrição" HorizontalAlign="Left" VerticalAlign="Middle"></asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="01" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="31/07/2011" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="Importante:  Após 48 horas sem retorno, a solicitação deverá ser fechada." HorizontalAlign="Left" VerticalAlign="Middle"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="02" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="31/07/2011" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="A solicitação deverá ser fechada após confirmação do usuário." HorizontalAlign="Left" VerticalAlign="Middle"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="03" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="31/07/2011" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="O tempo válido para o SLA corresponde ao período de 08:00 ás 18:00 de segunda a sexta." HorizontalAlign="Left" VerticalAlign="Middle"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="04" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="31/07/2011" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="O tempo que o chamado permanecer com o status 'Pendente c/ Usuário' não será considerado." HorizontalAlign="Left" VerticalAlign="Middle"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="05" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="31/07/2011" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell Text="Caso o chamado não for de responsábilidade da CSF Digital deverá ser fechado ou encaminhado para equipe correta." HorizontalAlign="Left" VerticalAlign="Middle"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                    </table>--%>
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