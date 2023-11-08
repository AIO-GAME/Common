/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Net;
using System.Net.Sockets;
using UdpClient = AIO.Net.UdpClient;

public class EchoUdpClient : UdpClient
{
    public bool Connected { get; set; }
    public bool Disconnected { get; set; }
    public bool Errors { get; set; }

    public EchoUdpClient(string address, int port) : base(address, port)
    {
    }

    protected override void OnConnected()
    {
        Connected = true;
        ReceiveAsync();
    }

    protected override void OnDisconnected()
    {
        Disconnected = true;
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {
        ReceiveAsync();
    }

    protected override void OnError(SocketError error)
    {
        Errors = true;
    }
}