using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [default(T).Equals(param)]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotDefault<T>(in T param) where T : struct
        {
            if (!Ensure.IsActive) return;

            if (default(T).Equals(param))
            {
                throw new ArgumentException(ExceptionMessages.ValueTypes_IsNotDefault_Failed, paramName);
            }
        }
    }
}