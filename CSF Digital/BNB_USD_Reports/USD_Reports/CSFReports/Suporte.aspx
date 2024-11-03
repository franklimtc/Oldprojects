<%@ Page Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeFile="Suporte.aspx.cs" Inherits="Suporte" Title="CSF Digital" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>    
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Cabecalho" ContentPlaceHolderID="ContentPlaceHolderCabecalho" runat="server">
    <div class="menuHome">  
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
    <div class="menuSuporteAtivo">  
        <a href="Suporte.aspx" class="link">suporte</a>      	
    </div>
</asp:Content>
<asp:Content ID="Corpo" ContentPlaceHolderID="ContentPlaceHolderCorpo" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div id="conteudoCentro" runat="server" style="margin: -7px 135px;" >        
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanelCampos" runat="server">
                        <ContentTemplate>
                            <div>
                                <table style="padding-top:10px;">
                                    <tr>
                                        <td class= "content">
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text="Data Inicial"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="rdpDtInicial" runat="server"></telerik:RadDatePicker>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text="Data Final"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="rdpDtFinal" runat="server"></telerik:RadDatePicker>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text="Condição"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RadioButtonListTipo" runat="server" CellPadding="3" AutoPostBack="True" onselectedindexchanged="RadioButtonListTipo_SelectedIndexChanged" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="bottom">
                                                        <asp:Button ID="btnGerar" runat="server" Text="Gerar" onclick="btnGerar_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table class="content" width="100%">
            <tr>
                <td align="center">
                    <div><asp:Label ID="Label1" runat="server" Text="Chamados por SLA"></asp:Label></div>
                     <asp:Chart ID="Chart1" runat="server" Width="400px">
                        <Series>
                            <asp:Series Name="SLA" XValueMember="Sim" YValueMembers="Qtd." IsVisibleInLegend="true" Palette="Excel" IsValueShownAsLabel="True" XValueType="String">
                            </asp:Series> 
                        </Series>
                         <ChartAreas>
                             <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false"> 
                                   <AxisX LineColor="DarkGray" IntervalAutoMode="VariableCount">  
                                       <MajorGrid LineColor="LightGray" /> 
                                   </AxisX> 
                                   <AxisY LineColor="DarkGray"> 
                                       <MajorGrid LineColor="LightGray" /> 
                                   </AxisY> 
                             </asp:ChartArea> 
                         </ChartAreas>
                     </asp:Chart>
                </td>
                <td align="center">
                    <div><asp:Label ID="Label3" runat="server" Text="Chamados por Categoria"></asp:Label></div>
                     <asp:Chart ID="Chart2" runat="server" Width="400px">
                        <Series>
                            <asp:Series Name="SLA" XValueMember="Categoria" YValueMembers="Qtd." IsVisibleInLegend="true" Palette="Excel" IsValueShownAsLabel="True" XValueType="String" YValueType="Int32" YAxisType="Primary">
                            </asp:Series> 
                        </Series>
                         <ChartAreas>
                             <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false"> 
                                   <AxisX LineColor="DarkGray" IntervalAutoMode="VariableCount"> 
                                       <MajorGrid LineColor="LightGray" /> 
                                   </AxisX> 
                                   <AxisY LineColor="DarkGray"> 
                                       <MajorGrid LineColor="LightGray" /> 
                                   </AxisY> 
                             </asp:ChartArea> 
                         </ChartAreas>
                     </asp:Chart>
                </td>
            </tr>
        </table>
        <table class="content" width="100%">
            <tr>
                <td align="center">
                    <div><asp:Label ID="Label2" runat="server" Text="Chamados por Estado"></asp:Label></div>
                     <asp:Chart ID="Chart3" runat="server" Width="950">
                         <Legends>
                             <asp:Legend Name="Legend1" Docking="Bottom" Alignment="Center" IsDockedInsideChartArea="False" LegendStyle="Row">
                             </asp:Legend>
                         </Legends>
                        <Series>
                            <asp:Series Name="SLA" XValueMember="UF" YValueMembers="Qtd." 
                                IsVisibleInLegend="False" IsXValueIndexed="True" ChartType="Column" 
                                Palette="Excel" XValueType="String" IsValueShownAsLabel="True" 
                                YAxisType="Primary" XAxisType="Primary" YValueType="Int32" Legend="Legend1">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false" BackImageAlignment="Center"> 
                                <AxisX LineColor="DarkGray" IntervalAutoMode="VariableCount"> 
                                    <MajorGrid LineColor="LightGray" />
                                </AxisX> 
                                <AxisY LineColor="DarkGray"> 
                                    <MajorGrid LineColor="LightGray" /> 
                                </AxisY>
                            </asp:ChartArea> 
                        </ChartAreas>
                     </asp:Chart>
                </td>
            </tr>
        </table>
        <table class="content" width="100%">
            <tr>
                <td align="center">
                    <div><asp:Label ID="Label4" runat="server" Text="Chamados por Atendente"></asp:Label></div>
                    <asp:Chart ID="Chart4" runat="server" Width="950">                        
                        <Series>
                            <asp:Series Name="SLA" XValueMember="Técnico" YValueMembers="Qtd." IsVisibleInLegend="true" Palette="Excel" IsValueShownAsLabel="True" XValueType="String">
                            </asp:Series> 
                        </Series>
                         <ChartAreas>
                             <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false"> 
                                   <AxisX LineColor="DarkGray" IntervalAutoMode="FixedCount"> 
                                       <MajorGrid LineColor="LightGray" />
                                   </AxisX> 
                                   <AxisY LineColor="DarkGray"> 
                                       <MajorGrid LineColor="LightGray" /> 
                                   </AxisY> 
                             </asp:ChartArea> 
                         </ChartAreas>
                     </asp:Chart>
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