#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

#endregion

/// <summary>
/// 游戏命令系统
/// </summary>
public static class GCommandSystem
{
    private const  string REGEX    = @"^\[\d+:\s*((""[^""]*"")|(\d+))(,\s*((""[^""]*"")|(\d+)|(True|False)))*]$";
    private static Regex  CMDRegex = new Regex(REGEX);

    private static Dictionary<string, GCommandSearchDatabase> CommandSearchs;
    private static Dictionary<int, List<GCommandAttribute>>   CommandDic;
    private static Dictionary<int, string>                    CommandKeyName;

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initialize()
    {
        CommandSearchs = new Dictionary<string, GCommandSearchDatabase>();
        CommandDic     = new Dictionary<int, List<GCommandAttribute>>();
        CommandKeyName = new Dictionary<int, string>();
    }

    /// <summary>
    /// 注册
    /// </summary>
    public static void Register(IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes())
        foreach (var method in type.GetMethods())
            Register(type, method);
    }

    /// <summary>
    /// 注册
    /// </summary>
    public static void Register(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        foreach (var method in type.GetMethods())
            Register(type, method);
    }

    /// <summary>
    /// 注册
    /// </summary>
    public static void Register(Type type)
    {
        foreach (var method in type.GetMethods()) Register(type, method);
    }

    /// <summary>
    /// 注册
    /// </summary>
    public static void Register(Type type, MethodInfo method)
    {
        var attribute = method.GetCustomAttribute<GCommandAttribute>();
        if (attribute is null) return;

        if (!method.IsStatic)
            throw new GCommandNotSupportedException(attribute.ID, "非静态函数", type, method);

        if (method.IsConstructor)
            throw new GCommandNotSupportedException(attribute.ID, "构造函数", type, method);

        if (method.IsAbstract)
            throw new GCommandNotSupportedException(attribute.ID, "抽象函数", type, method);

        if (method.IsVirtual)
            throw new GCommandNotSupportedException(attribute.ID, "虚函数", type, method);

        if (method.IsGenericMethodDefinition)
            throw new GCommandNotSupportedException(attribute.ID, "泛型函数", type, method);

        attribute.Update(type, method);
        Add(attribute);
    }

    internal static void Add(GCommandAttribute attribute)
    {
        var list = attribute.Title.Split('/');
        var label = list[0];

        if (!CommandKeyName.ContainsKey(attribute.ID))
        {
            CommandKeyName.Add(attribute.ID, attribute.FullName);
            CommandDic.Add(attribute.ID, new List<GCommandAttribute>());
            CommandDic[attribute.ID].Add(attribute);
        }

        if (CommandKeyName[attribute.ID] == attribute.FullName)
        {
            CommandDic[attribute.ID].Add(attribute);

            if (!CommandSearchs.ContainsKey(label)) CommandSearchs.Add(label, new GCommandSearchDatabase(label));
            if (list.Length > 1)
            {
                var labels = new string[list.Length - 1];
                Array.ConstrainedCopy(list, 1, labels, 0, labels.Length);
                CommandSearchs[label].Add(new List<string>(labels), attribute);
            }
            else
            {
                CommandSearchs[label].Add(attribute);
            }
        }
        else
        {
            throw new GCommandDuplicationKeyException(attribute.ID, attribute.FullName, CommandKeyName[attribute.ID]);
        }
    }

    /// <summary>
    /// 打印
    /// </summary>
    public static void Debug()
    {
        foreach (var item in CommandSearchs) Console.WriteLine(item.Value.ToString());
    }

    /// <summary>
    /// 导出命令
    /// </summary>
    public static void Export(in string path)
    {
        var str = new StringBuilder();
        foreach (var item in CommandSearchs) str.Append(item.Value);

        var dicroot = Path.GetDirectoryName(path);
        if (!Directory.Exists(dicroot)) Directory.CreateDirectory(dicroot);
        File.WriteAllText(path, str.ToString(), Encoding.UTF8);
    }

    /// <summary>
    /// 解析字符串
    /// </summary>
    public static void Invoke(in string cmd)
    {
        if (CMDRegex.IsMatch(cmd))
        {
            var CMD = cmd.Substring(1, cmd.Length - 2);
            if (int.TryParse(CMD.Split(':')[0], out var id))
            {
                var argStr = CMD.Remove(0, id.ToString().Length + 1);
                var args = argStr.Split(',');
                var objs = new object[args.Length];
                for (var i = 0; i < objs.Length; i++)
                    if (int.TryParse(args[i], out var intValue))
                        objs[i] = intValue;
                    else if (bool.TryParse(args[i], out var boolValue))
                        objs[i] = boolValue;
                    else
                        objs[i] = args[i].Trim('\"');

                Invoke(id, objs);
                return;
            }
        }

        throw new GCommandResolverException(cmd);
    }

    /// <summary>
    /// 解析字符串
    /// </summary>
    public static void Invoke(in int cmd, params object[] obj)
    {
        if (CommandDic.TryGetValue(cmd, out var value))
        {
            foreach (var item in value)
                if (item.CheckParameters(obj))
                {
                    item.Invoke(obj);
                    return;
                }

            throw new GCommandParameterNoMatchException(cmd, obj);
        }

        throw new GCommandNotFoundException(cmd);
    }

    /// <summary>
    /// 解析字符串
    /// </summary>
    public static void Invoke(in int cmd)
    {
        if (CommandDic.TryGetValue(cmd, out var value))
        {
            foreach (var item in value)
                if (item.Parameters.Length == 0)
                {
                    item.Invoke();
                    return;
                }

            throw new GCommandParameterNoMatchException(cmd);
        }

        throw new GCommandNotFoundException(cmd);
    }

    /// <summary>
    /// 撤销注册命令
    /// </summary>
    public static void UnRegister() { }
}