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
            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Get(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Get;
                    request.ContentType = "application/json; charset=UTF-8";
                    return GetResponseText(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return string.Empty;
                }
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<string> GetAsync(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Get;
                    request.ContentType = "application/json; charset=UTF-8";
                    return await GetResponseTextAsync(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return string.Empty;
                }
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Stream GetStream(string remoteUrl, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Get;
                    request.ContentType = "application/json; charset=UTF-8";
                    return GetResponseStream(request);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return null;
                }
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<Stream> GetStreamAsync(string remoteUrl, ushort timeout = TIMEOUT,
                string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Get;
                    request.ContentType = "application/json; charset=UTF-8";
                    return await GetResponseStreamAsync(request);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return null;
                }
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常</exception>
            public static string GetMD5(string remoteUrl, ushort timeout = TIMEOUT)
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = (HttpWebRequest)WebRequest.Create(remote);
                request.Method = WebRequestMethods.Http.Get;
                request.Timeout = timeout;
                using var response = request.GetResponse();
                using var stream = response.GetResponseStream();
                if (stream is null) throw new NetGetResponseStream("HTTP", response);
                using var md5 = System.Security.Cryptography.MD5.Create();
                var expectedMd5Bytes = md5.ComputeHash(stream);
                return BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower();
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常</exception>
            public static async Task<string> GetMD5Async(string remoteUrl, ushort timeout = TIMEOUT)
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = (HttpWebRequest)WebRequest.Create(remote);
                request.Method = WebRequestMethods.Http.Get;
                request.Timeout = timeout;
                using var response = await request.GetResponseAsync();
                using var stream = response.GetResponseStream();
                if (stream is null) throw new NetGetResponseStream("HTTP", response);
                using var md5 = System.Security.Cryptography.MD5.Create();
                var expectedMd5Bytes = md5.ComputeHash(stream);
                return BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower();
            }
        }
    }
}