/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-08
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Net;
using System.Net.Sockets;
using AIO.Net;

namespace AIO
{
    public class MulticastUdpServer : UdpServer
    {
        public bool Started { get; set; }
        public bool Stopped { get; set; }
        public bool Errors { get; set; }

        public MulticastUdpServer(IPAddress address, int port) : base(address, port)
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

        protected override void OnError(SocketError error)
        {
            Errors = true;
        }
    }
}