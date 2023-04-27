﻿using System;
using System.Text;

internal class GCommandParameterNoMatchException : Exception
{
    public override string Message
    {
        get { return Messages; }
    }

    private string Messages { get; }

    /// <summary>
    /// 游戏命令参数不匹配
    /// </summary>
    public GCommandParameterNoMatchException(int id, params object[] objs)
    {
        var str = new StringBuilder();
        str.Append('[');
        foreach (var item in objs) str.AppendFormat("v={0},t={1}|", item, item.GetType());
        str.Remove(str.Length - 1, 1).Append(']');
        Messages = string.Format("未查找到指定命令 -> {0} {1}", id, str);
    }
}