/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Net.Sockets;
using AIO.Net;

class HttpTraceServer : HttpServer
{
    public HttpTraceServer(IPAddress address, int port) : base(address, port)
    {
    }

    protected override TcpSession CreateSession()
    {
        return new HttpTraceSession(this);
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Server caught an error with code {error}");
    }
}