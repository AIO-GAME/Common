using System;
using System.IO;
using System.Net;

public partial class AHelper
{
    public partial class Net
    {
        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="remotePath">上传路径</param>
        /// <param name="iprogress">回调</param>
        /// <param name="timeout">超时</param>
        /// <param name="buffSize">缓存大小</param>
        public static void FTPUploadFile(string uri, string username, string password,
            string localPath,
            string remotePath,
            AHandle.IProgress iprogress = null,
            ushort timeout = 3000,
            int buffSize = 2048
        )
        {
            var info = new FileInfo(localPath);
            if (info.Exists == false) throw new Exception($"FTP Upload : Target File Not Found {localPath}");

            var progress = new AHandle.Progress(iprogress);
            progress.Total = info.Length;

            var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri + remotePath));
            reqFTP.Credentials = new NetworkCredential(username, password);
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.KeepAlive = false;
            reqFTP.UseBinary = true;
            reqFTP.Timeout = timeout;
            reqFTP.ContentLength = info.Length;

            var buff = new byte[buffSize];

            try
            {
                using var fileStream = info.OpenRead();
                using var requestStream = reqFTP.GetRequestStream();

                var contentLen = fileStream.Read(buff, 0, buffSize);
                var total = contentLen;
                while (contentLen != 0)
                {
                    requestStream.Write(buff, 0, contentLen);
                    total += contentLen;
                    progress.Report(total);
                    contentLen = fileStream.Read(buff, 0, buffSize);
                }

                requestStream.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                progress.Error(ex);
            }
            finally
            {
                progress.Complete();
            }
        }

        /// <summary>
        /// FTP上传文件夹
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="remotePath">上传路径</param>
        /// <param name="searchOption">搜索模式</param>
        /// <param name="iprogress">进度回调</param>
        /// <param name="searchPattern">匹配模式</param>
        /// <param name="timeout">超时</param>
        /// <param name="buffSize">缓存大小</param>
        public static void FTPUploadFolder(string uri, string username, string password,
            string localPath,
            string remotePath,
            AHandle.IProgress iprogress = null,
            SearchOption searchOption = SearchOption.TopDirectoryOnly,
            string searchPattern = "*",
            ushort timeout = 3000,
            int buffSize = 2048
        )
        {
            var info = new DirectoryInfo(localPath);
            if (info.Exists == false) throw new Exception($"FTP Upload Folder : Target File Not Found {localPath}");

            var progress = new AHandle.Progress(iprogress);
            var fileInfos = info.GetFiles(searchPattern, searchOption);
            foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;
            var total = 0;


            var buff = new byte[buffSize];
            foreach (var fileInfo in fileInfos)
            {
                var relativePath = fileInfo.FullName.Replace(info.FullName, "");
                var request = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, remotePath, relativePath)));
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = false;
                request.UseBinary = true;
                request.Timeout = timeout;
                request.ContentLength = fileInfo.Length;

                try
                {
                    using var fileStream = fileInfo.OpenRead();
                    using var requestStream = request.GetRequestStream();

                    var contentLen = fileStream.Read(buff, 0, buffSize);
                    total += contentLen;
                    while (contentLen != 0)
                    {
                        requestStream.Write(buff, 0, contentLen);
                        total += contentLen;
                        progress.Report(total);
                        contentLen = fileStream.Read(buff, 0, buffSize);
                    }

                    requestStream.Close();
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    progress.Error(ex);
                }
            }

            progress.Complete();
        }
    }
}