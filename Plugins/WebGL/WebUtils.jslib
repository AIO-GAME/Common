mergeInto(LibraryManager.library, {
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

  sendfile: function (){

  },
});
