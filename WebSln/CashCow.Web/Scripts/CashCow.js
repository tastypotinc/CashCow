/* Contains all scripts to be used across the application. Please use only common scripts here. */

// Function to close an overylay. Overy is identified with the help of class OverlayParent.
// currElement - The element that called this function. Generally the close or save button.
function CloseOverlay(currElement) {
    // Get the overylay parent element.
    var $overlayContainerElement = $(currElement).closest($(".overlayContainer"));
    var $overlayEditorElement = $(currElement).closest($(".gridOverlayEditor"));

    // Hide the overlay.
    $overlayContainerElement.slideToggle("slow");

    // Remove contents of the overlay from the DOM.
    $overlayEditorElement.html("");

    // Restore body scroll.
    $("body").css("overflow", "auto");
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////