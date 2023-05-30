mergeInto(LibraryManager.library, {
  // Get file MD5
  JSGetNetMD5: function (url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", Pointer_stringify(url), false);
    xhr.responseType = "arraybuffer";
    xhr.onload = function () {
      if (xhr.status == 200) {
        var data = new Uint8Array(xhr.response);
        var md5 = new SparkMD5.ArrayBuffer();
        md5.append(data);
        var result = md5.end();
        var buffer = _malloc(result.length + 1);
        writeAsciiToMemory(result, buffer);
        Runtime.dynCall("vi", callback, [buffer]);
        _free(buffer);
      } else {
        Runtime.dynCall("vi", callback, [0]);
      }
    };
    xhr.onerror = function () {
      Runtime.dynCall("vi", callback, [0]);
    };
    xhr.send();
  },
});
