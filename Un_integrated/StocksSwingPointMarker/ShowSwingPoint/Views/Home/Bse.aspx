<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ShowSwing.Master"
    Inherits="System.Web.Mvc.ViewPage<ShowSwingPoint.Models.HomeModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css">
        .jqplot-point-label
        {
            background-color: #660066;
            color: #EBDDE2;
            font-size: 11px;
            font-weight: bold;
        }
        .jqplot-highlighter-tooltip
        {
            background-color: #660066;
            color: #EBDDE2;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bse
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Bse</h2>
    <form id="bseForm" action="../home/bse" method="post">
    <div>
        <%: Html.DropDownListFor(
        x => x.SelectedStock,
                        new SelectList(Model.WatchList, "BSESymbol", "Name"), "-- Select --"

    ) %>
    </div>
    <div id="divCandlestickChart" style="height: 500px; width: 300%;">
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            // Handle dropdown change.
            $('#SelectedStock').change(function (e) {
                e.preventDefault();
                $("#bseForm").submit();
            });

            // Plot the graph if data is available.
            <% if(ViewData["PlotData"] != null)
               {%>
            
                var data = <%= ViewData["PlotData"] %>;                
                
                PlotCandleStickChart('divCandlestickChart', data);
            <%
               }%>               
        });
    </script>
</asp:Content>
