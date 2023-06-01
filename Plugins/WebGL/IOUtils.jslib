var IOUtilsWebGL = {

  /**
   * Refresh the data to IndexedDB
   */
  SyncDB: function () {
    FS.syncfs(false, function (err) {
      if (err) console.error("syncfs error: " + err);
    });
  },

  /**
   * check whether the file exists
   * @param {string} filePath
   * @return {boolean}
   */
  Exists: function (filePath) {
    var status = false;
    var fPath = UTF8ToString(filePath);
    if (/^http/.test(fPath)) {
      var xhr = new XMLHttpRequest();
      xhr.open("GET", fPath, false);
      xhr.send();
      status = xhr.status == 200;
      xhr = null;
    } else {
      try {
        status = FS.analyzePath(fPath).exists;
      } catch (err) {
        console.error("read the local file failed -> error " + err);
      }
    }
    fPath = null;
    return status;
  },

  /**
   * get file size
   * @param {string} filePath
   * @return {number}
   */
  GetFileSize: function (filePath) {
    var fPath = UTF8ToString(filePath);
    var size = 0;
    if (/^http/.test(fPath)) {
      var xhr = new XMLHttpRequest();
      xhr.open("HEAD", fPath, false);
      xhr.send();
      size = xhr.getResponseHeader("Content-Length");
    } else {
      try {
        size = FS.stat(fPath).size;
      } catch (err) {
        console.error("can not read the file, please confirm the file is existing -> error " + err);
      }
    }
    return size;
  },

  /**
   * read binary file
   * @param {string} url
   * @return {string} intptr,length
   */
  ReadBinaryFile: function (filePath) {
    var fPath = UTF8ToString(filePath);
    if (/^http/.test(fPath)) {
      var str = "";
      var xhr = new XMLHttpRequest;
      xhr.open("GET", UTF8ToString(filePath), false);
      xhr.send();
      if (xhr.status === 200) {
        var res = xhr.response;
        var array = new Array;
        for (var i = 0, j = res.length; i < j; ++i) {
          array.push(res.charCodeAt(i))
        }
        res = null;
        try {
          var buffer = _malloc(array.length);
          HEAPU8.set(array, buffer);
          str = buffer + "," + array.length
        } finally {
          _free(buffer)
        }
      }
      xhr = null;
      try {
        var strBufferSize = lengthBytesUTF8(str) + 1;
        var strBuffer = _malloc(strBufferSize);
        stringToUTF8(str, strBuffer, strBufferSize);
        return strBuffer
      } catch (err) {
        console.error("something -> error " + err)
      }
    } else {
      try {
        var content = FS.readFile(fPath);
        var buffer2 = _malloc(content.length);
        writeArrayToMemory(content, buffer2);
        return buffer2
      } catch (err) {
        console.error("please confirm the file is existing -> error " + err)
      }
    }
    return null;
  },

  /**
   * read text file
   * @param {string} url
   * @return {string}
   */
  ReadTextFile: function (filePath) {
    var fPath = UTF8ToString(filePath);
    if (/^http/.test(fPath)) {
      var xhr = new XMLHttpRequest();
      xhr.open("GET", fPath, false);
      xhr.onerror = (function () {
        console.error("Network error")
      });
      xhr.send();
      var str = "";
      if (xhr.status === 200) {
        str = xhr.response
      } else {
        console.error("Request url = " + fPath + "failed: " + xhr.statusText)
      }
      xhr = null;
      var bufferSize = lengthBytesUTF8(str) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(str, buffer, bufferSize);
      return buffer;
    } else {
      var content = FS.readFile(fPath, { encoding: 'utf8' });
      var buffer = _malloc(content.length + 1);
      stringToUTF8(content, buffer);
      return buffer;
    }
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
  DeleteFile: function (filePath) {
    var fPath = UTF8ToString(filePath);
    var status = false;
    if (/^http/.test(fPath)) {
      try {
        var xhr = new XMLHttpRequest();
        xhr.open("DELETE", fPath, false);
        xhr.send();
        if (xhr.status === 200) {
          status = true;
        } else {
          console.error("failed to delete file: " + fPath);
        }
      } catch (error) {
        console.error("failed to delete file: " + fPath);
      } finally {
        xhr = null;
      }
    } else {
      try {
        FS.unlink(fPath);
      } catch (error) {
        console.error("cannot find the file -> error " + error);
      }
    }
    return status;
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
      FS.stat(oldPath, function (eer, stats) {
        if (err) throw err;
        if (stats.isFile()) {
          FS.rename(oldPath, newPath, function (err) {
            if (err) throw err;
            _SyncDB();
          });
        } else {
          console.error('The file does not exist or is occupied -> ' + oldPath);
        }
      });
    } catch (error) {
      console.error("can not find the file -> error " + error);
    }
  },

  /**
   * copy file
   * @param {string} sourcePath
   * @param {string} targetPath
   * @returns {boolean}
   */
  CopyFile: function (sourcePath, targetPath) {
    var target = UTF8ToString(targetPath);
    var source = UTF8ToString(sourcePath);
    var status = false;
    if (/^http/.test(source)) {
      var data;
      var xhr = new XMLHttpRequest;
      xhr.open("GET", source, false);
      xhr.send();
      if (xhr.status === 200) {
        data = xhr.response
      } else {
        console.error("can not find the file : " + source)
      }
      try {
        FS.writeFile(target, data, { encoding: 'utf8', flag: 'w' });
        status = true
      } catch (error) {
        console.error("failed to copy file -> error " + error)
      } finally {
        xhr = null;
        data = null
      }
    } else {
      try {
        FS.copyFile(source, target);
        status = true
      } catch (error) {
        console.error("something error -> error " + error)
      }
    }
    _SyncDB();
    return status
  },

  /**
   * read folder
   * @param {string} targetPath
   * @param {boolean} recursive
   * @returns {*}
   */
  ReadDirectory: function (targetPath, recursive) {
    var target = UTF8ToString(targetPath);
    try {
      var xhr = new XMLHttpRequest();
      xhr.open('GET', target, false);
      xhr.send();
      if (xhr.status === 200) {
        var data = JSON.parse(xhr.responseText);
        var files = data.files;
        var folders = data.folders;
        if (recursive) {
          for (var i = 0; i < folders.length; i++) {
            var subfolderPath = target + '/' + folders[i];
            var subfolderData = Module.ccall('ReadFolder', 'string', ['string', 'number'], [subfolderPath, recursive]);
            var subfolderFiles = JSON.parse(subfolderData).files;
            files = files.concat(subfolderFiles);
          }
        }
        return files;
      } else {
        console.error('Failed to read folder: ' + target);
        return null; // read error
      }
    } catch (error) {
      console.error('Failed to read folder: ' + target + "-> error " + error);
      return null; // read error
    }
  },

  /**
   * create folder
   * @param {string} path
   */
  CreateDirectory: function (path) {
    var folderPath = UTF8ToString(path);
    FS.mkdir(folderPath);
  },

  /**
   * delete folder
   * @param {string} path
   */
  DeleteDirectory: function (path) {
    var folderPath = UTF8ToString(path);
    try {
      FS.rmdir(folderPath);
    } catch (error) {
      console.log("delete error -> error " + error);
    }
  },

  /**
   * move folder
   * @param {string} path
   */
  MoveDirectory: function (oldPath, newPath) {
    var oldFolderPath = UTF8ToString(oldPath);
    var newFolderPath = UTF8ToString(newPath);
    try {
      FS.rename(oldFolderPath, newFolderPath);
    } catch (error) {
      console.log("move failed -> error " + error);
    }
  },

  /**
   * copy folder
   * @param {string} path
   */
  CopyDirectory: function (sourcePath, destinationPath, copySubdirectories) {
    var source = UTF8ToString(sourcePath);
    var destination = UTF8ToString(destinationPath);
    if (!FS.isDir(source)) FS.mkdir(destination);
    if (/^http/.test(source)) {
      var xhr = new XMLHttpRequest();
      // xhr.responseType = "blob"
      try {
        xhr.open("GET", source, false);
        xhr.send();
        if (xhr.status === 200) {
          var folderData = xhr.responseText;
          var files = folderData.files;
          console.log(xhr)
          var folders = folderData.folders;
          for (var i = 0; i < files.length; i++) {
            var remoteFilePath = source + "/" + files[i];
            var localFilePath = destination + "/" + files[i];
            var fileData = Module.ccall("ReadRemoteFile", "string", ["string"], [remoteFilePath]);
            Module.ccall("WriteFile", "void", ["string", "string"], [localFilePath, fileData])
          }
          if (copySubdirectories) {
            for (var j = 0; j < folders.length; j++) {
              var remoteSubfolderPath = source + "/" + folders[j];
              var localSubfolderPath = destination + "/" + folders[j];
              Module.ccall("CopyRemoteFolder", "void", ["string", "string", "boolean"], [remoteSubfolderPath, localSubfolderPath, copySubdirectories])
            }
          } else {
            console.error("Failed to copy remote folder: " + source)
          }
        }
      } catch (error) {
        console.error("Failed to copy remote folder: " + source + error)
      } finally {
        xhr = null
      }
    } else {
      var files = FS.readdir(source);
      for (var i = 0; i < files.length; i++) {
        var file = files[i];
        if (file === "." || file === "..") {
          continue
        }
        var sourceFilePath = source + "/" + file;
        var destinationFilePath = destination + "/" + file;
        if (FS.isDir(FS.stat(sourceFilePath).mode)) {
          if (copySubdirectories) {
            this._CopyDirectory(sourceFilePath, destinationFilePath, copySubdirectories)
          }
        } else {
          FS.copyFile(sourceFilePath, destinationFilePath)
        }
      }
    }
  }
};

mergeInto(LibraryManager.library, IOUtilsWebGL);
