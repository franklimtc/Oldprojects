<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EqptParados.aspx.cs" Inherits="Relatórios_EqptParados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div style="text-align:center">
<table>
        <tr align="center" width="100%">
        <td>
             <asp:Chart ID="Chart1" runat="server" DataSourceID="dtEquipamentosParados"
                Palette="Excel">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie" XValueMember="modelo" 
                        YValueMembers="qtd" IsValueShownAsLabel="True" Legend="Legend1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Legend1" Alignment="Center" Docking="Bottom">
                    </asp:Legend>
                </Legends>
                <Titles>
                    <asp:Title Name="Title1" Text="Equipamentos Parados" 
                        Font="Microsoft Sans Serif, 14pt, style=Bold">
                    </asp:Title>
                </Titles>
            </asp:Chart>
        </td>
        <td align="center" width="100%">
          <asp:Chart ID="Chart2" runat="server" DataSourceID="dtEquipamentosPrecarios" 
                Palette="Excel">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie" XValueMember="modelo" 
                        YValueMembers="qtd" IsValueShownAsLabel="True" Legend="Legend1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Legend1" Alignment="Center" Docking="Bottom">
                    </asp:Legend>
                </Legends>
                <Titles>
                    <asp:Title Name="Title1" Text="Equipamentos Precários" 
                        Font="Microsoft Sans Serif, 14pt, style=Bold">
                    </asp:Title>
                </Titles>
            </asp:Chart>
        </td>
    </tr>
</table>
   
</div>
<div>
        <asp:Menu ID="menuViews" runat="server" Orientation="Horizontal" 
            CssClass="menu" onmenuitemclick="menuViews_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Paradas" Value="0" ></asp:MenuItem>
                
                <asp:MenuItem Text="Precárias" Value="1"></asp:MenuItem>
            </Items>
        </asp:Menu>

    <asp:MultiView ID="mv1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewParadas" runat="server">
            <asp:GridView ID="GridView1" runat="server" DataSourceID="dtListaParadas" Width="100%"
                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idreqPeca" 
                ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="idreqPeca" HeaderText="idreqPeca" ReadOnly="True" 
                        SortExpression="idreqPeca" />
                    <asp:BoundField DataField="reqUSD" HeaderText="reqUSD" 
                        SortExpression="reqUSD" />
                    <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                    <asp:BoundField DataField="cidade" HeaderText="cidade" ReadOnly="True" 
                        SortExpression="cidade" />
                    <asp:BoundField DataField="unidade" HeaderText="unidade" 
                        SortExpression="unidade" />
                    <asp:BoundField DataField="serieEqpto" HeaderText="serieEqpto" 
                        SortExpression="serieEqpto" />
                    <asp:BoundField DataField="partNumber" HeaderText="partNumber" 
                        SortExpression="partNumber" />
                    <asp:BoundField DataField="descricao" HeaderText="descricao" 
                        SortExpression="descricao" />
                    <asp:BoundField DataField="solicitante" HeaderText="solicitante" 
                        SortExpression="solicitante" />
                    <asp:BoundField DataField="tecnico" HeaderText="tecnico" 
                        SortExpression="tecnico" />
                    <asp:BoundField DataField="dtSolicitacao" HeaderText="dtSolicitacao" 
                        SortExpression="dtSolicitacao" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" ReadOnly="True" 
                        SortExpression="qtdDias" />
                    <asp:BoundField DataField="Status" HeaderText="Status" 
                        SortExpression="Status" />
                    <asp:BoundField DataField="eqpParado" HeaderText="eqpParado" 
                        SortExpression="eqpParado" />
                    <asp:BoundField DataField="dtCriacao" HeaderText="dtCriacao" 
                        SortExpression="dtCriacao" DataFormatString="{0:d}" />
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
        <asp:View ID="view1" runat="server">
            <asp:GridView ID="GridView2" runat="server" DataSourceID="dtListaPrecarias" Width="100%"
                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idreqPeca" 
                ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="idreqPeca" HeaderText="idreqPeca" ReadOnly="True" 
                        SortExpression="idreqPeca" />
                    <asp:BoundField DataField="reqUSD" HeaderText="reqUSD" 
                        SortExpression="reqUSD" />
                    <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
                    <asp:BoundField DataField="cidade" HeaderText="cidade" ReadOnly="True" 
                        SortExpression="cidade" />
                    <asp:BoundField DataField="unidade" HeaderText="unidade" 
                        SortExpression="unidade" />
                    <asp:BoundField DataField="serieEqpto" HeaderText="serieEqpto" 
                        SortExpression="serieEqpto" />
                    <asp:BoundField DataField="partNumber" HeaderText="partNumber" 
                        SortExpression="partNumber" />
                    <asp:BoundField DataField="descricao" HeaderText="descricao" 
                        SortExpression="descricao" />
                    <asp:BoundField DataField="solicitante" HeaderText="solicitante" 
                        SortExpression="solicitante" />
                    <asp:BoundField DataField="tecnico" HeaderText="tecnico" 
                        SortExpression="tecnico" />
                    <asp:BoundField DataField="dtSolicitacao" HeaderText="dtSolicitacao" 
                        SortExpression="dtSolicitacao" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" ReadOnly="True" 
                        SortExpression="qtdDias" />
                    <asp:BoundField DataField="Status" HeaderText="Status" 
                        SortExpression="Status" />
                    <asp:BoundField DataField="eqpParado" HeaderText="eqpParado" 
                        SortExpression="eqpParado" />
                    <asp:BoundField DataField="dtCriacao" HeaderText="dtCriacao" 
                        SortExpression="dtCriacao" DataFormatString="{0:d}" />
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
   </asp:MultiView>
  <%--  <asp:GridView ID="GridView1" runat="server" DataSourceID="EquipamentosParados" 
        AutoGenerateColumns="False" DataKeyNames="idreqPeca" Width="100%" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="idreqPeca" HeaderText="idreqPeca" ReadOnly="True" 
                SortExpression="idreqPeca" />
            <asp:BoundField DataField="reqUSD" HeaderText="reqUSD" 
                SortExpression="reqUSD" />
            <asp:BoundField DataField="uf" HeaderText="uf" SortExpression="uf" />
            <asp:BoundField DataField="cidade" HeaderText="cidade" ReadOnly="True" 
                SortExpression="cidade" />
            <asp:BoundField DataField="unidade" HeaderText="unidade" 
                SortExpression="unidade" />
            <asp:BoundField DataField="serieEqpto" HeaderText="serieEqpto" 
                SortExpression="serieEqpto" />
            <asp:BoundField DataField="partNumber" HeaderText="partNumber" 
                SortExpression="partNumber" />
            <asp:BoundField DataField="descricao" HeaderText="descricao" 
                SortExpression="descricao" />
            <asp:BoundField DataField="solicitante" HeaderText="solicitante" 
                SortExpression="solicitante" />
            <asp:BoundField DataField="tecnico" HeaderText="tecnico" 
                SortExpression="tecnico" />
            <asp:BoundField DataField="dtSolicitacao" HeaderText="dtSolicitacao" 
                SortExpression="dtSolicitacao" DataFormatString="{0:d}" />
            <asp:BoundField DataField="qtdDias" HeaderText="qtdDias" ReadOnly="True" 
                SortExpression="qtdDias" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
            <asp:BoundField DataField="eqpParado" HeaderText="eqpParado" 
                SortExpression="eqpParado" />
            <asp:BoundField DataField="dtCriacao" HeaderText="dtCriacao" 
                SortExpression="dtCriacao" DataFormatString="{0:d}" />
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
    </asp:GridView>--%>

</div>

        <asp:SqlDataSource ID="dtEquipamentosParados" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            
        SelectCommand="select modelo, count(*) 'qtd' from vwEquipamentosParados group by modelo" >
        </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="EquipamentosParados" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
            SelectCommand="select * from pecasPendentes3 where eqpParado = 'Sim'" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dtEquipamentosPrecarios" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
             SelectCommand="select modelo, count(*) 'qtd' from vwEquipamentosPrecarios group by modelo" >
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dtListaParadas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
             SelectCommand="select * from pecasPendentes3 where serieEqpto in (select serieEqpto from vwEquipamentosParados) and eqpParado = 'Sim' order by qtdDias DESC" >
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dtListaPrecarias" runat="server" 
            ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" 
             SelectCommand="select * from pecasPendentes3 where serieEqpto in (select serieEqpto from vwEquipamentosPrecarios) and eqpParado = 'Não'  order by qtdDias DESC" >
        </asp:SqlDataSource>

</asp:Content>

