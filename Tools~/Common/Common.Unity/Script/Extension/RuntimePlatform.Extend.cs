using UnityEngine;

namespace AIO.Unity
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class RuntimePlatformExtend
    {
        /// <summary>
        /// 是否为编辑器
        /// </summary>
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
        public static bool IsAndroid(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.Android;
        }

        /// <summary>
        /// 是否为安卓平台
        /// </summary>
        public static bool IsIOS(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.IPhonePlayer;
        }

        /// <summary>
        /// 是否为安卓平台
        /// </summary>
        public static bool IsWebGL(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.WebGLPlayer;
        }

        /// <summary>
        /// 是否为独立平台
        /// </summary>
        public static bool IsStandalone(this RuntimePlatform platform)
        {
            return
                platform == RuntimePlatform.WindowsPlayer ||
                platform == RuntimePlatform.OSXPlayer ||
                platform == RuntimePlatform.LinuxPlayer;
        }
    }
}