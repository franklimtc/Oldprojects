<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Relatorios.aspx.cs" Inherits="Suprimentos_Relatorios" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="container-fluid">
	<div class="row">
		<div class="col-md-2">
            <span class="label label-default">Data Inicial: </span>
            <asp:TextBox runat="server" ID="tbDtInicial" CssClass="form-control" Text="<% DateTime.Now.AddDays(-7).ToShortDateString(); %>"></asp:TextBox><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*" Type="Date" ControlToValidate="tbDtInicial" CssClass="label label-danger"></asp:RangeValidator>
		</div>
        <div class="col-md-2">
            <span class="label label-default">Data Inicial: </span>
            <asp:TextBox runat="server" ID="tbDtFinal" CssClass="form-control" Text="<% DateTime.Now.ToShortDateString(); %>">></asp:TextBox><asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="*" Type="Date" ControlToValidate="tbDtFinal" CssClass="label label-danger">"></asp:RangeValidator>
		</div>
	</div>
    <div class="row">
        <div class="col-lg-2">
            <asp:Button runat="server" ID="btPesquisar" Text ="Pesquisar" CssClass="btn btn-default media-middle" OnClick="btPesquisar_Click" />

        </div>
        <div class="col-lg-2">
            <asp:Button runat="server" ID="btReport" Text ="Emitir Relatório" CssClass="btn btn-default media-middle" OnClick="btReport_Click"/>
        </div>
    </div>
    <br />
    <asp:Panel runat="server" ID="pnGv">

    
    <div class="row">
        <div class="col-mg-12">
            <asp:GridView runat="server" ID="gvEnvios" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" DataSourceID="objDSEnvios" AllowPaging="True" PageSize="100" OnRowCommand="gvEnvios_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdEnvio" HeaderText="IdEnvio" SortExpression="IdEnvio" />
                    <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
                    <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                    <asp:BoundField DataField="Etiqueta" HeaderText="Etiqueta" SortExpression="Etiqueta" />
                    <asp:BoundField DataField="TpEnvio" HeaderText="TpEnvio" SortExpression="TpEnvio" />
                    <asp:BoundField DataField="Postagem" HeaderText="Postagem" SortExpression="Postagem" />
                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                    <asp:BoundField DataField="DtEnvio" HeaderText="DtEnvio" SortExpression="DtEnvio" />
                    <asp:BoundField DataField="Filial" HeaderText="Filial" SortExpression="Filial" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btExcluir" 
                                Text="Excluir" CommandName="Excluir" 
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <PagerSettings Mode="NumericFirstLast" />

            </asp:GridView>
            <asp:ObjectDataSource ID="objDSEnvios" runat="server" SelectMethod="ListarPorData" TypeName="EnvioSuprimento">
                <SelectParameters>
                    <asp:ControlParameter ControlID="tbDtInicial" Name="dataInicial" PropertyName="Text" Type="DateTime" />
                    <asp:ControlParameter ControlID="tbDtFinal" Name="dataFinal" PropertyName="Text" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnReport" Visible="false">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
</asp:Content>

