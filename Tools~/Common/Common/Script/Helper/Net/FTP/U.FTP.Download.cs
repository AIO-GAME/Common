using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

public partial class AHelper
{
    /// <summary>
    /// 网络 工具类
    /// </summary>
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// FTP下载文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="progress">回调</param>
            /// <param name="overwrite">是否覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">下载缓存大小</param>
            public static void DownloadFile(string uri, string username, string password,
                string localPath,
                bool overwrite = false, ProgressArgs progress = default, ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE)
            {
                var fileSize = GetFileSize(uri, username, password);
                if (fileSize <= 0) return;

                try
                {
                    var request = CreateRequest(uri, username, password, "RETR", timeout);
                    using var response = request.GetResponse();
                    progress.Total = fileSize;
                    progress.OnCancel = request.Abort;

                    using var responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new Exception("FTP Stream is Null");

                    var buffer = new byte[bufferSize];
                    var readCount = responseStream.Read(buffer, 0, bufferSize);
                    using var outputStream =
                        new FileStream(localPath, overwrite ? FileMode.CreateNew : FileMode.Create);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        progress.Current += readCount;
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }

                    progress.OnComplete?.Invoke();
                    responseStream.Close();
                    outputStream.Close();
                    response.Close();
                }
                catch (WebException ex)
                {
                    File.Delete(localPath);
                    progress.OnError?.Invoke(ex);
                }
            }

            /// <summary>
            /// FTP下载文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="progress">回调</param>
            /// <param name="searchPattern">搜索过滤</param>
            /// <param name="searchOption">搜索模式</param>
            /// <param name="isOverWrite">是否覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">下载缓存大小</param>
            public static void DownloadFolder(string uri, string username, string password,
                string localPath,
                AHandle.FTP.ListType searchOption = AHandle.FTP.ListType.ALL,
                string searchPattern = "*",
                bool isOverWrite = false,
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var remoteList = GetRemoteList(uri, username, password,
                    searchOption, false, searchPattern, timeout);

                var dict = new Dictionary<string, long>();
                foreach (var remote in remoteList)
                {
                    if (progress.IsCancel) continue;
                    var fileSize = GetFileSize(string.Concat(uri, '/', remote), username, password);
                    progress.Total += fileSize;
                    dict.Add(remote, fileSize);
                }

                foreach (var item in dict.Where(item => item.Value > 0))
                {
                    if (progress.IsCancel) continue;
                    try
                    {
                        var remote = string.Concat(uri, '/', item.Key);
                        var request = CreateRequest(remote, username, password, "RETR", timeout);
                        progress.OnCancel = request.Abort;

                        using var response = request.GetResponse();
                        using var responseStream = response.GetResponseStream();
                        if (responseStream is null) throw new Exception("FTP Stream is Null");
                        using var outputStream = new FileStream(Path.Combine(localPath, item.Key),
                            isOverWrite ? FileMode.CreateNew : FileMode.Create);

                        var buffer = new byte[bufferSize];
                        var readCount = responseStream.Read(buffer, 0, bufferSize);
                        while (readCount > 0)
                        {
                            outputStream.Write(buffer, 0, readCount);
                            progress.Current += readCount;
                            readCount = responseStream.Read(buffer, 0, bufferSize);
                        }

                        progress.OnComplete?.Invoke();
                        responseStream.Close();
                        outputStream.Close();
                        response.Close();
                    }
                    catch (WebException ex)
                    {
                        File.Delete(localPath);
                        progress.OnError?.Invoke(ex);
                    }
                }
            }
        }
    }
}