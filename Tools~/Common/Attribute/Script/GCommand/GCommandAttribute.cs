#region

using System;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

/// <summary>
/// 命令属性
/// 放置在 构造函数 抽象函数 非静态函数 虚函数 重写函数 函数泛型 都会自动过滤
/// 验证函数同样要求 并且不能包含参数
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class GCommandAttribute : Attribute
{
    /// <summary>
    /// 该类别的命令可以帮助您浏览它
    /// </summary>
    public string Category;

    /// <summary>
    /// 一些帮助显示，以便用户理解命令
    /// </summary>
    public string Help;

    /// <summary>
    /// 可以用来搜索命令的名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 可以搜索命令的快速名称(使其简短以快速访问命令)
    /// </summary>
    public string QuickName;

    /// <summary>
    /// 验证函数
    /// </summary>
    public string Validation;

    /// <summary>
    /// 游戏命令
    /// </summary>
    public GCommandAttribute(int id)
    {
        ID = id;
        if (string.IsNullOrEmpty(Name)) Name = id.ToString();
        HeaderList = Name.Split('/');
    }

    /// <summary>
    /// 命令ID
    /// </summary>
    public int ID { get; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title => HeaderList[0];

    /// <summary>
    /// 标题分段列表
    /// </summary>
    public string[] HeaderList { get; private set; }

    /// <summary>
    /// 方法
    /// </summary>
    internal MethodInfo CommandMethod { get; private set; }

    /// <summary>
    /// 参数
    /// </summary>
    internal ParameterInfo[] Parameters { get; private set; }

    /// <summary>
    /// 所属类
    /// </summary>
    internal Type RuntimeType { get; private set; }

    /// <summary>
    /// 验证函数 Validation
    /// </summary>
    internal MethodInfo ValidationMethod { get; private set; }

    /// <summary>
    /// 全局命名
    /// </summary>
    internal string FullName => string.Format("{0}.{1}", RuntimeType.FullName, CommandMethod.Name);

    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format(" ID:{0}\n 执行函数:{1}\n 参数信息:{2}\n 验证函数:{3}\n 帮助信息:{4}\n 命令指令:{5}",
                             ID, GetMethodInfo(CommandMethod), GetParameterInfo(Parameters), GetMethodInfo(ValidationMethod), Help, GetCommand());
    }

    internal string GetCommand()
    {
        if (Parameters == null || Parameters.Length == 0) return string.Format("[{0}]", ID);
        var info = Parameters.Select(param => { return param.ParameterType.Name; }).ToArray();
        return string.Format("[{0}:{1}]", ID, string.Join(",", info));
    }

    internal static string GetMethodInfo(in MethodInfo Method)
    {
        if (Method is null) return "Null";
        var str = new StringBuilder();
        str.AppendFormat("{0}", Method.Name);
        return str.ToString();
    }

    internal static string GetParameterInfo(params ParameterInfo[] parameters)
    {
        if (parameters is null || parameters.Length == 0) return "Null";
        var str = new StringBuilder();
        str.Append('(');
        foreach (var parameter in parameters) str.AppendFormat("[{0}_{1}|{2}],", parameter.Position, parameter.Name, parameter.ParameterType.Name);

        str.Remove(str.Length - 1, 1).Append(')');
        return str.ToString();
    }

    internal void Update(Type type, MethodInfo method)
    {
        RuntimeType   = type;
        CommandMethod = method;
        Parameters    = method.GetParameters();
        if (!string.IsNullOrEmpty(Validation))
        {
            ValidationMethod = type.GetMethod(Validation, BindingFlags.Static);
            if (ValidationMethod != null)
            {
                if (ValidationMethod.GetParameters().Length != 0)
                {
                    ValidationMethod = null;
                    throw new GCommandNotSupportedException(ID, "验证函数不支持 包含参数", type, method);
                }

                if (ValidationMethod.ReturnType != typeof(bool))
                {
                    ValidationMethod = null;
                    throw new GCommandNotSupportedException(ID, "验证函数不支持 返回值类型不为 Boolean", type, method);
                }
            }
        }
    }

    internal bool CheckParameters(params object[] args)
    {
        if (Parameters.Length != args.Length) return false;
        foreach (var item in Parameters)
            if (item.ParameterType != args[item.Position].GetType())
                return false;

        return true;
    }

    internal void Invoke(params object[] args)
    {
        if (CommandMethod is null) return;
        if (ValidationMethod != null)
            if ((bool)ValidationMethod.Invoke(null, null))
                CommandMethod.Invoke(null, args);

        CommandMethod.Invoke(null, args);
    }

    internal void Invoke()
    {
        if (CommandMethod is null) return;
        if (ValidationMethod != null)
            if ((bool)ValidationMethod.Invoke(null, null))
                CommandMethod.Invoke(null, null);

        CommandMethod.Invoke(null, null);
    }
}