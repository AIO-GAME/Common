namespace AIO
{
    /// <summary>
    /// 缓存处理
    /// </summary>
    public interface ICacheableHandler
    {
        /// <summary>
        /// 登记
        /// </summary>
        void OnCheckIn();

        /// <summary>
        /// 检出
        /// </summary>
        void OnCheckOut();
    }
}
