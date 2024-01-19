/*|============|*|
|*|Author:     |*| xi nan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace AIO.UEditor
{
    /// <summary>
    /// Utils Unity Editor
    /// </summary>
    public static partial class EHelper
    {
        /// <summary>
        /// 是否为命令行模式
        /// </summary>
        /// <returns>
        /// Ture: 是
        /// False: 否
        /// </returns>
        public static bool IsCMD()
        {
            return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }

        public static void DisplayProgressBar(string title, string info, float progress)
        {
            if (IsCMD()) Console.WriteLine($"[{title}/{progress * 100:00}] {info}");
            else EditorUtility.DisplayProgressBar(title, info, progress);
        }

        public static void DisplayDialog(string title, string info, string ok)
        {
            if (IsCMD()) Console.WriteLine($"[{title}] {info}");
            else
            {
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog(title, info, ok);
            }
        }
    }
}