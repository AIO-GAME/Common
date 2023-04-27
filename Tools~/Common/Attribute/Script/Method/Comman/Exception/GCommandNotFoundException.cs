using System;

internal class GCommandNotFoundException : Exception
{
    private int ID;
    public override string Message => string.Format("查询指定游戏命令异常 : {0}", ID);

    /// <summary>
    /// 查询指定游戏命令异常
    /// </summary>
    public GCommandNotFoundException(int id) : base()
    {
        ID = id;
    }
}