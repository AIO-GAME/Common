/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Net;

namespace AIO.Net
{
    /// <summary>
    /// HTTP client is used to communicate with HTTP Web server. It allows to send GET, POST, PUT, DELETE requests and receive HTTP result.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class HttpClient : TcpClient
    {
        /// <summary>
        /// Initialize HTTP client with a given IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public HttpClient(IPAddress address, int port) : base(address, port)
        {
            Request = new HttpRequest();
            Response = new HttpResponse();
        }

        /// <summary>
        /// Initialize HTTP client with a given IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public HttpClient(string address, int port) : base(address, port)
        {
            Request = new HttpRequest();
            Response = new HttpResponse();
        }

        /// <summary>
        /// Initialize HTTP client with a given DNS endpoint
        /// </summary>
        /// <param name="endpoint">DNS endpoint</param>
        public HttpClient(DnsEndPoint endpoint) : base(endpoint)
        {
            Request = new HttpRequest();
            Response = new HttpResponse();
        }

        /// <summary>
        /// Initialize HTTP client with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public HttpClient(IPEndPoint endpoint) : base(endpoint)
        {
            Request = new HttpRequest();
            Response = new HttpResponse();
        }

        /// <summary>
        /// Get the HTTP request
        /// </summary>
        public HttpRequest Request { get; protected set; }

        /// <summary>
        /// Get the HTTP response
        /// </summary>
        protected HttpResponse Response { get; set; }

        #region Send request / Send request body

        /// <summary>
        /// Send the current HTTP request (synchronous)
        /// </summary>
        /// <returns>Size of sent data</returns>
        public long SendRequest() => SendRequest(Request);

        /// <summary>
        /// Send the HTTP request (synchronous)
        /// </summary>
        /// <param name="request">HTTP request</param>
        /// <returns>Size of sent data</returns>
        public long SendRequest(HttpRequest request) =>
            Send(request.Cache.Arrays, request.Cache.Offset, request.Cache.Count);

        /// <summary>
        /// Send the HTTP request body (synchronous)
        /// </summary>
        /// <param name="body">HTTP request body</param>
        /// <returns>Size of sent data</returns>
        public long SendRequestBody(string body) => Send(body);

        /// <summary>
        /// Send the HTTP request body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <returns>Size of sent data</returns>
        public long SendRequestBody(byte[] buffer) => Send(buffer);

        /// <summary>
        /// Send the HTTP request body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <param name="offset">HTTP request body buffer offset</param>
        /// <param name="size">HTTP request body size</param>
        /// <returns>Size of sent data</returns>
        public long SendRequestBody(byte[] buffer, long offset, long size) => Send(buffer, (int)offset, (int)size);

        /// <summary>
        /// Send the current HTTP request (asynchronous)
        /// </summary>
        /// <returns>'true' if the current HTTP request was successfully sent, 'false' if the session is not connected</returns>
        public bool SendRequestAsync() => SendRequestAsync(Request);

        /// <summary>
        /// Send the HTTP request (asynchronous)
        /// </summary>
        /// <param name="request">HTTP request</param>
        /// <returns>'true' if the current HTTP request was successfully sent, 'false' if the session is not connected</returns>
        public bool SendRequestAsync(HttpRequest request) =>
            SendAsync(request.Cache.Arrays, request.Cache.Offset, request.Cache.Count);

        /// <summary>
        /// Send the HTTP request body (asynchronous)
        /// </summary>
        /// <param name="body">HTTP request body</param>
        /// <returns>'true' if the HTTP request body was successfully sent, 'false' if the session is not connected</returns>
        public bool SendRequestBodyAsync(string body) => SendAsync(body);

        /// <summary>
        /// Send the HTTP request body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <returns>'true' if the HTTP request body was successfully sent, 'false' if the session is not connected</returns>
        public bool SendRequestBodyAsync(byte[] buffer) => SendAsync(buffer);

        /// <summary>
        /// Send the HTTP request body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <param name="offset">HTTP request body buffer offset</param>
        /// <param name="size">HTTP request body size</param>
        /// <returns>'true' if the HTTP request body was successfully sent, 'false' if the session is not connected</returns>
        public bool SendRequestBodyAsync(byte[] buffer, int offset, int size) => SendAsync(buffer, offset, size);

        #endregion

        #region Session handlers

        /// <inheritdoc />
        protected override void OnReceived(byte[] buffer, int offset, int size)
        {
            // Receive HTTP response header
            if (Response.IsPendingHeader())
            {
                if (Response.ReceiveHeader(buffer, offset, size))
                    OnReceivedResponseHeader(Response);

                size = 0;
            }

            // Check for HTTP response error
            if (Response.IsErrorSet)
            {
                OnReceivedResponseError(Response, "Invalid HTTP response!");
                Response.Clear();
                Disconnect();
                return;
            }

            // Receive HTTP response body
            if (Response.ReceiveBody(buffer, offset, size))
            {
                OnReceivedResponse(Response);
                Response.Clear();
                return;
            }

            // Check for HTTP response error
            if (Response.IsErrorSet)
            {
                OnReceivedResponseError(Response, "Invalid HTTP response!");
                Response.Clear();
                Disconnect();
            }
        }

        /// <inheritdoc />
        protected override void OnDisconnected()
        {
            // Receive HTTP response body
            if (Response.IsPendingBody())
            {
                OnReceivedResponse(Response);
                Response.Clear();
            }
        }

        /// <summary>
        /// Handle HTTP response header received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP response header was received from the server.</remarks>
        /// <param name="response">HTTP request</param>
        protected virtual void OnReceivedResponseHeader(HttpResponse response)
        {
        }

        /// <summary>
        /// Handle HTTP response received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP response was received from the server.</remarks>
        /// <param name="response">HTTP response</param>
        protected virtual void OnReceivedResponse(HttpResponse response)
        {
        }

        /// <summary>
        /// Handle HTTP response error notification
        /// </summary>
        /// <remarks>Notification is called when HTTP response error was received from the server.</remarks>
        /// <param name="response">HTTP response</param>
        /// <param name="error">HTTP response error</param>
        protected virtual void OnReceivedResponseError(HttpResponse response, string error)
        {
        }

        #endregion
    }
}