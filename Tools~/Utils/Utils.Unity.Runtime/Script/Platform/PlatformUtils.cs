namespace AIO.Unity
{
    using System.Runtime.CompilerServices;

    using UnityEngine;

    /// <summary>
    /// 平台工具类
    /// </summary>
    public static class PlatformUtils
    {
        /// <summary>
        /// 支持jit
        /// </summary>
        public static readonly bool supportsJit;

        static PlatformUtils()
        {
            supportsJit = CheckJitSupport();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckJitSupport()
        {
            // Temporary hotfix
            // Generally it seems like JIT is becoming more and more unreliable
            // And some of the generated IL we were using crashes in some cases, but it's hard to isolate
            // Because the delegate approach is very close in speed, we'll just disable it altogether until Bolt 2
            // generates full C# scripts.
            // https://forum.unity.com/threads/is-jit-no-longer-supported-on-standalone-mono.671572/
            // https://support.ludiq.io/communities/5/topics/3129-bolt-143-runtime-broken
            // https://support.ludiq.io/communities/5/topics/4013-unity-crash-randomly-after-hit-play
            return false;
        }

        /// <summary>
        /// 是否为编辑器
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEditor(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.WindowsEditor ||
                platform == RuntimePlatform.OSXEditor ||
                platform == RuntimePlatform.LinuxEditor;
        }

        /// <summary>
        /// 是否为安卓平台
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAndroid(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.Android;
        }

        /// <summary>
        /// 是否为安卓平台
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIOS(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.IPhonePlayer;
        }

        /// <summary>
        /// 是否为安卓平台
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWebGL(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.WebGLPlayer;
        }

        /// <summary>
        /// 是否为独立平台
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStandalone(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.WindowsPlayer ||
                platform == RuntimePlatform.OSXPlayer ||
                platform == RuntimePlatform.LinuxPlayer;
        }
    }
}
