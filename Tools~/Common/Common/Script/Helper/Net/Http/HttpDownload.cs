using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using HTTPClient11 = System.Net.Http.HttpClient;

namespace AIO
{
    /// <summary>
    /// Http下载
    /// </summary>
    public class HttpDownload : IDisposable
    {
        private static readonly byte[] CODE = new byte[] { 1, 3, 9, 3, 1, 3, 9, 3, 1 };

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
        public HTTPClient11 Client { get; private set; }

        /// <summary>
        /// Http客户端
        /// </summary>
        private HTTPClient11 MD5Client { get; set; }

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
        private readonly HttpClientHandler ClientHandler;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloadUrls">下载地址</param>
        /// <param name="downloadPath">保存地址</param>
        public HttpDownload(in IList<string> downloadUrls, in string downloadPath)
        {
            Urls           = downloadUrls;
            SavePath       = downloadPath;
            MaxDownloadNum = Urls.Count;

            ClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression =
                    DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None,
                UseProxy          = false,
                UseCookies        = false,
                AllowAutoRedirect = false
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
            Urls           = downloadUrls;
            SavePath       = downloadPath;
            MaxDownloadNum = Urls.Count;
            ClientHandler  = handler;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloadUrls">下载地址</param>
        /// <param name="downloadPath">保存地址</param>
        public HttpDownload(in string downloadUrls, in string downloadPath)
            : this(new[] { downloadUrls }, downloadPath) { }

        /// <summary>
        /// 开启异步下载
        /// </summary>
        /// <param name="timeout">超时时间</param>
        public async Task Async(int timeout = 10)
        {
            if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);

            Client = new HTTPClient11(ClientHandler);
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            Client.Timeout = TimeSpan.FromSeconds(timeout);

            MD5Client = new HTTPClient11(ClientHandler);
            MD5Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            MD5Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            MD5Client.Timeout = TimeSpan.FromSeconds(timeout);

            var task = new Task[MaxDownloadNum >= Urls.Count ? Urls.Count : MaxDownloadNum];
            var count = 0;
            while (count < Urls.Count)
            {
                for (var i = 0; i < task.Length && count < Urls.Count; i++) task[i] = DownloadFileAsync(count++);

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
                                var expectedMd5Bytes =
                                    md5.ComputeHash(new MemoryStream(await response.Content.ReadAsByteArrayAsync()));
                                info.SetMD5(BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower());

                                var header = new byte[CODE.Length];
                                using (var stream = File.OpenRead(fileInfo.FullName))
                                {
                                    _ = stream.Read(header, 0, header.Length);
                                    var resume = !CODE.Where((t, i) => t != header[i]).Any();

                                    if (resume)
                                    {
                                        // 断点续传
                                        info.SetResume(fileInfo.Length);
                                        request.Headers.Range = new RangeHeaderValue(info.DownloadedSize, null);
                                    }
                                    else
                                    {
                                        // 下载完整的文件
                                        stream.Position = 0;
                                        var local5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                        if (local5 != info.MD5)
                                        {
                                            stream.Close();
                                            fileInfo.Delete();
                                        }
                                        else
                                        {
                                            stream.Close();
                                            return;
                                        }
                                    }

                                    stream.Close();
                                }
                            }

                            info.Cancel();
                        }
                    }

                    info.SetCancellationTokenSource(new CancellationTokenSource());
                    using (var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                                                                 info.GetToken()))
                    {
                        info.Start().SetResponse(response);
                        if (string.IsNullOrEmpty(info.MD5))
                        {
                            var expectedMd5Bytes = MD5.Create().ComputeHash(new MemoryStream(await response.Content.ReadAsByteArrayAsync()));
                            info.SetMD5(BitConverter.ToString(expectedMd5Bytes).Replace("-", "").ToLower());
                        }

                        using (var fileStream = new FileStream(info.FullPath, FileMode.Append, FileAccess.Write,
                                                               FileShare.None, 4096, true))
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
            var headerSize = CODE.Length;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var buffer = new byte[stream.Length - headerSize];
                stream.Position = headerSize;
                _               = stream.Read(buffer, 0, buffer.Length);
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
}