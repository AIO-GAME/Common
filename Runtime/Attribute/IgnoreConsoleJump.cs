/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-20
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AIO
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct)]
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

        public IgnoreConsoleJumpAttribute(
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            FilePath = filePath;
            MemberName = memberName;
            LineNumber = lineNumber;
        }

        public sealed override string ToString()
        {
            return $"{FilePath} {MemberName} {LineNumber}";
        }
    }
}