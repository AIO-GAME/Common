#region

using System;

#endregion

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(value) != 0]
        /// </summary>
        public void Is<T>(in T param, in T value)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;
            if (param.CompareTo(value) != 0)
                throw new ArgumentException(ExceptionMessages.Comp_Is_Failed.Inject(param, value), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(value) == 0]
        /// </summary>
        public void IsNot<T>(in T param, in T value)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;
            if (param.CompareTo(value) == 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNot_Failed.Inject(param, value), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(value) >= 0]
        /// </summary>
        public void IsLt<T>(in T param, in T value)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;
            if (param.CompareTo(value) >= 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNotLt.Inject(param, value), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(value) > 0]
        /// </summary>
        public void IsLte<T>(in T param, in T value)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;
            if (param.CompareTo(value) > 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNotLte.Inject(param, value), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(value) &lt;= 0]
        /// </summary>
        public void IsGt<T>(in T param, in T value)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;
            if (param.CompareTo(value) <= 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNotGt.Inject(param, value), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(value) &lt; 0]
        /// </summary>
        public void IsGte<T>(in T param, in T value)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;
            if (param.CompareTo(value) < 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNotGte.Inject(param, value), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [param.CompareTo(min) &lt; 0]
        /// [param.CompareTo(max) > 0]
        /// </summary>
        public void IsInRange<T>(in T param, in T min, in T max)
        where T : struct, IComparable<T>
        {
            if (!Ensure.IsActive) return;

            if (param.CompareTo(min) < 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNotInRange_ToLow.Inject(param, min), paramName);

            if (param.CompareTo(max) > 0)
                throw new ArgumentException(ExceptionMessages.Comp_IsNotInRange_ToHigh.Inject(param, max), paramName);
        }
    }
}