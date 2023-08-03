namespace AIO
{
    /// <summary>
    /// 对象池
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// 创建
        /// </summary>
        void New();

        /// <summary>
        /// 释放
        /// </summary>
        void Free();
    }
}
