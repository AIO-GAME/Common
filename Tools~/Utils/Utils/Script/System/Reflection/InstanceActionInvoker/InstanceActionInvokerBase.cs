using System.Reflection;

namespace AIO
{
    public abstract class InstanceActionInvokerBase<TTarget> : InstanceInvokerBase<TTarget>
    {
        protected InstanceActionInvokerBase(MethodInfo methodInfo) : base(methodInfo) { }
    }
}