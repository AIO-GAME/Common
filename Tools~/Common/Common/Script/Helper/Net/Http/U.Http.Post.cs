using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class HTTP
        {
            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Post(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                encoding ??= Encoding.UTF8;
                return Post(remoteUrl, encoding.GetBytes(data), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Post(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return Post(remoteUrl, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Post(string remoteUrl, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = "application/x-www-form-urlencoded";
                    WriteRequestStream(request, data);
                    if (data == null || data.Length == 0) request.ContentLength = 0;
                    return GetResponseText(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return null;
                }
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="cookie">cookie</param>
            public static Stream PostStream(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return PostStream(remoteUrl, (encoding ?? Encoding.UTF8).GetBytes(data), timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Stream PostStream(string remoteUrl, ushort timeout = TIMEOUT, string cookie = null)
            {
                return PostStream(remoteUrl, Array.Empty<byte>(), timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Stream PostStream(string remoteUrl,
                byte[] data, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = "application/x-www-form-urlencoded";
                    WriteRequestStream(request, data);
                    if (data == null || data.Length == 0) request.ContentLength = 0;
                    return GetResponseStream(request);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return null;
                }
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Task<string> PostAsync(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                encoding ??= Encoding.UTF8;
                return PostAsync(remoteUrl, encoding.GetBytes(data), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Task<string> PostAsync(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return PostAsync(remoteUrl, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <param name="data">上传数据</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <returns>返回内容</returns>
            public static async Task<string> PostAsync(string remoteUrl, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = "application/x-www-form-urlencoded";
                    await WriteRequestStreamAsync(request, data);
                    if (data == null || data.Length == 0) request.ContentLength = 0;
                    return await GetResponseTextAsync(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return null;
                }
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="encoding">编码:默认UTF-8</param>
            /// <param name="cookie">cookie</param>
            public static Task<Stream> PostStreamAsync(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return PostStreamAsync(remoteUrl, (encoding ?? Encoding.UTF8).GetBytes(data), timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Task<Stream> PostStreamAsync(string remoteUrl, ushort timeout = TIMEOUT, string cookie = null)
            {
                return PostStreamAsync(remoteUrl, Array.Empty<byte>(), timeout, cookie);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <param name="data">上传数据</param>
            /// <returns>返回内容</returns>
            public static async Task<Stream> PostStreamAsync(string remoteUrl, byte[] data,
                ushort timeout = TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = "application/x-www-form-urlencoded";
                    await WriteRequestStreamAsync(request, data);
                    if (data == null || data.Length == 0) request.ContentLength = 0;
                    return await GetResponseStreamAsync(request);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return null;
                }
            }
        }
    }
}