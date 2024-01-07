/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;

namespace AIO
{
    /// <summary>
    /// nameof(AExpNetGetResponseStream)
    /// </summary>
    public class AExpNetGetResponseStream : WebException
    {
        /// <inheritdoc />
        public AExpNetGetResponseStream(
            in string message, in WebResponse response) :
            base(message, new Exception("GetResponseStream is Null"), WebExceptionStatus.ReceiveFailure, response)
        {
        }
       
        /// <inheritdoc />
        public AExpNetGetResponseStream(
            in string message) :
            base(message, new Exception("GetResponseStream is Null"), WebExceptionStatus.ReceiveFailure, null)
        {
        }
    }
}