/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UdpClient = AIO.Net.UdpClient;

public class EchoClient : UdpClient
{
    public EchoClient(string address, int port) : base(address, port)
    {
    }

    public void DisconnectAndStop()
    {
        _stop = true;
        Disconnect();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Echo UDP client connected a new session with Id {Id}");

        // Start receive datagrams
        ReceiveAsync();
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Echo UDP client disconnected a session with Id {Id}");

        // Wait for a while...
        Thread.Sleep(1000);

        // Try to connect again
        if (!_stop)
            Connect();
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, int offset, int size)
    {
        Console.WriteLine("Incoming: " + AHelper.Binary.Deserialize<string>(buffer));

        // Continue receive datagrams
        ReceiveAsync();
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Echo UDP client caught an error with code {error}");
    }

    private bool _stop;
}