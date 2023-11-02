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
        /// <summary>
        /// FTP下载文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="remotePath">上传路径</param>
        /// <param name="progress">回调</param>
        /// <param name="isOverWrite">是否覆盖</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">下载缓存大小</param>
        public static void FTPDownloadFile(string uri, string username, string password,
            string localPath, string remotePath,
            ProgressArgs progress = default, bool isOverWrite = false, ushort timeout = TIMEOUT,
            int bufferSize = BUFFER_SIZE)
        {
            var fileSize = FTPGetFileSize(uri, username, password, remotePath);
            if (fileSize <= 0) return;

            try
            {
                progress.Total = fileSize;
                var outputStream = new FileStream(localPath, isOverWrite ? FileMode.CreateNew : FileMode.Create);
                var request = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, remotePath)));
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;
                request.Timeout = timeout;
                var response = (FtpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                if (responseStream is null) throw new Exception("FTP Stream is Null");

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
            catch (Exception ex)
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
        /// <param name="remotePath">上传路径</param>
        /// <param name="progress">回调</param>
        /// <param name="searchPattern">搜索过滤</param>
        /// <param name="searchOption">搜索模式</param>
        /// <param name="isOverWrite">是否覆盖</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">下载缓存大小</param>
        public static void FTPDownloadFolder(string uri, string username, string password,
            string localPath,
            string remotePath,
            ProgressArgs progress = default,
            AHandle.FTP.ListType searchOption = AHandle.FTP.ListType.ALL,
            string searchPattern = "*",
            bool isOverWrite = false,
            ushort timeout = TIMEOUT,
            int bufferSize = BUFFER_SIZE
        )
        {
            var remoteList = FTPGetRemoteList(Path.Combine(uri, remotePath), username, password,
                searchOption, false, searchPattern, timeout);

            var dict = new Dictionary<string, long>();
            foreach (var remote in remoteList)
            {
                var fileSize = FTPGetFileSize(uri, username, password, remote);
                progress.Total += fileSize;
                dict.Add(remote, fileSize);
            }

            foreach (var item in dict.Where(item => item.Value > 0))
            {
                try
                {
                    var outputStream = new FileStream(Path.Combine(localPath, item.Key),
                        isOverWrite ? FileMode.CreateNew : FileMode.Create);
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, item.Key)));
                    request.Credentials = new NetworkCredential(username, password);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    request.UseBinary = true;
                    request.Timeout = timeout;
                    var response = (FtpWebResponse)request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new Exception("FTP Stream is Null");

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
                catch (Exception ex)
                {
                    File.Delete(localPath);
                    progress.OnError?.Invoke(ex);
                }
            }
        }
    }
}