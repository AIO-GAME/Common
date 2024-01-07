using System;

namespace AIO
{
    /// <summary>
    /// 表示当枚举类型的值不在预期范围内时引发的异常。
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    public class AexpUnexpectedEnumValue<T> : Exception
    {
        /// <summary>
        /// 使用指定的枚举值初始化 UnexpectedEnumValueException&lt;T&gt; 类的新实例。
        /// </summary>
        /// <param name="value">不在预期范围内的枚举值</param>
        public AexpUnexpectedEnumValue(in T value)
            : base($"Value {value} of enum {typeof(T).Name} is unexpected.")
        {
            Value = value;
        }

        /// <summary>
        /// 获取引发当前异常的枚举值。
        /// </summary>
        public T Value { get; }
    }
}