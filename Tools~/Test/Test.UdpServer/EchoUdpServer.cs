/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Net;
using System.Net.Sockets;
using System.Threading;
using AIO.Net;

public class EchoUdpServer : UdpServer
{
    public bool Started { get; set; }
    public bool Stopped { get; set; }
    public bool Errors { get; set; }

    public EchoUdpServer(IPAddress address, int port) : base(address, port)
    {
    }

    protected override void OnStarted()
    {
        Started = true;
        ReceiveAsync();
    }

    protected override void OnStopped()
    {
        Stopped = true;
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {
        SendAsync(endpoint, buffer, offset, size);
    }

    protected override void OnSent(EndPoint endpoint, long sent)
    {
        ThreadPool.QueueUserWorkItem(o => { ReceiveAsync(); });
    }

    protected override void OnError(SocketError error)
    {
        Errors = true;
    }
}