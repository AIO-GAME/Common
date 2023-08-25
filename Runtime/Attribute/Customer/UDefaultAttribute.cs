/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-28
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;

namespace AIO
{
    /// <summary>
    /// 单位换算默认单位
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class UDefaultAttribute : Attribute
    {
        /// <summary>
        /// 单位换算默认单位
        /// </summary>
        public double Unit { get; }

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
    }
}
