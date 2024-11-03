<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="ConsultaParametro.aspx.cs" Inherits="CSFCenterReports.ConsultaParametro" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlConsulta" runat="server" Visible="True">
                    <table width="100%">                        
                        <tr>
                            <td style="width: 150px;">
                                <asp:Label ID="lbConsultaNome" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td style="width: 450px;">
                                <telerik:RadTextBox ID="rtxtNome" runat="server" MaxLength="250" Width="400px">
                                </telerik:RadTextBox>
                            </td>
                            <td align="right">
                                <telerik:RadButton ID="rbtnConsultar" runat="server" Text="Consultar" Width="100px" onclick="rbtnConsultar_Click">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadGrid ID="rgrdParametros" runat="server" AutoGenerateColumns="False" 
                                    CellSpacing="0" Culture="Portuguese (Brazil)" 
                                    DataSourceID="ObjectDataSourceParametros" GridLines="Horizontal" Width="100%" 
                                    onitemcommand="rgrdParametros_ItemCommand" GroupingEnabled="False">
                                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                                    <MasterTableView DataSourceID="ObjectDataSourceParametros" ShowFooter="True" 
                                        ShowGroupFooter="True" NoDetailRecordsText="Não há subitens a exibir." 
                                        NoMasterRecordsText="Não há itens a exibir.">
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn DataTextField="Nome" HeaderText="Nome"
                                            FilterControlAltText="Filter Nome column"
                                            SortExpression="Nome" UniqueName="Nome" CommandName="Alterar">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="Valor" 
                                                FilterControlAltText="Filter Valor column" HeaderText="Valor" 
                                                SortExpression="Valor" UniqueName="Valor">
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
                                <asp:ObjectDataSource ID="ObjectDataSourceParametros" runat="server" 
                                    SelectMethod="RetornaParametros" TypeName="CSFCenterReports.Controls.Parametro">
                                    <SelectParameters>
                                        <asp:Parameter Name="nome" Type="String" />
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
                        <asp:Panel ID="pnlCampos" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbNome" runat="server" Text="Nome"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="rtbNome" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbValor" runat="server" Text="Valor"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="rtbValor" runat="server" MaxLength="250" Width="400px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <telerik:RadButton ID="rbtnFechar" runat="server" Text="Fechar" onclick="rbtnFechar_Click">
                                        </telerik:RadButton>
                                    </td>
                                    <td align="right">
                                        <telerik:RadButton ID="rbtnConfirmar" runat="server" Text="Confirmar" onclick="rbtnConfirmar_Click">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lbMensagem" runat="server" Text="" ForeColor="Red"></asp:Label>
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