﻿/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace AIO.Net
{
    /// <summary>
    /// HTTP session is used to receive/send HTTP requests/responses from the connected HTTP client.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class HttpSession : TcpSession
    {
        /// <inheritdoc />
        public HttpSession(HttpServer server) : base(server)
        {
            Cache = server.Cache;
            Request = new HttpRequest();
            Response = new HttpResponse();
        }

        /// <summary>
        /// Get the static content cache
        /// </summary>
        public FileCache Cache { get; }

        /// <summary>
        /// Get the HTTP request
        /// </summary>
        protected HttpRequest Request { get; }

        /// <summary>
        /// Get the HTTP response
        /// </summary>
        public HttpResponse Response { get; }

        #region Send response / Send response body

        /// <summary>
        /// Send the current HTTP response (synchronous)
        /// </summary>
        /// <returns>Size of sent data</returns>
        public long SendResponse() => SendResponse(Response);

        /// <summary>
        /// Send the HTTP response (synchronous)
        /// </summary>
        /// <param name="response">HTTP response</param>
        /// <returns>Size of sent data</returns>
        public long SendResponse(HttpResponse response) =>
            this.Send(response.Cache.Arrays, response.Cache.Offset, response.Cache.Count);

        /// <summary>
        /// Send the HTTP response body (synchronous)
        /// </summary>
        /// <param name="body">HTTP response body</param>
        /// <returns>Size of sent data</returns>
        public long SendResponseBody(string body) => this.Send(body);

        /// <summary>
        /// Send the HTTP response body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <returns>Size of sent data</returns>
        public long SendResponseBody(byte[] buffer) => Send(buffer);

        /// <summary>
        /// Send the HTTP response body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <param name="offset">HTTP response body buffer offset</param>
        /// <param name="size">HTTP response body size</param>
        /// <returns>Size of sent data</returns>
        public long SendResponseBody(byte[] buffer, int offset, int size) => this.Send(buffer, offset, size);

        /// <summary>
        /// Send the current HTTP response (asynchronous)
        /// </summary>
        /// <returns>'true' if the current HTTP response was successfully sent, 'false' if the session is not connected</returns>
        public bool SendResponseAsync() => SendResponseAsync(Response);

        /// <summary>
        /// Send the HTTP response (asynchronous)
        /// </summary>
        /// <param name="response">HTTP response</param>
        /// <returns>'true' if the current HTTP response was successfully sent, 'false' if the session is not connected</returns>
        public bool SendResponseAsync(HttpResponse response) =>
            this.SendAsync(response.Cache.Arrays, response.Cache.Offset, response.Cache.Count);

        /// <summary>
        /// Send the HTTP response body (asynchronous)
        /// </summary>
        /// <param name="body">HTTP response body</param>
        /// <returns>'true' if the HTTP response body was successfully sent, 'false' if the session is not connected</returns>
        public bool SendResponseBodyAsync(string body) => this.SendAsync(body);

        /// <summary>
        /// Send the HTTP response body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <returns>'true' if the HTTP response body was successfully sent, 'false' if the session is not connected</returns>
        public bool SendResponseBodyAsync(byte[] buffer) => SendAsync(buffer);

        /// <summary>
        /// Send the HTTP response body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <param name="offset">HTTP response body buffer offset</param>
        /// <param name="size">HTTP response body size</param>
        /// <returns>'true' if the HTTP response body was successfully sent, 'false' if the session is not connected</returns>
        public bool SendResponseBodyAsync(byte[] buffer, int offset, int size) => this.SendAsync(buffer, offset, size);

        #endregion

        #region Session handlers

        /// <inheritdoc />
        protected override void OnReceived(byte[] buffer, int offset, int size)
        {
            // Receive HTTP request header
            if (Request.IsPendingHeader())
            {
                if (Request.ReceiveHeader(buffer, (int)offset, (int)size))
                    OnReceivedRequestHeader(Request);

                size = 0;
            }

            // Check for HTTP request error
            if (Request.IsErrorSet)
            {
                OnReceivedRequestError(Request, "Invalid HTTP request!");
                Request.Clear();
                Disconnect();
                return;
            }

            // Receive HTTP request body
            if (Request.ReceiveBody(buffer, (int)offset, (int)size))
            {
                OnReceivedRequestInternal(Request);
                Request.Clear();
                return;
            }

            // Check for HTTP request error
            if (Request.IsErrorSet)
            {
                OnReceivedRequestError(Request, "Invalid HTTP request!");
                Request.Clear();
                Disconnect();
                return;
            }
        }

        /// <inheritdoc />
        protected override void OnDisconnected()
        {
            // Receive HTTP request body
            if (Request.IsPendingBody())
            {
                OnReceivedRequestInternal(Request);
                Request.Clear();
                return;
            }
        }

        /// <summary>
        /// Handle HTTP request header received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP request header was received from the client.</remarks>
        /// <param name="request">HTTP request</param>
        protected virtual void OnReceivedRequestHeader(HttpRequest request)
        {
        }

        /// <summary>
        /// Handle HTTP request received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP request was received from the client.</remarks>
        /// <param name="request">HTTP request</param>
        protected virtual void OnReceivedRequest(HttpRequest request)
        {
        }

        /// <summary>
        /// Handle HTTP cached request received notification
        /// </summary>
        /// <remarks>
        /// Notification is called when HTTP request was received
        /// from the client and the corresponding cached content
        /// was found.
        ///
        /// Default behavior is just send cached response content
        /// to the client.
        /// </remarks>
        /// <param name="request">HTTP request</param>
        /// <param name="content">Cached response content</param>
        protected virtual void OnReceivedCachedRequest(HttpRequest request, byte[] content)
        {
            SendAsync(content);
        }

        /// <summary>
        /// Handle HTTP request error notification
        /// </summary>
        /// <remarks>Notification is called when HTTP request error was received from the client.</remarks>
        /// <param name="request">HTTP request</param>
        /// <param name="error">HTTP request error</param>
        protected virtual void OnReceivedRequestError(HttpRequest request, string error)
        {
        }

        #endregion

        private void OnReceivedRequestInternal(HttpRequest request)
        {
            // Try to get the cached response
            if (request.Method == "GET")
            {
                var index = request.Url.IndexOf('?');
                var response = Cache.Get(index < 0 ? request.Url : request.Url.Substring(0, index));
                if (response.Item1)
                {
                    // Process the request with the cached response
                    OnReceivedCachedRequest(request, response.Item2);
                    return;
                }
            }

            // Process the request
            OnReceivedRequest(request);
        }
    }
}