
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 验证数据
    /// </summary>
    public static class Ensure
    {
        private static readonly EnsureThat instance = new EnsureThat();

        /// <summary>
        /// 激活
        /// </summary>
        public static bool IsActive { get; set; }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Off() => IsActive = false;

        /// <summary>
        /// 开启
        /// </summary>
        public static void On() => IsActive = true;

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <returns>参数验证</returns>
        public static EnsureThat That(in string paramName)
        {
            instance.paramName = paramName;
            return instance;
        }

        /// <summary>
        /// 运行期间设置
        /// </summary>
        public static void OnRuntimeMethodLoad(in bool isActive)
        {
            IsActive = isActive;
        }
    }
}
