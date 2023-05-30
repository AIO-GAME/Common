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

	// Read binary file
	// url string
	// callback function<int,int>
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
});
