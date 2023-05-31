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

  /**
   * Get Url Params
   * @param defaultValue
   * @returns params value
   */

  JSGetUrlParams: function (defaultValue) {
    var name = Pointer_stringify(defaultValue);
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) {
      var returnStr = unescape(r[2]);
      var bufferSize = lengthBytesUTF8(returnStr) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(returnStr, buffer, bufferSize);
      return buffer;
    }
    else return null; //返回参数值
  },
});
