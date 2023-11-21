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
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="progress">回调</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static bool UploadFile(string uri, string user, string pass,
                string localPath,
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var fileInfo = new FileInfo(localPath);
                if (fileInfo.Exists == false)
                    throw new FileNotFoundException($"ftp upload : target file not found {localPath}");

                try
                {
                    var startIndex = uri.LastIndexOf('/') + 1;
                    var parent = uri.Substring(0, startIndex);
                    if (!CheckDir(parent, user, pass, timeout))
                    {
                        if (!CreateDir(parent, user, pass, timeout))
                            return false;
                    }

                    var request = CreateRequestFile(uri, user, pass, "STOR", timeout);

                    request.ContentLength = fileInfo.Length;
                    progress.Total = fileInfo.Length;
                    progress.CurrentInfo = fileInfo.FullName;
                    progress.OnCancel = request.Abort;

                    using (var requestStream = request.GetRequestStream())
                    {
                        using (var fileStream = fileInfo.OpenRead())
                        {
                            var contentLen = 0L;
                            while (fileStream.Position < fileStream.Length)
                            {
                                fileStream.CopyTo(requestStream, bufferSize);
                                contentLen = fileStream.Position - contentLen;
                                progress.Current += contentLen;
                            }

                            requestStream.Flush();
                        }
                    }


                    progress.OnComplete?.Invoke();
                    return true;
                }
                catch (WebException ex)
                {
                    progress.OnError?.Invoke(ex);
                    return false;
                }
            }

            /// <summary>
            /// FTP上传文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="progress">回调</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static async Task<bool> UploadFileAsync(string uri, string user, string pass,
                string localPath,
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var fileInfo = new FileInfo(localPath);
                if (fileInfo.Exists == false)
                    throw new FileNotFoundException($"FTP Upload : Target File Not Found {localPath}");

                try
                {
                    var startIndex = uri.LastIndexOf('/') + 1;
                    var parent = uri.Substring(0, startIndex);
                    if (!await CheckDirAsync(parent, user, pass, timeout))
                    {
                        if (!await CreateDirAsync(parent, user, pass, timeout))
                            return false;
                    }

                    var request = CreateRequestFile(uri, user, pass, "STOR", timeout);
                    request.ContentLength = fileInfo.Length;
                    progress.Total = fileInfo.Length;
                    progress.CurrentInfo = fileInfo.FullName;
                    progress.OnCancel = request.Abort;

                    using (var requestStream = await request.GetRequestStreamAsync())
                    {
                        using (var fileStream = fileInfo.OpenRead())
                        {
                            var contentLen = 0L;
                            while (fileStream.Position < fileStream.Length)
                            {
                                await fileStream.CopyToAsync(requestStream, bufferSize);
                                contentLen = fileStream.Position - contentLen;
                                progress.Current += contentLen;
                            }
                        }

                        await requestStream.FlushAsync();
                    }

                    progress.OnComplete?.Invoke();
                    return true;
                }
                catch (WebException ex)
                {
                    progress.OnError?.Invoke(ex);
                    return false;
                }
            }

            /// <summary>
            /// FTP上传文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="option">搜索模式</param>
            /// <param name="progress">进度回调</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static bool UploadFolder(string uri, string user, string pass,
                string localPath,
                SearchOption option = SearchOption.AllDirectories,
                string pattern = "*",
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var info = new DirectoryInfo(localPath);
                if (info.Exists == false)
                    throw new DirectoryNotFoundException($"ftp upload folder : target file not found {localPath}");

                foreach (var dicInfo in info.GetDirectories(pattern, option))
                {
                    if (progress.IsCancel) throw new WebException($"FTP Upload Folder Cancel -> {uri}");
                    var relativePath = dicInfo.FullName.Replace(info.FullName, "");
                    var remote = string.Concat(uri, '/', relativePath);
                    if (CheckDir(remote, user, pass, timeout)) continue;
                    if (CreateDir(remote, user, pass, timeout)) continue;
                    progress.OnError?.Invoke(new WebException($"FTP Upload Folder Create Dir Error -> {uri}"));
                    return false;
                }

                var fileInfos = info.GetFiles(pattern, option);
                foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;

                foreach (var fileInfo in fileInfos)
                {
                    if (progress.IsCancel) throw new WebException($"FTP Upload Folder Cancel -> {uri}");
                    var relativePath = fileInfo.FullName.Replace(info.FullName, "").Trim('/', '\\');
                    var remote = string.Concat(uri, '/', relativePath);
                    var request = CreateRequestFile(remote, user, pass, "STOR", timeout);
                    request.ContentLength = fileInfo.Length;
                    progress.CurrentInfo = relativePath;

                    try
                    {
                        using (var requestStream = request.GetRequestStream())
                        {
                            using (var fileStream = fileInfo.OpenRead())
                            {
                                var contentLen = 0L;
                                while (fileStream.Position < fileStream.Length)
                                {
                                    fileStream.CopyTo(requestStream, bufferSize);
                                    contentLen = fileStream.Position - contentLen;
                                    progress.Current += contentLen;
                                }
                            }

                            requestStream.Flush();
                        }


                        request.Abort();
                    }
                    catch (WebException ex)
                    {
                        request.Abort();
                        progress.OnError?.Invoke(ex);
                        return false;
                    }
                }

                progress.OnComplete?.Invoke();
                return true;
            }

            /// <summary>
            /// FTP上传文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="option">搜索模式</param>
            /// <param name="progress">进度回调</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static async Task<bool> UploadFolderAsync(string uri, string user, string pass,
                string localPath,
                SearchOption option = SearchOption.AllDirectories,
                string pattern = "*",
                ProgressArgs progress = default,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var info = new DirectoryInfo(localPath);
                if (info.Exists == false)
                    throw new DirectoryNotFoundException($"ftp upload folder : target file not found {localPath}");

                foreach (var dicInfo in info.GetDirectories(pattern, option))
                {
                    if (progress.IsCancel) throw new WebException($"FTP Upload Folder Cancel -> {uri}");
                    var relativePath = dicInfo.FullName.Replace(info.FullName, "");
                    var remote = string.Concat(uri, '/', relativePath);
                    if (await CheckDirAsync(remote, user, pass, timeout)) continue;
                    if (await CreateDirAsync(remote, user, pass, timeout)) continue;
                    progress.OnError?.Invoke(new WebException($"FTP Upload Folder Create Dir Error -> {remote}"));
                    return false;
                }

                var fileInfos = info.GetFiles(pattern, option);
                foreach (var fileInfo in fileInfos) progress.Total += fileInfo.Length;

                foreach (var fileInfo in fileInfos)
                {
                    if (progress.IsCancel) throw new WebException($"FTP Upload Folder Cancel -> {uri}");
                    var relativePath = fileInfo.FullName.Replace(info.FullName, "").Trim('/', '\\');
                    var remote = string.Concat(uri, '/', relativePath);
                    var request = CreateRequestFile(remote, user, pass, "STOR", -1);
                    request.ContentLength = fileInfo.Length;
                    progress.CurrentInfo = relativePath;

                    try
                    {
                        using (var requestStream = await request.GetRequestStreamAsync())
                        {
                            using (var fileStream = fileInfo.OpenRead())
                            {
                                var contentLen = 0L;
                                while (fileStream.Position < fileStream.Length)
                                {
                                    await fileStream.CopyToAsync(requestStream, bufferSize, progress.CancellationToken);
                                    contentLen = fileStream.Position - contentLen;
                                    progress.Current += contentLen;
                                }
                            }

                            await requestStream.FlushAsync();
                        }

                        request.Abort();
                    }
                    catch (WebException ex)
                    {
                        progress.OnError?.Invoke(ex);
                        request.Abort();
                        return false;
                    }
                }

                progress.OnComplete?.Invoke();
                return true;
            }
        }
    }
}