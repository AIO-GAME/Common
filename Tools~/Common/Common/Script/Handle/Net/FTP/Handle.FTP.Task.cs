using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public partial class AHandle
{
    /// <summary>
    /// FTP 处理器
    /// </summary>
    public sealed partial class FTP
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public async Task<bool> InitAsync()
        {
            if (await AHelper.FTP.CheckDirAsync(URI, UserName, Password, TimeOut)) return true;
            return await AHelper.FTP.CreateDirAsync(URI, UserName, Password, TimeOut);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="currentRemotePath">当前远端路径</param>
        /// <param name="newRemoteName">新远端路径</param>
        public Task<bool> MoveAsync(string currentRemotePath, string newRemoteName)
        {
            return AHelper.FTP.ReNameAsync(URI, UserName, Password, currentRemotePath, newRemoteName);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotePath">远端文件路径</param>
        public Task<bool> DeleteFileAsync(string remotePath)
        {
            return AHelper.FTP.DeleteFileAsync(string.Concat(URI, '/', remotePath), UserName, Password);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remotePath">远端文件夹路径</param>
        public Task<bool> DeleteDirAsync(string remotePath = null)
        {
            return AHelper.FTP.DeleteDirAsync(string.Concat(URI, '/', remotePath), UserName, Password);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="remotePath">远端文件夹路径</param>
        public Task<bool> CreateDirAsync(string remotePath = null)
        {
            return AHelper.FTP.CreateDirAsync(string.Concat(URI, '/', remotePath), UserName, Password);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="iEvent">回调</param>
        /// <param name="searchPattern">搜索字段</param>
        /// <param name="searchOption">搜索模式</param>
        public async Task<bool> UploadDirAsync(string localPath, string remotePath, IProgressEvent iEvent,
            SearchOption searchOption = SearchOption.AllDirectories,
            string searchPattern = "*")
        {
            var remote = string.Concat(URI, '/', remotePath);
            var handler = AHelper.FTP.UploadDir(remote, UserName, Password, localPath,
                searchOption, searchPattern, TimeOut, BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="iEvent">回调</param>
        /// <param name="isOverWrite">是否重写</param>
        public async Task<bool> DownloadFileAsync(string localPath, string remotePath, IProgressEvent iEvent = null,
            bool isOverWrite = false)
        {
            var handler = AHelper.FTP.DownloadFile(string.Concat(URI, '/', remotePath), UserName, Password,
                localPath, isOverWrite, TimeOut, BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
        /// <returns>文件列表</returns>
        public Task<List<string>> GetListAsync(string remotePath = null, string keyword = null)
        {
            return AHelper.FTP.GetRemoteListAsync(string.Concat(URI, '/', remotePath), UserName, Password,
                keyword, TimeOut);
        }

        /// <summary>
        /// 获取目录列表
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
        /// <returns>文件列表</returns>
        public Task<List<string>> GetListFileAsync(string remotePath = null, string keyword = null)
        {
            return AHelper.FTP.GetRemoteListFileAsync(string.Concat(URI, '/', remotePath), UserName, Password,
                keyword, TimeOut);
        }

        /// <summary>
        /// 检查FTP是否有效
        /// </summary>
        /// <returns>Ture:有效 False:无效</returns>
        public Task<bool> CheckAsync(string remotePath = null)
        {
            return AHelper.FTP.CheckAsync(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
        }

        /// <summary>
        /// 检查文件是否有效
        /// </summary>
        /// <returns>Ture:有效 False:无效</returns>
        public Task<bool> CheckFileAsync(string remotePath = null)
        {
            return AHelper.FTP.CheckFileAsync(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
        }

        /// <summary>
        /// 检查文件夹是否有效
        /// </summary>
        /// <returns>Ture:有效 False:无效</returns>
        public Task<bool> CheckDirAsync(string remotePath = null)
        {
            return AHelper.FTP.CheckDirAsync(string.Concat(URI, '/', remotePath), UserName, Password, TimeOut);
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
        /// <returns>文件列表</returns>
        public List<string> GetListDir(string remotePath = null, string keyword = "")
        {
            return AHelper.FTP.GetRemoteListDir(string.Concat(URI, '/', remotePath), UserName, Password,
                keyword, TimeOut);
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="keyword">获取指定文件夹 空时获取全部 当获取类型为全部时 则不生效</param>
        /// <returns>文件列表</returns>
        public Task<List<string>> GetListDirAsync(string remotePath = null, string keyword = null)
        {
            return AHelper.FTP.GetRemoteListDirAsync(string.Concat(URI, '/', remotePath), UserName, Password,
                keyword, TimeOut);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="iEvent">回调</param>
        /// <param name="searchPattern">搜索字段</param>
        /// <param name="searchOption">搜索模式</param>
        /// <param name="isOverWrite">是否重写</param>
        public async Task<bool> DownloadDirAsync(
            string localPath,
            IProgressEvent iEvent = null,
            SearchOption searchOption = SearchOption.AllDirectories,
            string searchPattern = "*",
            bool isOverWrite = false)
        {
            var handler = AHelper.FTP.DownloadDir(URI, UserName, Password,
                localPath, searchOption, searchPattern, isOverWrite, TimeOut, BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <returns>文件大小</returns>
        public Task<long> GetFileSizeAsync(string remotePath)
        {
            return AHelper.FTP.GetFileSizeAsync(string.Concat(URI, '/', remotePath), UserName, Password);
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
        public async Task<bool> DownloadDirAsync(
            string localPath,
            string remotePath,
            IProgressEvent iEvent = null,
            SearchOption searchOption = SearchOption.AllDirectories,
            string searchPattern = "*",
            bool isOverWrite = false)
        {
            var handler = AHelper.FTP.DownloadDir(string.Concat(URI, '/', remotePath), UserName, Password,
                localPath, searchOption, searchPattern, isOverWrite,
                TimeOut, BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="iEvent">回调</param>
        /// <param name="searchPattern">搜索字段</param>
        /// <param name="searchOption">搜索模式</param>
        public async Task<bool> UploadDirAsync(string localPath, IProgressEvent iEvent,
            SearchOption searchOption = SearchOption.AllDirectories,
            string searchPattern = "*")
        {
            var handler = AHelper.FTP.UploadDir(URI, UserName, Password, localPath,
                searchOption, searchPattern, TimeOut, BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="remotePath">远端路径</param>
        /// <param name="iEvent">回调</param>
        public async Task<bool> UploadFileAsync(string localPath, string remotePath, IProgressEvent iEvent = null)
        {
            var remote = string.Concat(URI, '/', remotePath);
            var handler = AHelper.FTP.UploadFile(remote, UserName, Password, localPath, TimeOut,
                BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件</param>
        /// <param name="iEvent">回调</param>
        public async Task<bool> UploadFileAsync(string localPath, IProgressEvent iEvent = null)
        {
            var handler = AHelper.FTP.UploadFile(URI, UserName, Password, localPath, TimeOut, BufferSize);
            handler.Event = iEvent;
            handler.Begin();
            await handler.WaitAsync();
            return handler.Report.State == EProgressState.Finish;
        }
    }
}