using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value.Length &lt; 1]
        /// </summary>

        public void HasItems<T>(in T[] value)
        {
            if (!Ensure.IsActive) return;

            IsNotNull(value);

            if (value.Length < 1) throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Any(predicate) == false]
        /// </summary>

        public void Any<T>(in T[] value, in Func<T, bool> predicate)
        {
            if (!Ensure.IsActive) return;

            if (!value.Any(predicate))
            {
                throw new ArgumentException(ExceptionMessages.Collections_Any_Failed, paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Length != expected]
        /// </summary>

        public void SizeIs<T>(in T[] value, in int expected)
        {
            if (!Ensure.IsActive) return;

            if (value.Length != expected)
            {
                throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Length), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Length != expected]
        /// </summary>

        public void SizeIs<T>(in T[] value, in long expected)
        {
            if (!Ensure.IsActive) return;

            if (value.Length != expected)
            {
                throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Length), paramName);
            }
        }
    }
}