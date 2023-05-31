var IOUtilsWebGL = {

  /**
   * Refresh the data to IndexedDB
   */
  SyncDB: function () {
    FS.syncfs(false, function (err) {
      if (err) console.log("syncfs error: " + err);
    });
  },

  /**
   * check whether the file exists
   * @param {string} url
   * @return {boolean}
   */
  Exists: function (filePath) {
    var xhr = new XMLHttpRequest();
    xhr.open('HEAD', UTF8ToString(filePath), false);
    xhr.send();
    return xhr.status == 200;
  },

  /**
   * get file size
   * @param {string} url
   * @return {number}
   */
  GetFileSize: function (filePath) {
    var xhr = new XMLHttpRequest();
    xhr.open('HEAD', UTF8ToString(filePath), false);
    xhr.send();
    return xhr.getResponseHeader("Content-Length");
  },

  /**
   * Read binary file
   * @param {string} url
   * @return {string} 'intptr,length'
   */
  ReadBinaryFile: function (filePath) {
    var xhr = new XMLHttpRequest;
    xhr.open("GET", UTF8ToString(filePath), false);
    xhr.send();
    var str = "";
    if (xhr.status == 200) {
        var res = xhr.response;
        var array = new Array();
        for (var i = 0, j = res.length; i < j; ++i) {
            array.push(res.charCodeAt(i));
        }
        res = null;
        try {
          var buffer = _malloc(array.length);
          HEAPU8.set(array, buffer);
          str = buffer + "," + array.length;
        }
        finally {
          _free(buffer);
        }
    }
    xhr = null
    var strbufferSize = lengthBytesUTF8(str) + 1;
    var strbuffer = _malloc(strbufferSize);
    stringToUTF8(str, strbuffer, strbufferSize);
    return strbuffer;
  },

  /**
   * Read text file
   * @param {string} url
   * @return {string}
   */
  ReadTextFile: function (filePath) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", UTF8ToString(filePath), false);
    xhr.onerror = function () {
      console.error('Network error');
    };

    xhr.send();
    var str = ""; 
    if (xhr.status === 200) {
      str = xhr.response;
    } else {
      console.error('Request url = ' + url + 'failed: ' + xhr.statusText);
    }
    xhr = null
    var strbufferSize = lengthBytesUTF8(str) + 1;
    var strbuffer = _malloc(strbufferSize);
    stringToUTF8(str, strbuffer, strbufferSize);
    return strbuffer;
  },

  /**
   * write binary file
   * @param {string} filePath
   * @param {byte[]} data
   * @param {size} size
   */
  WriteBinaryFile: function (filePath, data, size) {
    var dataArray = new Uint8Array(HEAPU8.buffer, data, size);
    var file = new Blob([dataArray], { type: "application/octet-stream" });
    saveAs(file, UTF8ToString(filePath));
  },

  /**
   * write json file
   * @param {string} filePath
   * @param {object} obj
   */
  WriteJsonFile: function (filePath, obj) {
    var json = JSON.stringify(obj);
    var blob = new Blob([json], { type: "application/json" });
    saveAs(blob, UTF8ToString(filePath));
  },

  /**
   * write text file
   * @param {string} filePath
   * @param {string} obj
   */
  WriteTextFile: function (filePath, obj) {
    var content = UTF8ToString(obj);
    var blob = new Blob([content], { type: "application/text" });
    saveAs(blob, UTF8ToString(filePath));
  },

  /**
   * delete file
   * @param {string} filePath
   * @returns {boolean}
   */
  DeleteFile: function (filePathPtr) {
    var filePath = UTF8ToString(filePathPtr);
    try {
      var xhr = new XMLHttpRequest();
      xhr.open('DELETE', filePath, false);
      xhr.send();

      if (xhr.status === 200) {
        return true;
      } else {
        console.error('Failed to delete file: ' + filePath);
        return false;
      }
    } catch (error) {
      console.error('Failed to delete file: ' + filePath);
      return false;
    }
  },

  /**
   * move file
   * @param {string} sourcePath
   * @param {string} targetPath
   * @returns {boolean}
   */
  MoveFile: function (sourcePath, targetPath) {
    var oldPath = UTF8ToString(sourcePath);
    var newPath = UTF8ToString(targetPath);

    try {
      var xhr = new XMLHttpRequest();
      xhr.open('MOVE', oldPath, false);
      xhr.setRequestHeader('Destination', newPath);
      xhr.send();

      if (xhr.status === 200) {
        return true;
      } else {
        console.error('Failed to move file from ' + oldPath + ' to ' + newPath);
        return false;
      }
    } catch (error) {
      console.error('Failed to move file from ' + oldPath + ' to ' + newPath);
      return false;
    }
  },

  /**
   * copy file
   * @param {string} sourcePath
   * @param {string} targetPath
   * @returns {boolean}
   */
  CopyFile: function (sourcePath, targetPath) {
    var srcFilePath = UTF8ToString(sourcePath);
    var destFilePath = UTF8ToString(targetPath);
    var data = FS.readFile(srcFilePath);
    FS.writeFile(destFilePath, data);
  },

  /**
   * read folder
   * @param {string} targetPath
   * @param {boolean} recursive
   * @returns {*}
   */
  ReadDirectory: function (targetPath, recursive) {
    var folderPath = UTF8ToString(targetPath);

    try {
      var xhr = new XMLHttpRequest();
      xhr.open('GET', folderPath, false);
      xhr.send();

      if (xhr.status === 200) {
        var folderData = JSON.parse(xhr.responseText);
        var files = folderData.files;
        var folders = folderData.folders;

        if (recursive) {
          for (var i = 0; i < folders.length; i++) {
            var subfolderPath = folderPath + '/' + folders[i];
            var subfolderData = Module.ccall('ReadFolder', 'string', ['string', 'number'], [subfolderPath, recursive]);
            var subfolderFiles = JSON.parse(subfolderData).files;
            files = files.concat(subfolderFiles);
          }
        }

        return files
      } else {
        console.error('Failed to read folder: ' + folderPath);
        return null; // 读取失败
      }
    } catch (error) {
      console.error('Failed to read folder: ' + folderPath);
      return null; // 读取失败
    }
  },


  // write folder
  CreateDirectory: function (path) {
    var folderPath = UTF8ToString(path);
    FS.mkdir(folderPath);
  },

  DeleteDirectory: function (path) {
    var folderPath = UTF8ToString(path);
    FS.rmdir(folderPath);
  },

  MoveDirectory: function (oldPath, newPath) {
    var oldFolderPath = UTF8ToString(oldPath);
    var newFolderPath = UTF8ToString(newPath);
    FS.rename(oldFolderPath, newFolderPath);
  },

  CopyDirectory: function (srcPath, destPath) {
    var srcFolderPath = UTF8ToString(srcPath);
    var destFolderPath = UTF8ToString(destPath);

    FS.mkdir(destFolderPath);

    var contents = FS.readdir(srcFolderPath);
    for (var i = 0; i < contents.length; i++) {
      var itemName = contents[i];
      if (itemName == '.' || itemName == '..') continue;

      var srcItemPath = srcFolderPath + '/' + itemName;
      var destItemPath = destFolderPath + '/' + itemName;
      var itemStat = FS.stat(srcItemPath);

      if (FS.isDir(itemStat.mode)) {
        _CopyFolder(srcItemPath, destItemPath);
      } else {
        var data = FS.readFile(srcItemPath);
        FS.writeFile(destItemPath, data);
      }
    }
  }
}

mergeInto(LibraryManager.library, IOUtilsWebGL);