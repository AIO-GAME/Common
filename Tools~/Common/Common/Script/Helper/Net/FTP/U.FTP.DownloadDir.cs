#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: FTP

        public partial class FTP
        {
            /// <summary>
            /// FTP下载文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="pattern">搜索过滤</param>
            /// <param name="option">搜索模式</param>
            /// <param name="overwrite">是否覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">下载缓存大小</param>
            public static IProgressOperation DownloadDir(
                string       uri,
                string       username,
                string       password,
                string       localPath,
                SearchOption option     = SearchOption.AllDirectories,
                string       pattern    = "*",
                bool         overwrite  = false,
                ushort       timeout    = Net.TIMEOUT,
                int          bufferSize = Net.BUFFER_SIZE
            )
            {
                return new FTPDownloadDirOperation(uri, username, password, localPath, option, pattern, overwrite,
                                                   timeout, bufferSize);
            }

            #region Nested type: FTPDownloadDirOperation

            private class FTPDownloadDirOperation : AOperation
            {
                /// <param name="uri">路径</param>
                /// <param name="username">用户名</param>
                /// <param name="password">密码</param>
                /// <param name="localPath">本地文件路径</param>
                /// <param name="pattern">搜索过滤</param>
                /// <param name="option">搜索模式</param>
                /// <param name="overwrite">是否覆盖</param>
                /// <param name="timeout">超时</param>
                /// <param name="bufferSize">下载缓存大小</param>
                public FTPDownloadDirOperation(
                    string       uri,
                    string       username,
                    string       password,
                    string       localPath,
                    SearchOption option     = SearchOption.AllDirectories,
                    string       pattern    = "*",
                    bool         overwrite  = false,
                    ushort       timeout    = Net.TIMEOUT,
                    int          bufferSize = Net.BUFFER_SIZE)
                {
                    this.uri        = uri.Replace('\\', '/');
                    this.username   = username;
                    this.password   = password;
                    this.localPath  = localPath;
                    this.option     = option;
                    this.pattern    = pattern;
                    this.overwrite  = overwrite;
                    this.timeout    = timeout;
                    this.bufferSize = bufferSize;
                }

                private string       uri        { get; }
                private string       username   { get; }
                private string       password   { get; }
                private string       localPath  { get; }
                private SearchOption option     { get; }
                private string       pattern    { get; }
                private bool         overwrite  { get; }
                private ushort       timeout    { get; }
                private int          bufferSize { get; }

                protected override void OnWait()
                {
                    var remoteList = GetDownloadList(uri, username, password,
                                                     option, pattern, timeout);
                    var dict = new Dictionary<string, FileStream>();
                    foreach (var remoteAbs in remoteList)
                    {
                        if (State == EProgressState.Cancel)
                        {
                            Event.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        var remote   = string.Concat(uri, '/', remoteAbs);
                        var fileSize = GetFileSize(remote, username, password, timeout);
                        if (fileSize <= 0) continue;

                        var outputStream = Net.AddFileHeader(
                                                             Path.Combine(localPath, remoteAbs),
                                                             () => GetMD5(remote, username, password, timeout),
                                                             overwrite);
                        if (outputStream is null) continue;
                        if (outputStream.CanWrite)
                        {
                            var temp = outputStream.Position - Net.CODE.Length;
                            if (temp < 0) continue;
                            TotalValue      += fileSize - temp;
                            StartValue      += temp;
                            dict[remoteAbs] =  outputStream;
                        }
                        else
                        {
                            StartValue += fileSize;
                        }
                    }

                    var buffer = new byte[bufferSize];
                    foreach (var pair in dict)
                    {
                        if (State == EProgressState.Cancel)
                        {
                            Event.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        using var outputStream = pair.Value;
                        var       remote       = string.Concat(uri, '/', pair.Key);
                        var       request      = CreateRequestFile(remote, username, password, "RETR", timeout, cancellationToken);
                        request.ContentOffset = outputStream.Position - Net.CODE.Length;
                        CurrentInfo           = request.RequestUri.AbsoluteUri;
                        try
                        {
                            using var response = request.GetResponse();
                            using (var responseStream = response.GetResponseStream())
                            {
                                if (responseStream is null)
                                {
                                    State = EProgressState.Fail;
                                    Event.OnError?.Invoke(new WebException("ftp stream is null"));
                                    return;
                                }


                                var readCount = responseStream.Read(buffer, 0, bufferSize);
                                while (readCount > 0)
                                    switch (State)
                                    {
                                        case EProgressState.Cancel:
                                            Event.OnError?.Invoke(new TaskCanceledException("ftp download cancel"));
                                            return;
                                        case EProgressState.Running:
                                            CurrentValue += readCount;
                                            outputStream.Write(buffer, 0, readCount);
                                            readCount = responseStream.Read(buffer, 0, bufferSize);
                                            break;
                                        default:
                                            Thread.Sleep(100);
                                            break;
                                    }

                                Net.RemoveFileHeader(outputStream, bufferSize);
                                outputStream.Flush();
                                outputStream.Close();
                            }

                            request.Abort();
                        }
                        catch (WebException ex)
                        {
                            State = EProgressState.Fail;
                            outputStream.Close();
                            File.Delete(string.Concat(localPath, '/', pair.Key));
                            Event.OnError?.Invoke(ex);
                            request.Abort();
                            return;
                        }
                    }

                    State = EProgressState.Finish;
                }

                protected override async Task OnWaitAsync()
                {
                    var remoteList = await GetDownloadListAsync(uri, username, password,
                                                                option, pattern, timeout, cancellationToken: cancellationToken);
                    var dict = new Dictionary<string, FileStream>();
                    foreach (var remoteAbs in remoteList)
                    {
                        if (State == EProgressState.Cancel)
                        {
                            Event.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        var remote   = string.Concat(uri, '/', remoteAbs);
                        var fileSize = await GetFileSizeAsync(remote, username, password, timeout, cancellationToken);
                        if (fileSize <= 0) continue;

                        var outputStream = await Net.AddFileHeaderAsync(
                                                                        Path.Combine(localPath, remoteAbs),
                                                                        () => GetMD5Async(remote, username, password, timeout, cancellationToken),
                                                                        overwrite);
                        if (outputStream is null) continue;
                        if (outputStream.CanWrite)
                        {
                            var temp = outputStream.Position - Net.CODE.Length;
                            if (temp < 0) continue;
                            TotalValue      += fileSize - temp;
                            StartValue      += temp;
                            dict[remoteAbs] =  outputStream;
                        }
                        else
                        {
                            StartValue += fileSize;
                        }
                    }

                    var buffer = new byte[bufferSize];
                    foreach (var pair in dict)
                    {
                        if (State == EProgressState.Cancel)
                        {
                            Event.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        using var outputStream = pair.Value;
                        var       remote       = string.Concat(uri, '/', pair.Key);
                        var       request      = CreateRequestFile(remote, username, password, "RETR", timeout, cancellationToken);
                        request.ContentOffset = outputStream.Position - Net.CODE.Length;
                        CurrentInfo           = request.RequestUri.AbsoluteUri;
                        try
                        {
                            using var response = await request.GetResponseAsync();
                            using (var responseStream = response.GetResponseStream())
                            {
                                if (responseStream is null)
                                {
                                    State = EProgressState.Fail;
                                    Event.OnError?.Invoke(new WebException("ftp stream is null"));
                                    return;
                                }


                                var readCount = await responseStream.ReadAsync(buffer, 0, bufferSize, cancellationToken);
                                while (readCount > 0)
                                    switch (State)
                                    {
                                        case EProgressState.Cancel:
                                            Event.OnError?.Invoke(new TaskCanceledException("ftp download cancel"));
                                            return;
                                        case EProgressState.Running:
                                            CurrentValue += readCount;
                                            await outputStream.WriteAsync(buffer, 0, readCount, cancellationToken);
                                            readCount = await responseStream.ReadAsync(buffer, 0, bufferSize,
                                                                                       cancellationToken);
                                            break;
                                        default:
                                            await Task.Delay(100, cancellationToken);
                                            break;
                                    }

                                await Net.RemoveFileHeaderAsync(outputStream, bufferSize, cancellationToken);
                                await outputStream.FlushAsync(cancellationToken);
                                outputStream.Close();
                            }

                            request.Abort();
                        }
                        catch (WebException ex)
                        {
                            State = EProgressState.Fail;
                            outputStream.Close();
                            File.Delete(string.Concat(localPath, '/', pair.Key));
                            Event.OnError?.Invoke(ex);
                            request.Abort();
                            return;
                        }
                    }

                    State = EProgressState.Finish;
                }
            }

            #endregion
        }

        #endregion
    }
}