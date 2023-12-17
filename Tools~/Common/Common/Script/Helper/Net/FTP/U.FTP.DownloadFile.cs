using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

public partial class AHelper
{
    /// <summary>
    /// 网络 工具类
    /// </summary>
    public partial class Net
    {
        public partial class FTP
        {
            private class FTPDownloadFileOperation : AOperation
            {
                private CancellationTokenSource cancellationTokenSource { get; }
                private CancellationToken cancellationToken;
                private string uri { get; }
                private string username { get; }
                private string password { get; }
                private string localPath { get; }
                private bool overwrite { get; }
                private ushort timeout { get; }
                private int bufferSize { get; }

                protected override void OnDispose()
                {
                    cancellationTokenSource.Dispose();
                }

                protected override void OnCancel()
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                public FTPDownloadFileOperation(
                    string uri,
                    string username,
                    string password,
                    string localPath,
                    bool overwrite = false,
                    ushort timeout = TIMEOUT,
                    int bufferSize = BUFFER_SIZE)
                {
                    this.uri = uri;
                    this.username = username;
                    this.password = password;
                    this.localPath = localPath;
                    this.overwrite = overwrite;
                    this.timeout = timeout;
                    this.bufferSize = bufferSize;
                    cancellationTokenSource = new CancellationTokenSource();
                    cancellationToken = cancellationTokenSource.Token;
                }

                protected override void OnWait()
                {
                    var fileSize = GetFileSize(uri, username, password);
                    if (fileSize <= 0)
                    {
                        State = EProgressState.Fail;
                        return;
                    }

                    try
                    {
                        var request = CreateRequestFile(uri, username, password, "RETR", timeout);
                        using var response = request.GetResponse();
                        progress.Total = fileSize;
                        progress.CurrentInfo = response.ResponseUri.AbsoluteUri;

                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream is null)
                            {
                                progress.State = EProgressState.Fail;
                                progress.OnError?.Invoke(new Exception("FTP Stream is Null"));
                                return;
                            }

                            var buffer = new byte[bufferSize];
                            using (var outputStream =
                                   AddFileHeader(localPath, GetMD5(uri, username, password), overwrite))
                            {
                                var readCount = responseStream.Read(buffer, 0, bufferSize);
                                while (readCount > 0)
                                {
                                    switch (progress.State)
                                    {
                                        case EProgressState.Cancel:
                                            progress.OnError?.Invoke(new TaskCanceledException());
                                            return;
                                        case EProgressState.Running:
                                            progress.Current += readCount;
                                            outputStream.Write(buffer, 0, readCount);
                                            readCount = responseStream.Read(buffer, 0, bufferSize);
                                            break;
                                        default:
                                            Thread.Sleep(100);
                                            break;
                                    }
                                }

                                RemoveFileHeader(outputStream, bufferSize);
                                outputStream.Flush();
                            }
                        }

                        State = EProgressState.Finish;
                        request.Abort();
                    }
                    catch (WebException ex)
                    {
                        File.Delete(localPath);
                        State = EProgressState.Fail;
                        progress.OnError?.Invoke(ex);
                    }
                }

                protected override async Task OnWaitAsync()
                {
                    var fileSize = await GetFileSizeAsync(uri, username, password);
                    if (fileSize <= 0)
                    {
                        State = EProgressState.Fail;
                        progress.OnError?.Invoke(new Exception("ftp file size is less or equal to 0"));
                        return;
                    }

                    try
                    {
                        var request = CreateRequestFile(uri, username, password, "RETR", timeout);
                        using var response = await request.GetResponseAsync();
                        progress.Total = fileSize;
                        progress.CurrentInfo = response.ResponseUri.AbsoluteUri;

                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream is null)
                            {
                                progress.State = EProgressState.Fail;
                                progress.OnError?.Invoke(new Exception("FTP Stream is Null"));
                                return;
                            }

                            var buffer = new byte[bufferSize];
                            using (var outputStream =
                                   await AddFileHeaderAsync(localPath, await GetMD5Async(uri, username, password),
                                       overwrite,
                                       cancellationToken))
                            {
                                var readCount =
                                    await responseStream.ReadAsync(buffer, 0, bufferSize, cancellationToken);
                                while (readCount > 0)
                                {
                                    switch (progress.State)
                                    {
                                        case EProgressState.Cancel:
                                            progress.OnError?.Invoke(new TaskCanceledException());
                                            return;
                                        case EProgressState.Running:
                                            progress.Current += readCount;
                                            await outputStream.WriteAsync(buffer, 0, readCount, cancellationToken);
                                            readCount = await responseStream.ReadAsync(buffer, 0, bufferSize,
                                                cancellationToken);
                                            break;
                                        default:
                                            await Task.Delay(100, cancellationToken);
                                            break;
                                    }
                                }

                                await RemoveFileHeaderAsync(outputStream, bufferSize, cancellationToken);
                                await outputStream.FlushAsync(cancellationToken);
                            }
                        }


                        request.Abort();
                        progress.State = EProgressState.Finish;
                    }
                    catch (WebException ex)
                    {
                        File.Delete(localPath);
                        progress.OnError?.Invoke(ex);
                        progress.State = EProgressState.Fail;
                    }
                }
            }

            /// <summary>
            /// FTP下载文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="overwrite">是否覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">下载缓存大小</param>
            public static IProgressOperation DownloadFile(
                string uri,
                string username,
                string password,
                string localPath,
                bool overwrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE)
            {
                return new FTPDownloadFileOperation(uri, username, password, localPath, overwrite, timeout, bufferSize);
            }

            /// <summary>
            /// 获取文件下载列表
            /// </summary>
            private static IEnumerable<string> GetDownloadList(string uri, string username, string password,
                SearchOption option = SearchOption.AllDirectories,
                string searchPattern = "*",
                ushort timeout = TIMEOUT, string abs = null
            )
            {
                var remoteList =
                    GetRemoteList(uri, username, password, AHandle.FTP.ListType.File, searchPattern, timeout)
                        .Select(file => string.Concat(abs, '/', Path.GetFileName(file)).Trim('/'))
                        .ToList();

                if (option != SearchOption.AllDirectories) return remoteList;

                foreach (var absPath in GetRemoteList(
                                 uri, username, password, AHandle.FTP.ListType.Directory, searchPattern, timeout)
                             .Select(Path.GetFileName))
                {
                    var collection = GetDownloadList(
                        string.Concat(uri, '/', absPath), username, password, option, searchPattern, timeout,
                        string.Concat(abs, '/', absPath).Trim('/'));
                    remoteList.AddRange(collection);
                }

                return remoteList;
            }
        }
    }
}