/*|============|*|
|*|Author:     |*| star fire
|*|Date:       |*| 2024-01-29
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;

namespace AIO.UEditor
{
    /// <summary>
    /// E InitAttr Mode
    /// </summary>
    [Flags]
    public enum EInitAttrMode : byte
    {
        /// <summary>
        /// 编辑器加载
        /// </summary>
        Editor = 1 << 0,

        /// <summary>
        /// 运行时加载 - 在场景加载之前
        /// </summary>
        RuntimeBeforeSceneLoad = 1 << 1,

        /// <summary>
        /// 运行时加载 - 在场景加载之后
        /// </summary>
        RuntimeAfterSceneLoad = 1 << 2,

        /// <summary>
        /// 运行时加载 - 在程序集加载之后
        /// </summary>
        RuntimeAfterAssembliesLoaded = 1 << 3,

        /// <summary>
        /// 运行时加载 - 在启动画面之前
        /// </summary>
        RuntimeBeforeSplashScreen = 1 << 4,

        /// <summary>
        /// 运行时加载 - 在子系统注册之后
        /// </summary>
        RuntimeSubsystemRegistration = 1 << 5,

        /// <summary>
        /// 运行时加载 - 在场景加载之前和之后 和 仅在编辑器中加载
        /// </summary>
        Both = Editor | RuntimeAfterSceneLoad,

        /// <summary>
        /// 所有
        /// </summary>
        ALL = Editor | RuntimeBeforeSceneLoad | RuntimeAfterSceneLoad | RuntimeAfterAssembliesLoaded |
              RuntimeBeforeSplashScreen | RuntimeSubsystemRegistration
    }
}