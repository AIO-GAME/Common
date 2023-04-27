using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value[x] == null]
        /// </summary>

        public void HasNoNullItem<T>(T value) where T : class, IEnumerable
        {
            if (!Ensure.IsActive) return;

            IsNotNull(value);

            foreach (var item in value)
            {
                if (item == null) throw new ArgumentException(ExceptionMessages.Collections_HasNoNullItemFailed, paramName);
            }
        }
    }
}