// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
//
// public partial class AHelper
// {
//     /// <summary>
//     /// 网络 工具类
//     /// </summary>
//     public partial class Net
//     {
//         public partial class FTP
//         {
//             /// <summary>
//             /// FTP下载文件
//             /// </summary>
//             /// <param name="uri">路径</param>
//             /// <param name="username">用户名</param>
//             /// <param name="password">密码</param>
//             /// <param name="localPath">本地文件路径</param>
//             /// <param name="iEvent">回调</param>
//             /// <param name="overwrite">是否覆盖</param>
//             /// <param name="timeout">超时</param>
//             /// <param name="bufferSize">下载缓存大小</param>
//             public static bool DownloadFile(string uri, string username, string password,
//                 string localPath,
//                 IProgressEvent iEvent = null,
//                 bool overwrite = false,
//                 ushort timeout = TIMEOUT,
//                 int bufferSize = BUFFER_SIZE)
//             {
//                 var fileSize = GetFileSize(uri, username, password);
//                 if (fileSize <= 0) return false;
//                 if (File.Exists(localPath) && !overwrite) return true;
//                 var progress = new AProgress(iEvent);
//                 try
//                 {
//                     var request = CreateRequestFile(uri, username, password, "RETR", timeout);
//                     using var response = request.GetResponse();
//                     progress.Total = fileSize;
//                     progress.CurrentInfo = response.ResponseUri.AbsoluteUri;
//
//                     using (var responseStream = response.GetResponseStream())
//                     {
//                         if (responseStream is null) throw new Exception("FTP Stream is Null");
//                         var buffer = new byte[bufferSize];
//                         using (var outputStream = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.Write))
//                         {
//                             var readCount = responseStream.Read(buffer, 0, bufferSize);
//                             while (readCount > 0)
//                             {
//                                 progress.Current += readCount;
//                                 outputStream.Write(buffer, 0, readCount);
//                                 readCount = responseStream.Read(buffer, 0, bufferSize);
//                             }
//
//                             outputStream.Flush();
//                         }
//                     }
//
//                     request.Abort();
//                     progress.Finish();
//                     return true;
//                 }
//                 catch (WebException ex)
//                 {
//                     File.Delete(localPath);
//                     progress.Error(ex);
//                     return false;
//                 }
//             }
//
//             /// <summary>
//             /// FTP下载文件
//             /// </summary>
//             /// <param name="uri">路径</param>
//             /// <param name="username">用户名</param>
//             /// <param name="password">密码</param>
//             /// <param name="localPath">本地文件路径</param>
//             /// <param name="iEvent">回调</param>
//             /// <param name="overwrite">是否覆盖</param>
//             /// <param name="timeout">超时</param>
//             /// <param name="bufferSize">下载缓存大小</param>
//             public static async Task<bool> DownloadFileAsync(string uri, string username, string password,
//                 string localPath,
//                 IProgressEvent iEvent = null,
//                 bool overwrite = false,
//                 ushort timeout = TIMEOUT,
//                 int bufferSize = BUFFER_SIZE)
//             {
//                 var fileSize = await GetFileSizeAsync(uri, username, password);
//                 if (fileSize <= 0) return false;
//                 if (File.Exists(localPath) && !overwrite) return true;
//                 var progress = new AProgress(iEvent);
//                 try
//                 {
//                     var request = CreateRequestFile(uri, username, password, "RETR", timeout);
//                     using var response = await request.GetResponseAsync();
//                     progress.Total = fileSize;
//                     progress.CurrentInfo = response.ResponseUri.AbsoluteUri;
//
//                     using (var responseStream = response.GetResponseStream())
//                     {
//                         if (responseStream is null) throw new Exception("FTP Stream is Null");
//                         using (var outputStream = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.Write))
//                         {
//                             var contentLen = 0L;
//                             while (outputStream.Position < fileSize)
//                             {
//                                 await responseStream.CopyToAsync(outputStream, bufferSize);
//                                 contentLen = outputStream.Position - contentLen;
//                                 progress.Current += contentLen;
//                             }
//                         }
//                     }
//
//                     request.Abort();
//                     progress.Finish();
//                     return true;
//                 }
//                 catch (WebException ex)
//                 {
//                     File.Delete(localPath);
//                     progress.Error(ex);
//                     return false;
//                 }
//             }
//
//             /// <summary>
//             /// FTP下载文件夹
//             /// </summary>
//             /// <param name="uri">路径</param>
//             /// <param name="username">用户名</param>
//             /// <param name="password">密码</param>
//             /// <param name="localPath">本地文件路径</param>
//             /// <param name="iEvent">回调</param>
//             /// <param name="pattern">搜索过滤</param>
//             /// <param name="option">搜索模式</param>
//             /// <param name="overwrite">是否覆盖</param>
//             /// <param name="timeout">超时</param>
//             /// <param name="bufferSize">下载缓存大小</param>
//             public static bool DownloadDir(string uri, string username, string password,
//                 string localPath,
//                 IProgressEvent iEvent = null,
//                 SearchOption option = SearchOption.AllDirectories,
//                 string pattern = "*",
//                 bool overwrite = false,
//                 ushort timeout = TIMEOUT,
//                 int bufferSize = BUFFER_SIZE
//             )
//             {
//                 var remoteList = GetDownloadList(uri, username, password, option, pattern, timeout);
//                 var dict = new Dictionary<string, long>();
//                 var progress = new AProgress(iEvent);
//                 foreach (var remoteAbs in remoteList)
//                 {
//                     if (progress.State == ProgressState.Cancel)
//                     {
//                         progress.Error(new Exception("User Cancel"));
//                         return false;
//                     }
//
//                     var remote = string.Concat(uri, '/', remoteAbs);
//                     var fileSize = GetFileSize(remote, username, password, timeout);
//                     if (fileSize <= 0) continue;
//                     progress.Total += fileSize;
//                     dict.Add(remoteAbs, fileSize);
//                 }
//
//                 foreach (var item in dict)
//                 {
//                     if (progress.State == ProgressState.Cancel)
//                     {
//                         progress.Error(new Exception("User Cancel"));
//                         return false;
//                     }
//
//                     try
//                     {
//                         var remote = string.Concat(uri, '/', item.Key);
//                         var request = CreateRequestDir(remote, username, password, "RETR", timeout);
//
//                         progress.CurrentInfo = request.RequestUri.AbsoluteUri;
//
//                         using var response = request.GetResponse();
//                         using (var responseStream = response.GetResponseStream())
//                         {
//                             if (responseStream is null) throw new Exception("FTP Stream is Null");
//
//                             var local = Path.Combine(localPath, item.Key).Replace('\\', '/');
//                             var localDir = new DirectoryInfo(local.Substring(0, local.LastIndexOf('/')));
//
//                             if (!localDir.Exists) Directory.CreateDirectory(localDir.FullName);
//                             else if (File.Exists(local) && !overwrite) continue;
//
//                             using (var outputStream = new FileStream(local, FileMode.OpenOrCreate, FileAccess.Write))
//                             {
//                                 var contentLen = 0L;
//                                 while (outputStream.Position < item.Value)
//                                 {
//                                     responseStream.CopyTo(outputStream, bufferSize);
//                                     contentLen = outputStream.Position - contentLen;
//                                     progress.Current += contentLen;
//                                 }
//
//                                 outputStream.Flush();
//                             }
//                         }
//
//
//                         request.Abort();
//                     }
//                     catch (WebException ex)
//                     {
//                         File.Delete(localPath);
//                         progress.Error(ex);
//                         return false;
//                     }
//                 }
//
//                 progress.Finish();
//                 return true;
//             }
//
//             /// <summary>
//             /// FTP下载文件夹
//             /// </summary>
//             /// <param name="uri">路径</param>
//             /// <param name="username">用户名</param>
//             /// <param name="password">密码</param>
//             /// <param name="localPath">本地文件路径</param>
//             /// <param name="iEvent">回调</param>
//             /// <param name="pattern">搜索过滤</param>
//             /// <param name="option">搜索模式</param>
//             /// <param name="overwrite">是否覆盖</param>
//             /// <param name="timeout">超时</param>
//             /// <param name="bufferSize">下载缓存大小</param>
//             public static async Task<bool> DownloadDirAsync(string uri, string username, string password,
//                 string localPath,
//                 IProgressEvent iEvent = default,
//                 SearchOption option = SearchOption.AllDirectories,
//                 string pattern = "*",
//                 bool overwrite = false,
//                 ushort timeout = TIMEOUT,
//                 int bufferSize = BUFFER_SIZE
//             )
//             {
//                 var remoteList =
//                     await GetDownloadListAsync(uri, username, password, option, pattern, timeout);
//                 var dict = new Dictionary<string, long>();
//                 var progress = new AProgress(iEvent);
//                 foreach (var remoteAbs in remoteList)
//                 {
//                     if (progress.State == ProgressState.Cancel)
//                     {
//                         progress.Error(new Exception("User Cancel"));
//                         return false;
//                     }
//
//                     var remote = string.Concat(uri, '/', remoteAbs);
//                     var fileSize = await GetFileSizeAsync(remote, username, password, timeout);
//                     if (fileSize <= 0) continue;
//                     progress.Total += fileSize;
//                     dict.Add(remoteAbs, fileSize);
//                 }
//
//                 foreach (var item in dict)
//                 {
//                     if (progress.State == ProgressState.Cancel)
//                     {
//                         progress.Error(new Exception("User Cancel"));
//                         return false;
//                     }
//
//                     try
//                     {
//                         var remote = string.Concat(uri, '/', item.Key);
//                         var request = CreateRequestDir(remote, username, password, "RETR", timeout);
//                         progress.CurrentInfo = request.RequestUri.AbsoluteUri;
//
//
//                         using var response = await request.GetResponseAsync();
//                         using (var responseStream = response.GetResponseStream())
//                         {
//                             if (responseStream is null) throw new Exception("FTP Stream is Null");
//
//                             var local = Path.Combine(localPath, item.Key).Replace('\\', '/');
//                             var localDir = new DirectoryInfo(local.Substring(0, local.LastIndexOf('/')));
//                             if (!localDir.Exists) Directory.CreateDirectory(localDir.FullName);
//                             else if (File.Exists(local) && !overwrite) continue;
//
//                             using (var outputStream = new FileStream(local, FileMode.OpenOrCreate, FileAccess.Write))
//                             {
//                                 var contentLen = 0L;
//                                 while (outputStream.Position < item.Value)
//                                 {
//                                     await responseStream.CopyToAsync(outputStream, bufferSize);
//                                     contentLen = outputStream.Position - contentLen;
//                                     progress.Current += contentLen;
//                                 }
//
//                                 await outputStream.FlushAsync();
//                             }
//                         }
//
//                         request.Abort();
//                     }
//                     catch (WebException ex)
//                     {
//                         File.Delete(localPath);
//                         progress.Error(ex);
//                         return false;
//                     }
//                 }
//
//                 progress.Finish();
//                 return true;
//             }
//
//             /// <summary>
//             /// 获取文件下载列表
//             /// </summary>
//             private static IEnumerable<string> GetDownloadList(string uri, string username, string password,
//                 SearchOption option = SearchOption.AllDirectories,
//                 string searchPattern = "*",
//                 ushort timeout = TIMEOUT, string abs = null
//             )
//             {
//                 var remoteList =
//                     GetRemoteList(uri, username, password, AHandle.FTP.ListType.File, searchPattern, timeout)
//                         .Select(file => string.Concat(abs, '/', Path.GetFileName(file)).Trim('/'))
//                         .ToList();
//
//                 if (option != SearchOption.AllDirectories) return remoteList;
//
//                 foreach (var absPath in GetRemoteList(
//                                  uri, username, password, AHandle.FTP.ListType.Directory, searchPattern, timeout)
//                              .Select(Path.GetFileName))
//                 {
//                     var collection = GetDownloadList(
//                         string.Concat(uri, '/', absPath), username, password, option, searchPattern, timeout,
//                         string.Concat(abs, '/', absPath).Trim('/'));
//                     remoteList.AddRange(collection);
//                 }
//
//                 return remoteList;
//             }
//
//
//             /// <summary>
//             /// 获取文件下载列表
//             /// </summary>
//             private static async Task<IEnumerable<string>> GetDownloadListAsync(string uri, string user,
//                 string pass,
//                 SearchOption option = SearchOption.AllDirectories,
//                 string pattern = "*",
//                 ushort timeout = TIMEOUT, string abs = null
//             )
//             {
//                 var remoteList =
//                     (await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.File, pattern, timeout))
//                     .Select(file => string.Concat(abs, '/', Path.GetFileName(file)).Trim('/'))
//                     .ToList();
//
//                 if (option != SearchOption.AllDirectories) return remoteList;
//
//                 foreach (var absPath in (await GetRemoteListAsync(
//                              uri, user, pass, AHandle.FTP.ListType.Directory, pattern, timeout))
//                          .Select(Path.GetFileName))
//                 {
//                     var collection = GetDownloadList(
//                         string.Concat(uri, '/', absPath), user, pass, option, pattern, timeout,
//                         string.Concat(abs, '/', absPath).Trim('/'));
//                     remoteList.AddRange(collection);
//                 }
//
//                 return remoteList;
//             }
//         }
//     }
// }