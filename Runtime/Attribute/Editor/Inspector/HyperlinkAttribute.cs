/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 超链接检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class HyperlinkAttribute : InspectorAttribute
    {
        public string Name { get; private set; }

        /// <summary>
        /// 超链接检视器（支持 string 类型）
        /// </summary>
        /// <param name="name">显示名称</param>
        public HyperlinkAttribute(string name)
        {
            Name = name;
        }
    }
}