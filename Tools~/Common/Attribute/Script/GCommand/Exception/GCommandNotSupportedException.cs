using System;
using System.Reflection;

/// <summary>
/// 游戏命令不受支持
/// </summary>
internal class GCommandNotSupportedException : Exception
{
    private int ID;
    private string Messages;
    private string FullName;
    public override string Message => string.Format("游戏命令 {0} 暂不支持 {1}", ID, Messages);

    public GCommandNotSupportedException(int id, string message, Type type, MethodInfo method) : base()
    {
        FullName = string.Format("{0}.{1}", type.FullName, method.Name);
        Messages = message;
        ID = id;
    }
}