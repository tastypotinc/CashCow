<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CashCow.Master" Inherits="System.Web.Mvc.ViewPage<CashCow.Web.Models.WatchList.WatchListSearchModel>" %>

<%@ Import Namespace="Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cash Cow: Watch List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <form id="indexForm" method="post" action="WatchList">
        <div>
            <%: Html.LabelFor(x => x.Name) %>
            <%: Html.TextBoxFor(x => x.Name) %>

            <%: Html.LabelFor(x => x.BseSymbol) %>
            <%: Html.TextBoxFor(x => x.BseSymbol)%>

            <%: Html.LabelFor(x => x.NseSymbol) %>
            <%: Html.TextBoxFor(x => x.NseSymbol)%>

            <%: Html.LabelFor(x => x.AltNameOne) %>
            <%: Html.TextBoxFor(x => x.AltNameOne)%>

            <%: Html.LabelFor(x => x.AltNameTwo) %>
            <%: Html.TextBoxFor(x => x.AltNameTwo)%>

            <%: Html.LabelFor(x => x.AltNameThree) %>
            <%: Html.TextBoxFor(x => x.AltNameThree)%>

            <%: Html.LabelFor(x => x.TempName) %>
            <%: Html.TextBoxFor(x => x.TempName)%>

            <%: Html.LabelFor(x => x.IsActive) %>
            <%: Html.RadioButtonFor(x => x.IsActive, "true")%><span>&nbsp;<%: BoolYesNo.Yes %></span>
            <%: Html.RadioButtonFor(x => x.IsActive, "false")%><span>&nbsp;<%: BoolYesNo.No %></span>

            <%: Html.LabelFor(x => x.AlertRequired) %>
            <%: Html.RadioButtonFor(x => x.AlertRequired, "true")%><span>&nbsp;<%: BoolYesNo.Yes %></span>
            <%: Html.RadioButtonFor(x => x.AlertRequired, "false")%><span>&nbsp;<%: BoolYesNo.No %></span>

            <%: Html.LabelFor(x => x.ModifiedOn) %>
            <%: Html.TextBoxFor(x => x.ModifiedOn)%>

            <%: Html.LabelFor(x => x.CreatedOn) %>
            <%: Html.TextBoxFor(x => x.CreatedOn)%>

            <%: Html.LabelFor(x => x.SearchAgainst) %>
            <%: Html.RadioButtonFor(x => x.SearchAgainst, "true")%><span>&nbsp;<%: BoolYesNo.Yes %></span>
            <%: Html.RadioButtonFor(x => x.SearchAgainst, "false")%><span>&nbsp;<%: BoolYesNo.No %></span>

            <%: Html.LabelFor(x => x.SearchWithAnd) %>
            <%: Html.RadioButtonFor(x => x.SearchWithAnd, "true")%><span>&nbsp;<%: SearchCondition.And %></span>
            <%: Html.RadioButtonFor(x => x.SearchWithAnd, "false")%><span>&nbsp;<%: SearchCondition.Or %></span>

            <input type="submit" value="Search" />
        </div>
    </form>

    <div>
        <input id="indexInput_addWatchList" type="button" value="Add new" />
    </div>

    <% Html.RenderPartial("Grid/GridMainView", ViewData["gridModel"]); %>

    <script type="text/javascript">
        $(document).ready(function () {
            // Handle form submit.
            $('#indexForm').submit(function (e) {
                e.preventDefault();
                Ajax_GetAjaxResponseForGrid($(this).serialize(), this.action, "POST");
            });

            // Handle click for "indexInput_addWatchList"
            $("#indexInput_addWatchList").click(function () {
                GridLinkHandler("WatchList/EditWatchList", null, "Popup");
            });
        })
    </script>
</asp:Content>
