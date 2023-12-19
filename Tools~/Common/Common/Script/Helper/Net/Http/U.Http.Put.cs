using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    public partial class HTTP
    {
        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteUrl">远端地址</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cookie">cookie</param>
        /// <exception cref="NetGetResponseStream">异常</exception>
        /// <returns>内容</returns>
        public static string Put(string remoteUrl, byte[] data,
            Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
        {
            return AutoCommonRequest(remoteUrl, PUT, data, encoding, timeout, cookie);
        }

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteUrl">远端地址</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cookie">cookie</param>
        /// <exception cref="NetGetResponseStream">异常</exception>
        /// <returns>内容</returns>
        public static string Put(string remoteUrl, string data,
            Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
        {
            return AutoCommonRequest(remoteUrl, PUT, data, encoding, timeout, cookie);
        }

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteUrl">远端地址</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cookie">cookie</param>
        /// <exception cref="NetGetResponseStream">异常</exception>
        /// <returns>内容</returns>
        public static Task<string> PutAsync(string remoteUrl, byte[] data,
            Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
        {
            return AutoCommonRequestAsync(remoteUrl, PUT, data, encoding, timeout, cookie);
        }

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteUrl">远端地址</param>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cookie">cookie</param>
        /// <returns>内容</returns>
        public static Task<string> PutAsync(string remoteUrl, string data,
            Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
        {
            return AutoCommonRequestAsync(remoteUrl, PUT, data, encoding, timeout, cookie);
        }
    }
}