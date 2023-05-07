"use strict";

function getDimensions () {
    return {
        Width: window.innerWidth,
        Height: window.innerHeight
    }
}

function resizeListener(dotnetReference) {
    window.addEventListener('resize', () => {
        let browserHeight = window.innerHeight;
        let browserWidth = window.innerWidth;
        dotnetReference.invokeMethodAsync('SetBrowserDimensions', browserWidth, browserHeight)
            .then(() => {
            // success, do nothing
        }).catch(error => {
            console.log("Error during browser resize: " + error);
        });
    });
}
 