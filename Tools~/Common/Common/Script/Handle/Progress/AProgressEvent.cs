using System;

namespace AIO
{
    /// <summary>
    /// 进度回调参数
    /// </summary>
    public struct AProgressEvent : IProgressEvent
    {
        /// <inheritdoc />
        public Action<IProgressInfo> OnProgress { get; set; }

        /// <inheritdoc />ß
        public Action<IProgressReport> OnComplete { get; set; }

        /// <inheritdoc />
        public Action OnBegin { get; set; }

        /// <inheritdoc />
        public Action<Exception> OnError { get; set; }

        /// <inheritdoc />
        public Action OnResume { get; set; }

        /// <inheritdoc />
        public Action OnPause { get; set; }

        /// <inheritdoc />
        public Action OnCancel { get; set; }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            OnProgress = null;
            OnComplete = null;
            OnBegin = null;
            OnError = null;
            OnResume = null;
            OnPause = null;
            OnCancel = null;
        }
    }
}