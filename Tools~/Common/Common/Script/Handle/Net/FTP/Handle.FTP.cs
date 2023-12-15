// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Threading.Tasks;
//
// public partial class AHandle
// {
//     /// <summary>
//     /// FTP 处理器
//     /// </summary>
//     public sealed class FTP : IDisposable
//     {
//         /// <summary>
//         /// 创建HTTP处理器
//         /// </summary>
//         /// <param name="serverIP">服务器IP</param>
//         /// <param name="userName">用户名</param>
//         /// <param name="password">密码</param>
//         /// <param name="remotePath">远端默认跟文件夹</param>
//         /// <returns>处理器</returns>
//         public static FTP Create(string serverIP, string userName, string password, string remotePath = null)
//         {
//             return new FTP(serverIP, userName, password, remotePath);
//         }
//
//         /// <summary>
//         /// 获取列表类型
//         /// </summary>
//         public enum ListType
//         {
//             /// <summary>
//             /// 文件
//             /// </summary>
//             File = 0,
//
//             /// <summary>
//             /// 文件夹
//             /// </summary>
//             Directory = 1,
//
//             /// <summary>
//             /// 文件和文件夹
//             /// </summary>
//             ALL = 2
//         }
//
//         /// <summary>
//         /// 指定FTP连接成功后的当前目录, 如果不指定即默认为根目录
//         /// </summary>
//         public string URI { get; private set; }
//
//         /// <summary>
//         /// FTP服务器IP地址
//         /// </summary>
//         public string ServerIP { get; private set; }
//
//         /// <summary>
//         /// FTP用户名
//         /// </summary>
//         public string UserName { get; private set; }
//
//         /// <summary>
//         /// FTP密码
//         /// </summary>
//         public string Password { get; private set; }
//
//         /// <summary>
//         /// FTP服务器上的目录
//         /// </summary>
//         public string RemotePath { get; private set; }
//
//         /// <summary>
//         /// 超时时间
//         /// </summary>
//         public ushort TimeOut { get; set; } = AHelper.Net.TIMEOUT;
//
//         /// <summary>
//         /// 缓存大小
//         /// </summary>
//         public int BufferSize { get; set; } = AHelper.BUFFER_SIZE;
//
//         /// <summary>
//         /// 构造函数
//         /// </summary>
//         /// <param name="serverIP">服务器IP</param>
//         /// <param name="userName">用户名</param>
//         /// <param name="password">密码</param>
//         /// <param name="remotePath">远端默认跟文件夹</param>
//         public FTP(string serverIP, string userName, string password, string remotePath)
//         {
//             ServerIP = serverIP;
//             UserName = userName;
//             Password = password;
//             if (string.IsNullOrEmpty(remotePath))
//             {
//                 URI = string.Concat("ftp://", serverIP);
//             }
//             else
//             {
//                 RemotePath = remotePath;
//                 URI = string.Concat("ftp://", serverIP, '/', remotePath);
//             }
//         }
//
//         /// <summary>
//         /// 初始化
//         /// </summary>
//         public async Task<bool> InitAsync()
//         {
//             if (await AHelper.Net.FTP.CheckDirAsync(URI, UserName, Password, TimeOut)) return true;
//             return await AHelper.Net.FTP.CreateDirAsync(URI, UserName, Password, TimeOut);
//         }
//
//         /// <summary>
//         /// 初始化
//         /// </summary>
//         public bool Init()
//         {
//             return AHelper.Net.FTP.CheckDir(URI, UserName, Password, TimeOut) ||
//                    AHelper.Net.FTP.CreateDir(URI, UserName, Password, TimeOut);
//         }
//
//         /// <inheritdoc />
//         public override string ToString()
//         {
//             return string.Concat("ServerIP: ", ServerIP, " UserName: ", UserName, " Password: ", Password,
//                 " RemotePath: ", RemotePath);
//         }
//
//         /// <summary>
//         /// 移动文件
//         /// </summary>
//         /// <param name="currentRemotePath">当前远端路径</param>
//         /// <param name="newRemoteName">新远端路径</param>
//         public bool Move(string currentRemotePath, string newRemoteName)
//         {
//             return AHelper.Net.FTP.ReName(URI, UserName, Password, currentRemotePath, newRemoteName);
//         }
//
//         /// <summary>
//         /// 移动文件
//         /// </summary>
//         /// <param name="currentRemotePath">当前远端路径</param>
//         /// <param name="newRemoteName">新远端路径</param>
//         public Task<bool> MoveAsync(string currentRemotePath, string newRemoteName)
//         {
//             return AHelper.Net.FTP.ReNameAsync(URI, UserName, Password, currentRemotePath, newRemoteName);
//         }
//
//         /// <summary>
//         /// 删除文件
//         /// </summary>
//         /// <param name="remotePath">远端文件路径</param>
//         public bool DeleteFile(string remotePath)
//         {
//             return AHelper.Net.FTP.DeleteFile(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 删除文件
//         /// </summary>
//         /// <param name="remotePath">远端文件路径</param>
//         public Task<bool> DeleteFileAsync(string remotePath)
//         {
//             return AHelper.Net.FTP.DeleteFileAsync(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 删除文件夹
//         /// </summary>
//         /// <param name="remotePath">远端文件夹路径</param>
//         public bool DeleteDir(string remotePath = null)
//         {
//             return AHelper.Net.FTP.DeleteDir(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 删除文件夹
//         /// </summary>
//         /// <param name="remotePath">远端文件夹路径</param>
//         public Task<bool> DeleteDirAsync(string remotePath = null)
//         {
//             return AHelper.Net.FTP.DeleteDirAsync(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 创建文件夹
//         /// </summary>
//         /// <param name="remotePath">远端文件夹路径</param>
//         public bool CreateDir(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CreateDir(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 创建文件夹
//         /// </summary>
//         /// <param name="remotePath">远端文件夹路径</param>
//         public Task<bool> CreateDirAsync(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CreateDirAsync(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         public bool UploadFile(string localPath, string remotePath, IProgressEvent iEvent = null)
//         {
//             var remote = string.Concat(URI, '/', remotePath);
//             return AHelper.Net.FTP.UploadFile(remote, UserName, Password, localPath, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="iEvent">回调</param>
//         public bool UploadFile(string localPath, IProgressEvent iEvent = null)
//         {
//             return AHelper.Net.FTP.UploadFile(URI, UserName, Password, localPath, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         public Task<bool> UploadFileAsync(string localPath, string remotePath, IProgressEvent iEvent = null)
//         {
//             var remote = string.Concat(URI, '/', remotePath);
//             return AHelper.Net.FTP.UploadFileAsync(remote, UserName, Password, localPath, iEvent, TimeOut,
//                 BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="iEvent">回调</param>
//         public Task<bool> UploadFileAsync(string localPath, IProgressEvent iEvent = null)
//         {
//             return AHelper.Net.FTP.UploadFileAsync(URI, UserName, Password, localPath, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         public bool UploadDir(string localPath, string remotePath,
//             IProgressEvent iEvent = null,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*")
//         {
//             var remote = string.Concat(URI, '/', remotePath);
//             return AHelper.Net.FTP.UploadFolder(remote, UserName, Password,
//                 localPath, searchOption, searchPattern, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         public bool UploadDir(string localPath,
//             IProgressEvent iEvent = null,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*")
//         {
//             return AHelper.Net.FTP.UploadFolder(URI, UserName, Password,
//                 localPath, searchOption, searchPattern, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         public Task<bool> UploadDirAsync(string localPath, string remotePath, IProgressEvent iEvent,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*")
//         {
//             var remote = string.Concat(URI, '/', remotePath);
//             return AHelper.Net.FTP.UploadFolderAsync(remote, UserName, Password, localPath,
//                 searchOption, searchPattern, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 上传文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         public Task<bool> UploadDirAsync(string localPath, IProgressEvent iEvent,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*")
//         {
//             return AHelper.Net.FTP.UploadFolderAsync(URI, UserName, Password, localPath,
//                 searchOption, searchPattern, iEvent, TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 下载文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="isOverWrite">是否重写</param>
//         public bool DownloadFile(string localPath, string remotePath, IProgressEvent iEvent = null,
//             bool isOverWrite = false)
//         {
//             return AHelper.Net.FTP.DownloadFile(string.Concat(URI, '/', remotePath), UserName, Password,
//                 localPath, iEvent, isOverWrite,
//                 TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 下载文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="isOverWrite">是否重写</param>
//         public Task<bool> DownloadFileAsync(string localPath, string remotePath, IProgressEvent iEvent = null,
//             bool isOverWrite = false)
//         {
//             return AHelper.Net.FTP.DownloadFileAsync(string.Concat(URI, '/', remotePath), UserName, Password,
//                 localPath, iEvent, isOverWrite,
//                 TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 下载文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         /// <param name="isOverWrite">是否重写</param>
//         public bool DownloadDir(
//             string localPath,
//             string remotePath,
//             IProgressEvent iEvent = null,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*",
//             bool isOverWrite = false)
//         {
//             return AHelper.Net.FTP.DownloadDir(string.Concat(URI, '/', remotePath), UserName, Password,
//                 localPath, iEvent, searchOption, searchPattern, isOverWrite,
//                 TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 下载文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         /// <param name="isOverWrite">是否重写</param>
//         public Task<bool> DownloadDirAsync(
//             string localPath,
//             string remotePath,
//             IProgressEvent iEvent = null,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*",
//             bool isOverWrite = false)
//         {
//             return AHelper.Net.FTP.DownloadDirAsync(string.Concat(URI, '/', remotePath), UserName, Password,
//                 localPath, iEvent, searchOption, searchPattern, isOverWrite,
//                 TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 下载文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         /// <param name="isOverWrite">是否重写</param>
//         public bool DownloadDir(
//             string localPath,
//             IProgressEvent iEvent = null,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*",
//             bool isOverWrite = false)
//         {
//             return AHelper.Net.FTP.DownloadDir(URI, UserName, Password,
//                 localPath, iEvent, searchOption, searchPattern, isOverWrite,
//                 TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 下载文件
//         /// </summary>
//         /// <param name="localPath">本地文件</param>
//         /// <param name="iEvent">回调</param>
//         /// <param name="searchPattern">搜索字段</param>
//         /// <param name="searchOption">搜索模式</param>
//         /// <param name="isOverWrite">是否重写</param>
//         public Task<bool> DownloadDirAsync(
//             string localPath,
//             IProgressEvent iEvent = null,
//             SearchOption searchOption = SearchOption.AllDirectories,
//             string searchPattern = "*",
//             bool isOverWrite = false)
//         {
//             return AHelper.Net.FTP.DownloadDirAsync(URI, UserName, Password,
//                 localPath, iEvent, searchOption, searchPattern, isOverWrite,
//                 TimeOut, BufferSize);
//         }
//
//         /// <summary>
//         /// 获取文件大小
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <returns>文件大小</returns>
//         public long GetFileSize(string remotePath)
//         {
//             return AHelper.Net.FTP.GetFileSize(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 获取文件大小
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <returns>文件大小</returns>
//         public Task<long> GetFileSizeAsync(string remotePath)
//         {
//             return AHelper.Net.FTP.GetFileSizeAsync(string.Concat(URI, '/', remotePath), UserName, Password);
//         }
//
//         /// <summary>
//         /// 获取文件或文件夹列表
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
//         /// <returns>文件列表</returns>
//         public List<string> GetList(string remotePath = null, string keyword = null)
//         {
//             return AHelper.Net.FTP.GetRemoteList(string.Concat(URI, '/', remotePath), UserName, Password,
//                 keyword, TimeOut);
//         }
//
//         /// <summary>
//         /// 获取文件夹列表
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
//         /// <returns>文件列表</returns>
//         public Task<List<string>> GetListAsync(string remotePath = null, string keyword = null)
//         {
//             return AHelper.Net.FTP.GetRemoteListAsync(string.Concat(URI, '/', remotePath), UserName, Password,
//                 keyword, TimeOut);
//         }
//
//         /// <summary>
//         /// 获取文件列表
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
//         /// <returns>文件列表</returns>
//         public List<string> GetListFile(string remotePath = null, string keyword = null)
//         {
//             return AHelper.Net.FTP.GetRemoteListFile(string.Concat(URI, '/', remotePath), UserName, Password,
//                 keyword, TimeOut);
//         }
//
//         /// <summary>
//         /// 获取目录列表
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
//         /// <returns>文件列表</returns>
//         public Task<List<string>> GetListFileAsync(string remotePath = null, string keyword = null)
//         {
//             return AHelper.Net.FTP.GetRemoteListFileAsync(string.Concat(URI, '/', remotePath), UserName, Password,
//                 keyword, TimeOut);
//         }
//
//         /// <summary>
//         /// 获取文件列表
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
//         /// <returns>文件列表</returns>
//         public List<string> GetListDir(string remotePath = null, string keyword = "")
//         {
//             return AHelper.Net.FTP.GetRemoteListDir(string.Concat(URI, '/', remotePath), UserName, Password,
//                 keyword, TimeOut);
//         }
//
//         /// <summary>
//         /// 获取文件夹列表
//         /// </summary>
//         /// <param name="remotePath">远端路径</param>
//         /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
//         /// <returns>文件列表</returns>
//         public Task<List<string>> GetListDirAsync(string remotePath = null, string keyword = null)
//         {
//             return AHelper.Net.FTP.GetRemoteListDirAsync(string.Concat(URI, '/', remotePath), UserName, Password,
//                 keyword, TimeOut);
//         }
//
//         /// <summary>
//         /// 检查FTP是否有效
//         /// </summary>
//         /// <returns>Ture:有效 False:无效</returns>
//         public bool Check(string remotePath = null)
//         {
//             return AHelper.Net.FTP.Check(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
//         }
//
//         /// <summary>
//         /// 检查FTP是否有效
//         /// </summary>
//         /// <returns>Ture:有效 False:无效</returns>
//         public Task<bool> CheckAsync(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CheckAsync(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
//         }
//
//         /// <summary>
//         /// 检查文件是否有效
//         /// </summary>
//         /// <returns>Ture:有效 False:无效</returns>
//         public bool CheckFile(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CheckFile(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
//         }
//
//         /// <summary>
//         /// 检查文件是否有效
//         /// </summary>
//         /// <returns>Ture:有效 False:无效</returns>
//         public Task<bool> CheckFileAsync(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CheckFileAsync(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
//         }
//
//         /// <summary>
//         /// 检查文件夹是否有效
//         /// </summary>
//         /// <returns>Ture:有效 False:无效</returns>
//         public bool CheckDir(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CheckDir(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
//         }
//
//
//         /// <summary>
//         /// 检查文件夹是否有效
//         /// </summary>
//         /// <returns>Ture:有效 False:无效</returns>
//         public Task<bool> CheckDirAsync(string remotePath = null)
//         {
//             return AHelper.Net.FTP.CheckDirAsync(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
//         }
//
//         /// <summary>
//         /// 释放
//         /// </summary>
//         public void Dispose()
//         {
//             ServerIP = null;
//             UserName = null;
//             Password = null;
//             RemotePath = null;
//             URI = null;
//         }
//     }
// }