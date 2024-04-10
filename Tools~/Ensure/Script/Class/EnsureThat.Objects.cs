#region

using System;

#endregion

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件 value != null
        /// </summary>
        public void IsNull<T>(in T value)
        {
            if (!Ensure.IsActive) return;

            if (value != null) throw new ArgumentNullException(paramName, ExceptionMessages.Common_IsNull_Failed);
        }

        /// <summary>
        /// 验证数据 报错条件 value == null 
        /// </summary>
        public void IsNotNull<T>(T value)
        {
            if (!Ensure.IsActive) return;

            if (value == null) throw new ArgumentNullException(paramName, ExceptionMessages.Common_IsNotNull_Failed);
        }
    }
}