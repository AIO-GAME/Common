#region

using System;
using System.Collections;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    /// <summary>
    /// 进度操作
    /// </summary>
    public interface IProgressOperation : IDisposable
    {
        /// <summary>
        /// 进度报告
        /// </summary>
        IProgressReport Report { get; }

        /// <summary>
        /// 进度参数
        /// </summary>
        IProgressEvent Event { get; set; }

        /// <summary>
        /// 进度信息
        /// </summary>
        public IProgressInfo Progress { get; }

        /// <summary>
        /// 开始
        /// </summary>
        void Begin();

        /// <summary>
        /// 取消
        /// </summary>
        void Cancel();

        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();

        /// <summary>
        /// 恢复
        /// </summary>
        void Resume();

        /// <summary>
        /// 重新开始
        /// </summary>
        void Again();

        /// <summary>
        /// 迭代器等待
        /// </summary>
#if NET_5_0_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    [Obsolete("net framework 5 or net core 3.0 or later version support this", true)]
#endif
        IEnumerator WaitCo();

        /// <summary>
        /// 异步等待
        /// </summary>
        /// <returns></returns>
        Task WaitAsync();

        /// <summary>
        /// 同步等待
        /// </summary>
        void Wait();

        /// <summary>
        /// 异步回调
        /// </summary>
        void WaitAsyncCallBack();
    }
}