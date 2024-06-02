#region

using System;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace AIO
{
    /// <summary>
    /// 执行时间
    /// </summary>
    public abstract class PrintElapse : IDisposable
    {
        /// <summary>
        /// 计算时间精度
        /// </summary>
        protected Stopwatch stopWatch;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="title">消耗时间标题</param>
        public PrintElapse(string title) { Title = title; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; }

        #region IDisposable Members

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            stopWatch.Stop();
            stopWatch = null;
        }

        #endregion

        /// <summary>
        /// 开始计时
        /// </summary>
        public PrintElapse Start()
        {
            if (stopWatch == null)
                stopWatch = Stopwatch.StartNew();
            else stopWatch.Restart();
            return this;
        }

        /// <summary>
        /// 时间重置
        /// </summary>
        public void Restart() { stopWatch.Restart(); }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="format">时间格式</param>
        protected internal abstract void Finish(string format = "g");

        /// <summary>
        /// 异常信息
        /// </summary>
        protected void Exception(Exception exception, string format = "g")
        {
            stopWatch.Stop();
            throw new Exception($"{$"{stopWatch.Elapsed.ToString(format, CultureInfo.CurrentCulture)}",-30} -> {Title}", exception);
        }
    }
}