﻿using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 文件路径检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class FilePathAttribute : InspectorAttribute
    {
        public string Extension { get; private set; }

        /// <summary>
        /// 文件路径检视器（支持 string 类型）
        /// </summary>
        /// <param name="extension">文件扩展名</param>
        public FilePathAttribute(string extension = "*.*")
        {
            Extension = extension;
        }
    }
}