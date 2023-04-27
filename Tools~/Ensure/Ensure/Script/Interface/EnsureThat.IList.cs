using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>

        public void SizeIs<T>(in IList<T> value, in int expected)
        {
            SizeIs(value as ICollection<T>, expected);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>

        public void SizeIs<T>(in IList<T> value, in long expected)
        {
            SizeIs(value as ICollection<T>, expected);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Any(predicate) == false]
        /// </summary>

        public void Any<T>(in IList<T> value, in Func<T, bool> predicate)
        {
            Any(value as ICollection<T>, predicate);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value.Count &lt; 1]
        /// </summary>

        public void HasItems<T>(in IList<T> value)
        {
            HasItems(value as ICollection<T>);
        }
    }
}