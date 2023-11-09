using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 函数信息扩展
    /// </summary>
    public static partial class ExtendMethodInfo
    {
        /// <summary>
        /// 使用给定的类型构造泛型方法，返回一个 MethodInfo 对象。
        /// </summary>
        /// <remarks>
        /// 返回的方法可能是开放构造的方法。
        /// </remarks>
        /// <param name="openConstructedMethod">要构造的开放构造方法</param>
        /// <param name="closedConstructedParameterTypes">用于构造泛型参数的类型</param>
        /// <returns>构造的泛型方法的 MethodInfo 对象</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo MakeGenericMethodVia(
            this MethodInfo openConstructedMethod,
            params Type[] closedConstructedParameterTypes)
        {
            if (!openConstructedMethod.ContainsGenericParameters) return openConstructedMethod;
            var resolvedGenericParameters = new Dictionary<Type, Type>();

            for (var i = 0; i < openConstructedMethod.GetParameters().Length; i++)
            {
                openConstructedMethod.GetParameters()[i].ParameterType
                    .MakeGenericTypeVia(closedConstructedParameterTypes[i], resolvedGenericParameters);
            }

            var closedConstructedGenericArguments = openConstructedMethod.GetGenericArguments().Select(
                openConstructedGenericArgument =>
                    resolvedGenericParameters.ContainsKey(openConstructedGenericArgument)
                        ? resolvedGenericParameters[openConstructedGenericArgument]
                        : openConstructedGenericArgument).ToArray();

            return openConstructedMethod.MakeGenericMethod(closedConstructedGenericArguments);
        }
    }
}