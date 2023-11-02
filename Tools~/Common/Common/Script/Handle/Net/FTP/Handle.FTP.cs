using System;
using System.Collections.Generic;
using System.IO;

public partial class AHandle
{
    /// <summary>
    /// FTP 处理器
    /// </summary>
    public sealed class FTP : IDisposable
    {
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
            Folder = 1,

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
        public string ServerIP { get; private set; }

        /// <summary>
        /// FTP用户名
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// FTP密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// FTP服务器上的目录
        /// </summary>
        public string RemotePath { get; private set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public ushort TimeOut { get; set; } = AHelper.Net.TIMEOUT;

        /// <summary>
        /// 缓存大小
        /// </summary>
        public int BufferSize { get; set; } = AHelper.BUFFER_SIZE;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="remotePath">远端默认跟文件夹</param>
        public FTP(string userName, string serverIP, string password, string remotePath)
        {
            ServerIP = serverIP;
            UserName = userName;
            Password = password;
            RemotePath = remotePath;
            URI = string.Concat("ftp://", serverIP + '/', remotePath + '/');
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="currentRemotePath">当前远端路径</param>
        /// <param name="newRemoteName">新远端路径</param>
        public void Move(string currentRemotePath, string newRemoteName)
        {
            AHelper.Net.FTPReName(URI, UserName, Password, currentRemotePath, newRemoteName);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remoteFilePath">远端文件路径</param>
        public void DeleteFile(string remoteFilePath)
        {
            AHelper.Net.FTPDeleteFile(URI, UserName, Password, remoteFilePath);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remoteDirPath">远端文件夹路径</param>
        public void DeleteFolder(string remoteDirPath)
        {
            AHelper.Net.FTPRemoveFolder(URI, UserName, Password, remoteDirPath);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="remoteDirPath">远端文件夹路径</param>
        public void CreateFolder(string remoteDirPath)
        {
            AHelper.Net.FTPCreateDir(URI, UserName, Password, remoteDirPath);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="progress">回调</param>
        public void UploadFile(string localPath, string remotePath, ProgressArgs progress = default)
        {
            AHelper.Net.FTPUploadFile(URI, UserName, Password, localPath, remotePath, progress, TimeOut, BufferSize);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="progress">回调</param>
        /// <param name="searchPattern">搜索字段</param>
        /// <param name="searchOption">搜索模式</param>
        public void UploadFolder(string localPath, string remotePath, ProgressArgs progress = default,
            SearchOption searchOption = SearchOption.TopDirectoryOnly,
            string searchPattern = "*")
        {
            AHelper.Net.FTPUploadFolder(URI, UserName, Password, localPath, remotePath, progress, searchOption,
                searchPattern, TimeOut, BufferSize);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="progress">回调</param>
        /// <param name="isOverWrite">是否重写</param>
        public void DownloadFile(string localPath, string remotePath, ProgressArgs progress = default,
            bool isOverWrite = false)
        {
            AHelper.Net.FTPDownloadFile(URI, UserName, Password,
                localPath, remotePath, progress, isOverWrite,
                TimeOut, BufferSize);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="progress">回调</param>
        /// <param name="searchPattern">搜索字段</param>
        /// <param name="searchOption">搜索模式</param>
        /// <param name="isOverWrite">是否重写</param>
        public void DownloadFolder(
            string localPath,
            string remotePath,
            ProgressArgs progress = default,
            ListType searchOption = ListType.ALL,
            string searchPattern = "*",
            bool isOverWrite = false)
        {
            AHelper.Net.FTPDownloadFolder(URI, UserName, Password,
                localPath, remotePath, progress, searchOption, searchPattern, isOverWrite,
                TimeOut, BufferSize);
        }


        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="remotePath"></param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string remotePath)
        {
            return AHelper.Net.FTPGetFileSize(URI, UserName, Password, remotePath);
        }

        /// <summary>
        /// 获取文件或文件夹列表
        /// </summary>
        /// <param name="type">获取列表类型</param>
        /// <param name="detail">是否获取详细信息</param>
        /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
        /// <returns>文件列表</returns>
        public List<string> GetList(ListType type, bool detail = false, string keyword = "")
        {
            return AHelper.Net.FTPGetRemoteList(URI, UserName, Password, type, detail, keyword);
        }

        /// <summary>
        /// 获取文件或文件夹列表
        /// </summary>
        /// <returns>Ture:有效 False:无效</returns>
        public bool Check()
        {
            return AHelper.Net.FTPCheck(URI, UserName, Password, TimeOut);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            ServerIP = null;
            UserName = null;
            Password = null;
            RemotePath = null;
            URI = null;
        }
    }
}