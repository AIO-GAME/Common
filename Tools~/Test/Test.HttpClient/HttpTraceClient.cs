/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net.Sockets;
using AIO.Net;

public class HttpTraceClient : HttpClient
{
    public HttpTraceClient(string address, int port, int messages) : base(address, port)
    {
        _messages = messages;
    }

    public void SendMessage()
    {
        SendRequestAsync(Request.MakeTraceRequest("/"));
    }

    protected override void OnConnected()
    {
        for (long i = _messages; i > 0; i--)
            SendMessage();
    }

    protected override void OnSent(long sent, long pending)
    {
        _sent += sent;
        base.OnSent(sent, pending);
    }

    protected override void OnReceived(byte[] buffer, int offset, int size)
    {
        _received += size;
        Program.TimestampStop = DateTime.UtcNow;
        Program.TotalBytes += size;
        base.OnReceived(buffer, offset, size);
    }

    protected override void OnReceivedResponse(HttpResponse response)
    {
        if (response.Status == 200)
            Program.TotalMessages++;
        else
            Program.TotalErrors++;
        SendMessage();
    }

    protected override void OnReceivedResponseError(HttpResponse response, string error)
    {
        Console.WriteLine($"Response error: {error}");
        Program.TotalErrors++;
        SendMessage();
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Client caught an error with code {error}");
        Program.TotalErrors++;
    }

    private long _sent = 0;
    private long _received = 0;
    private long _messages = 0;
}