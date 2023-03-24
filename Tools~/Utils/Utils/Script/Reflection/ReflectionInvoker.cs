using System;
using System.Linq;
using System.Reflection;

namespace AIO
{
    public class ReflectionInvoker : IOptimizedInvoker
    {
        public ReflectionInvoker(in MethodInfo methodInfo)
        {
            if (OptimizedReflection.safeMode)
            {
                Ensure.That(nameof(methodInfo)).IsNotNull(methodInfo);
            }

            this.methodInfo = methodInfo;
        }

        private readonly MethodInfo methodInfo;

        public void Compile()
        {
        }

        public object Invoke(in object target, params object[] args)
        {
            return methodInfo.Invoke(target, args);
        }

        public object Invoke(in object target)
        {
            return methodInfo.Invoke(target, EmptyObjects);
        }

        public object Invoke(in object target, in object arg0)
        {
            return methodInfo.Invoke(target, new[] { arg0 });
        }

        public object Invoke(in object target, in object arg0, in object arg1)
        {
            return methodInfo.Invoke(target, new[] { arg0, arg1 });
        }

        public object Invoke(in object target, in object arg0, in object arg1, in object arg2)
        {
            return methodInfo.Invoke(target, new[] { arg0, arg1, arg2 });
        }

        public object Invoke(in object target, in object arg0, in object arg1, in object arg2, in object arg3)
        {
            return methodInfo.Invoke(target, new[] { arg0, arg1, arg2, arg3 });
        }

        public object Invoke(in object target, in object arg0, in object arg1, in object arg2, in object arg3, in object arg4)
        {
            return methodInfo.Invoke(target, new[] { arg0, arg1, arg2, arg3, arg4 });
        }

        public Type[] GetParameterTypes()
        {
            return methodInfo.GetParameters().Select(pi => pi.ParameterType).ToArray();
        }

        private static readonly object[] EmptyObjects = Array.Empty<object>();
    }
}