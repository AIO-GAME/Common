namespace AIO
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 类型扩展
    /// </summary>
    public static partial class ITypeExtend
    {
        /// <summary>
        /// 获取申明的全部构造函数
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetDeclaredConstructors(this _Type type)
        {
            return type.GetConstructors(IReflectExtend.DeclaredFlags & ~BindingFlags.Static); // LUDIQ: 排除静态构造函数
        }

        /// <summary>
        /// 获取申明构造函数
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo GetDeclaredConstructor(this _Type type, in Type[] parameters)
        {
            var ctors = GetDeclaredConstructors(type);

            for (var i = 0; i < ctors.Length; ++i)
            {
                var ctor = ctors[i];
                var ctorParams = ctor.GetParameters();

                if (parameters.Length != ctorParams.Length) continue;
                for (var j = 0; j < ctorParams.Length; ++j)
                {
                    if (ctorParams[j].ParameterType != parameters[j])
                    {
                        continue;
                    }
                }

                return ctor;
            }

            return null;
        }

    }
}
