#region

using System;

#endregion

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件 value.Equals(Guid.Empty) == true
        /// </summary>
        public void IsNotEmpty(in Guid value)
        {
            if (!Ensure.IsActive) return;

            if (value.Equals(Guid.Empty)) throw new ArgumentException(ExceptionMessages.Guids_IsNotEmpty_Failed, paramName);
        }
    }
}