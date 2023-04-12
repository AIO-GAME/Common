using System.Collections.Generic;

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
        IWriteUInt64,
        IWriteICollection,
        IWriteIList,
        IWriteIDictionary
    {
        /// <summary>
        /// 写入二进制数据
        /// </summary>
        /// <param name="buffer">数据</param>
        void WriteData<T>(T buffer) where T : IBinSerialize, new();


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        void WriteDataArray<T>(in ICollection<T> collection) where T : IBinSerialize, new();
    }
}