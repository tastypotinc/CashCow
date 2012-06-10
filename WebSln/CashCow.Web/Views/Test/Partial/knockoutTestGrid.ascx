<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CashCow.Web.Models.WatchList.WatchListGridModel>" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<div id="watchListGridMain">
    <div data-bind="ifnot: watchListItemList().length > 0">
        No record(s) were found.
    </div>
    <div data-bind="if: watchListItemList().length > 0">
        <table>
            <thead>
                <tr>
                    <th>Name *</th>
                    <th>BSE Symbol *</th>
                    <th>NSE Symbol *</th>
                    <th>Alt Name One</th>
                    <th>Alt Name Two</th>
                    <th>Alt Name Three</th>
                    <th>Temp Name</th>
                    <th>Is Active</th>
                    <th>Is Alert Required</th>
                    <th>Modified On</th>
                    <th>Created On</th>
                    <th></th>
                </tr>
            </thead>
            <tbody data-bind="foreach: watchListItemList">
                <%-- Row in display mode --%>
                <tr data-bind="visible: !$data.IsEditMode">
                    <td data-bind="text: $data.Name"></td>
                    <td data-bind="text: $data.BseSymbol"></td>
                    <td data-bind="text: $data.NseSymbol"></td>
                    <td data-bind="text: $data.AltNameOne"></td>
                    <td data-bind="text: $data.AltNameTwo"></td>
                    <td data-bind="text: $data.AltNameThree"></td>
                    <td data-bind="text: $data.TempName"></td>
                    <td data-bind="text: ($data.IsActive)? 'Yes' : 'No'"></td>
                    <td data-bind="text: ($data.AlertRequired)? 'Yes' : 'No'"></td>
                    <td data-bind="text: ConvertJsonDateToDDMMYYY($data.ModifiedOn)"></td>
                    <td data-bind="text: ConvertJsonDateToDDMMYYY($data.CreatedOn)"></td>
                    <td><button data-bind="click: $parent.ToggleEditMode">Edit</button></td>
                </tr>
                <%-- Row in edit mode --%>
                <tr data-bind="visible: $data.IsEditMode">
                    <td><input data-bind="value: $data.Name" /></td>
                    <td><input data-bind="value: $data.BseSymbol" /></td>
                    <td><input data-bind="value: $data.NseSymbol" /></td>
                    <td><input data-bind="value: $data.AltNameOne" /></td>
                    <td><input data-bind="value: $data.AltNameTwo" /></td>
                    <td><input data-bind="value: $data.AltNameThree" /></td>
                    <td><input data-bind="value: $data.TempName" /></td>
                    <td data-bind="text: $data.IsActive == 'true' ? 'Yes' : 'No'"></td>
                    <td data-bind="text: $data.AlertRequired == 'true' ? 'Yes' : 'No'"></td>
                    <td data-bind="text: ConvertJsonDateToDDMMYYY($data.ModifiedOn)"></td>
                    <td data-bind="text: ConvertJsonDateToDDMMYYY($data.CreatedOn)"></td>
                    <td><button data-bind="click: $parent.ToggleEditMode">Cancel</button></td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        // Apply Knockout view model bindings.
        ko.applyBindings(new watchListGridMainModel(), document.getElementById("watchListGridMain"));
     });
    
    function watchListGridMainModel() {
        var self = this;

        // Set model properties
        self.watchListItemList = ko.observableArray(<%= ViewData["WatchListItemList"]  %>);        
        self.currPage = ko.observable(<%: Model.CurrPage %>);
        self.pageSize = ko.observable(<%: Model.PageSize %>);        
        self.totalPages = ko.observable(<%: Model.TotalPages %>);

        // Create model functions.
        // Function toggle between edit and display mode.
        self.ToggleEditMode = function (watchListItem){
            watchListItem.IsEditMode = (watchListItem.IsEditMode) ? false : true;
            
            // Replace this element in the element. This will lead to toggling as the array is observable.
            var oldIndex = self.watchListItemList.indexOf(watchListItem);
            self.watchListItemList.remove(watchListItem);
            self.watchListItemList.splice(oldIndex, 0, watchListItem);
        }
    }

    function ConvertJsonDateToDDMMYYY(jsonDate)
    {
        var date = new Date(parseInt(jsonDate.substr(6)))
        return date.getDate() + '/' + (date.getMonth() + 1) +  '/' + date.getFullYear();
    }

</script>
