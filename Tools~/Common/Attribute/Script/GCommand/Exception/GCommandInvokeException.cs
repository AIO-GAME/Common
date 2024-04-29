#region

using System;

#endregion

/// <summary>
/// 命令执行异常
/// </summary>
internal class GCommandInvokeException : Exception
{
    private int    ID;
    private string Messages;

    public GCommandInvokeException(int id, string message)
    {
        Messages = message;
        ID       = id;
    }

    public override string Message => string.Format("命令执行异常 : {0} {1}", ID, Messages);
}