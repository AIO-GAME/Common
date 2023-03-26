using System.Reflection;

namespace AIO
{
    public abstract class StaticActionInvokerBase : StaticInvokerBase
    {
        protected StaticActionInvokerBase(in MethodInfo methodInfo) : base(methodInfo)
        {
        }
    }
}