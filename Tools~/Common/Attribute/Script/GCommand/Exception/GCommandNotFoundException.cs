#region

using System;

#endregion

internal class GCommandNotFoundException : Exception
{
    private int ID;

    /// <summary>
    /// 查询指定游戏命令异常
    /// </summary>
    public GCommandNotFoundException(int id)
    {
        ID = id;
    }

    public override string Message => string.Format("查询指定游戏命令异常 : {0}", ID);
}