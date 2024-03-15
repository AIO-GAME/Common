using System;

namespace AIO
{
    /// <summary>
    /// 进度回调参数
    /// </summary>
    public interface IProgressEvent : IDisposable
    {
        /// <summary>
        /// 进度回调
        /// </summary>
        Action<IProgressInfo> OnProgress { get; set; }

        /// <summary>
        /// 完成回调
        /// </summary>
        Action<IProgressReport> OnComplete { get; set; }

        /// <summary>
        /// 开始回调
        /// </summary>
        Action OnBegin { get; set; }

        /// <summary>
        /// 错误回调
        /// </summary>
        Action<Exception> OnError { get; set; }

        /// <summary>
        /// 恢复
        /// </summary>
        Action OnResume { get; set; }

        /// <summary>
        /// 暂停
        /// </summary>
        Action OnPause { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        Action OnCancel { get; set; }
    }
}