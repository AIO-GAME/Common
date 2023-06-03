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
   * Path to the file or folder
   * return 0: not exist
   * return 1: file
   * return 2: folder
   * return 3: other
   */
  IsPathFileOrFolder: function (path) {
    var targetPath = UTF8ToString(path);
    var analyze = FS.analyzePath(targetPath);
    if (analyze.exists) {
      if (FS.isFile(analyze.object.mode)) {
        return 1;
      } else if (FS.isDir(analyze.object.mode)) {
        return 2;
      } else {
        return 3;
      }
    }
    return 0;
  },

  /**
   * free memory address
   * @param {int} ptr
   */
  FreeMemory: function (ptr) {
    var index = freeMemoryList.indexOf(ptr);
    if (index >= 0) {
      freeMemoryList.splice(index, 1);
      _free(ptr)
    }
  },

  /**
   * free memory list
   */
  $freeMemoryList: new Array(),

  /**
   * check whether the file exists
   * @param {string} filePath
   * @return {boolean}
   */
  ExistsFile: function (filePath) {
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
        var analyze = FS.analyzePath(fPath.replace("\\", "/"));
        if (analyze.exists && FS.isFile(analyze.object.mode)) {
          status = true;
        }
      } catch (err) {
        console.error("read the local file failed -> error " + err);
      }
    }
    fPath = null;
    return status;
  },

  /**
   * check whether the directory exists
   * @param {string} filePath
   * @return {boolean}
   */
  ExistsDirectory: function (filePath) {
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
        var analyze = FS.analyzePath(fPath.replace("\\", "/"));
        if (analyze.exists && FS.isDir(analyze.object.mode)) {
          status = true;
        }
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
      var xhr = new XMLHttpRequest;
      xhr.open("HEAD", fPath, false);
      xhr.send();
      size = xhr.getResponseHeader("Content-Length")
    } else {
      try {
        fPath = fPath.replace("\\", "/");
        if (FS.analyzePath(fPath).exists) {
          var stat = FS.stat(fPath);
          size = stat.size
        }
      } catch (err) {
        console.log("err  -> " + err)
      }
    }
    return size
  },


  /**
   * read binary file
   * @param {string} url
   * @return {string} intptr,length
   */
  ReadBinaryFile: function (filePath) {
    var fPath = UTF8ToString(filePath);
    var str = "";
    var array = null;
    if (/^http/.test(fPath)) {
      var xhr = new XMLHttpRequest;
      xhr.open("GET", fPath, false);
      xhr.send();
      if (xhr.status === 200) {
        var res = xhr.response;
        array = [];
        for (var i = 0, j = res.length; i < j; ++i) {
          array.push(res.charCodeAt(i))
        }
        res = null;
      }
      xhr = null
    } else {
      fPath = fPath.replace("\\", "/");
      var analyze = FS.analyzePath(fPath);
      if (analyze.exists && FS.isFile(analyze.object.mode)) {
        array = FS.readFile(fPath)
      }
    }
    if (array != null) {
      try {
        var buffer = _malloc(array.length);
        HEAPU8.set(array, buffer);
        str = buffer + "," + array.length;
      } finally {
        freeMemoryList.push(buffer);
      }
    }
    var strBufferSize = lengthBytesUTF8(str) + 1;
    var strBuffer = _malloc(strBufferSize);
    try {
      stringToUTF8(str, strBuffer, strBufferSize)
    } catch (err) {
      console.error("something -> error " + err)
    } finally {
      str = null;
      return strBuffer;
    }
  },

  /**
   * read text file
   * @param {string} url
   * @return {string}
   */
  ReadTextFile: function (filePath) {
    var fPath = UTF8ToString(filePath);
    var str = "";
    if (/^http/.test(fPath)) {
      var xhr = new XMLHttpRequest;
      xhr.open("GET", fPath, false);
      xhr.onerror = (function () {
        console.error("Network error")
      });
      xhr.send();
      if (xhr.status === 200) {
        str = xhr.response
      } else {
        console.error("Request url = " + fPath + "failed: " + xhr.statusText)
      }
      xhr = null
    } else {
      fPath = fPath.replace("\\", "/");
      var analyze = FS.analyzePath(fPath);
      if (analyze.exists && FS.isFile(analyze.object.mode)) {
        str = FS.readFile(fPath, { encoding: "utf8" })
      }
    }

    var bufferSize = lengthBytesUTF8(str) + 1;
    var buffer = _malloc(str.length + 32);
    stringToUTF8(str, buffer, bufferSize);
    return buffer
  },

  /**
   * write binary file
   * @param {string} filePath
   * @param {byte[]} data
   * @param {number} size
   * @param {boolean} overwrite
   */
  WriteBinaryFile: function (overwrite, filePath, data, size) {
    var target = UTF8ToString(filePath).replace("\\", "/");
    var analyze = FS.analyzePath(target);
    if (analyze.exists && FS.isFile(analyze.object.mode)) {
      if (overwrite) FS.unlink(target);
      else return;
    }

    var parent = PATH.dirname(target);
    analyze = FS.analyzePath(parent);
    if (!analyze.exists) {
      FS.mkdirTree(parent);
      FS.chmod(parent, 511);
    } else if (analyze.exists && !FS.isDir(analyze.object.mode)) {
      FS.mkdir(parent, analyze.object.mode);
      FS.chmod(parent, 511);
    }

    try {
      var byteArray = new Uint8Array(Module.HEAPU8.buffer, data, size);
      FS.writeFile(target, byteArray, { encoding: 'binary', flag: 'w' });
    } catch (error) {
      console.error("failed to write binary file: " + target + " -> " + error)
    } finally {
      analyze = null;
      target = null;
      byteArray = null;
    }
  },

  /**
   * write text file
   * @param {string} filePath
   * @param {string} obj
   * @param {boolean} overwrite
   */
  WriteTextFile: function (filePath, obj, overwrite) {
    var target = UTF8ToString(filePath).replace("\\", "/");
    var analyze = FS.analyzePath(target);
    if (analyze.exists && FS.isFile(analyze.object.mode)) {
      if (overwrite) FS.unlink(target);
      else return;
    }

    var parent = PATH.dirname(target);
    analyze = FS.analyzePath(parent);
    if (!analyze.exists) {
      FS.mkdirTree(parent);
      FS.chmod(parent, 511);
    } else if (!FS.isDir(analyze.object.mode)) {
      FS.mkdir(parent, analyze.object.mode);
      FS.chmod(parent, 511);
    }

    try {
      var data = UTF8ToString(obj);
      FS.writeFile(target, data, { encoding: 'utf8', flag: 'w' });
    } catch (error) {
      console.error("failed to write text file: " + target + " -> " + error);
    } finally {
      analyze = null;
      target = null;
      data = null;
    }
  },

  /**
   * delete file
   * @param {string} filePath
   * @returns {boolean}
   */
  DeleteFile: function (filePath) {
    var fPath = UTF8ToString(filePath).replace("\\", "/");
    var status = false;
    if (/^http/.test(fPath)) {
      try {
        var xhr = new XMLHttpRequest;
        xhr.open("DELETE", fPath, false);
        xhr.send();
        if (xhr.status === 200) {
          status = true
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
        var analyze = FS.analyzePath(fPath);
        if (analyze.exists && FS.isFile(analyze.object.mode))
          FS.unlink(fPath);
        status = true;
      } catch (error) {
        console.error(" error  -> " + error)
      } finally {
        analyze = null;
      }
    }
    return status
  },

  /**
   * move file
   * @param {string} sourcePath
   * @param {string} targetPath
   * @param {boolean} overwrite
   * @returns {boolean}
   */
  MoveFile: function (sourcePath, targetPath, overwrite) {
    var oldPath = UTF8ToString(sourcePath).replace("\\", "/");
    var newPath = UTF8ToString(targetPath).replace("\\", "/");

    var newAnalyze = FS.analyzePath(newPath);
    if (newAnalyze.exists && FS.isFile(newAnalyze.object.mode) && !overwrite) {
      if (!overwrite) {
        console.log(newPath + " -> file exist , skip");
        return
      } else {
        FS.unlink(newPath)
      }
    }

    try {
      var parentDir = PATH.dirname(newPath)
      var parentAnalyze = FS.analyzePath(parentDir)
      if (!parentAnalyze.exists) {
        FS.mkdirTree(parentDir);
        FS.chmod(parentDir, 511);
      } else if (!FS.isDir(parentAnalyze.object.mode)) {
        FS.mkdir(parentDir, parentAnalyze.object.mode);
        FS.chmod(parentDir, 511);
      }

      var data = FS.readFile(oldPath, { encoding: "binary" });
      FS.writeFile(newPath, data, { encoding: 'binary' });
      FS.unlink(oldPath);
    } catch (error) {
      console.error("error  ->" + error)
    } finally {
      data = null;
      parentAnalyze = null;
      parentDir = null;
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
    try {
      var fPath = UTF8ToString(path).replace("\\", "/");
      var analyze = FS.analyzePath(fPath);
      if (!analyze.exists) {
        FS.mkdirTree(fPath);
        FS.chmod(fPath, 511);
      } else if (!FS.isDir(analyze.object.mode)) {
        FS.mkdir(fPath, analyze.object.mode);
        FS.chmod(fPath, 511);
      }
    } catch (error) {
      console.log("create dir : " + fPath + " -> error " + error)
    } finally {
      fPath = null;
      analyze = null
    }
  },

  /**
   * delete folder
   * @param {string} path
   */
  DeleteDirectory: function (path) {
    var fPath = UTF8ToString(path).replace("\\", "/");
    var analyze = FS.analyzePath(fPath);
    try {
      if (analyze.exists && FS.isDir(analyze.object.mode)) {
        var stack = [fPath];
        while (stack.length > 0) {
          var currentPath = stack.pop();
          var files = FS.readdir(currentPath);
          for (var i = 0; i < files.length; i++) {
            if (files[i] === "." || files[i] === "..") continue;
            var filePath = currentPath + "/" + files[i];
            var analyzeTemp = FS.analyzePath(filePath);
            if (analyzeTemp.exists) {
              if (FS.isDir(analyzeTemp.object.mode)) stack.push(filePath);
              else FS.unlink(filePath);
            }
          }
        }
        FS.rmdir(fPath);
      }
    } catch (error) {
      console.log("delete dir : " + fPath + " -> error " + error)
    } finally {
      analyze = null;
      fPath = null
    }
  },

  /**
   * move folder
   * @param {string} path
   */
  MoveDirectory: function (oldPath, newPath, overwrite) {
    var oldFolderPath = UTF8ToString(oldPath).replace("\\", "/");
    var newFolderPath = UTF8ToString(newPath).replace("\\", "/");
    var oldAnalyze = FS.analyzePath(oldFolderPath);
    var newAnalyze = FS.analyzePath(newFolderPath);
    try {
      if (oldAnalyze.exists && FS.isDir(oldAnalyze.object.mode)) {
        // if new folder not exist, rename directly
        if (!newAnalyze.exists || !FS.isDir(newAnalyze.object.mode)) {
          FS.rename(oldFolderPath, newFolderPath);
        } else if (overwrite) {
          // delete new folder
          FS.rmdir(newFolderPath);
          FS.rename(oldFolderPath, newFolderPath);
        }
      }
    } catch (error) {
      console.log("move dir failed | " + oldFolderPath + " -> " + newFolderPath + "| -> error " + error);
    } finally {
      oldAnalyze = null;
      newAnalyze = null;
      oldFolderPath = null;
      newFolderPath = null;
    }
  },

  /**
   * copy folder
   * @param {string} sourcePath
   * @param {string} destinationPath
   * @param {boolean} copySubdirectories
   * @param {boolean} overwrite
   */
  CopyDirectory: function (sourcePath, destinationPath, copySubdirectories, overwrite) {
    var source = UTF8ToString(sourcePath);
    var destination = UTF8ToString(destinationPath).replace("\\", "/");

    if (/^http/.test(source)) {
      console.warn("Cannot copy HTTP directory " + source);
      return;
    }

    var analyze = FS.analyzePath(destination);
    if (!analyze.exists) {
      FS.mkdirTree(destination);
      FS.chmod(destination, 511)
    } else {
      if (!FS.isDir(analyze.object.mode)) {
        FS.mkdir(destination, analyze.object.mode);
        FS.chmod(destination, 511)
      } else if (overwrite) {
        FS.rmdir(destination);
        FS.mkdir(destination);
        FS.chmod(destination, 511)
      } else return;
    }

    var files = FS.readdir(source);
    for (var i = 0; i < files.length; i++) {
      var file = files[i];
      if (file === "." || file === "..") continue;

      var sourceFilePath = source + "/" + file;
      analyze = FS.analyzePath(sourceFilePath);

      var destinationFilePath = destination + "/" + file;
      if (FS.isDir(analyze.mode) && copySubdirectories) {
        var sourceBufferSize = lengthBytesUTF8(sourceFilePath) + 1;
        var sourceBuffer = _malloc(sourceFilePath.length + 32);
        stringToUTF8(sourceFilePath, sourceBuffer, sourceBufferSize);

        var destinationBufferSize = lengthBytesUTF8(destinationFilePath) + 1;
        var destinationBuffer = _malloc(destinationFilePath.length + 32);
        stringToUTF8(destinationFilePath, destinationBuffer, destinationBufferSize);

        this._CopyDirectory(sourceBuffer, destinationBuffer, copySubdirectories, overwrite)
      } else if (FS.isFile(analyze.mode)) {
        FS.copyFile(sourceFilePath, destinationFilePath)
      }
    }

    analyze = null;
    source = null;
    destination = null;
    files = null;
  }
};

autoAddDeps(IOUtilsWebGL, '$freeMemoryList');
mergeInto(LibraryManager.library, IOUtilsWebGL);
