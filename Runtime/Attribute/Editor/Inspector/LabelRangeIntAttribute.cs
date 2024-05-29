using System;
using System.Diagnostics;

namespace AIO.UEngine
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property), Conditional("UNITY_EDITOR")]
    public class LabelRangeIntAttribute : LabelAttribute
    {
        public readonly int min;
        public readonly int max;

        /// <summary>
        /// 使字段在Inspector中显示自定义的名称。
        /// </summary>
        public LabelRangeIntAttribute(string name, int min, int max) : base(name)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// 使字段在Inspector中显示自定义的名称。
        /// </summary>
        public LabelRangeIntAttribute(string name, int order, int min, int max) : base(name, order)
        {
            this.min = min;
            this.max = max;
        }
    }
}