using System;

namespace AIO
{
    /// <summary>
    /// 用于查询类型转换的结构体，实现了IEquatable接口
    /// </summary>
    internal readonly struct ConversionQuery : IEquatable<ConversionQuery>
    {
        /// <summary>
        /// 源类型
        /// </summary>
        public readonly Type source;

        /// <summary>
        /// 目标类型
        /// </summary>
        public readonly Type destination;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="source">源类型</param>
        /// <param name="destination">目标类型</param>
        public ConversionQuery(in Type source, in Type destination)
        {
            this.source = source;
            this.destination = destination;
        }

        /// <inheritdoc/>
        public bool Equals(ConversionQuery other)
        {
            return
                source == other.source &&
                destination == other.destination;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is ConversionQuery query) return Equals(query);
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Utils.Hash.GetHashCode(source, destination);
        }
    }
}