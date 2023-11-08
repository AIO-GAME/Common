/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;
using System.Threading;
using Xunit;

namespace AIO
{
    public class UdpMulticastTests
    {
        [Fact(DisplayName = "UDP server multicast test")]
        public void UdpMulticastServerTest()
        {
            string listenAddress = "0.0.0.0";
            string multicastAddress = "239.255.0.1";
            int multicastPort = 3335;

            // Create and start multicast server
            var server = new MulticastUdpServer(IPAddress.Any, 0);
            Assert.True(server.Start(multicastAddress, multicastPort));
            while (!server.IsStarted)
                Thread.Yield();


            // Multicast some data to all clients
            server.Multicast("test");

            Thread.Sleep(100);

            // Multicast some data to all clients
            server.Multicast("test");

            Thread.Sleep(100);

            // Multicast some data to all clients
            server.Multicast("test");

            Thread.Sleep(100);

            // Multicast some data to all clients
            server.Multicast("test");

            Thread.Sleep(100);

            // Multicast some data to all clients
            server.Multicast("test");

            Thread.Sleep(100);

            // Stop the Echo server
            Assert.True(server.Stop());
            while (server.IsStarted)
                Thread.Yield();

            // Check the multicast server state
            Assert.True(server.Started);
            Assert.True(server.Stopped);
            Assert.True(server.BytesSent > 0);
            Assert.True(server.BytesReceived == 0);
            Assert.True(!server.Errors);
        }

        [Fact(DisplayName = "UDP server multicast random test")]
        public void UdpMulticastServerRandomTest()
        {
            string listenAddress = "0.0.0.0";
            string multicastAddress = "239.255.0.1";
            int multicastPort = 3336;

            // Create and start multicast server
            var server = new MulticastUdpServer(IPAddress.Any, 0);
            Assert.True(server.Start(multicastAddress, multicastPort));
            while (!server.IsStarted)
                Thread.Yield();

            // Test duration in seconds
            int duration = 10;

            // Start random test
            var rand = new Random();
            var start = DateTime.UtcNow;
            while ((DateTime.UtcNow - start).TotalSeconds < duration)
            {
                // Create a new client and connect
                if ((rand.Next() % 100) == 0)
                {
                }
                // Connect/Disconnect the random client
                else if ((rand.Next() % 100) == 0)
                {
                }
                // Multicast a message to all clients
                else if ((rand.Next() % 10) == 0)
                {
                    server.Multicast("test");
                }

                // Sleep for a while...
                Thread.Sleep(1);
            }

            // Stop the multicast server
            Assert.True(server.Stop());
            while (server.IsStarted)
                Thread.Yield();

            // Check the multicast server state
            Assert.True(server.Started);
            Assert.True(server.Stopped);
            Assert.True(server.BytesSent > 0);
            Assert.True(server.BytesReceived == 0);
            Assert.True(!server.Errors);
        }
    }
}