/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Xunit;

namespace AIO
{
    public class UdpTests
    {
        [Fact(DisplayName = "UDP server test")]
        public static void UdpServerTest()
        {
            string address = "127.0.0.1";
            int port = 3333;

            // Create and start Echo server
            var server = new EchoUdpServer(IPAddress.Any, port);
            Assert.True(server.Start());
            while (!server.IsStarted)
                Thread.Yield();

            // Stop the Echo server
            Assert.True(server.Stop());
            while (server.IsStarted)
                Thread.Yield();

            // Check the Echo server state
            Assert.True(server.Started);
            Assert.True(server.Stopped);
            Assert.True(server.BytesSent == 4);
            Assert.True(server.BytesReceived == 4);
            Assert.True(!server.Errors);
        }

        [Fact(DisplayName = "UDP server random test")]
        public static void UdpServerRandomTest()
        {
            string address = "127.0.0.1";
            int port = 3334;

            // Create and start Echo server
            var server = new EchoUdpServer(IPAddress.Any, port);
            Assert.True(server.Start());
            while (!server.IsStarted)
                Thread.Yield();

            // Stop the Echo server
            Assert.True(server.Stop());
            while (server.IsStarted)
                Thread.Yield();

            // Check the Echo server state
            Assert.True(server.Started);
            Assert.True(server.Stopped);
            Assert.True(server.BytesSent > 0);
            Assert.True(server.BytesReceived > 0);
            Assert.True(!server.Errors);
        }
    }
}