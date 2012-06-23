<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ShowSwing.Master"
    Inherits="System.Web.Mvc.ViewPage<ShowSwingPoint.Models.HomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bse
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Bse</h2>
    <form id="bseForm" action="/home/test" method="post">
    <div>
        <%: Html.DropDownListFor(
        x => x.SelectedStock,
                        new SelectList(Model.WatchList, "BSESymbol", "Name"), "-- Select --"

    ) %>
    </div>
    <div id="divOHLCChart" style="height:400px;width:100%; "></div>
    <div id="divCandlestickChart" style="height:400px;width:100%; "></div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            // Handle dropdown change.
            $('#SelectedStock').change(function (e) {
                e.preventDefault();
                Ajax_GetAjaxResponseForPlotting($("#bseForm").serialize(), this.action, "POST", "divOHLCChart");
            });
        });
    </script>
</asp:Content>
