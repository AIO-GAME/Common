/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net.Sockets;
using System.Threading;
using AIO;
using AIO.Net;
using TcpClient = AIO.Net.TcpClient;

class HttpTraceSession : HttpSession
{
    public HttpTraceSession(HttpServer server) : base(server)
    {
    }

    protected override void OnReceivedRequest(HttpRequest request)
    {
        // Process HTTP request methods
        if (request.Method == "TRACE")
            SendResponseAsync(Response.MakeTraceResponse(request));
        else
            SendResponseAsync(Response.MakeErrorResponse("Unsupported HTTP method: " + request.Method));
    }

    protected override void OnReceivedRequestError(HttpRequest request, string error)
    {
        Console.WriteLine($"Request error: {error}");
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Session caught an error with code {error}");
    }
}