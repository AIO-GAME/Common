#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入数据
    /// </summary>
    public interface IWriteDataBytes
        : IWriteBool,
          IWriteByte,
          IWriteChar,
          IWriteDecimal,
          IWriteDouble,
          IWriteEnum,
          IWriteFloat,
          IWriteInt16,
          IWriteInt32,
          IWriteInt64,
          IWriteLength,
          IWriteSByte,
          IWriteString,
          IWriteUInt16,
          IWriteUInt32,
          IWriteUInt64 { }
}