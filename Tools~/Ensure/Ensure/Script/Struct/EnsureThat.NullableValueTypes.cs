using System;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件 value == null
        /// </summary>
        public void IsNotNull<T>(in T? value) where T : struct
        {
            if (!Ensure.IsActive) return;

            if (value == null) throw new ArgumentNullException(paramName, ExceptionMessages.Common_IsNotNull_Failed);
        }
    }
}