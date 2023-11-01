using System;

/// <summary>
/// 处理器帮助类
/// </summary>
public static partial class AHandle
{
    /// <summary>
    /// 进度处理器
    /// </summary>
    public interface IProgress : IDisposable, IProgress<float>
    {
        /// <summary>
        /// 进度回调
        /// </summary>
        Action<float> OnProgress { get; set; }

        /// <summary>
        /// 完成回调
        /// </summary>
        Action OnComplete { get; set; }

        /// <summary>
        /// 错误回调
        /// </summary>
        Action<Exception> OnError { get; set; }
    }

    /// <summary>
    /// 进度处理器
    /// </summary>
    internal sealed class Progress : IProgress
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private float Current { get; set; }

        /// <summary>
        /// 总值
        /// </summary>
        internal float Total { get; set; }

        /// <summary>
        /// 进度回调
        /// </summary>
        public Action<float> OnProgress { get; set; }

        /// <summary>
        /// 完成回调
        /// </summary>
        public Action OnComplete { get; set; }

        /// <summary>
        /// 错误回调
        /// </summary>
        public Action<Exception> OnError { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onProgress">进度回调</param>
        /// <param name="onComplete">完成回调</param>
        public Progress(Action<float> onProgress, Action onComplete)
        {
            OnProgress = onProgress;
            OnComplete = onComplete;
            Current = 0;
            Total = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Progress(IProgress data)
        {
            OnProgress = data.OnProgress;
            OnComplete = data.OnComplete;
            Current = 0;
            Total = 0;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            OnProgress = null;
            OnComplete = null;
        }

        /// <summary>
        /// 报告进度
        /// </summary>
        /// <param name="value">当前值</param>
        public void Report(float value)
        {
            if (OnProgress == null) return;
            Current = value;
            var progress = value / Total;
            OnProgress.Invoke(progress);
        }

        internal void Complete()
        {
            OnComplete?.Invoke();
            Dispose();
        }

        internal void Error(Exception error)
        {
            OnError?.Invoke(error);
        }
    }
}