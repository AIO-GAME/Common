#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 单位换算默认单位
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class UDefaultAttribute : Attribute
    {
        /// <inheritdoc />
        public UDefaultAttribute(double unit)
        {
            Unit = unit;
        }

        /// <inheritdoc />
        public UDefaultAttribute(float unit)
        {
            Unit = unit;
        }

        /// <inheritdoc />
        public UDefaultAttribute(int unit)
        {
            Unit = unit;
        }

        /// <inheritdoc />
        public UDefaultAttribute(long unit)
        {
            Unit = unit;
        }

        /// <inheritdoc />
        public UDefaultAttribute(short unit)
        {
            Unit = unit;
        }

        /// <summary>
        /// 单位换算默认单位
        /// </summary>
        public double Unit { get; }
    }
}