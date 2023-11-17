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
            /// <param name="progress">回调</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static void UploadFile(string uri, string username, string password,
                string localPath,
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var info = new FileInfo(localPath);
                if (info.Exists == false)
                    throw new FileNotFoundException($"ftp upload : target file not found {localPath}");

                try
                {
                    var request = CreateRequest(uri, username, password, WebRequestMethods.Ftp.UploadFile, timeout);
                    request.ContentLength = info.Length;

                    using var requestStream = request.GetRequestStream();
                    using var fileStream = info.OpenRead();
                    var buff = new byte[bufferSize];

                    var contentLen = fileStream.Read(buff, 0, bufferSize);
                    progress.Total = info.Length;
                    progress.Current += contentLen;
                    while (contentLen != 0)
                    {
                        requestStream.Write(buff, 0, contentLen);
                        progress.Current += contentLen;
                        contentLen = fileStream.Read(buff, 0, bufferSize);
                    }

                    fileStream.Close();
                    requestStream.Close();
                    request.Abort();
                }
                catch (WebException ex)
                {
                    progress.OnError?.Invoke(new Exception(string.Format("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(UploadFile), ex.Response.ResponseUri, username, password, ex.Message)));
                }
                finally
                {
                    progress.OnComplete?.Invoke();
                }
            }

            /// <summary>
            /// FTP上传文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="progress">回调</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static async Task UploadFileAsync(string uri, string username, string password,
                string localPath,
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var info = new FileInfo(localPath);
                if (info.Exists == false)
                    throw new FileNotFoundException($"FTP Upload : Target File Not Found {localPath}");

                try
                {
                    var request = CreateRequest(uri, username, password, WebRequestMethods.Ftp.UploadFile, timeout);
                    request.ContentLength = info.Length;
                    using var fileStream = info.OpenRead();
                    using var requestStream = await request.GetRequestStreamAsync();

                    var buff = new byte[bufferSize];
                    var contentLen = await fileStream.ReadAsync(buff, 0, bufferSize);
                    progress.Total = info.Length;
                    progress.Current += contentLen;
                    while (contentLen != 0)
                    {
                        await requestStream.WriteAsync(buff, 0, contentLen);
                        progress.Current += contentLen;
                        contentLen = await fileStream.ReadAsync(buff, 0, bufferSize);
                    }

                    requestStream.Close();
                    fileStream.Close();
                }
                catch (WebException ex)
                {
                    progress.OnError?.Invoke(new Exception(string.Format("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(UploadFileAsync), ex.Response.ResponseUri, username, password, ex.Message)));
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
            /// <param name="searchOption">搜索模式</param>
            /// <param name="progress">进度回调</param>
            /// <param name="searchPattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static bool UploadFolder(string uri, string username, string password,
                string localPath,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*",
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var result = false;
                var info = new DirectoryInfo(localPath);
                if (info.Exists == false)
                    throw new DirectoryNotFoundException($"ftp upload folder : target file not found {localPath}");

                foreach (var dicInfo in info.GetDirectories(searchPattern, searchOption))
                {
                    var relativePath = dicInfo.FullName.Replace(info.FullName, "");
                    var remote = string.Concat(uri, '/', relativePath);
                    CreateDir(remote, username, password, timeout);
                }

                var fileInfos = info.GetFiles(searchPattern, searchOption);
                foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;

                var buff = new byte[bufferSize];
                foreach (var fileInfo in fileInfos)
                {
                    var relativePath = fileInfo.FullName.Replace(info.FullName, "");
                    var remote = string.Concat(uri, '/', relativePath);
                    var request = CreateRequest(remote, username, password, WebRequestMethods.Ftp.UploadFile, timeout);
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
                    catch (WebException ex)
                    {
                        progress.OnError?.Invoke(new WebException(string.Format("{0} {2}:{3}@{1} ->\n {4}",
                            nameof(UploadFolder), ex.Response.ResponseUri, username, password, ex.Message)));
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
            /// <param name="searchOption">搜索模式</param>
            /// <param name="progress">进度回调</param>
            /// <param name="searchPattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static async Task<bool> UploadFolderAsync(string uri, string username, string password,
                string localPath,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*",
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var result = false;
                var info = new DirectoryInfo(localPath);
                if (info.Exists == false)
                    throw new DirectoryNotFoundException($"ftp upload folder : target file not found {localPath}");

                foreach (var dicInfo in info.GetDirectories(searchPattern, searchOption))
                {
                    if (progress.IsCancel) break;
                    var relativePath = dicInfo.FullName.Replace(info.FullName, "");
                    var remote = string.Concat(uri, '/', relativePath);
                    await CreateDirAsync(remote, username, password, timeout);
                }

                var fileInfos = info.GetFiles(searchPattern, searchOption);
                foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;

                var buff = new byte[bufferSize];
                foreach (var fileInfo in fileInfos)
                {
                    if (progress.IsCancel) break;
                    var relativePath = fileInfo.FullName.Replace(info.FullName, "");
                    var remote = string.Concat(uri, '/', relativePath);
                    var request = CreateRequest(remote, username, password, "STOR", timeout);
                    request.ContentLength = fileInfo.Length;
                    progress.OnCancel = request.Abort;
                    try
                    {
                        using var requestStream = await request.GetRequestStreamAsync();
                        using var fileStream = fileInfo.OpenRead();

                        var contentLen = await fileStream.ReadAsync(buff, 0, bufferSize);
                        progress.Current += contentLen;
                        while (contentLen != 0)
                        {
                            await requestStream.WriteAsync(buff, 0, contentLen);
                            progress.Current += contentLen;
                            contentLen = await fileStream.ReadAsync(buff, 0, bufferSize);
                        }

                        fileStream.Close();
                        requestStream.Close();
                        result = true;
                    }
                    catch (WebException ex)
                    {
                        progress.OnError?.Invoke(new WebException(string.Format("{0} {2}:{3}@{1} ->\n {4}",
                            nameof(UploadFolderAsync), ex.Response.ResponseUri, username, password, ex.Message)));
                    }
                }

                progress.OnComplete?.Invoke();
                return result;
            }
        }
    }
}