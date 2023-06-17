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

    // Set the screen to full
    OpenJSObserver : function(){
        // var canvas = document.getElementById("unity-canvas");
        // var bodyEle = document.querySelector("body");
        // var resizeObserver = new ResizeObserver(function(entries) {
        //     for (var i = 0; i < entries.length; i++) {
        //         var target = entries[i].target;
        //         var re = target.getBoundingClientRect();
        //         var width = re.width;
        //         var height = re.height;
        //         canvas.style.width = width+ "px";
        //         canvas.style.height = bodyEle.clientHeight+ "px";
        //     }
        // });
        // var targetEle = document.querySelector("body");
        // resizeObserver.observe(targetEle);
    },

    // Set the screen to full
    DestoryJSObserver : function(){
        // var targetEle = document.querySelector("body");
        // var resizeObserver = new ResizeObserver();
        // resizeObserver.unobserve(targetEle);
    }
});
