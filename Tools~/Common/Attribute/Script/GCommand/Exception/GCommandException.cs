using System;

/// <summary>
/// 游戏命令异常
/// </summary>
internal class GCommandException : Exception
{
    private int ID;
    private string Messages;
    public override string Message => string.Format("游戏命令异常 : {0} {1}", ID, Messages);

    public GCommandException(int id, string messages) : base()
    {
        ID = id;
        Messages = messages;
    }
}