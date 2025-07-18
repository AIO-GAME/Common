#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 数据读取接口
    /// </summary>
    public interface IReadDataBytes
        : IReadBool,
          IReadByte,
          IReadChar,
          IReadDecimal,
          IReadDouble,
          IReadEnum,
          IReadFloat,
          IReadInt16,
          IReadInt32,
          IReadInt64,
          IReadLength,
          IReadSByte,
          IReadString,
          IReadUInt16,
          IReadUInt32,
          IReadUInt64
    {
        /// <summary>
        /// 可读数据长度
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 检查是否有指定数据长度
        /// </summary>
        /// <param name="count">长度</param>
        /// <returns>Ture:满足 False:不满足</returns>
        bool CheckSize(int count);

        /// <summary>
        /// 跳过读取指定长度
        /// </summary>
        /// <param name="count">长度</param>
        void Skip(int count);
    }
}