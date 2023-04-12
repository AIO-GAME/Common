using System.Collections.Generic;

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
        IReadUInt64,
        IReadICollection,
        IReadIList,
        IReadIDictionary
    {
        /// <summary>
        /// 可读数据长度
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        T ReadData<T>() where T : IBinDeserialize, new();

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>值</returns>
        void ReadDataArray<T>(ICollection<T> collection) where T : IBinDeserialize, new();
    }
}