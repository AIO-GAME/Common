using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件 value == false
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsTrue(in bool value)
        {
            if (!Ensure.IsActive) return;

            if (!value) throw new ArgumentException(ExceptionMessages.Booleans_IsTrueFailed, paramName);
        }

        /// <summary>
        /// 验证数据 报错条件 value == true
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsFalse(in bool value)
        {
            if (!Ensure.IsActive) return;

            if (value) throw new ArgumentException(ExceptionMessages.Booleans_IsFalseFailed, paramName);
        }
    }
}