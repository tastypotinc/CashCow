<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CashCow.Web.Models.WatchList.WatchListModel>" %>
<%@ Import Namespace="Helpers" %>

<form id="editForm" method="post" action="WatchList/EditWatchList">
    <div>
        <%: Html.HiddenFor(x => x.WatchListID) %>
            
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
        
        <%: Html.HiddenFor(x => x.ModifiedOn)%>
        <%: Html.HiddenFor(x => x.CreatedOn)%>

        <% Html.RenderPartial("OverylayFormFooter"); %>
    </div>
</form>

<script type="text/javascript">
    $(document).ready(function () {
        // Handle form submit.
        $('#editForm').submit(function (e) {
            e.preventDefault();
            Ajax_GetAjaxResponseForGrid($(this).serialize(), this.action, "POST", this);
        });
    })
</script>