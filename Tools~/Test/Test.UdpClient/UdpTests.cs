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
    public class Test
    {
        [Fact(DisplayName = "UDP server test")]
        public static void UdpClientTest()
        {
            string address = "127.0.0.1";
            int port = 3333;

            // Create and connect Echo client
            var client = new EchoUdpClient(address, port);
            Assert.True(client.Connect());
            while (!client.IsConnected)
                Thread.Yield();

            // Send a message to the Echo server
            client.Send("test");

            // Wait for all data processed...
            while (client.BytesReceived != 4)
                Thread.Yield();

            // Disconnect the Echo client
            Assert.True(client.Disconnect());
            while (client.IsConnected)
                Thread.Yield();

            // Check the Echo client state
            Assert.True(client.Connected);
            Assert.True(client.Disconnected);
            Assert.True(client.BytesSent == 4);
            Assert.True(client.BytesReceived == 4);
            Assert.True(!client.Errors);
        }
        
        
        [Fact(DisplayName = "UDP server random test")]
        public static void UdpServerRandomTest()
        {
            string address = "127.0.0.1";
            int port = 3334;

            // Test duration in seconds
            int duration = 10;

            // Clients collection
            var clients = new List<EchoUdpClient>();

            // Start random test
            var rand = new Random();
            var start = DateTime.UtcNow;
            while ((DateTime.UtcNow - start).TotalSeconds < duration)
            {
                // Create a new client and connect
                if ((rand.Next() % 100) == 0)
                {
                    if (clients.Count < 100)
                    {
                        // Create and connect Echo client
                        var client = new EchoUdpClient(address, port);
                        clients.Add(client);
                        client.Connect();
                        while (!client.IsConnected)
                            Thread.Yield();
                    }
                }
                // Connect/Disconnect the random client
                else if ((rand.Next() % 100) == 0)
                {
                    if (clients.Count > 0)
                    {
                        int index = rand.Next() % clients.Count;
                        var client = clients[index];
                        if (client.IsConnected)
                        {
                            client.Disconnect();
                            while (client.IsConnected)
                                Thread.Yield();
                        }
                        else
                        {
                            client.Connect();
                            while (!client.IsConnected)
                                Thread.Yield();
                        }
                    }
                }
                // Reconnect the random client
                else if ((rand.Next() % 100) == 0)
                {
                    if (clients.Count > 0)
                    {
                        int index = rand.Next() % clients.Count;
                        var client = clients[index];
                        if (client.IsConnected)
                        {
                            client.Reconnect();
                            while (!client.IsConnected)
                                Thread.Yield();
                        }
                    }
                }
                // Send a message from the random client
                else if ((rand.Next() % 1) == 0)
                {
                    if (clients.Count > 0)
                    {
                        int index = rand.Next() % clients.Count;
                        var client = clients[index];
                        if (client.IsConnected)
                            client.Send("test");
                    }
                }

                // Sleep for a while...
                Thread.Sleep(1);
            }

            // Disconnect clients
            foreach (var client in clients)
            {
                client.Disconnect();
                while (client.IsConnected)
                    Thread.Yield();
            }
        }
    }
}