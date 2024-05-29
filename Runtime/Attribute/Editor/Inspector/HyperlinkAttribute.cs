#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 超链接检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property), Conditional("UNITY_EDITOR")]
    public sealed class HyperlinkAttribute : InspectorAttribute
    {
        public HyperlinkAttribute(string name) { Name = name; }

        public string Name { get; private set; }
    }
}