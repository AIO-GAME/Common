namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 简单选项类型。这类似于可空类型。
    /// </summary>
    public struct fsOption<T>
    {
        private bool _hasValue;
        private T _value;

        /// <summary>
        /// 是否存在值
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        /// 是否为NULL
        /// </summary>
        public bool IsEmpty => _hasValue == false;

        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get
            {
                if (IsEmpty)
                {
                    throw new InvalidOperationException("fsOption is empty");
                }
                return _value;
            }
        }

        /// <summary>
        /// 可选类型
        /// </summary>
        public fsOption(in T value)
        {
            _hasValue = true;
            _value = value;
        }

        /// <summary>
        /// 空
        /// </summary>
        public static fsOption<T> Empty;
    }

    /// <summary>
    /// 可选
    /// </summary>
    public static class fsOption
    {
        /// <summary>
        /// 正确的
        /// </summary>
        public static fsOption<T> Just<T>(in T value)
        {
            return new fsOption<T>(value);
        }
    }
}
