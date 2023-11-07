using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class ExtendIReflect
    {
        /// <summary>
        /// 申明
        /// </summary>
        internal const BindingFlags DeclaredFlags =
                   BindingFlags.NonPublic |
                   BindingFlags.Public |
                   BindingFlags.Instance |
                   BindingFlags.Static |
                   BindingFlags.DeclaredOnly;

        /// <summary>
        /// 获取申明的指定属性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetDeclaredProperty(this IReflect type, in string propertyName)
        {
            var props = GetDeclaredProperties(type);

            for (var i = 0; i < props.Length; ++i)
            {
                if (props[i].Name == propertyName)
                {
                    return props[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取申明的指定字段
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldInfo GetDeclaredField(this IReflect type, in string propertyName)
        {
            var props = GetDeclaredFields(type);

            for (var i = 0; i < props.Length; ++i)
            {
                if (props[i].Name == propertyName)
                {
                    return props[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取申明的指定函数
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo GetDeclaredMethod(this IReflect type, in string methodName)
        {
            var methods = GetDeclaredMethods(type);

            for (var i = 0; i < methods.Length; ++i)
            {
                if (methods[i].Name == methodName)
                {
                    return methods[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取申明的指定成员
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemberInfo GetDeclaredMember(this IReflect type, in string memberName)
        {
            var members = GetDeclaredMembers(type);

            for (var i = 0; i < members.Length; ++i)
            {
                if (members[i].Name == memberName)
                {
                    return members[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取声明的全部函数
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetDeclaredMethods(this IReflect type)
        {
            return type.GetMethods(DeclaredFlags);
        }

        /// <summary>
        /// 获取声明的全部属性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo[] GetDeclaredProperties(this IReflect type)
        {
            return type.GetProperties(DeclaredFlags);
        }

        /// <summary>
        /// 获取声明的全部字段
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldInfo[] GetDeclaredFields(this IReflect type)
        {
            return type.GetFields(DeclaredFlags);
        }

        /// <summary>
        /// 获取声明的全部成员
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemberInfo[] GetDeclaredMembers(this IReflect type)
        {
            return type.GetMembers(DeclaredFlags);
        }
    }
}
