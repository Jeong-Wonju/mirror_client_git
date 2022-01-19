var LibraryWebSockets = {
$webSocketInstances: [],

SocketCreate: function()
{
	var socket = {
		socket: null,
		buffer: null,
		error: null,
		messages: null,
		open:false
	}

	var instance = webSocketInstances.push(socket) - 1;
	return instance;
},

SocketConnect: function(socketInstance, url)
{
	var socket = webSocketInstances[socketInstance];

	var str = Pointer_stringify(url);
	socket.buffer = new Uint8Array(0);
	socket.messages = [];
	socket.error = null;
	socket.open = false;
	socket.socket = new WebSocket(str);
	socket.socket.binaryType = 'arraybuffer';

	socket.socket.onopen = function (e) {
		socket.open = true;
	}

	socket.socket.onmessage = function (e) {
		// Todo: handle other data types?
		if (e.data instanceof Blob)
		{
			var reader = new FileReader();
			reader.addEventListener("loadend", function() {
				var array = new Uint8Array(reader.result);
				socket.messages.push(array);
			});
			reader.readAsArrayBuffer(e.data);
		}
		else if (e.data instanceof ArrayBuffer)
		{
			var array = new Uint8Array(e.data);
			socket.messages.push(array);
		}
	};

	socket.socket.onclose = function (e) {
		socket.open = false;
		// 1000 normal close
		if (e.code != 1000)
		{
			if (e.reason != null && e.reason.length > 0)
				socket.error = e.reason;
			else
			{
				switch (e.code)
				{
					case 1001: 
						socket.error = "Endpoint going away.";
						break;
					case 1002: 
						socket.error = "Protocol error.";
						break;
					case 1003: 
						socket.error = "Unsupported message.";
						break;
					case 1005: 
						socket.error = "No status.";
						break;
					case 1006: 
						socket.error = "Abnormal disconnection.";
						break;
					case 1009: 
						socket.error = "Data frame too large.";
						break;
					default:
						socket.error = "Error "+e.code;
				}
			}
		}
	}

},

SocketIsOpen: function (socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	return socket.open;
},

SocketState: function (socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	if (!socket.open)
		return 0;
	return socket.socket.readyState;
},

SocketError: function (socketInstance, ptr, bufsize)
{
 	var socket = webSocketInstances[socketInstance];
 	if (socket.error == null)
 		return 0;
    var str = socket.error.slice(0, Math.max(0, bufsize - 1));
    writeStringToMemory(str, ptr, false);
	return 1;
},

SocketSend: function (socketInstance, ptr, length)
{
	var socket = webSocketInstances[socketInstance];
	if (!socket.open)
		return;
	socket.socket.send (HEAPU8.buffer.slice(ptr, ptr+length));
},

SocketRecvLength: function(socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	if (!socket.open)
		return 0;
	if (socket.messages.length == 0)
		return 0;
	return socket.messages[0].length;
},

SocketRecv: function (socketInstance, ptr, length)
{
	var socket = webSocketInstances[socketInstance];
	if (!socket.open)
		return 0;
	if (socket.messages.length == 0)
		return 0;
	if (socket.messages[0].length > length)
		return 0;
	HEAPU8.set(socket.messages[0], ptr);
	socket.messages = socket.messages.slice(1);
},

SocketClose: function (socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	socket.socket.close();
	socket.socket = null;
	socket.open = false;
	socket.error = null;
	socket.buffer = null;
	socket.messages = null;
}
};

autoAddDeps(LibraryWebSockets, '$webSocketInstances');
mergeInto(LibraryManager.library, LibraryWebSockets);
