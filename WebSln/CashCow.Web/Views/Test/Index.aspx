<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CashCow.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="CashCow.Grid.Models.Grid" %>
<%@ Import Namespace="Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("Grid/GridMainView", ViewData["gridModel"]); %>

</asp:Content>
