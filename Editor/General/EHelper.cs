#region

using System;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// Utils Unity Editor
    /// </summary>
    public static partial class EHelper
    {
        private const string kFirstEnterUnity = "FirstEnterUnity"; //是否首次进入unity

        public static bool FirstEnterUnity
        {
            get
            {
                if (!SessionState.GetBool(kFirstEnterUnity, true)) return true;
                SessionState.SetBool(kFirstEnterUnity, false);
                return false;
            }
        }

        /// <summary>
        /// 是否为命令行模式
        /// </summary>
        /// <returns>
        /// Ture: 是
        /// False: 否
        /// </returns>
        public static bool IsCMD()
        {
#if UNITY_2019_4_OR_NEWER
            return Application.isBatchMode;
#else
            return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
#endif
        }

        public static void DisplayProgressBar(string title, string info, float progress)
        {
            if (IsCMD()) Console.WriteLine($"[{title}/{progress * 100:00}] {info}");
            else EditorUtility.DisplayProgressBar(title, info, progress);
        }

        public static void DisplayDialog(string title, string info, string ok)
        {
            if (IsCMD())
            {
                Console.WriteLine($"[{title}] {info}");
            }
            else
            {
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog(title, info, ok);
            }
        }
    }
}