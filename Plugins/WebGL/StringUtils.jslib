mergeInto(LibraryManager.library, {
	getProductIdStrFromJSCode: function(){
		console.log("jslib getProductIdStrFromJSCode " + window.getModelIdFromJSCode());
		var id = window.getModelIdFromJSCode();
		var bufferSize = lengthBytesUTF8(id) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(id, buffer, bufferSize);
		return buffer;
	},
});