using System;
using System.Collections.Generic;
using System.IO;

namespace AIO
{
    public partial class AHandle
    {
        /// <summary>
        /// FTP 处理器
        /// </summary>
        public sealed partial class FTP : IDisposable
        {
            /// <summary>
            /// 创建HTTP处理器
            /// </summary>
            /// <param name="serverIP">服务器IP</param>
            /// <param name="userName">用户名</param>
            /// <param name="pass">密码</param>
            /// <returns>处理器</returns>
            public static FTP Create(string serverIP, string userName, string pass)
            {
                return new FTP(serverIP, userName, pass, string.Empty);
            }

            /// <summary>
            /// 创建HTTP处理器
            /// </summary>
            /// <param name="serverIP">服务器IP</param>
            /// <param name="userName">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="remotePath">远端默认跟文件夹</param>
            /// <returns>处理器</returns>
            public static FTP Create(string serverIP, string userName, string pass, string remotePath)
            {
                return new FTP(serverIP, userName, pass, remotePath);
            }

            /// <summary>
            /// 创建HTTP处理器
            /// </summary>
            /// <param name="serverIP">服务器IP</param>
            /// <param name="port">端口</param>
            /// <param name="userName">用户名</param>
            /// <param name="pass">密码</param>
            /// <returns>处理器</returns>
            public static FTP Create(string serverIP, int port, string userName, string pass)
            {
                return new FTP(serverIP, userName, pass, string.Empty, port);
            }

            /// <summary>
            /// 创建HTTP处理器
            /// </summary>
            /// <param name="serverIP">服务器IP</param>
            /// <param name="port">端口</param>
            /// <param name="userName">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="remotePath">远端默认跟文件夹</param>
            /// <returns>处理器</returns>
            public static FTP Create(string serverIP, int port, string userName, string pass, string remotePath)
            {
                return new FTP(serverIP, userName, pass, remotePath, port);
            }

            /// <summary>
            /// 获取列表类型
            /// </summary>
            public enum ListType
            {
                /// <summary>
                /// 文件
                /// </summary>
                File = 0,

                /// <summary>
                /// 文件夹
                /// </summary>
                Directory = 1,

                /// <summary>
                /// 文件和文件夹
                /// </summary>
                ALL = 2
            }

            /// <summary>
            /// 指定FTP连接成功后的当前目录, 如果不指定即默认为根目录
            /// </summary>
            public string URI { get; private set; }

            /// <summary>
            /// FTP服务器IP地址
            /// </summary>
            public string Server { get; private set; }

            /// <summary>
            /// FTP服务器端口
            /// </summary>
            public int Port { get; private set; }

            /// <summary>
            /// FTP用户名
            /// </summary>
            public string User { get; private set; }

            /// <summary>
            /// FTP密码
            /// </summary>
            public string Pass { get; private set; }

            /// <summary>
            /// FTP服务器上的目录
            /// </summary>
            public string Absolute { get; private set; }

            /// <summary>
            /// 超时时间
            /// </summary>
            public ushort TimeOut { get; set; } = AHelper.Net.TIMEOUT;

            /// <summary>
            /// 缓存大小
            /// </summary>
            public int BufferSize { get; set; } = AHelper.Net.BUFFER_SIZE;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="server">服务器IP</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="absolute">远端默认跟文件夹</param>
            /// <param name="port">端口</param>
            public FTP(string server, string user, string pass, string absolute, int port = 21)
            {
                Server = server;
                User = user;
                Pass = pass;
                Port = port;
                if (string.IsNullOrEmpty(absolute))
                {
                    URI = string.Concat("ftp://", Server, ':', Port);
                }
                else
                {
                    Absolute = absolute.Replace('\\', '/').Trim('/', '\\');
                    URI = string.Concat("ftp://", Server, ':', Port, '/', Absolute);
                }
            }

            /// <summary>
            /// 初始化
            /// </summary>
            public bool Init()
            {
                var remote = string.Concat(Server, ':', Port);
                if (string.IsNullOrEmpty(Absolute))
                    return AHelper.FTP.CheckDir(remote, User, Pass, TimeOut) ||
                           AHelper.FTP.CreateDir(remote, User, Pass, TimeOut);

                foreach (var item in Absolute.Split('/'))
                {
                    remote = string.Concat(remote, '/', item);
                    if (AHelper.FTP.CheckDir(remote, User, Pass, TimeOut)) continue;
                    if (AHelper.FTP.CreateDir(remote, User, Pass, TimeOut)) continue;
                    return false;
                }

                return true;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return string.Concat("ServerIP: ", Server, " UserName: ", User, " Password: ", Pass,
                    " RemotePath: ", Absolute);
            }

            /// <summary>
            /// 移动文件
            /// </summary>
            /// <param name="currentRemotePath">当前远端路径</param>
            /// <param name="newRemoteName">新远端路径</param>
            public bool Move(string currentRemotePath, string newRemoteName)
            {
                return AHelper.FTP.ReName(URI, User, Pass, currentRemotePath, newRemoteName);
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="remotePath">远端文件路径</param>
            public bool DeleteFile(string remotePath)
            {
                return AHelper.FTP.DeleteFile(string.Concat(URI, '/', remotePath), User, Pass);
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            /// <param name="remotePath">远端文件夹路径</param>
            public bool DeleteDir(string remotePath = null)
            {
                return AHelper.FTP.DeleteDir(string.Concat(URI, '/', remotePath), User, Pass);
            }

            /// <summary>
            /// 创建文件夹
            /// </summary>
            /// <param name="remotePath">远端文件夹路径</param>
            public bool CreateDir(string remotePath = null)
            {
                return AHelper.FTP.CreateDir(string.Concat(URI, '/', remotePath), User, Pass);
            }

            /// <summary>
            /// 上传文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="remotePath">远端路径</param>
            /// <param name="iEvent">回调</param>
            public bool UploadFile(string localPath, string remotePath, IProgressEvent iEvent = null)
            {
                var remote = string.Concat(URI, '/', remotePath);
                var handler = AHelper.FTP.UploadFile(remote, User, Pass, localPath, TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 上传文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="iEvent">回调</param>
            public bool UploadFile(string localPath, IProgressEvent iEvent = null)
            {
                var handler = AHelper.FTP.UploadFile(URI, User, Pass, localPath, TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 上传文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="remotePath">远端路径</param>
            /// <param name="iEvent">回调</param>
            /// <param name="searchPattern">搜索字段</param>
            /// <param name="searchOption">搜索模式</param>
            public bool UploadDir(string localPath, string remotePath,
                IProgressEvent iEvent = null,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*")
            {
                var remote = string.Concat(URI, '/', remotePath);
                var handler = AHelper.FTP.UploadDir(remote, User, Pass,
                    localPath, searchOption, searchPattern, TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 上传文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="iEvent">回调</param>
            /// <param name="searchPattern">搜索字段</param>
            /// <param name="searchOption">搜索模式</param>
            public bool UploadDir(string localPath,
                IProgressEvent iEvent = null,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*")
            {
                var handler = AHelper.FTP.UploadDir(URI, User, Pass,
                    localPath, searchOption, searchPattern, TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="remotePath">远端路径</param>
            /// <param name="iEvent">回调</param>
            /// <param name="isOverWrite">是否重写</param>
            public bool DownloadFile(string localPath, string remotePath, IProgressEvent iEvent = null,
                bool isOverWrite = false)
            {
                var handler = AHelper.FTP.DownloadFile(string.Concat(URI, '/', remotePath), User, Pass,
                    localPath, isOverWrite,
                    TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="remotePath">远端路径</param>
            /// <param name="iEvent">回调</param>
            /// <param name="searchPattern">搜索字段</param>
            /// <param name="searchOption">搜索模式</param>
            /// <param name="isOverWrite">是否重写</param>
            public bool DownloadDir(
                string localPath,
                string remotePath,
                IProgressEvent iEvent = null,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*",
                bool isOverWrite = false)
            {
                var handler = AHelper.FTP.DownloadDir(string.Concat(URI, '/', remotePath), User, Pass,
                    localPath, searchOption, searchPattern, isOverWrite,
                    TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="localPath">本地文件</param>
            /// <param name="iEvent">回调</param>
            /// <param name="searchPattern">搜索字段</param>
            /// <param name="searchOption">搜索模式</param>
            /// <param name="isOverWrite">是否重写</param>
            public bool DownloadDir(
                string localPath,
                IProgressEvent iEvent = null,
                SearchOption searchOption = SearchOption.AllDirectories,
                string searchPattern = "*",
                bool isOverWrite = false)
            {
                var handler = AHelper.FTP.DownloadDir(URI, User, Pass,
                    localPath, searchOption, searchPattern, isOverWrite,
                    TimeOut, BufferSize);
                handler.Event = iEvent;
                handler.Begin();
                handler.Wait();
                return handler.Report.State == EProgressState.Finish;
            }

            /// <summary>
            /// 获取文件大小
            /// </summary>
            /// <param name="remotePath">远端路径</param>
            /// <returns>文件大小</returns>
            public long GetFileSize(string remotePath)
            {
                return AHelper.FTP.GetFileSize(string.Concat(URI, '/', remotePath), User, Pass);
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="remotePath">远端路径</param>
            /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
            /// <returns>文件列表</returns>
            public List<string> GetList(string remotePath = null, string keyword = null)
            {
                return AHelper.FTP.GetRemoteList(string.Concat(URI, '/', remotePath), User, Pass,
                    keyword, TimeOut);
            }

            /// <summary>
            /// 获取文件列表
            /// </summary>
            /// <param name="remotePath">远端路径</param>
            /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
            /// <returns>文件列表</returns>
            public List<string> GetListFile(string remotePath = null, string keyword = null)
            {
                return AHelper.FTP.GetRemoteListFile(string.Concat(URI, '/', remotePath), User, Pass,
                    keyword, TimeOut);
            }

            /// <summary>
            /// 检查FTP是否有效
            /// </summary>
            /// <returns>Ture:有效 False:无效</returns>
            public bool Check(string remotePath = null)
            {
                return AHelper.FTP.Check(string.Concat(URI, '/', remotePath), User, Pass, TimeOut);
            }

            /// <summary>
            /// 检查文件是否有效
            /// </summary>
            /// <returns>Ture:有效 False:无效</returns>
            public bool CheckFile(string remotePath = null)
            {
                return AHelper.FTP.CheckFile(string.Concat(URI, '/', remotePath), User, Pass, TimeOut);
            }

            /// <summary>
            /// 检查文件夹是否有效
            /// </summary>
            /// <returns>Ture:有效 False:无效</returns>
            public bool CheckDir(string remotePath = null)
            {
                return AHelper.FTP.CheckDir(string.Concat(URI, '/', remotePath), User, Pass, TimeOut);
            }

            /// <summary>
            /// 释放
            /// </summary>
            public void Dispose()
            {
                Server = null;
                User = null;
                Pass = null;
                Absolute = null;
                URI = null;
            }
        }
    }
}