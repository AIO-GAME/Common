namespace AIO
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 注销平台DLL
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class UnRegistInitializePlatformAttribute : UnRegistInitializeDLLAttribute
    {
        /// <summary>
        /// 游戏平台
        /// </summary>
        public readonly ERuntimePlatform Platform;

        /// <summary>
        /// 注册DLL
        /// </summary>
        public UnRegistInitializePlatformAttribute(Type type, string name, ERuntimePlatform platform) : base(type, name)
        {
            Platform = platform;
        }
    }
}
