using System;

internal class GCommandDuplicationKeyException : Exception
{
    private int ID;
    private string Name1;
    private string Name2;
    public override string Message => string.Format("游戏命令存在多个重复Key : {0} -> {1} != {2}", ID, Name1, Name2);

    /// <summary>
    /// 游戏命令Key存在多个
    /// </summary>
    public GCommandDuplicationKeyException(int id, string name1, string name2) : base()
    {
        ID = id;
        Name1 = name1;
        Name2 = name2;
    }
}