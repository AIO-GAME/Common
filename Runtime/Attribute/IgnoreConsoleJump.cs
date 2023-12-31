﻿/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-20
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIO
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(
        AttributeTargets.Method |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Constructor |
        AttributeTargets.Property |
        AttributeTargets.Interface, Inherited = false, AllowMultiple = false
    )]
    public class IgnoreConsoleJumpAttribute : Attribute
    {
        /// <summary>
        /// 是否忽略 (默认:false)
        /// </summary>
        /// Ture : 全部不跳转
        /// False: 自动索引到下一个 at 文件信息 
        public bool Ignore { get; set; } = false;

        /// <summary>
        /// 文件名 绝对路径 需要有dll的pdb文件
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 函数名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int LineNumber { get; set; }

        private static string Project;

        public IgnoreConsoleJumpAttribute(bool ignore = false,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            Ignore = ignore;
            if (string.IsNullOrEmpty(Project))
                Project = Application.dataPath.Substring(0,
                    Application.dataPath.LastIndexOf("/", StringComparison.CurrentCulture) + 1);
            FilePath = filePath.Replace('\\', '/').Replace(Project, "");
            MemberName = memberName;
            LineNumber = lineNumber;
        }

        public sealed override string ToString()
        {
            return $"{FilePath} {MemberName} {LineNumber}";
        }
    }
}