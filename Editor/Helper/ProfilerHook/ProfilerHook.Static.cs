/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-03-22
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AIO.UEditor
{
    partial class ProfilerHookTask
    {
        private static string OUTPUT_DIR => Path.Combine(EHelper.Path.Assets, "Editor/Gen");

        private static string OUTPUT_FILE => Path.Combine(EHelper.Path.Assets, $"Editor/Gen/{nameof(ProfilerHook)}.cs");

        private const string SUMMARY = "\n/// <summary>\n/// {0}\n/// </summary>";

        private const string Template = @"
internal sealed class ProfilerHook_FULL_NAME_PTR : ProfilerHook
{
    public ProfilerHook_FULL_NAME_PTR() : base(""TITLE"") { }
    METHOD_TARGET

    protected override MethodBase MethodReplace() =>
        GetType().GetMethod(nameof(HookReplace), BindingFlags.NonPublic | BindingFlags.Instance);

    protected override MethodBase MethodProxy() =>
        GetType().GetMethod(nameof(HookProxy), BindingFlags.NonPublic | BindingFlags.Instance);

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private METHOD_RETURN HookReplace(METHOD_PARAMETERS)
    {
        Profiler.BeginSample(Title);
        var _ = ProxyMethod.Invoke(null, METHOD_PARAMETERS_INVOKE);
        Profiler.EndSample();RETURN_VALUE
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private METHOD_RETURN HookProxy(METHOD_PARAMETERS)
    {
        throw new NotImplementedException();
    }
}";

        private const string Template_Method_Target = @"    
    protected override MethodBase MethodTarget()
    {
        var methods = typeof(CLASS_FULL_NAME).GetMethods();
        foreach (var method in methods)
        {
            if (method.Name == ""METHOD_NAME"") continue; // 判断函数名
            if (IS_PUBLICmethod.IsPublic) continue; // 判断访问修饰符
            if (IS_STATICmethod.IsStatic) continue; // 静态修饰符
            if (IS_VIRTUALmethod.IsVirtual) continue; // 判断是否使用 override 修饰符 以及是否是虚方法
            if (IS_GENERICmethod.IsGenericMethod) continue; // 判断是否是泛型方法

            if (method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) IS_ASYNC null) continue; // 判断是否使用 async 修饰符
            if (method.GetCustomAttribute(typeof(UnsafeValueTypeAttribute)) IS_UNSAFE null) continue; // 判断是否使用 unsafe 修饰符

METHOD_CODE            return method;
        }

        throw new NotImplementedException();
    }";

        private static string Get_Method_Target(MethodInfo method)
        {
            var CLASS_FULL_NAME = method.ReflectedType.ToDetails();
            var METHOD_NAME = method.Name;
            var content = new StringBuilder();
            content.Append(Template_Method_Target);
            content.Replace("METHOD_NAME", METHOD_NAME);
            content.Replace("CLASS_FULL_NAME", CLASS_FULL_NAME);
            // 判断函数结构
            content.Replace("IS_PUBLIC", method.IsPublic ? "!" : string.Empty);
            content.Replace("IS_STATIC", method.IsStatic ? "!" : string.Empty);
            content.Replace("IS_VIRTUAL", method.IsVirtual ? "!" : string.Empty);
            content.Replace("IS_GENERIC", method.IsGenericMethod ? "!" : string.Empty);
            content.Replace("IS_ASYNC", method.IsOpAsync() ? "is" : "!=");
            content.Replace("IS_UNSAFE", method.IsOpUnsafe() ? "is" : "!=");

            var code = new StringBuilder();
            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments(); // 泛型参数
                var genericArgumentsStr = string.Join(", ", genericArguments.Select(x => $"typeof({x.ToDetails()})"));

                code.AppendLine(
                    $"            var parameters = method.MakeGenericMethod({genericArgumentsStr}).GetParameters();");

                var parameters = method.GetParameters();
                code.AppendLine($"            if (parameters.Length != {parameters.Length}) continue;");
                for (var i = 0; i < parameters.Length; i++)
                {
                    code.AppendLine(
                        $"            if (parameters[{i}].ParameterType != typeof({parameters[i].ParameterType.ToDetails()})) continue;");
                }

                // 判断返回值类型 需要考虑如果是泛型方法 返回值类型也是泛型的情况
                code.AppendLine(
                    $"            if (method.ReturnType != typeof({method.ReturnType.ToDetails()})) continue;");
            }
            else
            {
                code.AppendLine($"            var parameters = method.GetParameters();");
                var parameters = method.GetParameters();
                code.AppendLine($"            if (parameters.Length != {parameters.Length}) continue;");
                for (var i = 0; i < parameters.Length; i++)
                {
                    code.AppendLine(
                        $"            if (parameters[{i}].ParameterType != typeof({parameters[i].ParameterType.ToDetails()})) continue;");
                }

                code.AppendLine(
                    $"            if (method.ReturnType != typeof({method.ReturnType.ToDetails()})) continue;");
            }

            content.Replace("METHOD_CODE", code.ToString());

            // 是否为虚函数
            // 是否为静态函数
            // 是否为泛型函数
            // 是否为异步函数
            // 是否为扩展函数
            // 是否为属性
            // 是否为事件
            // 是否为构造函数
            // 是否为析构函数
            // 是否为操作符
            // 是否为接口
            // 是否为委托
            // 是否为抽象函数

            return content.ToString();
        }
    }
}