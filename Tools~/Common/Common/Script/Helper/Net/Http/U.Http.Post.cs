using System;
using System.IO;
using System.Net;
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
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="options">选项参数</param>
            public static string Post(string remoteUrl, string data, Option options = default)
            {
                options ??= new Option();
                return Post(remoteUrl, options?.Encoding.GetBytes(data), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="options">选项参数</param>
            public static string Post(string remoteUrl, Option options = default)
            {
                return Post(remoteUrl, Array.Empty<byte>(), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="options">选项参数</param>
            public static string Post(string remoteUrl, byte[] data, Option options = default)
            {
                options ??= new Option();
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, options.Timeout, options.Cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = options.ContentType;
                    WriteRequestStream(request, data);
                    if (data == null || data.Length == 0) request.ContentLength = 0;
                    return GetResponseText(request, options.Encoding);
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
            /// <param name="options">选项参数</param>
            public static Stream PostStream(string remoteUrl, string data, Option options = default)
            {
                return PostStream(remoteUrl, options.Encoding.GetBytes(data), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="options">选项参数</param>
            public static Stream PostStream(string remoteUrl, Option options = default)
            {
                return PostStream(remoteUrl, Array.Empty<byte>(), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="options">选项参数</param>
            public static Stream PostStream(string remoteUrl, byte[] data, Option options = default)
            {
                options ??= new Option();
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, options.Timeout, options.Cookie);
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
            /// <param name="options">选项参数</param>
            public static Task<string> PostAsync(string remoteUrl, string data, Option options = default)
            {
                return PostAsync(remoteUrl, options.Encoding.GetBytes(data), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="options">选项参数</param>
            public static Task<string> PostAsync(string remoteUrl, Option options = default)
            {
                return PostAsync(remoteUrl, Array.Empty<byte>(), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="options">选项参数</param>
            /// <returns>返回内容</returns>
            public static async Task<string> PostAsync(string remoteUrl, byte[] data, Option options = default)
            {
                options ??= new Option();
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, options.Timeout, options.Cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = options.ContentType;
                    await WriteRequestStreamAsync(request, data);
                    if (data == null || data.Length == 0) request.ContentLength = 0;
                    return await GetResponseTextAsync(request, options.Encoding);
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
            /// <param name="options">选项参数</param>
            public static Task<Stream> PostStreamAsync(string remoteUrl, string data,
                Option options = default)
            {
                options ??= new Option();
                return PostStreamAsync(remoteUrl, options.Encoding.GetBytes(data), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体 
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="options">选项参数</param>
            public static Task<Stream> PostStreamAsync(string remoteUrl, Option options = default)
            {
                return PostStreamAsync(remoteUrl, Array.Empty<byte>(), options);
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="data">上传数据</param>
            /// <param name="options">选项参数</param>
            /// <returns>返回内容</returns>
            public static async Task<Stream> PostStreamAsync(string remoteUrl, byte[] data,
                Option options = default)
            {
                options ??= new Option();
                HttpWebRequest request = null;
                try
                {
                    request = CreateHttpWebRequest(remoteUrl, options.Timeout, options.Cookie);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = options.ContentType;
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