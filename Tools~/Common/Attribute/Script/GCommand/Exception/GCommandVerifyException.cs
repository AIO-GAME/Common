#region

using System;

#endregion

/// <summary>
/// 命令验证异常
/// </summary>
internal class GCommandVerifyException : Exception
{
    private int    ID;
    private string Messages;

    public GCommandVerifyException(int id, string messages)
    {
        Messages = messages;
        ID       = id;
    }

    public override string Message => string.Format("命令验证异常 : {0} {1}", ID, Messages);
}