#region

using System;
using System.Reflection;

#endregion

namespace AIO
{
    public static partial class ExtendType
    {
        /// <summary>
        ///     转化为指定类型
        /// </summary>
        public static T As<T>(this object target)
        where T : class
        {
            return target as T;
        }

        /// <summary>
        ///     转化为指定类型
        /// </summary>
        public static T To<T>(this object? target, bool throwException = true)
        {
            if (target is null)
                return throwException
                    ? throw new ArgumentNullException(nameof(target))
                    : (T)default;

            try
            {
                return (T)target;
            }
            catch (Exception e)
            {
                return throwException
                    ? throw new InvalidCastException($"Conversion failure : {e.Message}")
                    : (T)default;
            }
        }

        /// <summary>
        ///     转化为标准字符串
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="target">类型</param>
        /// <returns>
        ///     <see cref="TypeReflectionInfo" />
        /// </returns>
        public static TypeReflectionInfo ToDetails(this Type target)
        {
            return target is null ? TypeReflectionInfo.Empty : new TypeReflectionInfo(target);
        }

        /// <summary>
        ///     转化为标准字符串
        /// </summary>
        /// <param name="parameter">参数信息</param>
        /// <returns>
        ///     <see cref="ParameterInfoReflectionInfo" />
        /// </returns>
        public static ParameterInfoReflectionInfo ToDetails(this ParameterInfo parameter)
        {
            return parameter is null ? ParameterInfoReflectionInfo.Empty : new ParameterInfoReflectionInfo(parameter);
        }

        /// <summary>
        ///     转化为标准字符串
        /// </summary>
        /// <returns>
        ///     <see cref="MethodReflectionInfo" />
        /// </returns>
        public static MethodReflectionInfo ToDetails(this MethodInfo method)
        {
            return method is null ? MethodReflectionInfo.Empty : new MethodReflectionInfo(method);
        }
    }
}