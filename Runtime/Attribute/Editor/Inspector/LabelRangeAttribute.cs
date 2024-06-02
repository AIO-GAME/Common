using System;
using System.Diagnostics;

namespace AIO.UEngine
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property), Conditional("UNITY_EDITOR")]
    public class LabelRangeAttribute : LabelAttribute
    {
        public readonly float min;
        public readonly float max;

        /// <summary>
        /// 使字段在Inspector中显示自定义的名称。
        /// </summary>
        public LabelRangeAttribute(string name, float min, float max) : base(name)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// 使字段在Inspector中显示自定义的名称。
        /// </summary>
        public LabelRangeAttribute(string name, int order, float min, float max) : base(name, order)
        {
            this.min = min;
            this.max = max;
        }
    }
}