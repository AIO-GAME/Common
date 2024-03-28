using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AIO
{
    public static partial class ExtendType
    {
        /// <summary>
        /// 使用 as 强转目标
        /// </summary>
        /// <typeparam name="T">强转的类型</typeparam>
        /// <param name="target">强转的对象</param>
        /// <returns>转换后的对象</returns>
        public static T To<T>(this object target) where T : class
        {
            return target as T;
        }

        /// <summary>
        /// 转化为标准字符串 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="target">类型</param>
        /// <returns> <see cref="TypeReflectionInfo"/> </returns>
        public static TypeReflectionInfo ToDetails(this Type target)
        {
            return target is null ? TypeReflectionInfo.Empty : new TypeReflectionInfo(target);
        }

        /// <summary>
        /// 转化为标准字符串
        /// </summary>
        /// <param name="parameter">参数信息</param>
        /// <returns> <see cref="ParameterInfoReflectionInfo"/> </returns>
        public static ParameterInfoReflectionInfo ToDetails(this ParameterInfo parameter)
        {
            return parameter is null ? ParameterInfoReflectionInfo.Empty : new ParameterInfoReflectionInfo(parameter);
        }


        /// <summary>
        /// 转化为标准字符串
        /// </summary>
        /// <returns> <see cref="MethodReflectionInfo"/> </returns>
        public static MethodReflectionInfo ToDetails(this MethodInfo method)
        {
            return method is null ? MethodReflectionInfo.Empty : new MethodReflectionInfo(method);
        }
    }
}