<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
                <h2>Sistema de Abertura de Requisições - <asp:Panel ID="cliente" runat="server"></asp:Panel></h2>
            <p>
                Utilize esse sistema para abrir chamados técnicos, solicitações de suprimentos ou para questionamento sobre a funcionalidade dos equipamentos.</p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <h3>Primeiros Passos:</h3>
    <ol class="round">
        <li class="one">
            <h5>Identifique as séries dos seus equipamentos</h5>
            Identifique as séries dos equipamentos instalados na sua unidade para que seja possível a abertura de requisições.</li>
        <li class="two">
            <h5>Adicione os equipamentos</h5>
            Para facilitar a abertura de requisições, selecione no menu <b>Meus Equipamentos</b> os equipamentos que deseja gerenciar.</li>
        <li class="three">
            <h5>Abra e acompanhe suas requisições</h5>
            No menu <b>Meus Chamados</b> é possível abrir, visualizar e acompanhar todas as requisições abertas.</li>
    </ol>
</asp:Content>