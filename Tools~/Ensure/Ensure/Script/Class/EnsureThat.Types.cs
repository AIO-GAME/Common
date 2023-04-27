using System;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [expectedType.IsAssignableFrom(param?.GetType()) == false]
        /// </summary>

        public void IsOfType<T>(in T param, in Type expectedType)
        {
            if (!Ensure.IsActive) return;

            if (!expectedType.IsAssignableFrom(param?.GetType()))
            {
                throw new ArgumentException(ExceptionMessages.Types_IsOfType_Failed.Inject(expectedType.ToString(), param?.GetType().ToString() ?? "null"), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [expectedType.IsAssignableFrom(param) == false]
        /// </summary>

        public void IsOfType(in Type param, in Type expectedType)
        {
            if (!Ensure.IsActive) return;

            if (!expectedType.IsAssignableFrom(param))
            {
                throw new ArgumentException(ExceptionMessages.Types_IsOfType_Failed.Inject(expectedType.ToString(), param.ToString()), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [typeof(T).IsAssignableFrom(param) == false]
        /// </summary>

        public void IsOfType<T>(in object param)
        {
            IsOfType(param, typeof(T));
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [typeof(T).IsAssignableFrom(param) == false]
        /// </summary>

        public void IsOfType<T>(in Type param)
        {
            IsOfType(param, typeof(T));
        }
    }
}