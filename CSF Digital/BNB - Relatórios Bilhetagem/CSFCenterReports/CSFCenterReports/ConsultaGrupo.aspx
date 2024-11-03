<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="ConsultaGrupo.aspx.cs" Inherits="CSFCenterReports.ConsultaGrupo" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ObjectDataSource ID="ObjectDataSourceUF" runat="server" 
                    SelectMethod="RetornaListaUf" TypeName="CSFCenterReports.Controls.Grupo">
                    <SelectParameters>
                        <asp:Parameter Name="ListaGrupos" Type="Object" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceCidade" runat="server" 
                    SelectMethod="RetornaListaCidades" TypeName="CSFCenterReports.Controls.Grupo">
                    <SelectParameters>
                        <asp:Parameter Name="uf" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceUnidade" runat="server" 
                    SelectMethod="RetornaListaUnidades" TypeName="CSFCenterReports.Controls.Grupo">
                    <SelectParameters>
                        <asp:Parameter Name="uf" Type="String" />
                        <asp:Parameter Name="cidade" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceSetor" runat="server" 
                    SelectMethod="RetornaListaSetores" TypeName="CSFCenterReports.Controls.Grupo">
                    <SelectParameters>
                        <asp:Parameter Name="uf" Type="String" />
                        <asp:Parameter Name="cidade" Type="String" />
                        <asp:Parameter Name="unidade" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Panel ID="pnlConsulta" runat="server" Visible="True">
                    <table width="100%">
                        <tr>
                            <td style="width: 80px;">
                                <asp:Label ID="lbConsultaCodigo" runat="server" Text="Codigo"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbConsultaCodigo" runat="server" Width="80px" 
                                    AutoPostBack="true" 
                                    onselectedindexchanged="rcbConsultaCodigo_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                <asp:Label ID="lbConsultaUf" runat="server" Text="UF"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbConsultaUf" runat="server" Width="200px" 
                                    AutoPostBack="True" DataSourceID="ObjectDataSourceUF" 
                                    onselectedindexchanged="rcbConsultaUf_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                <asp:Label ID="lbConsultaCidade" runat="server" Text="Cidade"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbConsultaCidade" runat="server" Width="400px" 
                                    AutoPostBack="true" DataSourceID="ObjectDataSourceCidade" 
                                    onselectedindexchanged="rcbConsultaCidade_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                <asp:Label ID="lbConsultaUnidade" runat="server" Text="Unidade"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbConsultaUnidade" runat="server" Width="400px" 
                                    AutoPostBack="true" DataSourceID="ObjectDataSourceUnidade" 
                                    onselectedindexchanged="rcbConsultaUnidade_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                <asp:Label ID="lbConsultaSetor" runat="server" Text="Setor"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbConsultaSetor" runat="server" Width="400px" 
                                    DataSourceID="ObjectDataSourceSetor" 
                                    onselectedindexchanged="rcbConsultaSetor_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px;">
                                <asp:Label ID="lbConsultaStatus" runat="server" Text="Ativo ?"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbConsultaStatus" runat="server" Width="70px" 
                                    onselectedindexchanged="rcbConsultaStatus_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Selected="True" Text="Sim" 
                                            Value="true" />
                                        <telerik:RadComboBoxItem runat="server" Text="Não" Value="false" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="right">
                                <telerik:RadButton ID="rbtnConsultar" runat="server" Text="Consultar" 
                                    Width="100px" onclick="rbtnConsultar_Click">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadGrid ID="rgrdGrupos" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                    CellSpacing="0" Culture="Portuguese (Brazil)" 
                                    DataSourceID="ObjectDataSourceGrupos" GridLines="Horizontal" 
                                    ShowGroupPanel="True" Width="100%"
                                    onitemcommand="rgrdGrupos_ItemCommand" >
                                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                                    <MasterTableView DataSourceID="ObjectDataSourceGrupos">
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn DataTextField="Codigo" HeaderText="Código"
                                            FilterControlAltText="Filter Codigo column"
                                            SortExpression="Codigo" UniqueName="Codigo" CommandName="Listar">
                                                <FooterStyle Width="70px" Wrap="False" />
                                                <HeaderStyle Width="70px" Wrap="False" />
                                                <ItemStyle Width="70px" Wrap="False" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="Uf" 
                                                FilterControlAltText="Filter Uf column" HeaderText="Uf" 
                                                SortExpression="Uf" UniqueName="Uf">
                                                <FooterStyle Width="50px" Wrap="False" />
                                                <HeaderStyle Width="50px" Wrap="False" />
                                                <ItemStyle Width="50px" Wrap="False" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cidade" 
                                                FilterControlAltText="Filter Cidade column" HeaderText="Cidade" 
                                                SortExpression="Cidade" UniqueName="Cidade">
                                                <FooterStyle Width="250px" Wrap="False" />
                                                <HeaderStyle Width="250px" Wrap="False" />
                                                <ItemStyle Width="250px" Wrap="False" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Unidade" 
                                                FilterControlAltText="Filter Unidade column" HeaderText="Unidade" 
                                                SortExpression="Unidade" UniqueName="Unidade">
                                                <FooterStyle Width="200px" Wrap="False" />
                                                <HeaderStyle Width="200px" Wrap="False" />
                                                <ItemStyle Width="200px" Wrap="False" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Setor" 
                                                FilterControlAltText="Filter Setor column" HeaderText="Setor" 
                                                SortExpression="Setor" UniqueName="Setor">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings AllowDragToGroup="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                                </telerik:RadGrid>
                                <asp:ObjectDataSource ID="ObjectDataSourceGrupos" runat="server" 
                                    SelectMethod="RetornaGrupos" TypeName="CSFCenterReports.Controls.Grupo">
                                    <SelectParameters>
                                        <asp:Parameter Name="codigo" Type="String" />
                                        <asp:Parameter Name="uf" Type="String" />
                                        <asp:Parameter Name="cidade" Type="String" />
                                        <asp:Parameter Name="unidade" Type="String" />
                                        <asp:Parameter Name="setor" Type="String" />
                                        <asp:Parameter Name="ativo" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <telerik:RadWindow Modal="true" AutoSize="true" ID="rWindow" runat="server" 
                    VisibleOnPageLoad="false" Animation="None" 
                    Behaviors="Minimize, Close, Maximize, Reload" VisibleStatusbar="False" 
                    VisibleTitlebar="False" Localization-Cancel="Cancelar" 
                    Localization-Close="Fechar" Localization-Maximize="Maximizar" 
                    Localization-Minimize="Minimizar" Localization-No="Não" 
                    Localization-Reload="Recarregar" Localization-Restore="Restaurar" 
                    Localization-Yes="Sim" Visible="true">
                    <ContentTemplate>
                        <asp:Panel ID="pnlUsuarios" runat="server" Visible="True">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="rgrdUsuarios" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                            CellSpacing="0" Culture="Portuguese (Brazil)" 
                                            DataSourceID="ObjectDataSourceUsuarios" GridLines="Horizontal" 
                                            ShowGroupPanel="True" Width="100%" 
                                            onitemcommand="rgrdUsuarios_ItemCommand"
                                            >
                                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                                            <MasterTableView DataSourceID="ObjectDataSourceUsuarios" ShowFooter="True" 
                                                ShowGroupFooter="True">
                                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridButtonColumn DataTextField="Codigo" HeaderText="Código"
                                                    FilterControlAltText="Filter Codigo column"
                                                    SortExpression="Codigo" UniqueName="Codigo" CommandName="Fechar" /> 
                                                    <telerik:GridBoundColumn DataField="Nome" 
                                                        FilterControlAltText="Filter Nome column" HeaderText="Nome" 
                                                        SortExpression="Nome" UniqueName="Nome">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DtCadastro" DataType="System.DateTime" 
                                                        FilterControlAltText="Filter DtCadastro column" HeaderText="Cadastrado em" 
                                                        SortExpression="DtCadastro" UniqueName="DtCadastro">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DtValidade" DataType="System.DateTime" 
                                                        FilterControlAltText="Filter DtValidade column" HeaderText="Válido até" 
                                                        SortExpression="DtValidade" UniqueName="DtValidade">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DtAlteracao" DataType="System.DateTime" 
                                                        FilterControlAltText="Filter DtAlteracao column" HeaderText="Alterado em" 
                                                        SortExpression="DtAlteracao" UniqueName="DtAlteracao">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridCheckBoxColumn DataField="Ativo" DataType="System.Boolean" 
                                                        FilterControlAltText="Filter Ativo column" HeaderText="Ativo?" 
                                                        SortExpression="Ativo" UniqueName="Ativo">
                                                    </telerik:GridCheckBoxColumn>
                                                    <telerik:GridBoundColumn DataField="CdGrupo" 
                                                        FilterControlAltText="Filter CdGrupo column" HeaderText="Grupo" 
                                                        SortExpression="CdGrupo" UniqueName="CdGrupo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridCheckBoxColumn DataField="UtilizaAD" DataType="System.Boolean" 
                                                        FilterControlAltText="Filter UtilizaAD column" HeaderText="Utiliza AD?" 
                                                        SortExpression="UtilizaAD" UniqueName="UtilizaAD">
                                                    </telerik:GridCheckBoxColumn>
                                                </Columns>                                        
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings AllowDragToGroup="True">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                                        </telerik:RadGrid>
                                        <asp:ObjectDataSource ID="ObjectDataSourceUsuarios" runat="server" 
                                            SelectMethod="RetornaUsuarios" TypeName="CSFCenterReports.Controls.Usuario">
                                            <SelectParameters>
                                                <asp:Parameter Name="grupo" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Localization Cancel="Cancelar" Close="Fechar" Maximize="Maximizar" 
                        Minimize="Minimizar" No="Não" Reload="Recarregar" Restore="Restaurar" 
                        Yes="Sim" />
                </telerik:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>