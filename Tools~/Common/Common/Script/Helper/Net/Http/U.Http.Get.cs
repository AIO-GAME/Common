#region

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: HTTP

        public partial class Http
        {
            #region Get

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Get(string   remoteUrl,
                                     Encoding encoding = null,
                                     ushort   timeout  = Net.TIMEOUT,
                                     string   cookie   = null) => Get(remoteUrl, Format.Json, encoding, timeout, cookie);

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static Task<string> GetAsync(string   remoteUrl,
                                                Encoding encoding = null,
                                                ushort   timeout  = Net.TIMEOUT,
                                                string   cookie   = null) => GetAsync(remoteUrl, Format.Json, encoding, timeout, cookie);

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="encoding">编码</param>
            /// <param name="format">格式</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static string Get(string   remoteUrl,
                                     Format   format   = Format.Json,
                                     Encoding encoding = null,
                                     ushort   timeout  = Net.TIMEOUT,
                                     string   cookie   = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request        = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Get;
                    var application = format switch
                    {
                        Format.Json => "application/json;",
                        Format.Xml  => "application/xml;",
                        Format.Yaml => "application/x-yaml;",
                        _           => "application/json;",
                    };
                    var charset = encoding?.BodyName ?? "UTF-8";
                    request.ContentType = $"{application} {charset}";
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
            /// <param name="format">格式</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<string> GetAsync(string   remoteUrl,
                                                      Format   format   = Format.Json,
                                                      Encoding encoding = null,
                                                      ushort   timeout  = Net.TIMEOUT,
                                                      string   cookie   = null)
            {
                HttpWebRequest request = null;
                try
                {
                    request        = CreateHttpWebRequest(remoteUrl, timeout, cookie);
                    request.Method = WebRequestMethods.Http.Get;
                    var application = format switch
                    {
                        Format.Json => "application/json;",
                        Format.Xml  => "application/xml;",
                        Format.Yaml => "application/x-yaml;",
                        _           => "application/json;",
                    };
                    var charset = encoding?.BodyName ?? "UTF-8";
                    request.ContentType = $"{application} {charset}";
                    return await GetResponseTextAsync(request, encoding);
                }
                catch (Exception)
                {
                    request?.Abort();
                    return string.Empty;
                }
            }

            #endregion

            #region GetStream

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
            public static async Task<Stream> GetStreamAsync(string remoteUrl,
                                                            ushort timeout = Net.TIMEOUT,
                                                            string cookie  = null)
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

            #endregion

            #region GetMD5

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常</exception>
            public static string GetMD5(string remoteUrl, ushort timeout = Net.TIMEOUT)
            {
                var remote  = remoteUrl.Replace("\\", "/");
                var request = WebRequest.Create(remote);
                request.Method  = WebRequestMethods.Http.Get;
                request.Timeout = timeout;
                using var response = request.GetResponse();
                using var stream   = response.GetResponseStream();
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
                var remote  = remoteUrl.Replace("\\", "/");
                var request = WebRequest.Create(remote);
                request.Method  = WebRequestMethods.Http.Get;
                request.Timeout = timeout;
                using var response = await request.GetResponseAsync();
                using var stream   = response.GetResponseStream();
                if (stream is null) throw new AExpNetGetResponseStream("HTTP", response);
                return await stream.GetMD5Async();
            }

            #endregion

            /// <summary>
            /// 编码格式
            /// </summary>
            public enum Format
            {
                /// <summary>
                /// Json
                /// </summary>
                Json,

                /// <summary>
                /// Xml
                /// </summary>
                Xml,

                /// <summary>
                /// Yaml
                /// </summary>
                Yaml
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="format">格式</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static T Get<T>(string   remoteUrl,
                                   Format   format   = Format.Json,
                                   Encoding encoding = null,
                                   ushort   timeout  = Net.TIMEOUT,
                                   string   cookie   = null)
            {
                var data = Get(remoteUrl, format, encoding, timeout, cookie);
                return format switch
                {
                    Format.Json => Json.Deserialize<T>(data),
                    Format.Xml  => Xml.Deserialize<T>(data),
                    Format.Yaml => Yaml.Deserialize<T>(data),
                    _           => throw new ArgumentOutOfRangeException(nameof(format), format, null)
                };
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteUrl">路径</param>
            /// <param name="format">格式</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            public static async Task<T> GetAsync<T>(string   remoteUrl,
                                                    Format   format   = Format.Json,
                                                    Encoding encoding = null,
                                                    ushort   timeout  = Net.TIMEOUT,
                                                    string   cookie   = null)
            {
                var data = await GetAsync(remoteUrl, format, encoding, timeout, cookie);
                return format switch
                {
                    Format.Json => Json.Deserialize<T>(data),
                    Format.Xml  => Xml.Deserialize<T>(data),
                    Format.Yaml => Yaml.Deserialize<T>(data),
                    _           => throw new ArgumentOutOfRangeException(nameof(format), format, null)
                };
            }

            #region Nested type: Option

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            public sealed class Option
            {
                /// <summary>
                /// 默认:application/x-www-form-urlencoded
                /// </summary>
                public string ContentType;

                /// <summary>
                /// cookie
                /// </summary>
                public string Cookie;

                /// <summary>
                /// 编码:默认UTF-8
                /// </summary>
                public Encoding Encoding;

                /// <summary>
                /// 超时时间
                /// </summary>
                public ushort Timeout;

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

            #endregion
        }

        #endregion
    }
}