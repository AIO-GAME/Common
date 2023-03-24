using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 函数扩展
    /// </summary>
    public static partial class MethodBaseExtend
    {
        public static IEnumerable<ParameterInfo> GetParametersWithoutThis(this MethodBase methodBase)
        {
            return methodBase.GetParameters().Skip(methodBase.IsExtensionMethod() ? 1 : 0);
        }
    }
}