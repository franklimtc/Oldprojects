<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.min.css" rel="stylesheet"/>
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        

        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <asp:Panel runat="server" ID="pnSolicitacoes" CssClass="jumbotron text-center">
                        <h1>Lista de Suprimentos Solicitados</h1>
                        <asp:GridView runat="server" ID="gvSolicitados" CssClass="table table-condensed table-hover">
			            </asp:GridView>
                    </asp:Panel>
                      
                </div>
            </div>
	        <div class="row">
                
		        <div class="col-md-12" id="divSolicitados">
                    <div class="jumbotron text-center">
                      <h1>Lista de Suprimentos enviados</h1>
                      <%--<p>Resize this responsive page to see the effect!</p>--%>
                    </div>
                    <%--table table-condensed table-hove--%>
			        <asp:GridView runat="server" ID="gvEnvios" CssClass="table table-condensed table-hover" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="serie" HeaderText="Série" SortExpression="serie" />
                            <asp:BoundField DataField="postagem" HeaderText="Postagem" SortExpression="postagem" />
                            <asp:BoundField DataField="tpSuprimento" HeaderText="Tipo" SortExpression="tpSuprimento" />
                            <asp:BoundField DataField="dtEnvio" HeaderText="Data do Envio" SortExpression="dtEnvio" />
                            <asp:BoundField DataField="statusEntrega" HeaderText="Status de Entrega" SortExpression="statusEntrega" />
                            <asp:BoundField DataField="prazoEntrega" HeaderText="Prazo" SortExpression="prazoEntrega" />
                            <asp:BoundField DataField="dtEntrega" HeaderText="Data da Entrega" SortExpression="dtEntrega" />
                        </Columns>

			        </asp:GridView>
                    <br /><asp:Label runat="server" ID="lbErroSerie" Text="*ERRO: Equipamento não localizado!" CssClass="label label-danger center-block" Visible="false"></asp:Label>
		        </div>
	        </div>
        </div>        

        <div class="container-fluid">
	        <div class="row">
		        <div class="col-md-3">
			 
			        <address>
				         <strong>São Luís-MA</strong><br /> Av. dos Holandeses, Qda. 32, nº 1, Lojas 06/07, Ed. Praia Shopping<br /> Calhau, CEP: 65071-380<br /> <a class="glyphicon glyphicon-earphone"></a> (98) 3301-2400
			        </address>
		        </div>
		        <div class="col-md-3">
			 
			        <address>
				         <strong>Fortaleza - CE</strong><br /> Rua Raimundo Oliveira Filho, 332.<br />Papicu, CEP: 60175-175<br /><a class="glyphicon glyphicon-earphone"></a> (85) 3022-0900
			        </address>
		        </div>
                 <div class="col-md-3">
			 
			        <address>
				         <strong>Fortaleza - CE (BNB)</strong><br /> Av. Dr. Silas Munguba, 5700<br />Passaré<br /><a class="glyphicon glyphicon-earphone"></a> (85) 3299-5516
			        </address>
		        </div>
	        </div>
        </div>

    </div>
    </form>
</body>
</html>
