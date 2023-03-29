using System;
using System.Collections.Generic;
using System.Linq;

using Object = UnityEngine.Object;

namespace AIO.Unity
{
    using UnityObject = Object;

    public static class UnityObjectExtend
    {
        public static bool IsDestroyed(this UnityObject target)
        {
            // Checks whether a Unity object is not actually a null reference,
            // but a rather destroyed native instance.

            return !ReferenceEquals(target, null) && target == null;
        }

        public static bool IsUnityNull(this UnityObject obj)
        {
            // Checks whether an object is null or Unity pseudo-null
            // without having to cast to UnityEngine.Object manually

            return obj == null || ((obj != null) && obj == null);
        }

        public static T AsUnityNull<T>(this T obj) where T : UnityObject
        {
            // Converts a Unity pseudo-null to a real null, allowing for coalesce operators.
            // e.g.: destroyedObject.AsUnityNull() ?? otherObject

            if (obj == null)
            {
                return null;
            }

            return obj;
        }

        public static IEnumerable<T> NotUnityNull<T>(this IEnumerable<T> enumerable) where T : UnityObject
        {
            return enumerable.Where(i => i != null);
        }

        public static string ToSafeString(this UnityObject uo)
        {
            if (ReferenceEquals(uo, null))
            {
                return "(null)";
            }

            if (!UnityThread.allowsAPI)
            {
                return uo.GetType().Name;
            }

            if (uo == null)
            {
                return "(Destroyed)";
            }

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