/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;
using AIO.Net;

public class EchoServer : UdpServer
{
    public EchoServer(IPAddress address, int port) : base(address, port)
    {
    }

    protected override void OnStarted()
    {
        // Start receive datagrams
        ReceiveAsync();
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, int offset, int size)
    {
        var data = new byte[size];
        Array.Copy(buffer, offset, data, 0, size);
        Console.WriteLine("Incoming: {0}", AHelper.Binary.Deserialize<string>(data));

        // Echo the message back to the sender
        this.Endpoint = endpoint;
        this.SendAsync(buffer, 0, size);
    }

    protected override void OnSent(EndPoint endpoint, int sent)
    {
        // Continue receive datagrams
        ReceiveAsync();
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Echo UDP server caught an error with code {error}");
    }
}