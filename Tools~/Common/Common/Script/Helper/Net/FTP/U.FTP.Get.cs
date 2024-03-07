using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AIO;

namespace AIO
{
    public partial class AHelper
    {
        public partial class FTP
        {
            /// <summary>
            /// 获取FTP文本内容
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public static string GetText(string uri, string user, string pass, ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var request = CreateRequestFile(uri, user, pass, "RETR", timeout);
                    using var response = request.GetResponse();
                    using var stream = response.GetResponseStream();
                    if (stream is null) throw new Exception("FTP Stream is Null");
                    using var reader = new StreamReader(stream);
                    var text = reader.ReadToEnd();
                    request.Abort();
                    return text;
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            /// <summary>
            /// 获取FTP文本内容
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <param name="cancellationToken">取消令牌</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public static async Task<string> GetTextAsync(string uri, string user, string pass,
                ushort timeout = Net.TIMEOUT,
                CancellationToken cancellationToken = default)
            {
                try
                {
                    var request = CreateRequestFile(uri, user, pass, "RETR", timeout, cancellationToken);
                    using var response = await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) throw new Exception("FTP Stream is Null");
                    using var reader = new StreamReader(stream);
                    var text = await reader.ReadToEndAsync();
                    request.Abort();
                    return text;
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            /// <summary>
            /// 获取FTP文件MD5
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>MD5</returns>
            public static string GetMD5(string uri, string user, string pass, ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var request = CreateRequestFile(uri, user, pass, "RETR", timeout);
                    using var response = request.GetResponse();
                    return response.GetResponseStream().GetMD5();
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{1} FTP GetMD5: {0}", ex.Message, uri);
#endif
                }

                return string.Empty;
            }

            /// <summary>
            /// 获取FTP文件MD5
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <param name="cancellationToken">取消令牌</param>
            /// <returns>MD5</returns>
            public static async Task<string> GetMD5Async(string uri, string user, string pass,
                ushort timeout = Net.TIMEOUT,
                CancellationToken cancellationToken = default)
            {
                if (cancellationToken == default) cancellationToken = CancellationToken.None;
                try
                {
                    var request = CreateRequestFile(uri, user, pass, "RETR", timeout);
                    using var response = await request.GetResponseAsync();
                    return await response.GetResponseStream().GetMD5Async(cancellationToken: cancellationToken);
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{1} GetMD5: {0}", ex.Message, uri);
#endif
                }

                return string.Empty;
            }

            /// <summary>
            /// 获取FTP文件大小
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>大小</returns>
            /// <exception cref="Exception"></exception>
            public static long GetFileSize(string uri, string user, string pass, ushort timeout = Net.TIMEOUT)
            {
                long fileSize;
                try
                {
                    var request = CreateRequestFile(uri, user, pass, "SIZE", timeout);
                    using var response = request.GetResponse();
                    fileSize = response.ContentLength;
                    request.Abort();
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{1} GetFileSize: {0}", ex.Message, uri);
#endif
                    fileSize = -1;
                }

                return fileSize;
            }

            /// <summary>
            /// 获取FTP文件大小
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <param name="cancellationToken">取消令牌</param>
            /// <returns>大小</returns>
            /// <exception cref="Exception"></exception>
            public static async Task<long> GetFileSizeAsync(string uri, string user, string pass,
                ushort timeout = Net.TIMEOUT,
                CancellationToken cancellationToken = default)
            {
                long fileSize;
                try
                {
                    var request = CreateRequestFile(uri, user, pass, "SIZE", timeout, cancellationToken);
                    using var response = await request.GetResponseAsync();
                    fileSize = response.ContentLength;
                    request.Abort();
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }

                return fileSize;
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static List<string> GetRemoteListFile(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = Net.TIMEOUT
            )
            {
                return GetRemoteList(uri, user, pass, AHandle.FTP.ListType.File, keyword, timeout);
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static List<string> GetRemoteListDir(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = Net.TIMEOUT
            )
            {
                return GetRemoteList(uri, user, pass, AHandle.FTP.ListType.Directory, keyword, timeout);
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static List<string> GetRemoteList(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = Net.TIMEOUT
            )
            {
                return GetRemoteList(uri, user, pass, AHandle.FTP.ListType.ALL, keyword, timeout);
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="type">
            /// 1:获取文件列表
            /// 2:获取文件夹列表
            /// 3:获取文件和文件夹列表
            /// </param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="WebException"></exception>
            private static List<string> GetRemoteList(string uri, string user, string pass,
                AHandle.FTP.ListType type, string keyword, ushort timeout)
            {
                var infos = new List<string>();
                try
                {
                    var request = CreateRequestDir(uri, user, pass, "LIST", timeout);
                    using var response = request.GetResponse();
                    using var stream = response.GetResponseStream();
                    if (stream is null) throw new Exception("FTP Stream is Null");
                    using var reader = new StreamReader(stream); //中文文件名
                    var line = reader.ReadLine();
                    keyword = keyword?.Trim();
                    while (line != null)
                    {
                        switch (type)
                        {
                            case AHandle.FTP.ListType.File:
                            {
                                if (line.StartsWith("d")) break;
                                if (string.IsNullOrEmpty(keyword)
                                    || keyword == "*"
                                    || line.IndexOf(keyword, StringComparison.CurrentCulture) > -1)
                                    infos.Add(line.Split(' ').Last());
                                break;
                            }
                            case AHandle.FTP.ListType.Directory:
                            {
                                if (!line.StartsWith("d")) break;
                                if (string.IsNullOrEmpty(keyword)
                                    || keyword == "*"
                                    || line.IndexOf(keyword, StringComparison.CurrentCulture) > -1)
                                    infos.Add(line.Split(' ').Last());
                                break;
                            }
                            default:
                            case AHandle.FTP.ListType.ALL:
                                infos.Add(line.Split(' ').Last());
                                break;
                        }

                        line = reader.ReadLine();
                    }

                    request.Abort();
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
                }

                return infos;
            }


            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task<List<string>> GetRemoteListFileAsync(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = Net.TIMEOUT
            )
            {
                return await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.File, keyword, timeout);
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task<List<string>> GetRemoteListDirAsync(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = Net.TIMEOUT
            )
            {
                return GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.Directory, keyword, timeout);
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task<List<string>> GetRemoteListAsync(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = Net.TIMEOUT
            )
            {
                return await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.ALL, keyword, timeout);
            }

            /// <summary>
            /// 获取文件下载列表
            /// </summary>
            private static async Task<IEnumerable<string>> GetDownloadListAsync(string uri, string user, string pass,
                SearchOption option = SearchOption.AllDirectories,
                string pattern = "*",
                ushort timeout = Net.TIMEOUT, string abs = null, CancellationToken cancellationToken = default
            )
            {
                var remoteList =
                    (await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.File, pattern, timeout,
                        cancellationToken))
                    .Select(file => string.Concat(abs, '/', Path.GetFileName(file)).Trim('/'))
                    .ToList();

                if (option != SearchOption.AllDirectories) return remoteList;

                foreach (var absPath in (await GetRemoteListAsync(
                             uri, user, pass, AHandle.FTP.ListType.Directory, pattern, timeout, cancellationToken))
                         .Select(Path.GetFileName))
                {
                    var collection = GetDownloadList(
                        string.Concat(uri, '/', absPath), user, pass, option, pattern, timeout,
                        string.Concat(abs, '/', absPath).Trim('/'));
                    remoteList.AddRange(collection);
                }

                return remoteList;
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="type">
            /// 1:获取文件列表
            /// 2:获取文件夹列表
            /// 3:获取文件和文件夹列表
            /// </param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <param name="cancellationToken">取消令牌</param>
            /// <returns></returns>
            /// <exception cref="WebException"></exception>
            private static async Task<List<string>> GetRemoteListAsync(string uri, string user, string pass,
                AHandle.FTP.ListType type, string keyword, ushort timeout,
                CancellationToken cancellationToken = default)
            {
                var infos = new List<string>();
                var request = CreateRequestDir(uri.TrimEnd('/'), user, pass, "LIST", timeout, cancellationToken);
                try
                {
                    using var response = await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) throw new WebException("ftp stream is null");
                    using var reader = new StreamReader(stream); //中文文件名
                    var line = await reader.ReadLineAsync();
                    keyword = keyword?.Trim();
                    while (line != null)
                    {
                        switch (type)
                        {
                            case AHandle.FTP.ListType.File:
                            {
                                if (line.StartsWith("d")) break;
                                if (string.IsNullOrEmpty(keyword)
                                    || keyword == "*"
                                    || line.IndexOf(keyword, StringComparison.CurrentCulture) > -1)
                                {
                                    var temp = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    var item = string.Empty;
                                    for (var index = 8; index < temp.Length; index++)
                                        item = string.Concat(item, " ", temp[index]);

                                    infos.Add(item.Trim(' '));
                                }

                                break;
                            }
                            case AHandle.FTP.ListType.Directory:
                            {
                                if (!line.StartsWith("d")) break;
                                if (string.IsNullOrEmpty(keyword)
                                    || keyword == "*"
                                    || line.IndexOf(keyword, StringComparison.CurrentCulture) > -1)
                                {
                                    var temp = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    var item = string.Empty;
                                    for (var index = 8; index < temp.Length; index++)
                                        item = string.Concat(item, " ", temp[index]);

                                    infos.Add(item.Trim(' '));
                                }

                                break;
                            }
                            default:
                            case AHandle.FTP.ListType.ALL:
                            {
                                var temp = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                var item = string.Empty;
                                for (var index = 8; index < temp.Length; index++)
                                    item = string.Concat(item, " ", temp[index]);

                                infos.Add(item.Trim(' '));
                                break;
                            }
                        }

                        line = await reader.ReadLineAsync();
                    }

                    request.Abort();
                }
                catch (WebException)
                {
                    request.Abort();
                    return infos;
                }

                return infos;
            }
        }
    }
}