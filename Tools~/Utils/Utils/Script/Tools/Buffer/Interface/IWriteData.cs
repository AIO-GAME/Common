namespace AIO
{
    /// <summary>
    /// 写入数据
    /// </summary>
    public partial interface IWriteData :
        IWriteBool,
        IWriteByte,
        IWriteChar,
        IWriteDecimal,
        IWriteDouble,
        IWriteEnum,
        IWriteFloat,
        IWriteInt16,
        IWriteInt32,
        IWriteInt64,
        IWriteJson,
        IWriteLength,
        IWriteSByte,
        IWriteString,
        IWriteUInt16,
        IWriteUInt32,
        IWriteUInt64
    {
    }
}