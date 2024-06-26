﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using UObject = UnityEngine.Object;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// Unity Object 扩展
    /// </summary>
    public static class UnityObjectExtend
    {
        /// <summary>
        /// 判断目标是否销毁
        /// </summary>
        public static bool IsDestroyed(this UObject target)
        {
            // Checks whether a Unity object is not actually a null reference,
            // but a rather destroyed native instance.

            return !ReferenceEquals(target, null) && target.Equals(null);
        }

        /// <summary>
        /// Unity是否为Null
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsUnityNull(this UObject target)
        {
            // Checks whether an object is null or Unity pseudo-null
            // without having to cast to UnityEngine.Object manually

            return target.Equals(null) || (target != null && target.Equals(null));
        }

        /// <summary>
        /// 转化为Unity字段
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T AsUnityNull<T>(this T obj)
        where T : UObject
        {
            // Converts a Unity pseudo-null to a real null, allowing for coalesce operators.
            // e.g.: destroyedObject.AsUnityNull() ?? otherObject
            return obj.Equals(null) ? null : obj;
        }

        /// <summary>
        /// 排除Unity空引用
        /// </summary>
        public static IEnumerable<T> NotUnityNull<T>(this IEnumerable<T> enumerable)
        where T : UObject
        {
            return enumerable.Where(i => i != null);
        }

        /// <summary>
        /// 转换为安全字符
        /// </summary>
        public static string ToSafeString(this UObject uo)
        {
            if (ReferenceEquals(uo, null)) return "(null)";
            if (uo == null) return "(Destroyed)";
            try
            {
                return uo.name;
            }
            catch (Exception ex)
            {
                return $"({ex.GetType().Name} in ToString: {ex.Message})";
            }
        }
    }
}