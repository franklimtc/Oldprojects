<%@ Page Title="Relatórios" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Relatorios.aspx.cs" Inherits="Relatorios" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
        <asp:Menu ID="menuViews" runat="server" Orientation="Horizontal" 
            onmenuitemclick="menuViews_MenuItemClick" CssClass="menu">
            <Items>
                <asp:MenuItem Text="Peças" Value="0" ></asp:MenuItem>
                
                <asp:MenuItem Text="Suprimentos" Value="1"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:MultiView ID="mv1" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewPecas" runat="server" ViewStateMode="Enabled">
                <h2 style="text-align:center">Gráficos de Peças</h2>
                <table border=1 width=100%>
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" DataSourceID="PecasPendentes" 
                        Palette="SeaGreen" Width="400px">
                        <series>
                            <asp:Series Name="Series1" XValueMember="uf" 
                                YValueMembers="qtd" IsVisibleInLegend="True"
                                IsXValueIndexed="False" IsValueShownAsLabel="True" Legend="Legend1">
                            </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </chartareas>
                        
                        <Titles>
                            <asp:Title Name="Title1" Text="Peças Pendentes - Estados" 
                                Font="Microsoft Sans Serif, 8pt, style=Bold">
                            </asp:Title>
                        </Titles>
                        
                    </asp:Chart>
                </td>
                <td align="center">
                    <asp:Chart ID="Chart3" runat="server" DataSourceID="PecasPendentes3"
                        Palette="Pastel" Width="400px">
                        <series>
                            <asp:Series Name="Series3" XValueMember="qtdDias" 
                                YValueMembers="qtd" IsVisibleInLegend="True"
                                IsXValueIndexed="False" IsValueShownAsLabel="True" ChartType="Pie">
                            </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="ChartArea3">
                            </asp:ChartArea>
                        </chartareas>
                        <Legends>
                            <asp:Legend Name="Legend3" Title="Qtd Dias" Alignment="Center" Docking="Bottom">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Title3" Text="Peças Pendentes - 0 a 5 Dias" 
                                Font="Microsoft Sans Serif, 8pt, style=Bold">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>   
                </td>
            </tr>
            <tr>
                <td>
                  <asp:Chart ID="Chart4" runat="server" DataSourceID="PecasPendentes4"
                        Palette="Pastel" Width="400px">
                        <series>
                            <asp:Series Name="Series4" XValueMember="qtdDias" 
                                YValueMembers="qtd" IsVisibleInLegend="True"
                                IsXValueIndexed="False" IsValueShownAsLabel="True" ChartType="Pie">
                            </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="ChartArea4">
                            </asp:ChartArea>
                        </chartareas>
                        <Legends>
                            <asp:Legend Name="Legend4" Title="Qtd Dias" Alignment="Center" Docking="Bottom">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Title4" Text="Peças Pendentes - 6 a 15 Dias" 
                                Font="Microsoft Sans Serif, 8pt, style=Bold">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                </td>
                <td align="center">
                       <asp:Chart ID="Chart5" runat="server" DataSourceID="PecasPendentes5"
                        Palette="Pastel" Width="400px">
                        <series>
                            <asp:Series Name="Series5" XValueMember="qtdDias" 
                                YValueMembers="qtd" IsVisibleInLegend="True"
                                IsXValueIndexed="False" IsValueShownAsLabel="True" ChartType="Pie">
                            </asp:Series>
                        </series>
                        <chartareas>
                            <asp:ChartArea Name="ChartArea5">
                            </asp:ChartArea>
                        </chartareas>
                        <Legends>
                            <asp:Legend Name="Legend5" Title="Qtd Dias" Alignment="Center" Docking="Bottom">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Title5" Text="Peças Pendentes - > 15 Dias" 
                                Font="Microsoft Sans Serif, 8pt, style=Bold">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                </td>
            </tr>
            <tr>
                <td  colspan=2>
                <h2 align="center">Solicitações abertas a mais de 15 dias</h2>
                     <%-- <asp:DataGrid ID="dtPecas" runat="server" DataSourceID="PecasPendentes6" 
                          CellPadding="4" ForeColor="#333333" GridLines="None">
                          <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                          <EditItemStyle BackColor="#999999" />
                          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                          <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                      </asp:DataGrid>--%>
                      <asp:Panel ID="panel1" runat="server">
                      <asp:GridView ID="gvPecas" runat="server" DataSourceID="PecasPendentes6" 
                         AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                         GridLines="None" Height="280px" Width="100%">
                          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                          <Columns>
                              <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf" />
                              <asp:BoundField DataField="cidade" HeaderText="Cidade" ReadOnly="True" 
                                  SortExpression="cidade" />
                              <asp:BoundField DataField="serieEqpto" HeaderText="Série" 
                                  SortExpression="serieEqpto" />
                              <asp:BoundField DataField="descricao" HeaderText="Peça" 
                                  SortExpression="descricao" />
                              <asp:BoundField DataField="solicitante" HeaderText="Solicitante" 
                                  ReadOnly="True" SortExpression="solicitante" />
                              <asp:BoundField DataField="dtSolicitacao" DataFormatString="{0:d}" 
                                  HeaderText="Data" SortExpression="dtSolicitacao" />
                          </Columns>
                          <EditRowStyle BackColor="#999999" />
                          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                          <SortedAscendingCellStyle BackColor="#E9E7E2" />
                          <SortedAscendingHeaderStyle BackColor="#506C8C" />
                          <SortedDescendingCellStyle BackColor="#FFFDF8" />
                          <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                     </asp:GridView>
                     </asp:Panel>
                </td>
            </tr>
        </table>
            </asp:View>
            <asp:View ID="viewSuprimentos" runat="server">
                <h2 style="text-align:center">Gráficos de Suprimentos</h2>
                <table width="100%">
            <tr>
                <td align="center">
                    <asp:Chart ID="chart2" runat="server" DataSourceID="ocomon1" Palette="SeaGreen" Width="400px">
                        <Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Pie" Name="Series1" 
                                XValueMember="qtdDias" YValueMembers="qtd" Legend="Legend1" IsValueShownAsLabel="True">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Legend1"  Title="Qtd Dias" Alignment="Center" Docking="Bottom">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Title1" Text="Solicitações Pendentes no Ocomon - < 5 dias" Font="Microsoft Sans Serif, 8pt, style=Bold">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                </td>
                <td align="center">
                    <asp:Chart ID="chart6" runat="server" DataSourceID="ocomon2" Palette="SeaGreen" Width="400px">
                        <Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Pie" Legend="Legend1" 
                                Name="Series1" XValueMember="qtdDias" YValueMembers="qtd" IsValueShownAsLabel="True">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Legend1" Title="Qtd Dias" Alignment="Center" Docking="Bottom">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Title1" Text="Solicitações Pendentes no Ocomon - > 5 dias" Font="Microsoft Sans Serif, 8pt, style=Bold">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                </td>
            </tr>
             <tr>
                <td colspan=2 align="center">
                <h2 align="center">Solicitações abertas a mais de 5 dias</h2>
                    <asp:GridView ID="gvOcomon" runat="server" DataSourceID="ocomon3" Width="100%" 
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                        GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="numero" HeaderText="Chamado" InsertVisible="False" 
                                SortExpression="numero" />
                            <asp:BoundField DataField="local" HeaderText="Local" SortExpression="local" />
                            <asp:BoundField DataField="problema" HeaderText="Solicitação" 
                                SortExpression="problema" />
                            <asp:BoundField DataField="status" HeaderText="Status" 
                                SortExpression="status" />
                            <asp:BoundField DataField="descricao" HeaderText="Descrição" 
                                SortExpression="descricao" />
                            <asp:BoundField DataField="data_abertura" DataFormatString="{0:d}" 
                                HeaderText="Data" SortExpression="data_abertura" />
                            <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" 
                                SortExpression="qtdDias" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
            </asp:View>
        </asp:MultiView>
        
    </div>
    
        <asp:SqlDataSource ID="PecasPendentes" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select uf, count(*)'qtd' from pecasPendentes group by uf" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="PecasPendentes2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select top 5 qtdDias, COUNT(*)'qtd' from pecasPendentes group by qtdDias order by qtdDias desc" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="PecasPendentes3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select qtdDias, COUNT(*)'qtd' from pecasPendentes where qtdDias between 0 and 5 group by qtdDias" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="PecasPendentes4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select qtdDias, COUNT(*)'qtd' from pecasPendentes where qtdDias between 6 and 15 group by qtdDias" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="PecasPendentes5" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select qtdDias, COUNT(*)'qtd' from pecasPendentes where qtdDias > 15 group by qtdDias" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="PecasPendentes6" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select uf, cidade, serieEqpto, partNumber,descricao,UPPER(solicitante)'solicitante', dtSolicitacao  from pecasPendentes where qtdDias > 15" >
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="ocomon1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                SelectCommand="select qtdDias, count(*) 'qtd' from resumoOcomon where qtdDias < 5 group by qtdDias;">
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="ocomon2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                SelectCommand="select qtdDias, count(*) 'qtd' from resumoOcomon where qtdDias > 5 group by qtdDias;">
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="ocomon3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
                SelectCommand="select * from resumoOcomon where qtdDias > 5;">
            </asp:SqlDataSource>
</asp:Content>

