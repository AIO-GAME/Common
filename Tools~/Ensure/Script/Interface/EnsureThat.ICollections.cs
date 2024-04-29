#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    public partial class EnsureThat
    {
        #region Any

        /// <summary>
        /// 验证数据 报错条件
        /// [!value.Any(predicate)]
        /// </summary>
        public void Any<T>(in ICollection<T> value, in Func<T, bool> predicate)
        {
            if (!Ensure.IsActive) return;

            if (!value.Any(predicate)) throw new ArgumentException(ExceptionMessages.Collections_Any_Failed, paramName);
        }

        #endregion

        #region HasItems

        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value.Count &lt; 1]
        /// </summary>
        public void HasItems<T>(in T value)
        where T : class, ICollection
        {
            if (!Ensure.IsActive) return;

            IsNotNull(value);

            if (value.Count < 1) throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value.Count &lt; 1]
        /// </summary>
        public void HasItems<T>(in ICollection<T> value)
        {
            if (!Ensure.IsActive) return;

            IsNotNull(value);

            if (value.Count < 1) throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, paramName);
        }

        #endregion

        #region SizeIs

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>
        public void SizeIs<T>(in T value, in int expected)
        where T : ICollection
        {
            if (!Ensure.IsActive) return;

            if (value.Count != expected) throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>
        public void SizeIs<T>(in T value, in long expected)
        where T : ICollection
        {
            if (!Ensure.IsActive) return;

            if (value.Count != expected) throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>
        public void SizeIs<T>(in ICollection<T> value, in int expected)
        {
            if (!Ensure.IsActive) return;

            if (value.Count != expected) throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>
        public void SizeIs<T>(in ICollection<T> value, in long expected)
        {
            if (!Ensure.IsActive) return;

            if (value.Count != expected) throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
        }

        #endregion
    }
}