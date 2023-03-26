using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace AIO
{
    /// <summary>
    /// 文件下载信息
    /// </summary>
    public sealed class HttpDownloadInfo : IDisposable
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        public string URL { get; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string FullPath => Path.Combine(SavePath, Name);

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// 下载文件大小
        /// </summary>
        public long DownloadedSize { get; private set; }

        /// <summary>
        /// 进度
        /// </summary>
        public int Progress => (int)((float)DownloadedSize / FileSize * 100);

        /// <summary>
        /// 下载异常信息
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 下载总时间
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// 是否为断点续传
        /// </summary>
        public bool IsResume { get; private set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string MD5 { get; private set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        private Stopwatch Stopwatch { get; set; }

        /// <summary>
        /// 请求体
        /// </summary>
        private HttpResponseMessage Response { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        private CancellationTokenSource CancelToken { get; set; }

        /// <summary>
        /// 下载信息
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="savePath">保存文件夹</param>
        /// <param name="name">文件名</param>
        internal HttpDownloadInfo(in string url, in string savePath, in string name)
        {
            URL = url;
            SavePath = savePath;
            Name = name;
        }

        /// <summary>
        /// 取消下载
        /// </summary>
        public void Cancel()
        {
            if (CancelToken == null) return;
            CancelToken.Cancel();
            CancelToken.Dispose();
            CancelToken = null;
        }

        internal HttpDownloadInfo SetCancellationTokenSource(in CancellationTokenSource value)
        {
            CancelToken = value;
            return this;
        }

        internal CancellationToken GetToken()
        {
            if (CancelToken == null) return CancellationToken.None;
            return CancelToken.Token;
        }

        internal HttpDownloadInfo SetResponse(in HttpResponseMessage value)
        {
            Response = value;
            if (Response.Content.Headers.ContentLength != null) FileSize = (long)Response.Content.Headers.ContentLength;
            else FileSize = 0;
            return this;
        }

        internal HttpDownloadInfo SetException(in Exception value)
        {
            Exception = value;
            return this;
        }

        internal HttpDownloadInfo SetResume(in long value)
        {
            IsResume = true;
            DownloadedSize = value;
            return this;
        }

        internal HttpDownloadInfo Update(in long value)
        {
            DownloadedSize += value;
            return this;
        }

        internal HttpDownloadInfo Start()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            return this;
        }

        internal HttpDownloadInfo Finish()
        {
            if (Stopwatch == null) Time = TimeSpan.Zero;
            else
            {
                Time = Stopwatch.Elapsed;
                Stopwatch.Stop();
                Stopwatch = null;
            }

            return this;
        }

        internal HttpDownloadInfo SetMD5(string md5)
        {
            MD5 = md5;
            return this;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            CancelToken?.Dispose();
            Response?.Dispose();
        }
    }
}