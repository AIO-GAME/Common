namespace AIO
{
    /// <summary>
    /// 优化访问器
    /// </summary>
    public interface IOptimizedAccessor
    {
        /// <summary>
        /// 编译器
        /// </summary>
        void Compile();

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="target">参数</param>
        /// <returns>值</returns>
        object GetValue(in object target);

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="target">目标值</param>
        /// <param name="value">设置值</param>
        void SetValue(in object target,in  object value);
    }
}
