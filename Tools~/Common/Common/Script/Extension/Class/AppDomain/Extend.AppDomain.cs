#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    ///     App Domain 扩展
    /// </summary>
    public static class ExtendAppDomain
    {
        /// <summary>
        ///     获取属性
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <returns>属性数组</returns>
        public static T[] GetAttributes<T>(this AppDomain domain, in bool inherit = false)
        where T : Attribute
        {
            var typeo = typeof(T);
            var list = new List<T>();
            foreach (var assembly in domain.GetAssemblies())
            foreach (var type in assembly.GetTypes())
            foreach (var item in type.GetCustomAttributes(typeo, inherit))
                if (item is T t)
                    list.Add(t);

            return list.ToArray();
        }

        /// <summary>
        ///     获取属性
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <returns>属性数组</returns>
        public static T[] GetAttributes<T>(this AppDomain domain, Func<Type, T, T> func, in bool inherit = false)
        where T : Attribute
        {
            var typeo = typeof(T);
            var list = new List<T>();
            foreach (var assembly in domain.GetAssemblies())
            foreach (var type in assembly.GetTypes())
            foreach (var item in type.GetCustomAttributes(typeo, inherit))
                if (item is T t)
                    list.Add(func.Invoke(type, t));

            return list.ToArray();
        }
    }
}