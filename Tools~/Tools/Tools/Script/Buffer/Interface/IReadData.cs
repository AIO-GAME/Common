namespace AIO
{
    /// <summary>
    /// 数据读取接口
    /// </summary>
    public interface IReadData :
        IReadBool,
        IReadByte,
        IReadChar,
        IReadDecimal,
        IReadDouble,
        IReadEnum,
        IReadFloat,
        IReadInt16,
        IReadInt32,
        IReadInt64,
        IReadJson,
        IReadLength,
        IReadSByte,
        IReadString,
        IReadUInt16,
        IReadUInt32,
        IReadUInt64
    {
    }
}