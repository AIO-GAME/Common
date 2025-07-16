using System;
using System.Net;

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: HTTP

        /// <summary>
        /// HTTP
        /// </summary>
        public partial class Http
        {
            private const string DELETE  = nameof(DELETE);
            private const string GET     = nameof(GET);
            private const string HEAD    = nameof(HEAD);
            private const string POST    = nameof(POST);
            private const string OPTIONS = nameof(OPTIONS);
            private const string PUT     = nameof(PUT);
            private const string TRACE   = nameof(TRACE);
            private const string CONNECT = nameof(CONNECT);

            private Http() { }

            /// <summary>
            /// 检测URL是否可用
            /// </summary>
            /// <param name="uri"> 链接 </param>
            /// <returns> True: 可用, False: 不可用 </returns>
            public static bool Check(string uri)
            {
                if (string.IsNullOrEmpty(uri)) return false;
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Method = "GET";
                    request.GetResponse();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        #endregion
    }
}