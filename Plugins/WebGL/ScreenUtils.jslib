mergeInto(LibraryManager.library, {
    // Set the screen to full
    JSSetFullScreen: function () {
        var htmlEle = document.querySelector("html");
        var canvas = document.getElementById("unity-canvas");
        canvas.height = document.documentElement.clientHeight;
        canvas.width = document.documentElement.clientWidth;
        canvas.style.height = "100%";
        canvas.style.width = htmlEle.clientWidth + 'px';
    },

    // Set the screen to full window
    JSSetWindowScreen: function () {
        var canvas = document.getElementById("unity-canvas");
        canvas.height = document.documentElement.clientHeight;
        canvas.width = document.documentElement.clientWidth;
        canvas.style.height = document.documentElement.clientHeight;
        canvas.style.width = document.documentElement.clientWidth;
    },
});
