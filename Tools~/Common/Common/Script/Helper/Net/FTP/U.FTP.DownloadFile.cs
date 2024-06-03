#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                bool   overwrite  = false,
                ushort timeout    = Net.TIMEOUT,
                int    bufferSize = Net.BUFFER_SIZE)
            {
                return new FTPDownloadFileOperation(uri, username, password, localPath, overwrite, timeout, bufferSize);
            }

            /// <summary>
            /// 获取文件下载列表
            /// </summary>
            private static IEnumerable<string> GetDownloadList(string       uri, string username, string password,
                                                               SearchOption option        = SearchOption.AllDirectories,
                                                               string       searchPattern = "*",
                                                               ushort       timeout       = Net.TIMEOUT, string abs = null
            )
            {
                var remoteList =
                    GetRemoteList(uri, username, password, AHandle.FTP.ListType.File, searchPattern, timeout).Select(file => string.Concat(abs, '/', Path.GetFileName(file)).Trim('/')).ToList();

                if (option != SearchOption.AllDirectories) return remoteList;

                foreach (var absPath in GetRemoteList(
                             uri, username, password, AHandle.FTP.ListType.Directory, searchPattern, timeout).Select(Path.GetFileName))
                {
                    var collection = GetDownloadList(
                        string.Concat(uri, '/', absPath), username, password, option, searchPattern, timeout,
                        string.Concat(abs, '/', absPath).Trim('/'));
                    remoteList.AddRange(collection);
                }

                return remoteList;
            }

            #region Nested type: FTPDownloadFileOperation

            private class FTPDownloadFileOperation : AOperation
            {
                public FTPDownloadFileOperation(
                    string uri,
                    string username,
                    string password,
                    string localPath,
                    bool   overwrite,
                    ushort timeout,
                    int    bufferSize)
                {
                    this.uri        = uri.Replace('\\', '/');
                    this.username   = username;
                    this.password   = password;
                    this.localPath  = localPath;
                    this.overwrite  = overwrite;
                    this.timeout    = timeout;
                    this.bufferSize = bufferSize;
                }

                private string uri        { get; }
                private string username   { get; }
                private string password   { get; }
                private string localPath  { get; }
                private bool   overwrite  { get; }
                private ushort timeout    { get; }
                private int    bufferSize { get; }

                protected override void OnWait()
                {
                    try
                    {
                        var fileSize = GetFileSize(uri, username, password);
                        if (fileSize <= 0)
                        {
                            State = EProgressState.Fail;
                            Event.OnError?.Invoke(new Exception("ftp file size is less or equal to 0"));
                            return;
                        }

                        using var outputStream = Net.AddFileHeader(
                            localPath,
                            () => GetMD5(uri, username, password, timeout),
                            overwrite);
                        if (outputStream is null)
                        {
                            State = EProgressState.Finish;
                            return;
                        }

                        if (!outputStream.CanWrite)
                        {
                            State      =  EProgressState.Finish;
                            StartValue += fileSize;
                            return;
                        }

                        var request = CreateRequestFile(uri, username, password, "RETR", timeout);
                        var temp = outputStream.Position - Net.CODE.Length;
                        if (temp > 0) request.ContentOffset = temp;

                        using var response = request.GetResponse();

                        TotalValue  = fileSize - temp;
                        CurrentInfo = response.ResponseUri.AbsoluteUri;
                        StartValue  = temp;

                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream is null)
                            {
                                State = EProgressState.Fail;
                                Event.OnError?.Invoke(new Exception("FTP Stream is Null"));
                                return;
                            }


                            var buffer = new byte[bufferSize];
                            var readCount = responseStream.Read(buffer, 0, bufferSize);
                            while (readCount > 0)
                                switch (State)
                                {
                                    case EProgressState.Cancel:
                                        Event.OnError?.Invoke(new TaskCanceledException());
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
                        }


                        request.Abort();
                        State = EProgressState.Finish;
                    }
                    catch (WebException ex)
                    {
                        File.Delete(localPath);
                        Event.OnError?.Invoke(ex);
                        State = EProgressState.Fail;
                    }
                }

                protected override async Task OnWaitAsync()
                {
                    try
                    {
                        var fileSize = await GetFileSizeAsync(uri, username, password);
                        if (fileSize <= 0)
                        {
                            State = EProgressState.Fail;
                            Event.OnError?.Invoke(new Exception("ftp file size is less or equal to 0"));
                            return;
                        }

                        using var outputStream = await Net.AddFileHeaderAsync(
                            localPath,
                            () => GetMD5Async(uri, username, password, timeout, cancellationToken),
                            overwrite,
                            cancellationToken);
                        if (outputStream is null)
                        {
                            State = EProgressState.Finish;
                            return;
                        }

                        if (!outputStream.CanWrite)
                        {
                            State      =  EProgressState.Finish;
                            StartValue += fileSize;
                            return;
                        }

                        var request = CreateRequestFile(uri, username, password, "RETR", timeout, cancellationToken);
                        var temp = outputStream.Position - Net.CODE.Length;
                        if (temp > 0) request.ContentOffset = temp;

                        using var response = await request.GetResponseAsync();
                        TotalValue  = fileSize - temp;
                        CurrentInfo = response.ResponseUri.AbsoluteUri;
                        StartValue  = temp;

                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream is null)
                            {
                                State = EProgressState.Fail;
                                Event.OnError?.Invoke(new WebException("ftp stream is null"));
                                return;
                            }

                            var buffer = new byte[bufferSize];
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
                        }


                        request.Abort();
                        State = EProgressState.Finish;
                    }
                    catch (WebException ex)
                    {
                        File.Delete(localPath);
                        Event.OnError?.Invoke(ex);
                        State = EProgressState.Fail;
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}