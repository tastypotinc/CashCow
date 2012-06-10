/* Contains all scripts that handles various Ajax request and response. No Ajax request should be made from anywhere in the application but here.
Make sure name of all methods starts with Ajax_ */

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Function to make Ajax request to specified URL and puts the response as inner html to the element provided. Request type is GET.
// $parentElement - The element in which the response should be shown as HTML.
// destUrl - The URL where request has to be sent.
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

// Function to make Ajax request to specified URL with given data. Updates "Global_GridKOModel" with response. The response should be in JSON.
// If overlay child element is present in parameter then close the overlay containing this child.
// dataToSend - Data to be sent to the server.
// destUrl - The URL where request has to be sent.
// requestType - Request type i.e "GET" or "POST".
// overlayChildElement - Any child element (preferably form) in layout that has to be closed on success.
function Ajax_GetAjaxResponseForGrid(dataToSend, destUrl, requestType, overlayChildElement) {
    $.ajax({
        url: destUrl,
        data: dataToSend,
        type: requestType,
        success: function (result) {
            ko.mapping.fromJS(result, Global_GridKOModel);

            // Call the overlay close function if overlayChildElement is not null
            if (overlayChildElement != null) {
                CloseOverlay(overlayChildElement);
            }
        },
        error: function (request, textStatus, errorThrown) {
            alert(request.statusText);
        }
    });
}