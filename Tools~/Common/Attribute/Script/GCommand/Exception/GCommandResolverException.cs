#region

using System;

#endregion

/// <summary>
/// 游戏命令解析异常
/// </summary>
internal class GCommandResolverException : Exception
{
    private string Messages;

    /// <summary>
    /// 游戏命令解析异常
    /// </summary>
    public GCommandResolverException(string messages)
    {
        Messages = messages;
    }

    public override string Message => string.Format("游戏命令解析异常 : {0}", Messages);
}