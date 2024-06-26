﻿#region

using System.IO;
using UnityEngine;

#endregion

namespace AIO
{
    partial class RHelper
    {
        #region Nested type: Path

        /// <summary>
        /// 提供了一些与路径相关的实用方法。
        /// 包含与程序集有关的实用方法和属性的静态类
        /// </summary>
        public static class Path
        {
            static Path()
            {
                Assets              = Application.dataPath;
                Project             = Directory.GetParent(Assets)?.FullName.Replace('\\', System.IO.Path.AltDirectorySeparatorChar);
                StreamingAssetsPath = Application.streamingAssetsPath;
                PersistentDataPath  = Application.persistentDataPath;
            }

            /// <summary>
            /// 获取当前项目 Assets 文件夹的完整路径。
            /// </summary>
            public static string Assets { get; }

            /// <summary>
            /// 获取当前项目 Streaming Assets 文件夹的完整路径。
            /// </summary>
            public static string StreamingAssetsPath { get; }

            /// <summary>
            /// 获取当前项目 Persistent Assets 文件夹的完整路径。
            /// </summary>
            public static string PersistentDataPath { get; }

            /// <summary>
            /// 获取当前项目所在文件夹的完整路径。
            /// </summary>
            public static string Project { get; }
        }

        #endregion
    }
}