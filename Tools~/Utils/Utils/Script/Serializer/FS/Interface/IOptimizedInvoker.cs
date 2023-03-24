namespace AIO
{
    /// <summary>
    /// 优化调用
    /// </summary>
    public interface IOptimizedInvoker
    {
        /// <summary>
        /// 编译
        /// </summary>
        void Compile();

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target);

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target, in object arg0);

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target, in object arg0, in object arg1);

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target, in object arg0, in object arg1, in object arg2);

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target, in object arg0, in object arg1, in object arg2, in object arg3);

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target, in object arg0, in object arg1, in object arg2, in object arg3, in object arg4);

        /// <summary>
        /// 调用
        /// </summary>
        object Invoke(in object target, params object[] args);
    }
}