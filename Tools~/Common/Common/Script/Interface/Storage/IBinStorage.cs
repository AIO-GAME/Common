#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 二进制数据存储
    /// </summary>
    public interface IBinStorage
        : IDeserialize,
          ISerialize,
          IDisposable
    {
        /// <summary>
        /// 数据
        /// </summary>
        byte[] Data { get; }
    }
}