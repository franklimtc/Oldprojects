<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Atualizar.aspx.cs" Inherits="Atendimentos_Atualizar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:MultiView ID="mvAtualizar" runat="server" ActiveViewIndex="0" >
            <asp:View ID="Inicio" runat="server">
                <fieldset style="text-align:center; width:700px;">
                    <legend>Atualização de Solicitações</legend>
                    <asp:Button ID="btSolAgendar" runat="server" Text="Agendar" Width="150px" Height="150px" Font-Size="X-Large" OnClick="btSolAgendar_Click" />
                    <asp:Button ID="btRetorno" runat="server" Text="Retorno" Width="150px" Height="150px" Font-Size="X-Large" OnClick="btRetorno_Click"/>
                    <asp:Button ID="btConcluir" runat="server" Text="Concluir" Width="150px" Height="150px" Font-Size="X-Large" OnClick="btConcluir_Click" />
                    <asp:Button ID="btCancelar" runat="server" Text="Cancelar" Width="150px" Height="150px" Font-Size="X-Large" OnClick="btCancelar_Click" />
                </fieldset>
            </asp:View>
            <asp:View ID="Agendar" runat="server">
                <fieldset style="width:230px; text-align:center">
                    <legend>Agendamento</legend>
                    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                    <br /><asp:Label ID="lb1" runat="server">Data:</asp:Label> <asp:TextBox ID="tbData" runat="server" Enabled="false" Width="100px"></asp:TextBox>    
                    <asp:Button ID="btAgendar" runat="server" Text="Agendar" OnClick="btAgendar_Click" />
                </fieldset>
                    <asp:Button ID="btVoltar" runat="server" Text="Voltar" OnClick="VoltarMonitor" />
                <br />
                <fieldset>
                    <legend>Histórico de Agendamento</legend>
                    <asp:GridView ID="gvHistoricoAgendamento" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="idPrevisao" DataSourceID="dsHistoricoAgendamento" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="idPrevisao" HeaderText="idPrevisao" InsertVisible="False" ReadOnly="True" SortExpression="idPrevisao" />
                            <asp:BoundField DataField="idReqAtendimento" HeaderText="idReqAtendimento" SortExpression="idReqAtendimento" />
                            <asp:BoundField DataField="dtPrevisao" DataFormatString="{0:d}" HeaderText="dtPrevisao" SortExpression="dtPrevisao" />
                            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                            <asp:BoundField DataField="data" DataFormatString="{0:d}" HeaderText="data" SortExpression="data" />
                            <asp:BoundField DataField="obs" HeaderText="obs" SortExpression="obs">
                            <ItemStyle Width="200px" />
                            </asp:BoundField>
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
                    <asp:SqlDataSource ID="dsHistoricoAgendamento" runat="server" ConnectionString="<%$ ConnectionStrings:pecasSigep01 %>" SelectCommand="select * from reqAtendimentosPrevisao where idReqAtendimento = @idReqAtendimento">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idReqAtendimento" QueryStringField="idReqAtendimento" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </fieldset>
            </asp:View>
            <asp:View ID="Concluir" runat="server">
                <fieldset style="width:230px; text-align:center">
                    <legend>Conclusão</legend>
                    <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged"></asp:Calendar>
                    <br /><asp:Label ID="Label1" runat="server">Data:</asp:Label> <asp:TextBox ID="tbDataConclusao" runat="server" Enabled="false" Width="100px"></asp:TextBox>    
                    <br /> <asp:CheckBox ID="checkPendencias" runat="server" Text="Pendente Peças ou Suprimentos" />
                    <br /><asp:Button ID="btConcluir2" runat="server" Text="Concluir" OnClick="btConcluir2_Click" />
                </fieldset>
            </asp:View>
        </asp:MultiView>
    <div>
        
    </div>
    </form>
</body>
</html>
