/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-22
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

namespace AIO.UEngine
{
    public enum EASMode
    {
        /// <summary>
        /// 编辑器模式
        /// </summary>
        Editor,

        /// <summary>
        /// 远端模式
        /// </summary>
        Remote,

        /// <summary>
        /// 远端模式 + 边玩边下
        /// </summary>
        RemoteWithSidePlayWithDownload,

        /// <summary>
        /// 本地模式
        /// </summary>
        Local,
    }

    public class ASConfig
    {
        /// <summary>
        /// 资源加载模式
        /// </summary>
        public EASMode ASMode = EASMode.Local;

        /// <summary>
        /// 热更新资源包服务器地址
        /// </summary>
        public string URL = "";

        /// <summary>
        /// 热更新资源同时最大下载数量
        /// </summary>
        public int DownloadingMaxNumber = 50;

        /// <summary>
        /// 热更新资源下载失败重试次数
        /// </summary>
        public int FailedTryAgain = 3;

        /// <summary>
        /// 热更新资源下载超时时间
        /// </summary>
        public int Timeout = 10;

        /// <summary>
        /// 自动激活清单
        /// </summary>
        public bool AutoSaveVersion;

        /// <summary>
        /// URL请求附加时间搓
        /// </summary>
        public bool AppendTimeTicks = true;
    }
}