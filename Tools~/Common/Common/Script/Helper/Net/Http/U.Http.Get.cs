using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
    {
        public partial class HTTP
        {
            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            public sealed class Option
            {
                /// <summary>
                /// 超时时间
                /// </summary>
                public ushort Timeout;

                /// <summary>
                /// cookie
                /// </summary>
                public string Cookie;

                /// <summary>
                /// 编码:默认UTF-8
                /// </summary>
                public Encoding Encoding;

                /// <summary>
                /// 默认:application/x-www-form-urlencoded
                /// </summary>
                public string ContentType;

                /// <summary>
                /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
                /// </summary>
                public Option()
                {
                    Timeout     = Net.TIMEOUT;
                    Cookie      = string.Empty;
                    Encoding    = Encoding.UTF8;
                    ContentType = "application/json";
                }

                /// <summary>
                /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
                /// </summary>
                /// <param name="encoding">编码:默认UTF-8</param>
                /// <param name="timeout">超时时间</param>
                /// <param name="cookie">cookie</param>
                /// <param name="contentType"></param>
                public Option(
                    ushort   timeout,
                    string   cookie,
                    Encoding encoding,
                    string   contentType)
                {
                    Timeout     = timeout;
                    Cookie      = cookie;
                    Encoding    = encoding ?? Encoding.UTF8;
                    ContentType = contentType;
                }
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Get(string   remoteUrl,
                                     Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request             = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method      = WebRequestMethods.Http.Get;
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
            public static async Task<string> GetAsync(string   remoteUrl,
                                                      Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request             = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method      = WebRequestMethods.Http.Get;
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
            public static Stream GetStream(string remoteUrl, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request             = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method      = WebRequestMethods.Http.Get;
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
            public static async Task<Stream> GetStreamAsync(string remoteUrl, ushort timeout = Net.TIMEOUT,
                                                            string cookie = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request             = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method      = WebRequestMethods.Http.Get;
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
            public static string GetMD5(string remoteUrl, ushort timeout = Net.TIMEOUT)
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = WebRequest.Create(remote);
                request.Method  = WebRequestMethods.Http.Get;
                request.Timeout = timeout;
                using var response = request.GetResponse();
                using var stream = response.GetResponseStream();
                if (stream is null) throw new AExpNetGetResponseStream("HTTP", response);
                return stream.GetMD5();
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常</exception>
            public static async Task<string> GetMD5Async(string remoteUrl, ushort timeout = Net.TIMEOUT)
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = WebRequest.Create(remote);
                request.Method  = WebRequestMethods.Http.Get;
                request.Timeout = timeout;
                using var response = await request.GetResponseAsync();
                using var stream = response.GetResponseStream();
                if (stream is null) throw new AExpNetGetResponseStream("HTTP", response);
                return await stream.GetMD5Async();
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static T GetJson<T>(string   remoteUrl,
                                       Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                var data = Get(remoteUrl, encoding, timeout, cookie);
                return Json.Deserialize<T>(data);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<T> GetJsonAsync<T>(string   remoteUrl,
                                                        Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                var data = await GetAsync(remoteUrl, encoding, timeout, cookie);
                return Json.Deserialize<T>(data);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static T GetXml<T>(string   remoteUrl,
                                      Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                var data = Get(remoteUrl, encoding, timeout, cookie);
                return Xml.Deserialize<T>(data);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<T> GetXmlAsync<T>(string   remoteUrl,
                                                       Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                var data = await GetAsync(remoteUrl, encoding, timeout, cookie);
                return Xml.Deserialize<T>(data);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static T GetYaml<T>(string   remoteUrl,
                                       Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                var data = Get(remoteUrl, encoding, timeout, cookie);
                return Yaml.Deserialize<T>(data);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<T> GetYamlAsync<T>(string   remoteUrl,
                                                        Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                var data = await GetAsync(remoteUrl, encoding, timeout, cookie);
                return Yaml.Deserialize<T>(data);
            }
        }
    }
}