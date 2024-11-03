<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script runat="server">
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(
                this,
                typeof(Page),
                "Mapa",
                Maps.ConstruirMapa(),
                true);
        }

    </script>--%>
</head>
<body>
     <form id="form1" runat="server">
        <div>
            <h2>Mapa de Equipamentos</h2>
            <asp:Button ID="btCadastro" runat="server" Text="Cadastrar Unidades" OnClick="btCadastro_Click" />
            <p></p>
            <div id ="map"  style="width: 100%; position: absolute; height: 100%" ></div>
            <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBVZmbIaQO1xQSLspKmwcMNH5HOCbaLWx8&callback=initMap">

        </script>
        </div>
    </form>
</body>
</html>
