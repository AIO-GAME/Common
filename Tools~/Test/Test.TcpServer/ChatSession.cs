/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net.Sockets;
using AIO;
using AIO.Net;

internal class ChatSession : TcpSession
{
    public ChatSession(TcpServer server) :
        base(server)
    {
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} connected!");

        // Send invite message
        const string message = "Hello from TCP chat! Please send a message or '!' to disconnect the client!";
        SendAsync(message);
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} disconnected!");
    }

    protected override void OnReceived(byte[] buffer, int offset, int size)
    {
        var data = new BufferByte(buffer, offset, size);
        var message = data.ReadStringUTF8();
        Console.WriteLine("Incoming: {0}", message);

        // Multicast message to all connected sessions
        Server.Multicast(message);

        // If the buffer starts with '!' the disconnect the current session
        if (message == "!")
            Disconnect();
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP session caught an error with code {error}");
    }
}