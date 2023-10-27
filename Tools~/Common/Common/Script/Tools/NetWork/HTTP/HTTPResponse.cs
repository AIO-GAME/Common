/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-10-07
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// NetHttpResponse
    /// /// </summary>
    public class HTTPResponse : INetResponse
    {
        /// <summary>
        /// 协议号
        /// </summary>
        public int Protocol { get; internal set; }

        public string Url { get; internal set; }

        /// <summary>
        /// 响应码
        /// </summary>
        public long ResponseCode { get; internal set; }

        /// <summary>
        /// 响应体
        /// </summary>
        public object Body { get; set; }

        /// <summary>
        /// 请求
        /// </summary>
        public INetRequest Request { get; internal set; }

        /// <inheritdoc />
        public async Task<T> GetAsync<T>(T defaultValue = default)
        {
            if (Body is HttpContent content)
            {
                var data = await content.ReadAsStringAsync();
                return AHelper.Json.Deserialize<T>(data);
            }

            return defaultValue;
        }

        /// <inheritdoc />
        public Task<string> GetStringAsync()
        {
            if (Body is HttpContent content) return content.ReadAsStringAsync();
            return null;
        }

        /// <inheritdoc />
        public Task<Stream> GetStreamAsync()
        {
            if (Body is HttpContent content) return content.ReadAsStreamAsync();
            return null;
        }

        /// <inheritdoc />
        public Task<byte[]> GetBytesAsync()
        {
            if (Body is HttpContent content) return content.ReadAsByteArrayAsync();
            return null;
        }
    }
}