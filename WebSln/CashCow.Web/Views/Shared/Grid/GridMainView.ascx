<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CashCow.Grid.Models.Grid.GridModel>" %>
<%@ Import Namespace="Helpers" %>

<!-- This is the main grid container. -->
<div id="gridMainViewDiv_gridMainContainer">
    
    <!-- Create the grid search field -->
    <!-- ko ifnot: GridContext.DisableGridSearch -->
        <div>
            <input type="text"
            data-bind="value: GridContext.SearchInfo.TextSearchKey,
                valueUpdate: 'afterkeydown',
                event: {keydown: SearchGrid},
                keydownBubble: false" />
        </div>
    <!-- /ko -->

    <!-- When no record is found. -->
    <div data-bind="ifnot: GridContext.GridPager.TotalPages() > 0">
        No record(s) were found.
    </div>
    
    <!-- This is actual grid table container. This is binded when records are found -->
    <div data-bind="if: GridContext.GridPager.TotalPages() > 0">
        
        <!-- Grid table. Set following properties: class, inline style -->
        <table data-bind="css:{<%: Model.GridContext.CssClass %>:GridContext.CssClass != null}">
            
            <!-- Grid header. -->
            <thead>
                            
                <!-- Header row. Also container for header cells iterator. Set following properties: class -->
                <tr data-bind="foreach: GridHeader.Cells,
                css:{<%: Model.GridHeader.CssClass %>:GridHeader.CssClass != null}">
                    
                    <!-- Header cell. Set following properties: class, inline style -->
                    <th data-bind="attr:{class:$data.CssClass},
                    style:{maxWidth: $data.Width() > 0? $data.Width() + 'px': '0px'}">
                        
                        <!-- Render header cell contents as according to ColumnTypeString. -->
                        <!-- ko if: $data.ColumnTypeString() == "CheckBox" -->
                            <input type="checkbox" name="selectRow" data-bind="disable: $data.IsDisabled" />
                        <!-- /ko -->
                        <!-- ko ifnot: $data.ColumnTypeString() == "CheckBox" -->
                            <!-- Now create header depending on weather or not column has been marked as sortable. -->
                            <!-- ko ifnot: $data.AllowSorting() -->
                                <span data-bind="text:$data.Label"></span>
                            <!-- /ko -->
                            <!-- ko if: $data.AllowSorting() -->
                                <span data-bind="text:$data.Label,
                                    css:{sortableColumn: $data.SortColumnName() != $root.GridContext.SortInfo.SortOn(),
                                         sortedAsc: $data.SortColumnName() == $root.GridContext.SortInfo.SortOn() &&
                                            $root.GridContext.SortInfo.SortOrder() == 0,
                                         sortedDesc: $data.SortColumnName() == $root.GridContext.SortInfo.SortOn() &&
                                            $root.GridContext.SortInfo.SortOrder() == 1},
                                    click: SortColumn.bind($data, $root, 0),
                                    clickBubble: false"></span>
                            <!-- /ko -->
                        <!-- /ko -->
                    </th>
                </tr>
            </thead>

            <!-- Grid body. -->
            <tbody>
                <!-- Iterator to construct body. -->
                <!-- ko foreach: {data: GridBodyModel.Rows, afterRender : AfterRenderBodyRowEventHandler} -->
                    <!-- Body row. Also container for body row cells iterator. Set class for rows and alternate rows. -->
                    <tr data-bind="foreach: $data.Cells,
                        css:{<%: Model.GridBodyModel.CssClass %>:$root.GridBodyModel.CssClass != null,
                        <%: Model.GridContext.RowCss %>: !isPositionEven, <%: Model.GridContext.AlternateRowCss %>: isPositionEven}">
                    
                        <!-- Body row cell. Set the following properties: class -->
                        <td data-bind="attr:{class:$data.CssClass}">
                    
                            <!-- Render cells contents as according to ColumnTypeString -->
                            <!-- ko if: $data.ColumnTypeString() == "CheckBox" -->
                                <input type="checkbox" name="selectRow" data-bind="disable: $data.IsDisabled" />
                            <!-- /ko -->
                            <!-- ko if: $data.ColumnTypeString() == "Image" -->
                                <img data-bind="attr:{src: $data.ImagePath}" />
                            <!-- /ko -->
                            <!-- ko if: $data.ColumnTypeString() == "Text" -->
                                <span data-bind="text: $data.Text"></span>
                            <!-- /ko -->
                            <!-- ko if: $data.ColumnTypeString() == "Link" -->                                
                                <!-- ko foreach: $data.Links-->
                                    <!-- If image path has been specified, create image link else go for text link. -->
                                        <!-- ko if: $data.ImagePath() != null -->
                                            <!-- If behaviour is redirect and alert not specified, construct normal link else override onclick event. -->
                                            <!-- ko if: $data.BehaviourString() == "Redirect" && $data.AlertMessage() == null -->                                                
                                                <span>
                                                    <a data-bind="attr: {href: $data.Action}">
                                                        <img data-bind="attr: {src: $data.ImagePath}" />
                                                    </a>
                                                </span>                                                
                                            <!-- /ko -->
                                            <!-- ko if: $data.BehaviourString() != "Redirect" || $data.AlertMessage() != null -->                                                
                                                <span>
                                                    <a data-bind="attr: {href: '#'},
                                                    click: GridLinkHandler.bind($data, $data.Action(), $data.AlertMessage(), $data.BehaviourString(), $root),
                                                    clickBubble: false">
                                                        <img data-bind="attr: {src: $data.ImagePath}" />
                                                    </a>
                                                </span>                                                
                                            <!-- /ko -->
                                        <!-- /ko -->
                                        <!-- ko if: $data.Text() != null && $data.ImagePath() == null -->
                                            <!-- If behaviour is redirect and alert not specified, construct normal link else override onclick event. -->
                                            <!-- ko if: $data.BehaviourString() == "Redirect" && $data.AlertMessage() == null -->                                                
                                                <span>
                                                    <a data-bind="text: $data.Text, attr: {href: $data.Action}"></a>
                                                </span>                                                
                                            <!-- /ko -->
                                            <!-- ko if: $data.BehaviourString() != "Redirect" || $data.AlertMessage() != null -->                                                
                                                <span>
                                                    <a data-bind="text: $data.Text, attr: {href: '#'},
                                                    click: GridLinkHandler.bind($data, $data.Action(), $data.AlertMessage(), $data.BehaviourString(), $root),
                                                    clickBubble: false"></a>
                                                </span>                                                
                                            <!-- /ko -->
                                        <!-- /ko -->
                                <!-- /ko -->
                            <!-- /ko -->
                        </td>
                    </tr>
                <!-- /ko -->

                <!-- Grid pager row. Set following properties: class -->
                <tr data-bind="css:{<%: Model.GridContext.GridPager.CssClass %>:GridContext.GridPager.CssClass != null}">
                    <!-- Set the colspan so as to occupy the total row. -->
                    <td data-bind="attr:{colspan: GridHeader.Cells().length }">
                        <!-- The pager element table. -->
                        <div>
                            <table>
                                <tr>
                                    <!-- Current page info. -->
                                    <td>
                                        <span data-bind="text: 'Page ' + GridContext.GridPager.CurrPageNumber() + ' of ' + GridContext.GridPager.TotalPages()"></span>
                                    </td>

                                    <!-- First page link. If currently on first page, do not create it. -->
                                    <td>
                                        <!-- ko if: GridContext.GridPager.CurrPageNumber() > 1 -->
                                            <span>
                                                <a href="#" data-bind="click: GotoFirstPage, clickBubble: false">&lt;&lt; First</a>
                                            </span>
                                        <!-- /ko -->
                                    </td>

                                    <!-- Link for navigation to previous set of pager elements.
                                    If currently first set of pager elements, do not create it. -->
                                    <td>
                                        <!--  ko if: GridContext.GridPager.CurrPagerElementSetNumber() > 1 -->
                                            <span><a href="#" data-bind="click: GotoPreviousPagerElementSet, clickBubble: false">&lt;</a></span>
                                        <!-- /ko -->
                                    </td>

                                    <!-- Pager elements. -->
                                    <!-- ko foreach: GridContext.GridPager.PagerElements -->
                                        <td>
                                            <!-- If it is current page then do not create element as link. -->
                                            <!-- ko if: $data == $root.GridContext.GridPager.CurrPageNumber() -->
                                                <span data-bind="text: $data"></span>
                                            <!-- /ko -->
                                            <!-- ko ifnot: $data == $root.GridContext.GridPager.CurrPageNumber() -->
                                                <span><a href="#" data-bind="text: $data, click: GotoSpecifiedPage.bind($data, $root.GridContext), clickBubble: false"></a></span>
                                            <!-- /ko -->
                                        </td>
                                    <!-- /ko -->

                                    <!-- Link for navigation to next set of pager elements.
                                    If currently last set of pager elements, do not create it. -->
                                    <td>
                                        <!--  ko if: GridContext.GridPager.CurrPagerElementSetNumber() < GridContext.GridPager.TotalPagerElementSets() -->
                                            <span><a href="#" data-bind="click: GotoNextPagerElementSet, clickBubble: false">&gt;</a></span>
                                        <!-- /ko -->
                                    </td>

                                    <!-- Last page link. If currently on last page, do not create it. -->
                                    <td>
                                        <!-- ko if: GridContext.GridPager.CurrPageNumber() < GridContext.GridPager.TotalPages() -->
                                            <span><a href="#" data-bind="click: GotoLastPage, clickBubble: false">Last &gt;&gt;</a></span>
                                        <!-- /ko -->
                                    </td>

                                    <!-- Pager set drop down. If there be only one pager element set, do not create. -->
                                    <td>
                                        <!-- ko if: GridContext.GridPager.TotalPagerElementSets > 1 -->
                                            <span>Pages: </span>
                                            <select data-bind="options: GridContext.GridPager.PagerElementSetList,
                                            optionsText: 'Key', optionsValue: 'Value', value: GridContext.GridPager.CurrPagerElementSetNumber,
                                            event: {change: ChangePagerElementSet}, changeBubble: false"></select>
                                        <!-- /ko -->
                                    </td>

                                    <!--  Page size drop down. -->
                                    <td>
                                        <span>Page Size: </span>
                                        <select data-bind="options: GridContext.GridPager.AllPageSizes,
                                        value: GridContext.GridPager.PageSize, optionsCaption: 'Select',
                                        event: {change: ChangePageSize}, changeBubble: false"></select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <div id="gridMainViewDiv_gridOverlay" class="overlayContainer"><div id="gridMainViewDiv_gridOverlayEditor" class="gridOverlayEditor"></div></div>
</div>

<script type="text/javascript">       

        $(document).ready(function () {
        // Apply Knockout view model bindings when document is in ready state.
        ko.applyBindings(Global_GridKOModel, document.getElementById("gridMainViewDiv_gridMainContainer"));
    });

    // Serialize the server model object. It will be used to create observable model.
    Global_GridKOModel = ko.mapping.fromJS (<%= DataFormatter.SerializeToJson(Model) %>);

</script>
