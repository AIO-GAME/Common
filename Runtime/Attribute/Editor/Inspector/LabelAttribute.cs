#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 标签检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property), Conditional("UNITY_EDITOR")]
    public class LabelAttribute : InspectorAttribute
    {
        public LabelAttribute(string name) { Name = name; }

        public LabelAttribute(string name, int order)
        {
            Name  = name;
            Order = order;
        }

        public int Order { get; private set; }

        public string Name { get; private set; }
    }
}