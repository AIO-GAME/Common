#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 数据序列化
    /// </summary>
    public interface IBinData
        : IDisposable,
          IBinDeserialize,
          IBinSerialize,
          IReset,
          ICloneable { }
}