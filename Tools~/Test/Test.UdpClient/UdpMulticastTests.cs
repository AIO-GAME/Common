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
    public class UdpMulticastTests
    {
        [Fact(DisplayName = "UDP server multicast test")]
        public void UdpMulticastServerTest()
        {
            string listenAddress = "0.0.0.0";
            string multicastAddress = "239.255.0.1";
            int multicastPort = 3335;

            // Create and connect multicast client
            var client1 = new MulticastUdpClient(listenAddress, multicastPort);
            client1.SetupMulticast(true);
            Assert.True(client1.Connect());
            while (!client1.IsConnected)
                Thread.Yield();

            // Join multicast group
            client1.JoinMulticastGroup(multicastAddress);
            Thread.Sleep(100);

            // Wait for all data processed...
            while (client1.BytesReceived != 4)
                Thread.Yield();

            // Create and connect multicast client
            var client2 = new MulticastUdpClient(listenAddress, multicastPort);
            client2.SetupMulticast(true);
            Assert.True(client2.Connect());
            while (!client2.IsConnected)
                Thread.Yield();

            // Join multicast group
            client2.JoinMulticastGroup(multicastAddress);
            Thread.Sleep(100);

            // Wait for all data processed...
            while ((client1.BytesReceived != 8) || (client2.BytesReceived != 4))
                Thread.Yield();

            // Create and connect multicast client
            var client3 = new MulticastUdpClient(listenAddress, multicastPort);
            client3.SetupMulticast(true);
            Assert.True(client3.Connect());
            while (!client3.IsConnected)
                Thread.Yield();

            // Join multicast group
            client3.JoinMulticastGroup(multicastAddress);
            Thread.Sleep(100);

            // Wait for all data processed...
            while ((client1.BytesReceived != 12) || (client2.BytesReceived != 8) || (client3.BytesReceived != 4))
                Thread.Yield();

            // Leave multicast group
            client1.LeaveMulticastGroup(multicastAddress);
            Thread.Sleep(100);

            // Disconnect the multicast client
            Assert.True(client1.Disconnect());
            while (client1.IsConnected)
                Thread.Yield();

            // Wait for all data processed...
            while ((client1.BytesReceived != 12) || (client2.BytesReceived != 12) || (client3.BytesReceived != 8))
                Thread.Yield();

            // Leave multicast group
            client2.LeaveMulticastGroup(multicastAddress);
            Thread.Sleep(100);

            // Disconnect the multicast client
            Assert.True(client2.Disconnect());
            while (client2.IsConnected)
                Thread.Yield();

            // Wait for all data processed...
            while ((client1.BytesReceived != 12) || (client2.BytesReceived != 12) || (client3.BytesReceived != 12))
                Thread.Yield();

            // Leave multicast group
            client3.LeaveMulticastGroup(multicastAddress);
            Thread.Sleep(100);

            // Disconnect the multicast client
            Assert.True(client3.Disconnect());
            while (client3.IsConnected)
                Thread.Yield();

            // Check the multicast client state
            Assert.True(client1.BytesSent == 0);
            Assert.True(client2.BytesSent == 0);
            Assert.True(client3.BytesSent == 0);
            Assert.True(client1.BytesReceived == 12);
            Assert.True(client2.BytesReceived == 12);
            Assert.True(client3.BytesReceived == 12);
            Assert.True(!client1.Errors);
            Assert.True(!client2.Errors);
            Assert.True(!client3.Errors);
        }

        [Fact(DisplayName = "UDP server multicast random test")]
        public void UdpMulticastServerRandomTest()
        {
            string listenAddress = "0.0.0.0";
            string multicastAddress = "239.255.0.1";
            int multicastPort = 3336;

            // Test duration in seconds
            int duration = 10;

            // Clients collection
            var clients = new List<MulticastUdpClient>();

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
                        var client = new MulticastUdpClient(listenAddress, multicastPort);
                        clients.Add(client);
                        client.SetupMulticast(true);
                        client.Connect();
                        while (!client.IsConnected)
                            Thread.Yield();

                        // Join multicast group
                        client.JoinMulticastGroup(multicastAddress);
                        Thread.Sleep(100);
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
                            // Leave multicast group
                            client.LeaveMulticastGroup(multicastAddress);
                            Thread.Sleep(100);

                            client.Disconnect();
                            while (client.IsConnected)
                                Thread.Yield();
                        }
                        else
                        {
                            client.Connect();
                            while (!client.IsConnected)
                                Thread.Yield();

                            // Join multicast group
                            client.JoinMulticastGroup(multicastAddress);
                            Thread.Sleep(100);
                        }
                    }
                }
                // Multicast a message to all clients
                else if ((rand.Next() % 10) == 0)
                {
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