namespace AIO
{
    /// <summary>
    /// 对象池
    /// </summary>
    public interface IPoolable : System.IDisposable
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();
    }
}