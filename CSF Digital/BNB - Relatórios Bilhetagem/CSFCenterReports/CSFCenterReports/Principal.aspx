<%@ Page Title="" Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="CSFCenterReports.Principal" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <div class="ColCenter">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: left;">
                    <table>
                        <tr>
                            <td colspan="3">                            
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rbRelatorios" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="rbRelatorios_SelectedIndexChanged" />
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
                                            <asp:ObjectDataSource ID="ObjectDataSourceCelula" runat="server" 
                                                SelectMethod="RetornaListaCelulas" TypeName="CSFCenterReports.Controls.Grupo">
                                                <SelectParameters>
                                                    <asp:Parameter Name="uf" Type="String" />
                                                    <asp:Parameter Name="cidade" Type="String" />
                                                    <asp:Parameter Name="unidade" Type="String" />
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
                                            <asp:ObjectDataSource ID="ObjectDataSourceImpressora" runat="server"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:Label ID="lbDescricaoRelatorio1" runat="server" Text="Descrição:"></asp:Label>
                                            <br />
                                            <asp:Label ID="lbDescricaoRelatorio" runat="server" Text="" Font-Italic="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>                            
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbUf" runat="server" Text="UF"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rcbUf" runat="server" 
                                                DataSourceID="ObjectDataSourceUF" Sort="Ascending" 
                                                onselectedindexchanged="rcbUf_SelectedIndexChanged" AutoPostBack="True" 
                                                Width="80px" Visible="False"></telerik:RadComboBox>
                                            <asp:Label ID="lbUfValor" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbCidade" runat="server" Text="Cidade"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rcbCidade" runat="server" 
                                                DataSourceID="ObjectDataSourceCidade" Sort="Ascending" 
                                                onselectedindexchanged="rcbCidade_SelectedIndexChanged" 
                                                AutoPostBack="True" Width="350px" Visible="False"></telerik:RadComboBox>
                                            <asp:Label ID="lbCidadeValor" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbUnidade" runat="server" Text="Unidade"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rcbUnidade" runat="server" 
                                                DataSourceID="ObjectDataSourceUnidade" Sort="Ascending" 
                                                AutoPostBack="True" onselectedindexchanged="rcbUnidade_SelectedIndexChanged" 
                                                Width="350px" Visible="False"></telerik:RadComboBox>
                                            <asp:Label ID="lbUnidadeValor" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbCelula" runat="server" Text="Centro de Custo"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rcbCelula" runat="server" 
                                                DataSourceID="ObjectDataSourceCelula" Sort="Ascending" 
                                                onselectedindexchanged="rcbCelula_SelectedIndexChanged" Width="350px" 
                                                AutoPostBack="True" Visible="False"></telerik:RadComboBox>
                                            <asp:Label ID="lbCelulaValor" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbAmbiente" runat="server" Text="Ambiente"></asp:Label>
                                        </td>
                                        <td>
                                        <%--onselectedindexchanged="rcbAmbiente_SelectedIndexChanged"--%>
                                            <telerik:RadComboBox ID="rcbAmbiente" runat="server" 
                                                DataSourceID="ObjectDataSourceSetor" Sort="Ascending" 
                                                
                                                Width="350px" 
                                                AutoPostBack="True" Visible="False"></telerik:RadComboBox>
                                            <asp:Label ID="lbAmbienteValor" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbUsuarios" runat="server" Text="Usuários"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="rtbUsuario" Runat="server" MaxLength="30" Wrap="False"  style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbCor" runat="server" Text="Colorido?"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rcbCor" runat="server">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Selected="True" Text="Indiferente" Value="" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Não" Value="N" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Sim" Value="S" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbFormatoFolha" runat="server" Text="Formato Folha"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rcbFormatoFolha" runat="server">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Selected="True" Text="Indiferente" Value="" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="A3" Value="A3" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="A4" Value="A4" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="A5" Value="A5" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="A6" Value="A6" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="B5" Value="B5" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Carta" Value="Carta" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Envelope" Value="Envelope" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Fólio" Value="Fólio" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Ofício" Value="Ofício" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Papel Personalizado" Value="Papel Personalizado" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="False" Text="Tablóide" Value="Tablóide" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbDtInicial" runat="server" Text="Data Inicial"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbDtFinal" runat="server" Text="Data Final"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="rdpInicial" runat="server"></telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="rdpFinal" runat="server"></telerik:RadDatePicker>
                                        </td>
                                    </tr>                        
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbImpressoras" runat="server" Text="Impressoras"></asp:Label>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <telerik:RadListBox ID="rlbImpressoras" runat="server" SelectionMode="Multiple" 
                                                CheckBoxes="True" Culture="Portuguese (Brazil)" AutoPostBack="True" 
                                                onitemcheck="rlbImpressoras_ItemCheck" Width="200px">
                                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="2">
                    <telerik:RadButton ID="rbEmitir" runat="server" Text="Emitir" onclick="rbEmitir_Click"></telerik:RadButton>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="lbObservacoes" runat="server" Text="Observações:"></asp:Label>
                    <br />
                    <asp:Label ID="lbObservacao1" runat="server" Text="-> Os relatórios listados acima possuem limitação de 91 dias quanto ao período informado."></asp:Label>
                    <br />
                    <asp:Label ID="lbObservacao2" runat="server" Text="-> Solicitações de acesso ou dúvidas podem ser registradas pelo VoIP *5516 ou através do site http://central3121 utilizando a categoria “EQUIPAMENTOS DE TI\IMPRESSORAS\XEROX\INFORMAÇÕES”."></asp:Label>
                    <br />
                    <asp:Label ID="lbObservacao3" runat="server" Text="-> Caso seu navegador seja o Internet Explorer 8 clique no link a seguir."></asp:Label><asp:HyperLink ID="PageInternetExplorer8" runat="server" NavigateUrl="~/AjudaIE8.aspx">Clique aqui.</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>