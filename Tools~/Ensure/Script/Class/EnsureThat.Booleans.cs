#region

using System;

#endregion

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件 value == false
        /// </summary>
        public void IsTrue(in bool value)
        {
            if (!Ensure.IsActive) return;

            if (!value) throw new ArgumentException(ExceptionMessages.Booleans_IsTrueFailed, paramName);
        }

        /// <summary>
        /// 验证数据 报错条件 value == true
        /// </summary>
        public void IsFalse(in bool value)
        {
            if (!Ensure.IsActive) return;

            if (value) throw new ArgumentException(ExceptionMessages.Booleans_IsFalseFailed, paramName);
        }
    }
}