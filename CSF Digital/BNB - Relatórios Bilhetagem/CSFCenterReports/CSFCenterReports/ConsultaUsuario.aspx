<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="ConsultaUsuario.aspx.cs" Inherits="CSFCenterReports.ConsultaUsuario" %>
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
                                <asp:Label ID="lbConsultaLogin" runat="server" Text="Login"></asp:Label>
                            </td>
                            <td style="width: 450px;">
                                <telerik:RadTextBox ID="rtxtLogin" runat="server" Width="100px" MaxLength="10">
                                </telerik:RadTextBox>
                            </td>
                            <td align="right">
                                <telerik:RadButton ID="rbtnCadastrar" runat="server" 
                                    Text="Cadastrar" Width="100px" onclick="rbtnCadastrar_Click">
                                </telerik:RadButton>
                            </td>
                        </tr>
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
                                <telerik:RadGrid ID="rgrdUsuarios" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                                    CellSpacing="0" Culture="Portuguese (Brazil)" 
                                    DataSourceID="ObjectDataSourceUsuarios" GridLines="Horizontal" 
                                    ShowGroupPanel="True" Width="100%" 
                                    onitemcommand="rgrdUsuarios_ItemCommand">
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
                                            SortExpression="Codigo" UniqueName="Codigo" CommandName="Alterar">
                                            </telerik:GridButtonColumn>
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
                                        <asp:Parameter Name="login" Type="String" />
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
                                        <asp:Label ID="lbLogin" runat="server" Text="Login" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="rtbLogin" runat="server" Width="100px" MaxLength="10">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbNome" runat="server" Text="Nome"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="rtbNome" runat="server" MaxLength="250" Width="400px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbCadastro" runat="server" Text="Cadastrado em"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="rdpDtCadastro" runat="server">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbDtValidade" runat="server" Text="Válido até"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="rdpDtValidade" runat="server">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbAlteracao" runat="server" Text="Alterado em"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="rdpDtAlteracao" runat="server">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbGrupo" runat="server" Text="Grupo"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rcbGrupo" runat="server" Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbStatus" runat="server" Text="Ativo ?"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rcbStatus" runat="server" Width="70px">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Selected="True" Text="Sim" Value="0" />
                                                <telerik:RadComboBoxItem runat="server" Text="Não" Value="1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbUtilizaAD" runat="server" Text="Utiliza AD ?"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rcbUtilizaAD" runat="server" Width="70px">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Selected="True" Text="Sim" Value="0" />
                                                <telerik:RadComboBoxItem runat="server" Text="Não" Value="1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                       <telerik:RadButton ID="rbtnResetarSenha" runat="server" Text="Resetar Senha" onclick="rbtnResetarSenha_Click">
                                       </telerik:RadButton> 
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