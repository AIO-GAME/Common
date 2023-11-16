using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// FTP上传文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="remotePath">上传路径</param>
            /// <param name="progress">回调</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static void UploadFile(string uri, string username, string password,
                string localPath,
                string remotePath,
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var info = new FileInfo(localPath);
                if (info.Exists == false) throw new Exception($"FTP Upload : Target File Not Found {localPath}");

                progress.Total = info.Length;
                var remote = string.Concat(uri, '/', remotePath);
                var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.Timeout = timeout;
                reqFTP.ContentLength = info.Length;

                var buff = new byte[bufferSize];

                try
                {
                    using var fileStream = info.OpenRead();
                    using var requestStream = reqFTP.GetRequestStream();

                    var contentLen = fileStream.Read(buff, 0, bufferSize);
                    progress.Current += contentLen;
                    while (contentLen != 0)
                    {
                        requestStream.Write(buff, 0, contentLen);
                        progress.Current += contentLen;
                        contentLen = fileStream.Read(buff, 0, bufferSize);
                    }

                    requestStream.Close();
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    progress.OnError?.Invoke(ex);
                }
                finally
                {
                    progress.OnComplete?.Invoke();
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
            /// <param name="progress">进度回调</param>
            /// <param name="searchPattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static bool UploadFolder(string uri, string username, string password,
                string localPath,
                string remotePath,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*",
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var result = false;
                var info = new DirectoryInfo(localPath);
                if (info.Exists == false) throw new Exception($"ftp upload folder : target file not found {localPath}");

                var dicInfos = info.GetDirectories(searchPattern, searchOption);
                foreach (var dicInfo in dicInfos)
                {
                    var relativePath = dicInfo.FullName.Replace(localPath, "").Trim('/', '\\', ' ');
                    if (!Check(uri, username, password, relativePath, timeout))
                        CreateDir(uri, username, password, relativePath, timeout);
                }

                var fileInfos = info.GetFiles(searchPattern, searchOption);
                foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;

                var buff = new byte[bufferSize];
                foreach (var fileInfo in fileInfos)
                {
                    var relativePath = fileInfo.FullName.Replace(localPath, "").Trim('/', '\\', ' ');
                    var remote = string.Concat(uri, '/', remotePath,
                        '/', relativePath);
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                    request.Credentials = new NetworkCredential(username, password);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Timeout = timeout;
                    request.ContentLength = fileInfo.Length;

                    try
                    {
                        using var fileStream = fileInfo.OpenRead();
                        using var requestStream = request.GetRequestStream();

                        var contentLen = fileStream.Read(buff, 0, bufferSize);
                        progress.Current += contentLen;
                        while (contentLen != 0)
                        {
                            requestStream.Write(buff, 0, contentLen);
                            progress.Current += contentLen;
                            contentLen = fileStream.Read(buff, 0, bufferSize);
                        }

                        requestStream.Close();
                        fileStream.Close();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        progress.OnError?.Invoke(ex);
                    }
                }

                progress.OnComplete?.Invoke();
                return result;
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
            /// <param name="progress">进度回调</param>
            /// <param name="searchPattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static async Task<bool> UploadFolderAsync(string uri, string username, string password,
                string localPath,
                string remotePath,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*",
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var result = false;
                var info = new DirectoryInfo(localPath);
                if (info.Exists == false) throw new Exception($"ftp upload folder : target file not found {localPath}");

                var dicInfos = info.GetDirectories(searchPattern, searchOption);
                foreach (var dicInfo in dicInfos)
                {
                    var relativePath = dicInfo.FullName.Replace(localPath, "").Trim('/', '\\', ' ');
                    if (!await CheckAsync(uri, username, password, relativePath, timeout))
                        await CreateDirAsync(uri, username, password, relativePath, timeout);
                }

                var fileInfos = info.GetFiles(searchPattern, searchOption);
                foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;

                var buff = new byte[bufferSize];
                foreach (var fileInfo in fileInfos)
                {
                    var relativePath = fileInfo.FullName.Replace(localPath, "").Trim('/', '\\', ' ');
                    var remote = string.Concat(uri, '/', remotePath,
                        '/', relativePath);
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                    request.Credentials = new NetworkCredential(username, password);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Timeout = timeout;
                    request.ContentLength = fileInfo.Length;

                    try
                    {
                        using var fileStream = fileInfo.OpenRead();
                        using var requestStream = await request.GetRequestStreamAsync();

                        var contentLen = fileStream.Read(buff, 0, bufferSize);
                        progress.Current += contentLen;
                        while (contentLen != 0)
                        {
                            await requestStream.WriteAsync(buff, 0, contentLen);
                            progress.Current += contentLen;
                            contentLen = await fileStream.ReadAsync(buff, 0, bufferSize);
                        }

                        requestStream.Close();
                        fileStream.Close();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        progress.OnError?.Invoke(ex);
                    }
                }

                progress.OnComplete?.Invoke();
                return result;
            }
        }
    }
}