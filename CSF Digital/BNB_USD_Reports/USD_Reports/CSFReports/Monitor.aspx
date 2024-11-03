<%@ Page Language="C#" MasterPageFile="~/AppSiteMaster.Master" AutoEventWireup="true" CodeFile="Monitor.aspx.cs" Inherits="Monitor" Title="CSF Digital" %>
<asp:Content ID="Cabecalho" ContentPlaceHolderID="ContentPlaceHolderCabecalho" runat="server">
    <div class="menuHome">  
        <a href="Home.aspx" class="link">home</a>      	
    </div>
    <div class="menuRelatorio">  
        <a href="Relatorio.aspx" class="link">relatorio</a>      	
    </div>
    <div class="menuMonitorAtivo">  
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
        <%--<table class= "content" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Ordenação crescente"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="CheckBoxOrdenaCrescente" runat="server" RepeatDirection="Horizontal" Width="100%" AutoPostBack="True" onselectedindexchanged="CheckBoxOrdenaCrescente_SelectedIndexChanged" ></asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;"></td>
            </tr>
        </table>
        <table class= "content" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Ordenação decrescente"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="CheckBoxOrdenaDecrescente" runat="server" RepeatDirection="Horizontal" Width="100%" onselectedindexchanged="CheckBoxOrdenaDecrescente_SelectedIndexChanged" AutoPostBack="True"></asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;"></td>
            </tr>
        </table>--%>
        <%--<table style="padding-top:10px" width="100%">
        </table>--%>
        <table style="padding-top:10px" width="100%">
            <tr>
                <td>
                    <asp:Table ID="tblMonitor" runat="server" HorizontalAlign="Justify" Width="100%" CellPadding="0" CellSpacing="0" GridLines="Both">
                        <asp:TableHeaderRow HorizontalAlign="Justify" VerticalAlign="Top">
                            <asp:TableHeaderCell ID="NN" Text="#" HorizontalAlign="Center" VerticalAlign="Middle"></asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NChamado" Text="Chamado" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgChamadoDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Chamado
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgChamadoAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NSerie" Text="Serie" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgSerieDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Série
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgSerieAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NDataAbertura" Text="Data Abertura" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgDataAberturaDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Data Abertura
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgDataAberturaAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NStatus" Text="Status" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgStatusDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Status
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgStatusAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NCidade" Text="Cidade" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgCidadeDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Cidade
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgCidadeAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NUF" Text="UF" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgUfDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            UF
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgUfAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NCategoria" Text="Categoria" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgCategoriaDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Categoria
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgCategoriaAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NResponsavel" Text="Responsável" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgResponsavelDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Responsável
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgResponsavelAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NTempoSLA" Text="Tempo SLA" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgTempoSLADec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            SLA Decorrido
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgTempoSLAAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NTempoSLARestante" Text="Tempo SLA Restante" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgTempoSLARestanteDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            SLA Restante
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgTempoSLARestanteAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NSLADefinida" Text="SLA Definida" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgSLADefinidaDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            SLA
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgSLADefinidaAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="NTempoTotal" Text="Tempo Total" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table style=" vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgTempoTotalDec" runat="server" ImageUrl="~/Imagens/SetaParaBaixo.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                        <td>
                                            Tempo Total
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgTempoTotalAsc" runat="server" ImageUrl="~/Imagens/SetaParaCima.jpg" onclick="imgOrd_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell Text="-" HorizontalAlign="Center" VerticalAlign="Middle">
                                
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlChamado" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlSerie" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlDtAbertura" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlStatus" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlCidade" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlUf" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlCategoria" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:DropDownList ID="ddlResponsavel" runat="server" onselectedindexchanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem Text="Todos" Value="Todos" Enabled="True" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Text="-" HorizontalAlign="Center" VerticalAlign="Middle">
                                <%--<asp:DropDownList ID="ddlTempoSLA" runat="server">
                                </asp:DropDownList>--%>
                            </asp:TableCell>
                            <asp:TableCell Text="-" HorizontalAlign="Center" VerticalAlign="Middle">
                                <%--<asp:DropDownList ID="ddlTempoSLARestante" runat="server">
                                </asp:DropDownList>--%>
                            </asp:TableCell>
                            <asp:TableCell Text="-" HorizontalAlign="Center" VerticalAlign="Middle">
                                <%--<asp:DropDownList ID="ddlSLADefinida" runat="server">
                                </asp:DropDownList>--%>
                            </asp:TableCell>
                            <asp:TableCell Text="-" HorizontalAlign="Center" VerticalAlign="Middle">
                                <%--<asp:DropDownList ID="ddlTempoTotal" runat="server">
                                </asp:DropDownList>--%>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
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