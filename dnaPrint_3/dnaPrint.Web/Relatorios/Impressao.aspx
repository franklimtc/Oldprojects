<%@ Page Title="Impressões" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Impressao.aspx.cs" Inherits="dnaPrint.Web.Relatorios.Impressao" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="md-col-12">
                <h3 class="text-default text-center">Relatórios de Impressões</h3>
            </div>
            <hr />
            
        </div>

        <div class="row">
		    <div class="col-md-12">
			    <div class="tabbable" id="tabs-766203">
				    <ul class="nav nav-tabs">
					    <li class="active">
						    <a href="#panel01" data-toggle="tab">Equipamento</a>
					    </li>
					    <li>
						    <a href="#panel02" data-toggle="tab">Usuário</a>
					    </li>
				    </ul>
				    <div class="tab-content">
					    <div class="tab-pane active" id="panel01">
                            <br /><hr /><br />
                            <div class="row">
                                <div class="md-col-2">
                                    <span class="label label-default">Equipamento</span>
                                    <asp:TextBox runat="server" ID="tbEquipamento" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="md-col-2">
                                    <span class="label label-default">Data Inicial</span>
                                    <asp:TextBox runat="server" ID="tbEqptoDtIni" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="md-col-2">
                                    <span class="label label-default">Data Final</span>
                                    <asp:TextBox runat="server" ID="tbEqptoDtFin" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button runat="server" ID="btPesquisarEquipamento" Text="Pesquisar" CssClass="btn btn-default" OnClick="btPesquisarEquipamento_Click"/>
                            <asp:Button runat="server" ID="btExportarEquipamento" Text="Exportar" CssClass="btn btn-default" OnClick="btExportarEquipamento_Click" Enabled="false"/>
					    </div>
					    <div class="tab-pane" id="panel02">
                            <br /><hr /><br />
                            <div class="row">
                                <div class="md-col-2">
                                    <span class="label label-default">Usuário</span>
                                    <asp:TextBox runat="server" ID="tbUsuario" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="md-col-2">
                                    <span class="label label-default">Data Inicial</span>
                                    <asp:TextBox runat="server" ID="tbUserDtIni" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="md-col-2">
                                    <span class="label label-default">Data Final</span>
                                    <asp:TextBox runat="server" ID="tbUserDtFin" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button runat="server" ID="btPesquisarUsuario" Text="Pesquisar" CssClass="btn btn-default" OnClick="btPesquisarUsuario_Click"/>
                            <asp:Button runat="server" ID="btExportarUsuario" Text="Exportar" CssClass="btn btn-default"  OnClick="btExportarUsuario_Click"/>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="gvImpressoes" CssClass="table table-hover table-condensed" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="PrinterName" HeaderText="Impressora" SortExpression="PrinterName" />
                        <asp:BoundField DataField="UserName" HeaderText="Usuário" SortExpression="UserName" />
                        <asp:BoundField DataField="Document" HeaderText="Documento" SortExpression="Document" />
                        <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                        <asp:BoundField DataField="Copies" HeaderText="Cópias" SortExpression="Copies" />
                        <asp:BoundField DataField="PagesPrinted" HeaderText="Páginas" SortExpression="PagesPrinted" />
                        <asp:BoundField DataField="TotalPages" HeaderText="Total" SortExpression="TotalPages" />
                        <asp:BoundField DataField="Submitted" HeaderText="Data" SortExpression="Submitted" />
                    </Columns>

                </asp:GridView>
            </div>
        </div>

        <div class="row">
            <div class="md-col-12">
                <rsweb:ReportViewer ID="Report" Visible="false" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                </rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>
