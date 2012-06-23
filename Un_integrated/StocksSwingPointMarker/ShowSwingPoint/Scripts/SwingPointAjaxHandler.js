function Ajax_GetResponseInInnerHtml($parentElement, destUrl) {
    $.ajax({
        url: destUrl,
        success: function (result) {
            $parentElement.html(result);
        },
        error: function (request, textStatus, errorThrown) {
            alert(request.statusText);
        }
    });
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function Ajax_GetAjaxResponseForPlotting(dataToSend, destUrl, requestType, chartContainerId) {
    $.ajax({
        url: destUrl,
        data: dataToSend,
        type: requestType,
        success: function (result) {
            // Call chart plotter here.
            alert(result);
            alert(chartContainerId);
            PlotOHLCChart(chartContainerId, result);
        },
        error: function (request, textStatus, errorThrown) {
            alert(request.statusText);
        }
    });
}