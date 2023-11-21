using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// 获取FTP文件大小
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>大小</returns>
            /// <exception cref="Exception"></exception>
            public static long GetFileSize(string uri, string user, string pass, ushort timeout = TIMEOUT)
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
            /// <returns>大小</returns>
            /// <exception cref="Exception"></exception>
            public static async Task<long> GetFileSizeAsync(string uri, string user, string pass,
                ushort timeout = TIMEOUT)
            {
                long fileSize;
                try
                {
                    var ftp = CreateRequestFile(uri, user, pass, "SIZE", timeout);
                    using var response = await ftp.GetResponseAsync();
                    fileSize = response.ContentLength;
                    ftp.Abort();
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
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
                ushort timeout = TIMEOUT
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
                ushort timeout = TIMEOUT
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
                ushort timeout = TIMEOUT
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
                ushort timeout = TIMEOUT
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
            public static async Task<List<string>> GetRemoteListDirAsync(string uri, string user, string pass,
                string keyword = null,
                ushort timeout = TIMEOUT
            )
            {
                return await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.Directory, keyword,
                    timeout);
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
                ushort timeout = TIMEOUT
            )
            {
                return await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.ALL, keyword, timeout);
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
            private static async Task<List<string>> GetRemoteListAsync(string uri, string user, string pass,
                AHandle.FTP.ListType type, string keyword, ushort timeout)
            {
                var infos = new List<string>();
                try
                {
                    var request = CreateRequestDir(uri, user, pass, "LIST", timeout);
                    using var response = await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) throw new Exception("FTP Stream is Null");
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

                        line = await reader.ReadLineAsync();
                    }

                    request.Abort();
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
                }

                return infos;
            }
        }
    }
}