#region

using System;

#endregion

/// <summary>
/// 游戏命令异常
/// </summary>
internal class GCommandException : Exception
{
    private int    ID;
    private string Messages;

    public GCommandException(int id, string messages)
    {
        ID       = id;
        Messages = messages;
    }

    public override string Message => string.Format("游戏命令异常 : {0} {1}", ID, Messages);
}