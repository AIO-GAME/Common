namespace AIO
{
    using System;

    /// <summary>
    /// 注册平台DLL
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class RegistInitializePlatformAttribute : RegistInitializeDLLAttribute
    {
        /// <summary>
        /// 游戏平台
        /// </summary>
        public readonly ERuntimePlatform Platform;

        /// <summary>
        /// 注册DLL
        /// </summary>
        public RegistInitializePlatformAttribute(Type type, string name, ERuntimePlatform platform) : base(type, name)
        {
            Platform = platform;
        }
    }
}
