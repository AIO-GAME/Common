mergeInto(LibraryManager.library, {
	// Refresh the data to IndexedDB
	JSSyncDB: function () {
		FS.syncfs(false, function (err) {
			if (err) console.log("syncfs error: " + err);
		});
	},

	// Check whether the file exists
	JSFileExists: function (url) {
		var xhr = new XMLHttpRequest();
		xhr.open('HEAD', Pointer_stringify(url), false);
		xhr.send();
		return xhr.status == 200 ? 1 : 0;
	},

	// Check whether the directory exists
	JSDirectoryExists: function (url) {
		var xhr = new XMLHttpRequest();
		xhr.open('HEAD', Pointer_stringify(url), false);
		xhr.send();
		return xhr.status === 200;
	},


	/**
	 * Read binary file
	 * @param {string} url 
	 * @param {(int)=>int} callback 
	 */
	JSReadBinaryFile: function (url, callback) {
		var xhr = new XMLHttpRequest();
		xhr.open('GET', Pointer_stringify(url), true);
		xhr.responseType = 'arraybuffer';
		xhr.onload = function () {
			if (xhr.status == 200) {
				var byteArray = new Uint8Array(xhr.response);
				var buffer = _malloc(byteArray.length);
				HEAPU8.set(byteArray, buffer);
				Runtime.dynCall('vii', callback, [buffer, byteArray.length]);
				_free(buffer);
			} else {
				Runtime.dynCall('vii', callback, [0, 0]);
			}
		};
		xhr.send();
	},

/**
 * read xml file
 * @param {string} path
 * @param {(string)=>any} callback
 */
 ReadXMLFile : function (path, callback) {
  var xhr = new XMLHttpRequest();
  xhr.responseType = 'document';
  xhr.onload = function() {
    if (xhr.status === 200) {
      var xmlDoc = xhr.responseXML;
      var xmlStr = new XMLSerializer().serializeToString(xmlDoc);
      var bufferSize = lengthBytesUTF8(xmlStr) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(xmlStr, buffer, bufferSize);
      runtime.dynCall('vi', callback, [buffer]);
      _free(buffer);
    } else {
      console.error('Request failed: ' + xhr.statusText);
    }
  };
  xhr.onerror = function() {
    console.error('Network error');
  };
  xhr.open('GET', path);
  xhr.send();
},

/**
 * read JSON file 
 * @param {string} path 
 * @param  callback 
 * 
 */
ReadJSONFile:function (path, callback) {
  var xhr = new XMLHttpRequest();
  xhr.responseType = 'text';
  xhr.onload = function() {
    if (xhr.status === 200) {
      var jsonStr = xhr.responseText;
      var jsonObj = JSON.parse(jsonStr);
      var bufferSize = lengthBytesUTF8(jsonStr) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(jsonStr, buffer, bufferSize);
      runtime.dynCall('vii', callback, [buffer, jsonObj]);
      _free(buffer);
    } else {
      console.error('Request failed: ' + xhr.statusText);
    }
  };
  xhr.onerror = function() {
    console.error('Network error');
  };
  xhr.open('GET', path);
  xhr.send();
},


//write binary file 
WriteBinaryFile2:function (filePath, data, size) {
  var dataArray = new Uint8Array(HEAPU8.buffer, data, size);
  var file = new Blob([dataArray], {type: "application/octet-stream"});
  saveAs(file, UTF8ToString(filePath));
},



//write XML file 
WriteXmlFile2 :  function (filePath, obj) {
  var xw = new XMLWriter();
  xw.startDocument();
  xw.startElement("root");
  for (var key in obj) {
    if (obj.hasOwnProperty(key)) {
      xw.startElement(key);
      xw.text(obj[key]);
      xw.endElement();
    }
  }
  xw.endElement();
  xw.endDocument();
  var xml = xw.toString();
  var blob = new Blob([xml], {type: "application/xml"});
  saveAs(blob, UTF8ToString(filePath));
},


//write JSON file 
WriteJsonFile : function (filePath, obj) {
  var json = JSON.stringify(obj);

  var blob = new Blob([json], {type: "application/json"});
  saveAs(blob, UTF8ToString(filePath));
},



/**
 * 
 * @param {string} filePathPtr 
 * @returns {boolean}
 */
DeleteFile :  function(filePathPtr) {
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
 * @param {string} oldPathPtr 
 * @param {string} newPathPtr 
 * @returns {boolean}
 */
MoveFile: function(oldPathPtr, newPathPtr) {
  var oldPath = UTF8ToString(oldPathPtr);
  var newPath = UTF8ToString(newPathPtr); 

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
 * 
 * @param {string} srcPath 
 * @param {string} destPath 
 * @returns {boolean} 
 */
CopyFile: function (srcPath, destPath) {
  var srcFilePath = Pointer_stringify(srcPath);
  var destFilePath = Pointer_stringify(destPath);
  var data = FS.readFile(srcFilePath);
  FS.writeFile(destFilePath, data);
},

/**
 * 
 * @param {string} folderPathPtr 
 * @param {boolean} recursive 
 * @returns {*} 
 */
ReadFolder:function(folderPathPtr, recursive) {
  var folderPath = UTF8ToString(folderPathPtr); 

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
WriteFolder: function (path) {
  var folderPath = Pointer_stringify(path);
  FS.mkdir(folderPath);
},



 DeleteFolder:function (path) {
  var folderPath = Pointer_stringify(path);
  FS.rmdir(folderPath);
},

MoveFolder: function (oldPath, newPath) {
  var oldFolderPath = Pointer_stringify(oldPath);
  var newFolderPath = Pointer_stringify(newPath);
  FS.rename(oldFolderPath, newFolderPath);
},

CopyFolder:function (srcPath, destPath) {
  var srcFolderPath = Pointer_stringify(srcPath);
  var destFolderPath = Pointer_stringify(destPath);

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





	
});















































