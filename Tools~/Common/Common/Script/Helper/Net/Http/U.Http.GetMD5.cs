using System;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        /// <summary>
        /// HTTP 下载文件
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="timeout">超时</param>
        /// <exception cref="Exception">异常</exception>
        public static string HTTPGetMD5(string remotePath, ushort timeout = TIMEOUT)
        {
            var remote = remotePath.Replace("\\", "/");
            var request = (HttpWebRequest)WebRequest.Create(remote);
            request.Method = "GET";
            request.Timeout = timeout;
            using var md5 = System.Security.Cryptography.MD5.Create();
            using var stream = request.GetResponse().GetResponseStream();
            if (stream is null) throw new Exception("HTTP Stream is Null");
            var expectedMd5Bytes = md5.ComputeHash(stream);
            return BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// HTTP 下载文件
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="timeout">超时</param>
        /// <exception cref="Exception">异常</exception>
        public static async Task<string> HTTPGetMD5Async(string remotePath, ushort timeout = TIMEOUT)
        {
            var remote = remotePath.Replace("\\", "/");
            var request = (HttpWebRequest)WebRequest.Create(remote);
            request.Method = "GET";
            request.Timeout = timeout;
            using var md5 = System.Security.Cryptography.MD5.Create();
            using var response = await request.GetResponseAsync();
            using var stream = response.GetResponseStream();
            if (stream is null) throw new Exception("HTTP Stream is Null");
            var expectedMd5Bytes = md5.ComputeHash(stream);
            return BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower();
        }
    }
}