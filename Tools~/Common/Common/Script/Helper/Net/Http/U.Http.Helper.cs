/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    public partial class Net
    {
        public partial class HTTP
        {
            private static Task<string> AutoCommonRequestAsync(string remoteUrl, string method, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                encoding ??= Encoding.UTF8;
                return AutoCommonRequestAsync(remoteUrl, method, encoding.GetBytes(data), encoding, timeout, cookie);
            }

            private static Task<string> AutoCommonRequestAsync(string remoteUrl, string method,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, method, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            private static async Task<string> AutoCommonRequestAsync(string remoteUrl, string method,
                byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = method;
                    await WriteRequestStreamAsync(request, data);
                    if (data != null) request.ContentType = "application/json";
                    return await GetResponseTextAsync(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return string.Empty;
                }
            }

            private static string AutoCommonRequest(string remoteUrl, string method, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                encoding ??= Encoding.UTF8;
                return AutoCommonRequest(remoteUrl, method, encoding.GetBytes(data), encoding, timeout, cookie);
            }

            private static string AutoCommonRequest(string remoteUrl, string method,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, method, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            private static string AutoCommonRequest(string remoteUrl, string method, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = method;
                    WriteRequestStream(request, data);
                    if (data != null) request.ContentType = "application/json";
                    return GetResponseText(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return string.Empty;
                }
            }

            private static string GetResponseText(WebRequest request, Encoding encoding)
            {
                Stream responseStream = null;
                StreamReader stream = null;
                WebResponse response = null;
                try
                {
                    response = request.GetResponse();
                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new NetGetResponseStream("HTTP", response);
                    stream = new StreamReader(responseStream, encoding ?? Encoding.UTF8);
                    var retString = stream.ReadToEnd();

                    stream.Close();
                    responseStream.Close();
                    response.Close();

                    return retString;
                }
                catch (Exception)
                {
                    stream?.Close();
                    responseStream?.Close();
                    response?.Close();
                    return string.Empty;
                }
            }

            private static async Task<string> GetResponseTextAsync(WebRequest request, Encoding encoding)
            {
                Stream responseStream = null;
                StreamReader stream = null;
                WebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync();
                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new NetGetResponseStream("HTTP", response);
                    stream = new StreamReader(responseStream, encoding ?? Encoding.UTF8);
                    var retString = await stream.ReadToEndAsync();

                    stream.Close();
                    responseStream.Close();
                    response.Close();

                    return retString;
                }
                catch (Exception)
                {
                    stream?.Close();
                    responseStream?.Close();
                    response?.Close();
                    return string.Empty;
                }
            }

            private static Stream GetResponseStream(WebRequest request)
            {
                Stream responseStream = null;
                BufferedStream stream = null;
                WebResponse response = null;
                try
                {
                    response = request.GetResponse();
                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new NetGetResponseStream("HTTP", response);
                    stream = new BufferedStream(responseStream);
                    responseStream.Close();
                    response.Close();
                    return stream;
                }
                catch (Exception)
                {
                    stream?.Close();
                    responseStream?.Close();
                    response?.Close();
                    return null;
                }
            }

            private static async Task<Stream> GetResponseStreamAsync(WebRequest request)
            {
                Stream responseStream = null;
                BufferedStream stream = null;
                WebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync();
                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new NetGetResponseStream("HTTP", response);
                    stream = new BufferedStream(responseStream);
                    responseStream.Close();
                    response.Close();
                    return stream;
                }
                catch (Exception)
                {
                    stream?.Close();
                    responseStream?.Close();
                    response?.Close();
                    return null;
                }
            }

            private static void WriteRequestStream(WebRequest request, byte[] data)
            {
                if (data is null || data.Length == 0) return;

                Stream requestStream = null;
                try
                {
                    request.ContentLength = data.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(data, 0, data.Length);
                    requestStream.Close();
                }
                catch (Exception)
                {
                    requestStream?.Close();
                }
            }


            private static async Task WriteRequestStreamAsync(WebRequest request, byte[] data)
            {
                if (data is null || data.Length == 0)
                {
                    request.ContentLength = 0;
                    return;
                }

                Stream requestStream = null;
                try
                {
                    request.ContentLength = data.Length;
                    requestStream = await request.GetRequestStreamAsync();
                    await requestStream.WriteAsync(data, 0, data.Length);
                    requestStream.Close();
                }
                catch (Exception)
                {
                    requestStream?.Close();
                }
            }

            private static HttpWebRequest CreateHttpWebRequest(string remoteUrl,
                ushort timeout = TIMEOUT, string cookie = null)
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = (HttpWebRequest)WebRequest.Create(remote);

                request.Timeout = timeout;
                request.AllowAutoRedirect = true;
                request.MaximumAutomaticRedirections = 1;
                request.AutomaticDecompression = DecompressionMethods.GZip;

                if (string.IsNullOrEmpty(cookie)) return request;

                request.CookieContainer = new CookieContainer();
                request.CookieContainer.SetCookies(new Uri(remote), cookie);
                return request;
            }
        }
    }
}