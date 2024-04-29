#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 超链接检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class HyperlinkAttribute : InspectorAttribute
    {
        /// <summary>
        /// 超链接检视器（支持 string 类型）
        /// </summary>
        /// <param name="name">显示名称</param>
        public HyperlinkAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}