using System.ComponentModel;

namespace AIO
{
    /// <summary>
    /// 缓存状态
    /// </summary>
    public enum ELiteCacheableState
    {
        /// <summary>
        /// 等待
        /// </summary>
        [Description("等待")] Idle,

        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")] Running,
    }
}
