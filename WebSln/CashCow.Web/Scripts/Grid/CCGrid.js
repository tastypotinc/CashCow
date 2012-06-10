/* Contains all scripts related to grid. Please use only this file for grid script. */

// Variable to keep track of alternate rows that is being rendered.
var isPositionEven = false;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle KO afterRender event for body row.
function AfterRenderBodyRowEventHandler(elements, data) {
    // Set row even or odd position.
    isPositionEven = !isPositionEven;
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle link click in grid.
// action - Action URL of the link clicked.
// alertMessage - Alert message to be shown when link is clicked.
// actionBehaviour - Defines the way link should open to the user.
// root - The full KO model.
function GridLinkHandler(action, alertMessage, actionBehaviour, root) {

    // Now handle different behaviours here.
    switch (actionBehaviour) {
        // This function will get called for this behaviour only if there is some alert message defined. 
        // Simply show the message and continue. 
        case "Redirect":
            if (confirm(alertMessage)) {
                window.location.replace(action);
            }
            else {
                return false;
            }
            break;

        // This is suppose to open the action in a modal window. 
        case "Popup":
            // Show alert message if there be one.
            if (alertMessage) {
                if (confirm(alertMessage)) {
                    Ajax_GetResponseInInnerHtml($("#gridMainViewDiv_gridOverlayEditor"), action);
                }
            }
            else {
                Ajax_GetResponseInInnerHtml($("#gridMainViewDiv_gridOverlayEditor"), action);
            }

            // Show the grid overlay.
            $("#gridMainViewDiv_gridOverlay").slideToggle("slow");

            // Hide the scroll of the body but make sure it is shown when the popup is closed.
            $("body").css("overflow", "hidden");

            break;

        // This is suppose to post to the server silently i.e. will not redirect user to any page. 
        case "PostSilent":
            // Show alert message if there be one.
            if (alertMessage) {
                if (confirm(alertMessage)) {
                    Ajax_GetAjaxResponseForGrid(ko.toJSON(root.GridContext), action, "POST");
                }
            }
            else {
                Ajax_GetAjaxResponseForGrid(ko.toJSON(root.GridContext), action, "POST");
            }

            break;
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle click on first page navigation link.
// data - The current item passed by default by KO. In this case it has the full KO model.
function GotoFirstPage(data) {
    // Set the current page id.
    data.GridContext.GridPager.CurrPageId(0);

    // Set the current pager element set number.
    data.GridContext.GridPager.CurrPagerElementSetNumber(1);

    // Make server request to the default action of the grid. Send the grid context.
    Ajax_GetAjaxResponseForGrid(ko.toJSON(data.GridContext), data.GridContext.DefaultAction(), "POST");
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle click on previous pager element set navigation link.
// data - The current item passed by default by KO. In this case it has the full KO model.
function GotoPreviousPagerElementSet(data) {
    // Set the current pager element set number.
    data.GridContext.GridPager.CurrPagerElementSetNumber(data.GridContext.GridPager.CurrPagerElementSetNumber() - 1);

    // Set the current page id.
    var newPageId = (data.GridContext.GridPager.CurrPagerElementSetNumber() - 1) * data.GridContext.GridPager.NumberOfPagerElements();
    data.GridContext.GridPager.CurrPageId(newPageId);

    // Make server request to the default action of the grid. Send the grid context.
    Ajax_GetAjaxResponseForGrid(ko.toJSON(data.GridContext), data.GridContext.DefaultAction(), "POST");
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle click on a pager element.
// gridContext - The grid context in KO form.
// data - The current item passed by default by KO. In this case it has pager element number.
function GotoSpecifiedPage(gridContext, data) {
    // Set the current page id.
    gridContext.GridPager.CurrPageId(data - 1);

    // Make server request to the default action of the grid. Send the grid context.
    Ajax_GetAjaxResponseForGrid(ko.toJSON(gridContext), gridContext.DefaultAction(), "POST");
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle click on next pager element set navigation link.
// data - The current item passed by default by KO. In this case it has the full KO model.
function GotoNextPagerElementSet(data) {
    // Set the current pager element set number.
    data.GridContext.GridPager.CurrPagerElementSetNumber(data.GridContext.GridPager.CurrPagerElementSetNumber() + 1);

    // Set the current page id.
    var newPageId = (data.GridContext.GridPager.CurrPagerElementSetNumber() - 1) * data.GridContext.GridPager.NumberOfPagerElements();
    data.GridContext.GridPager.CurrPageId(newPageId);

    // Make server request to the default action of the grid. Send the grid context.
    Ajax_GetAjaxResponseForGrid(ko.toJSON(data.GridContext), data.GridContext.DefaultAction(), "POST");
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle click on last page navigation link.
// data - The current item passed by default by KO. In this case it has the full KO model.
function GotoLastPage(data) {
    // Set the current page id.
    data.GridContext.GridPager.CurrPageId(data.GridContext.GridPager.TotalPages() - 1);

    // Set the current pager element set number.
    data.GridContext.GridPager.CurrPagerElementSetNumber(GridContext.GridPager.TotalPagerElementSets());

    // Make server request to the default action of the grid. Send the grid context.
    Ajax_GetAjaxResponseForGrid(ko.toJSON(data.GridContext), data.GridContext.DefaultAction(), "POST");
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle page size dropdown change event.
// data - The current item passed by default by KO. In this case it has the full KO model.
function ChangePageSize(data) {
    // The page size is already changed by KO. Since page size is changed hence user should be taken to first page.
    GotoFirstPage(data);
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle pager element dropdown change event.
// data - The current item passed by default by KO. In this case it has the full KO model.
function ChangePagerElementSet(data) {
    // The current pager element set number has already changed by KO. Just calculate the current page number (not ID) and send user to that page.    
    var newPageNum = (data.GridContext.GridPager.CurrPagerElementSetNumber() - 1) * data.GridContext.GridPager.NumberOfPagerElements() + 1;
    GotoSpecifiedPage(data.GridContext, newPageNum);
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle click on a sortable column i.e. handles sorting on columns.
// root - The full KO model.
// defaultSortDirection - Default sort direction. 0 = Ascending.
// data - The current item passed by default by KO. In this case it has grid header cell.
function SortColumn(root, defaultSortDirection, data) {
    // Frist change the sort info in grid context.
    // If the column is already sorted then just change the sort direction else set default sort and change the sort column name.
    if (data.SortColumnName() == root.GridContext.SortInfo.SortOn()) {
        if (root.GridContext.SortInfo.SortOrder() == 0) {
            // Change direction to descending.
            root.GridContext.SortInfo.SortOrder(1);
        }
        else {
            // Change direction to ascending.
            root.GridContext.SortInfo.SortOrder(0);
        }
    }
    else {
        root.GridContext.SortInfo.SortOrder(defaultSortDirection);
        root.GridContext.SortInfo.SortOn(data.SortColumnName());
    }
    
    // If there be more than one page  or if sorting is suppose to happen on a link column then post to the server else do the sorting here itself.
    if (root.GridContext.GridPager.TotalPages() > 1 || data.ColumnTypeString() == "Link") {
        // Make server request to the default action of the grid. Send the grid context.
        Ajax_GetAjaxResponseForGrid(ko.toJSON(root.GridContext), root.GridContext.DefaultAction(), "POST");
    }
    else {
        // Get the index of header cell clicked.
        var indexOfColumnToSort = root.GridHeader.Cells.indexOf(data);

        // Iterate through each body row, pick up the above indexed cell from each row and put them in an array.
        // Basically create and array of cells of the column that has to be sorted.
        var listOfCellsToSort = new Array();
        for (loop = 0; loop < root.GridBodyModel.Rows().length; loop++) {
            var row = root.GridBodyModel.Rows()[loop];

            listOfCellsToSort.push(row.Cells()[indexOfColumnToSort]);
        }

        // Now sort the above list of cells as per sort info set in grid context earlier and the column type.
        // If the column type is Image then sort the column based on image path.        
        if (data.ColumnTypeString() == "Image") {
            // Sort ascending.
            if (root.GridContext.SortInfo.SortOrder() == 0) {
                listOfCellsToSort.sort(function (left, right) { return left.ImagePath() == right.ImagePath() ? 0 : (left.ImagePath() < right.ImagePath() ? -1 : 1) });
            }
            // Sort descending.
            else {
                listOfCellsToSort.sort(function (left, right) { return left.ImagePath() == right.ImagePath() ? 0 : (left.ImagePath() < right.ImagePath() ? 1 : -1) });
            }
        }
        // If column type is Text then simple sort on text will work.
        else if (data.ColumnTypeString() == "Text") {
            // Sort ascending.
            if (root.GridContext.SortInfo.SortOrder() == 0) {
                listOfCellsToSort.sort(function (left, right) { return left.Text() == right.Text() ? 0 : (left.Text() < right.Text() ? -1 : 1) });
            }
            // Sort descending.
            else {
                listOfCellsToSort.sort(function (left, right) { return left.Text == right.Text() ? 0 : (left.Text() < right.Text() ? 1 : -1) });
            }
        }

        // Now create one more array. This array will have a copy of body rows such that it is arranged in the array in the order in which
        // cells are arranged in the array "listOfCellsToSort". Once the array is created replace the original array in root (parameter).
        // To do this, iterate through the array "listOfCellsToSort" containing sorted cells and copy the body row that contains the cell to the new
        // array. Finally replace the original array with the new sorted array.        
        var listOfSortedRows = new Array();
        for (cellsToSortLoop = 0; cellsToSortLoop < listOfCellsToSort.length; cellsToSortLoop++) {
            for (rowsLoop = 0; rowsLoop < root.GridBodyModel.Rows().length; rowsLoop++) {
                // Push the row element into the new array if the cell element from listOfCellsToSort exists in the list of cells in this row.
                if (root.GridBodyModel.Rows()[rowsLoop].Cells.indexOf(listOfCellsToSort[cellsToSortLoop]) > -1) {
                    listOfSortedRows.push(root.GridBodyModel.Rows()[rowsLoop]);
                    break;
                }
            }
        }

        // Now replace the original row array with listOfSortedRows.
        root.GridBodyModel.Rows(listOfSortedRows);
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to handle search on grid.
// data - The current item passed by default by KO. In this case full KO model.
// event - The current event object passed by KO. In this case the keydown event.
function SearchGrid(data, event) {    
    // Read the key code and proceed ahead only if enter is pressed.
    var keyCode = (event.which ? event.which : event.keyCode);
    if (keyCode === 13) {
        // Trim the string only if it is not null or empty.
        if (data.GridContext.SearchInfo.TextSearchKey() != null && data.GridContext.SearchInfo.TextSearchKey() != "") {            
            data.GridContext.SearchInfo.TextSearchKey(data.GridContext.SearchInfo.TextSearchKey().replace(/^\s+|\s+$/g, ''));
        }

        // The model is already updated. Simply make the server request.
        // Make server request to the default action of the grid. Send the grid context.        
        Ajax_GetAjaxResponseForGrid(ko.toJSON(data.GridContext), data.GridContext.DefaultAction(), "POST");

        return false;        
    }

    return true;
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
