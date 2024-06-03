#region

using System;
using System.Collections;
using System.Linq;

#endregion

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value[x] == null]
        /// </summary>
        public void HasNoNullItem<T>(T value)
        where T : class, IEnumerable
        {
            if (!Ensure.IsActive) return;

            IsNotNull(value);

            if (value.Cast<object>().Any(item => item == null)) throw new ArgumentException(ExceptionMessages.Collections_HasNoNullItemFailed, paramName);
        }
    }
}