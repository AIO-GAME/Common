using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

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
        public string FullPath
        {
            get { return Path.Combine(SavePath, Name); }
        }

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

    /// <summary>
    /// Http下载
    /// </summary>
    public class HttpDownload : IDisposable
    {
        private readonly static byte[] CODE = new byte[] { 1, 3, 9, 3, 1, 3, 9, 3, 1 };

        /// <summary>
        /// 文件地址
        /// </summary>
        public IList<string> Urls { get; }

        /// <summary>
        /// 保存地址
        /// </summary>
        public string SavePath { get; }

        /// <summary>
        /// Http客户端
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// Http客户端
        /// </summary>
        private HttpClient MD5Client { get; set; }

        /// <summary>
        /// 最大下载数量
        /// </summary>
        public int MaxDownloadNum { get; private set; }

        /// <summary>
        /// 进度回调
        /// </summary>
        private Action<HttpDownloadInfo> ProgressAction;

        /// <summary>
        /// 完成回调
        /// </summary>
        private Action<HttpDownloadInfo> CompleteAction;

        /// <summary>
        /// 完成回调
        /// </summary>
        private Action<HttpDownloadInfo> ExceptionAction;

        /// <summary>
        /// Http头信息
        /// </summary>
        private HttpClientHandler ClientHandler;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloadUrls">下载地址</param>
        /// <param name="downloadPath">保存地址</param>
        public HttpDownload(in IList<string> downloadUrls, in string downloadPath)
        {
            Urls = downloadUrls;
            SavePath = downloadPath;
            MaxDownloadNum = Urls.Count;

            ClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None,
                UseProxy = false,
                UseCookies = false,
                AllowAutoRedirect = false,
            };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloadUrls">下载地址</param>
        /// <param name="downloadPath">保存地址</param>
        /// <param name="handler">表头参数</param>
        public HttpDownload(in IList<string> downloadUrls, in string downloadPath, in HttpClientHandler handler)
        {
            Urls = downloadUrls;
            SavePath = downloadPath;
            MaxDownloadNum = Urls.Count;
            ClientHandler = handler;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloadUrls">下载地址</param>
        /// <param name="downloadPath">保存地址</param>
        public HttpDownload(in string downloadUrls, in string downloadPath) : this(new string[] { downloadUrls }, downloadPath)
        {
        }

        /// <summary>
        /// 开启异步下载
        /// </summary>
        /// <param name="timeout">超时时间</param>
        public async Task Async(int timeout = 10)
        {
            if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);

            Client = new HttpClient(ClientHandler);
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            Client.Timeout = TimeSpan.FromSeconds(timeout);

            MD5Client = new HttpClient(ClientHandler);
            MD5Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            MD5Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            MD5Client.Timeout = TimeSpan.FromSeconds(timeout);

            var task = new Task[MaxDownloadNum >= Urls.Count ? Urls.Count : MaxDownloadNum];
            var count = 0;
            while (count < Urls.Count)
            {
                for (var i = 0; i < task.Length && count < Urls.Count; i++)
                {
                    task[i] = DownloadFileAsync(count++);
                }
                await Task.WhenAll(task);
            }
        }

        private async Task DownloadFileAsync(int index)
        {
            var name = Path.GetFileName(Urls[index]);
            var info = new HttpDownloadInfo(Urls[index], SavePath, name);
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, Urls[index]))
                {
                    var fileInfo = new FileInfo(info.FullPath);
                    if (fileInfo.Exists && fileInfo.Length > 0)
                    {
                        info.SetCancellationTokenSource(new CancellationTokenSource());
                        using (var response = await Client.GetAsync(Urls[index], info.GetToken()))
                        {
                            using (var md5 = MD5.Create())
                            {
                                var expectedMd5Bytes = md5.ComputeHash(new MemoryStream(await response.Content.ReadAsByteArrayAsync()));
                                info.SetMD5(BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower());

                                var header = new byte[CODE.Length];
                                using (var stream = File.OpenRead(fileInfo.FullName))
                                {
                                    stream.Read(header, 0, header.Length);
                                    var resume = true;
                                    for (int i = 0; i < CODE.Length; i++)
                                    {
                                        if (CODE[i] != header[i]) { resume = false; break; }
                                    }

                                    if (resume)
                                    {   // 断点续传
                                        info.SetResume(fileInfo.Length);
                                        request.Headers.Range = new RangeHeaderValue(info.DownloadedSize, null);
                                    }
                                    else
                                    {   // 下载完整的文件
                                        stream.Position = 0;
                                        var localmd5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                        if (localmd5 != info.MD5)
                                        {
                                            stream.Close();
                                            stream.Dispose();
                                            fileInfo.Delete();
                                        }
                                        else
                                        {
                                            stream.Close();
                                            stream.Dispose();
                                            return;
                                        }
                                    }
                                    stream.Close();
                                    stream.Dispose();
                                }
                            }
                            info.Cancel();
                        }
                    }
                    info.SetCancellationTokenSource(new CancellationTokenSource());
                    using (var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, info.GetToken()))
                    {
                        info.Start().SetResponse(response);
                        if (string.IsNullOrEmpty(info.MD5))
                        {
                            var expectedMd5Bytes = MD5.Create().ComputeHash(new MemoryStream(await response.Content.ReadAsByteArrayAsync()));
                            info.SetMD5(BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower());
                        }

                        using (var fileStream = new FileStream(info.FullPath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                        {
                            using (var contentStream = await response.Content.ReadAsStreamAsync())
                            {
                                // 如果不是断点续传 则写入锁码 等待下载完成后 删除锁码
                                if (!info.IsResume) await fileStream.WriteAsync(CODE, 0, CODE.Length);

                                var buffer = new byte[81920];
                                while (true)
                                {
                                    var bytes = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                                    if (bytes == 0) break;
                                    await fileStream.WriteAsync(buffer, 0, bytes);
                                    info.Update(bytes);
                                    ProgressAction?.Invoke(info);
                                }
                            }
                            fileStream.Close();
                            fileStream.Dispose();
                        }
                    }
                }

                RemoveFileHeader(info.FullPath);
                info.Finish();
                CompleteAction?.Invoke(info);
            }
            catch (Exception ex)
            {
                info.SetException(ex);
                ExceptionAction?.Invoke(info);
            }
        }

        internal static void RemoveFileHeader(string filePath)
        {
            int headerSize = CODE.Length;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var buffer = new byte[stream.Length - headerSize];
                stream.Position = headerSize;
                stream.Read(buffer, 0, buffer.Length);
                stream.SetLength(buffer.Length);
                stream.Position = 0;
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }

        /// <summary>
        /// 设置最大下载数量
        /// </summary>
        public HttpDownload SetDownloadNum(in int value)
        {
            MaxDownloadNum = value;
            return this;
        }

        /// <summary>
        /// 进度回调
        /// </summary>
        public HttpDownload OnComplete(in Action<HttpDownloadInfo> action)
        {
            CompleteAction = action;
            return this;
        }

        /// <summary>
        /// 进度回调
        /// </summary>
        public HttpDownload OnException(in Action<HttpDownloadInfo> action)
        {
            ExceptionAction = action;
            return this;
        }

        /// <summary>
        /// 进度回调
        /// </summary>
        public HttpDownload OnProgress(in Action<HttpDownloadInfo> action)
        {
            ProgressAction = action;
            return this;
        }

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        public TaskAwaiter GetAwaiter()
        {
            return Async().GetAwaiter();
        }
    }

    /// <summary>
    /// Http 工具类
    /// </summary>
    public static class HttpUtils
    {
    }
}