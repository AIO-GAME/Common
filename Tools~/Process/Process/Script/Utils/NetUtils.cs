/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Net;

#endregion

namespace AIO
{
    internal static class NetUtils
    {
        /// <summary>
        /// 判断地址是否有效
        /// </summary>
        internal static bool UrlCheck(string url, int outTime = 1000)
        {
            if (string.IsNullOrEmpty(url)) return false;
            HttpWebRequest request = null;
            if (!url.Contains("http://") && !url.Contains("https://")) url = "http://" + url;

            try
            {
                request         = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                request.Method  = "HEAD";
                request.Timeout = outTime; //超时时间10秒
                var res = (HttpWebResponse)request.GetResponse();
                return res.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
            finally
            {
                request?.Abort();
            }
        }
    }
}